/******************************************************************************
Module:  AsyncEnumPatterns.cs
Notices: Copyright (c) 2006-2009 by Jeffrey Richter and Wintellect
******************************************************************************/

// System.Net Tracing: http://blogs.msdn.com/dgorti/archive/2005/09/18/471003.aspx

#define AsyncEnumeratorDebug
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using Wintellect.Threading.AsyncProgModel;

///////////////////////////////////////////////////////////////////////////////

public static class AsyncEnumPatterns {
   public static void Main() {
      DoDemo(HtmlToFileDemo);
      DoDemo(WaitForAllAndEachDemo);
      DoDemo(DiscardGroupDemo);
      DoDemo(CancelationDemo);
      DoDemo(TimeoutDemo);
      DoDemo(SharedResourceDemo);
      DoDemo(SubroutineWithResultDemo);
      Console.ReadLine();
   }

   #region Html to File Demo
   private static void HtmlToFileDemo() {
      AsyncEnumerator.EnableDebugSupport();
      AsyncEnumerator ae = new AsyncEnumerator("Jeff");
      String file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ms.com.txt");
      Execute(ae, HtmlToFile(ae, "http://www.microsoft.com/", file));
      Process.Start(file).WaitForExit();
      File.Delete(file);
   }

   private static IEnumerator<Int32> HtmlToFile(AsyncEnumerator ae, String url, String file) {
      // Issue asynchronous web request operation
      WebRequest webRequest = WebRequest.Create(url);
      ae.SetOperationTag(url);
      webRequest.BeginGetResponse(ae.End(), null);
      Console.WriteLine(ae);
      yield return 1;

      Debugger.Break();
      WebResponse webResponse;
      try {
         webResponse = webRequest.EndGetResponse(ae.DequeueAsyncResult());
      }
      catch (WebException e) {
         Console.WriteLine("Failed to contact server: {0}", e.Message);
         yield break;
      }

      using (webResponse) {
         Stream webResponseStream = webResponse.GetResponseStream();

         // Read the stream data and write it to a file in 1KB chunks
         Byte[] data = new Byte[1024];
         using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write,
            FileShare.None, data.Length, FileOptions.Asynchronous)) {

            // See http://support.microsoft.com/default.aspx?scid=kb%3Ben-us%3B156932 
            fs.SetLength(webResponse.ContentLength);

            while (true) {
               // Issue asynchronous web response stream read operation
               webResponseStream.BeginRead(data, 0, data.Length, ae.End(), null);
               yield return 1;

               // Get result of web response stream read operation
               Int32 bytesRead = webResponseStream.EndRead(ae.DequeueAsyncResult());
               if (bytesRead == 0) break; // End of stream, close file, and exit

               // Issue asynchronous file write operation
               fs.BeginWrite(data, 0, bytesRead, ae.End(), null);
               yield return 1;

               // Get result of file write operation
               fs.EndWrite(ae.DequeueAsyncResult());
            }
         }
      }
   }
   #endregion


   private static String[] s_urls = new String[] { 
      "http://Wintellect.com/", 
      "http://1.1.1.1/",   // Demonstrates error recovery
      "http://www.Devscovery.com/" 
   };


   #region Wait for All and Each Demos
   private static void WaitForAllAndEachDemo() {
      // Demonstrate process
      AsyncEnumerator ae = new AsyncEnumerator();
      Execute(ae, ProcessAllAndEachOps(ae, s_urls));
   }

   private static IEnumerator<Int32> ProcessAllAndEachOps(AsyncEnumerator ae, String[] urls) {
      Int32 numOps = urls.Length;

      // Issue all the asynchronous operation(s) so they run concurrently
      for (Int32 n = 0; n < numOps; n++) {
         WebRequest wr = WebRequest.Create(urls[n]);
         wr.BeginGetResponse(ae.End(), wr);
      }

      // Have AsyncEnumerator wait until ALL operations complete
      yield return numOps;

      Console.WriteLine("All the operations completed:");
      for (Int32 n = 0; n < numOps; n++) {
         Console.WriteLine(CompleteWebRequest(ae.DequeueAsyncResult()));
      }

      Console.WriteLine(); // *** Blank line between demos ***

      // Issue all the asynchronous operation(s) so they run concurrently
      for (Int32 n = 0; n < numOps; n++) {
         WebRequest wr = WebRequest.Create(urls[n]);
         wr.BeginGetResponse(ae.End(), wr);
      }

      for (Int32 n = 0; n < numOps; n++) {
         // Have AsyncEnumerator wait until EACH operation completes
         yield return 1;

         Console.WriteLine("An operation completed:");
         Console.WriteLine(CompleteWebRequest(ae.DequeueAsyncResult()));
      }
   }

   private static String CompleteWebRequest(IAsyncResult ar) {
      String result = String.Empty;
      WebRequest wr = (WebRequest) ar.AsyncState;
      try {
         result = "   Uri=" + wr.RequestUri + "    ";
         using (WebResponse response = wr.EndGetResponse(ar)) {
            result += "ContentLength=" + response.ContentLength;
         }
      }
      catch (WebException e) {
         result += "WebException=" + e.Message;
      }
      return result;
   }
   #endregion

   #region Discard Group Demo
   private static void DiscardGroupDemo() {
      AsyncEnumerator ae = new AsyncEnumerator();
      Execute(ae, DiscardGroupDemo(ae, s_urls));
   }

   private static IEnumerator<Int32> DiscardGroupDemo(AsyncEnumerator ae, String[] urls) {
      ae.ThrowOnMissingDiscardGroup(true);

      // Issue asynchronous web request operation(s)
      for (Int32 n = 0; n < urls.Length; n++) {
         WebRequest wr = WebRequest.Create(urls[n]);
         wr.BeginGetResponse(ae.End(0, CompleteWebRequest), wr);
      }

      yield return 1;

      Console.Write("   One result: " + CompleteWebRequest(ae.DequeueAsyncResult()));
      ae.DiscardGroup(0); // OPTIONAL: Ignore the rest 
   }
   #endregion

   #region Cancelation Demo
   private static void CancelationDemo() {
      AsyncEnumerator<String> ae = new AsyncEnumerator<String>();
      IAsyncResult result = ae.BeginExecute(CancelationDemo(ae), null);
      Console.Write("   Hit any key to cancel (10 seconds)?");
      for (Int32 halfSeconds = 20; (halfSeconds > 0) && !result.AsyncWaitHandle.WaitOne(500, false); halfSeconds -= 1) {
         if (Console.KeyAvailable) ae.Cancel("Canceled on " + DateTime.Now.ToLongTimeString());
      }
      Console.WriteLine();
      Console.WriteLine("   Result=" + ae.EndExecute(result));
   }

   private static IEnumerator<Int32> CancelationDemo(AsyncEnumerator<String> ae) {
      ae.ThrowOnMissingDiscardGroup(true);

      ae.SetCancelTimeout(10000, "Timeout");
      yield return 1;   // Will wait forever until cancel or timeout since no BeginXxx method was caled

      Object cancelValue;
      if (ae.IsCanceled(out cancelValue)) {
         ae.Result = cancelValue.ToString();
      } else {
         Debug.Assert(false); // Should never get here
      }
   }
   #endregion

   #region Wait for Timeout Demo
   private static void TimeoutDemo() {
      AsyncEnumerator ae = new AsyncEnumerator();
      // This also shows how to invoke asynchronously (good for Windows Forms/WPF)
      ae.BeginExecute(TimeoutDemo(ae, s_urls),
         delegate(IAsyncResult ar) { ae.EndExecute(ar); Console.WriteLine("   AsyncEnumerator is done (Hit Enter)"); });
      Console.ReadLine();
   }

   private static IEnumerator<Int32> TimeoutDemo(AsyncEnumerator ae, String[] urls) {
      ae.ThrowOnMissingDiscardGroup(true);

      Int32 numOps = s_urls.Length;
      // Issue asynchronous web request operation(s)
      for (Int32 n = 0; n < numOps; n++) {
         WebRequest wr = WebRequest.Create(urls[n]);
         wr.BeginGetResponse(ae.End(0, CompleteWebRequest), wr);
      }

      ae.SetCancelTimeout(1000, null); // Process what we can in 1 second

      Int32 opsComplete = 0;
      for (; opsComplete < numOps; opsComplete++) {
         yield return 1;
         if (ae.IsCanceled()) break; // Timeout expired
         Console.WriteLine(CompleteWebRequest(ae.DequeueAsyncResult()));
      }
      Console.WriteLine("   Timer expired, num ops completed=" + opsComplete);
   }
   #endregion

   #region Shared Resource Demo
   private static SyncGate s_syncGate = new SyncGate();

   private static void SharedResourceDemo() {
      ThreadPool.SetMinThreads(10, 10);
      for (Int32 x = 0; x < 6; x++) {
         AsyncEnumerator ae = new AsyncEnumerator();
         ae.BeginExecute(SharedResource(ae,
            x, (x < 3) ? SyncGateMode.Exclusive : SyncGateMode.Shared), ae.EndExecute);
      }
      Console.ReadLine();
   }

   private static IEnumerator<Int32> SharedResource(AsyncEnumerator ae, Int32 n, SyncGateMode mode) {
      // Use the SyncGate to protect a resource accessed by multiple AEs
      Console.WriteLine("Request {0}: Requesting {1} mode", n, mode);
      s_syncGate.BeginRegion(mode, ae.End(), null);
      yield return 1;
      Console.WriteLine("Request {0}: In {1} mode for 5 seconds", n, mode);
      Thread.Sleep(5000);
      Console.WriteLine("Request {0}: Releasing {1} mode", n, mode);
      s_syncGate.EndRegion(ae.DequeueAsyncResult());
   }
   #endregion

   #region Subroutine (Composition) with Result Demo
   private static void SubroutineWithResultDemo() {
      AsyncEnumerator<String> ae = new AsyncEnumerator<String>();
      Console.WriteLine(Execute(ae, GetMultipleUris(ae, s_urls)));
   }

   private static IEnumerator<Int32> GetMultipleUris(AsyncEnumerator<String> aeOuter, String[] urls) {
      // Create 1 AsyncEnumerator per subroutine
      var results = (from url in urls select new { url, aeSub = new AsyncEnumerator<String>() }).ToArray();

      // Start executing all the subroutines. Notice multiple iterators ALL running concurrently - great scalability!
      for (Int32 index = 0; index < results.Length; index++) {
         // Get the AsyncEnumerator for this subroutine
         AsyncEnumerator<String> aeSub = results[index].aeSub;

         // Start executing this subroutine; when done ithe outer AE should be notified
         aeSub.BeginExecute(GetSingleUriLength(aeSub, results[index].url), aeOuter.End(), index);
      }
      yield return urls.Length;  // Wait for all the subroutines to complete

      // Build string composed of each subroutine's individual results
      StringBuilder sb = new StringBuilder();
      for (Int32 n = 0; n < urls.Length; n++) {
         IAsyncResult asyncResult = aeOuter.DequeueAsyncResult();
         Int32 index = (Int32) asyncResult.AsyncState;   // See BeginExecute's last argument
         String subroutineResult = results[index].aeSub.EndExecute(asyncResult);
         sb.AppendFormat("{0}: {1}", results[index].url, subroutineResult);
         sb.AppendLine();
      }
      aeOuter.Result = sb.ToString(); // Return the full result string
   }

   // This iterator knows how to asynchronously query 1 website and return the # of bytes as a String
   private static IEnumerator<Int32> GetSingleUriLength(AsyncEnumerator<String> ae, String uri) {
      WebRequest wr = WebRequest.Create(uri);
      wr.BeginGetResponse(ae.End(), null);
      yield return 1;   // Wait for this one web request to complete

      try {
         using (WebResponse response = wr.EndGetResponse(ae.DequeueAsyncResult())) {
            ae.Result = "ContentLength=" + response.ContentLength;
         }
      }
      catch (WebException e) { ae.Result = "Error=" + e.Message; }
   }
   #endregion

   [DebuggerStepThrough]
   private static void Execute(AsyncEnumerator ae, IEnumerator<Int32> enumerator) {
      ae.EndExecute(ae.BeginExecute(enumerator, null));
   }

   [DebuggerStepThrough]
   private static TResult Execute<TResult>(AsyncEnumerator<TResult> ae, IEnumerator<Int32> enumerator) {
      return ae.EndExecute(ae.BeginExecute(enumerator, null));
   }

   private delegate void DemoMethod();

   [DebuggerStepThrough]
   private static void DoDemo(DemoMethod dm) {
      Console.WriteLine("Demo start: " + dm.Method.Name);
      dm();
      Console.WriteLine();
      Console.WriteLine();
   }
}