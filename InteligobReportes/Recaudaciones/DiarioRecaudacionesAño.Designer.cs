namespace Intelligob.Reportes.Recaudaciones
{
    partial class DiarioRecaudacionesAño
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
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.txtUsuario = new Telerik.Reporting.TextBox();
            this.odsRecaudaciones = new Telerik.Reporting.ObjectDataSource();
            this.reportFooterSection1 = new Telerik.Reporting.ReportFooterSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.2997640073299408D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21,
            this.shape1});
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9577484130859375E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.0996459349989891D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.0999612808227539D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "= \"Total año \" + Fields.Año + Format(\" ({0} trans.)\", Count(Fields.Id))";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:N2}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1895833015441895D), Telerik.Reporting.Drawing.Unit.Inch(0.099685348570346832D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000020265579224D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "= Sum(Fields.Total)";
            // 
            // textBox19
            // 
            this.textBox19.Format = "{0:N2}";
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9270834922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.099685348570346832D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "= Sum(Fields.Rebajas)";
            // 
            // textBox20
            // 
            this.textBox20.Format = "{0:N2}";
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.5208334922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.099685348570346832D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "= Sum(Fields.Recargos)";
            // 
            // textBox21
            // 
            this.textBox21.Format = "{0:N2}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.1145834922790527D), Telerik.Reporting.Drawing.Unit.Inch(0.099685348570346832D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61450523138046265D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "= Sum(Fields.Total + Fields.Recargos - Fields.Rebajas)";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1895828247070312D), Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.539506196975708D), Telerik.Reporting.Drawing.Unit.Inch(0.079999998211860657D));
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20011799037456513D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3});
            this.groupHeaderSection.Name = "groupHeaderSection";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999606132507324D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "= \"Año \" + Fields.Año";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.70015734434127808D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.txtInstitucion,
            this.pictureBox1,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox24,
            this.textBox2});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.57291668653488159D), Telerik.Reporting.Drawing.Unit.Inch(0.20011806488037109D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1561717987060547D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Style.Font.Bold = false;
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "=\'Diario de recaudaciones por año: Desde \' + Format(\"{0:dd/MM/yyyy}\",Parameters.p" +
    "FechaInicio.Value.Date) + \' hasta \' + Format(\"{0:dd/MM/yyyy}\",Parameters.pFechaI" +
    "nicio.Value.Date)";
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.57291668653488159D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1561727523803711D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "= Parameters.pInstitucion.Value";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(81.279998779296875D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(22.85999870300293D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox13.Style.Color = System.Drawing.Color.White;
            this.textBox13.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "Codigo";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(106.68000030517578D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
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
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(125.41249847412109D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
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
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(140.49374389648438D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
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
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(155.57499694824219D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(15.610419273376465D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox17.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox17.Style.Color = System.Drawing.Color.White;
            this.textBox17.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "Total";
            // 
            // textBox24
            // 
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(45.719997406005859D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox24.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox24.Style.Color = System.Drawing.Color.White;
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox24.Style.LineColor = System.Drawing.Color.Black;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "Contribuyente";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(48.260002136230469D), Telerik.Reporting.Drawing.Unit.Mm(12.702999114990234D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(30.479999542236328D), Telerik.Reporting.Drawing.Unit.Mm(5.080996036529541D));
            this.textBox2.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox2.Style.Color = System.Drawing.Color.White;
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Concepto";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(0.20007872581481934D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox8,
            this.textBox9,
            this.textBox5,
            this.textBox4,
            this.textBox11,
            this.textBox6});
            this.detail.Name = "detail";
            // 
            // textBox10
            // 
            this.textBox10.Format = "{0:N2}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.125D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60408920049667358D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox10.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "= Fields.Total + Fields.Recargos - Fields.Rebajas";
            // 
            // textBox8
            // 
            this.textBox8.Format = "{0:N2}";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.53125D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "= Fields.Recargos";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.9375D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "= Fields.Rebajas";
            // 
            // textBox5
            // 
            this.textBox5.Format = "{0:N2}";
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000020265579224D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "= Fields.Total";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.89999991655349731D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.TextWrap = false;
            this.textBox4.Value = "= Fields.Codigo";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox11.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.TextWrap = false;
            this.textBox11.Value = "= Fields.Nombres";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.9000002145767212D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.TextWrap = false;
            this.textBox6.Value = "= Fields.Conceptoden";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29984250664711D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox7,
            this.textBox25,
            this.txtUsuario});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(0D), Telerik.Reporting.Drawing.Unit.Mm(2.5360007286071777D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.270832061767578D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.Value = "= \'Pag. \' + PageNumber";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(55.827083587646484D), Telerik.Reporting.Drawing.Unit.Mm(2.5360007286071777D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(58.418006896972656D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Value = "= Now()";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Mm(120.38541412353516D), Telerik.Reporting.Drawing.Unit.Mm(2.5360007286071777D));
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Mm(50.533424377441406D), Telerik.Reporting.Drawing.Unit.Mm(5.0789976119995117D));
            this.txtUsuario.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.txtUsuario.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.txtUsuario.Value = "= Parameters.pUsuario.Value";
            this.txtUsuario.ItemDataBinding += new System.EventHandler(this.txtUsuario_ItemDataBinding);
            // 
            // odsRecaudaciones
            // 
            this.odsRecaudaciones.DataMember = "ReporteRecaudacionesDetallado";
            this.odsRecaudaciones.DataSource = typeof(Intelligob.Cliente.Depositos.RepRecaudacionesDep);
            this.odsRecaudaciones.Name = "odsRecaudaciones";
            this.odsRecaudaciones.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pOrden", typeof(string), "\"año\""),
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaInicio", typeof(System.DateTime), "=Parameters.pFechaInicio.Value"),
            new Telerik.Reporting.ObjectDataSourceParameter("pFechaCorte", typeof(System.DateTime), "=Parameters.pFechaCorte.Value")});
            // 
            // reportFooterSection1
            // 
            this.reportFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.29988208413124084D);
            this.reportFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox23,
            this.textBox26,
            this.textBox27,
            this.textBox28});
            this.reportFooterSection1.Name = "reportFooterSection1";
            // 
            // textBox22
            // 
            this.textBox22.Format = "{0:N2}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.116666316986084D), Telerik.Reporting.Drawing.Unit.Inch(0.099803447723388672D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.61450523138046265D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox22.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox22.Style.Color = System.Drawing.Color.White;
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "= Sum(Fields.Total + Fields.Recargos - Fields.Rebajas)";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.522916316986084D), Telerik.Reporting.Drawing.Unit.Inch(0.099803447723388672D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox23.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox23.Style.Color = System.Drawing.Color.White;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "= Sum(Fields.Recargos)";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:N2}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.929166316986084D), Telerik.Reporting.Drawing.Unit.Inch(0.099803447723388672D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.50000011920928955D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox26.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox26.Style.Color = System.Drawing.Color.White;
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "= Sum(Fields.Rebajas)";
            // 
            // textBox27
            // 
            this.textBox27.Format = "{0:N2}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.1895828247070312D), Telerik.Reporting.Drawing.Unit.Inch(0.099803447723388672D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.60000020265579224D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox27.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox27.Style.Color = System.Drawing.Color.White;
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "= Sum(Fields.Total)";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.099803291261196136D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.0999612808227539D), Telerik.Reporting.Drawing.Unit.Inch(0.20003931224346161D));
            this.textBox28.Style.BackgroundColor = System.Drawing.Color.DimGray;
            this.textBox28.Style.Color = System.Drawing.Color.White;
            this.textBox28.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "= \'TOTAL GENERAL \' + Format(\"({0} trans.)\", Count(Fields.Id))";
            // 
            // DiarioRecaudacionesAño
            // 
            this.DataSource = this.odsRecaudaciones;
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Año"));
            group1.Name = "grupoAño";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1,
            this.reportFooterSection1});
            this.Name = "DiarioRecaudacionesAño";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pFechaInicio";
            reportParameter1.Text = "pFechaInicio";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter1.Value = "=Today()";
            reportParameter1.Visible = true;
            reportParameter2.Name = "pFechaCorte";
            reportParameter2.Text = "pFechaCorte";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.DateTime;
            reportParameter2.Value = "=Today()";
            reportParameter2.Visible = true;
            reportParameter3.Name = "pInstitucion";
            reportParameter3.Value = "Empresa";
            reportParameter4.Name = "pUsuario";
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.7415885925292969D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource odsRecaudaciones;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox txtUsuario;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.ReportFooterSection reportFooterSection1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox28;
    }
}