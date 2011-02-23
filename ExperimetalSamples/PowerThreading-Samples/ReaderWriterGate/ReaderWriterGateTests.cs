/******************************************************************************
Module:  ReaderWriterGateTests.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using Wintellect.Threading;
using Wintellect.Threading.ReaderWriterGate;


///////////////////////////////////////////////////////////////////////////////


public static class Program {
    public static void Main() {
#if false
        // Functional test
        {
            FuncTest_ReaderWriterGate ftrwg = new FuncTest_ReaderWriterGate();
            ReaderWriterGate gate = new ReaderWriterGate(false);
            ftrwg.SimpleTest(gate);
            ftrwg.TestAll(gate);
        }
#endif
        // Return value test
        {
            ReaderWriterGate gate = new ReaderWriterGate(false);
            gate.BeginRead(releaser => {
                releaser.ResultValue = "Releasing";
                releaser.Release(false);
                Thread.Sleep(5000);
                Object o = null;
                o.GetType();
                return "Returning";

            }, ar => Console.WriteLine(gate.EndRead(ar)));
            Console.ReadLine();
        }
        // Performance test
        {
            const Int32 c_PerfIterations = 10 * 1000 * 1000;

            ReaderWriterGate gate = new ReaderWriterGate();
            PerformanceTest pt = new PerformanceTest(c_PerfIterations);
            foreach (Boolean write in new Boolean[] { false, true }) {
                Console.WriteLine("{0} {1} {2}",
                   pt.Test(write, gate), write ? "writing" : "reading", gate.ToString());
            }
        }
    }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class PerformanceTest {
   private Int32 m_iterations;
   public PerformanceTest(Int32 iterations) {
      m_iterations = iterations;
   }

   public TimeSpan Test(Boolean exclusive, ReaderWriterGate gate) {
      ReaderWriterGateCallback gateCallback = delegate(ReaderWriterGateReleaser releaser) { return null; };
      gateCallback(null);
      gate.BeginRead(gateCallback, delegate(IAsyncResult result) { gate.EndRead(result); });
      gate.BeginWrite(gateCallback, delegate(IAsyncResult result) { gate.EndWrite(result); });
      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      Stopwatch stopWatch = Stopwatch.StartNew();
      for (Int32 x = 0; x < m_iterations; x++) {
         if (!exclusive) gate.BeginRead(gateCallback, delegate(IAsyncResult result) { gate.EndRead(result); });
         else gate.BeginWrite(gateCallback, delegate(IAsyncResult result) { gate.EndWrite(result); });
      }
      return stopWatch.Elapsed;
   }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class FuncTest_ReaderWriterGate {
   private Int32 m_NumReaders = 0;
   private Int32 m_NumWriters = 0;
   private Int32 m_NumConcurrentWriters = 0;
   private AutoResetEvent m_are;

   public FuncTest_ReaderWriterGate() { }

   public void Test(ReaderWriterGate gate, Int32 numInitialReaders, Int32 numInitialWriters,
      Int32 numAdditionalReaders, Int32 numAdditionalWriters) {

      Console.WriteLine();
      Console.WriteLine("FuncTest_Deadlock_Test: WR={0}, WW={1}, QR={2}, QW={3}",
         numInitialReaders, numInitialWriters, numAdditionalReaders, numAdditionalWriters);

      Thread.VolatileWrite(ref m_NumReaders, 0);
      Thread.VolatileWrite(ref m_NumWriters, 0);
      Thread.VolatileWrite(ref m_NumConcurrentWriters, 0);

      using (m_are = new AutoResetEvent(false)) {
         // Queue up a bunch of read requests
         for (Int32 i = 0; i < numInitialReaders; i++) {
            ThreadPool.QueueUserWorkItem(
               delegate { gate.BeginRead(ReadAndWait, delegate(IAsyncResult result) { gate.EndRead(result); }); });
         }

         // Make sure the readers have started to be processed
         Thread.Sleep(100);

         // Queue up a bunch of write requests (these should wait)
         for (Int32 i = 0; i < numInitialWriters; i++) {
            ThreadPool.QueueUserWorkItem(
               delegate { gate.BeginWrite(WriteAndWait, delegate(IAsyncResult result) { gate.EndWrite(result); }); });
         }

         // Make sure the writers have been queued
         Thread.Sleep(100);

         // Since readers own the gate with writers pending, all of these additional readers should queue
         for (int i = 0; i < numAdditionalReaders; i++) {
            gate.BeginRead(ReadResourceCallback, i, delegate(IAsyncResult result) { gate.EndRead(result); }, null);
         }

         // Since writers own the gate, these writers should be processed before the additional readers
         for (int i = 0; i < numAdditionalWriters; i++) {
            gate.BeginWrite(WriteResourceCallback, i, delegate(IAsyncResult result) { gate.EndWrite(result); }, null);
         }

         // Now, release the waiting reader threads
         for (int i = 0; i < numInitialReaders + numInitialWriters; i++) {
            m_are.Set();
            Thread.Sleep(50);
         }

         while (Thread.VolatileRead(ref m_NumReaders) != numInitialReaders + numAdditionalReaders) 
            Thread.Sleep(1);
         Console.WriteLine("All readers done");

         while (Thread.VolatileRead(ref m_NumWriters) != numInitialWriters + numAdditionalWriters) 
            Thread.Sleep(1);
         Console.WriteLine("All readers & writers done");
      }
   }

   public void TestAll(ReaderWriterGate gate) {
      Test(gate, 0, 0, 1, 0); // Uncontended Read test
      Test(gate, 0, 0, 0, 1); // Uncontended Write test
      Test(gate, 0, 1, 1, 0); // A writer writing and a reader queued up
      Test(gate, 0, 1, 5, 0); // A writer writing and several readers queued up
      Test(gate, 0, 1, 0, 5); // A writer writing and several writers queued up
      Test(gate, 0, 1, 5, 5); // A writer writing and several readers & writers queued up
      Test(gate, 1, 0, 1, 5); // A reader reading and a writer queued up
      Test(gate, 5, 1, 0, 1); // Several readers reading and a writer queued up
   }

   private Object ReadAndWait(ReaderWriterGateReleaser r) {
      Console.WriteLine("Waiting reader in : " + m_NumReaders);
      m_are.WaitOne();
      Console.WriteLine("Waiting reader out: " + m_NumReaders);
      Interlocked.Increment(ref m_NumReaders);
      return null;
   }

   private Object ReadResourceCallback(ReaderWriterGateReleaser releaser) {
      Console.WriteLine("Reader in : {0} ({1})", m_NumReaders, releaser.State);
      Thread.Sleep(750);
      Console.WriteLine("Reader out: {0} ({1})", m_NumReaders, releaser.State);
      Interlocked.Increment(ref m_NumReaders);
      return null;
   }

   private Object WriteAndWait(ReaderWriterGateReleaser releaser) {
      if (Interlocked.Increment(ref m_NumConcurrentWriters) != 1) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Launch();
      }
      Console.WriteLine("Waiting writer in : {0} ({1})", m_NumWriters, releaser.State);
      m_are.WaitOne();
      Console.WriteLine("Waiting writer out: {0} ({1})", m_NumWriters, releaser.State);
      Interlocked.Increment(ref m_NumWriters);
      if (Interlocked.Decrement(ref m_NumConcurrentWriters) != 0) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Launch();
      }
      return null;
   }

   private Object WriteResourceCallback(ReaderWriterGateReleaser releaser) {
      Console.WriteLine("Writer in : " + m_NumWriters);
      if (Interlocked.Increment(ref m_NumConcurrentWriters) != 1) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Break();
      }
      Thread.Sleep(750);
      Console.WriteLine("Writer out: " + m_NumWriters);
      Interlocked.Increment(ref m_NumWriters);
      if (Interlocked.Decrement(ref m_NumConcurrentWriters) != 0) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Break();
      }
      return null;
   }

   private Object WriteResourceCallbackUI(ReaderWriterGateReleaser releaser) {
      Console.WriteLine("Writer in : " + m_NumWriters);
      if (Interlocked.Increment(ref m_NumConcurrentWriters) != 1) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Break();
      }
      System.Windows.Forms.MessageBox.Show("Writer in: " + m_NumWriters);
      Console.WriteLine("Writer out: " + m_NumWriters);
      Interlocked.Increment(ref m_NumWriters);
      if (Interlocked.Decrement(ref m_NumConcurrentWriters) != 0) {
         // More than 1 concurrent writer; this should never happen
         Debugger.Break();
      }
      return null;
   }

   public void SimpleTest(ReaderWriterGate gate) {
      Byte stop = 0;
      // Start a thread that posts periodic read requests
      ThreadPool.QueueUserWorkItem(delegate {
         for (Int32 x = 0; Thread.VolatileRead(ref stop) == 0; x++) {
            gate.BeginRead(ReadResourceCallback, x, delegate(IAsyncResult result) { gate.EndRead(result); }, null);
            Thread.Sleep(500);
         }
      });

      // While Read requests are being posted in the background, periodically post some write requests
      Int32 numWritersToQueue = 1;
      Thread.Sleep(2000);
      for (Int32 x = 0; numWritersToQueue > 0; x++) {
         for (int i = 0; i < numWritersToQueue; i++) {
            gate.BeginWrite(WriteResourceCallbackUI, x, delegate(IAsyncResult result) { gate.EndWrite(result); }, null);
         }
         Console.WriteLine("Writer request");
         if (!Int32.TryParse(Console.ReadLine(), out numWritersToQueue))
            numWritersToQueue = 1;
      }
      Thread.VolatileWrite(ref stop, 1);
      Console.WriteLine("Done queuing, hit <Enter> to exit test");
      Console.ReadLine();
   }
}


///////////////////////////////////////////////////////////////////////////////

#if false
namespace RssFetcher {
   using System.Text;
   using System.IO;
   using System.Net;

   internal sealed class Fetcher {
      private DateTime m_lastUpdate = DateTime.MinValue;

      // Pass true to prevent readers from reading until we have data
      private ReaderWriterGate m_gate = new ReaderWriterGate(true);

      private String url = @"http://xyz.com";
      private String m_filename = @"C:\foo.txt";

      // Called when a client wants the latest RSS info
      public IAsyncResult BeginFetchRss(String strUrl) {
         // If we don't have data or it has been 10 minutes since we last got data, get new data
         if ((m_lastUpdate == DateTime.MinValue) || DateTime.Now > m_lastUpdate + TimeSpan.FromMilliseconds(10)) {
            // Issue the I/O request asynchronously to update the data
            WebRequest wr = WebRequest.Create(url);
            wr.BeginGetResponse(delegate(IAsyncResult ar) {
               // This executes when the data has come in
               Byte[] bytes = new Byte[1000];
               wr.EndGetResponse(ar).GetResponseStream().Read(bytes, 0, bytes.Length);
               String s = Encoding.ASCII.GetString(bytes);

               // Update the local file when we can have exclusive access to it
               m_gate.QueueWrite(UpdateFile, s);
            }, null);
         }

         ClientState cs = new ClientState();
         // For this client request, read data from the local file and return it when we can have read access to it
         return m_gate.BeginRead(ReadDataFromFile, cs, ReturnDataToClient, cs);
      }

      private void UpdateFile(ReaderWriterGateReleaser r) {
         // Update the file (NOTE: should be async I/O if file is on remote server)
         File.WriteAllText(m_filename, (String)r.State);

         // Update the time of the last update
         m_lastUpdate = DateTime.Now;
      }

      // This can execute when no thread is updating the file
      private void ReadDataFromFile(Wintellect.Threading.ReaderWriterGate.ReaderWriterGateReleaser r) {
         ClientState cs = (ClientState)r.State;
         // Read the file and save the data that is to be sent back to the client
         // NOTE: Should be async I/O if file is on remote server
         cs.m_data = File.ReadAllText(m_filename);
      }

      private void ReturnDataToClient(IAsyncResult ar) {
         // Called when data can be returned to the client
         ClientState cs = (ClientState)ar.AsyncState;
         // TODO: send what’s in cs.m_data back to the client
      }

      private sealed class ClientState {
         public Object m_data;
      }
   }
}
#endif


//////////////////////////////// End of File //////////////////////////////////
