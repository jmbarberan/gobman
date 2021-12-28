using System.Windows;

namespace Intelligob.Escritorio.Vistas.Seguridad
{
    /// <summary>
    /// Lógica de interacción para Clave.xaml
    /// </summary>
    public partial class Clave : Window
    {
        public string Contraseña = "";
        public Clave()
        {
            InitializeComponent();
        }

        public Clave(string pcontraseña)
        {
            InitializeComponent();
            txClave.Password = pcontraseña;
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txClave.Password.Length > 0)
            {
                Contraseña = txClave.Password;
                DialogResult = true;
            }
            this.Close();
        }
    }
}
