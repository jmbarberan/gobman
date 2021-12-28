using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Agua
{
    public class EmisionesVM : BaseMV<Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.EmisionesDep emiDep = new Cliente.Depositos.EmisionesDep();

        //private readonly Cliente.Depositos.ConceptosDep conDep = new Intelligob.Cliente.Depositos.ConceptosDep();

        private int añoEmision = DateTime.Today.Year;
        public int AñoEmision 
        {
            get { return this.añoEmision; }
            set { this.añoEmision = value; OnPropertyChanged("AñoEmision"); }
        }        

        private int mesEmision = DateTime.Today.Month - 1;
        public int MesEmision
        {
            get { return this.mesEmision; }
            set { this.mesEmision = value; OnPropertyChanged("MesEmision"); }
        }
        public int MesEmisionCompleto
        {
            get { return this.mesEmision + 1; }
        }

        private int cuentaId = 0;

        private String cuentaCodigo = "";
        public String CuentaCodigo
        {
            get { return this.cuentaCodigo; }
            set { this.cuentaCodigo = value; OnPropertyChanged("CuentaCodigo"); }
        }

        private String contribuyenteNombres;
        public String Nombres
        {
            get { return this.contribuyenteNombres; }
            set { this.contribuyenteNombres = value; OnPropertyChanged("Nombres"); }
        }

        private double totalEmision = 0;
        public double TotalEmision
        {
            get { return this.totalEmision; }
            set { this.totalEmision = value; OnPropertyChanged("TotalEmision"); }
        }

        private ObservableCollection<Cliente.Referencia.RubroCalcularConcepto> lRubros = new ObservableCollection<Cliente.Referencia.RubroCalcularConcepto>();
        public ObservableCollection<Cliente.Referencia.RubroCalcularConcepto> LRubros
        {
            get { return this.lRubros; }
            set { this.lRubros = value; OnPropertyChanged("LRubros"); }
        }

        #region Declaracion de comandos

        public System.Windows.Input.ICommand CmdCalcular { get; internal set; }

        public System.Windows.Input.ICommand CmdEmitir { get; internal set; }

        public System.Windows.Input.ICommand CmdBuscar { get; internal set; }

        public System.Windows.Input.ICommand CmdEmisionGeneral { get; internal set; }

        public System.Windows.Input.ICommand CmdRegresar { get; internal set; }

        public System.Windows.Input.ICommand CmdAdelantar { get; internal set; }

        #endregion

        public EmisionesVM() : base(new Vistas.Agua.Emisiones())
        {
            this.CrearComandos();

        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => AccionBuscar());
            this.CmdCalcular = new Comandos.ComandoDelegado((o) => AccionCalcular(), (o) => PuedeCalcular());
            this.CmdEmitir = new Comandos.ComandoDelegado((o) => AccionEmitir(), (o) => PuedeEmitir());
            this.CmdEmisionGeneral = new Comandos.ComandoDelegado((o) => AccionEmisionGeneral());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => AccionRegresar(), (o) => PuedeRegresar());
            this.CmdAdelantar = new Comandos.ComandoDelegado((o) => AccionAdelantar(), (o) => PuedeAdelantar());
        }

        #region Habilitadores de comandos
        private bool PuedeEmitir()
        {
            return this.cuentaId > 0 && this.TotalEmision > 0;
        }

        private bool PuedeCalcular()
        {
            return this.cuentaId > 0;
        }

        private bool PuedeRegresar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        #endregion

        #region Acciones de comandos
        private void AccionBuscar()
        {
            AguaCuentaSeleccionarVM cs = new AguaCuentaSeleccionarVM();
            if (cs.Vista.DialogResult == true)
            {
                this.cuentaId = cs.Seleccionado.Id;
                this.CuentaCodigo = cs.Seleccionado.Codigo;
                if (cs.Seleccionado.Contribuyente != null && cs.Seleccionado.Contribuyente > 0)
                {
                    Cliente.Depositos.ContribuyentesDep d = new Cliente.Depositos.ContribuyentesDep();
                    Cliente.Referencia.ContribuyenteDto c = d.ContribuyentePorId((int)cs.Seleccionado.Contribuyente);
                    this.Nombres = c.Nombres;
                }
            }
        }

        private void AccionCalcular()
        {
            double total = 0;
            this.TotalEmision = total;
            List<Cliente.Referencia.ConceptosEmisionDto> pars = new List<Cliente.Referencia.ConceptosEmisionDto>();
            pars.Add(new Cliente.Referencia.ConceptosEmisionDto());
            pars[0].Calcula = 1;
            pars[0].OrigenTipo = 1;
            pars[0].TipoDato = 1;
            pars[0].Identificador = this.cuentaId;
            IEnumerable<Cliente.Referencia.RubroCalcularConcepto> rubs = emiDep.CalcularRubrosPorConcepto(6, pars);
            foreach (Cliente.Referencia.RubroCalcularConcepto r in rubs)
            {
                total = total + (double)r.VALOR;
            }
            this.LRubros = new ObservableCollection<Cliente.Referencia.RubroCalcularConcepto>(rubs);
            this.TotalEmision = Math.Round(total, 2);
        }

        private void AccionEmitir()
        {
            string proceso = "¿Seguro de ejecutar esta emision nueva de Agua Potable?";
            bool proceder = true;
            Cliente.Depositos.ConceptosDep d = new Cliente.Depositos.ConceptosDep();
            if (d.ConceptoPorId(6).Estado == 8)
            {
                Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "Se esta realizando la emision general de este rubro, vuelva intentar una vez terminado dicho proceso", "");
                proceder = false;
            }
            else
            {
                Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
                int ya = DateTime.Today.Year;
                int ma = DateTime.Today.Month;
                ya = (int)td.ClavePorId(6).Superior;
                ma = (int)td.ClavePorId(7).Superior;
                if (this.AñoEmision > ya || this.AñoEmision < ya - 5)
                {
                    Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "La emision no puede ser superior a la ultima emision general ni inferior a 5 periodos", "");
                    proceder = false;
                }
                else
                {
                    if (this.AñoEmision == ya)
                    {
                        if (this.MesEmisionCompleto > ma)
                        {
                            Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "La emision no puede ser superior al ultimo mes de la emision general", "");
                            proceder = false;
                        }
                    }
                }
            }

            if (proceder)
            {
                Cliente.Depositos.RecaudacionesDep rd = new Cliente.Depositos.RecaudacionesDep();
                Cliente.Referencia.PlanillaDto p = rd.PlanillaPorEmision(this.AñoEmision, 6, cuentaId);
                if (p != null && p.Id > 0)
                {
                    if (p.Estado == 1)
                    {
                        Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede volver emitir", "Esta emision fue realizada anteriormente y sus valores ya fueron cobrados", "");
                        proceder = false;
                    }
                    else
                    {
                        if (p.Estado == 0)
                            proceso = "Esta emision ya se fue realizada anteriormente ¿Desea reliquidar los valores?";
                    }
                }
            }

            if (proceder)
            {
                TaskDialogInterop.TaskDialogResult r = Utiles.CuadroMensajes.Preguntar("Emision de titulos", "Confirmar operacion", proceso);
                if (r.CustomButtonResult == 0)
                {
                    List<Cliente.Referencia.ConceptosEmisionDto> pars = new List<Cliente.Referencia.ConceptosEmisionDto>();
                    
                    Cliente.Referencia.ConceptosEmisionDto parCta = new Cliente.Referencia.ConceptosEmisionDto();
                    parCta.Calcula = 1;
                    parCta.Emite = 1;
                    parCta.OrigenTipo = 1;
                    parCta.TipoDato = 1;
                    parCta.Identificador = this.cuentaId;

                    Cliente.Referencia.ConceptosEmisionDto parAño = new Cliente.Referencia.ConceptosEmisionDto();
                    parAño.Calcula = 1;
                    parAño.Emite = 1;
                    parAño.OrigenTipo = 2;
                    parAño.TipoDato = 1;
                    parAño.Identificador = this.AñoEmision;

                    Cliente.Referencia.ConceptosEmisionDto parMes = new Cliente.Referencia.ConceptosEmisionDto();
                    parMes.Calcula = 1;
                    parMes.Emite = 1;
                    parMes.OrigenTipo = 2;
                    parMes.TipoDato = 1;
                    parMes.Identificador = this.MesEmision;

                    pars.Add(parCta);
                    pars.Add(parAño);
                    pars.Add(parMes);
                    emiDep.EmitirTituloPorConcepto(6, pars);
                    this.LRubros = new ObservableCollection<Cliente.Referencia.RubroCalcularConcepto>();
                    this.TotalEmision = 0;
                    this.cuentaId = 0;
                    this.CuentaCodigo = "";
                    this.Nombres = "";
                    Utiles.CuadroMensajes.Aceptar("Emision de titulos", "Operacion completa", "La emision se completó satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                }                
            }
        }

        private void AccionEmisionGeneral()
        {
            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
            string sm = " del año ";
            int y = (int)td.ClavePorId(6).Superior;
            int m = (int)td.ClavePorId(7).Superior;
            if (m == 12)
            {
                m = 1;
                y = y + 1;
            }
            else
                m = m + 1;
            sm = sm + y.ToString();
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            string mes = formatoFecha.GetMonthName(m);
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            sm = sm + " mes de " + textInfo.ToTitleCase(mes);
            TaskDialogInterop.TaskDialogResult r = Utiles.CuadroMensajes.Preguntar("Emision de titulos", "Confirmar operacion", "Se ejecutara la emision general de Agua Potable" + sm + ". ¿Seguro de proceder?");
            if (r.CustomButtonResult == 0)
            {
                if (!Cliente.Depositos.EmisionAsyncronica.Instance.IsBusy)
                {                    
                    Cliente.Depositos.EmisionAsyncronica.Instance.RunWorkerAsync(6);
                }
                    
                else
                    Utiles.CuadroMensajes.Alertar("Emision de titulo", "Proceso ocupado", "No se puede ejecutar otro proceso hasta que se termine el que esta en ejecucion", "");
            } 
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
    }
}
