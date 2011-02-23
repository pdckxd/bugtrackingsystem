using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using Microsoft.Phone.Controls;
using Wintellect.Threading.AsyncProgModel;

namespace PowerThreadingTestApp {
   public partial class MainPage : PhoneApplicationPage {
      public MainPage() {
         InitializeComponent();

         SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
      }

      private void button1_Click(object sender, RoutedEventArgs e) {
         var ae = new AsyncEnumerator("String download");
         ae.BeginExecute(DownloadString(ae, "http://Wintellect.com/"), ae.EndExecute);
      }

      private IEnumerator<Int32> DownloadString(AsyncEnumerator ae, String uri) {
         var request = WebRequest.Create(uri);
         request.BeginGetResponse(ae.End(), null);
         yield return 1;
         var response = (WebResponse) request.EndGetResponse(ae.DequeueAsyncResult());
         Byte[] buffer = new Byte[response.ContentLength];
         using (var stream = response.GetResponseStream()) {
            stream.BeginRead(buffer, 0, buffer.Length, ae.End(), null);
            yield return 1;
            stream.EndRead(ae.DequeueAsyncResult());
         }
         m_textBox.Text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
      }
   }
}