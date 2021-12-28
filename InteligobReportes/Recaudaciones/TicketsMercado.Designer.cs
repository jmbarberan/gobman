namespace Intelligob.Reportes.Recaudaciones
{
    partial class TicketsMercado
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketsMercado));
            Telerik.Reporting.SortingAction sortingAction1 = new Telerik.Reporting.SortingAction();
            Telerik.Reporting.SortingAction sortingAction2 = new Telerik.Reporting.SortingAction();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.dsReporte = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.txtInstitucion,
            this.pictureBox1,
            this.textBox2,
            this.textBox9,
            this.textBox12,
            this.textBox13,
            this.textBox14});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.709D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.559D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Recaudacion de tickets de mercado desde: \' + Format(\"{0:dd/MM/yyyy}\",Parameters" +
    ".pFechaInicio.Value.Date) + \' hasta: \' + Format(\"{0:dd/MM/yyyy}\", Parameters.pFe" +
    "chaCorte.Value)";
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.709D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.559D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "= Parameters.pInstitucion.Value";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.009D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox1.MimeType = "image/jpeg";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.022D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.778D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox2.Style.Color = System.Drawing.Color.White;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Contribuyente";
            // 
            // textBox9
            // 
            sortingAction1.SortingExpression = "= Fields.Rebajas";
            sortingAction1.Targets.AddRange(new Telerik.Reporting.IActionTarget[] {
            this});
            this.textBox9.Action = sortingAction1;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox9.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox9.Style.Color = System.Drawing.Color.White;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "Puesto";
            // 
            // textBox12
            // 
            sortingAction2.SortingExpression = "= Fields.SoporteNumero";
            sortingAction2.Targets.AddRange(new Telerik.Reporting.IActionTarget[] {
            this});
            this.textBox12.Action = sortingAction2;
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.2D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox12.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox12.Style.Color = System.Drawing.Color.White;
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "No. de ticket";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.3D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.27D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox13.Style.Color = System.Drawing.Color.White;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Año";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.8D), Telerik.Reporting.Drawing.Unit.Cm(1.4D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.014D), Telerik.Reporting.Drawing.Unit.Cm(0.6D));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox14.Style.Color = System.Drawing.Color.White;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Valor";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.692D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox11,
            this.textBox10,
            this.textBox4,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:#.}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.756D), Telerik.Reporting.Drawing.Unit.Inch(0.036D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.787D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox3.TextWrap = false;
            this.textBox3.Value = "= Fields.Rebajas";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.036D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.677D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox11.TextWrap = false;
            this.textBox11.Value = "= Fields.Nombres";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.433D), Telerik.Reporting.Drawing.Unit.Inch(0.036D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.793D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Value = "= Fields.Pagos";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.843D), Telerik.Reporting.Drawing.Unit.Inch(0.036D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Value = "= Fields.Fecha.Year";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.622D), Telerik.Reporting.Drawing.Unit.Inch(0.036D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.142D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox5.TextWrap = false;
            this.textBox5.Value = "= Fields.SoporteNumero";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.508D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.252D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.974D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox8.Style.Font.Bold = false;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "=Format(\"Pag. {0}\", PageNumber)";
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.631D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox7,
            this.textBox15});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.118D), Telerik.Reporting.Drawing.Unit.Inch(0.039D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.108D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox6.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "= Sum(Fields.Pagos)";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.252D), Telerik.Reporting.Drawing.Unit.Inch(0.039D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.787D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.TextWrap = false;
            this.textBox7.Value = "TOTAL";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.009D), Telerik.Reporting.Drawing.Unit.Inch(0.039D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.747D), Telerik.Reporting.Drawing.Unit.Inch(0.2D));
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.TextWrap = false;
            this.textBox15.Value = "= Format(\"{0} Registros\", Count(Fields.Id))";
            // 
            // dsReporte
            // 
            this.dsReporte.DataMember = "ReporteRecaudacionTicketsLocal";
            this.dsReporte.DataSource = typeof(Intelligob.Cliente.Depositos.RepRecaudacionesDep);
            this.dsReporte.Name = "dsReporte";
            this.dsReporte.Parameters.Add(new Telerik.Reporting.ObjectDataSourceParameter("pLocal", typeof(int), "= Parameters.pLocal.Value"));
            this.dsReporte.Parameters.Add(new Telerik.Reporting.ObjectDataSourceParameter("pFechaInicio", typeof(System.DateTime), "= Parameters.pFechaInicio.Value"));
            this.dsReporte.Parameters.Add(new Telerik.Reporting.ObjectDataSourceParameter("pFechaCorte", typeof(System.DateTime), "= Parameters.pFechaCorte.Value"));
            // 
            // TicketsMercado
            // 
            this.DataSource = this.dsReporte;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "TicketsMercado";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.4D), Telerik.Reporting.Drawing.Unit.Mm(25.4D), Telerik.Reporting.Drawing.Unit.Mm(25.4D), Telerik.Reporting.Drawing.Unit.Mm(25.4D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pFechaInicio";
            reportParameter1.Text = "Desde";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter1.Visible = true;
            reportParameter2.Name = "pFechaCorte";
            reportParameter2.Text = "Hasta";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter2.Visible = true;
            reportParameter3.Name = "pLocal";
            reportParameter3.Text = "Numero de puesto (0 = TODOS)";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter3.Value = "0";
            reportParameter3.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.Sortings.Add(new Telerik.Reporting.Sorting("= Fields.SoporteNumero", Telerik.Reporting.SortDirection.Asc));
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(15.92D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.ObjectDataSource dsReporte;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
    }
}