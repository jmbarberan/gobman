using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Intelligob.Escritorio.Vistas.General
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window, IVentanaPrincipal
    {
        //private bool pfExpandido = true;
        private bool pmExpandido = true;

        public VentanaPrincipal()
        {
            InitializeComponent();            
            Storyboard sbe = this.FindResource("sbExpandirModulos") as Storyboard;
            Storyboard sbc = this.FindResource("sbContraerModulos") as Storyboard;
            PanelModulos.BeginStoryboard(sbe);
            PanelModulos.BeginStoryboard(sbc);
        }

        private void tabButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                Configuracion.InsPriAuxiliar.MenuExpandido = !Configuracion.InsPriAuxiliar.MenuExpandido;
                //tabButton.CambiarIcono(Configuracion.InsPriAuxiliar.MenuExpandido);
            }
        }

        private void tabModulos_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                if (pmExpandido == false)
                {
                    Storyboard sb = this.FindResource("sbExpandirModulos") as Storyboard;                    
                    PanelModulos.BeginStoryboard(sb);
                }
                else
                {
                    Storyboard sb = this.FindResource("sbContraerModulos") as Storyboard;
                    PanelModulos.BeginStoryboard(sb);
                }
                pmExpandido = !pmExpandido;
                //tabModulos.CambiarIcono(pmExpandido);
            }
        }

        private void FrameNavegador_Navigated(object sender, NavigationEventArgs e)
        {
            bool blo = ((Intelligob.Escritorio.ModeloVista.VentanaPrincipalMV)this.DataContext).Bloqueando;
            if (!blo)
            {
                if (Configuracion.InsPriAuxiliar.Iniciando)
                {
                    Configuracion.InsPriAuxiliar.Iniciando = false;
                }
                else
                {
                    if (Configuracion.InsPriAuxiliar.MenuExpandido == true)
                    {
                        Configuracion.InsPriAuxiliar.MenuExpandido = false;
                    } 
                }
            }
            if (e.Content != null)
            {
                try
                {
                    ((Intelligob.Escritorio.ModeloVista.VentanaPrincipalMV)this.DataContext).TituloPaginaActiva = " : " + ((Page)e.Content).Title;
                }
                catch
                {
                    ((Intelligob.Escritorio.ModeloVista.VentanaPrincipalMV)this.DataContext).TituloPaginaActiva = " : Gestion Administrativa";
                }
            }
        }

        private void FrameFunciones_Navigated(object sender, NavigationEventArgs e)
        {
            if (!Configuracion.InsPriAuxiliar.MenuExpandido)
                Configuracion.InsPriAuxiliar.MenuExpandido = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Cliente.Depositos.EmisionAsyncronica.Instance.IsBusy)
            {
                Utiles.CuadroMensajes.Alertar("Intelligob", "No se puede salir", "Se esta ejecutando una emision general, no puede salir hasta que esta finalice", "");
                e.Cancel = true;
            }
        }
    }
}
