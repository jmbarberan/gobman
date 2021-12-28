using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Catastros;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.ComponentModel;
using System.Windows.Input;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Utiles;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente;
using Intelligob.Escritorio.Vistas.General;
//using Telerik.Reporting;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista
{
    public class PrediosListaVM : BaseMV<IPagina>, IDataErrorInfo
    {
        private readonly CatastrosDep catastrosDep = DepositosControl.Instance.CatastrosDepositoCrear();
        private readonly SeguridadDep seguridadDep = DepositosControl.Instance.SeguridadDepositoCrear();
        private readonly TablasDep tablasDep = DepositosControl.Instance.TablasDepositoCrear();
        private readonly string codigoSeparador = "";
        private ObservableCollection<PredioBaseDto> lPredios = new ObservableCollection<PredioBaseDto>();
        public ObservableCollection<PredioBaseDto> LPredios
        {
            get { return this.lPredios; }
            set
            {
                this.lPredios = value;
                OnPropertyChanged("LPredios");
            }
        }

        #region Control de busqueda

        private bool consultaOcupada;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        public string BusyContent
        {
            get { return "Consultando registros prediales"; }
        }

        private PredioBaseDto seleccionado;
        public PredioBaseDto Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                this.seleccionado = value;
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
        #endregion

        #region Atributos para busqueda

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

        private int filtroTP = 0;
        public int FiltroTP
        {
            get { return this.filtroTP; }
            set { this.filtroTP = value; OnPropertyChanged("FiltroTP"); }
        }

        private string textoBusqueda = "";
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        private bool buscarPorContribuyente = false;
        public bool BuscarPorContribuyente
        {
            get { return this.buscarPorContribuyente; }
            set 
            { 
                this.buscarPorContribuyente = value;                
                OnPropertyChanged("BuscarPorContribuyente"); 
                OnPropertyChanged("TextoBusqueda"); 
            }
        }

        private ContribuyenteDto contribuyenteBusqueda = null;
        #endregion

        #region Comandos y habilitadores
        private bool nuevo;
        private bool modificar;
        private bool buscar;
        private bool eliminar;
        private bool restaurar;
        private bool calcular;
        private bool conavaluos;
        //private bool certificado;

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

        public ICommand CmdCalcular
        {
            get;
            internal set;
        }

        public ICommand CmdRegresar
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

        public ICommand CmdConAvaluos
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        #endregion
        public PrediosListaVM() : base(new PrediosLista())
        {
            ProcesarPrivilegios();
            this.codigoSeparador = tablasDep.CodigoSeparador;
            #region Crear comandos delegado
            CmdBuscar = new ComandoDelegado((o) => this.AccionBuscar(), (o) => this.HabilitaBuscar());
            CmdNuevo = new ComandoDelegado((o) => AccionNuevo(), (o) => HabilitaNuevo());
            CmdModificar = new ComandoDelegado((o) => AccionModificar(), (o) => HabilitaModificar());
            CmdEliminar = new ComandoDelegado((o) => AccionEliminar(), (o) => HabilitaEliminar());
            CmdRestaurar = new ComandoDelegado((o) => AccionRestaurar(), (o) => HabilitaRestaurar());
            CmdCalcular = new ComandoDelegado((o) => AccionCalcular(), (o) => HabilitaCalcular());
            CmdContribuyente = new ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContribuyente());
            CmdVaciarTexto = new ComandoDelegado((o) => AccionVaciarTexto(), (o) => HabilitaVaciarTexto());
            CmdRegresar = new ComandoDelegado((o) => AccionRegresar(), (o) => HabilitaRegresar());
            CmdConAvaluos = new ComandoDelegado((o) => AccionConAvaluos(), (o) => HabilitaConAvaluos());
            //CmdCertificado = new ComandoDelegado((o) => AccionCertificado(), (o) => HabilitaCertficado());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            #endregion
        }

        private void ProcesarPrivilegios()
        {            
            this.nuevo = false;
            this.modificar = false;
            this.eliminar = false;
            this.restaurar = false;
            this.calcular = false;
            this.conavaluos = false;
            
            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(13, SesionUtiles.Instance.UsuarioActivo.Id);
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
                this.calcular = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("6"))
                this.conavaluos = true;
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            this.LPredios = new ObservableCollection<PredioBaseDto>((IEnumerable<PredioBaseDto>)e.Result);
            this.ConsultaOcupada = false;            
            if (LPredios.Count <= 0)
            {
                this.BarraEstado = "No se encontraron registros";
            }
            else
                this.BarraEstado = LPredios.Count.ToString() + " Registros encontrados";

        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        private IEnumerable<PredioBaseDto> ConsultaAsincronica()
        {
            IEnumerable<PredioBaseDto> res = null;
            int mEstado = Filtro;
            if (Filtro == 3)
            {
                mEstado = 9;
            }

            int filtp = FiltroTP - 1;
            if (filtp < 0)
                filtp = 9;

            if (BuscarPorContribuyente)
            {
                if (contribuyenteBusqueda != null && contribuyenteBusqueda.Id > 0)
                {
                    res = catastrosDep.PrediosPorContribuyente(contribuyenteBusqueda.Id, mEstado, filtp);
                }
            }
            else
            {
                string s = this.TextoBusqueda;
                if (this.TipoBusqueda == TipoBusquedaTexto.tbConteniendo)
                {
                    s = s.Replace(this.codigoSeparador + "0" + this.codigoSeparador, this.codigoSeparador + "%" + this.codigoSeparador);
                }
                res = catastrosDep.PrediosPorCodigo(s, mEstado, TipoBusqueda, filtp);
            }
            return res;
        }

        public string this[string pAtributo]
        {
            get
            {
                string error = string.Empty;
                if (pAtributo == "TextoBusqueda")
                {
                    if (BuscarPorContribuyente)
                    {
                        if (this.contribuyenteBusqueda == null)
                        {
                            error = "Debe seleccionar un contribuyente";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(this.textoBusqueda))
                        {
                            error = "Debe digitar el codigo a buscar";
                        }
                    }
                }
                this.buscar = error == string.Empty;
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

        #region Habilitadores de comandos
        private bool HabilitaBuscar()
        {
            return this.buscar && ! ConsultaOcupada;
        }

        private bool HabilitaNuevo()
        {
            return this.nuevo && !ConsultaOcupada;
        }

        private bool HabilitaModificar()
        {
            return this.modificar && this.Seleccionado != null && !ConsultaOcupada;
        }

        private bool HabilitaEliminar()
        {
            return this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado == 0 && !ConsultaOcupada;
        }

        private bool HabilitaRestaurar()
        {
            return this.restaurar && this.Seleccionado != null && this.Seleccionado.Estado == 2 && !ConsultaOcupada;
        }

        private bool HabilitaCalcular()
        {
            return this.calcular && this.Seleccionado != null && !ConsultaOcupada;
        }
        
        private bool HabilitaContribuyente()
        {
            return BuscarPorContribuyente && !ConsultaOcupada;
        }

        private bool HabilitaVaciarTexto()
        {
            return this.TextoBusqueda.Length > 0 && !ConsultaOcupada;
        }

        private bool HabilitaConAvaluos()
        {
            return this.conavaluos && this.Seleccionado != null && !ConsultaOcupada;
        }

        /*
        private bool HabilitaCertficado()
        {
            return this.certificado;
        }
        */

        private bool HabilitaRegresar()
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
            this.ConsultaOcupada = true;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(); 
        }

        private void AccionNuevo()
        {
            PredioEditorVM pre = new PredioEditorVM();
            string codigo = pre.EPredio.Codigo;
            if (pre.Modificado)
            {
                this.BarraEstado = string.Format("{0} Fue creado", codigo);
            }
        }

        private void AccionModificar()
        {
            if (this.Seleccionado != null && this.Seleccionado.Id > 0)
            {
                
                PredioEditorVM pe = new PredioEditorVM(this.Seleccionado);
                if (pe.Modificado)
                {
                    int index = this.LPredios.IndexOf(this.Seleccionado);
                    this.LPredios.Remove(this.Seleccionado);
                    this.LPredios.Insert(index, pe.EPredio);
                    this.BarraEstado = string.Format("{0} Fue modificado", pe.EPredio.Codigo);
                }
            }
        }

        private void AccionEliminar()
        {
            if (this.Seleccionado != null)
            {
                string c = this.Seleccionado.Codigo;
                this.Seleccionado.Estado = 2;
                catastrosDep.PredioModificar(this.Seleccionado);
                this.BarraEstado = string.Format("{0} Fue eliminado", c);
            }
        }

        private void AccionRestaurar()
        {
            if (this.Seleccionado != null)
            {
                this.Seleccionado.Estado = 0;
                catastrosDep.PredioModificar(this.Seleccionado);
                this.AccionBuscar();
                this.BarraEstado = string.Format("{0} Fue restaurado", this.Seleccionado.Codigo);
            }
        }

        private void AccionCalcular()
        {
            PredioCalculoAvaluoVM cal = new PredioCalculoAvaluoVM(this.Seleccionado);
            if (cal.Vista.DialogResult == true)
            {
                this.BarraEstado = String.Format("{0} fue actualizado el valor de la propiedad", this.Seleccionado.Codigo);
                AccionBuscar();
            }
                
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
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }
        
        private void AccionConAvaluos()
        {
            int pcon = (int)Seleccionado.FormatoCodigo + 1;
            AvaluosConsultaVM ca = new AvaluosConsultaVM(pcon, Seleccionado.Codigo);            
        }

        /*
        private void AccionCertificado()
        {
            ContribuyenteDto contrib = null;
            if (this.Seleccionado.PropietariosNav.Length == 1)
                contrib = this.Seleccionado.PropietariosNav[0].ContribuyenteNav;
            else
            {
                if (this.Seleccionado.PropietariosNav.Length > 1)
                {
                    List<ContribuyenteDto> cons = new List<ContribuyenteDto>();
                    foreach (PredioPropietarioDto pro in this.Seleccionado.PropietariosNav)
                    {
                        cons.Add(pro.ContribuyenteNav);
                    }
                    SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM(cons.ToArray());
                    if (sc.Vista.DialogResult == true)
                    {
                        contrib = sc.Seleccionado;
                    }
                }
            }
            RecaudacionesDep rd = new RecaudacionesDep();
            PlanillaDto p = rd.DocumentosPuedeEmitir(10, contrib.Id);
            if (p != null && p.Id > 0)
            {
                ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
                if (iajustes.Vista.ShowDialog() == true)
                {
                    Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                    InstanceReportSource rs = new InstanceReportSource();
                    
                    if (this.Seleccionado.FormatoCodigo == 0)
                    {
                        Intelligob.Reportes.Catastros.CertificadoAvaluos rfu = new Intelligob.Reportes.Catastros.CertificadoAvaluos();
                        rs.ReportDocument = rfu;
                    }
                    else
                    {
                        Intelligob.Reportes.Catastros.CertificadoAvaluosRural rfr = new Intelligob.Reportes.Catastros.CertificadoAvaluosRural();
                        rs.ReportDocument = rfr;
                    }
                    
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pId", this.Seleccionado.Id));
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pPropietario", contrib.Nombres));
                    if (contrib.Cedula.Length > 0)
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pCedula", contrib.Cedula));
                    else
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pCedula", "N/D"));
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pYear", DateTime.Today.Year));
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pInstitucion", tablasDep.NombreEmpresa));
                    string sjefe = "";
                    TablaClaveDto jefe = tablasDep.ClavesPorTablaCve(2,8).FirstOrDefault();
                    if (jefe != null)
                        sjefe = jefe.Denominacion;
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pJefe", sjefe));                    

                    reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                    rd.DocumentoMarcarPorPlanilla(p.Id);                                        
                }
            }
            else
                CuadroMensajes.Alertar("Atencion", "No se puede imprimir certificado", "No se encontro un titulo pagado por este contribuyente", "");
        }
        */

        #endregion
    }
}
