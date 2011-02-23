/******************************************************************************
Module:  APMImplementationTests.cs
Notices: Copyright (c) 2006-2008 by Jeffrey Richter and Wintellect
******************************************************************************/


using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Wintellect;
using Wintellect.Threading;
using Wintellect.Threading.AsyncProgModel;


///////////////////////////////////////////////////////////////////////////////


public static class Program {
   public static void Main() {
      // Functional: Test the APM Implementations
      FunctionalTest();

      // Performance: Compare the performance of various APM Implementations
      PerformanceTest();

      // Functional: How to derive from AsyncResult<TResult> and performs an async I/O operation
      AsyncIOWithResult.Test();

      // Functional: How to defive from AsyncResultNoResult and perform an async I/O operation
      AsyncIONoResult.Test();
   }

   private static APMTestClass s_apmTestClass = new APMTestClass();

   #region FunctionalTests
   public static void FunctionalTest() {
      // Functional tests: No callback 
      s_apmTestClass.Compute(false);
      try { s_apmTestClass.Compute(true); }
      catch (InvalidOperationException) { Console.WriteLine("Caught"); }

      s_apmTestClass.EndComputeDelegate(s_apmTestClass.BeginComputeDelegate(false, null, null));
      try { s_apmTestClass.EndComputeDelegate(s_apmTestClass.BeginComputeDelegate(true, null, null)); }
      catch (InvalidOperationException) { Console.WriteLine("Caught"); }

#if false
      s_apmTestClass.EndComputeReflection(s_apmTestClass.BeginComputeReflection(false, null, null));
      try { s_apmTestClass.EndComputeReflection(s_apmTestClass.BeginComputeReflection(true, null, null)); }
      catch (InvalidOperationException) { Console.WriteLine("Caught"); }
#endif

      s_apmTestClass.EndComputeSpecific(s_apmTestClass.BeginComputeSpecific(false, null, null));
      try { s_apmTestClass.EndComputeSpecific(s_apmTestClass.BeginComputeSpecific(true, null, null)); }
      catch (InvalidOperationException) { Console.WriteLine("Caught"); }

      // Functional tests: Callback 
      s_apmTestClass.BeginComputeDelegate(false, delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeDelegate(ar);
      }, s_apmTestClass);

#if false
      s_apmTestClass.BeginComputeReflection(false, delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeReflection(ar);
      }, s_apmTestClass);
#endif

      s_apmTestClass.BeginComputeSpecific(false, delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeSpecific(ar);
      }, s_apmTestClass);

      Thread.Sleep(2000);
      Console.WriteLine("Functional tests complete.");
   }
   #endregion

   #region PerformanceTests
   public static void PerformanceTest() {
      const Int32 count = 100 * 1000;

      // Performance tests: No callback
      using (new OperationTimer("Compute, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.Compute(false);
         }
      }

      using (new OperationTimer("BeginComputeDelegate, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.EndComputeDelegate(s_apmTestClass.BeginComputeDelegate(false, null, null));
         }
      }

#if false
      using (new OperationTimer("BeginComputeReflection, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.EndComputeReflection(s_apmTestClass.BeginComputeReflection(false, null, null));
         }
      }
#endif

      using (new OperationTimer("BeginComputeSpecific, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.EndComputeSpecific(s_apmTestClass.BeginComputeSpecific(false, null, null));
         }
      }

      // Performance tests: Callback
      AutoResetEvent done = new AutoResetEvent(false);
      Int32 countdown;

      countdown = count;
      AsyncCallback acDelegate = delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeDelegate(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginComputeDelegate, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.BeginComputeDelegate(false, acDelegate, s_apmTestClass);
         }
         done.WaitOne();
      }

#if false
      countdown = count;
      AsyncCallback acReflection = delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeReflection(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginComputeReflection, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.BeginComputeReflection(false, acReflection, s_apmTestClass);
         }
         done.WaitOne();
      }
