using System;
using System.Linq;

namespace Intelligob.Escritorio.Vistas.Emisiones
{
    /// <summary>
    /// Lógica de interacción para Bajas.xaml
    /// </summary>
    public partial class Bajas : Intelligob.Escritorio.Vistas.Interfaces.IPagina
    {
        public Bajas()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.txAño.Focus();
            }
        }
    }
}
