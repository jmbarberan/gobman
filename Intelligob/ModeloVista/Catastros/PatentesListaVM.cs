using System;
using System.Linq;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.ComponentModel;
using Intelligob.Escritorio.Vistas.Catastros;
using System.Collections.ObjectModel;
using Intelligob.Cliente.Referencia;
using System.Windows.Input;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente;
using Intelligob.Utiles;
using System.Collections.Generic;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class PatentesListaVM : BaseMV<IPagina>, IDataErrorInfo
    {
        private readonly SeguridadDep seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
        private readonly CatastrosDep catastrosDep = DepositosControl.Instance.CatastrosDepositoCrear();
        private readonly string codigoSeparador;

        private ObservableCollection<PatenteDto> lpatentes = new ObservableCollection<PatenteDto>();
        public ObservableCollection<PatenteDto> LPatentes
        {
            get { return lpatentes; }
            set { this.lpatentes = value; OnPropertyChanged("LPatentes"); }
        }

        private PatenteDto seleccionado;
        public PatenteDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        #region Atributos de busqueda

        private bool buscarPorContribuyente = false;
        public bool BuscarPorContribuyente
        {
            get { return this.buscarPorContribuyente; }
            set { this.buscarPorContribuyente = value; OnPropertyChanged("BuscarPorContribuyente"); OnPropertyChanged("TextoBusqueda"); }
        }

        private string textoBusqueda = String.Empty;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        public List<KeyValuePair<string, TipoBusquedaTexto>> ListaTipoBusqueda
        {
            get { return EnumTipoBusquedaTexto.TraerLista(); }
        }

        private TipoBusquedaTexto tipoBusqueda = TipoBusquedaTexto.tbComenzando;
        public TipoBusquedaTexto TipoBusqueda
        {
            get { return tipoBusqueda; }
            set
            {
                tipoBusqueda = value;
                OnPropertyChanged("TipoBusqueda");
            }
        }

        private int filtro = 0;
        public int Filtro
        {
            get { return this.filtro; }
            set { this.filtro = value; OnPropertyChanged("Filtro"); }
        }

        private ContribuyenteDto contribuyenteBusqueda = null;

        private String barraEstado = "Listo";
        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        #endregion

        #region declaracion de comandos y control de privilegios

        private bool nuevoHabilitado;
        private bool modificarHabilitado;
        private bool eliminarHabilitado;
        private bool restaurarHabilitado;

        public ICommand CmdBuscar
        {
            get;
            internal set;
        }

        public ICommand CmdNuevo
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

        public ICommand CmdContribuyente
        {
            get;
            internal set;
        }

        public ICommand CmdVaciarTexto
        {
            get;
            internal set;
        }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        #endregion

        public PatentesListaVM() : this(new PatentesLista()) { }
        public PatentesListaVM(PatentesLista vista) : base(vista) 
        {
            this.ProcesarPrivilegios();
            IniciarComandos();
            using (TablasDep dep = DepositosControl.Instance.TablasDepositoCrear())
            {
                codigoSeparador = dep.CodigoSeparador;
            }
        }

        private void IniciarComandos()
        {
            this.CmdBuscar = new ComandoDelegado((o) => AccionBuscar());
            this.CmdNuevo = new ComandoDelegado((o) => AccionNuevo(), (o) => HabilitaNuevo());
            this.CmdModificar = new ComandoDelegado((o) => AccionModificar(), (o) => HabilitaModificar());
            this.CmdEliminar = new ComandoDelegado((o) => AccionEliminar(), (o) => HabilitaEliminar());
            this.CmdRestaurar = new ComandoDelegado((o) => AccionRestaurar(), (o) => HabilitaRestaurar());
            this.CmdContribuyente = new ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContribuyente());
            this.CmdVaciarTexto = new ComandoDelegado((o) => AccionVaciarTexto(), (o) => HabilitaVaciarTexto());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            this.CmdRegresar = new ComandoDelegado((o) => this.AccionRegresar(), (o) => this.PuedeRegresar());
        }

        private void ProcesarPrivilegios()
        {
            this.nuevoHabilitado = false;
            this.modificarHabilitado = false;
            this.eliminarHabilitado = false;
            this.restaurarHabilitado = false;
            
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(19, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.nuevoHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.modificarHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.eliminarHabilitado = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.restaurarHabilitado = true;
        }

        #region Habilitadores de comandos
        private bool HabilitaNuevo()
        {
            return this.nuevoHabilitado;
        }

        private bool HabilitaModificar()
        {
            return this.modificarHabilitado && Seleccionado != null && Seleccionado.Estado == 0;
        }

        private bool HabilitaEliminar()
        {
            return this.eliminarHabilitado && Seleccionado != null && Seleccionado.Estado == 0;
        }

        private bool HabilitaRestaurar()
        {
            return this.restaurarHabilitado && Seleccionado != null && Seleccionado.Estado == 2;
        }

        private bool HabilitaContribuyente()
        {
            return this.BuscarPorContribuyente;
        }

        private bool HabilitaVaciarTexto()
        {
            return !this.BuscarPorContribuyente;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private bool PuedeRegresar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        #endregion

        #region Acciones de comandos

        private void AccionBuscar()
        {
            int mEstado = Filtro;
            if (Filtro == 3)
            {
                mEstado = 9;
            }
            if (BuscarPorContribuyente)
            {
                if (contribuyenteBusqueda != null && contribuyenteBusqueda.Id > 0)
                {
                    LPatentes = new ObservableCollection<PatenteDto>(catastrosDep.PatentePorContribuyente(contribuyenteBusqueda.Id, mEstado));
                }
            }
            else
            {
                string s = this.TextoBusqueda;
                if (this.TipoBusqueda == TipoBusquedaTexto.tbConteniendo)
                {
                    s = s.Replace(this.codigoSeparador + "0" + this.codigoSeparador, this.codigoSeparador + "%" + this.codigoSeparador);
                }
                LPatentes = new ObservableCollection<PatenteDto>(catastrosDep.PatentePorCodigo(s, mEstado, this.TipoBusqueda));
            }
        }

        private void AccionNuevo()
        {
            PatenteEditorVM pe = new PatenteEditorVM();
            if (pe.Vista.DialogResult == true)
            {
                //LPatentes.Add(pe.EPatente);
                this.BarraEstado = string.Format("{0} Fue creado", "[ " + pe.EPatente.Codigo + " ] " + pe.EPatente.ContribuyenteNav.Nombres);
            }
        }

        private void AccionModificar()
        {
            PatenteEditorVM pe = new PatenteEditorVM(Seleccionado);
            if (pe.Vista.DialogResult == true)
            {
                int index = this.LPatentes.IndexOf(this.Seleccionado);
                this.LPatentes.Remove(this.Seleccionado);
                this.LPatentes.Insert(index, pe.EPatente);
                this.BarraEstado = string.Format("{0} Fue modificado", "[ " + pe.EPatente.Codigo + " ] " + pe.EPatente.ContribuyenteNav.Nombres);
            }
        }

        private void AccionEliminar()
        {
            string c = "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres;
            this.Seleccionado.Estado = 2;
            catastrosDep.PatenteModificar(this.Seleccionado);
            this.AccionBuscar();
            this.BarraEstado = string.Format("{0} Fue eliminado", c);
        }

        private void AccionRestaurar()
        {
            string c = "[ " + this.Seleccionado.Codigo + " ] " + this.Seleccionado.ContribuyenteNav.Nombres;
            this.Seleccionado.Estado = 0;
            catastrosDep.PatenteModificar(this.Seleccionado);
            this.AccionBuscar();
            this.BarraEstado = string.Format("{0} Fue restaurado", c);
        }

        private void AccionContribuyente()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.contribuyenteBusqueda = sc.Seleccionado;
                this.TextoBusqueda = sc.Seleccionado.Nombres;
            }
        }

        private void AccionVaciarTexto()
        {
            if (this.BuscarPorContribuyente)
            {
                this.contribuyenteBusqueda = null;
            }
            this.TextoBusqueda = String.Empty;
        }        

        private void AccionRegresar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        #endregion

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                if (columnName == "TextBusqueda")
                { 
                    if (string.IsNullOrWhiteSpace(this.TextoBusqueda))
                        error = "Digite el texto a buscar";
                    else
                    {
                        if (this.BuscarPorContribuyente)
                        {
                            if (contribuyenteBusqueda == null)
                                error = "Seleccione un contribuyente para buscar";
                        }
                    }
                }
                return error;
            }
        }
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
