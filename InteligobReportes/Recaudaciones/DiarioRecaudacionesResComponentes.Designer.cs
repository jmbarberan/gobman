namespace Intelligob.Reportes.Recaudaciones
{
    partial class DiarioRecaudacionesResComponentes
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.txtUsuario = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70003920793533325D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.txtInstitucion,
            this.textBox1,
            this.textBox14,
            this.textBox24});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1083335876464844D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "= Parameters.pInstitucion.Value";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(0.20011806488037109D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1083335876464844D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Diario de recaudaciones por componentes: Desde \' + Format(\"{0:dd/MM/yyyy}\",Para" +
    "meters.pFechaInicio.Value.Date) + \' hasta \' + Format(\"{0:dd/MM/yyyy}\",Parameters" +
    ".pFechaInicio.Value.Date)";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(106.68000030517578D), Telerik.Reporting.Drawing.Unit.Mm(12.699999809265137D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(29.473590850830078D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox14.Style.Color = System.Drawing.Color.White;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Valor";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0.0010012307902798057D), Telerik.Reporting.Drawing.Unit.Mm(12.699999809265137D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(104.13899230957031D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox24.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox24.Style.Color = System.Drawing.Color.White;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.LineColor = System.Drawing.Color.Black;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Concepto => Rubro";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20011813938617706D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox11});
            this.detail.Name = "detail";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1603776216506958D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Fields.Total";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.TextWrap = false;
            this.textBox11.Value = "= Fields.Nombres";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.30000019073486328D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox25,
            this.txtUsuario});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(2.5400016307830811D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.270832061767578D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Value = "= \'Pag. \' + PageNumber";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(55.033332824707031D), Telerik.Reporting.Drawing.Unit.Mm(2.5400016307830811D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(58.418006896972656D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "= Now()";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(119.59165954589844D), Telerik.Reporting.Drawing.Unit.Mm(2.5400016307830811D));
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.799995422363281D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.txtUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtUsuario.Value = "= Parameters.pUsuario.Value";
            this.txtUsuario.ItemDataBinding += new System.EventHandler(this.txtUsuario_ItemDataBinding);
            // 
            // DiarioRecaudacionesResComponentes
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "DiarioRecaudacionesResComponentes";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pInstitucion";
            reportParameter1.Value = "Empresa";
            reportParameter2.Name = "pUsuario";
            reportParameter2.Value = "Desarrollador";
            reportParameter3.Name = "pFechaInicio";
            reportParameter3.Text = "Fecha de inicio";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter3.Value = "= Today()";
            reportParameter3.Visible = true;
            reportParameter4.Name = "pFechaCorte";
            reportParameter4.Text = "Fecha de corte";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter4.Value = "= Today()";
            reportParameter4.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.7083334922790527D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox txtUsuario;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox11;
    }
}