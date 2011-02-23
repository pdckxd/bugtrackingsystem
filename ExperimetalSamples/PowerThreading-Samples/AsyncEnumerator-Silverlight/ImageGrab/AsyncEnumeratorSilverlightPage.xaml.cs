/******************************************************************************
Module:  AsyncEnumeratorSilverlightPage.xaml.cs
Notices: Copyright (c) 2008 by Jeffrey Richter and Wintellect
******************************************************************************/

/* Use can use the AsyncEnumerator via Silverlight to asynchronously work
 * with any of the following base classes & classes derived from them:
 * System.Delegate: BeginInvoke
 * System.IO.Stream (FileStream, IsolatedStorageFileStream, etc): BeginRead/BeginWrite
 * System.Net.WebRequest (HttpWebRequest): BeginGetRequestStream/BeginGetResponse
 * System.ServiceModel.ICommunicationObject: BeginOpen/BeginClose
 * System.ServiceModel.Channels.IInputChannel: BeginReceive/BeginWaitForMessage/etc
 * System.ServiceModel.Channels.IOutputChannel: BeginSend
 * System.ServiceModel.Channels.IDuplexSession: BeginCloseOutputSession
 * System.ServiceModel.Channels.IRequestChannel: BeginRequest
 * and, of course, other web service calls
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wintellect.Threading.AsyncProgModel;


namespace ImageGrab {
   public static class UriHelper {
      private static String s_baseAddress;
      public static Uri ConvertRelativeUriStringToAbsolute(String relativeUri) {
         if (s_baseAddress == null) {
            s_baseAddress = new WebClient().BaseAddress;
            s_baseAddress = s_baseAddress.Substring(0, s_baseAddress.LastIndexOf('/') + 1);
         }
         String absoluteUri = s_baseAddress + relativeUri;
         return new Uri(absoluteUri);
      }
   }

   public partial class Page : UserControl {
      private Image[] m_images;
      private TextBox[] m_textboxes;
      private AsyncEnumerator m_ae;

      public Page() {
         InitializeComponent();
         m_textboxes = new TextBox[] { Result1, Result2 };
         m_images = new Image[] { Image1, Image2 };
      }

      private void BlankImages() {
         for (Int32 n = 0; n < m_textboxes.Length; n++) {
            m_textboxes[n].Text = String.Empty;
            m_images[n].Source = null;
         }
      }

      private void GetImages_Click(object sender, RoutedEventArgs e) {
         BlankImages();
         m_ae = new AsyncEnumerator();
         m_ae.BeginExecute(DownloadImages(m_ae), m_ae.EndExecute);
      }

      private IEnumerator<Int32> DownloadImages(AsyncEnumerator ae) {
         ae.ThrowOnMissingDiscardGroup(true);
         GetImages.IsEnabled = false;
         Cancel.IsEnabled = true;
         WebRequest[] requests = new WebRequest[] {
            WebRequest.Create(UriHelper.ConvertRelativeUriStringToAbsolute("Images/Wintellect.jpg")),
            WebRequest.Create(UriHelper.ConvertRelativeUriStringToAbsolute("Images/JeffreyRichter.jpg")),
         };
         for (Int32 requestNum = 0; requestNum < requests.Length; requestNum++) {
            requests[requestNum].BeginGetResponse(
               ae.EndVoid(0, asyncResult => {
                  requests[requestNum].EndGetResponse(asyncResult).Close();
               }), requestNum);
         }

         for (Int32 resultNum = 0; resultNum < requests.Length; resultNum++) {
            yield return 1;
            if (ae.IsCanceled()) break;
            IAsyncResult asyncResult = ae.DequeueAsyncResult();
            Int32 index = (Int32)asyncResult.AsyncState;
            try {
               using (WebResponse response = requests[index].EndGetResponse(asyncResult)) {
                  using (Stream stream = response.GetResponseStream()) {
                     BitmapImage bitmapImage = new BitmapImage();
                     bitmapImage.SetSource(stream);
                     m_images[index].Source = bitmapImage;
                  }
               }
            }
            catch (WebException e) {
               m_textboxes[index].Text = "Failed: " + e.Message;
            }
         }
         GetImages.IsEnabled = true;
         Cancel.IsEnabled = false;
      }

      private void Cancel_Click(object sender, RoutedEventArgs e) {
         m_ae.Cancel(null);
      }
   }
}
