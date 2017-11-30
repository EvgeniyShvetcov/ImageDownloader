using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageDownloader.Views
{
    /// <summary>
    /// Логика взаимодействия для ImageDownloaderView.xaml
    /// </summary>
    public partial class ImageDownloaderView : UserControl
    {
        public ImageDownloaderView()
        {
            InitializeComponent();
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            var webClient = new WebClient();
            webClient.DownloadProgressChanged += (s, ev) =>
            {
                TextBox1.Text = ev.ProgressPercentage.ToString();
            };
            webClient.DownloadDataCompleted += (s, ev) =>
            {
                var image = new BitmapImage();
                if(ev.Result.Length == 0) { }
                else
                {
                    using (var stream = new MemoryStream(ev.Result))
                    {
                        stream.Position = 0;
                        image.BeginInit();
                            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.UriSource = null;
                            image.StreamSource = stream;
                        image.EndInit();
                    }
                    image.Freeze();
                    ImageSource = image;
                    image1.Source = image;
                }
            };
            webClient.DownloadDataAsync(new Uri("http://www.bfoto.ru/images/proverka_tcveta.jpg"));
        }*/
    }
}
