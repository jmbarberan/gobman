using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class ComandoExportar : ICommand
    {
        readonly Predicate<object> canExecute;
        private readonly PrediosConsultaVM model;

        public ComandoExportar(PrediosConsultaVM model, Predicate<object> pcanExecute)
        {
            this.model = model;
            this.canExecute = pcanExecute;
        }        

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.model.ExportarAccion(parameter);
        }
    }

    public class ComandoImprimir : ICommand
    {
        readonly Predicate<object> canExecute;
        private readonly PrediosConsultaVM model;

        public ComandoImprimir(PrediosConsultaVM model, Predicate<object> pcanExcute)
        {
            this.model = model;
            this.canExecute = pcanExcute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            this.model.ImprimirAccion(parameter);
        }
    }

    public class PrediosConsultaVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.CatastrosDep catastroDep = new Cliente.Depositos.CatastrosDep();
        private ObservableCollection<Cliente.Referencia.RepPredioDto> lPredios;
        public ObservableCollection<Cliente.Referencia.RepPredioDto> LPredios
        {
            get { return this.lPredios; }
            set { this.lPredios = value; OnPropertyChanged("LPredios"); }
        }

        private String barraEstado = String.Empty;
        public String BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        private bool consultaOcupada = false;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        public string BusyContent
        {
            get { return "Consultando catastro predial"; }
        }

        private int tipoConsulta = 0;
        public int TipoConsulta
        {
            get { return this.tipoConsulta; }
            set { this.tipoConsulta = value; OnPropertyChanged("TipoConsulta"); OnPropertyChanged("ConsultaRurales"); }
        }
        public Boolean ConsultaRurales
        {
            get { return this.TipoConsulta == 1; }
        }
        
        private bool mostrarEliminados = false;
        public bool MostrarEliminados
        {
            get { return this.mostrarEliminados; }
            set { this.mostrarEliminados = value; OnPropertyChanged("MostrarEliminados"); }
        }

        public System.Windows.Input.ICommand CmdConsultar { get; internal set; }
        public System.Windows.Input.ICommand CmdImprimir { get; internal set; }
        public System.Windows.Input.ICommand CmdRegresar { get; internal set; }
        public System.Windows.Input.ICommand CmdAvanzar { get; internal set; }

        private ComandoExportar exportCommand = null;
        public ComandoExportar CmdExportar
        {
            get
            {
                return this.exportCommand;
            }
            set
            {
                if (this.exportCommand != value)
                {
                    this.exportCommand = value;
                    OnPropertyChanged("CmdExportar");
                }
            }
        }

        public PrediosConsultaVM() : base(new Escritorio.Vistas.Catastros.PrediosConsulta())
        {
            this.CmdConsultar = new Comandos.ComandoDelegado((o) => ConsultaAccion(), (o) => ConsultaHabilita());
            this.CmdExportar = new ComandoExportar(this, (o) => ExportarHabilita());
            //this.CmdImprimir = new ComandoImprimir(this, (o) => ImprimirHabilita());
            //this.CmdImprimir = new Comandos.ComandoDelegado((o) => ImprimirAccion(object parameter), (o) => ImprimirHabilita());
            this.CmdAvanzar = new Comandos.ComandoDelegado((o) => AvanzarAccion(), (o) => AvanzarHabilita());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            this.LPredios = new ObservableCollection<Cliente.Referencia.RepPredioDto>((IEnumerable<Cliente.Referencia.RepPredioDto>)e.Result);
            this.ConsultaOcupada = false;            
            this.BarraEstado = LPredios.Count.ToString() + " Predios encontrados";
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        private IEnumerable<Cliente.Referencia.RepPredioDto> ConsultaAsincronica()
        {
            int pEstado = 0;
            if (this.MostrarEliminados)
                pEstado = 9;
            return this.catastroDep.PrediosConsultaPorTipoEstado(this.TipoConsulta, pEstado);
        }

        #region Exportar

        private void ElementExporting(object sender, Telerik.Windows.Controls.GridViewElementExportingEventArgs e)
        {
            /*if (e.Element == ExportElement.HeaderRow || e.Element == ExportElement.FooterRow
                || e.Element == ExportElement.GroupFooterRow)
            {
                e.Background = Colors.LightGray;
                e.Foreground = Colors.Black;
                e.FontSize = 20;
                e.FontWeight = System.Windows.FontWeights.Bold;
            }
            else if (e.Element == ExportElement.Row)
            {
                e.Background = Colors.White;
                e.Foreground = Colors.Black;
            }*/           
        }

        #endregion

        #region Imprimir

        private static RadRichTextBox CreateRadRichTextBox(RadGridView grid)
        {
            return new RadRichTextBox()
            {
                IsReadOnly = true,
                LayoutMode = Telerik.Windows.Documents.Model.DocumentLayoutMode.Paged,
                IsSelectionEnabled = false,
                IsSpellCheckingEnabled = false,
                Document = CreateDocument(grid)
            };
        }

        private static Telerik.Windows.Documents.Model.RadDocument CreateDocument(RadGridView grid)
        {
            Telerik.Windows.Documents.Model.RadDocument document = null;

            using (var stream = new MemoryStream())
            {
                EventHandler<GridViewElementExportingEventArgs> elementExporting = (s, e) =>
                {
                    /*if (e.Element == ExportElement.Table)
                    {
                        e.Attributes["border"] = "0";
                    }
                    else if (e.Element == ExportElement.HeaderRow)
                    {
                        e.Styles.Add("background-color", Colors.Gray.ToString().Remove(1, 2));
                        e.Styles.Add("foreground-color", Colors.White.ToString().Remove(1, 2));
                    }
                    else if (e.Element == ExportElement.GroupHeaderRow)
                    {
                        e.Styles.Add("background-color", Colors.Silver.ToString().Remove(1, 2));
                    }
                    else if (e.Element == ExportElement.Row)
                    {
                        e.Styles.Add("background-color", Colors.White.ToString().Remove(1, 2));
                    }*/
                };

                grid.ElementExporting += elementExporting;

                grid.Export(stream, new GridViewExportOptions()
                {
                    Format = Telerik.Windows.Controls.ExportFormat.Html,
                    ShowColumnFooters = grid.ShowColumnFooters,
                    ShowColumnHeaders = grid.ShowColumnHeaders,
                    ShowGroupFooters = grid.ShowGroupFooters
                });

                grid.ElementExporting -= elementExporting;

                stream.Position = 0;

                document = new Telerik.Windows.Documents.FormatProviders.Html.HtmlFormatProvider().Import(stream);
            }

            return document;
        }

        private void Imprimir(RadGridView grid)
        {
            var rtb = CreateRadRichTextBox(grid);
            /*var window = new RadWindow() { Height = 0, Width = 0, Opacity = 0, Content = rtb };
            rtb.PrintCompleted += (s, e) => { window.Close(); };
            window.Show();*/

            rtb.Print("CatastroPredial", Telerik.Windows.Documents.UI.PrintMode.Native);
        }

        #endregion

        private bool ConsultaHabilita()
        {
            return !this.ConsultaOcupada;
        }

        private void ConsultaAccion()
        {
            this.ConsultaOcupada = true;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(); 
        }
        
        private bool ExportarHabilita()
        {
            return this.LPredios != null && this.LPredios.Count > 0 && !this.ConsultaOcupada;
        }

        public void ExportarAccion(Object parameter)
        {
            var grid = (RadGridView) parameter;
            if (grid != null)
            {
                grid.ElementExporting -= this.ElementExporting;
                grid.ElementExporting += this.ElementExporting;
                var format = ExportFormat.Html;
                string extension = "xls";
                var dialog = new SaveFileDialog();
                dialog.DefaultExt = extension;
                dialog.Filter = String.Format("Archivos de Excel (*.{0})|*.{0}|All files (*.*)|*.*", extension);
                dialog.FilterIndex = 1;
                DialogResult res = dialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    using (var stream = dialog.OpenFile())
                    {
                        var exportOptions = new GridViewExportOptions();
                        exportOptions.Format = format;
                        exportOptions.ShowColumnFooters = true;
                        exportOptions.ShowColumnHeaders = true;
                        exportOptions.ShowGroupFooters = true;

                        grid.Export(stream, exportOptions);
                    }
                    this.BarraEstado = "Se ha exportado la consulta a " + dialog.FileName;
                }
            }
        }

        private bool ImprimirHabilita()
        {
            return this.LPredios != null && this.LPredios.Count > 0 && !this.ConsultaOcupada;
        }

        public void ImprimirAccion(Object parameter)
        {
            var grid = parameter as RadGridView;
            
            if (grid != null)
            {
                this.Imprimir(grid);
            }
        }

        private bool AvanzarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private void AvanzarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private bool RegresarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private void RegresarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }
    }
}
