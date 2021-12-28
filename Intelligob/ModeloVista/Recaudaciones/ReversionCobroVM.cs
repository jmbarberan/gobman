using System.Collections.Generic;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;
using Intelligob.Escritorio.Vistas.Recaudaciones;
using System.Windows.Input;
using Intelligob.Cliente.Referencia;
using System.Collections.ObjectModel;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Utiles;
using System.ComponentModel;
using Intelligob.Cliente;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class ReversionCobroVM :BaseMV<IPagina>
    {
        private readonly RecaudacionesDep recaudacionesDep = new RecaudacionesDep();
        private ContribuyenteDto contribuyenteConsultado;

        private ObservableCollection<PlanillaDto> lPlanillas = new ObservableCollection<PlanillaDto>();
        public ObservableCollection<PlanillaDto> LPlanillas
        {
            get { return this.lPlanillas; }
            set { this.lPlanillas = value; OnPropertyChanged("LPlanillas"); }
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

        private PlanillaDto planillaSeleccionada;
        public PlanillaDto PlanillaSeleccionada
        {
            get { return this.planillaSeleccionada; }
            set { this.planillaSeleccionada = value; OnPropertyChanged("PlanillaSeleccionada"); }
        }

        public List<ConceptoDto> LConceptos
        {
            get 
            {
                return Intelligob.Cliente.ModeloCache.Instance.McConceptos.Where(c => c.Estado == 0).ToList(); 
            }
        }

        private bool reportarSoporteAnulado = false;
        public Boolean ReportarSoporteAnulado
        {
            get { return reportarSoporteAnulado; }
            set { reportarSoporteAnulado = value; OnPropertyChanged("ReportarSoporteAnulado"); }
        }

        #region Control de consulta

        private ConceptoDto buscaTituloConcepto;
        public ConceptoDto BuscaTituloConcepto
        {
            get { return this.buscaTituloConcepto; }
            set { this.buscaTituloConcepto = value; OnPropertyChanged("BuscaTituloConcepto"); }
        }

        private string buscaTituloCodigo = String.Empty;
        public String BuscaTituloCodigo
        {
            get { return this.buscaTituloCodigo; }
            set { this.buscaTituloCodigo = value; OnPropertyChanged("BuscaTituloCodigo"); }
        }

        private string buscaTituloEstado;
        public String BuscaTituloEstado
        {
            get { return this.buscaTituloEstado; }
            set { this.buscaTituloEstado = value; OnPropertyChanged("BuscaTituloEstado"); }
        }

        private int buscaTituloAño = 0;
        public int BuscaTituloAño
        {
            get { return this.buscaTituloAño; }
            set { this.buscaTituloAño = value; OnPropertyChanged("BuscaTituloAño"); }
        }

        private bool filtrarFecha = false;
        public bool FiltrarFecha
        {
            get { return this.filtrarFecha; }
            set { this.filtrarFecha = value; OnPropertyChanged("FiltrarFecha"); }
        }

        private DateTime fechaInicio = DateTime.Today;
        public DateTime FechaInicio
        {
            get { return this.fechaInicio; }
            set { this.fechaInicio = value; OnPropertyChanged("FechaInicio"); }
        }

        private DateTime fechaCorte = DateTime.Today;
        public DateTime FechaCorte
        {
            get { return this.fechaCorte; }
            set { this.fechaCorte = value; OnPropertyChanged("FechaCorte"); }
        }

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

        #endregion

        #region Declaracion de comandos
        private bool revertirAutoriza = false;

        public ICommand CmdConsultar
        { get; internal set; }

        public ICommand CmdRevertir
        { get; internal set; }

        public ICommand CmdContribuyente
        { get; internal set; }

        public ICommand CmdLimpiarConsulta
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        #endregion

        public ReversionCobroVM() : base(new ReversionCobro()) 
        { 
            this.CrearComandos();
            this.revertirAutoriza = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                using (SeguridadDep sd = new SeguridadDep())
                {
                    PrivilegioDto p = sd.PrivilegiosFuncionPorUsuario(21, SesionUtiles.Instance.UsuarioActivo.Id);
                    if (p != null && p.Comandos != null)
                        c = p.Comandos;
                }
            }

            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.revertirAutoriza = true;
        }

        private void CrearComandos()
        {
            this.CmdConsultar = new ComandoDelegado((o) => ConsultarAccion(), (o) => ConsultarHabilita());
            this.CmdRevertir = new ComandoDelegado((o) => RevertirAccion(), (o) => RevertirHabilita());
            this.CmdContribuyente = new ComandoDelegado((o) => ContribuyenteAccion(), (o) => ContribuyenteHabilita());
            this.CmdLimpiarConsulta = new ComandoDelegado((o) => LimpiarAccion(), (o) => LimpiarHabilita());
            this.CmdRegresar =  new ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
        }

        private IEnumerable<PlanillaDto> ConsultaAsincronica()
        {
            IEnumerable<PlanillaDto> res = null;

            int con = 0;
            if (contribuyenteConsultado != null && contribuyenteConsultado.Id > 0)
                con = contribuyenteConsultado.Id;

            int cop = 0;
            if (this.BuscaTituloConcepto != null && this.BuscaTituloConcepto.Id > 0)
                cop = this.BuscaTituloConcepto.Id;

            if (this.FiltrarFecha)
                res = recaudacionesDep.PlanillasPagadas(con, this.BuscaTituloAño, this.BuscaTituloCodigo, cop, this.FechaInicio, this.FechaCorte);
            else
                res = recaudacionesDep.PlanillasPagadas(con, this.BuscaTituloAño, this.BuscaTituloCodigo, cop);

            return res;
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            this.LPlanillas = new ObservableCollection<PlanillaDto>((IEnumerable<PlanillaDto>)e.Result);
            this.ConsultaOcupada = false;
            if (LPlanillas.Count <= 0)
            {
                this.BarraEstado = "No se encontraron registros";
            }
            else
            {
                string nom = "";
                if (this.contribuyenteConsultado != null)
                    nom = this.contribuyenteConsultado.Nombres + " - ";
                this.BarraEstado = nom + String.Format("{0} Registros encontrados", LPlanillas.Count);
            }
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        private bool RevertirHabilita()
        {
            return this.PlanillaSeleccionada != null && !ConsultaOcupada && revertirAutoriza;
        }        

        private bool RegresarHabilita()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private bool ConsultarHabilita()
        {
            return this.BuscaTituloConcepto != null && ! ConsultaOcupada && (this.contribuyenteConsultado != null || this.BuscaTituloCodigo.Length > 0);
        }

        private bool ContribuyenteHabilita()
        {
            return !ConsultaOcupada;
        }

        private bool LimpiarHabilita()
        {
            return !ConsultaOcupada;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private void ConsultarAccion()
        {
            this.ConsultaOcupada = true;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(); 
        }

        private void RevertirAccion()
        {
            try
            {
                if (PlanillaSeleccionada.ConceptoNav.PagosParciales == true)
                {
                    Boolean completo = false;
                    IEnumerable<CobroTransaccionDto> cobs = recaudacionesDep.CobrosPorPlanillaEstado(PlanillaSeleccionada.Id, 0);
                    if (cobs.Count() == 1)
                    {
                        General.CapturaTextoVM ct = new General.CapturaTextoVM("Descripcion de esta reversion");
                        ct.Vista.ShowDialog();
                        if (ct.Vista.DialogResult == true)
                        {
                            recaudacionesDep.CobroRevertirPorPlanilla((int)cobs.ElementAt(0).Cobro, PlanillaSeleccionada.Id, ReportarSoporteAnulado, ct.Texto);
                            completo = true;
                        }
                    }
                    else
                    {
                        if (cobs.Count() > 1)
                        {
                            IEnumerable<Intelligob.Cliente.Modelos.CobroPlanillaReversion> cs = recaudacionesDep.CobrosParcialesPorPlanillaEstado(planillaSeleccionada.Id, 0);
                            ReversionCobrosParcialesVM rc = new ReversionCobrosParcialesVM(cs);
                            if (rc.Vista.DialogResult == true)
                            {                                
                                recaudacionesDep.CobroRevertirPorPlanilla(rc.Seleccionado.Cobro.Id, PlanillaSeleccionada.Id, ReportarSoporteAnulado, rc.Descripcion);
                                completo = true;
                            }
                        }
                    }
                    if (completo)
                    {
                        CuadroMensajes.Aceptar("Informacion", "Operacion completa", "La reversion se completo exitosamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                        this.ConsultarAccion();
                    }                        
                }
                else
                {
                    General.CapturaTextoVM ct = new General.CapturaTextoVM("Descripcion de esta reversion");
                    ct.Vista.ShowDialog();
                    if (ct.Vista.DialogResult == true)
                    {
                        recaudacionesDep.PlanillaRevertirCobro(PlanillaSeleccionada.Id, true, ReportarSoporteAnulado, ct.Texto);
                        this.ConsultarAccion();
                        CuadroMensajes.Aceptar("Reversion de cobros", "Operacion completa", "La reversion del cobro se completo satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                    }
                }           
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("No se pudo revertir", "Se ha presentado el siguiente error", ex.Message, "");
            }
        }

        private void ContribuyenteAccion()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                if (sc.Seleccionado != null && sc.Seleccionado.Id > 0)
                {
                    this.contribuyenteConsultado = sc.Seleccionado;
                    this.BarraEstado = sc.Seleccionado.Nombres + " seleccionado para consultar";
                    if (this.BuscaTituloConcepto != null)
                        ConsultarAccion();
                }
            }
        }

        private void LimpiarAccion()
        {
            this.BuscaTituloConcepto = null;
            this.contribuyenteConsultado = null;
            this.BuscaTituloCodigo = String.Empty;
            this.BuscaTituloAño = 0;
            LPlanillas = new ObservableCollection<PlanillaDto>();
        }

        private void RegresarAccion()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

    }
}
