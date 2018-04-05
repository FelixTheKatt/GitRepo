using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToolboxMvvm.wpf
{
    
    public class CommandBase : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public CommandBase(Action execute)
        {
            _execute = execute;
        }

        public CommandBase(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    //reference a presentation core , le system.WIndows.input ne suffit pas
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {

                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
