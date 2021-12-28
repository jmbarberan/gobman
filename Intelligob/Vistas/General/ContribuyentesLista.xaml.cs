using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Controls;
using System.Windows.Input;

namespace Intelligob.Escritorio.Vistas.General
{
    /// <summary>
    /// Lógica de interacción para ContribuyentesLista.xaml
    /// </summary>
    public partial class ContribuyentesLista : Page, IPagina
    {
        public ContribuyentesLista()
        {
            InitializeComponent();
        }

        private void TxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ((ContribuyentesListaVM)this.DataContext).CmdBuscar.Execute(null);
        }
    }
}
