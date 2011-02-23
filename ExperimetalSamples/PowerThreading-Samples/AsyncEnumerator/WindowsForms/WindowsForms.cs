/******************************************************************************
File: WindowsFormsViaAsyncEnumerator.cs written by Jeffrey Richter
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Wintellect.Threading.AsyncProgModel;


namespace WinFormUsingAsyncEnumerator {
   public partial class WindowsForms : Form {
      public static void Main() {
         Application.Run(new WindowsForms());
      }

      public WindowsForms() {
         InitializeComponent();
      }

      private AsyncEnumerator m_ae = null;
      private void m_btnStart_Click(object sender, EventArgs e) {
         String[] uris = new String[] {
            "http://Wintellect.com/", 
            "http://1.1.1.1/",   // Demonstrates error recovery
            "http://www.Devscovery.com/" 
         };

         // Construct an AsyncEnumerator
         m_ae = new AsyncEnumerator();
         // NOTE: The AsyncEnumerator automatically saves the 
         // Windows Forms SynchronizationContext with it ensuring
         // that the iterator always runs on the GUI thread; 
         // this allows the iterator to access the UI Controls

         // Start iterator asynchonously so that GUI thread doesn't block
         m_ae.BeginExecute(GetWebData(m_ae, uris), m_ae.EndExecute);
      }

      // Because SyncContext is set, all the iterator code runs on the GUI thread
      private IEnumerator<Int32> GetWebData(AsyncEnumerator ae, String[] uris) {
         ToggleStartAndCancelButtonState(false);
         m_lbResults.Items.Clear();

         // Auto-cancel after 5 seconds if the user desires
         if (m_chkAutoCancel.Checked)
            ae.SetCancelTimeout(5000, ae);

         // Issue several web requests (all in discard group 0) simultaneously
         foreach (String uri in uris) {
            WebRequest webRequest = WebRequest.Create(uri);

            // If the AsyncEnumerator is canceled, DiscardWebRequest cleans-up
            // any outstanding operations as they complete in the future
            webRequest.BeginGetResponse(ae.EndVoid(0, DiscardWebRequest), webRequest);
         }

         yield return uris.Length;  // Process the completed web requests after all complete

         String resultStatus; // Ultimate result of processing shown to user

         // Check if iterator was canceled
         Object cancelValue;
         if (ae.IsCanceled(out cancelValue)) {
            // Tell the AE to auto-cleanup any operations issued as part of discard group 0
            ae.DiscardGroup(0);
            // Note: In this example calling DiscardGroup above is not mandatory
            // because the whole iterator is stopping execution; causing all
            // discard groups to be discarded automatically.

            resultStatus = (cancelValue == ae) ? "Timeout" : "User canceled";
            goto Complete;
         }

         // Iterator wasn't canceled, process all the completed operations
         for (Int32 n = 0; n < uris.Length; n++) {
            // Grab the result of a completed web request
            IAsyncResult result = ae.DequeueAsyncResult();

            // Get the WebRequest object used to initate the request 
            // (see BeginGetResponse's last argument above)
            WebRequest webRequest = (WebRequest)result.AsyncState;

            // Build the String showing the result of this completed web request
            String s = "URI=" + webRequest.RequestUri + ", ";
            try {
               using (WebResponse webResponse = webRequest.EndGetResponse(result)) {
                  s += "ContentLength=" + webResponse.ContentLength;
               }
            }
            catch (WebException e) {
               s += "Error=" + e.Message;
            }
            m_lbResults.Items.Add(s);  // Add result of operation to listbox
         }
         resultStatus = "All operations completed.";

      Complete:
         // All operations have completed or cancelation occurred, tell user
         MessageBox.Show(this, resultStatus);

         // Reset everything so that the user can start over if they desire
         m_ae = null;   // Reset since we're done
         ToggleStartAndCancelButtonState(true);
      }

      private void m_btnCancel_Click(object sender, EventArgs e) {
         m_ae.Cancel(null);
         m_ae = null;
      }

      // Swap the Start/Cancel button states
      private void ToggleStartAndCancelButtonState(Boolean enableStart) {
         m_btnStart.Enabled = enableStart;
         m_btnCancel.Enabled = !enableStart;
         m_chkAutoCancel.Enabled = enableStart;
      }

      private void DiscardWebRequest(IAsyncResult result) {
         // Get the WebRequest object used to initate the request 
         // (see BeginGetResponse's last argument)
         WebRequest webRequest = (WebRequest)result.AsyncState;

         // Cleanup the async operation and Close the WebResponse (if no exception)
         webRequest.EndGetResponse(result).Close();
      }
   }
}
