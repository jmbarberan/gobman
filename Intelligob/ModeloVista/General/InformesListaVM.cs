using System.Collections.Generic;
using Intelligob.Cliente.Modelos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Telerik.Reporting;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class InformesListaVM : BaseMV<IPagina>
    {
        private readonly Cliente.Depositos.RepRecaudacionesDep depRec = new Cliente.Depositos.RepRecaudacionesDep();
        private ObservableCollection<Informe> lInformes = new ObservableCollection<Informe>();

        public ObservableCollection<Informe> LInformes
        {
            get { return this.lInformes; }
            set { this.lInformes = value; OnPropertyChanged("LInformes"); }
        }

        private Informe seleccionado;
        public Informe Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public ICommand CmdConsultar
        { get; internal set; }

        public ICommand CmdImprimir
        { get; internal set; }

        /*public ICommand CmdAbrir
        { get; internal set; }*/

        public ICommand CmdRegresar
        { get; internal set; }

        public ICommand CmdAdelante { get; internal set; }        

        public InformesListaVM(int modulo) : this(modulo, new Intelligob.Escritorio.Vistas.InformesLista()) { }

        public InformesListaVM(int modulo, IPagina vista) : base(vista) 
        {
            this.CargarReportes(modulo);
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarPuede());
            this.CmdConsultar = new Comandos.ComandoDelegado((o) => ConsultarAccion(), (o) => InformeSeleccionado());
            this.CmdImprimir = new Comandos.ComandoDelegado((o) => ImprimirAccion(), (o) => InformeSeleccionado());
            //this.CmdAbrir = new Comandos.ComandoDelegado((o) => AbrirAccion());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AccionAdelantar(), (o) => this.PuedeAdelantar());
        }

        private void CargarReportes(int mod)
        {
            int p = 0;
            Intelligob.Cliente.Referencia.TablaClaveDto tr = Intelligob.Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 10).FirstOrDefault();
            if (tr != null && tr.Superior != null && tr.Superior > 0)
            {
                p = (int)tr.Superior;
            }

            int pp = 0;
            Intelligob.Cliente.Referencia.TablaClaveDto trp = Intelligob.Cliente.ModeloCache.Instance.McClaves.Where(o => o.Tabla == 2 && o.Clave == 11).FirstOrDefault();
            if (trp != null && trp.Superior != null && trp.Superior > 0)
            {
                pp = (int)trp.Superior;
            }

            Intelligob.Reportes.ManejadorInformes repMan = new Intelligob.Reportes.ManejadorInformes();
            LInformes = new ObservableCollection<Informe>(repMan.TraerInformesPorModulo(mod));
            IEnumerable<Cliente.Referencia.ReporteDto> reps = depRec.ReportesPorModuloEstado(mod, 0);
            foreach (Cliente.Referencia.ReporteDto r in reps)
            {
                if (r.Id != p && r.Id != pp)
                {
                    LInformes.Add(new Cliente.Modelos.Informe(r.Denominacion
                     , ""
                     , null
                     , r.Id
                     , "../Imagenes/infonuevo.png"
                     , r.Definicion, null));
                }
            }
        }

        private Boolean RegresarPuede()
        {
            return Navegador.NavigationService.CanGoBack;
        }

        private void RegresarAccion()
        {
            Navegador.NavigationService.GoBack();
        }

        private bool PuedeAdelantar()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private void AccionAdelantar()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private bool InformeSeleccionado()
        {
            return this.Seleccionado != null;
        }

        /*private void AbrirAccion()
        {
            // No se implementara
        }*/

        private void ConsultarAccion()
        {
            if (Seleccionado.Alternativos != null && Seleccionado.Alternativos.Count > 0)
            {
                string cls = Seleccionado.Alternativos[0].Codigo;
                if (Seleccionado.Seleccionado != null)
                    cls = Seleccionado.Seleccionado.Codigo;
                Telerik.Reporting.TypeReportSource rs = new Telerik.Reporting.TypeReportSource();
                rs.TypeName = cls;
                ReporteVisorVM rep = new ReporteVisorVM(rs);
                Navegador.NavigationService.Navigate(rep.Vista);
            }
            else
            {
                if (Seleccionado.Clase.Length > 0)
                {
                    Telerik.Reporting.TypeReportSource rs = new Telerik.Reporting.TypeReportSource();
                    rs.TypeName = Seleccionado.Clase;
                    ReporteVisorVM rep = new ReporteVisorVM(rs);
                    Navegador.NavigationService.Navigate(rep.Vista);
                }
                else
                {
                    if (Seleccionado.Definicion.Length > 0)
                    {
                        //var con = new Reportes.ManejadorConexion("data source=servidor;initial catalog=unigob;user id=sysdba;password=masterkey");
                        //ReportSource rs = new InstanceReportSource();
                        Report report;
                        Telerik.Reporting.XmlSerialization.ReportXmlSerializer xmlSerializer = new Telerik.Reporting.XmlSerialization.ReportXmlSerializer();
                        Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                        rsOrigen.Xml = Seleccionado.Definicion;
                        //rs = con.UpdateReportSource(rsOrigen);
                        var settings = new System.Xml.XmlReaderSettings();
                        settings.IgnoreWhitespace = true;
                        var textReader = new System.IO.StringReader(rsOrigen.Xml);
                        using (var xmlReader = System.Xml.XmlReader.Create(textReader, settings))
                        {
                            //var xmlSerializer = new Telerik.Reporting.XmlSerialization.ReportXmlSerializer();
                            report = (Telerik.Reporting.Report)xmlSerializer.Deserialize(xmlReader);
                            this.SetConnectionString(report);
                        }
                        //reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                        ReporteVisorVM rep = new ReporteVisorVM(rsOrigen);
                        Navegador.NavigationService.Navigate(rep.Vista);
                    }
                }
            }
        }

        private void ImprimirAccion()
        {
            ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
            if (iajustes.Vista.ShowDialog() == true)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                if (Seleccionado.Alternativos != null && Seleccionado.Alternativos.Count > 0)
                {
                    string cls = Seleccionado.Alternativos[0].Codigo;
                    if (Seleccionado.Seleccionado != null)
                        cls = Seleccionado.Seleccionado.Codigo;
                    Telerik.Reporting.TypeReportSource rs = new Telerik.Reporting.TypeReportSource();
                    rs.TypeName = cls;
                    reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                }
                else
                {                    
                    if (Seleccionado.Clase.Length > 0)
                    {
                        Telerik.Reporting.TypeReportSource rs = new Telerik.Reporting.TypeReportSource();
                        rs.TypeName = Seleccionado.Clase;
                        reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                    }
                    else
                    {
                        if (Seleccionado.Definicion.Length > 0)
                        {
                            var con = new Intelligob.Reportes.ReportConnectionStringManager(Utilerias.ConexionDatos.CadenaConexion);                            
                            Telerik.Reporting.XmlReportSource rsOrigen = new Telerik.Reporting.XmlReportSource();
                            con.UpdateReportSource(rsOrigen);
                            rsOrigen.Xml = Seleccionado.Definicion;
                            var rs = con.UpdateReportSource(rsOrigen);
                            reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
                        }
                    }
                }
            }
        }

        public void SetConnectionString(ReportItemBase reportItemBase)
        {
            if (reportItemBase.Items.Count < 1)
                return;

            if (reportItemBase is Report)
            {
                var report = (Report)reportItemBase;

                if (report.DataSource is SqlDataSource)
                {
                    var sqlDataSource = (SqlDataSource)report.DataSource;
                    sqlDataSource.ProviderName = Utilerias.ConexionDatos.Proveedor;
                    sqlDataSource.ConnectionString = Utilerias.ConexionDatos.CadenaConexion;
                }
                foreach (var parameter in report.ReportParameters)
                {
                    if (parameter.AvailableValues.DataSource is SqlDataSource)
                    {
                        var sqlDataSource = (SqlDataSource)parameter.AvailableValues.DataSource;
                        sqlDataSource.ProviderName = Utilerias.ConexionDatos.Proveedor;
                        sqlDataSource.ConnectionString = Utilerias.ConexionDatos.CadenaConexion;
                    }
                }
            }

            foreach (var item in reportItemBase.Items)
            {
                //recursively set the connection string to the items from the Items collection
                /*SetConnectionString(item);

                //set the drillthrough report connection strings
                var drillThroughAction = item.Action as NavigateToReportAction;
                if (null != drillThroughAction)
                {
                    var updatedReportInstance = this.UpdateReportSource(drillThroughAction.ReportSource);
                    drillThroughAction.ReportSource = updatedReportInstance;
                }

                if (item is SubReport)
                {
                    var subReport = (SubReport)item;
                    subReport.ReportSource = this.UpdateReportSource(subReport.ReportSource);
                    continue;
                }*/

                //Covers all data items(Crosstab, Table, List, Graph, Map and Chart)
                if (item is DataItem)
                {
                    var dataItem = (DataItem)item;
                    if (dataItem.DataSource is SqlDataSource)
                    {
                        var sqlDataSource = (SqlDataSource)dataItem.DataSource;
                        //sqlDataSource.ConnectionString = connectionString;
                        sqlDataSource.ConnectionString = Utilerias.ConexionDatos.CadenaConexion;
                        continue;
                    }
                }
            }
        }
    }
}
