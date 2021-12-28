using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using Telerik.Windows.Controls.Pivot.Export;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class RepEmisionesSaldoVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>, Vistas.Interfaces.IModeloExportador
    {
        private readonly Cliente.Depositos.EmisionesDep emisionesDep = new Cliente.Depositos.EmisionesDep();
        private readonly Cliente.Depositos.ConceptosDep conceptosDep = new Cliente.Depositos.ConceptosDep();

        private bool consultaOcupada = false;
        public bool ConsultaOcupada
        {
            get { return this.consultaOcupada; }
            set { this.consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        private bool excluirPosteriores = false;
        public bool ExcluirPosteriores
        {
            get { return this.excluirPosteriores; }
            set { this.excluirPosteriores = value; OnPropertyChanged("ExcluirPosteriores"); }
        }

        private DateTime fechaCorte = DateTime.Today;
        public DateTime FechaCorte
        {
            get { return this.fechaCorte; }
            set { this.fechaCorte = value; OnPropertyChanged("FechaCorte"); }
        }

        private ObservableCollection<Cliente.Referencia.ConceptoDto> lConceptos = new ObservableCollection<Cliente.Referencia.ConceptoDto>();
        public ObservableCollection<Cliente.Referencia.ConceptoDto> LConceptos
        {
            get { return this.lConceptos; }
            set { this.lConceptos = value; OnPropertyChanged("LConceptos"); }
        }

        private Cliente.Referencia.ConceptoDto conceptoSeleccionado;
        public Cliente.Referencia.ConceptoDto ConceptoSeleccionado
        {
            get { return this.conceptoSeleccionado; }
            set { this.conceptoSeleccionado = value; OnPropertyChanged("ConceptoSeleccionado"); }
        }

        private ObservableCollection<Cliente.Referencia.EmisionesSaldosItem> lConsulta = new ObservableCollection<Cliente.Referencia.EmisionesSaldosItem>();
        public ObservableCollection<Cliente.Referencia.EmisionesSaldosItem> LConsulta
        {
            get { return this.lConsulta; }
            set { this.lConsulta = value; OnPropertyChanged("LConsulta"); }
        }

        private string barraEstado = "Listo";
        public string BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }        

        public System.Windows.Input.ICommand CmdConsultar
        { get; internal set; }

        private Comandos.ComandoExportar exportCommand = null;
        public Comandos.ComandoExportar CmdExportar
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
        public System.Windows.Input.ICommand CmdRegresar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdAdelante
        { get; internal set; }

        public System.Windows.Input.ICommand CmdImprimir
        { get; internal set; }

        public RepEmisionesSaldoVM() : base(new Escritorio.Vistas.Emisiones.RepEmisionesSaldo())
        {
            this.CmdExportar = new Comandos.ComandoExportar(this, (o) => this.ExportarHablita());
            this.CmdConsultar = new Comandos.ComandoDelegado((o) => ConsultaAccion(), (o) => ConsultaHabilita());
            this.CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            this.CmdAdelante = new Comandos.ComandoDelegado((o) => this.AdelantarAccion(), (o) => this.AdelantarHabilita());
            this.CmdImprimir = new Comandos.ComandoDelegado((o) => this.ImprimirAccion(), (o) => this.ImprimirHabilita());
            Cliente.Referencia.ConceptoDto ct = new Cliente.Referencia.ConceptoDto();
            ct.Id = 0;
            ct.Denominacion = "CONSULTAR TODOS";
            ct.Periodo = 1;
            ct.Estado = 0;
            this.LConceptos = new ObservableCollection<Cliente.Referencia.ConceptoDto>(this.conceptosDep.ConceptosPorCarpetaCatastral());
            this.LConceptos.Add(ct);
            this.conceptoSeleccionado = ct;
        }

        #region Consulta Asincronica

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;            
            this.LConsulta = new ObservableCollection<Cliente.Referencia.EmisionesSaldosItem>((IEnumerable<Cliente.Referencia.EmisionesSaldosItem>)e.Result);
            this.ConsultaOcupada = false;
            this.BarraEstado = LConsulta.Count.ToString() + " Registros consultados";            
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        private IEnumerable<Cliente.Referencia.EmisionesSaldosItem> ConsultaAsincronica()
        {
            int f = 0;
            if (this.ConceptoSeleccionado != null && this.ConceptoSeleccionado.Id > 0)
                f = this.ConceptoSeleccionado.Id;
            return this.emisionesDep.EmisionesSaldoFecha(f, this.FechaCorte, this.ExcluirPosteriores);
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

        private bool ExportarHablita()
        {
            return !this.ConsultaOcupada && this.LConsulta != null && this.LConsulta.Count > 0;
        }

        private bool RegresarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool AdelantarHabilita()
        {
            return Escritorio.Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        private void RegresarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoBack();
        }

        private void AdelantarAccion()
        {
            Escritorio.Vistas.General.Navegador.NavigationService.GoForward();
        }

        private Boolean ImprimirHabilita()
        {
            return !this.ConsultaOcupada && this.LConsulta != null && this.LConsulta.Count > 0;
        }

        private void ImprimirAccion()
        {
            ImpresionAjustesVM iajustes = new ImpresionAjustesVM();
            if (iajustes.Vista.ShowDialog() == true)
            {
                Telerik.Reporting.Processing.ReportProcessor reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                Telerik.Reporting.InstanceReportSource rs = new Telerik.Reporting.InstanceReportSource();
                Reportes.Emisiones.EmisionesSaldos rf = new Reportes.Emisiones.EmisionesSaldos();
                rs.ReportDocument = rf;

                int c = 0;
                if (this.ConceptoSeleccionado != null && this.ConceptoSeleccionado.Id > 0)
                    c = this.ConceptoSeleccionado.Id;
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaCorte", this.FechaCorte));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pConcepto", c));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pEmisionesNuevas", this.ExcluirPosteriores));

                reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
            }
            
        }

        #region exportar a Excel

        private Workbook GenerateWorkbook(object param)
        {
            var pivot = (Telerik.Windows.Controls.RadPivotGrid)param;
            var export = pivot.GenerateExport();
            Workbook workbook = new Workbook();
            workbook.History.IsEnabled = false;
            workbook.SuspendLayoutUpdate();

            Worksheet worksheet = workbook.Worksheets.Add();

            int rowCount = export.RowCount;
            int columnCount = export.ColumnCount;

            var allCells = worksheet.Cells[0, 0, rowCount - 1, columnCount - 1];
            allCells.SetFontFamily(new ThemableFontFamily(pivot.FontFamily));
            allCells.SetFontSize(12);
            allCells.SetFill(GenerateFill(pivot.Background));

            foreach (var cellInfo in export.Cells)
            {
                int rowStartIndex = cellInfo.Row;
                int rowEndIndex = rowStartIndex + cellInfo.RowSpan - 1;
                int columnStartIndex = cellInfo.Column;
                int columnEndIndex = columnStartIndex + cellInfo.ColumnSpan - 1;

                CellSelection cellSelection = worksheet.Cells[rowStartIndex, columnStartIndex];

                var value = cellInfo.Value;
                if (value != null)
                {
                    cellSelection.SetValue(Convert.ToString(value));
                    cellSelection.SetVerticalAlignment(RadVerticalAlignment.Center);
                    cellSelection.SetHorizontalAlignment(GetHorizontalAlignment(cellInfo.TextAlignment));
                    int indent = cellInfo.Indent;
                    if (indent > 0)
                    {
                        cellSelection.SetIndent(indent);
                    }
                }

                cellSelection = worksheet.Cells[rowStartIndex, columnStartIndex, rowEndIndex, columnEndIndex];

                SetCellProperties(cellInfo, cellSelection);
            }

            for (int i = 0; i < columnCount; i++)
            {
                var columnSelection = worksheet.Columns[i];
                columnSelection.AutoFitWidth();

                //NOTE: workaround for incorrect autofit.
                var newWidth = worksheet.Columns[i].GetWidth().Value.Value + 15;
                columnSelection.SetWidth(new ColumnWidth(newWidth, false));
            }

            workbook.ResumeLayoutUpdate();

            return workbook;
        }

        private RadHorizontalAlignment GetHorizontalAlignment(TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Center:
                    return RadHorizontalAlignment.Center;

                case TextAlignment.Left:
                    return RadHorizontalAlignment.Left;

                case TextAlignment.Right:
                    return RadHorizontalAlignment.Right;

                case TextAlignment.Justify:
                default:
                    return RadHorizontalAlignment.Justify;
            }
        }

        private static void SetCellProperties(PivotExportCellInfo cellInfo, CellSelection cellSelection)
        {
            var fill = GenerateFill(cellInfo.Background);
            if (fill != null)
            {
                cellSelection.SetFill(fill);
            }

            SolidColorBrush solidBrush = cellInfo.Foreground as SolidColorBrush;
            if (solidBrush != null)
            {
                cellSelection.SetForeColor(new ThemableColor(solidBrush.Color));
            }

            if (cellInfo.FontWeight.HasValue && cellInfo.FontWeight.Value != FontWeights.Normal)
            {
                cellSelection.SetIsBold(true);
            }

            SolidColorBrush solidBorderBrush = cellInfo.BorderBrush as SolidColorBrush;
            if (solidBorderBrush != null && cellInfo.BorderThickness.HasValue)
            {
                var borderThickness = cellInfo.BorderThickness.Value;
                var color = new ThemableColor(solidBorderBrush.Color);
                var leftBorder = new CellBorder(GetBorderStyle(borderThickness.Left), color);
                var topBorder = new CellBorder(GetBorderStyle(borderThickness.Top), color);
                var rightBorder = new CellBorder(GetBorderStyle(borderThickness.Right), color);
                var bottomBorder = new CellBorder(GetBorderStyle(borderThickness.Bottom), color);
                var insideBorder = cellInfo.Background != null ? new CellBorder(CellBorderStyle.None, color) : null;
                cellSelection.SetBorders(new CellBorders(leftBorder, topBorder, rightBorder, bottomBorder, insideBorder, insideBorder, null, null));
            }
        }

        private static CellBorderStyle GetBorderStyle(double thickness)
        {
            if (thickness < 1)
            {
                return CellBorderStyle.None;
            }
            else if (thickness < 2)
            {
                return CellBorderStyle.Thin;
            }
            else if (thickness < 3)
            {
                return CellBorderStyle.Medium;
            }
            else
            {
                return CellBorderStyle.Thick;
            }
        }

        private static IFill GenerateFill(Brush brush)
        {
            if (brush != null)
            {
                SolidColorBrush solidBrush = brush as SolidColorBrush;
                if (solidBrush != null)
                {
                    return PatternFill.CreateSolidFill(solidBrush.Color);
                }
            }

            return null;
        }
        
        #endregion

        public void Exportar(object parametro)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "xlsx";
            dialog.Filter = "Hoja de calculos Excel (xlsx) | *.xlsx |Todos los archivos (*.*) | *.*";


            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    var stArchivo = dialog.OpenFile();
                    var workbook = GenerateWorkbook(parametro);
                    var prov = new XlsxFormatProvider();
                    prov.Export(workbook, stArchivo);
                    this.BarraEstado = LConsulta.Count.ToString() + " Consulta Exportada";
                    Utiles.CuadroMensajes.Aceptar("Informacion", "Proceso completado", "La consulta ha sido exportada exitosamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                }
                catch (System.IO.IOException ex)
                {
                    Utiles.CuadroMensajes.Alertar("Error", "No se puede exportar", ex.Message, "");
                }
            }
        }
    }
}