#endif


      countdown = count;
      AsyncCallback acSpecific = delegate(IAsyncResult ar) {
         ((APMTestClass)ar.AsyncState).EndComputeSpecific(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginComputeSpecific, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_apmTestClass.BeginComputeSpecific(false, acSpecific, s_apmTestClass);
         }
         done.WaitOne();
      }

      Console.WriteLine("Performance tests complete.");
      Console.ReadLine();
   }
   #endregion
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class APMTestClass {
   private delegate String ComputeDelegate(Boolean fail);
   private static ComputeDelegate s_Compute;
#if false
   private static MethodInfo s_MethodInfo;
#endif

   public String Compute(Boolean fail) {
      if (fail) throw new InvalidOperationException();
      return null;
   }

   public IAsyncResult BeginComputeDelegate(Boolean fail, AsyncCallback ac, Object state) {
      if (s_Compute == null) {
         Interlocked.CompareExchange(ref s_Compute, new ComputeDelegate(Compute), null);
      }
      return s_Compute.BeginInvoke(fail, ac, state);
   }
   public String EndComputeDelegate(IAsyncResult ar) {
      return s_Compute.EndInvoke(ar);
   }

#if false
   public IAsyncResult BeginComputeReflection(Boolean fail, AsyncCallback ac, Object state) {
      if (s_MethodInfo == null) {
         Interlocked.CompareExchange(ref s_MethodInfo, typeof(APMTestClass).GetMethod("Compute"), null);
      }
      return new AsyncResultReflection<String>(ac, state, this, s_MethodInfo, fail);
   }
   public String EndComputeReflection(IAsyncResult ar) {
      return ((AsyncResultReflection<String>)ar).EndInvoke();
   }
#endif

   private class ComputeAsyncResult : AsyncResult<String> {
      // State needed to do the asynchronous operation
      private readonly APMTestClass m_target;
      private readonly Boolean m_fail;

      public ComputeAsyncResult(AsyncCallback ac, Object state, APMTestClass target, Boolean fail)
         : base(ac, state) {
         m_target = target;
         m_fail = fail;
         BeginInvokeOnWorkerThread();
      }
      protected override String OnCompleteOperation(IAsyncResult ar) {
         return m_target.Compute(m_fail);
      }
   }

   public IAsyncResult BeginComputeSpecific(Boolean fail, AsyncCallback ac, Object state) {
      return new ComputeAsyncResult(ac, state, this, fail);
   }

   public String EndComputeSpecific(IAsyncResult ar) {
      return ((ComputeAsyncResult)ar).EndInvoke();
   }
}


///////////////////////////////////////////////////////////////////////////////


// This shows how to derive from AsyncResult<TResult> and create a method that
// internally performs an async I/O operation (not a compute operation)
internal sealed class AsyncIOWithResult : AsyncResult<String> {
   private static AsyncIOWithResult s_object;

   public static void Test() {
      AsyncIOWithResult o = new AsyncIOWithResult(delegate(IAsyncResult ar) {
         Console.WriteLine(s_object.EndInvoke());
      }, null);

      Console.WriteLine("Enter <Enter> when you see the result");
      Console.ReadLine();
   }

   private FileStream m_fs;
   private Byte[] m_bytes = new Byte[10000];

   public AsyncIOWithResult(AsyncCallback ac, Object state)
      : base(ac, state) {
      s_object = this;
      m_fs = new System.IO.FileStream(@"c:\boot.ini", FileMode.Open);
      m_fs.BeginRead(m_bytes, 0, m_bytes.Length, GetAsyncCallbackHelper(), this);
   }

   protected sealed override String OnCompleteOperation(IAsyncResult ar) {
      Int32 x = m_fs.EndRead(ar);
      m_fs.Close();
      Array.Resize(ref m_bytes, x);
      return Encoding.ASCII.GetString(m_bytes);
   }
}


///////////////////////////////////////////////////////////////////////////////


internal sealed class AsyncIONoResult : AsyncResult {
   private static AsyncIONoResult s_object;

   public static void Test() {
      AsyncIONoResult o = new AsyncIONoResult(delegate(IAsyncResult ar) {
         s_object.EndInvoke();
         Console.WriteLine("Done reading");
      }, null);

      Console.WriteLine("Enter <Enter> when you see the result");
      Console.ReadLine();
   }

   private FileStream m_fs;
   private Byte[] m_bytes = new Byte[10000];

   public AsyncIONoResult(AsyncCallback ac, Object state)
      : base(ac, state) {
      s_object = this;
      m_fs = new System.IO.FileStream(@"c:\boot.ini", FileMode.Open);
      m_fs.BeginRead(m_bytes, 0, m_bytes.Length, GetAsyncCallbackHelper(), this);
   }

   protected sealed override void OnCompleteOperation(IAsyncResult ar) {
      Int32 x = m_fs.EndRead(ar);
      m_fs.Close();
      Array.Resize(ref m_bytes, x);
      String s = Encoding.ASCII.GetString(m_bytes);
      Console.WriteLine(s);
   }
}


//////////////////////////////// End of File //////////////////////////////////
#if false
using System;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Wintellect.Threading;
using Wintellect.Threading.AsyncProgModel;


