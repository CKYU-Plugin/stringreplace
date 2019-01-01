using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.Models
{
    public class SimpleCommand : ICommand
    {
        public SimpleCommand(Action<object> execute = null, Predicate<object> canExecute = null)
        {
            this.CanExecuteDelegate = canExecute;
            this.ExecuteDelegate = execute;
        }

        public Predicate<object> CanExecuteDelegate { get; set; }
        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)
        {
            var canExecute = this.CanExecuteDelegate;
            return canExecute == null || canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.ExecuteDelegate?.Invoke(parameter);
        }
    }
}
