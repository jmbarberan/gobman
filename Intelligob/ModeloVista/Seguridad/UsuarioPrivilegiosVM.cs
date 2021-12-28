using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Seguridad;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Intelligob.Utiles;

namespace Intelligob.Escritorio.ModeloVista
{
    class UsuarioPrivilegiosVM : BaseMV<IVentanaDialogo>
    {
        private bool removiendoPrivilegio;
        private readonly SeguridadDep seguridadDep;

        private ObservableCollection<FuncionDto> lFunciones;
        public ObservableCollection<FuncionDto> LFunciones
        {
            get { return this.lFunciones; }
            set
            {
                this.lFunciones = value;
                OnPropertyChanged("LFunciones");
            }
        }

        private FuncionDto funcionSeleccionada;
        public FuncionDto FuncionSeleccionada
        {
            get { return this.funcionSeleccionada; }
            set
            {
                this.funcionSeleccionada = value;
                OnPropertyChanged("FuncionSeleccionada");
            }
        }

        private ObservableCollection<PrivilegioDto> lPrivilegios;
        public ObservableCollection<PrivilegioDto> LPrivilegios
        {
            get { return this.lPrivilegios; }
            set
            {
                this.lPrivilegios = value;
                OnPropertyChanged("LPrivilegios");
            }
        }
        private PrivilegioDto privilegioSeleccionado;

        public PrivilegioDto PrivilegioSeleccionado
        {
            get { return this.privilegioSeleccionado; }
            set
            {
                if (this.privilegioSeleccionado != null)
                {
                    if (!removiendoPrivilegio)
                        PasarComandosPrivilegios();
                }
                this.privilegioSeleccionado = value;
                OnPropertyChanged("PrivilegioSeleccionado");
                CrearComandosPrivilegios();
            }
        }

        private readonly List<PrivilegioDto> lPrivilegiosEliminados = new List<PrivilegioDto>();

        private ObservableCollection<ComandosPrivilegioAuxiliar> lComandos = new ObservableCollection<ComandosPrivilegioAuxiliar>();        
        public ObservableCollection<ComandosPrivilegioAuxiliar> LComandos
        {
            get { return this.lComandos; }
            set
            {
                this.lComandos = value;
                OnPropertyChanged("LComandos");
            }
        }

        private readonly UsuarioDto mUsuario;

        public ICommand CmdGuardar
        {
            get;
            internal set;
        }

        public ICommand CmdAgregar
        {
            get;
            internal set;
        }

        public ICommand CmdRemover
        {
            get;
            internal set;
        }

        public UsuarioPrivilegiosVM(UsuarioDto pusr) : this(DepositosControl.Instance.SeguridadDepositoCrear(), pusr) { }
        public UsuarioPrivilegiosVM(SeguridadDep dep, UsuarioDto pusr) : this(new UsuarioPrivilegios(), dep, pusr) { }
        public UsuarioPrivilegiosVM(IVentanaDialogo vista, SeguridadDep dep, UsuarioDto pusr) : base(vista)
        {
            if (dep != null)
                seguridadDep = dep;
            else
                seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            mUsuario = pusr;
            LPrivilegios = new ObservableCollection<PrivilegioDto>(seguridadDep.PrivilegiosPorUsuario(pusr.Id));
            LFunciones = new ObservableCollection<FuncionDto>(seguridadDep.FuncionesPorEstado(0));
            CrearComandos();
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            Vista.Owner = App.Current.MainWindow;
            Vista.ShowDialog();
        }

        private void CrearComandos()
        {
            this.CmdAgregar = new ComandoDelegado((o) => this.AccionAgregar(), (o) => this.HabilitaAgregar());
            this.CmdRemover = new ComandoDelegado((o) => this.AccionRemover(), (o) => this.HabilitaRemover());
            this.CmdGuardar = new ComandoDelegado((o) => this.AccionGuardar(), (o) => this.HabilitaGuardar());
        }

        private bool FuncionInsertada()
        {
            bool res = false;
            if (LPrivilegios.Count > 0)
            {
                foreach(PrivilegioDto p in LPrivilegios)
                {
                    if (p.Funcion == FuncionSeleccionada.Id)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        private void CrearComandosPrivilegios()
        {
            if (PrivilegioSeleccionado != null)
            {
                IEnumerable<ComandoDto> cmds = PrivilegioSeleccionado.FuncionNav.ComandosNav;
                this.LComandos.Clear();
                foreach (ComandoDto c in cmds)
                {
                    ComandosPrivilegioAuxiliar cpa = new ComandosPrivilegioAuxiliar();
                    cpa.Etiqueta = c.Denominacion.Trim();
                    cpa.Indice = (int)c.Indice;
                    cpa.Seleccionado = PrivilegioSeleccionado.Comandos.Contains(c.Indice.ToString());
                    this.LComandos.Add(cpa);
                }
            }
        }

        private void PasarComandosPrivilegios()
        {
            string cmd = "";
            foreach (ComandosPrivilegioAuxiliar c in LComandos)
            {
                if (c.Seleccionado)
                {
                    cmd = cmd + c.Indice.ToString() + "-";
                }
            }
            PrivilegioSeleccionado.Comandos = cmd;
        }
    
        private bool HabilitaAgregar()
        {
            return this.FuncionSeleccionada != null && !FuncionInsertada();
        }
        private bool HabilitaRemover()
        {
            return this.PrivilegioSeleccionado != null;
        }
        private bool HabilitaGuardar()
        {
            return this.LPrivilegios.Count > 0;
        }
        
        private void AccionAgregar()
        {
            PrivilegioDto p = new PrivilegioDto();
            p.Id = 0;
            p.FuncionNav = FuncionSeleccionada;
            p.Funcion = p.FuncionNav.Id;
            p.Estado = 0;
            p.Comandos = "";
            if (mUsuario != null)
            {
                p.UsuarioNav = mUsuario;
                p.Usuario = mUsuario.Id;
            }
            LPrivilegios.Add(p);
        }

        private void AccionRemover()
        {
            if (PrivilegioSeleccionado.Id > 0)
            {
                lPrivilegiosEliminados.Add(PrivilegioSeleccionado);                
            }
            removiendoPrivilegio = true;
            LPrivilegios.Remove(PrivilegioSeleccionado);
            removiendoPrivilegio = false;
        }

        private void AccionGuardar()
        {
            if (PrivilegioSeleccionado != null)
            {
                PasarComandosPrivilegios();
            }
            seguridadDep.PrivilegiosEliminar(lPrivilegiosEliminados);
            foreach (PrivilegioDto p in LPrivilegios)
            {
                if (p.Id > 0)
                {
                    seguridadDep.PrivilegioModificar(p);
                }
                else
                {
                    seguridadDep.PrivilegioCrear(p);
                }
            }
            CuadroMensajes.Aceptar("Guardar", "Operacion exitosa", "Los cambios se han guardado satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            Vista.DialogResult = true;
            Vista.Close();
        }
    }
}
