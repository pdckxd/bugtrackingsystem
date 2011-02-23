/******************************************************************************
File: MultipleOps.cs written by Jeffrey Richter
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using Wintellect.Threading.AsyncProgModel;


///////////////////////////////////////////////////////////////////////////////


public static class AsyncEnumeratorPatterns {
   public static void Main() {
      String[] urls = new String[] { 
         "http://Wintellect.com/", 
         "http://1.1.1.1/",   // Demonstrates error recovery
         "http://www.Devscovery.com/" 
      };

      // Demonstrate process
      AsyncEnumerator ae = new AsyncEnumerator();
      ae.EndExecute(ae.BeginExecute(ProcessAllAndEachOps(ae, urls), null));
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
         ProcessCompletedWebRequest(ae.DequeueAsyncResult());
      }

      Console.WriteLine(); // *** Blank line between demos ***
      System.Diagnostics.Debugger.Break();

      // Issue all the asynchronous operation(s) so they run concurrently
      for (Int32 n = 0; n < numOps; n++) {
         WebRequest wr = WebRequest.Create(urls[n]);
         wr.BeginGetResponse(ae.End(), wr);
      }

      for (Int32 n = 0; n < numOps; n++) {
         // Have AsyncEnumerator wait until EACH operation completes
         yield return 1;

         Console.WriteLine("An operation completed:");
         ProcessCompletedWebRequest(ae.DequeueAsyncResult());
      }
   }

   private static void ProcessCompletedWebRequest(IAsyncResult ar) {
      WebRequest wr = (WebRequest)ar.AsyncState;
      try {
         Console.Write("   Uri=" + wr.RequestUri + "    ");
         using (WebResponse response = wr.EndGetResponse(ar)) {
            Console.WriteLine("ContentLength=" + response.ContentLength);
         }
      }
      catch (WebException e) {
         Console.WriteLine("WebException=" + e.Message);
      }
   }
}
