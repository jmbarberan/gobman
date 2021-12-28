namespace Intelligob.Reportes.Emisiones
{
    partial class ResumenEmisionPeriodo
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter5 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter6 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter7 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.detailSection1 = new Telerik.Reporting.DetailSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.odsEmision = new Telerik.Reporting.ObjectDataSource();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.txtUsuario = new Telerik.Reporting.TextBox();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.shape2 = new Telerik.Reporting.Shape();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.shape3 = new Telerik.Reporting.Shape();
            this.shape4 = new Telerik.Reporting.Shape();
            this.textBox18 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29996061325073242D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox6,
            this.textBox9,
            this.shape1,
            this.textBox17});
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4399607181549072D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Value = "= Format(\"Total {0}\", Fields.concepto)";
            // 
            // textBox6
            // 
            this.textBox6.Format = "{0:N2}";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox6.Value = "= Sum(Fields.Ajeno)";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5750000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Value = "= Sum(Fields.Propio)";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5D), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.2999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(0.099763549864292145D));
            // 
            // textBox17
            // 
            this.textBox17.Format = "{0:N2}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(0.099921226501464844D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Value = "= Sum(Fields.Suma)";
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1});
            this.groupHeaderSection.Name = "groupHeaderSection";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4399607181549072D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Value = "= Fields.concepto";
            // 
            // detailSection1
            // 
            this.detailSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003938674926758D);
            this.detailSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox16});
            this.detailSection1.Name = "detailSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1400001049041748D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox2.Value = "= Fields.rubro";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:N2}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5748434066772461D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox3.Value = "= Fields.Propio";
            // 
            // textBox4
            // 
            this.textBox4.Format = "{0:N2}";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.Value = "= Fields.Ajeno";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0:N2}";
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Value = "= Fields.Suma";
            // 
            // odsEmision
            // 
            this.odsEmision.DataMember = "ResumenEmisionesPeriodo";
            this.odsEmision.DataSource = typeof(Intelligob.Cliente.Depositos.RepRecaudacionesDep);
            this.odsEmision.Name = "odsEmision";
            this.odsEmision.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pAño", typeof(int), "= Parameters.pAño.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaInicio", typeof(System.DateTime), "= Parameters.pFechaInicio.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaCorte", typeof(System.DateTime), "= Parameters.pFechaCorte.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pPeriodo", typeof(int), "= Parameters.pPeriodo.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pAfectantes", typeof(bool), "= Parameters.pAfectantes.Value")});
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.93999999761581421D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.pictureBox1,
            this.reportNameTextBox,
            this.textBox7,
            this.textBox10,
            this.textBox11,
            this.textBox12});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1999611854553223D), Telerik.Reporting.Drawing.Unit.Inch(0.29996070265769958D));
            this.titleTextBox.Style.BackgroundColor = System.Drawing.Color.White;
            this.titleTextBox.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.titleTextBox.Style.Font.Bold = false;
            this.titleTextBox.Style.Font.Name = "Verdana";
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.titleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "= Parameters.pInstitucion.Value";
            this.titleTextBox.ItemDataBinding += new System.EventHandler(this.titleTextBox_ItemDataBinding);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0062500634230673313D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(45D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1999611854553223D), Telerik.Reporting.Drawing.Unit.Inch(0.16867126524448395D));
            this.reportNameTextBox.Style.BackgroundColor = System.Drawing.Color.White;
            this.reportNameTextBox.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.reportNameTextBox.Style.Font.Bold = true;
            this.reportNameTextBox.Style.Font.Name = "Verdana";
            this.reportNameTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.reportNameTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "= Format(\"Resumen de Emisiones del año {0} Periodo: {1} a {2}\", Parameters.pAño.V" +
    "alue, Parameters.pFechaInicio.Value, Parameters.pFechaCorte.Value) ";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.51999998092651367D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.440000057220459D), Telerik.Reporting.Drawing.Unit.Inch(0.39992126822471619D));
            this.textBox7.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.029999999329447746D);
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox7.Value = "CONCEPTO \\ RUBRO";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(0.51999998092651367D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(0.3999212384223938D));
            this.textBox10.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.029999999329447746D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox10.Value = "TOTAL";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.51999998092651367D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(0.3999212384223938D));
            this.textBox11.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.029999999329447746D);
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox11.Value = "FONDOS AJENOS";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5748434066772461D), Telerik.Reporting.Drawing.Unit.Inch(0.520000159740448D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.0000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(0.3999212384223938D));
            this.textBox12.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Padding.Bottom = Telerik.Reporting.Drawing.Unit.Inch(0.029999999329447746D);
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox12.Value = "FONDOS PROPIOS";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2251582145690918D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox8.Name = "ReportPageNumberTextBox";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4D), Telerik.Reporting.Drawing.Unit.Cm(0.50800001621246338D));
            this.textBox8.Style.Color = System.Drawing.Color.DimGray;
            this.textBox8.Style.Font.Name = "Segoe UI";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "Pag.: {PageNumber}";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20003938674926758D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.txtUsuario});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.60390043258667D), Telerik.Reporting.Drawing.Unit.Cm(0.50800001621246338D));
            this.txtUsuario.Style.Color = System.Drawing.Color.DimGray;
            this.txtUsuario.Style.Font.Name = "Segoe UI";
            this.txtUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtUsuario.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.txtUsuario.Value = "= Parameters.pUsuario.Value";
            this.txtUsuario.ItemDataBinding += new System.EventHandler(this.txtUsuario_ItemDataBinding);
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.40416669845581055D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape2,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.shape3,
            this.shape4,
            this.textBox18});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // shape2
            // 
            this.shape2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0062500634230673313D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.shape2.Name = "shape2";
            this.shape2.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7937111854553223D), Telerik.Reporting.Drawing.Unit.Inch(0.099763549864292145D));
            // 
            // textBox13
            // 
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5750000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.099842391908168793D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.Value = "= Sum(Fields.Propio)";
            // 
            // textBox14
            // 
            this.textBox14.Format = "{0:N2}";
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.7000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.099842391908168793D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.Value = "= Sum(Fields.Ajeno)";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0020834605675190687D), Telerik.Reporting.Drawing.Unit.Inch(0.099842391908168793D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.4379165172576904D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Value = "= Format(\"Total {0}\", Fields.concepto)";
            // 
            // shape3
            // 
            this.shape3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.31999999284744263D));
            this.shape3.Name = "shape3";
            this.shape3.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699D));
            // 
            // shape4
            // 
            this.shape4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.0062500634230673313D), Telerik.Reporting.Drawing.Unit.Inch(0.35208335518836975D));
            this.shape4.Name = "shape4";
            this.shape4.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.7937111854553223D), Telerik.Reporting.Drawing.Unit.Inch(0.0520833320915699D));
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N2}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.7999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(0.099842391908168793D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Value = "= Sum(Fields.Suma)";
            // 
            // ResumenEmisionPeriodo
            // 
            this.DataSource = this.odsEmision;
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.concepto"));
            group1.Name = "group";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.detailSection1,
            this.pageHeaderSection1,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "ResumenEmisionPeriodo";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D), Telerik.Reporting.Drawing.Unit.Inch(0.25D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pAño";
            reportParameter1.Text = "Año de consulta";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter2.Name = "pFechaInicio";
            reportParameter2.Text = "pFechaInicio";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter3.Name = "pFechaCorte";
            reportParameter3.Text = "pFechaCorte";
            reportParameter3.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter4.Name = "pPeriodo";
            reportParameter4.Text = "pPeriodo";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter5.Name = "pAfectantes";
            reportParameter5.Text = "pAfectantes";
            reportParameter5.Type = Telerik.Reporting.ReportParameterType.Boolean;
            reportParameter6.Name = "pUsuario";
            reportParameter6.Text = "Usuario";
            reportParameter6.Value = "N/D";
            reportParameter7.Name = "pInstitucion";
            reportParameter7.Text = "Institucion";
            reportParameter7.Value = "GAD Municipal";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            this.ReportParameters.Add(reportParameter4);
            this.ReportParameters.Add(reportParameter5);
            this.ReportParameters.Add(reportParameter6);
            this.ReportParameters.Add(reportParameter7);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Aspect.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.Black;
            styleRule2.Style.Font.Name = "Verdana";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Aspect.TableHeader")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(222)))), ((int)(((byte)(209)))));
            styleRule3.Style.Font.Name = "Verdana";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Aspect.TableBody")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Verdana";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.7999610900878906D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detailSection1;
        private Telerik.Reporting.ObjectDataSource odsEmision;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox txtUsuario;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.Shape shape2;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.Shape shape3;
        private Telerik.Reporting.Shape shape4;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;

    }
}