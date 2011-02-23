#define AsyncEnumerator
#define AsyncEnumeratorDebug
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Wintellect.Threading.AsyncProgModel;


namespace Flickr {
   public partial class Page : UserControl {
      private const int c_cx = 400;
      private const int c_cy = 300;
      private const int c_border = 10;
      private const int c_angle = 30;
      private const string c_template =
          "<Border Name=\"Photo_{0}\" Canvas.Left=\"{1}\" Canvas.Top=\"{2}\" Width=\"{3}\" Height=\"{4}\" Background=\"White\" Visibility=\"Collapsed\" xmlns=\"http://schemas.microsoft.com/client/2007\">" +
          "<Border.RenderTransform>" +
          "<RotateTransform Name=\"SpinTransform\" CenterX=\"{5}\" CenterY=\"{6}\" />" +
          "</Border.RenderTransform>" +

          "<Border.Resources>" +
          "<Storyboard Name=\"EntranceStoryboard\">" +
          "<DoubleAnimation Name=\"DA1\" Storyboard.TargetName=\"SpinTransform\" Storyboard.TargetProperty=\"Angle\" Duration=\"0:0:0.25\" />" +
          "<DoubleAnimation Name=\"DA2\" Storyboard.TargetName=\"Photo_{0}\" Storyboard.TargetProperty=\"(Canvas.Left)\" Duration=\"0:0:0.25\" /> " +
          "<DoubleAnimation Name=\"DA3\" Storyboard.TargetName=\"Photo_{0}\" Storyboard.TargetProperty=\"(Canvas.Top)\" Duration=\"0:0:0.25\" />" +
          "</Storyboard>" +
          "</Border.Resources>" +

          "<Image Width=\"{7}\" Height=\"{8}\" Stretch=\"UniformToFill\" /> " +
          "</Border>";

      private Boolean m_dragging = false;
      private Double m_lastx, m_lasty;
      private FrameworkElement m_owner;
      private Random m_rand = new Random(DateTime.Now.Millisecond);
      private Int32 m_zindex = 100;

      public Page() {
         InitializeComponent();

         // Center the search panel
         Double width = Application.Current.Host.Content.ActualWidth;
         Double height = Application.Current.Host.Content.ActualHeight;
         SearchPanel.SetValue(Canvas.LeftProperty, (width - 264.0) / 2.0);
         SearchPanel.SetValue(Canvas.TopProperty, (height - 100.0) / 2.0);

         // Register a handler for the control's Resized events
         Application.Current.Host.Content.Resized += OnResized;
      }

      private void FlickrButton_Click(object sender, RoutedEventArgs e) {
         // Initiate a Flickr request
         String input = Input.Text.Replace(' ', '+');

#if !AsyncEnumerator
         WebRequest request = HttpWebRequest.Create(
            new Uri(String.Format("http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&tags={1}&per_page=10",
               FlickrKey.Key, input)));
         request.BeginGetResponse(WebRequestComplete, request);
#else
#if DEBUG
         AsyncEnumerator.EnableDebugSupport();
#endif
         AsyncEnumerator ae = new AsyncEnumerator("Query Flickr Photos");
         ae.BeginExecute(GetPhotos(ae, input), ae.EndExecute);
#endif
      }

#if AsyncEnumerator
      private IEnumerator<Int32> GetPhotos(AsyncEnumerator ae, String input) {
         WebRequest request = HttpWebRequest.Create(
            new Uri(String.Format("http://api.flickr.com/services/rest/?method=flickr.photos.search&api_key={0}&tags={1}&per_page=10", 
               FlickrKey.Key, input)));
         ae.SetOperationTag("Querying " + request.RequestUri);
         request.BeginGetResponse(ae.End(), null);
         yield return 1;
         // Complete the Flickr request (this is guaranteed to run on the GUI thread)
         String data;
         using (HttpWebResponse response = (HttpWebResponse) request.EndGetResponse(ae.DequeueAsyncResult())) {
            using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
               data = reader.ReadToEnd();
            }
         }

