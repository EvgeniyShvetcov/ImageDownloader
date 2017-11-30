using ImageDownloader.Command;
using ImageDownloader.Models;
using ImageDownloader.ProperyChangedImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageDownloader.ViewModels
{
    class ImageDownloaderViewModel : BasePropertyChanged
    {
        public ImageDownloaderViewModel()
        {
            _imageDownloader = new ImageDownloaderModel();
            _imageDownloader.PropertyChanged += OnModelPropertyChanged;
        }

        public BitmapImage ImageSource
        {
            get => _imageDownloader.ImageSource;
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != null)
                {
                    _imageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }

        private double _downloadingProgress;
        public double DownloadingProgress
        {
            get
            {
                return _downloadingProgress;
            }
            set
            {
                _downloadingProgress = value;
                OnPropertyChanged();
            }
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get => _isDownloading;
            set
            {
                _isDownloading = value;
                OnPropertyChanged();
            }
        }

        private ImageDownloaderCommand _startDownloadCommand;
        public ICommand StartDownloadingCommand
        {
            get
            {
                if(_startDownloadCommand is null)
                {
                    _startDownloadCommand = new ImageDownloaderCommand((obj) =>
                    {
                        StartDownload();
                    });
                }
                return _startDownloadCommand;
            }
        }

        public void StartDownload()
        {
            _imageDownloader.ImageSource = null;
            if (ImageUrl != null)
                _imageDownloader.DownloadImage(ImageUrl, new Progress<double>(p => DownloadingProgress = p));
            IsDownloading = true;
        }

        private ImageDownloaderCommand _stopDownloadCommand;
        public ICommand StopDownloadingCommand
        {
            get
            {
                if (_stopDownloadCommand is null)
                {
                    _stopDownloadCommand = new ImageDownloaderCommand((obj) =>
                    {
                        StopDownload();
                    });
                }
                return _stopDownloadCommand;
            }
        }

        public void StopDownload()
        {
            IsDownloading = false;
            DownloadingProgress = 0.0;
            _imageDownloader.ImageSource = null;
            _imageDownloader.CancelDownload();
        }

        private readonly ImageDownloaderModel _imageDownloader;

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
