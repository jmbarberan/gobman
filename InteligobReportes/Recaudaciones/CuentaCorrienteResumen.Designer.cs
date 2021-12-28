namespace Intelligob.Reportes.Recaudaciones
{
    partial class CuentaCorrienteResumen
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.NavigateToReportAction navigateToReportAction1 = new Telerik.Reporting.NavigateToReportAction();
            Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.NavigateToReportAction navigateToReportAction2 = new Telerik.Reporting.NavigateToReportAction();
            Telerik.Reporting.TypeReportSource typeReportSource2 = new Telerik.Reporting.TypeReportSource();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            this.odsOrden = new Telerik.Reporting.ObjectDataSource();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.txtUsuario = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.odsCuentaCorriente = new Telerik.Reporting.ObjectDataSource();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // odsOrden
            // 
            this.odsOrden.DataMember = "EleOrdenCuentaCorriente";
            this.odsOrden.DataSource = typeof(Intelligob.Reportes.ManejadorInformes);
            this.odsOrden.Name = "odsOrden";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70015746355056763D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.txtInstitucion,
            this.textBox1,
            this.textBox24,
            this.textBox2,
            this.textBox13});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55208331346511841D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1561727523803711D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "= Parameters.pInstitucion.Value";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.55208331346511841D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1561717987060547D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Cuenta corriente al \' + Format(\"{0:dd/MM/yyyy}\",Today()) + \' ordenada por \' + P" +
    "arameters.pOrden.Value";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0.0010012307902798057D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(75.9990005493164D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox24.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox24.Style.Color = System.Drawing.Color.White;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.LineColor = System.Drawing.Color.Black;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Contribuyente";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(78D), Telerik.Reporting.Drawing.Unit.Mm(12.7030029296875D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(22.859994888305664D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox2.Style.Color = System.Drawing.Color.White;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Titulos inpagos";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(104.13999176025391D), Telerik.Reporting.Drawing.Unit.Mm(12.701001167297363D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(32.860008239746094D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox13.Style.Color = System.Drawing.Color.White;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Monto adeudado";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.18003934621810913D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4,
            this.textBox5,
            this.textBox10,
            this.textBox11});
            this.detail.Name = "detail";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.99208664894104D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox3.StyleName = "";
            this.textBox3.Value = "= Fields.Nombres";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:N0}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(78D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89999985694885254D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.Value = "= Fields.Conteo";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0999999046325684D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.293701171875D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Value = "= Fields.Deuda";
            // 
            // textBox10
            // 
            typeReportSource1.Parameters.Add(new Telerik.Reporting.Parameter("pContribuyente", "= Intelligob.Reportes.ManejadorInformes.ContribuyentePorCodigo(Fields.Contribuyen" +
            "tes)"));
            typeReportSource1.TypeName = "Intelligob.Reportes.Recaudaciones.ConsultaDeuda, IntelligobReportes, Version=3.0." +
    "0.0, Culture=neutral, PublicKeyToken=null";
            navigateToReportAction1.ReportSource = typeReportSource1;
            this.textBox10.Action = navigateToReportAction1;
            this.textBox10.Format = "{0:N0}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.4499998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.481102854013443D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.Visible = true;
            this.textBox10.StyleName = "HyperLink";
            this.textBox10.Value = "Detalle";
            // 
            // textBox11
            // 
            typeReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("pContribuyente", "= Fields.Nombres"));
            typeReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("pTitulos", "= Fields.Conteo"));
            typeReportSource2.Parameters.Add(new Telerik.Reporting.Parameter("pMonto", "= Fields.Deuda"));
            typeReportSource2.TypeName = "Intelligob.Reportes.Recaudaciones.NotificacionDeuda, IntelligobReportes, Version=" +
    "3.0.0.0, Culture=neutral, PublicKeyToken=null";
            navigateToReportAction2.ReportSource = typeReportSource2;
            this.textBox11.Action = navigateToReportAction2;
            this.textBox11.Format = "{0:N0}";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.96999979019165D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.72000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.Visible = true;
            this.textBox11.StyleName = "HyperLink";
            this.textBox11.Value = "Notificacion";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.txtUsuario,
            this.textBox25,
            this.textBox7});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(119.85626220703125D), Telerik.Reporting.Drawing.Unit.Mm(2.5400016307830811D));
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.533424377441406D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.txtUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtUsuario.Value = "= Parameters.pUsuario.Value";
            this.txtUsuario.ItemDataBinding += new System.EventHandler(this.txtUsuario_ItemDataBinding);
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(55.880001068115234D), Telerik.Reporting.Drawing.Unit.Mm(2.5400016307830811D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(58.418006896972656D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "= Now()";
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
            // odsCuentaCorriente
            // 
            this.odsCuentaCorriente.DataMember = "CuentaCorrienteResumen";
            this.odsCuentaCorriente.DataSource = typeof(Intelligob.Cliente.Depositos.RepRecaudacionesDep);
            this.odsCuentaCorriente.Name = "odsCuentaCorriente";
            this.odsCuentaCorriente.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pOrden", typeof(string), "=Parameters.pOrden.Value")});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28007888793945312D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox8,
            this.textBox9,
            this.shape1});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.0157480239868164D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3779531717300415D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "= Sum(Fields.Deuda)";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N0}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0708663463592529D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.79763787984848022D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox8.Value = "= Sum(Fields.Conteo)";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N0}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.89212608337402344D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.18000000715255737D));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Value = "Total general";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.0708661079406738D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.3228347301483154D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            // 
            // CuentaCorrienteResumen
            // 
            this.DataSource = this.odsCuentaCorriente;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "CuentaCorrienteResumen";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AvailableValues.DataSource = this.odsOrden;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.Denominacion";
            reportParameter1.AvailableValues.ValueMember = "= Fields.Codigo";
            reportParameter1.Name = "pOrden";
            reportParameter1.Text = "Orden de la consulta";
            reportParameter1.Value = "nombres";
            reportParameter1.Visible = true;
            reportParameter2.Name = "pInstitucion";
            reportParameter2.Value = "GAD Municipal";
            reportParameter3.Name = "pUsuario";
            reportParameter3.Value = "jmbarberan";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.StyleName = "";
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.TextBox), "HyperLink")});
            styleRule2.Style.Color = System.Drawing.Color.Blue;
            styleRule2.Style.Font.Underline = true;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.7082557678222656D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource odsCuentaCorriente;
        private Telerik.Reporting.ObjectDataSource odsOrden;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox txtUsuario;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
    }
}