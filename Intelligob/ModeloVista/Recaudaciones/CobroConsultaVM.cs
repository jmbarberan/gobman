using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Recaudaciones;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Cliente;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Intelligob.Cliente.Referencia;
using System.Collections.Generic;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Utiles;
using Telerik.Reporting;
using System.Windows.Xps.Packaging;
using System.Windows.Controls;
using System.Printing;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{    
    public class CobroConsultaVM : BaseMV<IPagina>, IDataErrorInfo
    {
        private readonly SeguridadDep seguridadDep = new SeguridadDep();
        private readonly RecaudacionesDep recaudacionesDep = new RecaudacionesDep();
        private readonly EmisionesDep emisionesDep = new EmisionesDep();
        private readonly TablasDep tablasDep = new TablasDep();
        private readonly CatastrosDep catastrosDep = new CatastrosDep();
        private readonly AguaDep aguaDep = new AguaDep();

        private string barraEstado = "Listo";

        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; }
        }

        private bool mostrarAcumuladoresColumna = true;
        public bool MostrarAcumuladoresColumna
        {
            get { return mostrarAcumuladoresColumna; }
            set { this.mostrarAcumuladoresColumna = value; OnPropertyChanged("MostrarAcumuladoresColumna"); }
        }

        private bool mostrarAcumuladores = true;
        public bool MostrarAcumuladores
        {
            get { return mostrarAcumuladores; }
            set { mostrarAcumuladores = value; OnPropertyChanged("MostrarAcumuladores"); }
        }

        private bool puedeCobrar = false;
        private bool puedeImprimir = false;
        private bool puedeActContribuyente = false;
        private int indiceConsultaSeleccion = 1;

        //private bool consultaPorContribuyente = true;
        public bool ConsultaPorContribuyente
        {
            get { return this.indiceConsultaSeleccion == 1; }
            set 
            {
                if (value)
                    this.indiceConsultaSeleccion = 1;
                OnPropertyChanged("ConsultaPorContribuyente");
                OnPropertyChanged("ConsultaPorCodigo");
                OnPropertyChanged("ConsultaNotaCobro");
                this.LimpiarAccion();
            }
        }

        public bool ConsultaPorCodigo
        {
            get { return this.indiceConsultaSeleccion == 2; }
            set 
            {
                if (value)
                    this.indiceConsultaSeleccion = 2;
                OnPropertyChanged("ConsultaPorCodigo");
                OnPropertyChanged("ConsultaPorContribuyente");
                OnPropertyChanged("ConsultaNotaCobro");
                this.LimpiarAccion();
            }
        }
        
        public bool ConsultaPorNotaCobro
        {
            get { return this.indiceConsultaSeleccion == 3; }
            set
            {
                if (value)
                    this.indiceConsultaSeleccion = 3;
                OnPropertyChanged("ConsultaPorCodigo");
                OnPropertyChanged("ConsultaPorContribuyente");
                OnPropertyChanged("ConsultaNotaCobro");
                this.LimpiarAccion();
            }
        }

        public bool AutoDesplegarGrupos
        {
            get { return Configuracion.CobrosAutoDesplegarGrupos; }
            set { Configuracion.CobrosAutoDesplegarGrupos = value; OnPropertyChanged("AutoDesplegarGrupos"); }
        }

        private string textoBusqueda = String.Empty;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        private ContribuyenteDto contribuyenteConsultado;

        private int filtro;
        public int Filtro
        {
            get { return filtro; }
            set { this.filtro = value; OnPropertyChanged("Filtro"); }
        }

        private bool listaOcupada = false;
        public bool ListaOcupada
        {
            get { return this.listaOcupada; }
            set { this.listaOcupada = value; OnPropertyChanged("ListaOcupada"); }
        }

        private bool consultaOcupada = false;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        //private string busyContent;
        public string BusyContent
        {
            get { return "Consultando valores adeudados"; }
            /*set
            {
                if (this.busyContent != value)
                {
                    this.busyContent = value;
                    this.OnPropertyChanged("BusyContent");
                }
            }*/
        }

        private ObservableCollection<PlanillaDto> lplanillas = new ObservableCollection<PlanillaDto>();
        public ObservableCollection<PlanillaDto> LPlanillas
        {
            get { return this.lplanillas; }
            set 
            {
                this.lplanillas = value; 
                OnPropertyChanged("LPlanillas"); 
            }
        }

        private ObservableCollection<object> planillasSeleccionadas;
        public ObservableCollection<object> PlanillasSeleccionadas
        {
            get
            {
                if (planillasSeleccionadas == null)
                {
                    planillasSeleccionadas = new ObservableCollection<object>();
                }
                return planillasSeleccionadas;
            }
        }

        public void SeleccionadosRemover(IEnumerable<object> pRemovidos)
        {
            foreach (object o in pRemovidos)
            {
                if (o != null && o.GetType() == typeof(PlanillaDto))
                {
                    planillasSeleccionadas.Remove((PlanillaDto)o);
                    double tot = 0;
                    if (((PlanillaDto)o).Total != null)
                        tot = (double)((PlanillaDto)o).Total;
                    double reb = 0;
                    if (((PlanillaDto)o).Rebajas != null)
                        reb = (double)((PlanillaDto)o).Rebajas;
                    double rec = 0;
                    if (((PlanillaDto)o).Recargos != null)
                        rec = (double)((PlanillaDto)o).Recargos;
                    double pag = 0;
                    if (((PlanillaDto)o).Pagos != null)
                        pag = (double)((PlanillaDto)o).Pagos;
                    TotalSeleccionado = TotalSeleccionado - (tot + rec - reb - pag);                                        
                }
            }
            if (PlanillasSeleccionadas != null)
                this.BarraEstado = PlanillasSeleccionadas.Count.ToString() + " Registros seleccionados";
        }

        public void SeleccionadosAgregar(IEnumerable<object> pAgregados)
        {
            foreach (object o in pAgregados)
            {
                if (o != null && o.GetType() == typeof(PlanillaDto))
                {
                    planillasSeleccionadas.Add((PlanillaDto)o);
                    double tot = 0;
                    if (((PlanillaDto)o).Total != null)
                        tot = (double)((PlanillaDto)o).Total;
                    double reb = 0;
                    if (((PlanillaDto)o).Rebajas != null)
                        reb = (double)((PlanillaDto)o).Rebajas;
                    double rec = 0;
                    if (((PlanillaDto)o).Recargos != null)
                        rec = (double)((PlanillaDto)o).Recargos;
                    double pag = 0;
                    if (((PlanillaDto)o).Pagos != null)
                        pag = (double)((PlanillaDto)o).Pagos;
                    TotalSeleccionado = TotalSeleccionado + (tot + rec - reb - pag);
                }
            }
            this.BarraEstado = PlanillasSeleccionadas.Count.ToString() + " Registros seleccionados";
        }

        private double totalSeleccionado = 0;
        public double TotalSeleccionado
        {
            get { return this.totalSeleccionado; }
            set 
            {
                this.totalSeleccionado = value; 
                OnPropertyChanged("TotalSeleccionado");
            }
        }

        public ICommand CmdConsultar
        { get; internal set; }

        public ICommand CmdCalcular
        {
            get;
            internal set;
        }

        public ICommand CmdCobrar
        {
            get;
            internal set;
        }

        public ICommand CmdContribuyente
        {
            get; internal set;
        }

        public ICommand CmdImprimir
        { get; internal set; }

        public ICommand CmdSeleTodo
        { get; internal set; }

        public ICommand CmdSeleNada
        { get; internal set; }

        public ICommand CmdSeleInvertir
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        public ICommand CmdLimpiarConsulta
        { get; internal set; }

        public ICommand CmdReliquidar
        { get; internal set; }

        public ICommand CmdActContribuyente { get; internal set; }

        public ICommand CmdNotaCobroCrear { get; internal set; }

        public CobroConsultaVM() : this(new CobroConsulta()) { }

        public CobroConsultaVM(CobroConsulta vista) : base(vista)
        {
            this.ProcesarPrivilegios();
            this.CmdConsultar = new ComandoDelegado((o) => ConsultarAccion(), (o) => ConsultarHabilita());
            this.CmdContribuyente = new ComandoDelegado((o) => ContribuyenteAccion(), (o) => ContribuyenteHabilita());
            this.CmdCalcular = new ComandoDelegado((o) => CalcularAccion(), (o) => CalcularHabilita());
            this.CmdCobrar = new ComandoDelegado((o) => CobrarAccion(), (o) => CobrarHabilita());
            this.CmdImprimir = new ComandoDelegado((o) => ImprimirAccion(), (o) => ImprimirHabilita());
            this.CmdSeleTodo = new ComandoDelegado((o) => SeleTodoAccion(), (o) => SeleTodoHabilita());
            this.CmdSeleNada = new ComandoDelegado((o) => SeleNadaAccion(), (o) => SeleNadaHabilita());
            this.CmdSeleInvertir = new ComandoDelegado((o) => SeleInvertirAccion(), (o) => SeleInvertirHabilita());
            this.CmdRegresar = new ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdAdelante = new ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            this.CmdLimpiarConsulta = new ComandoDelegado((o) => LimpiarAccion(), (o) => LimpiarHabilita());
            this.CmdReliquidar = new ComandoDelegado((o) => ReliquidarAccion(), (o) => ReliquidarHabilita());
            this.CmdActContribuyente = new ComandoDelegado((o) => ActContribuyenteAccion(), (o) => ActContribuyenteHabilita());
        }
        
        private void ProcesarPrivilegios()
        {
            this.puedeCobrar = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(12, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }
                
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.puedeCobrar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.puedeImprimir = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("6"))
                this.puedeActContribuyente = true;
        }        

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            if (e.Result != null)
                this.LPlanillas = new ObservableCollection<PlanillaDto>((IEnumerable<PlanillaDto>)e.Result);
            this.ConsultaOcupada = false;
            //this.BusyContent = String.Empty;
            string tipoConsulta = "contribuyente";
            if (this.ConsultaPorCodigo)
                tipoConsulta = "codigo";
            if (LPlanillas.Count <= 0)
            {
                this.BarraEstado = "No se encontraron registros";
                CuadroMensajes.Aceptar("Informacion", "No presenta deuda", "El " + tipoConsulta + " consultado no presenta documentos pendientes de cobro", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            }                
            else
                this.BarraEstado = LPlanillas.Count.ToString() + " Registros encontrados";
            
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ConsultarDeuda();
        }

        private IEnumerable<PlanillaDto> ConsultarDeuda()
        {
            IEnumerable<PlanillaDto> res = null;
            if (this.ConsultaPorContribuyente)
            {
                try
                {
                    res = new ObservableCollection<PlanillaDto>(recaudacionesDep.TraerDeudaContribuyente(this.contribuyenteConsultado.Id));
                }
                catch (Exception ex)
                {
                    res = null;
                }
            }
            else
            {
                res = new ObservableCollection<PlanillaDto>(recaudacionesDep.TraerDeudaCodigo(TextoBusqueda));
            }
            return res;
        }

        private void ConsultarAccion()
        {
            //this.BusyContent = ;
            this.ConsultaOcupada = true;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();            
        }

        private void CalcularAccion()
        {
            ListaOcupada = true;
            double t = 0;
            foreach(PlanillaDto p in PlanillasSeleccionadas)
            {
                t = t + p.Saldo;
            }
            this.TotalSeleccionado = t;
            ListaOcupada = false;
        }

        private void ImprimirAccion()
        {
            ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
            if (iajustes.Vista.ShowDialog() == true)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                InstanceReportSource rs = new InstanceReportSource();
                if (this.ConsultaPorContribuyente)
                {                    
                    Intelligob.Reportes.Recaudaciones.ConsultaDeuda rf = new Reportes.Recaudaciones.ConsultaDeuda();
                    rs.ReportDocument = rf;
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pContribuyente", this.contribuyenteConsultado.Id));
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));
                }
                else
                {
                    Intelligob.Reportes.Recaudaciones.ConsultaDeudaCodigo rf = new Reportes.Recaudaciones.ConsultaDeudaCodigo();
                    rs.ReportDocument = rf;
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pCodigo", this.TextoBusqueda));
                    rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));
                }
                reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
            }
        }

        private void ReliquidarAccion()
        {
            /*TaskDialogInterop.TaskDialogResult res = CuadroMensajes.Preguntar("Reliquidar titulo(s)", "Cofirme esta operacion", "¿Seguro de ejecutar la reliquidacion de los titulos seleccionados?");
            if (res.CustomButtonResult == 0)
            {
                foreach (PlanillaDto p in PlanillasSeleccionadas)
                {
                    if (p.Concepto == 1)
                    {
                        if (p.Servicio > 0)
                        {
                            recaudacionesDep.EmitirTituloUrbano(p.Servicio, p.Año);
                        }
                    }
                    else
                    {
                        if (p.Concepto == 2)
                        {
                            if (p.Servicio > 0)
                            {
                                recaudacionesDep.EmitirTituloRural(p.Servicio, p.Año);
                            }
                        }
                    }
                }
                ConsultarAccion();
            }*/
        }

        private void CobrarAccion()
        {
            List<PlanillaDto> parciales = new List<PlanillaDto>();
            double montoParciales = 0;
            double montoNormal = 0;

            foreach(PlanillaDto p in PlanillasSeleccionadas)
            {
                if (p.ConceptoNav.PagosParciales == true)
                {
                    parciales.Add(p);
                }
                else
                    montoNormal = montoNormal + (double)(p.Total + p.Recargos - p.Rebajas - p.Pagos);
            }
            Boolean b = false;
            if (parciales.Count > 0)
            {
                CobrosParcialesVM cp = new CobrosParcialesVM(parciales);
                if (cp.Vista.DialogResult == true)
                {
                    parciales = cp.LPlanillas.ToList();
                    b = true;
                    foreach(PlanillaDto ppm in parciales)
                    {
                        montoParciales = montoParciales + ppm.Parcial;
                    }
                }
            }

            List<int> lc = new List<int>();
            if (this.ConsultaPorContribuyente) // un solo contribuyente
            {
                lc.Add(this.contribuyenteConsultado.Id);
            }
            else // Varios contribuyentes
            {
                String cons = String.Empty;
                foreach(PlanillaDto pcons in PlanillasSeleccionadas)
                {
                    String s = pcons.Contribuyentes;
                    if (!cons.Contains(s))
                        cons = cons + s;
                }                
                String scons = cons.Substring(1, cons.Length - 2);
                String[] sacons = scons.Split(']');
                foreach (string c in sacons)
                {
                    if (!String.IsNullOrWhiteSpace(c.Trim()))
                    {
                        String m = c.Trim();
                        if (m.Contains("["))
                            m = m.Substring(1);
                        lc.Add(Convert.ToInt32(m));
                    }
                }
            }

            CobroComprobanteVM cobEle = new CobroComprobanteVM(PlanillasSeleccionadas.Count, montoParciales+montoNormal, lc);
            b = (Boolean)cobEle.Vista.DialogResult;
            
            int contribNC = 0;
            if (b)
            {
                // Seleccionar contribuyente para nota de credito
                if (cobEle.CrearNCreditoMarca && cobEle.Diferencia < 0)
                {
                    if (lc.Count == 1)
                    {
                        if (lc[0] > 0)
                            contribNC = lc[0];
                    }
                    else
                    {
                        // Si hay varios posibles beneficiarios seleccionar uno
                        if (lc.Count > 1)
                        {
                            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM(lc);
                            if (sc.Vista.DialogResult == true)
                            {
                                if (sc.Seleccionado.Id > 0)
                                    contribNC = sc.Seleccionado.Id;
                            }
                        }
                    }                    
                    b = contribNC > 0;
                    if (!b)
                        Utiles.CuadroMensajes.Alertar("Atencion", "No se puede completar el cobro", "No se ha seleccionado el contribuyente para crear la nota de credito", "");
                }
            }            
            
            if (b)
            {
                
                CobroDto cob = new CobroDto();
                cob.Fecha = DateTime.Today;
                cob.Id = 0;
                cob.Estado = 0;
                double tp = 0;
                List<CobroTransaccionDto> ts = new List<CobroTransaccionDto>();
                List<CobrosElementoDto> el = new List<CobrosElementoDto>();

                #region Crear Cobro-Transaccion por titulos con pagos no parciales
                foreach (PlanillaDto p in PlanillasSeleccionadas)
                {
                    if (p.ConceptoNav.PagosParciales == false)
                    {
                        CobroTransaccionDto t = new CobroTransaccionDto();
                        t.Cobro = 0;
                        t.Estado = 0;
                        t.Id = 0;
                        t.Transaccion = p.Id;
                        t.Valor = p.Saldo;
                        t.Recargos = p.Recargos;
                        t.Rebajas = p.Rebajas;                        
                        tp = tp + p.Saldo;
                        ts.Add(t);
                        recaudacionesDep.MarcarPlanilla(p);
                    }
                }
                #endregion

                #region Crear Cobro-Transaccion por titulos con pagos parciales
                List<CobrosRubroDto> cobrubs = new List<CobrosRubroDto>();
                if (parciales.Count > 0)
                {
                    foreach (PlanillaDto pcp in parciales)
                    {
                        bool aplicaAnciano = false;
                        bool aplicaArtesano = false;
                        bool aplicaDiscapacidad = false;
                        double fracAnciano = 0;
                        double fracDiscapacidad = 0;
                        double fracArtesano = 0;

                        #region Obtener Porcentaje descuento Discapacidad -- DESHABILITADO
                        /*double porcDiscapacidad = 0;
                        List<int> pContribuyentes = new List<int>();
                        String scons = pcp.Contribuyentes.Substring(1, pcp.Contribuyentes.Length - 2);
                        String[] sacons = scons.Split(']');
                        foreach (string c in sacons)
                        {
                            if (!String.IsNullOrWhiteSpace(c.Trim()))
                            {
                                String m = c.Trim();
                                if (m.Contains("["))
                                    m = m.Substring(1);
                                pContribuyentes.Add(Convert.ToInt32(m));
                            }
                        }

                        if (pContribuyentes.Count > 0)
                        {
                            foreach (int cont in pContribuyentes)
                            {
                                porcDiscapacidad = recaudacionesDep.RebajaDiscapacidadPorcentaje(cont);
                                break;
                            }
                        }*/
                        #endregion
                        
                        List<int> pContribuyentes = new List<int>();
                        String scons = pcp.Contribuyentes.Substring(1, pcp.Contribuyentes.Length - 2);
                        String[] sacons = scons.Split(']');
                        foreach (string c in sacons)
                        {
                            if (!String.IsNullOrWhiteSpace(c.Trim()))
                            {
                                String m = c.Trim();
                                if (m.Contains("["))
                                    m = m.Substring(1);
                                pContribuyentes.Add(Convert.ToInt32(m));
                            }
                        }

                        #region Obtener procentajes de descuento
                        if (pContribuyentes.Count > 0)
                        {
                            foreach (int cont in pContribuyentes)
                            {
                                fracAnciano = recaudacionesDep.RebajaRubroPorcentajeContribuyente(1, 0, cont);
                                fracArtesano = recaudacionesDep.RebajaRubroPorcentajeContribuyente(3, 0, cont);
                                fracDiscapacidad = recaudacionesDep.RebajaRubroPorcentajeContribuyente(2, 0, cont);
                                ContribuyentesDep cd = new ContribuyentesDep();
                                aplicaAnciano = cd.RebajaContribuyenteBeneficio(cont, 1);
                                aplicaArtesano = cd.RebajaContribuyenteBeneficio(cont, 3);
                                aplicaDiscapacidad = cd.RebajaContribuyenteBeneficio(cont, 2);
                            }
                        }
                        #endregion

                        Double sumaSaldo = 0;
                        foreach (PlanillaRubroDto pcpr in pcp.RubrosNav)
                        {
                            if (pcpr.Origen == 1)
                                sumaSaldo = sumaSaldo + pcpr.SaldoAbono;
                        }

                        
                        Double rebs = 0;
                        Double rebajaAnciano = 0;
                        Double rebajaArtesano = 0;
                        Double rebajaDiscapacidad = 0;

                        #region Rubros deudores
                        foreach (PlanillaRubroDto pcpr in pcp.RubrosNav)
                        {
                            if (pcpr.Origen == 1)
                            {
                                if (pcpr.Saldo > 0)
                                {
                                    Double factorAbono = pcpr.SaldoAbono / sumaSaldo;
                                    Double rubroAbono = pcp.Parcial * factorAbono;
                                    pcpr.Abono = rubroAbono;
                                    pcpr.Pagos = pcpr.Pagos + rubroAbono;

                                    if (pcpr.RubroNav.RebajasCodigos.Contains("2.1"))
                                    {
                                        if (aplicaAnciano)
                                        {
                                            if (pcpr.Rubro != null)
                                                fracAnciano = recaudacionesDep.RebajaRubroPorcentaje(1, (int)pcpr.Rubro);
                                            if (fracAnciano > 0)
                                            {
                                                pcpr.RebajaAbono = (((rubroAbono) * 100) / (100 - fracAnciano)) - (rubroAbono);
                                                rebajaAnciano = rebajaAnciano + pcpr.RebajaAbono;
                                            }
                                        }
                                    }

                                    if (pcpr.RubroNav.RebajasCodigos.Contains("2.3") && fracArtesano > 0)
                                    {
                                        if (aplicaArtesano)
                                        {
                                            /*if (pcpr.Rubro != null)
                                            fracArtesano = recaudacionesDep.RebajaRubroPorcentaje(3, (int)pcpr.Rubro);*/
                                            pcpr.RebajaAbono = (((rubroAbono) * 100) / (100 - fracArtesano)) - (rubroAbono);
                                            rebajaArtesano = rebajaArtesano + pcpr.RebajaAbono;
                                        }
                                        
                                    }
                                    
                                    if (pcpr.RubroNav.RebajasCodigos.Contains("2.2") && fracDiscapacidad > 0)
                                    {
                                        if (aplicaDiscapacidad)
                                        {
                                            /*if (pcpr.Rubro != null)
                                            fracDiscapacidad = recaudacionesDep.RebajaRubroPorcentaje(2, (int)pcpr.Rubro);*/
                                            pcpr.RebajaAbono = (((rubroAbono) * 100) / (100 - fracDiscapacidad)) - (rubroAbono);
                                            rebajaDiscapacidad = rebajaDiscapacidad + pcpr.RebajaAbono;
                                        }
                                    }

                                    pcpr.Rebajas = pcpr.Rebajas + pcpr.RebajaAbono;
                                        
                                    rebs = rebs + pcpr.RebajaAbono;
                                    CobrosRubroDto cr = new CobrosRubroDto();
                                    cr.Cobro = 0;
                                    cr.Id = 0;
                                    cr.Origen = pcpr.Origen;
                                    cr.Referencia = pcpr.Planilla;
                                    cr.Rubro = pcpr.Rubro;
                                    cr.Estado = 0;
                                    cr.Valor = pcpr.Abono;
                                    cr.Rebajas = pcpr.RebajaAbono;
                                    cobrubs.Add(cr);
                                }
                            }
                        }
                        #endregion

                        #region Rubros Acreedores
                        Double sumaRebajas = 0;
                        Double rebajaCobro = 0;
                        foreach (PlanillaRubroDto pr in pcp.RubrosNav)
                        {
                            if (pr.Origen == -1)
                            {
                                Double valRebaja = 0;
                                switch (pr.Rubro)
                                {
                                    case 4:
                                        {
                                            if (pr.Id == 0)
                                            {
                                                pr.Valor = rebajaAnciano;                                                
                                            }
                                            else
                                            {                                                
                                                pr.Valor = pr.Rebaja + rebajaAnciano;
                                            }
                                            valRebaja = rebajaAnciano;                                            
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (pr.Id == 0)
                                            {
                                                pr.Valor = rebajaDiscapacidad;
                                            }
                                            else
                                            {
                                                pr.Valor = pr.Rebaja + rebajaDiscapacidad;
                                            }
                                            valRebaja = rebajaDiscapacidad;
                                            break;
                                        }
                                    case 29:
                                        {
                                            if (pr.Id == 0)
                                            {
                                                pr.Valor = rebajaArtesano;
                                            }
                                            else
                                            {
                                                pr.Valor = pr.Rebaja + rebajaArtesano;
                                            }
                                            valRebaja = rebajaArtesano;
                                            break;
                                        }
                                }

                                if (valRebaja > 0)
                                {
                                    CobrosRubroDto cr = new CobrosRubroDto();
                                    cr.Cobro = 0;
                                    cr.Id = 0;
                                    cr.Origen = pr.Origen;
                                    cr.Referencia = pr.Planilla;
                                    cr.Rubro = pr.Rubro;
                                    cr.Estado = 0;
                                    cr.Valor = valRebaja;
                                    cobrubs.Add(cr);
                                }

                                rebajaCobro = rebajaCobro + valRebaja;
                                sumaRebajas = sumaRebajas + (Double)pr.Valor;
                            }
                        }
                        #endregion

                        CobroTransaccionDto t = new CobroTransaccionDto();
                        t.Cobro = 0;
                        t.Estado = 0;
                        t.Id = 0;
                        t.Transaccion = pcp.Id;
                        t.Valor = pcp.Parcial;
                        t.Rebajas = rebajaCobro;
                        t.Recargos = 0;
                        tp = tp + pcp.Parcial;
                        ts.Add(t);

                        pcp.Rebajas = sumaRebajas;
                        recaudacionesDep.MarcarPlanillaParcial(pcp);
                    }                    
                }

                #endregion

                cob.Valor = tp;
                cob.TransaccionesNav = ts.ToArray();
                cob.RubrosNav = cobrubs.ToArray();

                // Crear CobroElementos segun ingreso
                foreach (Cliente.Modelos.ElementoPago cele in cobEle.LAbonos)
                {
                    CobrosElementoDto e = new CobrosElementoDto();
                    e.Id = 0;
                    e.Valor = cele.Valor;
                    e.Cobro = 0;
                    e.FormaPago = cele.Tipo + 1; // 1:Efectivo, 2:Cheque, 3:N.Credito
                    e.Estado = 0;
                    switch (cele.Tipo)
                    {
                        case 0: { e.Entidad = cele.CajaId; break; }
                        case 1:
                            {
                                e.Nombres = cele.Nombres;
                                e.Denominacion = cele.Banco;
                                e.Codigo = cele.Cuenta;
                                e.Numero = cele.Numero;
                                e.Fecha = cele.Fecha;
                                break;
                            }
                        case 2:
                            {
                                e.Entidad = cele.NotaCreditoId;
                                break;
                            }
                    }
                    el.Add(e);
                }

                cob.ElementosNav = el.ToArray();
                int i = recaudacionesDep.CobroRegistrar(cob);

                // Crear Nota de credito de ser necesario
                if (cobEle.CrearNCreditoMarca && cobEle.Diferencia < 0 && contribNC > 0)
                {
                    String s = cobEle.Diferencia.ToString();
                    s = s.Replace("-", "");
                    Double d = Convert.ToDouble(s);
                    ConvenioDto nc = new ConvenioDto
                    {
                        Tipo = 2,
                        Contribuyente = contribNC,
                        FechaEmision = this.emisionesDep.Servicio.Hoy(),
                        Concepto = i, // Cobro
                        Pagos = 0,
                        Emisiones = 0,
                        Valor = d,
                        Estado = 0
                    };
                    this.emisionesDep.NotaCreditoCrear(nc);
                }

                foreach (Cliente.Modelos.ElementoPago cele in cobEle.LAbonos)
                {
                    if (cele.Tipo == 2)
                        emisionesDep.NCreditoAplicarPago(cele.NotaCreditoId, cele.Valor, i); // Actualizar nota de credito con el valor aplicado
                }

                ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
                if (iajustes.Vista.ShowDialog() == true)
                {
                    #region Imprimir titulos no parciales
                    foreach (PlanillaDto p in PlanillasSeleccionadas)
                    {
                        if (p.ConceptoNav.PagosParciales == false)
                        {
                            Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                            Telerik.Reporting.ReportSource rsdb = null;
                            // Buscar si el titulo tiene definicion por concepto
                            if (p.ConceptoNav.ReporteDefinicion != null && p.ConceptoNav.ReporteDefinicion.Length > 0)
                            {
                                var con = new Intelligob.Reportes.ReportConnectionStringManager(Utilerias.ConexionDatos.CadenaConexion);
                                Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                                rsOrigen.Xml = p.ConceptoNav.ReporteDefinicion;
                                rsdb = con.UpdateReportSource(rsOrigen);
                            }

                            // Intentar traer desde la base de datos 2-10
                            TablaClaveDto tr = ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 10).FirstOrDefault();
                            if (tr != null && tr.Superior != null && tr.Superior > 0)
                            {
                                RepRecaudacionesDep depRep = new RepRecaudacionesDep();
                                Cliente.Referencia.ReporteDto repTit = depRep.ReportePorId((int)tr.Superior);
                                if (repTit != null)
                                {
                                    var con = new Intelligob.Reportes.ReportConnectionStringManager(Utilerias.ConexionDatos.CadenaConexion);
                                    Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                                    rsOrigen.Xml = repTit.Definicion;
                                    rsdb = con.UpdateReportSource(rsOrigen);
                                }
                            }                            
                            if (rsdb != null)
                            {
                                rsdb.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", p.Id));
                                reportProcessor.PrintReport(rsdb, iajustes.AjustesImpresion);
                            }
                            else
                            {                                
                                InstanceReportSource rs = new InstanceReportSource();
                                Intelligob.Reportes.Recaudaciones.Planilla pf = new Reportes.Recaudaciones.Planilla();
                                rs.ReportDocument = pf;
                                rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", p.Id));
                                rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmpresa", tablasDep.NombreEmpresa));
                                reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);

                            }
                            
                        }

                    }

                    #endregion

                    #region Imprime titulos de pago parcial
                    foreach (PlanillaDto tcp in parciales)
                    {
                        Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                        Telerik.Reporting.ReportSource rsdb = null;
                        // Buscar si el titulo tiene definicion por concepto
                        if (tcp.ConceptoNav.ReporteDefinicion != null && tcp.ConceptoNav.ReporteDefinicion.Length > 0)
                        {
                            var con = new Intelligob.Reportes.ReportConnectionStringManager(Utilerias.ConexionDatos.CadenaConexion);
                            Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                            rsOrigen.Xml = tcp.ConceptoNav.ReporteDefinicion;
                            rsdb = con.UpdateReportSource(rsOrigen);
                        }

                        // Intentar traer desde la base de datos 2-10
                        TablaClaveDto tr = ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 11).FirstOrDefault();
                        if (tr != null && tr.Superior != null && tr.Superior > 0)
                        {
                            RepRecaudacionesDep depRep = new RepRecaudacionesDep();
                            Cliente.Referencia.ReporteDto repTit = depRep.ReportePorId((int)tr.Superior);
                            if (repTit != null)
                            {
                                var con = new Intelligob.Reportes.ReportConnectionStringManager(Utilerias.ConexionDatos.CadenaConexion);
                                Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                                rsOrigen.Xml = repTit.Definicion;
                                rsdb = con.UpdateReportSource(rsOrigen);
                            }
                        }                            
                        if (rsdb != null)
                        {
                            rsdb.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", tcp.Id));
                            reportProcessor.PrintReport(rsdb, iajustes.AjustesImpresion);
                        }
                        else
                        {
                            InstanceReportSource rs = new InstanceReportSource();
                            Intelligob.Reportes.Recaudaciones.PlanillaParcial pf = new Reportes.Recaudaciones.PlanillaParcial();
                            rs.ReportDocument = pf;
                            rs.Parameters.Add(new Telerik.Reporting.Parameter("pPlanilla", tcp.Id));
                            rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaPago", recaudacionesDep.HoyServidor()));
                            reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                        }                                                
                    }
                    #endregion
                }                

                ConsultarAccion();
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
                    this.TextoBusqueda = sc.Seleccionado.Nombres;
                    ConsultarAccion();
                }
            }           
        }

        private void SeleTodoAccion()
        {
            ((CobroConsulta)this.Vista).ModificarSeleccion(9);
        }

        private void SeleNadaAccion()
        {
            ((CobroConsulta)this.Vista).ModificarSeleccion(0);
        }

        private void SeleInvertirAccion()
        {
            ((CobroConsulta)this.Vista).ModificarSeleccion(1);
        }

        private void RegresarAccion()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private void LimpiarAccion()
        {
            if (this.contribuyenteConsultado != null)
                this.contribuyenteConsultado = null;
            this.TextoBusqueda = "";
            this.LPlanillas.Clear();
            if (this.TotalSeleccionado > 0)
                this.TotalSeleccionado = 0;
        }

        private void ActContribuyenteAccion()
        {
            bool modificado = false;                
            foreach(PlanillaDto p in PlanillasSeleccionadas)
            {
                String conid = String.Empty;
                int servid = 0;
                switch(p.Concepto)
                {
                    case 1:
                        {
                            #region Predio urbanos
                            if (p.Servicio > 0)
                            {
                                IEnumerable<PredioPropietarioDto> pros = this.emisionesDep.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", p.Servicio));
                                int conteo = pros.Count();
                                if (conteo == 1)
                                {
                                    conid = "[" + pros.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                }
                                else
                                {
                                    if (conteo > 1)
                                    {
                                        foreach(PredioPropietarioDto prepro in pros)
                                        {
                                            conid = conid + "[" + prepro.Contribuyente.ToString() + "]";
                                        }
                                    }
                                }                                
                            }
                            else // Buscar por codigo
                            {
                                int preid = 0;
                                IEnumerable<PredioBaseDto> pres = catastrosDep.PrediosPorCodigo(p.Codigo.Trim(), 0, Utiles.TipoBusquedaTexto.tbIgual, 0, false);
                                int conteo = pres.Count();
                                if (conteo == 1)
                                {
                                    preid = pres.ElementAtOrDefault(0).Id;
                                }
                                else
                                {
                                    // Escoger predio
                                    if (conteo > 1)
                                    {
                                        ModeloVista.Catastros.PredioSeleccionarVM presel = new Catastros.PredioSeleccionarVM(pres, 0);
                                        if (presel.Vista.DialogResult == true)
                                        {
                                            preid = presel.Seleccionado.Id;
                                        }
                                    }
                                }
                                if (preid > 0)
                                {
                                    IEnumerable<PredioPropietarioDto> pros = this.emisionesDep.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", p.Servicio));
                                    int conteopro = pros.Count();
                                    if (conteopro == 1)
                                    {
                                        conid = "[" + pros.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                    }
                                    else
                                    {
                                        if (conteopro > 1)
                                        {
                                            foreach (PredioPropietarioDto prepro in pros)
                                            {
                                                conid = conid + "[" + prepro.Contribuyente.ToString() + "]";
                                            }
                                        }
                                    }
                                    servid = preid;
                                }
                            }                       
                            break;
                            #endregion
                        }
                    case 2:
                        {
                            #region Predios rusticos
                            if (p.Servicio > 0)
                            {
                                IEnumerable<PredioPropietarioDto> pros = this.emisionesDep.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", p.Servicio));
                                int conteo = pros.Count();
                                if (conteo == 1)
                                {
                                    conid = "[" + pros.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                }
                                else
                                {
                                    if (conteo > 1)
                                    {
                                        foreach (PredioPropietarioDto prepro in pros)
                                        {
                                            conid = conid + "[" + prepro.Contribuyente.ToString() + "]";
                                        }
                                    }
                                }
                            }
                            else // Buscar por codigo
                            {
                                int preid = 0;
                                IEnumerable<PredioBaseDto> pres = catastrosDep.PrediosPorCodigo(p.Codigo.Trim(), 0, Utiles.TipoBusquedaTexto.tbIgual, 1, false);
                                int conteo = pres.Count();
                                if (conteo == 1)
                                {
                                    preid = pres.ElementAtOrDefault(0).Id;
                                }
                                else
                                {
                                    // Escoger predio
                                    if (conteo > 1)
                                    {
                                        ModeloVista.Catastros.PredioSeleccionarVM presel = new Catastros.PredioSeleccionarVM(pres, 1);
                                        if (presel.Vista.DialogResult == true)
                                        {
                                            preid = presel.Seleccionado.Id;
                                        }
                                    }
                                }
                                if (preid > 0)
                                {
                                    IEnumerable<PredioPropietarioDto> pros = this.emisionesDep.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", p.Servicio));
                                    int conteopro = pros.Count();
                                    if (conteopro == 1)
                                    {
                                        conid = "[" + pros.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                    }
                                    else
                                    {
                                        if (conteopro > 1)
                                        {
                                            foreach (PredioPropietarioDto prepro in pros)
                                            {
                                                conid = conid + "[" + prepro.Contribuyente.ToString() + "]";
                                            }
                                        }
                                    }
                                    servid = preid;
                                }
                            }                            
                            break;
                            #endregion
                        }
                    case 3:
                        {
                            #region Patentes municipales
                            if (p.Servicio > 0)
                            {
                                PatenteDto pat = emisionesDep.Servicio.ReadPatente(String.Format(emisionesDep.FormatoClave, p.Servicio));
                                if (pat.Contribuyente != null && pat.Contribuyente > 0)
                                    conid = "[" + pat.Contribuyente.ToString() + "]";
                            }
                            else
                            {
                                IEnumerable<PatenteDto> pats = catastrosDep.PatentePorCodigo(p.Codigo, 9, TipoBusquedaTexto.tbIgual);
                                int conteo = pats.Count();
                                if (conteo == 1)
                                {
                                    conid = "[" + pats.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                    servid = pats.ElementAtOrDefault(0).Id;
                                }
                                else
                                {
                                    if (conteo > 1)
                                    {
                                        ModeloVista.Catastros.PatenteSeleccionarVM patsel = new Catastros.PatenteSeleccionarVM(pats);
                                        if (patsel.Vista.DialogResult == true)
                                        {
                                            conid = "[" + patsel.Seleccionado.Contribuyente.ToString() + "]";
                                            servid = patsel.Seleccionado.Id;
                                        }
                                    }
                                    
                                }
                            }
                            break;
                            #endregion
                        }
                    case 6:
                        {
                            #region Agua Potable
                            if (p.Servicio > 0)
                            {
                                AguaPotableDto cta = emisionesDep.Servicio.ReadAguaPotable(String.Format(emisionesDep.FormatoClave, p.Servicio));
                                if (cta.Contribuyente != null && cta.Contribuyente > 0)
                                    conid = "[" + cta.Contribuyente.ToString() + "]";
                            }
                            else
                            {
                                IEnumerable<AguaPotableDto> ctas = aguaDep.CuentasPorCodigo(p.Codigo, 9, 0);
                                int conteo = ctas.Count();
                                if (conteo == 1)
                                {
                                    conid = "[" + ctas.ElementAtOrDefault(0).Contribuyente.ToString() + "]";
                                    servid = ctas.ElementAtOrDefault(0).Id;
                                }
                                else
                                {
                                    if (conteo > 1)
                                    {
                                        ModeloVista.Agua.AguaCuentaSeleccionarVM ctasel = new Agua.AguaCuentaSeleccionarVM(ctas);
                                        if (ctasel.Vista.DialogResult == true)
                                        {
                                            conid = "[" + ctasel.Seleccionado.Contribuyente.ToString() + "]";
                                            servid = ctasel.Seleccionado.Id;
                                        }
                                    }
                                }
                            }
                            break;
                            #endregion
                        }
                }
                if (conid != String.Empty)
                {
                    PlanillaDto pla = this.recaudacionesDep.Servicio.ReadPlanilla(String.Format(recaudacionesDep.FormatoClave, p.Id));
                    pla.Contribuyentes = conid;
                    if ((pla.Servicio == null || pla.Servicio == 0) && servid > 0)
                        pla.Servicio = servid;
                    recaudacionesDep.Servicio.UpdatePlanilla(pla);
                    modificado = true;
                }
                else
                {

                }
            }
            if (modificado)
            {

                this.ConsultarAccion(); 
            }
            else
            { 
                Utiles.CuadroMensajes.Alertar("Informacion", "No se realizo ningun cambio", "No se ha podido actualizar el/los titulo(s) seleccionado(s) por no poder ubicarlo en el catastro correspondiente", ""); 
            }
            // Volver a ejecutar la consulta de ley
        }

        private void CobroNotaCrearAccion()
        {
            // Crear Nota de cobro
            CobrosNotaDto nc = new CobrosNotaDto();
            nc.Fecha = DateTime.Today;
            int i = recaudacionesDep.CrearNotaCobro(nc);

            // Crear Elementos de Nota de cobro
            foreach (PlanillaDto p in PlanillasSeleccionadas)
            {
                CobrosNotasElementoDto e = new CobrosNotasElementoDto();
                e.Nota = i;
                e.Transaccion = p.Id;
                //eles.Add(e);
                // imprimir planilla
                recaudacionesDep.CrearNotaCobEle(e);
            }  

            // imprimir nota de cobro y titulos de ser necesario presentar pantalla con titulos seleccionados para imprimir
            // presentar ventana con los titulos selecciona

        }

        private bool ContribuyenteHabilita()
        {
            return this.ConsultaPorContribuyente && ! ConsultaOcupada;
        }

        private bool ConsultarHabilita()
        {
            bool ret = false;
            if (this.ConsultaPorContribuyente && this.contribuyenteConsultado != null && this.contribuyenteConsultado.Id > 0)
                ret = true;
            else
            {
                if (this.ConsultaPorCodigo && this.TextoBusqueda.Length > 0)
                    ret = true;
            }
            return ret && ! ConsultaOcupada;
        }

        private bool CalcularHabilita()
        {
            return this.PlanillasSeleccionadas.Count > 0 && !ConsultaOcupada;
        }

        private bool CobrarHabilita()
        {
            return this.puedeCobrar && this.PlanillasSeleccionadas.Count > 0 && ! ConsultaOcupada;
        }

        private bool ReliquidarHabilita()
        {
            return this.puedeCobrar && this.PlanillasSeleccionadas.Count > 0 && ! ConsultaOcupada;
        }

        private bool SeleTodoHabilita()
        {
            return this.LPlanillas.Count > 0 && ! ConsultaOcupada;
        }

        private bool SeleInvertirHabilita()
        {
            return this.PlanillasSeleccionadas.Count > 0 && !ConsultaOcupada;
        }

        private bool SeleNadaHabilita()
        {
            return this.PlanillasSeleccionadas.Count > 0 && !ConsultaOcupada;
        }

        private bool ImprimirHabilita()
        {
            return this.puedeImprimir && this.LPlanillas.Count > 0 && !ConsultaOcupada;
        }

        private bool RegresarHabilita()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private bool LimpiarHabilita()
        {
            return this.LPlanillas.Count > 0 && !ConsultaOcupada;
        }

        private bool ActContribuyenteHabilita()
        {
            return this.puedeActContribuyente && this.PlanillasSeleccionadas.Count > 0 && !ConsultaOcupada;
        }

        private bool NotaCobroCrearHabilita()
        {
            return this.puedeImprimir && this.PlanillasSeleccionadas.Count > 0 && !ConsultaOcupada;
        }

        public string this[string columnName]
        {
            get
            {
                String error = String.Empty;
                if (columnName == "TextoBusqueda")
                {                    
                    switch (indiceConsultaSeleccion)
                    {
                        case 1:
                            {
                                if (this.contribuyenteConsultado == null || this.contribuyenteConsultado.Id <= 0)
                                    error = "Debe seleccionar un contribuyente";
                                break;
                            }
                        case 2:
                            {
                                if (this.TextoBusqueda.Length <= 0)
                                    error = "Digite el codigo a consultar";
                                break;
                            }
                        case 3:
                            {
                                if (this.TextoBusqueda.Length > 0)
                                {
                                    try
                                    {
                                        int i = Convert.ToInt32(this.TextoBusqueda);
                                    } catch (Exception ex)
                                    {
                                        error = "Digite un numero valido";
                                    }
                                }
                                else
                                {
                                    error = "Digite el numero de Nota de cobro";
                                }
                                break;
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
