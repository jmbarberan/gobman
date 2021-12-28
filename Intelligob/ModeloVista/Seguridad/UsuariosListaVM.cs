using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Seguridad;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Intelligob.Cliente;

namespace Intelligob.Escritorio.ModeloVista
{
    class UsuariosListaVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep = new SeguridadDep();
        private ObservableCollection<UsuarioDto> lUsuarios;
        public ObservableCollection<UsuarioDto> LUsuarios
        {
            get { return this.lUsuarios; }
            set
            {
                this.lUsuarios = value;
                OnPropertyChanged("LUsuarios");
            }
        }

        private UsuarioDto seleccionado;
        public UsuarioDto Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                this.seleccionado = value;
                OnPropertyChanged("Seleccionado");
            }
        }

        private int filtro = 0;
        public int Filtro
        {
            get { return this.filtro; }
            set
            {
                this.filtro = value;
                OnPropertyChanged("Filtro");
            }
        }

        private string barraEstado;
        public string BarraEstado
        {
            get { return this.barraEstado; }
            set
            {
                this.barraEstado = value;
                OnPropertyChanged("BarraEstado");
            }
        }

        #region Habilitadores y Comandos
        private bool puedeCrear;
        private bool puedeModificar;
        private bool puedeEliminar;
        private bool puedeRestaurar;
        private bool puedeRestringir;

        public ICommand CmdBuscar
        {
            get;
            internal set;
        }
        public ICommand CmdCrear
        {
            get;
            internal set;
        }
        public ICommand CmdModificar
        {
            get;
            internal set;
        }
        public ICommand CmdEliminar
        {
            get;
            internal set;
        }
        public ICommand CmdRestaurar
        {
            get;
            internal set;
        }
        public ICommand CmdRestringir
        {
            get;
            internal set;
        }
        public ICommand CmdRegresar
        {
            get;
            internal set;
        }
        public ICommand CmdAdelante { get; internal set; }

        #endregion

        public UsuariosListaVM() : this(new UsuariosLista()) { }

        public UsuariosListaVM(IPagina pvista) : base(pvista)
        {
            this.ProcesarPrivilegios();
            this.LUsuarios = new ObservableCollection<UsuarioDto>(seguridadDep.UsuariosPorEstado(0));
            this.CrearComandos();
        }

        private void ProcesarPrivilegios()
        {
            this.puedeCrear = false;
            this.puedeModificar = false;
            this.puedeEliminar = false;
            this.puedeRestaurar = false;
            this.puedeRestringir = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador && SesionUtiles.Instance.UsuarioActivo.Id > 1)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(8, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || c.Contains("1"))
                this.puedeCrear = true;
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || c.Contains("2"))
                this.puedeModificar = true;
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || c.Contains("3"))
                this.puedeEliminar = true;
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || c.Contains("5"))
                this.puedeRestaurar = true;
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || c.Contains("4"))
                this.puedeRestringir = true;
        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => AccionBuscar());
            this.CmdCrear = new ComandoDelegado((o) => AccionCrear(), (o) => PuedeCrear());
            this.CmdModificar = new ComandoDelegado((o) => AccionModificar(), (o) => PuedeModificar());
            this.CmdEliminar = new ComandoDelegado((o) => AccionEliminar(), (o) => PuedeEliminar());
            this.CmdRestaurar = new ComandoDelegado((o) => AccionRestaurar(), (o) => PuedeRestaurar());
            this.CmdRestringir = new ComandoDelegado((o) => AccionRestringir(), (o) => PuedeRestringir());
            this.CmdRegresar = new ComandoDelegado((o) => AccionRegresar(), (o) => PuedeRegresar());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
        }

        #region Habilitadores de comandos
        private bool PuedeCrear()
        {
            return this.puedeCrear;
        }
        private bool PuedeModificar()
        {
            return this.puedeModificar && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }
        private bool PuedeEliminar()
        {
            return this.puedeEliminar && this.Seleccionado != null && this.Seleccionado.Estado == 0 && this.Seleccionado.Id > 1;
        }
        private bool PuedeRestaurar()
        {
            return this.puedeRestaurar && this.Seleccionado != null && this.Seleccionado.Estado == 2;
        }
        private bool PuedeRestringir()
        {
            return this.puedeRestringir && this.Seleccionado != null && this.Seleccionado.Id > 1;
        }
        private bool PuedeRegresar()
        {
            return Navegador.NavigationService.CanGoBack;
        }
        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }
        #endregion

        #region Acciones de comandos
        private void AccionBuscar()
        {
            switch (Filtro)
            {
                case 1: { LUsuarios = new ObservableCollection<UsuarioDto>(seguridadDep.UsuariosPorEstado(9)); break; }
                case 2: { LUsuarios = new ObservableCollection<UsuarioDto>(seguridadDep.UsuariosPorEstado(2)); break; }
                default: { LUsuarios = new ObservableCollection<UsuarioDto>(seguridadDep.UsuariosPorEstado(0)); break; }
            }
        }
        private void AccionCrear()
        {
            UsuarioEditorVM ce = new UsuarioEditorVM();
            if (ce.Vista.DialogResult == true)
            {
                this.LUsuarios.Add(ce.EUsuario);
                this.BarraEstado = string.Format("{0} Fue creado", ce.EUsuario.Nombres);
            }
        }
        private void AccionModificar()
        {
            UsuarioEditorVM ce = new UsuarioEditorVM(this.Seleccionado);
            if (ce.Vista.DialogResult == true)
            {
                int index = this.LUsuarios.IndexOf(this.Seleccionado);
                this.LUsuarios.Remove(this.Seleccionado);
                this.LUsuarios.Insert(index, ce.EUsuario);
                this.BarraEstado = string.Format("{0} Fue modificado", ce.EUsuario.Nombres);
            }
        }
        private void AccionEliminar()
        {
            this.Seleccionado.Estado = 2;
            seguridadDep.UsuarioModificar(this.Seleccionado);
        }
        private void AccionRestaurar()
        {
            this.Seleccionado.Estado = 0;
            seguridadDep.UsuarioModificar(this.Seleccionado);
        }
        private void AccionRestringir()
        {
            var prv = new UsuarioPrivilegiosVM(seguridadDep, Seleccionado);            
        }
        private void AccionRegresar()
        {
            Navegador.NavigationService.GoBack();
        }
        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }
        #endregion
    }
}
