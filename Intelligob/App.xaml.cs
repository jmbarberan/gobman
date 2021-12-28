using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Utiles;
using System;
using System.Linq;
using System.Windows;

namespace Intelligob.Escritorio
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            VentanaPrincipalMV pm = new VentanaPrincipalMV();
            bool servidorHabilitado = true;
            try
            {
                Intelligob.Cliente.Referencia.IEntidades er = new Intelligob.Cliente.Referencia.EntidadesClient(Configuracion.ConfiguracionPunto, Configuracion.DireccionServidor);
                TablaDto tab = er.ReadTablas().Where(t => t.Id == 1).FirstOrDefault();
                servidorHabilitado = true;
            }
            catch (Exception ex)
            {
                TaskDialogInterop.TaskDialogResult r = CuadroMensajes.Alertar("Error", "No se puede establecer una conexion al servidor", ex.Message, "Configuracion direccion del servidor");
                servidorHabilitado = false;
                if (r.VerificationChecked == true)
                {
                    ConexionServidorVM cs = new ConexionServidorVM();
                    cs.Vista.Owner = (Window)pm.Vista;
                    cs.Vista.ShowDialog();
                    if (cs.Vista.DialogResult == true)
                    {
                        try
                        {
                            Intelligob.Cliente.Referencia.IEntidades er = new Intelligob.Cliente.Referencia.EntidadesClient(Configuracion.ConfiguracionPunto, Configuracion.DireccionServidor);
                            TablaDto tab = er.ReadTablas().Where(t => t.Id == 1).FirstOrDefault();
                            servidorHabilitado = true;
                        }
                        catch (Exception ex1)
                        {
                            CuadroMensajes.Alertar("Error", "El sistema se cerrara", "No se ha podido establecer una conexion con el servidor, Error: " + ex1.Message, "");
                            servidorHabilitado = false; 
                        }
                    }
                }
            }


            if (!servidorHabilitado)
            {
                Shutdown(1);
                return;
            }
            pm.CargarDatos();
            Navegador.NavigationService = ((VentanaPrincipal)pm.Vista).FrameNavegador.NavigationService;
            NavegadorFunciones.NavigationService = ((VentanaPrincipal)pm.Vista).FrameFunciones.NavigationService;
            ValidarIngresoVM vim = new ValidarIngresoVM();
            NavegadorFunciones.NavigationService.Navigate(vim.Vista);
            pm.Vista.Show();
            Configuracion.InsPriAuxiliar.MenuExpandido = true;
        }
    }
}
