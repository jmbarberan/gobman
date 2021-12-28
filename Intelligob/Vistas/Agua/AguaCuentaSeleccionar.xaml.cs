using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;

namespace Intelligob.Escritorio.Vistas.Agua
{
    /// <summary>
    /// Lógica de interacción para AguaCuentaSeleccionar.xaml
    /// </summary>
    public partial class AguaCuentaSeleccionar :  BaseDialogoVista, IVentanaDialogo
    {
        public AguaCuentaSeleccionar()
        {
            InitializeComponent();
        }

        private void txBusqueda_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ((Intelligob.Escritorio.ModeloVista.Agua.AguaCuentaSeleccionarVM)this.DataContext).CmdBuscar.Execute(null);
        }
    }
}
