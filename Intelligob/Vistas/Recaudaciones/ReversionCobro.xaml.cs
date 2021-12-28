using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;

namespace Intelligob.Escritorio.Vistas.Recaudaciones
{
    /// <summary>
    /// Lógica de interacción para ReversionCobro.xaml
    /// </summary>
    public partial class ReversionCobro : IPagina
    {
        public ReversionCobro()
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
