using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Intelligob.Escritorio.ModeloVista.Catastros;
using Intelligob.Escritorio.Vistas.General;
using System.Collections.Generic;
using Intelligob.Escritorio.ModeloVista.Agua;
using System.Globalization;
using Intelligob.Cliente.Depositos;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class EmisionesListaVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.EmisionesDep emiDep = new Cliente.Depositos.EmisionesDep();

        private readonly Cliente.Depositos.ConceptosDep conDep = new Intelligob.Cliente.Depositos.ConceptosDep();        
        
        private ObservableCollection<Intelligob.Cliente.Referencia.ConceptoDto> lConceptos;
        public ObservableCollection<Intelligob.Cliente.Referencia.ConceptoDto> LConceptos
        {
            get { return this.lConceptos; }
            set { this.lConceptos = value; OnPropertyChanged("LConceptos"); }
        }
        
        private Intelligob.Cliente.Referencia.ConceptoDto seleccionado;
        public Intelligob.Cliente.Referencia.ConceptoDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value ; OnPropertyChanged("Seleccionado"); }
        }

        private String barraEstado = "Listo";
        public String BarraEstado
        { 
            get { return this.barraEstado; } 
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); } 
        }

        public ICommand CmdCalcular
        { get; internal set; }

        public ICommand CmdEmitir
        { get; internal set; }

        public ICommand CmdImprimir
        { get; internal set; }

        public ICommand CmdSeleccionar
        { get; internal set; }

        public ICommand CmdGeneral
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }

        public EmisionesListaVM() : this(null) { }

        public EmisionesListaVM(IEnumerable<Cliente.Referencia.ConceptoDto> plConceptos) : base(new Intelligob.Escritorio.Vistas.Emisiones.EmisionesLista())
        {
            if (plConceptos == null || plConceptos.Count() == 0)
                LConceptos = new ObservableCollection<Cliente.Referencia.ConceptoDto>(conDep.ConceptosParaEmision());
            else
                LConceptos = new ObservableCollection<Cliente.Referencia.ConceptoDto>(plConceptos);
            this.CmdCalcular = new Comandos.ComandoDelegado((o) => Calcular());
            this.CmdEmitir = new Comandos.ComandoDelegado((o) => Emitir(), (o) => EmitirHabilita());
            this.CmdImprimir = new Comandos.ComandoDelegado((o) => Imprimir(), (o) => ImprimirHabilita());
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => Seleccionar(), (o) => SeleccionarHabilita());
            this.CmdGeneral = new Comandos.ComandoDelegado((o) => EmitirGeneral(), (o) => GeneralHabilita());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => Regresar(), (o) => RegresarHabilita());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            // filtrar para traer los rubros habilitados para el usuario
        }

        #region Habilitadores de comandos

        public Boolean SeleccionarHabilita()
        {
            return this.Seleccionado != null && this.Seleccionado.ParametroSeleccionado != null && this.Seleccionado.ParametroSeleccionado.Origen == 0;
        }

        public Boolean GeneralHabilita()
        {
            return this.Seleccionado != null && this.Seleccionado.Periodo <= 2;
        }

        public Boolean EmitirHabilita()
        {
            return this.Seleccionado != null && this.Seleccionado.TotalEmision > 0;
        }

        public Boolean ImprimirHabilita()
        {
            return this.Seleccionado != null && Seleccionado.TotalEmision > 0;
        }

        public Boolean RegresarHabilita()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        #endregion

        public void Calcular()
        {
            double total = 0;
            IEnumerable<Cliente.Referencia.RubroCalcularConcepto> rubs = emiDep.CalcularRubrosPorConcepto(Seleccionado.Id, Seleccionado.EmisionParametros);
            foreach (Cliente.Referencia.RubroCalcularConcepto r in rubs)
            {
                total = total + (double)r.VALOR;
            }
            this.Seleccionado.RubrosCalculo = rubs;
            this.Seleccionado.TotalEmision = Math.Round(total, 2);
        }

        public void Emitir()
        {
            string proceso = "¿Seguro de ejecutar esta emision nueva de " + Seleccionado.Denominacion + "?";
            bool proceder = true;            
            if (Seleccionado.Periodo <= 2)
            { // Validar (No se puede emitir individual mientras se emite general)
                
                Cliente.Depositos.ConceptosDep d = new Cliente.Depositos.ConceptosDep();
                if (d.ConceptoPorId(Seleccionado.Id).Estado == 8)
                {
                    Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "Se esta realizando la emision general de este rubro, vuelva intentar una vez terminado dicho proceso", "");
                    proceder = false;
                }
                else
                {
                    Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
                    int ya = DateTime.Today.Year;
                    int ma = DateTime.Today.Month;
                    int yn = DateTime.Today.Year;
                    int mn = DateTime.Today.Month;
                    foreach(Cliente.Referencia.ConceptosEmisionDto e in Seleccionado.EmisionParametros)
                    {
                        if (e.Periodico == 1)
                        {
                            ya = (int)td.ClavePorId((int)e.Referencia).Superior;
                            yn = e.Identificador;
                        }                            
                        else
                        {
                            if (e.Periodico == 2)
                            {                                
                                ma = (int)td.ClavePorId((int)e.Referencia).Superior;
                                mn = e.Identificador;
                            }
                            
                        }
                    }
                    if (Seleccionado.Periodo == 1)
                    { // No se puede emitir año posterior ni anterior a 5 periodos
                        if (yn > ya || yn < ya - 5)
                        {
                            Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "La emision no puede ser superior a la ultima emision general ni inferior a 5 periodos", "");
                            proceder = false;
                        }
                    }
                    else
                    {
                        if (Seleccionado.Periodo == 2)
                        {
                            if (yn > ya || yn < ya - 5)
                            {
                                Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "La emision no puede ser superior a la ultima emision general ni inferior a 5 periodos", "");
                                proceder = false;
                            }
                            else
                            {
                                if (yn == ya)
                                {
                                    if (mn > ma)
                                    {
                                        Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", "La emision no puede ser superior al ultimo mes de la emision general", "");
                                        proceder = false;
                                    }
                                }
                            }
                        }
                    }
                    if (proceder)
                    {
                        Cliente.Depositos.RecaudacionesDep rd = new Cliente.Depositos.RecaudacionesDep();
                        Cliente.Referencia.PlanillaDto p = rd.PlanillaPorEmision(yn, Seleccionado.Id, Seleccionado.EmisionParametros.ElementAt(0).Identificador);
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
                                    proceso = "Esta emision ya fue realizada anteriormente ¿Desea reliquidar los valores?";                                
                            }   
                        }                                            
                    }
                }
            }
            
            if (proceder)
            {
                if (Seleccionado.Validar != null && Seleccionado.Validar == 1)
                {
                    string ms = emiDep.ValidarEmisionConcepto(Seleccionado.Id, Seleccionado.EmisionParametros);
                    if (ms != "V")
                    {
                        proceder = false;
                        Utiles.CuadroMensajes.Alertar("Emision de titulos", "No se puede emitir", ms, "");
                    }
                }

                if (proceder)
                {
                    TaskDialogInterop.TaskDialogResult r = Utiles.CuadroMensajes.Preguntar("Emision de titulos", "Confirmar operacion", proceso);
                    if (r.CustomButtonResult == 0)
                    {
                        emiDep.EmitirTituloPorConcepto(Seleccionado.Id, Seleccionado.EmisionParametros);
                        Seleccionado.RubrosCalculo = null;
                        Seleccionado.TotalEmision = 0;
                        foreach (Cliente.Referencia.ConceptosEmisionDto p in Seleccionado.EmisionParametros)
                        {
                            if (p.Periodico == 1 || p.Periodico == 2)
                                p.Identificador = (int)Cliente.ModeloCache.Instance.McClaves.Where(c => c.Id == p.Referencia).FirstOrDefault().Superior;
                            else
                                p.Identificador = 0;
                            p.Valor = 0;
                            p.SeleccionadoPresentacion = "";
                            p.Cadena = "";
                        }
                        Utiles.CuadroMensajes.Aceptar("Emision de titulos", "Operacion completa", "La emision se completó satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                    }
                }                
            }
        }

        public void Imprimir()
        { }

        public void Seleccionar()
        {
            switch (Seleccionado.ParametroSeleccionado.OrigenClave)
            {
                case 7:
                    {
                        SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
                        if (sc.Vista.DialogResult == true)
                        {
                            Seleccionado.ParametroSeleccionado.Identificador = sc.Seleccionado.Id;
                            Seleccionado.ParametroSeleccionado.SeleccionadoPresentacion = sc.Seleccionado.Nombres;
                        }
                        break;
                    }
                case 8:
                    {
                        AguaCuentaSeleccionarVM cs = new AguaCuentaSeleccionarVM();
                        if (cs.Vista.DialogResult == true)
                        {
                            Seleccionado.ParametroSeleccionado.Identificador = cs.Seleccionado.Id;
                            Seleccionado.ParametroSeleccionado.SeleccionadoPresentacion = cs.Seleccionado.Codigo + " : " + cs.Seleccionado.ContribuyenteNav.Nombres;
                        }
                        break;
                    }
                case 13:
                    {
                        PredioSeleccionarVM ps = new PredioSeleccionarVM((int)Seleccionado.ParametroSeleccionado.Referencia);
                        if (ps.Vista.DialogResult == true)
                        {
                            Seleccionado.ParametroSeleccionado.Identificador = ps.Seleccionado.Id;
                            Seleccionado.ParametroSeleccionado.SeleccionadoPresentacion = ps.Seleccionado.Codigo + " : " + ps.Seleccionado.PropietariosCadena;
                        }
                        break;
                    }
                case 19:
                    {
                        PatenteSeleccionarVM ps = new PatenteSeleccionarVM();
                        if (ps.Vista.DialogResult == true)
                        {
                            Seleccionado.ParametroSeleccionado.Identificador = ps.Seleccionado.Id;
                            Seleccionado.ParametroSeleccionado.SeleccionadoPresentacion = ps.Seleccionado.Codigo + " : " + ps.Seleccionado.ContribuyenteNav.Nombres;
                        }
                        break;
                    }
            }
        }

        public void EmitirGeneral()
        {
            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
            string sm = Seleccionado.Denominacion + " del año ";
            int y = DateTime.Today.Year;
            int m = DateTime.Today.Month;
            foreach (Cliente.Referencia.ConceptosEmisionDto e in Seleccionado.EmisionParametros)
            {
                if (e.Periodico == 1)
                {
                    y = (int)td.ClavePorId((int)e.Referencia).Superior;
                }
                else
                {
                    if (e.Periodico == 2)
                    {
                        m = (int)td.ClavePorId((int)e.Referencia).Superior;
                    }

                }
            }
            
            if (Seleccionado.Periodo == 2)
            {

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

            }
            else
            {
                y = y + 1;
                sm = sm + y.ToString();
            }
                            
            TaskDialogInterop.TaskDialogResult r = Utiles.CuadroMensajes.Preguntar("Emision de titulos", "Confirmar operacion", "Se ejecutara la emision general de " + sm + ". ¿Seguro de proceder?");
            if (r.CustomButtonResult == 0)
            {
                if (!EmisionAsyncronica.Instance.IsBusy)
                    EmisionAsyncronica.Instance.RunWorkerAsync(Seleccionado.Id);
                else
                    Utiles.CuadroMensajes.Alertar("Emision de titulo", "Proceso ocupado", "No se puede ejecutar el proceso hasta que se termine el proceso en ejecucion", "");
            }            
        }

        public void Regresar()
        {
            Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }
    }
}
