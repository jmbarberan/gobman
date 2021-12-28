using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;

namespace Intelligob.Escritorio.Vistas.Catastros
{
    /// <summary>
    /// Lógica de interacción para PatenteSeleccion.xaml
    /// </summary>
    public partial class PatenteSeleccion : BaseDialogoVista, IVentanaDialogo
    {
        public PatenteSeleccion()
        {
            InitializeComponent();
        }

        private void txBusqueda_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ((Intelligob.Escritorio.ModeloVista.Catastros.PatenteSeleccionarVM)this.DataContext).CmdBuscar.Execute(null);
        }
    }
}
