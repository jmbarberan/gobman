using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Input;

namespace Intelligob.Escritorio.Vistas
{
    /// <summary>
    /// Lógica de interacción para CuentasLista.xaml
    /// </summary>
    public partial class AguaCuentasLista : IPagina
    {
        public AguaCuentasLista()
        {
            InitializeComponent();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.cbxCriterio.SelectedIndex != 0)
                {
                    ((Intelligob.Escritorio.ModeloVista.AguaCuentasListaVM)this.DataContext).CmdBuscar.Execute(null);
                }
            }
        }
    }
}
