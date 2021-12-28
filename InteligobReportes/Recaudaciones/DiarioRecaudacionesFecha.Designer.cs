namespace Intelligob.Reportes.Recaudaciones
{
    partial class DiarioRecaudacionesFecha
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
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.txtUsuario = new Telerik.Reporting.TextBox();
            this.odsReporte = new Telerik.Reporting.ObjectDataSource();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70015740394592285D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox24,
            this.txtInstitucion,
            this.pictureBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D), Telerik.Reporting.Drawing.Unit.Inch(0.20011813938617706D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999604225158691D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Diario de recaudaciones por fecha: Desde \' + Format(\"{0:dd/MM/yyyy}\",Parameters" +
    ".pFechaInicio.Value.Date) + \' hasta \' + Format(\"{0:dd/MM/yyyy}\", Parameters.pFec" +
    "haCorte.Value)";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(50.270832061767578D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox12.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox12.Style.Color = System.Drawing.Color.White;
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox12.Style.LineColor = System.Drawing.Color.Black;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "Concepto";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(78.210830688476562D), Telerik.Reporting.Drawing.Unit.Mm(12.703001022338867D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(22.858999252319336D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox13.Style.Color = System.Drawing.Color.White;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Codigo";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(118.85082244873047D), Telerik.Reporting.Drawing.Unit.Mm(12.703001022338867D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(15.240004539489746D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox14.Style.Color = System.Drawing.Color.White;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "Subtotal";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(136.63082885742188D), Telerik.Reporting.Drawing.Unit.Mm(12.703001022338867D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(12.700002670288086D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox15.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox15.Style.Color = System.Drawing.Color.White;
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "Desctos.";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(151.87086486816406D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(12.699995040893555D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox16.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox16.Style.Color = System.Drawing.Color.White;
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.TextWrap = false;
            this.textBox16.Value = "Recargos";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(167.11083984375D), Telerik.Reporting.Drawing.Unit.Mm(12.703001022338867D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(15.238009452819824D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox17.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox17.Style.Color = System.Drawing.Color.White;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "Total";
            // 
            // textBox18
            // 
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(103.61083221435547D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(12.700002670288086D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox18.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox18.Style.Color = System.Drawing.Color.White;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "Año";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(48.260002136230469D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox24.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox24.Style.Color = System.Drawing.Color.White;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.LineColor = System.Drawing.Color.Black;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Contribuyente";
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.70000004768371582D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999213218688965D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "= Parameters.pInstitucion.Value";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20535452663898468D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox11,
            this.textBox10,
            this.textBox9,
            this.textBox8,
            this.textBox5});
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.100001335144043D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89996057748794556D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox2.TextWrap = false;
            this.textBox2.Value = "= Fields.Codigo";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1000018119812012D), Telerik.Reporting.Drawing.Unit.Inch(3.9577484130859375E-05D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49999988079071045D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Value = "= Fields.Año";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2D), Telerik.Reporting.Drawing.Unit.Inch(3.9577484130859375E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox4.TextWrap = false;
            this.textBox4.Value = "= Fields.Conceptoden";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9656955777900293E-05D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8999607563018799D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox11.TextWrap = false;
            this.textBox11.Value = "= Fields.Nombres";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.5791668891906738D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.59992164373397827D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Value = "= Fields.Total + Fields.Recargos - Fields.Rebajas";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3812503814697266D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Value = "= Fields.Rebajas";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N2}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.9854168891906738D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Value = "= Fields.Recargos";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.679166316986084D), Telerik.Reporting.Drawing.Unit.Inch(7.85986558184959E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000050067901611D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Value = "= Fields.Total";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox25,
            this.txtUsuario});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(2.5399975776672363D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(43.180000305175781D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Value = "= \'Pag. \' + PageNumber";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(55.880001068115234D), Telerik.Reporting.Drawing.Unit.Mm(2.5399975776672363D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(58.418006896972656D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "= Now()";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(132.08000183105469D), Telerik.Reporting.Drawing.Unit.Mm(2.5399975776672363D));
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.799995422363281D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.txtUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtUsuario.Value = "= Parameters.pUsuario.Value";
            this.txtUsuario.ItemDataBinding += new System.EventHandler(this.txtUsuario_ItemDataBinding);
            // 
            // odsReporte
            // 
            this.odsReporte.DataMember = "ReporteRecaudacionesFecha";
            this.odsReporte.DataSource = typeof(Intelligob.Cliente.Depositos.RepRecaudacionesDep);
            this.odsReporte.Name = "odsReporte";
            this.odsReporte.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaInicio", typeof(System.DateTime), "= Parameters.pFechaInicio.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaCorte", typeof(System.DateTime), "= Parameters.pFechaCorte.Value")});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.31496062874794006D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox19,
            this.shape2,
            this.textBox23,
            this.textBox22,
            this.textBox21,
            this.textBox20});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(33.019996643066406D), Telerik.Reporting.Drawing.Unit.Mm(2.5420041084289551D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(43.180004119873047D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Value = "= Count(Fields.Id) + \' titulo(s) TOTAL\'";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(0D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(182.34884643554688D), Telerik.Reporting.Drawing.Unit.Mm(2.5399994850158691D));
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5534617900848389D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89992141723632812D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Value = "= Sum(Fields.Total)";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N2}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.3791670799255371D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69992142915725708D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Value = "= Sum(Fields.Recargos)";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:N2}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.5770835876464844D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.69992142915725708D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox21.Style.Font.Bold = false;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Value = "= Sum(Fields.Rebajas)";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:N2}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1812500953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.10007890313863754D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.99992132186889648D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Value = "= Sum(Fields.Total + Fields.Recargos - Fields.Rebajas)";
            // 
            // DiarioRecaudacionesFecha
            // 
            this.DataSource = this.odsReporte;
            this.DocumentName = "Diario de recaudaciones por fecha";
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "DiarioRecaudacionesFecha";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pFechaInicio";
            reportParameter1.Text = "Fecha de inicio";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter1.Value = "= Today()";
            reportParameter1.Visible = true;
            reportParameter2.Name = "pFechaCorte";
            reportParameter2.Text = "Fecha de corte";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter2.Value = "= Today()";
            reportParameter2.Visible = true;
            reportParameter3.Name = "pInstitucion";
            reportParameter3.Text = "Institucion";
            reportParameter3.Value = "Empresa publica";
            reportParameter4.Name = "pUsuario";
            reportParameter4.Text = "Usuario";
            reportParameter4.Value = "Desarrollador";
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
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Mm;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(7.2000002861022949D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource odsReporte;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox txtUsuario;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox20;
    }
}