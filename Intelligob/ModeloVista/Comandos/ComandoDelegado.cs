using System;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Comandos
{
    public class ComandoDelegado : ICommand
    {
        #region Fields

        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always pexecute.
        /// </summary>
        /// <param name="pexecute">The execution logic.</param>
        public ComandoDelegado(Action<object> pexecute)
            : this(pexecute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="pexecute">The execution logic.</param>
        /// <param name="pcanExecute">The execution status logic.</param>
        public ComandoDelegado(Action<object> pexecute, Predicate<object> pcanExecute)
        {
            if (pexecute == null)
                throw new ArgumentNullException("execute");

            execute = pexecute;
            canExecute = pcanExecute;
        }        

        #endregion // Constructors

        #region ICommand Members
        
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        #endregion // ICommand Members
    }
}