///////////////////////////////////////////////////////////////////////////////


internal class MathTest {
   // Method that does the operation synchronously
   public void ComputeOp() { }


#region Delegates
   private delegate void ComputeOpDelegate();

   private static ComputeOpDelegate s_ComputeOp;

   // Method that starts the operation off asynchronously
   public IAsyncResult BeginComputeOp(AsyncCallback ac, Object state) {
      if (s_ComputeOp == null) {
         Interlocked.CompareExchange(ref s_ComputeOp, new ComputeOpDelegate(ComputeOp), null);
      }
      return s_ComputeOp.BeginInvoke(ac, state);
   }

   // Method that ends the asynchronous operation
   public void EndComputeOp(IAsyncResult ar) {
      s_ComputeOp.EndInvoke(ar);
   }
   #endregion


#region Reflection
   public IAsyncResult BeginDivideReflection(Int32 n, Int32 d, AsyncCallback ac, Object state) {
      MethodInfo mi = typeof(MathTest).GetMethod("Divide");
      return new AsyncResultReflection<Int32>(ac, state, this, mi, n, d);
   }
   public Int32 EndDivideReflection(IAsyncResult ar) {
      return ((AsyncResultReflection<Int32>)ar).EndInvoke();
   }
   #endregion


#region Specific
   internal class AsyncResultDivide : AsyncResult<Int32> {
      // State needed to do the asynchronous operation
      private readonly MathTest m_target;
      private readonly Int32 m_numerator, m_denominator;

      public AsyncResultDivide(AsyncCallback ac, Object state, MathTest target, Int32 numerator, Int32 denominator)
         : base(ac, state) {
         m_target = target;
         m_numerator = numerator;
         m_denominator = denominator;
         BeginInvokeOnWorkerThread();
      }
      protected override Int32 OnCompleteOperation(IAsyncResult ar) {
         return m_target.Divide(m_numerator, m_denominator);
      }
   }

   public IAsyncResult BeginDivideSpecific(Int32 numerator, Int32 denominator, AsyncCallback ac, Object state) {
      return new AsyncResultDivide(ac, state, this, numerator, denominator);
#if false
		Boolean doSync = (numerator % 2 == 0); // For testing, even #s are done synchronously
		if (doSync) {
			// Choice: Call callback using this thread or threadpool thread? Potential stack size issue
			s_DivideAsyncHelper(ar);
		} else {
			ThreadPool.QueueUserWorkItem(s_DivideAsyncHelper, ar);
		}
		return ar;
#endif
   }

   public Int32 EndDivideSpecific(IAsyncResult ar) {
      return ((AsyncResultDivide)ar).EndInvoke();
   }
   #endregion
}


///////////////////////////////////////////////////////////////////////////////

namespace A {
   using System.IO;
   class FileTest : AsyncResult<String> {
      FileStream m_fs;
      Byte[] m_bytes = new Byte[10000];
      public static FileTest s_ft;

      public FileTest(AsyncCallback ac, Object state) : base(ac, state) {
         s_ft = this;
         m_fs = new System.IO.FileStream(@"c:\boot.ini", FileMode.Open);
         m_fs.BeginRead(m_bytes, 0, m_bytes.Length, GetAsyncCallbackHelper(), this);
      }
      protected sealed override String OnCompleteOperation(IAsyncResult ar) {
         int x = m_fs.EndRead(ar);
         Array.Resize(ref m_bytes, x);
         return System.Text.Encoding.ASCII.GetString(m_bytes);
      }
   }
   class FileTestNoReturn : AsyncResultNoReturn {
      FileStream m_fs;
      Byte[] m_bytes = new Byte[10000];
      public static FileTestNoReturn s_ft;

      public FileTestNoReturn(AsyncCallback ac, Object state)
         : base(ac, state) {
         s_ft = this;
         m_fs = new System.IO.FileStream(@"c:\boot.ini", FileMode.Open);
         m_fs.BeginRead(m_bytes, 0, m_bytes.Length, GetAsyncCallbackHelper(), this);
      }
      protected sealed override void OnCompleteOperation(IAsyncResult ar) {
         int x = m_fs.EndRead(ar);
         Array.Resize(ref m_bytes, x);
         String s = System.Text.Encoding.ASCII.GetString(m_bytes);
         Console.WriteLine(s);
      }
   }
}

internal sealed class APMTest {
   static MathTest s_mt = new MathTest();

