/******************************************************************************
Module:  ResourceLockTests.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Wintellect;
using Wintellect.Threading;
using Wintellect.Threading.ResourceLocks;
using Wintellect.Threading.ResourceLocks.Diagnostics;


///////////////////////////////////////////////////////////////////////////////


public static class Program {
   public static void Main() {
      UInt32 x = 5;
      x = InterlockedEx.CompareExchange(ref x, 7, 2);
      UInt32 y = InterlockedEx.CompareExchange(ref x, 7, 5);

      String[] notice = new String[] {
         "This tester app contains code which performs functional and performance",
         "tests against the various classes in Wintellect's Power Threading Library.",
         null,
         "The code in here can give you an idea of how to use some of these classes",
         "but it is not the purpose of this code.",
         null,
         "Also, please note that some of the classes in the Power Threading Library",
         "require Windows Vista. Attempting to use these classes on a pre-Vista OS",
         "will result in an exception such as EntryPointNotFoundException.",
         null
      };
      Array.ForEach(notice, Console.WriteLine);

      Process.GetCurrentProcess().PriorityBoostEnabled = false;

      // Performance: Compare performance of various locks
      PerfTest_ResourceLocks();

      // Functional: Test a specific ResourceLock-derived class
      //FuncTest_ResourceLock(new OptexResourceLock());

      // Functional: Test a specific ResourceLock-derived class
      //StressTest_ResourceLocks();

      // Functional: Test the Deadlock detector
      //FuncTest_DeadlockDetector();
   }

   private static void FuncTest_ResourceLock(ResourceLock rl) {
      FuncTest_ResourceLock rlt = new FuncTest_ResourceLock(8);
      rlt.Test(rl);
      //rlt.Test(new OneManyResourceLock());
      //rlt.Test(new OneManySpinResourceLock());
   }

   private static void StressTest_ResourceLocks() {
      StressTest_ResourceLock strl = new StressTest_ResourceLock(20, 1000 * 1000, 5, 2000);
      foreach (Type t in typeof(ResourceLock).Assembly.GetExportedTypes()) {
         if (!typeof(ResourceLock).IsAssignableFrom(t)) continue;    // Consider only ResourceLock-derived types
         if (typeof(ResourceLockObserver).IsAssignableFrom(t)) continue;    // Skip over diagnostic locks
         if (t == typeof(ResourceLock)) continue;    // Skip the base class

         Console.WriteLine("Stress testing: {0}", t.FullName);
         ResourceLock rl = (ResourceLock)Activator.CreateInstance(t);
         strl.Test(rl);
         Console.WriteLine();
      }
   }

   private static void PerfTest_ResourceLocks() {
      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
      const Int32 c_PerfIterations = 2 * 1000 * 1000;
      PerfTest_ResourceLocks cpt = new PerfTest_ResourceLocks(c_PerfIterations);
      cpt.CalcResults();
      Console.WriteLine("\nSorted Results:");
      cpt.ShowResultsByThreads();
      cpt.ShowResultsByType('\t');  // Use ',' for CSV file which can be loaded into Excel
   }


#if DEADLOCK_DETECTION
   #region Deadlock Detector Funcional Tests
   private static MonitorResourceLock s_lockA = new MonitorResourceLock();
   private static MonitorResourceLock s_lockB = new MonitorResourceLock();
   private static MonitorResourceLock s_lockC = new MonitorResourceLock();

   private static void FuncTest_Deadlock_Test(ResourceLock l, Boolean write1st, Boolean write2nd, Boolean expectedToFail) {
      try {
         try {
            l.Enter(write1st);
            {
               l.Enter(write2nd);
               l.Leave();
            }
            l.Leave();

            if (expectedToFail)
               throw new InvalidProgramException("No deadlock detected when expected.");
         }
         catch (Exception<DeadlockExceptionArgs>) {
            if (!expectedToFail)
               throw new InvalidProgramException("Deadlock detected when not expected.");
         }
      }
      catch (InvalidOperationException) {
         // This can happen if deadlock detector throws before taking
         // a lock and the we try to release the lock anyway
      }
      //DeadlockDetector.ForceClear();
   }

   private static void FuncTest_DeadlockDetector() {
      ResourceLock.PerformDeadlockDetection(true);    // Turn on deadlock detection

      // Exclusive lock: Owned, Recursive
      FuncTest_Deadlock_Test(new MonitorResourceLock(), true, true, false);    // AW/AW: no deadlock
      FuncTest_Deadlock_Test(new MonitorResourceLock(), false, false, false);  // AR/AR: no deadlock
      FuncTest_Deadlock_Test(new MonitorResourceLock(), true, false, false);   // AW/AR: no deadlock
      FuncTest_Deadlock_Test(new MonitorResourceLock(), false, true, false);   // AR/AW: no deadlock

      // Reader/Writer lock: Owned, Recursive
      FuncTest_Deadlock_Test(new ReaderWriterSlimResourceLock(), true, true, false);    // AW/AW: no deadlock
      FuncTest_Deadlock_Test(new ReaderWriterSlimResourceLock(), false, false, false);  // AR/AR: no deadlock
      FuncTest_Deadlock_Test(new ReaderWriterSlimResourceLock(), true, false, true);    // AW/AR: deadlock
      FuncTest_Deadlock_Test(new ReaderWriterSlimResourceLock(), false, true, true);    // AR/AW: deadlock

      // Reader/Writer lock: Unowned, Non-recursive
      FuncTest_Deadlock_Test(new OneManyResourceLock(), true, true, true);      // AW/AW: deadlock
      FuncTest_Deadlock_Test(new OneManyResourceLock(), false, false, false);    // AR/AR: no deadlock
      FuncTest_Deadlock_Test(new OneManyResourceLock(), true, false, true);      // AW/AR: deadlock
      FuncTest_Deadlock_Test(new OneManyResourceLock(), false, true, true);      // AR/AW: deadlock

      // Reader/Writer lock: Owned, Non-recursive (not a valid combination)


      s_lockA.Name = "Lock A";
      s_lockB.Name = "Lock B";
      s_lockC.Name = "Lock C";

      s_lockA.Enter(true);
      Thread t = new Thread(delegate(Object o) {
         Thread.Sleep(2000);
         s_lockA.Enter(true);
      });
      t.Start();
      //DeadlockDetector.BlockForJoin(t, Timeout.Infinite);
      s_lockA.Leave();
      FuncTest_DeadlockDetector_LockA(null);
   }

   private static void FuncTest_DeadlockDetector_LockA(Object o) {
      s_lockA.Enter(true);
      ThreadPool.QueueUserWorkItem(FuncTest_DeadlockDetector_LockB);
      MessageBox.Show("Let thread 1 get B");
      s_lockB.Enter(true);
      s_lockB.Leave();

      s_lockA.Leave();
   }

   private static void FuncTest_DeadlockDetector_LockB(Object o) {
      s_lockB.Enter(true);
      AppDomain ad = AppDomain.CreateDomain("Other AD", null, null);
      ThreadPool.QueueUserWorkItem(delegate { ad.DoCallBack(FuncTest_DeadlockDetector_LockC); });
      MessageBox.Show("Let thread 2 get C");
      s_lockC.Enter(false);
      s_lockC.Leave();

      s_lockB.Leave();
   }

   private static void FuncTest_DeadlockDetector_LockC() {
      s_lockC.Enter(false);

      MessageBox.Show("Let thread 3 get A");
      s_lockA.Enter(false);
      s_lockA.Leave();

      s_lockC.Leave();
   }
   #endregion
#endif
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class FuncTest_ResourceLock {
   private ManualResetEvent[] m_Threads;
   private ResourceLock m_ResourceLock;

   public FuncTest_ResourceLock(Int32 numThreads) {
      m_Threads = new ManualResetEvent[numThreads];
   }

   public void Test(ResourceLock rl) {
      m_ResourceLock = new ThreadSafeCheckerResourceLockObserver(rl);

      // Spawn a bunch of threads that will attempt to read/write
      for (Int32 ThreadNum = 0; ThreadNum < m_Threads.Length; ThreadNum++) {
         m_Threads[ThreadNum] = new ManualResetEvent(false);
         ThreadPool.QueueUserWorkItem(ThreadFunc, ThreadNum);
      }
      WaitHandle.WaitAll(m_Threads);
      Console.WriteLine(m_ResourceLock.ToString());
   }

   private void ThreadFunc(Object state) {
      String caption = String.Format("ResourceLock FuncTest_Deadlock_Test: Thread {0}", state);
      DialogResult dr = MessageBox.Show("YES: Attempt to write\nNO: Attempt to read",
          caption, MessageBoxButtons.YesNo);
      // Attempt to read or write
      m_ResourceLock.Enter(dr == DialogResult.Yes);
      MessageBox.Show(m_ResourceLock.ToString() + Environment.NewLine +
          ((dr == DialogResult.Yes) ? "OK stops WRITING" : "OK stops READING"),
          caption, MessageBoxButtons.OK);
      m_ResourceLock.Leave();
      m_Threads[(Int32)state].Set();
   }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class PerfTest_ResourceLock {
   private Int32 m_iterations;
   public PerfTest_ResourceLock(Int32 iterations) {
      m_iterations = iterations;
   }

   public TimeSpan Test(Boolean write, Int32 threadCount, ResourceLock rl) {
      // Make sure that the methods are JITted so that JIT time is not included in the results
      rl.Enter(false); rl.Leave();
      rl.Enter(true); rl.Leave();
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      try {
         Thread.CurrentThread.Priority = ThreadPriority.Highest;
         Stopwatch stopWatch = Stopwatch.StartNew();
         Thread[] threads = new Thread[threadCount - 1];
         for (Int32 t = 0; t < threads.Length - 1; t++) {
            threads[t] = new Thread((ThreadStart)delegate { Loop(write, rl); });
            threads[t].Name = "FuncTest_Deadlock_Test thread #" + t;
            threads[t].Start();
         }
         Loop(write, rl);
         for (Int32 t = 0; t < threads.Length - 1; t++) {
            threads[t].Join();
            //threads[ls].Dispose();
         }
         return stopWatch.Elapsed;
      }
      finally {
         Thread.CurrentThread.Priority = ThreadPriority.Normal;
      }
   }

   private void Loop(Boolean write, ResourceLock rl) {
      Int32 z = 0;
      for (Int32 x = 0; x < m_iterations; x++) {
         if (write) { rl.Enter(true); z = x; rl.Leave(); } else { rl.Enter(false); Int32 y = z; rl.Leave(); }
      }
   }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class StressTest_ResourceLock {
   private readonly Int32 m_threadCount;
   private readonly Int32 m_iterations;
   private readonly Int32 m_readerWriterRatio;
   private readonly Int32 m_workSpinCount;

   public StressTest_ResourceLock(Int32 threadCount, Int32 iterations, Int32 readerWriterRatio, Int32 workSpinCount) {
      m_threadCount = threadCount;
      m_iterations = iterations;
      m_readerWriterRatio = readerWriterRatio;
      m_workSpinCount = workSpinCount;
   }

   public void Test(ResourceLock rl) {
      // Use the thread-safety checker and the statistics gatherer in case anything goes wrong
      rl = new StatisticsGatheringResourceLockObserver(new ThreadSafeCheckerResourceLockObserver(rl));

      Thread[] threads = new Thread[m_threadCount - 1];
      for (Int32 t = 0; t < threads.Length - 1; t++) {
         threads[t] = new Thread((ThreadStart)delegate { StressLoop(rl); });
         threads[t].Name = "FuncTest_Deadlock_Test thread #" + t;
         threads[t].Start();
      }
      StressLoop(rl); // This thread will do it too
      for (Int32 t = 0; t < threads.Length - 1; t++)
         threads[t].Join();
   }

   private void StressLoop(ResourceLock rl) {
      Random random = new Random((Int32)DateTime.Now.Ticks);
      for (Int32 i = 0; i < m_iterations; i++) {
         if ((i > 0) && (i % (m_iterations / 10)) == 0)
            Console.WriteLine("   {0}: iteration={1}", Thread.CurrentThread.Name, i);

         rl.Enter(random.Next(m_readerWriterRatio) == 0);
         for (Int32 work = 0; work < m_workSpinCount; work++) ;
         rl.Leave();
      }
   }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class PerfTest_ResourceLocks {
   private IList<ResourceLock> m_Locks = new List<ResourceLock>();
   private Int32[] m_NumThreads = new Int32[] { 1, 2 };
   private TimeSpan[, ,] m_ResultTimes;
   private Int32 m_iterations;
   private PerfTest_ResourceLock m_pt;

   public PerfTest_ResourceLocks(Int32 iterations) {
      m_Locks.Add(new MonitorResourceLock());
      m_Locks.Add(new NullResourceLock());
      m_Locks.Add(new OneManyResourceLock());
      //m_Locks.Add(new RecursionResourceLock(new OneManyResourceLock(), 10));
      //m_Locks.Add(new ReaderWriterSlimResourceLock());

      m_iterations = iterations;
      m_pt = new PerfTest_ResourceLock(m_iterations);
      m_ResultTimes = new TimeSpan[m_Locks.Count, 2, m_NumThreads.Length];
   }

   public void CalcResults() {
      String formatStr = "{0} Threads={1}, {2} Type={3}";
      for (Int32 rlIndex = 0; rlIndex < m_Locks.Count; rlIndex++) {
         for (Int32 threadIndex = 0; threadIndex < m_NumThreads.Length; threadIndex++) {
            foreach (Boolean write in new Boolean[] { false, true }) {
               Console.Write(formatStr, "Testing", m_NumThreads[threadIndex],
                   write ? "Writing" : "Reading", m_Locks[rlIndex].GetType().Name);

               TimeSpan result = m_pt.Test(write, m_NumThreads[threadIndex], m_Locks[rlIndex]);

               Console.SetCursorPosition(0, Console.CursorTop);
               Console.WriteLine(formatStr, result, m_NumThreads[threadIndex],
                   write ? "Writing" : "Reading",
                   m_Locks[rlIndex].GetType().Name);
               m_ResultTimes[rlIndex, write ? 1 : 0, threadIndex] = result;
            }
         }
      }
   }

   public void ShowResultsByThreads() {
      for (Int32 threadIndex = 0; threadIndex < m_NumThreads.Length; threadIndex++) {
         List<String> output = new List<String>(m_Locks.Count);
         for (Int32 rlIndex = 0; rlIndex < m_Locks.Count; rlIndex++) {
            output.Add(String.Format("{0}\t{1}\t{2}",
                m_ResultTimes[rlIndex, 0, threadIndex].ToString().Substring(6, 6),
                m_ResultTimes[rlIndex, 1, threadIndex].ToString().Substring(6, 6),
                m_Locks[rlIndex].GetType().Name));
         }
         output.Sort();
         Console.WriteLine();
         Console.WriteLine("Performance results where # of threads={0} (ordered by read time)", m_NumThreads[threadIndex]);
         output.ForEach(delegate(String s) { Console.WriteLine(s); });
      }
   }

   public void ShowResultsByType(Char delimiter) {
      List<String> output = new List<String>(m_Locks.Count);
      for (Int32 rlIndex = 0; rlIndex < m_Locks.Count; rlIndex++) {
         StringBuilder sb = new StringBuilder(String.Format("{0,-30}", m_Locks[rlIndex].GetType().Name));
         for (Int32 threadIndex = 0; threadIndex < m_NumThreads.Length; threadIndex++) {
            sb.AppendFormat("{0}{1}{0}{2}", delimiter,
               m_ResultTimes[rlIndex, 0, threadIndex].ToString().Substring(6, 6),
               m_ResultTimes[rlIndex, 1, threadIndex].ToString().Substring(6, 6));
         }
         output.Add(sb.ToString());
      }
      output.Sort();
      Console.WriteLine();
      Console.WriteLine("Performance results sorted by type");
      output.ForEach(Console.WriteLine);
   }
}


//////////////////////////////// End of File //////////////////////////////////