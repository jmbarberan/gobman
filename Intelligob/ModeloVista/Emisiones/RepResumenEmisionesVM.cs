using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class RepResumenEmisionesVM : BaseMV<Escritorio.Vistas.Interfaces.IPagina>, Vistas.Interfaces.IModeloExportador
    {
        private ObservableCollection<Cliente.Referencia.ResumenEmisionesPeriodoItem> lConsulta;
        public ObservableCollection<Cliente.Referencia.ResumenEmisionesPeriodoItem> LConsulta
        {
            get { return lConsulta; }
            set { lConsulta = value; OnPropertyChanged("LConsulta"); }
        }

        private ObservableCollection<Utiles.ElementoSeleccion> lPeriodos = new ObservableCollection<Utiles.ElementoSeleccion>();
        public ObservableCollection<Utiles.ElementoSeleccion> LPeriodos
        {
            get { return this.lPeriodos; }
            set { this.lPeriodos = value; OnPropertyChanged("LPeriodos"); }
        }

        private Utiles.ElementoSeleccion periodo;
        public Utiles.ElementoSeleccion Periodo
        {
            get { return periodo; }
            set { periodo = value; OnPropertyChanged("Periodo"); }
        }

        private int añoConsulta = DateTime.Today.Year;
        public int AñoConsulta
        {
            get { return añoConsulta; }
            set { añoConsulta = value; OnPropertyChanged("AñoConsulta"); }
        }

        private DateTime fechaInicio = DateTime.Today;
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; OnPropertyChanged("FechaInicio"); }
        }

        private DateTime fechaCorte = DateTime.Today;
        public DateTime FechaCorte
        {
            get { return fechaCorte; }
            set { fechaCorte = value; OnPropertyChanged("FechaCorte"); }
        }

        private Boolean mostrarAfectantes = false;
        public Boolean MostrarAfectantes
        {
            get { return mostrarAfectantes; }
            set { mostrarAfectantes = value; OnPropertyChanged("MostrarAfectantes"); }
        }

        private String barraEstado = "Listo";
        public String BarraEstado
        {
            get { return barraEstado; }
            set { barraEstado = value; }
        }

        private Boolean consultaOcupada = false;
        public Boolean ConsultaOcupada
        {
            get { return consultaOcupada; }
            set { consultaOcupada = value; OnPropertyChanged("ConsultaOcupada"); }
        }

        public ICommand CmdConsultar
        { get; internal set; }
        
        public ICommand CmdImprimir
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

        public ICommand CmdAdelantar
        { get; internal set; }

        public ICommand CmdRegresar
        { get; internal set; }

        public RepResumenEmisionesVM() : base(new Escritorio.Vistas.Emisiones.RepResumenEmisiones())
        {
            Utiles.ElementoSeleccion e = new Utiles.ElementoSeleccion(0, "TODOS");
            Periodo = e;
            this.LPeriodos.Add(e);
            this.LPeriodos.Add(new Utiles.ElementoSeleccion(1, "Anual"));
            this.LPeriodos.Add(new Utiles.ElementoSeleccion(2, "Mensual"));
            this.LPeriodos.Add(new Utiles.ElementoSeleccion(3, "Semanal"));
            this.LPeriodos.Add(new Utiles.ElementoSeleccion(4, "Diario"));

            CmdExportar = new Comandos.ComandoExportar(this, (o) => this.ExportarHablita());
            CmdConsultar = new Comandos.ComandoDelegado((o) => ConsultarAccion(), (o) => ConsultarHabilita());
            CmdImprimir = new Comandos.ComandoDelegado((o) => ImprimirAccion(), (o) => ImprimirHabilita());
            CmdRegresar = new Comandos.ComandoDelegado((o) => RegresarAccion(), (o) => RegresarHabilita());
            CmdAdelantar = new Comandos.ComandoDelegado((o) => AdelantarAccion(), (o) => AdelantarHabilita());
        }

        #region Consulta asincronica

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;
            if (this.LConsulta != null && this.LConsulta.Count > 0)
                this.LConsulta.Clear();
            if (e.Result != null)
                this.LConsulta = new ObservableCollection<Cliente.Referencia.ResumenEmisionesPeriodoItem>((IEnumerable<Cliente.Referencia.ResumenEmisionesPeriodoItem>)e.Result);
            this.ConsultaOcupada = false;
            this.BarraEstado = LConsulta.Count.ToString() + " Registros consultados";
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.ConsultaAsincronica();
        }

        private IEnumerable<Cliente.Referencia.ResumenEmisionesPeriodoItem> ConsultaAsincronica()
        {
            Cliente.Depositos.RepRecaudacionesDep d = new Cliente.Depositos.RepRecaudacionesDep();
            return d.ResumenEmisionesPeriodo(AñoConsulta, FechaInicio, FechaCorte, Periodo.Id, MostrarAfectantes);
        } 
        
        #endregion

        private Boolean ConsultarHabilita()
        {
            return !this.ConsultaOcupada;
        }

        private void ConsultarAccion()
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
                Reportes.Emisiones.ResumenEmisionPeriodo rf = new Reportes.Emisiones.ResumenEmisionPeriodo();
                rs.ReportDocument = rf;

                rs.Parameters.Add(new Telerik.Reporting.Parameter("pAño", AñoConsulta));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaInicio", FechaInicio));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pFechaCorte", FechaCorte));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pPeriodo", Periodo.Id));
                rs.Parameters.Add(new Telerik.Reporting.Parameter("pAfectantes", MostrarAfectantes));

                reportProcessor.PrintReport(rs, iajustes.AjustesImpresion);
            }
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

        #region exportar a Excel
        private RadHorizontalAlignment GetHorizontalAlignment(System.Windows.TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case System.Windows.TextAlignment.Center:
                    return RadHorizontalAlignment.Center;

                case System.Windows.TextAlignment.Left:
                    return RadHorizontalAlignment.Left;

                case System.Windows.TextAlignment.Right:
                    return RadHorizontalAlignment.Right;

                case System.Windows.TextAlignment.Justify:
                default:
                    return RadHorizontalAlignment.Justify;
            }
        }

        private static void SetCellProperties(Telerik.Windows.Controls.Pivot.Export.PivotExportCellInfo cellInfo, CellSelection cellSelection)
        {
            var fill = GenerateFill(cellInfo.Background);
            if (fill != null)
            {
                cellSelection.SetFill(fill);
            }

            System.Windows.Media.SolidColorBrush solidBrush = cellInfo.Foreground as System.Windows.Media.SolidColorBrush;
            if (solidBrush != null)
            {
                cellSelection.SetForeColor(new ThemableColor(solidBrush.Color));
            }

            if (cellInfo.FontWeight.HasValue && cellInfo.FontWeight.Value != System.Windows.FontWeights.Normal)
            {
                cellSelection.SetIsBold(true);
            }

            System.Windows.Media.SolidColorBrush solidBorderBrush = cellInfo.BorderBrush as System.Windows.Media.SolidColorBrush;
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

        private static IFill GenerateFill(System.Windows.Media.Brush brush)
        {
            if (brush != null)
            {
                System.Windows.Media.SolidColorBrush solidBrush = brush as System.Windows.Media.SolidColorBrush;
                if (solidBrush != null)
                {
                    return PatternFill.CreateSolidFill(solidBrush.Color);
                }
            }

            return null;
        }

        private static Workbook CrearWorkbook(object parametro)
        {
            var workbook = new Workbook();
            var worksheet = workbook.Worksheets.Add();

            for (var i = 0; i < ((Telerik.Windows.Controls.RadGridView)parametro).Items.Count; i++)
            {
                for (var j = 0; j < ((Telerik.Windows.Controls.RadGridView)parametro).Columns.Count; j++)
                {
                    var boundColumn = ((Telerik.Windows.Controls.RadGridView)parametro).Columns[j] as Telerik.Windows.Controls.GridViewBoundColumnBase;
                    if (boundColumn != null)
                    {
                        worksheet.Cells[i, j].SetValue(string.Format("{0}", boundColumn.GetValueForItem(((Telerik.Windows.Controls.RadGridView)parametro).Items[i])));
                    }
                }
            }

            return workbook;
        }

        #endregion

        public void Exportar(object parametro)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.DefaultExt = "xlsx";
            dialog.Filter = "Hoja de calculos Excel (xlsx) | *.xlsx |Todos los archivos (*.*) | *.*";


            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var stArchivo = dialog.OpenFile();
                    var workbook = CrearWorkbook(parametro);
                    var prov = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx.XlsxFormatProvider();
                    prov.Export(workbook, stArchivo);
                    using (System.IO.Stream s = dialog.OpenFile())
                    {
                        ((Telerik.Windows.Controls.RadGridView)parametro).Export(s,
                         new Telerik.Windows.Controls.GridViewExportOptions()
                         {
                             Format = Telerik.Windows.Controls.ExportFormat.Html,
                             ShowColumnHeaders = true,
                             ShowColumnFooters = true,
                             ShowGroupFooters = false,
                         });
                    }
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
