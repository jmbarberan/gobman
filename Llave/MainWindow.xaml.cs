using System;
using System.Linq;
using System.Windows;
using Intelligob.Cliente.Depositos;

namespace Llave
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SeguridadDep dep = new SeguridadDep();
        public MainWindow()
        {
            InitializeComponent();
            
            if (dep.UsuariosDesarrolladorActivo())
                this.btComando.Content = "Desactivar";
            else
                this.btComando.Content = "Activar";
        }

        private void btComando_Click(object sender, RoutedEventArgs e)
        {
            if (this.btComando.Content == "Activar")
            {
                dep.UsuarioDesarrolladorAcceso(1);
                this.btComando.Content = "Desactivar";
                
            }
            else
            {
                dep.UsuarioDesarrolladorAcceso(0);
                this.btComando.Content = "Activar";
            }
                
        }
    }
}
