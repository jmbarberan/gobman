using System;
using System.Linq;
using Intelligob.Cliente.Referencia;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Utiles;
using System.ComponentModel;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class BajasVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.RecaudacionesDep recaudacionesDep = new Cliente.Depositos.RecaudacionesDep();
        private readonly Cliente.Depositos.TablasDep tablasDep = new Cliente.Depositos.TablasDep();

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

        private bool consultaOcupada;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        public string BusyContent
        {
            get { return "Consultando registros"; }
        }

        public String Nombres
        {
            get 
            {
                String res = "";
                if (this.contribuyenteConsultado != null && this.contribuyenteConsultado.Nombres != null)
                    res = this.contribuyenteConsultado.Nombres;
                return res;
            }
        }

        public ICommand CmdConsultar
        { get; internal set; }

        public ICommand CmdBajar
        { get; internal set; }

        public ICommand CmdContribuyente
        { get; internal set; }

        public ICommand CmdLimpiarConsulta
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        public BajasVM() : this(new Intelligob.Escritorio.Vistas.Emisiones.Bajas()) { }

        public BajasVM(Intelligob.Escritorio.Vistas.Interfaces.IPagina pVista) : base(pVista)
        {
            this.CmdConsultar = new ComandoDelegado((o) => ConsultarAccion(), (o) => ConsultarHabilita());
            this.CmdBajar = new ComandoDelegado((o) => BajarAccion(), (o) => BajarHabilita());
            this.CmdContribuyente = new ComandoDelegado((o) => ContribuyenteAccion());
            this.CmdLimpiarConsulta = new ComandoDelegado((o) => LimpiarAccion());
            this.CmdRegresar = new ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            this.LPlanillas = new ObservableCollection<PlanillaDto>((IEnumerable<PlanillaDto>)e.Result);
            this.ConsultaOcupada = false;
            string nom = "";
            if (this.contribuyenteConsultado != null)
                nom = this.contribuyenteConsultado.Nombres + " - ";
            this.BarraEstado = nom + String.Format("{0} Titulos encontrados", LPlanillas.Count);

        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
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

            res = new ObservableCollection<PlanillaDto>(recaudacionesDep.TraerDeudaParaBaja(con, this.BuscaTituloAño, this.BuscaTituloCodigo, cop));

            return res;
        }

        private bool BajarHabilita()
        {
            return this.PlanillaSeleccionada != null;
        }

        private bool RegresarHabilita()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private bool ConsultarHabilita()
        {
            return this.BuscaTituloConcepto != null && (this.contribuyenteConsultado != null || this.BuscaTituloCodigo.Length > 0);
        }

        private bool PuedeAdelantar()
        {
            return Navegador.NavigationService.CanGoForward;
        }

        private void ConsultarAccion()
        {
            this.ConsultaOcupada = true;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BajarAccion()
        {
            try
            {
                if (PlanillaSeleccionada.ConceptoNav.PagosParciales == true)
                {
                    /*if (PlanillaSeleccionada.Año >= DateTime.Today.Year)
                        // Validar si el catastro esta suspendido
                        CuadroMensajes.Alertar("No se puede ejecutar la baja", "El año del titulo no aplica", "No se puede dar de baja porque este titulo pertence al año en curso, puede aplicar notas de credito", "");
                    else
                    {*/
                    string combaja = "Baja sin descripcion";
                    General.CapturaTextoVM comDlg = new General.CapturaTextoVM("Descripcion de la baja");
                    comDlg.Vista.ShowDialog();
                    if (comDlg.Vista.DialogResult == true)
                    {
                        combaja = comDlg.Texto.Trim();
                    }

                    PlanillaSeleccionada.Estado = 2;
                    PlanillaSeleccionada.FechaCancelacion = DateTime.Now;
                    PlanillaSeleccionada.Comentarios = combaja;
                    recaudacionesDep.PlanillaActualizar(PlanillaSeleccionada);

                    #region Imprimir titulo parcial                        
                    /*var dialog = new System.Windows.Controls.PrintDialog();
                    dialog.PrintQueue = System.Printing.LocalPrintServer.GetDefaultPrintQueue();
                    dialog.PrintTicket = dialog.PrintQueue.DefaultPrintTicket;
                    dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                    if (dialog.ShowDialog() == true)
                    {
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        Telerik.Reporting.InstanceReportSource rs = new Telerik.Reporting.InstanceReportSource();
                        Intelligob.Reportes.Recaudaciones.PlanillaParcial pf = new Reportes.Recaudaciones.PlanillaParcial();
                        rs.ReportDocument = pf;
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", PlanillaSeleccionada.Id));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaPago", recaudacionesDep.HoyServidor()));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pEsBaja", true));

                        var deviceInfo = new System.Collections.Hashtable();
                        var result = reportProcessor.RenderReport("XPS", rs, deviceInfo);
                        var tempName = System.IO.Path.GetTempFileName();
                        using (var fileStream = new System.IO.FileStream(tempName, System.IO.FileMode.Create))
                        {
                            fileStream.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                        }

                        using (var document = new System.Windows.Xps.Packaging.XpsDocument(tempName, System.IO.FileAccess.Read))
                        {
                            var fixedDocSeq = document.GetFixedDocumentSequence();
                            dialog.PrintDocument(fixedDocSeq.DocumentPaginator, result.DocumentName + "");
                        }

                        try
                        {
                            System.IO.File.Delete(tempName);
                        }
                        catch { }
                    }*/
                    ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
                    if (iajustes.Vista.ShowDialog() == true)
                    {
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        Telerik.Reporting.InstanceReportSource rs = new Telerik.Reporting.InstanceReportSource();
                        Intelligob.Reportes.Recaudaciones.PlanillaParcial pf = new Reportes.Recaudaciones.PlanillaParcial();
                        rs.ReportDocument = pf;
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", PlanillaSeleccionada.Id));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaPago", recaudacionesDep.HoyServidor()));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pEsBaja", true));
                        reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                    }
                    #endregion

                    CuadroMensajes.Aceptar("Operacion completa", "La baja del titulo se completo con exito", "Se ha enviado el titulo a la impresora", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                    this.ConsultarAccion();
                    //}
                }
                else
                {
                    // Baja titulo pago total
                    string combaja = "Baja sin descripcion";
                    General.CapturaTextoVM comDlg = new General.CapturaTextoVM("Descripcion de la baja");
                    comDlg.Vista.ShowDialog();
                    if (comDlg.Vista.DialogResult == true)
                    {
                        combaja = comDlg.Texto.Trim();
                    }

                    PlanillaSeleccionada.Estado = 2;
                    PlanillaSeleccionada.FechaCancelacion = DateTime.Now;
                    PlanillaSeleccionada.Comentarios = combaja;
                    recaudacionesDep.PlanillaActualizar(PlanillaSeleccionada);                    
                    
                    #region Imprimir titulo
                    /*var dialog = new System.Windows.Controls.PrintDialog();
                    dialog.PrintQueue = System.Printing.LocalPrintServer.GetDefaultPrintQueue();
                    dialog.PrintTicket = dialog.PrintQueue.DefaultPrintTicket;
                    dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                    if (dialog.ShowDialog() == true)
                    {
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        Telerik.Reporting.InstanceReportSource rs = new Telerik.Reporting.InstanceReportSource();
                        Intelligob.Reportes.Recaudaciones.Planilla pf = new Reportes.Recaudaciones.Planilla();
                        rs.ReportDocument = pf;
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", PlanillaSeleccionada.Id));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));

                        var deviceInfo = new System.Collections.Hashtable();
                        var result = reportProcessor.RenderReport("XPS", rs, deviceInfo);
                        var tempName = System.IO.Path.GetTempFileName();
                        using (var fileStream = new System.IO.FileStream(tempName, System.IO.FileMode.Create))
                        {
                            fileStream.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                        }

                        using (var document = new System.Windows.Xps.Packaging.XpsDocument(tempName, System.IO.FileAccess.Read))
                        {
                            var fixedDocSeq = document.GetFixedDocumentSequence();
                            dialog.PrintDocument(fixedDocSeq.DocumentPaginator, result.DocumentName + "");
                        }
                        try
                        {
                            System.IO.File.Delete(tempName);
                        }
                        catch { }
                    }*/

                    ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
                    if (iajustes.Vista.ShowDialog() == true)
                    {
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        Telerik.Reporting.InstanceReportSource rs = new Telerik.Reporting.InstanceReportSource();
                        Reportes.Recaudaciones.Planilla pf = new Reportes.Recaudaciones.Planilla();
                        rs.ReportDocument = pf;
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", PlanillaSeleccionada.Id));
                        rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));
                        reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                    }
                    #endregion

                    CuadroMensajes.Aceptar("Operacion completa", "La baja del titulo se completo con exito", "Se ha enviado el titulo a la impresora", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                    this.ConsultarAccion();
                }
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("No se pudo completar la baja", "Se ha presentado el siguiente error", ex.Message, "");
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
                    OnPropertyChanged("Nombres");
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
