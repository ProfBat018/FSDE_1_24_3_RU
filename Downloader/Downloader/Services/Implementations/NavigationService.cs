using Downloader.Services.Abstractions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Services.Implementations;

class NavigationService : INavigationService
{
    public void NavigateTo<T>() where T : ViewModelBase
    {
        throw new NotImplementedException();
    }
}
