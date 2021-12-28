using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Agua;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows.Input;
using Intelligob.Cliente;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuAguaVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep;

        #region Propiedades de control de privilegios
        private bool prvAgua;
        private bool prvLecturas;
        private bool prvContribuyentes;
        private bool prvEmisiones;
        private bool prvTransacciones;
        private bool prvConsultas;
        #endregion

        #region Comandos
        public ICommand CmdContribuyentes
        {
            get;
            internal set;
        }

        public ICommand CmdAgua
        {
            get;
            internal set;
        }

        public ICommand CmdLecturas
        {
            get;
            internal set;
        }

        public ICommand CmdEmisiones
        {
            get;
            internal set;
        }

        public ICommand CmdTransacciones
        {
            get;
            internal set;
        }

        public ICommand CmdConsultas
        {
            get;
            internal set;
        }
        #endregion

        public MenuAguaVM()
            : this(new MenuAgua())
        { }

        public MenuAguaVM(IPagina pVista)
            : base(pVista)
        {
            if (this.seguridadDep == null)
            {
                seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            }
            this.ProcesarPrivilegios();
            this.CmdContribuyentes = new ComandoDelegado((o) => AccionMostrarContribuyentes(), (o) => HabilitadoContribuyentes());
            this.CmdAgua = new ComandoDelegado((o) => AccionMostrarAgua(), (o) => HabilitadoAgua());
            this.CmdLecturas = new ComandoDelegado((o) => AccionMostrarLecturas(), (o) => HabilitadoLecturas());
            this.CmdEmisiones = new ComandoDelegado((o) => AccionMostrarEmisiones(), (o) => HabilitadoEmisiones());
            this.CmdTransacciones = new ComandoDelegado((o) => AccionMostrarTransacciones(), (o) => HabilitadoTransacciones());
            this.CmdConsultas = new ComandoDelegado((o) => AccionMostrarConsultas(), (o) => HabilitadoConsultas());
        }

        private void ProcesarPrivilegios()
        {
            this.prvContribuyentes = false;
            this.prvAgua = false;
            this.prvLecturas = false;
            this.prvEmisiones = false;
            this.prvTransacciones = false;
            this.prvConsultas = true;

            if (SesionUtiles.Instance.EsDesarrollador || seguridadDep.PrivilegiosFuncionPorUsuario(8, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvAgua = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvContribuyentes = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(16, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvLecturas = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(17, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvEmisiones = true;

            if (SesionUtiles.Instance.EsDesarrollador == true || seguridadDep.PrivilegiosFuncionPorUsuario(18, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.prvTransacciones = true;
        }

        #region Habilitadores de comandos

        private bool HabilitadoContribuyentes()
        {
            return this.prvContribuyentes;
        }

        private bool HabilitadoAgua()
        {
            return this.prvAgua;
        }

        private bool HabilitadoLecturas()
        {
            return this.prvLecturas;
        }

        private bool HabilitadoEmisiones()
        {
            return this.prvEmisiones;
        }

        private bool HabilitadoTransacciones()
        {
            return this.prvTransacciones;
        }

        private bool HabilitadoConsultas()
        {
            return this.prvConsultas;
        }
        
        #endregion

        #region Acciones de Comandos

        private void AccionMostrarContribuyentes()
        {
            ContribuyentesListaVM c = new ContribuyentesListaVM();
            Navegador.NavigationService.Navigate(c.Vista);
        }

        private void AccionMostrarAgua()
        {
            AguaCuentasListaVM a = new AguaCuentasListaVM();
            Navegador.NavigationService.Navigate(a.Vista);
        }

        private void AccionMostrarLecturas()
        {
            //
        }

        private void AccionMostrarEmisiones()
        {
            Agua.EmisionesVM e = new Agua.EmisionesVM();
            Navegador.NavigationService.Navigate(e.Vista);
        }

        private void AccionMostrarTransacciones()
        {
            //
        }

        private void AccionMostrarConsultas()
        {
            General.InformesListaVM il = new General.InformesListaVM(4);
            Navegador.NavigationService.Navigate(il.Vista);
        }

        #endregion
    }
}
