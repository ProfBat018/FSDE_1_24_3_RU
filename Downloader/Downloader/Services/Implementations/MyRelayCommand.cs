using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downloader.Services.Implementations;
class MyRelayCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly Action _action;
    private readonly Func<bool> _canExecute;

    public MyRelayCommand(Action action)
        : this(action, () => true) { }

    public MyRelayCommand(Action action, Func<bool> canExecute)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute();

    public void Execute(object? parameter) => _action();

    public void RaiseCanExecuteChanged()
    { 
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
