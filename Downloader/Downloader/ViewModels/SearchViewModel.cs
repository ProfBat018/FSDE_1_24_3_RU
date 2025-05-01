using Downloader.Services.Abstractions;
using Downloader.Services.Implementations;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Downloader.ViewModels;

class SearchViewModel : ViewModelBase, INotifyPropertyChanged
{
    private static readonly Regex YoutubeUrlRegex = new Regex(
    @"^(https?\:\/\/)?(www\.)?(youtube\.com\/watch\?v=|youtu\.be\/)[\w\-]{11}(&.+)?$",
    RegexOptions.Compiled | RegexOptions.IgnoreCase);


    private readonly IAudioService _audioService;
    private readonly IVideoService _videoService;
    private string _url;

    public SearchViewModel(IAudioService audioService, IVideoService videoService)
    {
        _audioService = audioService;
        _videoService = videoService;
    }


    public MyRelayCommand DownloadCommand { get => new(() =>
    {
        MessageBox.Show("Buenos dias");
    }, CanDownload);}

    public string URL
    {
        get => _url;
        set
        {
            if (_url != value)
            {
                _url = value;
                MessageBox.Show("Url изменён: " + _url);
                OnPropertyChanged(nameof(URL));
                DownloadCommand.RaiseCanExecuteChanged(); // ⚠️ важно!
            }
        }
    }

    private bool CanDownload()
    {
        return !string.IsNullOrWhiteSpace(URL) && YoutubeUrlRegex.IsMatch(URL.Trim());
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}