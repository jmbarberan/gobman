using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class HabilitantesListaVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly int modulo = 3;

        private readonly Cliente.Depositos.RecaudacionesDep recaudaDep = new Cliente.Depositos.RecaudacionesDep();

        private String barraEstado = "Listo";
        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        private ObservableCollection<Cliente.Referencia.PlanillaDto> lHabilitantes;
        public ObservableCollection<Cliente.Referencia.PlanillaDto> LHabilitantes
        {
            get { return lHabilitantes; }
            set { this.lHabilitantes = value; OnPropertyChanged("LHabilitantes"); }
        }

        private ObservableCollection<Utiles.ElementoSeleccion> lEstados;
        public ObservableCollection<Utiles.ElementoSeleccion> LEstados
        { get { return this.lEstados; } set { this.lEstados = value; OnPropertyChanged("LEstados"); } }

        private Cliente.Referencia.PlanillaDto seleccionado;
        public Cliente.Referencia.PlanillaDto Seleccionado
        {
            get { return seleccionado; }
            set 
            {
                this.seleccionado = value; 
                OnPropertyChanged("Seleccionado");
                if (value != null)
                    this.BarraEstado = "Seleccionado: " + value.ConceptoNav.Denominacion + " No. " + Convert.ToString(value.Numero) + " de " + value.ContribuyentesCadena;
            }
        }

        private bool consultaPredeterminada = true;
        public bool ConsultaPredeterminada
        {
            get { return this.consultaPredeterminada; }
            set { this.consultaPredeterminada = value; OnPropertyChanged("ConsultaPredeterminada"); }
        }

        private DateTime fechaIncio = DateTime.Today;
        public DateTime FechaIncio
        {
            get { return this.fechaIncio; }
            set { this.fechaIncio = value; OnPropertyChanged("FechaIncio"); }
        }

        private DateTime fechaCorte = DateTime.Today;
        public DateTime FechaCorte
        {
            get { return this.fechaCorte; }
            set { this.fechaCorte = value; OnPropertyChanged("FechaCorte"); }
        }

        private Utiles.ElementoSeleccion estado;
        public Utiles.ElementoSeleccion EstadoDocumento
        {
            get { return this.estado; }
            set { this.estado = value; OnPropertyChanged("EstadoDocumento"); }
        }

        public bool PermisoImprimir = false;
        public bool PermisoDesmarcar = false;
        public bool PermisoCodebarBuscar = false;

        public System.Windows.Input.ICommand CmdConsultar { get; internal set; }

        public System.Windows.Input.ICommand CmdImprimir { get; internal set; }

        public System.Windows.Input.ICommand CmdDesmarcar { get; internal set; }

        public System.Windows.Input.ICommand CmdRegresar { get; internal set; }

        public System.Windows.Input.ICommand CmdAdelante { get; internal set; }

        public System.Windows.Input.ICommand CmdCodebarBuscar { get; internal set; }

        public HabilitantesListaVM(int pModulo) : base(new Escritorio.Vistas.Recaudaciones.HabilitantesLista())
        {
            Utiles.ElementoSeleccion[] eles = new Utiles.ElementoSeleccion[] 
            {
                new Utiles.ElementoSeleccion(1, 0, "", "Impresos"),
                new Utiles.ElementoSeleccion(0, 0, "", "No impresos"),
                new Utiles.ElementoSeleccion(9, 0, "", "TODOS")
            };
            this.LEstados = new ObservableCollection<Utiles.ElementoSeleccion>(eles);

            this.ProcesarPrivilegios();

            this.CmdConsultar = new Comandos.ComandoDelegado((o) => this.AccionConsultar());
            this.CmdImprimir = new Comandos.ComandoDelegado((o) => this.AccionImprimir(), (o) => this.PuedeImprimir());
            this.CmdDesmarcar = new Comandos.ComandoDelegado((o) => this.AccionDesmarcar(), (o) => this.PuedeDesmarcar());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => this.AccionRegresar(), (o) => this.PuedeRegresar());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
            this.CmdCodebarBuscar = new Comandos.ComandoDelegado((o) => this.AccionCodebarBuscar(), (o) => this.PuedeCodebarBuscar());
            this.modulo = pModulo;
            this.AccionConsultar();
        }

        private void ProcesarPrivilegios()
        {
            Cliente.Depositos.SeguridadDep seguDep = new Cliente.Depositos.SeguridadDep();
            this.PermisoImprimir = false;
            this.PermisoDesmarcar = false;            

            string c = "";
            if (!Cliente.SesionUtiles.Instance.EsDesarrollador)
            {
                Cliente.Referencia.PrivilegioDto p = seguDep.PrivilegiosFuncionPorUsuario(28, Cliente.SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                {
                    c = p.Comandos;
                }
            }

            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.PermisoImprimir = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.PermisoDesmarcar = true;
            if (Cliente.SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.PermisoCodebarBuscar = true;
        }

        private bool PuedeImprimir()
        {
            return this.PermisoImprimir && this.Seleccionado != null && this.Seleccionado.Especie == 0;
        }

        private bool PuedeDesmarcar()
        {
            return this.PermisoDesmarcar && this.Seleccionado != null && this.Seleccionado.Especie == 1;
        }

        private bool PuedeRegresar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private bool PuedeCodebarBuscar()
        {
            return this.PermisoCodebarBuscar;
        }

        private void AccionConsultar()
        {
            if (this.ConsultaPredeterminada)
                this.LHabilitantes = new ObservableCollection<Cliente.Referencia.PlanillaDto>(recaudaDep.DocumentosPorEmitir(modulo));
            else 
            {
                this.LHabilitantes = new ObservableCollection<Cliente.Referencia.PlanillaDto>(
                    recaudaDep.DocumentosPorHistorico(this.EstadoDocumento.Id, FechaIncio.Date, FechaCorte.Date));
            }
            String cadena = " Registros encontrados";
            if (this.LHabilitantes.Count == 1)
                cadena = " Registro encontrado";
            this.BarraEstado = this.LHabilitantes.Count + cadena;
        }

        private void AccionImprimir()
        {
            Cliente.Depositos.ConceptosDep conDep = new Cliente.Depositos.ConceptosDep();
            ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
            if (iajustes.Vista.ShowDialog() == true)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                System.Collections.Generic.IEnumerable<Cliente.Referencia.ConceptosDocumentoDto> docs = conDep.DocumentosPorConceptoEstado((int)this.Seleccionado.Concepto, 0);
                foreach (Cliente.Referencia.ConceptosDocumentoDto d in docs)
                {
                    if (d.Definicion != null && d.Definicion.Length > 0)
                    {
                        Telerik.Reporting.TypeReportSource rs = new Telerik.Reporting.TypeReportSource();
                        rs.TypeName = d.Definicion;
                        int con = Convert.ToInt32(this.Seleccionado.Contribuyentes.Replace("[", "").Replace("]", ""));
                        IEnumerable<Telerik.Reporting.Parameter> prs = this.TraerParametrosPorConceptoContribuyente((int)this.Seleccionado.Concepto, con);
                        if (prs != null && prs.Count() > 0)
                        {
                            rs.Parameters.AddRange(prs);
                            reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                            Cliente.Depositos.RecaudacionesDep rd = new Cliente.Depositos.RecaudacionesDep();
                            rd.DocumentoMarcarPorPlanilla(this.Seleccionado.Id);
                        }
                        else
                        {
                            Utiles.CuadroMensajes.Aceptar("Certificado de avaluos", "No se puede imprimir", "El contribuyente no posee predios para emitir el certificado", "", TaskDialogInterop.VistaTaskDialogIcon.Error);
                        }
                    }
                }
            }
            this.BarraEstado = "Se ha impreso: " + this.Seleccionado.ConceptoNav.Denominacion + " No. " + Convert.ToString(this.Seleccionado.Numero) + " de " + this.Seleccionado.ContribuyentesCadena;
            this.AccionConsultar();
        }

        private void AccionDesmarcar()
        {
            TaskDialogInterop.TaskDialogResult res = Utiles.CuadroMensajes.Preguntar("Documentos habilitantes", "Confirmar operacion", "¿Seguro de desmarcar el documento seleccionado?");
            if (res.CustomButtonResult == 0)
                recaudaDep.DocumentoDesmarcarPorPlanilla(this.Seleccionado.Id);
            if (this.Seleccionado != null)
                this.BarraEstado = "Se ha desmarcado: " + this.Seleccionado.ConceptoNav.Denominacion + " No. " + Convert.ToString(this.Seleccionado.Numero) + " de " + this.Seleccionado.ContribuyentesCadena;
            this.AccionConsultar();
        }

        private void AccionRegresar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private void AccionCodebarBuscar()
        {
            this.BarraEstado = "Esta opcion requiere un dispositivo lector de codigo de barras";
        }
    
        private IEnumerable<Telerik.Reporting.Parameter> TraerParametrosPorConceptoContribuyente(int pConcepto, int pContribuyente)
        {
            List<Telerik.Reporting.Parameter> lParams = new List<Telerik.Reporting.Parameter>();
            switch (pConcepto)
            {
                case 10: 
                    {                        
                        int id = 0;
                        Cliente.Depositos.CatastrosDep cd = new Cliente.Depositos.CatastrosDep();
                        IEnumerable<Cliente.Referencia.PredioBaseDto> ps = cd.PrediosPorContribuyente(pContribuyente, 0, 9, false);
                        if (ps.Count() > 1)
                        {
                            if (this.Seleccionado.ContribuyenteNav == null)
                            {
                                Cliente.Depositos.ContribuyentesDep condp = new Cliente.Depositos.ContribuyentesDep();
                                this.Seleccionado.ContribuyenteNav = condp.ContribuyentePorId(pContribuyente);
                            }                                
                            ModeloVista.Catastros.PredioSeleccionarVM sp = new Catastros.PredioSeleccionarVM(this.Seleccionado.ContribuyenteNav, ps);
                            if (sp.Vista.DialogResult == true)
                                id = sp.Seleccionado.Id;
                        }
                        else
                        {
                            if (ps.Count() == 1)
                            {
                                id = ps.ElementAt(0).Id;
                            }
                        }
                        if (id > 0)
                        {
                            Cliente.Depositos.ContribuyentesDep condep = new Cliente.Depositos.ContribuyentesDep();
                            int cid = Convert.ToInt32(this.Seleccionado.Contribuyentes.Replace("[", "").Replace("]", ""));
                            Cliente.Referencia.ContribuyenteDto con = condep.ContribuyentePorId(cid);
                            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
                            lParams.Add(new Telerik.Reporting.Parameter("pId", id));
                            lParams.Add(new Telerik.Reporting.Parameter("pTitulo", this.Seleccionado.Id));

                            lParams.Add(new Telerik.Reporting.Parameter("pPropietario", this.Seleccionado.ContribuyentesCadena));
                            if (con.Cedula != null && con.Cedula.Length > 0)
                                lParams.Add(new Telerik.Reporting.Parameter("pCedula", con.Cedula));
                            else
                                lParams.Add(new Telerik.Reporting.Parameter("pCedula", "N/D"));
                            lParams.Add(new Telerik.Reporting.Parameter("pYear", DateTime.Today.Year));
                            lParams.Add(new Telerik.Reporting.Parameter("pInstitucion", td.NombreEmpresa));
                            string sjefe = "";
                            Cliente.Referencia.TablaClaveDto jefe = td.ClavesPorTablaCve(2, 8).FirstOrDefault();
                            if (jefe != null)
                                sjefe = jefe.Denominacion;
                            lParams.Add(new Telerik.Reporting.Parameter("pJefe", sjefe));
                        }
                        
                        break; 
                    }
                case 11: 
                    {
                        Cliente.Depositos.ContribuyentesDep condep = new Cliente.Depositos.ContribuyentesDep();
                        int cid = Convert.ToInt32(this.Seleccionado.Contribuyentes.Replace("[", "").Replace("]", ""));
                        Cliente.Referencia.ContribuyenteDto con = condep.ContribuyentePorId(cid);
                        Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
                        String sjefe = String.Empty;
                        lParams.Add(new Telerik.Reporting.Parameter("pId", this.Seleccionado.Id));
                        lParams.Add(new Telerik.Reporting.Parameter("pNombres", this.Seleccionado.ContribuyentesCadena));
                        if (con.Cedula != null && con.Cedula.Length > 0)
                            lParams.Add(new Telerik.Reporting.Parameter("pCedula", con.Cedula));
                        else
                            lParams.Add(new Telerik.Reporting.Parameter("pCedula", "N/D"));
                        Cliente.Referencia.TablaClaveDto jefe = td.ClavesPorTablaCve(2, 9).FirstOrDefault();
                        if (jefe != null)
                            sjefe = jefe.Denominacion;
                        lParams.Add(new Telerik.Reporting.Parameter("pTesorero", sjefe));
                        break; 
                    }
                case 12: 
                    {
                        Cliente.Depositos.ContribuyentesDep condep = new Cliente.Depositos.ContribuyentesDep();
                        int cid = Convert.ToInt32(this.Seleccionado.Contribuyentes.Replace("[", "").Replace("]", ""));
                        Cliente.Referencia.ContribuyenteDto con = condep.ContribuyentePorId(cid);
                        int id = 0;
                        Cliente.Depositos.CatastrosDep cd = new Cliente.Depositos.CatastrosDep();
                        IEnumerable<Cliente.Referencia.PredioBaseDto> ps = cd.PrediosPorContribuyente(pContribuyente, 0, 9, false);
                        if (ps.Count() > 1)
                        {
                            if (this.Seleccionado.ContribuyenteNav == null)
                            {
                                Cliente.Depositos.ContribuyentesDep condp = new Cliente.Depositos.ContribuyentesDep();
                                this.Seleccionado.ContribuyenteNav = condp.ContribuyentePorId(pContribuyente);
                            } 
                            ModeloVista.Catastros.PredioSeleccionarVM sp = new Catastros.PredioSeleccionarVM(this.Seleccionado.ContribuyenteNav, ps);
                            if (sp.Vista.DialogResult == true)
                                id = sp.Seleccionado.Id;
                        }
                        else
                        {
                            if (ps.Count() == 1)
                            {
                                id = ps.ElementAt(0).Id;
                            }
                        }
                        if (id > 0)
                        {
                            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
                            lParams.Add(new Telerik.Reporting.Parameter("pId", id));
                            lParams.Add(new Telerik.Reporting.Parameter("pTitulo", this.Seleccionado.Id));

                            lParams.Add(new Telerik.Reporting.Parameter("pPropietario", this.Seleccionado.ContribuyentesCadena));
                            if (con.Cedula != null && con.Cedula.Length > 0)
                                lParams.Add(new Telerik.Reporting.Parameter("pCedula", con.Cedula));
                            else
                                lParams.Add(new Telerik.Reporting.Parameter("pCedula", "N/D"));
                            lParams.Add(new Telerik.Reporting.Parameter("pYear", DateTime.Today.Year));
                            lParams.Add(new Telerik.Reporting.Parameter("pInstitucion", td.NombreEmpresa));
                            string sjefe = "";
                            Cliente.Referencia.TablaClaveDto jefe = td.ClavesPorTablaCve(2, 8).FirstOrDefault();
                            if (jefe != null)
                                sjefe = jefe.Denominacion;
                            lParams.Add(new Telerik.Reporting.Parameter("pJefe", sjefe));
                        }
                        break; 
                    }
            }
            return lParams;
        }
    }
}
