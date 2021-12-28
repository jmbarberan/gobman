using Intelligob.Escritorio.Vistas.General;
using System.Windows;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.ModeloVista;

namespace Intelligob.Escritorio.Vistas.Seguridad
{
    /// <summary>
    /// Lógica de interacción para UsuarioEditor.xaml
    /// </summary>
    public partial class UsuarioEditor : BaseDialogoVista, IVentanaDialogo, IVentanaMetodo
    {
        private bool enviarAvm = true;
        public UsuarioEditor()
        {
            InitializeComponent();            
        }

        private void txClave_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (enviarAvm)
                ((UsuarioEditorVM)this.DataContext).Clave = txClave.Password;
        }

        public void Ejecutar()
        {
            enviarAvm = false;
            this.txClave.Password = ((UsuarioEditorVM)this.DataContext).Clave;
            enviarAvm = true;
        }
    }
}
