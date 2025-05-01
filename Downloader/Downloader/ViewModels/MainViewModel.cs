using Downloader.Services.Abstractions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace Downloader.ViewModels;

class MainViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    private ViewModelBase _currentView;
        
    public ViewModelBase CurrentView
    {
        get { return _currentView; }
        set { Set(ref _currentView, value); }
    }


    public MainViewModel(IMessenger messenger, INavigationService navigationService)
    {
        _messenger = messenger;
        _navigationService = navigationService;

        CurrentView = App.Container.GetInstance<SearchViewModel>();
    }
}
