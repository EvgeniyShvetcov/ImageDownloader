using ImageDownloader.Command;
using ImageDownloader.ProperyChangedImpl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageDownloader.ViewModels
{
    class MainWindowViewModel : BasePropertyChanged
    {
        public MainWindowViewModel()
        {
            FirstDownloader = new ImageDownloaderViewModel();
            SecondDownloader = new ImageDownloaderViewModel();
            ThirdDownloader = new ImageDownloaderViewModel();

            FirstDownloader.PropertyChanged += ImageDownloader_PropertyChanged;
            SecondDownloader.PropertyChanged += ImageDownloader_PropertyChanged;
            ThirdDownloader.PropertyChanged += ImageDownloader_PropertyChanged;
        }

        private void ImageDownloader_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "DownloadingProgress")
            {
                OnPropertyChanged("OverallBootProcess");
            }
            else if(e.PropertyName == "IsDownloading")
            {
                if(((ImageDownloaderViewModel)sender).IsDownloading == true)
                {
                    CountOfActiveDownloader++;
                }
                else
                {
                    CountOfActiveDownloader--;
                }
                OnPropertyChanged("CountOfActiveDownloader");
            }
        }

        public MainWindowViewModel(ImageDownloaderViewModel first, ImageDownloaderViewModel second, ImageDownloaderViewModel third)
        {
            FirstDownloader = first;
            SecondDownloader = second;
            ThirdDownloader = third;
        }

        private ImageDownloaderCommand _startDownloadCommand;
        public ICommand StartAllDownloadingCommand
        {
            get
            {
                if (_startDownloadCommand is null)
                {
                    _startDownloadCommand = new ImageDownloaderCommand((obj) =>
                    {
                        FirstDownloader.StartDownload();
                        SecondDownloader.StartDownload();
                        ThirdDownloader.StartDownload();
                    });
                }
                return _startDownloadCommand;
            }
        }

        public double OverallBootProcess
        {
            get
            {
                return (_firstDownloader.DownloadingProgress + _secondDownloader.DownloadingProgress
                        + _thirdDownloader.DownloadingProgress) / (CountOfActiveDownloader != 0 ? CountOfActiveDownloader : 1);
            }
        }

        private ImageDownloaderViewModel _firstDownloader;
        public ImageDownloaderViewModel FirstDownloader
        {
            get => _firstDownloader;
            set
            {
                _firstDownloader = value;
                OnPropertyChanged("FirstDownloader");
            }
        }

        private ImageDownloaderViewModel _secondDownloader;
        public ImageDownloaderViewModel SecondDownloader
        {
            get => _secondDownloader;
            set
            {
                _secondDownloader = value;
                OnPropertyChanged("SecondDownloader");
            }
        }

        private ImageDownloaderViewModel _thirdDownloader;
        public ImageDownloaderViewModel ThirdDownloader
        {
            get => _thirdDownloader;
            set
            {
                _thirdDownloader = value;
                OnPropertyChanged("FirstDownloader");
            }
        }

        private int _countOfActiveDownloader;
        public int CountOfActiveDownloader
        {
            get => _countOfActiveDownloader;
            set
            {
                _countOfActiveDownloader = value;
                OnPropertyChanged("OverallBootProcess");
            }
        }
    }
}