   public static void Main() {
      const Int32 count = 100 * 1000;
      Int32 result;
      //A.FileTest ft = new A.FileTest(delegate(IAsyncResult ar) { Console.WriteLine(A.FileTest.s_ft.EndInvoke()); }, null);
      A.FileTestNoReturn ftnr = new A.FileTestNoReturn(delegate(IAsyncResult ar) { A.FileTestNoReturn.s_ft.EndInvoke(); Console.WriteLine("Done reading");  }, null);
      Thread.Sleep(30000);

      // Functional tests: No callback 
      Console.WriteLine(s_mt.Divide(12, 3));
      try { Console.WriteLine(s_mt.Divide(12, 0)); }
      catch (DivideByZeroException) { Console.WriteLine("DivideByZero caught"); }

      Console.WriteLine(s_mt.EndDivideDelegate(s_mt.BeginDivideDelegate(12, 3, null, null)));
      try { Console.WriteLine(s_mt.EndDivideDelegate(s_mt.BeginDivideDelegate(12, 0, null, null))); }
      catch (DivideByZeroException) { Console.WriteLine("DivideByZero caught"); }

      Console.WriteLine(s_mt.EndDivideReflection(s_mt.BeginDivideReflection(12, 3, null, null)));
      try { Console.WriteLine(s_mt.EndDivideReflection(s_mt.BeginDivideReflection(12, 0, null, null))); }
      catch (DivideByZeroException) { Console.WriteLine("DivideByZero caught"); }

      Console.WriteLine(s_mt.EndDivideSpecific(s_mt.BeginDivideSpecific(12, 3, null, null)));
      try { Console.WriteLine(s_mt.EndDivideSpecific(s_mt.BeginDivideSpecific(12, 0, null, null))); }
      catch (DivideByZeroException) { Console.WriteLine("DivideByZero caught"); }

      // Functional tests: Callback 
      s_mt.BeginDivideDelegate(12, 3,
         delegate(IAsyncResult ar)
         {
            Console.WriteLine(((MathTest)ar.AsyncState).EndDivideDelegate(ar));
         }, s_mt);

      s_mt.BeginDivideReflection(12, 3,
         delegate(IAsyncResult ar)
         {
            Console.WriteLine(((MathTest)ar.AsyncState).EndDivideReflection(ar));
         }, s_mt);

      s_mt.BeginDivideSpecific(12, 3,
         delegate(IAsyncResult ar)
         {
            Console.WriteLine(((MathTest)ar.AsyncState).EndDivideSpecific(ar));
         }, s_mt);

      Thread.Sleep(2000);
      Console.WriteLine("Functional tests complete. Starting performance tests.");

      // Performance tests: No callback
      using (new OperationTimer("Divide, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            result = s_mt.Divide(12, 3);
         }
      }

      using (new OperationTimer("BeginDivideDelegate, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            result = s_mt.EndDivideDelegate(s_mt.BeginDivideDelegate(12, 3, null, null));
         }
      }

      using (new OperationTimer("BeginDivideReflection, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            result = s_mt.EndDivideReflection(s_mt.BeginDivideReflection(12, 3, null, null));
         }
      }

      using (new OperationTimer("BeginDivideSpecific, no callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            result = s_mt.EndDivideSpecific(s_mt.BeginDivideSpecific(12, 3, null, null));
         }
      }

      // Performance tests: Callback
      AutoResetEvent done = new AutoResetEvent(false);
      Int32 countdown;

      countdown = count;
      AsyncCallback acDelegate = delegate(IAsyncResult ar)
      {
         result = ((MathTest)ar.AsyncState).EndDivideDelegate(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginDivideDelegate, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_mt.BeginDivideDelegate(12, 3, acDelegate, s_mt);
         }
         done.WaitOne();
      }


      countdown = count;
      AsyncCallback acReflection = delegate(IAsyncResult ar)
      {
         result = ((MathTest)ar.AsyncState).EndDivideReflection(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginDivideReflection, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_mt.BeginDivideReflection(12, 3, acReflection, s_mt);
         }
         done.WaitOne();
      }



      countdown = count;
      AsyncCallback acSpecific = delegate(IAsyncResult ar)
      {
         result = ((MathTest)ar.AsyncState).EndDivideSpecific(ar);
         if (Interlocked.Decrement(ref countdown) == 0) done.Set();
      };
      using (new OperationTimer("BeginDivideSpecific, callback, no throw")) {
         for (Int32 x = 0; x < count; x++) {
            s_mt.BeginDivideSpecific(12, 3, acSpecific, s_mt);
         }
         done.WaitOne();
      }

      Console.WriteLine("All tests complete.");
      Console.ReadLine();
   }
}


//////////////////////////////// End of File //////////////////////////////////
#endif