         ae.SetOperationTag("Starting Storyboard");
         EventApmFactory<EventArgs> eventArgsFactory = new EventApmFactory<EventArgs>();
         EventHandler storyboardCompletedEventHandler = eventArgsFactory.PrepareOperation(ae.End()).EventHandler;
         SearchPanelStoryBoard.Completed += storyboardCompletedEventHandler;
         SearchPanelStoryBoard.Begin();
         yield return 1;

         SearchPanelStoryBoard.Completed -= storyboardCompletedEventHandler;
         eventArgsFactory.EndInvoke(ae.DequeueAsyncResult());
         PhotoBoard.Width = Application.Current.Host.Content.ActualWidth;
         PhotoBoard.Height = Application.Current.Host.Content.ActualHeight - 100.0;

         XDocument doc = XDocument.Parse(data);
         var photos = from results in doc.Descendants("photo")
                      select new {
                         id = results.Attribute("id").Value.ToString(),
                         farm = results.Attribute("farm").Value.ToString(),
                         server = results.Attribute("server").Value.ToString(),
                         secret = results.Attribute("secret").Value.ToString()
                      };

         PhotoBoard.Children.Clear();
         Int32 count = 0;

         EventApmFactory<OpenReadCompletedEventArgs> openReadCompletedEventArgsFactory = new EventApmFactory<OpenReadCompletedEventArgs>();
         foreach (var photo in photos) {
            // Create a photo
            Border item = (Border) XamlReader.Load(String.Format(CultureInfo.InvariantCulture,
                c_template, count,
                (PhotoBoard.Width - c_cx) / 2, (PhotoBoard.Height - c_cy) / 2,   // Initial position (center of workspace)
                c_cx + c_border, c_cy + c_border,                                // Width and height of photo border
                (c_cx + c_border) / 2, (c_cy + c_border) / 2,                    // Center of rotation
                c_cx, c_cy));                                                    // Width and height of photo


            WebClient wc = new WebClient();
            ae.SetOperationTag("Loading photo " + count);
            wc.OpenReadCompleted += openReadCompletedEventArgsFactory.PrepareOperation(ae.End()).EventHandler;
            wc.OpenReadAsync(
               new Uri(String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg", photo.farm, photo.server, photo.id, photo.secret), 
                  UriKind.Absolute), item);
            count++;
         }
         while (count-- > 0) {
            yield return 1;  // Process each image as it comes back
            OnReadCompleted(null, openReadCompletedEventArgsFactory.EndInvoke(ae.DequeueAsyncResult()));
         }
      }

#else
      private String m_data;

      private void WebRequestComplete(IAsyncResult ar) {
         // Complete the Flickr request and marshal to the UI thread
         using (HttpWebResponse response = (HttpWebResponse)((HttpWebRequest)ar.AsyncState).EndGetResponse(ar)) {
            using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
               string results = reader.ReadToEnd();
               LayoutRoot.Dispatcher.BeginInvoke((Action<String>)UpdateUI, results);
            }
         }
      }

      private void UpdateUI(string data) {
         m_data = data;
         SearchPanelStoryBoard.Completed += SearchPanelStoryBoard_Completed;
         SearchPanelStoryBoard.Begin();
      }

      private void SearchPanelStoryBoard_Completed(Object sender, EventArgs e) {
         SearchPanelStoryBoard.Completed -= SearchPanelStoryBoard_Completed;
         PhotoBoard.Width = Application.Current.Host.Content.ActualWidth;
         PhotoBoard.Height = Application.Current.Host.Content.ActualHeight - 100.0;

         XDocument doc = XDocument.Parse(m_data);
         var photos = from results in doc.Descendants("photo")
                      select new {
                         id = results.Attribute("id").Value.ToString(),
                         farm = results.Attribute("farm").Value.ToString(),
                         server = results.Attribute("server").Value.ToString(),
                         secret = results.Attribute("secret").Value.ToString()
                      };

         PhotoBoard.Children.Clear();
         Int32 count = 0;

         foreach (var photo in photos) {
            // Create a photo
            Border item = (Border)XamlReader.Load(String.Format(CultureInfo.InvariantCulture,
                c_template, count++,
                (PhotoBoard.Width - c_cx) / 2, (PhotoBoard.Height - c_cy) / 2,   // Initial position (center of workspace)
                c_cx + c_border, c_cy + c_border,                                // Width and height of photo border
                (c_cx + c_border) / 2, (c_cy + c_border) / 2,                    // Center of rotation
                c_cx, c_cy));                                                    // Width and height of photo

            WebClient wc = new WebClient();
            wc.OpenReadCompleted += OnReadCompleted;
            wc.OpenReadAsync(
               new Uri(String.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}_m.jpg", photo.farm, photo.server, photo.id, photo.secret), UriKind.Absolute),
               item);
         }
      }
