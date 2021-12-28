namespace Intelligob.Reportes.Recaudaciones
{
    partial class CertificadoNoAdeudar
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificadoNoAdeudar));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter2 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter3 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.ReportParameter reportParameter4 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.pictureBox3 = new Telerik.Reporting.PictureBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.htmlTextBox1 = new Telerik.Reporting.HtmlTextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.shape1 = new Telerik.Reporting.Shape();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.pictureBox2 = new Telerik.Reporting.PictureBox();
            this.barcode1 = new Telerik.Reporting.Barcode();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5001181960105896D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.pictureBox3,
            this.pictureBox1,
            this.textBox1});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.50007867813110352D), Telerik.Reporting.Drawing.Unit.Inch(0.30011811852455139D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4935145378112793D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox2.Style.Font.Bold = false;
            this.textBox2.Style.Font.Name = "Tahoma";
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "Direccion financiera";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.0833334922790527D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.pictureBox3.MimeType = "image/png";
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(40D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox3.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.AutoSize;
            this.pictureBox3.Value = ((object)(resources.GetObject("pictureBox3.Value")));
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
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.5D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4935927391052246D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox1.Style.Font.Name = "Tahoma";
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "= Parameters.pInstitucion.Value";
            this.textBox1.ItemDataBinding += new System.EventHandler(this.textBox1_ItemDataBinding);
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(7.9748039245605469D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.htmlTextBox1,
            this.textBox4,
            this.textBox13,
            this.textBox12,
            this.shape1});
            this.detail.Name = "detail";
            // 
            // htmlTextBox1
            // 
            this.htmlTextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.10000002384185791D), Telerik.Reporting.Drawing.Unit.Inch(2.6998822689056396D));
            this.htmlTextBox1.Name = "htmlTextBox1";
            this.htmlTextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.2999610900878906D), Telerik.Reporting.Drawing.Unit.Inch(1.7999999523162842D));
            this.htmlTextBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.htmlTextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.htmlTextBox1.Value = resources.GetString("htmlTextBox1.Value");
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.39999997615814209D), Telerik.Reporting.Drawing.Unit.Inch(2.0998821258544922D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4935927391052246D), Telerik.Reporting.Drawing.Unit.Inch(0.20000004768371582D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Font.Name = "Tahoma";
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "CERTIFICADO DE NO ADEUDAR";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0000391006469727D), Telerik.Reporting.Drawing.Unit.Inch(5.5998821258544922D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999608516693115D), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332D));
            this.textBox13.Style.Font.Bold = false;
            this.textBox13.Style.Font.Name = "Tahoma";
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Value = "Tesorero Municipal";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0000391006469727D), Telerik.Reporting.Drawing.Unit.Inch(5.3998823165893555D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999608516693115D), Telerik.Reporting.Drawing.Unit.Inch(0.1999211311340332D));
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.Font.Name = "Tahoma";
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Value = "= Parameters.pTesorero.Value";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.0000391006469727D), Telerik.Reporting.Drawing.Unit.Inch(5.2998037338256836D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999603748321533D), Telerik.Reporting.Drawing.Unit.Inch(0.099999748170375824D));
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox2,
            this.barcode1,
            this.textBox3});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.pictureBox2.MimeType = "image/png";
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(100D), Telerik.Reporting.Drawing.Unit.Pixel(48D));
            this.pictureBox2.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox2.Value = ((object)(resources.GetObject("pictureBox2.Value")));
            // 
            // barcode1
            // 
            this.barcode1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.6999611854553223D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.barcode1.Name = "barcode1";
            this.barcode1.ShowText = false;
            this.barcode1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7999998331069946D), Telerik.Reporting.Drawing.Unit.Inch(0.49996057152748108D));
            this.barcode1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.barcode1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.barcode1.Value = "= Parameters.pId.Value";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.1041666641831398D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D));
            this.textBox3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox3.Style.Font.Name = "Tahoma";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Valido por 30 dias desde la fecha emision";
            // 
            // CertificadoNoAdeudar
            // 
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "CertificadoNoAdeudar";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Cm(1.5D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "pTesorero";
            reportParameter2.Name = "pNombres";
            reportParameter3.Name = "pCedula";
            reportParameter4.Name = "pId";
            reportParameter4.Type = Telerik.Reporting.ReportParameterType.Integer;
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
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.PictureBox pictureBox3;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PictureBox pictureBox2;
        private Telerik.Reporting.Barcode barcode1;
        private Telerik.Reporting.HtmlTextBox htmlTextBox1;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.Shape shape1;
    }
}