using System;
using System.Windows.Input;

namespace DeskLinkServer.Framework.Base
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (canExecute == null || canExecute.Invoke(parameter));
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
