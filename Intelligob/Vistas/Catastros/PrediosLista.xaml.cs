using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Input;

namespace Intelligob.Escritorio.Vistas.Catastros
{
    /// <summary>
    /// Lógica de interacción para PrediosLista.xaml
    /// </summary>
    public partial class PrediosLista : IPagina
    {
        public PrediosLista()
        {
            InitializeComponent();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.tgbContribuyente.IsChecked == false)
                    ((PrediosListaVM)this.DataContext).CmdBuscar.Execute(null);
            }
        }
    }
}
