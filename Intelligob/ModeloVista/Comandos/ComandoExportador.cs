using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Escritorio.ModeloVista.Comandos
{
    public class ComandoExportar : System.Windows.Input.ICommand
    {
        readonly Predicate<object> canExecute;
        private readonly Vistas.Interfaces.IModeloExportador modelo;

        public ComandoExportar(Vistas.Interfaces.IModeloExportador pModelo, Predicate<object> pcanExecute)
        {
            modelo = pModelo;
            this.canExecute = pcanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { System.Windows.Input.CommandManager.RequerySuggested += value; }
            remove { System.Windows.Input.CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object param)
        {
            this.modelo.Exportar(param);
        }
    }
}
