using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Messages;

class NavigationMessage
{
    public ViewModelBase ViewModelType { get; set; }
}
