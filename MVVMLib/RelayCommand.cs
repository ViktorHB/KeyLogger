using System;
using System.Windows.Input;

namespace MWVMLib
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }
        public RelayCommand(Action<object> execute,
                            Predicate<object> canExecute = null)
        {
            _canExecute = canExecute;
            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute.Invoke(parameter);
        }
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
               CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

    }
}
