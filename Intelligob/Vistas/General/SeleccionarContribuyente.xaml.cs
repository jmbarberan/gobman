using System;
using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.Interfaces;
namespace Intelligob.Escritorio.Vistas.General
{
    /// <summary>
    /// Lógica de interacción para SeleccionarContribuyente.xaml
    /// </summary>
    public partial class SeleccionarContribuyente : BaseDialogoVista, IVentanaDialogo
    {
        public SeleccionarContribuyente()
        {
            InitializeComponent();
        }

        private void txFiltro_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (!String.IsNullOrWhiteSpace(this.txFiltro.Text.Trim()))
                    ((SeleccionarContribuyenteVM)this.DataContext).CmdConsultar.Execute(null);
            }
        }
    }
}
