<%@ WebService Language="C#" Class="AsyncEnumeratorService" %>

/******************************************************************************
File: AsyncEnumeratorService.asmx written by Jeffrey Richter
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Web.Services;
using Wintellect.Threading.AsyncProgModel;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AsyncEnumeratorService : System.Web.Services.WebService {
   // This static s_recentRequests field contains the 10 most recent requests 
   // made by clients into the web service. This field can be accessed 
   // simultaneously by multiple clients and so thread synchronization via the
   // static s_recentRequestsSyncGate field is required to avoid corruption.
   private static List<String[]> s_recentRequests = new List<String[]>(10);
   private static SyncGate s_recentRequestsSyncGate = new SyncGate();


   #region GetWebSiteDataLength Web Service Support
   // Each client request has its own AsyncEnumerator object allowing
   // multiple concurrent client requests to be in-process simultaneously
   private AsyncEnumerator<String[]> m_webSiteDataLength;

   // ASP.NET calls this method to initiate the asynchronous web service
   [WebMethod]
   public IAsyncResult BeginGetWebSiteDataLength(String uris, AsyncCallback callback, Object state) {
      // Construct an AsyncEnumerator that will eventually return a String[]
      m_webSiteDataLength = new AsyncEnumerator<String[]>();
      // NOTE: The AsyncEnumerator automatically saves the ASP.NET 
      // SynchronizationContext with it ensuring that the iterator 
      // always executes using the correct IPrincipal, 
      // CurrentCulture, and CurrentUICulture.

      // Initiate the iterator asynchronously. 
      return m_webSiteDataLength.BeginExecute(
         GetWebSiteDataLength(m_webSiteDataLength, uris.Split(',')), callback, state);
      // NOTE: Since the AsyncEnumerator's BeginExecute method returns an 
      // IAsyncResult, we can just return this back to ASP.NET
   }

   // This is the C# iterator that does the actual web service work
   private IEnumerator<Int32> GetWebSiteDataLength(AsyncEnumerator<String[]> ae, String[] uris) {

      // Issue several web request simultaneously
      foreach (String uri in uris) {
         WebRequest webRequest = WebRequest.Create(uri);
         webRequest.BeginGetResponse(ae.End(), webRequest);
      }

      yield return uris.Length;  // Wait for ALL the web requests to complete

      // Construct the String[] that will be the ultimate result
      ae.Result = new String[uris.Length];

      // Process ALL of the completed web requests
      for (Int32 n = 0; n < uris.Length; n++) {
         // Grab the result of a completed web request
         IAsyncResult result = ae.DequeueAsyncResult();

         // Get the WebRequest object used to initate the request 
         // (see BeginGetResponse's last argument above)
         WebRequest webRequest = (WebRequest)result.AsyncState;

         // Build the String showing the result of this completed web request
         ae.Result[n] = "URI=" + webRequest.RequestUri + ", ";
         try {
            using (WebResponse webResponse = webRequest.EndGetResponse(result)) {
               ae.Result[n] += "ContentLength=" + webResponse.ContentLength;
            }
         }
         catch (WebException e) {
            ae.Result[n] += "Error=" + e.Message;
         }
      }

      // In an exclusive way, modify the collection of most-recent queries
      s_recentRequestsSyncGate.BeginRegion(SyncGateMode.Exclusive, ae.End());
      yield return 1;   // Continue when collection can be updated (modified)

      // If collection is full, remove the oldest item
      if (s_recentRequests.Count == s_recentRequests.Capacity)
         s_recentRequests.RemoveAt(0);

      s_recentRequests.Add(ae.Result);
      s_recentRequestsSyncGate.EndRegion(ae.DequeueAsyncResult());  // Updating is done
   }

   // ASP.NET calls this method when the iterator completes. This method returns 
   // the AsyncEnumerator's String[] result from the web service back to the client
   [WebMethod]
   public String[] EndGetWebSiteDataLength(IAsyncResult result) {
      return m_webSiteDataLength.EndExecute(result);
   }
   #endregion


   #region GetRecentQueries Web Service Support
   private AsyncEnumerator<String[][]> m_aeRecentRequests;

   [WebMethod]
   public IAsyncResult BeginGetRecentRequests(AsyncCallback callback, Object state) {
      m_aeRecentRequests = new AsyncEnumerator<String[][]>();
      return m_aeRecentRequests.BeginExecute(GetRecentRequests(m_aeRecentRequests), callback, state);
   }

   private IEnumerator<Int32> GetRecentRequests(AsyncEnumerator<String[][]> ae) {
      // In a shared way, read the collection of most-recent requests
      s_recentRequestsSyncGate.BeginRegion(SyncGateMode.Shared, ae.End());
      yield return 1;   // Continue when collection can be examined (read)

      // Return a copy of the collection as an array
      ae.Result = s_recentRequests.ToArray();
      s_recentRequestsSyncGate.EndRegion(ae.DequeueAsyncResult());  // Reading is done
   }

   [WebMethod]
   public String[][] EndGetRecentRequests(IAsyncResult result) {
      return m_aeRecentRequests.EndExecute(result);
   }
   #endregion
}