#endif

      private void OnReadCompleted(object sender, OpenReadCompletedEventArgs e) {
         Stream result = e.Result;
         Border item = (Border)e.UserState;

         // Assign the downloaded bits to the image
         Image image = (Image)item.Child;
         BitmapImage bi = new BitmapImage();
         bi.SetSource(result);
         image.Source = bi;

         // Add the photo to the scene
         PhotoBoard.Children.Add(item);
         item.Visibility = Visibility.Visible;

         // Register event handlers
         item.MouseLeftButtonDown += OnButtonDown;
         item.MouseMove += OnMouseMove;
         item.MouseLeftButtonUp += OnButtonUp;

         // Compute spatial parameters
         double width = Application.Current.Host.Content.ActualWidth;
         double height = Application.Current.Host.Content.ActualHeight - 200.0;

         double x = m_rand.NextDouble() * (width - c_cx);
         double y = m_rand.NextDouble() * (height - c_cy);
         double angle = c_angle - (m_rand.NextDouble() * c_angle * 2);

         // Animate the image
         Storyboard sb = (Storyboard)item.FindName("EntranceStoryboard");
         DoubleAnimation da1 = (DoubleAnimation)item.FindName("DA1");
         DoubleAnimation da2 = (DoubleAnimation)item.FindName("DA2");
         DoubleAnimation da3 = (DoubleAnimation)item.FindName("DA3");

         da1.From = 0;
         da1.To = angle;
         da2.From = (width - c_cx) / 2;
         da2.To = x;
         da3.From = (height - c_cy) / 2;
         da3.To = y;

         sb.Begin();
      }

      private void OnButtonDown(object sender, MouseButtonEventArgs e) {
         // Move the photo to the top of the Z-order
         ((FrameworkElement)sender).SetValue(Canvas.ZIndexProperty, m_zindex++);

         // Record the current mouse position
         m_lastx = e.GetPosition(null).X;
         m_lasty = e.GetPosition(null).Y;

         // Begin dragging
         m_owner = (FrameworkElement)sender;
         ((FrameworkElement)sender).CaptureMouse();
         m_dragging = true;
      }

      private void OnMouseMove(object sender, MouseEventArgs e) {
         if (m_dragging) {
            double x = e.GetPosition(null).X;
            double y = e.GetPosition(null).Y;

            double dx = x - m_lastx;
            double dy = y - m_lasty;

            m_lastx = x;
            m_lasty = y;

            ((FrameworkElement)sender).SetValue(Canvas.LeftProperty, (double)((FrameworkElement)sender).GetValue(Canvas.LeftProperty) + dx);
            ((FrameworkElement)sender).SetValue(Canvas.TopProperty, (double)((FrameworkElement)sender).GetValue(Canvas.TopProperty) + dy);
         }
      }

      private void OnButtonUp(object sender, MouseButtonEventArgs e) {
         ((FrameworkElement)sender).ReleaseMouseCapture();
         m_dragging = false;
      }

      private void OnResized(Object sender, EventArgs e) {
         // When the control resizes, center the search panel horizontally
         double width = Application.Current.Host.Content.ActualWidth;
         SearchPanel.SetValue(Canvas.LeftProperty, (width - 264.0) / 2.0);
      }
   }
}
