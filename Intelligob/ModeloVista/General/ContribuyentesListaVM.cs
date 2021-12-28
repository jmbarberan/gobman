using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Intelligob.Cliente;
using Intelligob.Escritorio.ModeloVista.General;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ContribuyentesListaVM : BaseMV<IPagina>, IDataErrorInfo
    {
        #region Comandos
        
        private bool nuevo;
        private bool modificar;
        private bool eliminar;
        private bool restaurar;
        private bool rebajas;

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

        public ICommand CmdRebajas
        {
            get;
            internal set;
        }

        public ICommand CmdRegresar
        {
            get;
            internal set;
        }

        public ICommand CmdUnificar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        #endregion

        private readonly ContribuyentesDep contribuyenteDep;
        private readonly SeguridadDep seguridadDep;

        private Boolean mostrarModal = false;
        public Boolean MostrarModal
        {
            get { return mostrarModal; }
            set { mostrarModal = value; OnPropertyChanged("MostrarModal"); }
        }

        private object vistaModal;
        public object VistaModal
        {
            get { return vistaModal; }
            set { vistaModal = value; OnPropertyChanged("VistaModal"); }
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

        private string filtroBusqueda = "";
        public string FiltroBusqueda
        {
            get { return filtroBusqueda; }
            set
            {
                filtroBusqueda = value;
                OnPropertyChanged("FiltroBusqueda");
            }
        }

        private bool mostrarEliminados;
        public bool MostrarEliminados
        {
            get { return mostrarEliminados; }
            set 
            {
                mostrarEliminados = value;
                OnPropertyChanged("MostrarEliminados");
            }
        }

        private bool puedeBuscar;
        public bool PuedeBuscar
        {
            get { return this.puedeBuscar; }
            set
            {
                this.puedeBuscar = value;
                OnPropertyChanged("PuedeBuscar");
            }
        }

        private bool buscarPorCedula;
        public bool BuscarPorCedula
        {
            get { return this.buscarPorCedula; }
            set 
            { 
                this.buscarPorCedula = value; 
                OnPropertyChanged("BuscarPorCedula"); 
                OnPropertyChanged("FiltroBusqueda"); 
            }
        }

        private ObservableCollection<ContribuyenteDto> lContribuyentes = new ObservableCollection<ContribuyenteDto>();
        public ObservableCollection<ContribuyenteDto> LContribuyentes
        {
            get { return lContribuyentes; }
            private set 
            {
                lContribuyentes = value;
                OnPropertyChanged("LContribuyentes");
            }
        }

        private ContribuyenteDto seleccionado;
        public ContribuyenteDto Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                seleccionado = value;
                if (this.seleccionado != null)
                {
                    this.BarraEstado = string.Format("{0} Seleccionado", this.Seleccionado.Nombres);
                }
                OnPropertyChanged("Seleccionado");
            }

        }

        private string barraEstado = "Listo";
        public string BarraEstado
        {
            get { return barraEstado; }
            set 
            {
                barraEstado = value;
                OnPropertyChanged("BarraEstado");
            }
        }

        private bool consultaOcupada = false;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        public string BusyContent
        {
            get { return "Consultando contribuyentes"; }
        }

        public ContribuyentesListaVM() : base(new ContribuyentesLista())
        { 
            this.contribuyenteDep = DepositosControl.Instance.ContribuyentesDepositoCrear();
            
            seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
            ProcesarPrivilegios();
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => AccionBuscar());
            this.CmdNuevo = new ComandoDelegado((o) => AccionNuevo(), (o) => HabilitaNuevo());
            this.CmdModificar = new ComandoDelegado((o) => AccionModificar(), (o) => HabilitaModificar());
            this.CmdEliminar = new ComandoDelegado((o) => AccionEliminar(), (o) => HabilitaEliminar());
            this.CmdRestaurar = new ComandoDelegado((o) => AccionRestaurar(), (o) => HabilitaRestaurar());
            this.CmdRebajas = new ComandoDelegado((o) => AccionRebajas(), (o) => HabilitaRebajas());
            this.CmdUnificar = new ComandoDelegado((o) => AccionUnificar(), (o) => HabilitaUnificar());
            this.CmdRegresar = new ComandoDelegado((o) => AccionRegresar(), (o) => HabilitaRegresar());
            this.CmdAdelante = new ComandoDelegado((o) => AccionAdelantar(), (o) => PuedeAdelantar());
            this.BarraEstado = "Listo para ejecutar busquedas";
        }        

        private void ProcesarPrivilegios()
        {
            this.nuevo = false;
            this.modificar = false;
            this.eliminar = false;
            this.restaurar = false;
            this.rebajas = false;
            
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(7, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.nuevo = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.modificar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.eliminar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.restaurar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("5"))
                this.rebajas = true;
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            this.LContribuyentes = new ObservableCollection<ContribuyenteDto>((IEnumerable<ContribuyenteDto>)e.Result);
            this.ConsultaOcupada = false;            
            if (LContribuyentes.Count <= 0)
            {
                this.BarraEstado = "No se encontraron registros";                
            }
            else
                this.BarraEstado = LContribuyentes.Count.ToString() + " Registros encontrados";

        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        #region Habilitadores de comandos

        private bool HabilitaNuevo()
        { return this.nuevo && !ConsultaOcupada && !MostrarModal; }

        private bool HabilitaModificar()
        { return this.modificar && this.Seleccionado != null && this.Seleccionado.Estado == 0 && !ConsultaOcupada; }

        private bool HabilitaEliminar()
        { return this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado == 0 && !ConsultaOcupada; }

        private bool HabilitaRestaurar()
        { return this.restaurar && this.Seleccionado != null && this.Seleccionado.Estado == 2 && !ConsultaOcupada; }

        private bool HabilitaRebajas()
        { return this.rebajas && this.Seleccionado != null && this.Seleccionado.Estado == 0 && !ConsultaOcupada; }

        private bool HabilitaUnificar()
        { return this.modificar && this.Seleccionado != null && this.Seleccionado.Estado == 0 && !ConsultaOcupada; }

        private bool HabilitaRegresar()
        { return Navegador.NavigationService.CanGoBack; }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        #endregion

        #region Acciones de comandos

        private IEnumerable<ContribuyenteDto> ConsultaAsincronica()
        {
            IEnumerable<ContribuyenteDto> res = null;
            if (BuscarPorCedula)
            {
                res = new ObservableCollection<ContribuyenteDto>(contribuyenteDep.ContribuyentesPorCedula(FiltroBusqueda));
            }
            else
            {
                int mEstado = 0;
                if (MostrarEliminados)
                    mEstado = 9;
                res = new ObservableCollection<ContribuyenteDto>(contribuyenteDep.ContribuyentesPorNombre(FiltroBusqueda, TipoBusqueda, mEstado));
            }
            return res;
        }

        private void AccionBuscar()
        {
            if (string.IsNullOrWhiteSpace(this.FiltroBusqueda))
            {
                this.BarraEstado = "Debe digitar un texto a buscar";
            }
            else
            {
                this.ConsultaOcupada = true;
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
                backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();  
            }
        }

        private void AccionNuevo()
        {
            Escritorio.Vistas.General.ContribuyenteEditorModal ce = new Escritorio.Vistas.General.ContribuyenteEditorModal();
            VistaModal = ce;
            ContribuyenteEditorVM cevm = new ContribuyenteEditorVM(ce, new ContribuyenteDto(), contribuyenteDep, AccionOcultar);
            MostrarModal = true;
        }

        private void AccionModificar()
        {
            if (this.Seleccionado != null && this.Seleccionado.Id > 0)
            {
                Escritorio.Vistas.General.ContribuyenteEditorModal ce = new Escritorio.Vistas.General.ContribuyenteEditorModal();
                VistaModal = ce;
                ContribuyenteEditorVM cevm = new ContribuyenteEditorVM(ce, Seleccionado, contribuyenteDep, AccionOcultar);
                MostrarModal = true;
            }
        }

        private void AccionEliminar()
        {
            if (this.Seleccionado != null)
            {
                string c = this.Seleccionado.Nombres;
                this.Seleccionado.Estado = 2;
                contribuyenteDep.ContribuyenteModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue eliminado", c);
            }
            else
            {
                this.BarraEstado = "Seleccione un elemento de la lista";
            }
        }

        private void AccionRestaurar()
        {
            if (this.Seleccionado != null)
            {
                string c = this.Seleccionado.Nombres;
                this.Seleccionado.Estado = 0;
                contribuyenteDep.ContribuyenteModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue restaurado", c);
            }
            else
            {
                this.BarraEstado = "Seleccione un elemento de la lista";
            }
        }

        private void AccionRebajas()
        {
            Escritorio.Vistas.General.ContribuyenteRebajas cr = new Escritorio.Vistas.General.ContribuyenteRebajas();
            VistaModal = cr;
            ContribuyenteRebajasVM crvm = new ContribuyenteRebajasVM(cr, this.Seleccionado.Id, AccionOcultar);
            MostrarModal = true;
        }

        private void AccionUnificar()
        {
            Escritorio.Vistas.General.ContribuyentesUnificar cu = new Escritorio.Vistas.General.ContribuyentesUnificar();
            VistaModal = cu;
            ContribuyentesUnificarVM uni = new ContribuyentesUnificarVM(Seleccionado, cu, AccionOcultar);
            MostrarModal = true;
        }

        private void AccionRegresar()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        public void AccionOcultar()
        {
            MostrarModal = false;
        }

        #endregion

        public string Error
        {
            get { throw new System.NotImplementedException(); }
        }

        public string this[string pAtributo]
        {
            get
            {
                string error = string.Empty;
                if (pAtributo == "FiltroBusqueda")
                {
                    if (string.IsNullOrWhiteSpace(this.FiltroBusqueda))
                    {
                        if (this.BuscarPorCedula)
                            error = "Digite el No. de cedula a buscar (sin guiones)";
                        else
                            error = "Digite al menos una aproximacion del nombre a buscar";
                    }
                    else
                    {
                        if (this.BuscarPorCedula)
                        {
                            try
                            {
                                int c = Convert.ToInt32(this.FiltroBusqueda);
                            }
                            catch
                            {
                                error = "El No. de cedula contiene caracteres invalidos (ejemplo: -, @, #, A-Z)";
                            }
                        }
                    }
                }
                PuedeBuscar = error == string.Empty;
                return error;
            }
        }
    }
}
