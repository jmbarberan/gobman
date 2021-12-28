using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.Interfaces;

namespace Intelligob.Escritorio.Vistas.General
{
    /// <summary>
    /// Lógica de interacción para ValidarIngreso.xaml
    /// </summary>
    public partial class ValidarIngreso : IPagina
    {
        public ValidarIngreso()
        {
            InitializeComponent();
        }

        private void txUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (this.txUsuario.Text.ToUpper() == "DESARROLLADOR")
                    ((ValidarIngresoVM)this.DataContext).AccionIngresar(null);
                else
                    this.txClave.Focus();
            }
        }

        private void txClave_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ((ValidarIngresoVM)this.DataContext).UsrClave = txClave.Password;
                ((ValidarIngresoVM)this.DataContext).AccionIngresar(null);
            }
        }

        private void txClave_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            ((ValidarIngresoVM)this.DataContext).UsrClave = txClave.Password;
        }
    }
}
