using Downloader.Services.Abstractions;
using Downloader.Services.Implementations;
using Downloader.ViewModels;
using Downloader.Views;
using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Downloader;


public partial class App : Application
{
    public static Container Container { get; private set; }

    void Register()
    {
        Container = new();

        Container.RegisterSingleton<IMessenger, Messenger>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IAudioService, AudioService>();
        Container.RegisterSingleton<IVideoService, VideoService>();
        
        Container.RegisterSingleton<MainViewModel>(); // MainViewModel mainViewModel = new(_navigationService, _messenger);
        Container.RegisterSingleton<SearchViewModel>();
        Container.RegisterSingleton<InfoViewModel>();

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        Register();

        Window mainWindow = new MainView();
        mainWindow.DataContext = Container.GetInstance<MainViewModel>();

        MainWindow.ShowDialog();

    }

}

