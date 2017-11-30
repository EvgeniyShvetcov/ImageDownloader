using ImageDownloader.ProperyChangedImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageDownloader.Models
{
    class ImageDownloaderModel : BasePropertyChanged
    {
        public void DownloadImage(string Uri, IProgress<double> progress)
        {
            _isCancelled = new CancellationTokenSource();

            var imageBytes = new byte[0];
            using (var webClient = new WebClient())
            {
                var cancellationToken = _isCancelled.Token;
                cancellationToken.Register(() => webClient.CancelAsync());
                webClient.DownloadDataCompleted += (sender, completedEvent) =>
                {
                    if (!completedEvent.Cancelled)
                    {
                        ImageSource = BitsToImage(completedEvent.Result);
                    }
                };
                webClient.DownloadProgressChanged += (sender, changedEvent) => progress.Report(changedEvent.ProgressPercentage);
                webClient.DownloadDataAsync(new Uri(Uri));

            }
        }

        public void CancelDownload()
        {
            if (_isCancelled is null)
            {
                throw new TypeLoadException("Ошибка при остановке загрузки");
            }
            _isCancelled.Cancel();
        }

        public BitmapImage BitsToImage(byte[] imageArray)
        {
            var image = new BitmapImage();
            if (imageArray.Length != 0)
            {
                using (var stream = new MemoryStream(imageArray))
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
            }
            return image;
        }

        private CancellationTokenSource _isCancelled;

        private BitmapImage _downloadedImage;
        public BitmapImage ImageSource
        {
            get => _downloadedImage;
            set
            {
                if (value != null)
                {
                    _downloadedImage = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
