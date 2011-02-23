﻿// NOTE: Make sure the ASPX file's page directive includes: Async="true" 

using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI.WebControls;
using Wintellect.Threading.AsyncProgModel;


public partial class _Default : System.Web.UI.Page {
   private static readonly Uri c_UrlImage1 = new Uri(@"http://images.wsdot.wa.gov/nwflow/cctv519.jpg");
   private static readonly Uri c_UrlImage2 = new Uri(@"http://images.wsdot.wa.gov/nwflow/cctv509.jpg");
   private static readonly Uri c_UrlFailure = new Uri(@"http://1.1.1.1/NoImage");

   private AsyncEnumerator m_ae;

   protected void m_btnAPMGetImages_Click(object sender, EventArgs e) {
      this.AddOnPreRenderCompleteAsync(BeginHandler, EndHandler);
   }

   // The BeginHandler method launches an async op returns immediately
   // The ASP.NET thread returns back to the thread pool
   // The thread pool waits on the IAsyncResult and then wakes and calls the EndHandler method
   private IAsyncResult BeginHandler(Object sender, EventArgs e, AsyncCallback cb, Object extraData) {
      m_lblPic1.Text = m_lblPic2.Text = null;
      m_ae = new AsyncEnumerator();
      return m_ae.BeginExecute(GetImages(m_ae), cb, extraData);
   }

   // An ASP.NET thread pool thread calls this when the IAsyncResult returned from BeginHandler indicates 
   // that the async op has completed. When EndHandler returns, ASP.NET renders the page
   private void EndHandler(IAsyncResult result) {
      m_ae.EndExecute(result);
   }

   private IEnumerator<Int32> GetImages(AsyncEnumerator ae) {
      ae.ThrowOnMissingDiscardGroup(true);

      // Request all the images asynchronously
      WebRequest[] requests = new WebRequest[] {
         WebRequest.Create(c_UrlImage1),
         WebRequest.Create(m_chkSimFailure.Checked ? c_UrlFailure : c_UrlImage2)
      };

      for (Int32 n = 0; n < requests.Length; n++)
         requests[n].BeginGetResponse(
            ae.EndVoid(0, asyncResult => { requests[(Int32)asyncResult.AsyncState].EndGetResponse(asyncResult).Close(); }), n);

      // Set timeout if specified.
      Int32 timeout;
      if (Int32.TryParse(m_txtServerTime.Text, out timeout))
         ae.SetCancelTimeout(timeout, null);

      // Wait for all operations to complete (or timeout)
      yield return requests.Length;
      if (ae.IsCanceled()) {
         m_lblPic1.Text = "Server couldn't process the request in the specified time.";
         yield break;
      }

      for (Int32 n = 0; n < requests.Length; n++) {
         IAsyncResult result = ae.DequeueAsyncResult();
         Int32 reqNum = (Int32)result.AsyncState;
         Label lbl = (reqNum == 0) ? m_lblPic1 : m_lblPic2;
         WebRequest request = requests[reqNum];
         WebResponse response = null;
         try {
            response = request.EndGetResponse(result);
            lbl.Text = String.Format("Image at {0} is {1:N0} bytes",
               response.ResponseUri, response.ContentLength);
         }
         catch (WebException e) {
            lbl.Text = String.Format("Error obtaining image at {0}: {1}",
               request.RequestUri, e.Message);
         }
         finally { if (response != null) response.Close(); }
      }
   }
}