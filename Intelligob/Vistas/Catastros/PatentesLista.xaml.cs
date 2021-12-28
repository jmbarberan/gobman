using Intelligob.Escritorio.ModeloVista.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.Vistas.Catastros
{
    /// <summary>
    /// Lógica de interacción para PatentesLista.xaml
    /// </summary>
    public partial class PatentesLista : IPagina
    {
        public PatentesLista()
        {
            InitializeComponent();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.tgbContribuyente.IsChecked == false)
                    ((PatentesListaVM)this.DataContext).CmdBuscar.Execute(null);
            }
        }
    }
}
