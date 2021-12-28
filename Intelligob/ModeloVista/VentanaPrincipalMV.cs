using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class VentanaPrincipalMV: BaseMV<IVentanaPrincipal>
    {
        #region Comandos de botones de Modulos
        private readonly ICommand cmdModAgua1;
        private readonly ICommand cmdModCatastros1;
        private readonly ICommand cmdModSeguridad1;
        private readonly ICommand cmdModConfiguracion1;
        private readonly ICommand cmdModRecaudaciones;
        private readonly ICommand cmdModRentas;

        public ICommand CmdModAgua
        {
            get { return this.cmdModAgua1; }
        }

        public ICommand CmdModCatastros
        {
            get { return this.cmdModCatastros1; }
        }

        public ICommand CmdModSeguridad
        {
            get { return this.cmdModSeguridad1; }
        }

        public ICommand CmdModConfiguracion
        {
            get { return this.cmdModConfiguracion1; }
        }

        public ICommand CmdModRecaudaciones
        { get { return this.cmdModRecaudaciones; } }

        public ICommand CmdModRentas
        { get { return this.cmdModRentas; } }

        #endregion

        public bool Bloqueando = false;
        private readonly ICommand cerrar;
        private readonly ICommand bloquear;
        private string tituloPagina;

        public ICommand Cerrar
        {
            get { return this.cerrar; }
        }

        public ICommand Bloquear
        {
            get { return this.bloquear; }
        }

        public string TituloPaginaActiva
        {
            get { return this.tituloPagina; }
            set
            {
                this.tituloPagina = value;
                OnPropertyChanged("TituloPaginaActiva");
            }
        }

        private string nombreEmpresa;
        public string NombreEmpresa
        {
            get { return this.nombreEmpresa; }
            set { this.nombreEmpresa = value; OnPropertyChanged("NombreEmpresa"); }
        }

        public VentanaPrincipalMV()
            : this(new VentanaPrincipal())
        { }

        public VentanaPrincipalMV(IVentanaPrincipal vista)
            : base(vista)
        {
            this.cerrar = new Comandos.ComandoDelegado((o) => AccionCerrar());
            this.bloquear = new Comandos.ComandoDelegado((o) => AccionBloquear());
            this.cmdModAgua1 = new Comandos.ComandoDelegado((o) => AccionModAgua());
            this.cmdModCatastros1 = new Comandos.ComandoDelegado((o) => AccionModCatastros());
            this.cmdModSeguridad1 = new Comandos.ComandoDelegado((o) => AccionModSeguridad());
            this.cmdModConfiguracion1 = new Comandos.ComandoDelegado((o) => AccionModConfiguracion());
            this.cmdModRecaudaciones = new Comandos.ComandoDelegado((o) => AccionRecaudaciones());
            this.cmdModRentas = new Comandos.ComandoDelegado((o) => AccionRentas());
            this.MostrarVista();
        }

        public void CargarDatos()
        {
            try
            {
                TablasDep tablasDep = DepositosControl.Instance.TablasDepositoCrear();
                this.NombreEmpresa = tablasDep.NombreEmpresa;
            }
            catch (Exception e)
            {
                CuadroMensajes.Alertar("Error", "Ha ocurrido el siguiente error:", e.Message, "");
            }
        }

        protected virtual void MostrarVista()
        {
            this.Vista.Show();
        }

        private void AccionCerrar()
        {
            this.Vista.Close();
        }

        private void AccionBloquear()
        {
            Bloqueando = true;
            Configuracion.InsPriAuxiliar.ModulosTodos(System.Windows.Visibility.Collapsed);
            Configuracion.InsPriAuxiliar.ModuloVisibilidad = System.Windows.Visibility.Hidden;
            ValidarIngresoVM vim = new ValidarIngresoVM();
            Inicio v = new Inicio();
            NavegadorFunciones.NavigationService.Navigate(vim.Vista);
            Navegador.NavigationService.Navigate(v);
            Bloqueando = false;
        }

        #region Acciones Modulos

        private void AccionModAgua()
        {
            MenuAguaVM ma = new MenuAguaVM();
            NavegadorFunciones.NavigationService.Navigate(ma.Vista);
            Configuracion.MenuInicial = "MenuAguaVM";
        }

        private void AccionModCatastros()
        {
            MenuCatastrosVM mc = new MenuCatastrosVM();
            NavegadorFunciones.NavigationService.Navigate(mc.Vista);
            Configuracion.MenuInicial = "MenuCatastrosVM";
        }

        private void AccionRecaudaciones()
        {
            MenuRecaudacionesVM mr = new MenuRecaudacionesVM();
            NavegadorFunciones.NavigationService.Navigate(mr.Vista);
            Configuracion.MenuInicial = "MenuRecaudacionesVM";
        }

        private void AccionRentas()
        {
            MenuRentasVM mr = new MenuRentasVM();
            NavegadorFunciones.NavigationService.Navigate(mr.Vista);
            Configuracion.MenuInicial = "MenuRentasVM";
        }

        private void AccionModSeguridad()
        {
            MenuSeguridadVM ms = new MenuSeguridadVM();
            NavegadorFunciones.NavigationService.Navigate(ms.Vista);
            Configuracion.MenuInicial = "MenuSeguridadVM";
        }

        private void AccionModConfiguracion()
        {
            MenuConfiguracionVM mf = new MenuConfiguracionVM();
            NavegadorFunciones.NavigationService.Navigate(mf.Vista);
            Configuracion.MenuInicial = "MenuConfiguracionVM";
        }

        #endregion
    }
}
