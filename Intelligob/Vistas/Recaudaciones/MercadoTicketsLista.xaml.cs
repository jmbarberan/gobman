using Intelligob.Escritorio.ModeloVista.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Intelligob.Escritorio.Vistas.Recaudaciones
{
    /// <summary>
    /// Interaction logic for MercadoLista.xaml
    /// </summary>
    public partial class MercadoTicketsLista : IPagina
    {
        public MercadoTicketsLista()
        {
            InitializeComponent();
        }

        private void TxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //((MercadoListaMV)DataContext).Buscar();
            }
        }
    }
}
