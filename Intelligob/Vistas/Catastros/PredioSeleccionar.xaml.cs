using System;
using System.Linq;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;

namespace Intelligob.Escritorio.Vistas.Catastros
{
    /// <summary>
    /// Lógica de interacción para PredioSeleccionar.xaml
    /// </summary>
    public partial class PredioSeleccionar : BaseDialogoVista, IVentanaDialogo
    {
        public PredioSeleccionar()
        {
            InitializeComponent();
        }

        private void txBusqueda_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ((Intelligob.Escritorio.ModeloVista.Catastros.PredioSeleccionarVM)this.DataContext).CmdBuscar.Execute(null);
        }
    }
}
