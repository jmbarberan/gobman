namespace Intelligob.Reportes.Recaudaciones
{
    partial class NotificacionDeuda
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
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.txtInstitucion = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.50003933906555176D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.txtInstitucion});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.pictureBox1.MimeType = "";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(48D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = "";
            this.pictureBox1.ItemDataBinding += new System.EventHandler(this.pictureBox1_ItemDataBinding);
            // 
            // txtInstitucion
            // 
            this.txtInstitucion.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.5999603271484375D), Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D));
            this.txtInstitucion.Name = "txtInstitucion";
            this.txtInstitucion.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.9000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.txtInstitucion.Style.Font.Bold = true;
            this.txtInstitucion.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.txtInstitucion.Value = "";
            this.txtInstitucion.ItemDataBinding += new System.EventHandler(this.textBox6_ItemDataBinding);
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(5.2999610900878906D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.shape1,
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox19,
            this.textBox11});
            this.detail.Name = "detail";
            // 
            // textBox1
            // 
            this.textBox1.Format = "{0:D}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D), Telerik.Reporting.Drawing.Unit.Inch(0.099960722029209137D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox1.Value = "= Today()";
            // 
            // textBox2
            // 
            this.textBox2.Format = "";
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D), Telerik.Reporting.Drawing.Unit.Inch(1.0999606847763062D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox2.Value = "= \'Sr(a) \' + Parameters.pContribuyente.Value";
            // 
            // textBox3
            // 
            this.textBox3.Format = "";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D), Telerik.Reporting.Drawing.Unit.Inch(1.3000396490097046D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox3.Value = "Ciudad";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D), Telerik.Reporting.Drawing.Unit.Inch(2.00003981590271D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000003814697266D), Telerik.Reporting.Drawing.Unit.Inch(0.099999904632568359D));
            // 
            // textBox4
            // 
            this.textBox4.Format = "";
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D), Telerik.Reporting.Drawing.Unit.Inch(1.799960732460022D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.4000000953674316D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Value = "ASUNTO: NOTIFICACION POR VALORES IMPAGOS";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.19999980926513672D), Telerik.Reporting.Drawing.Unit.Inch(2.2999608516693115D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848D), Telerik.Reporting.Drawing.Unit.Inch(0.40000024437904358D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Justify;
            this.textBox5.Value = "Estimado(a) Contribuyente nos ponemos en contacto con usted el refrencia a la ded" +
    "a que mantiene esta institucion que se resume de la siguiente manera:";
            // 
            // textBox6
            // 
            this.textBox6.Format = "";
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(2.7999608516693115D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox6.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.Font.Bold = true;
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Value = "Titulos Impagos";
            // 
            // textBox7
            // 
            this.textBox7.Format = "";
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(2.7999608516693115D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox7.Style.BorderColor.Default = System.Drawing.Color.Black;
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.Font.Bold = true;
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox7.Value = "Monto deudado";
            // 
            // textBox8
            // 
            this.textBox8.Format = "";
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(3.00003981590271D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Value = "= Parameters.pTitulos.Value";
            // 
            // textBox9
            // 
            this.textBox9.Format = "{0:N2}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.1000001430511475D), Telerik.Reporting.Drawing.Unit.Inch(3.00003981590271D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2999997138977051D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.Value = "= Parameters.pMonto.Value";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D), Telerik.Reporting.Drawing.Unit.Inch(3.39996075630188D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Justify;
            this.textBox10.Value = "Les agradeceríamos que regularizaran su situación lo antes posible. Si tiene cual" +
    "quier duda al respecto puede acercarse a la Tesoreria Municipal. Sin otro partic" +
    "ular, le saludamos cordialmente.";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(4.5999608039855957D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.Font.Bold = true;
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "";
            this.textBox19.ItemDataBinding += new System.EventHandler(this.textBox19_ItemDataBinding);
            // 
            // textBox11
            // 
            this.textBox11.Format = "";
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.6999998092651367D), Telerik.Reporting.Drawing.Unit.Inch(4.8000397682189941D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.20000012218952179D));
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Value = "= \'Tesorero Municipal\'";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D);
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // NotificacionDeuda
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "NotificacionDeuda";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.Name = "pTitulos";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Value = "0";
            reportParameter2.Name = "pMonto";
            reportParameter2.Type = Telerik.Reporting.ReportParameterType.Float;
            reportParameter2.Value = "0";
            reportParameter3.Name = "pContribuyente";
            reportParameter3.Text = "= Parameters.pContribuyente.Value";
            this.ReportParameters.Add(reportParameter1);
            this.ReportParameters.Add(reportParameter2);
            this.ReportParameters.Add(reportParameter3);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox txtInstitucion;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox11;
    }
}