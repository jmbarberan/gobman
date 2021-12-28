namespace Intelligob.Reportes.Recaudaciones
{
    partial class Planilla
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup7 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup8 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup9 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.Barcodes.Code128Encoder code128Encoder1 = new Telerik.Reporting.Barcodes.Code128Encoder();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.dsPlanilla = new Telerik.Reporting.ObjectDataSource();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.dsAtributos = new Telerik.Reporting.ObjectDataSource();
            this.table2 = new Telerik.Reporting.Table();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.dsRubros = new Telerik.Reporting.ObjectDataSource();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.barcode1 = new Telerik.Reporting.Barcode();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox11
            // 
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0604162216186523D), Telerik.Reporting.Drawing.Unit.Inch(0.20003953576087952D));
            this.textBox11.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Value = "Rubro";
            // 
            // textBox15
            // 
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.73750007152557373D), Telerik.Reporting.Drawing.Unit.Inch(0.20003953576087952D));
            this.textBox15.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.Font.Bold = true;
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox15.Value = "Valor";
            // 
            // dsPlanilla
            // 
            this.dsPlanilla.DataMember = "PlanillaPagada";
            this.dsPlanilla.DataSource = typeof(Intelligob.Cliente.Depositos.RecaudacionesDep);
            this.dsPlanilla.Name = "dsPlanilla";
            this.dsPlanilla.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pPlanilla", typeof(int), "= Parameters.pPlanilla.Value")});
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox3,
            this.table1,
            this.table2});
            this.detail.Name = "detail";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000789880752564D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox4.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "Valor";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.3000788688659668D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.TextWrap = false;
            this.textBox5.Value = "Año";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000787496566773D), Telerik.Reporting.Drawing.Unit.Inch(0.3000788688659668D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox6.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.TextWrap = false;
            this.textBox6.Value = "= Fields.Año";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.50015753507614136D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6999999284744263D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox7.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.TextWrap = false;
            this.textBox7.Value = "Codigo";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.8000783920288086D), Telerik.Reporting.Drawing.Unit.Inch(0.50015753507614136D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7000006437301636D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox8.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.TextWrap = false;
            this.textBox8.Value = "= Fields.CodigoExtendigo";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(7.8678131103515625E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "Atributo";
            // 
            // table1
            // 
            this.table1.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Left)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.8020974397659302D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.697929859161377D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.21018059551715851D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox12);
            this.table1.Body.SetCellContent(0, 1, this.textBox14);
            tableGroup1.Name = "tableGroup";
            tableGroup2.Name = "tableGroup1";
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.DataSource = this.dsAtributos;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox14});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9339065551757812E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.70023632049560547D));
            this.table1.Name = "table1";
            tableGroup3.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup3.Name = "detailTableGroup";
            this.table1.RowGroups.Add(tableGroup3);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.5000274181365967D), Telerik.Reporting.Drawing.Unit.Inch(0.21018059551715851D));
            // 
            // textBox12
            // 
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.8020972013473511D), Telerik.Reporting.Drawing.Unit.Inch(0.21018059551715851D));
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.TextWrap = false;
            this.textBox12.Value = "= Fields.Denominacion";
            // 
            // textBox14
            // 
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6979300975799561D), Telerik.Reporting.Drawing.Unit.Inch(0.21018059551715851D));
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.TextWrap = false;
            this.textBox14.Value = "= Fields.AtributoCadena";
            // 
            // dsAtributos
            // 
            this.dsAtributos.DataMember = "PlanillaPagadaAtributos";
            this.dsAtributos.DataSource = typeof(Intelligob.Cliente.Depositos.RecaudacionesDep);
            this.dsAtributos.Name = "dsAtributos";
            this.dsAtributos.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pPlanilla", typeof(int), "=Parameters.pPlanilla.Value")});
            // 
            // table2
            // 
            this.table2.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(2.0604162216186523D)));
            this.table2.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.73750030994415283D)));
            this.table2.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20003964006900787D)));
            this.table2.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.20833328366279602D)));
            this.table2.Body.SetCellContent(0, 0, this.textBox13);
            this.table2.Body.SetCellContent(0, 1, this.textBox16);
            this.table2.Body.SetCellContent(1, 0, this.textBox22);
            this.table2.Body.SetCellContent(1, 1, this.textBox23);
            tableGroup4.Name = "tableGroup2";
            tableGroup4.ReportItem = this.textBox11;
            tableGroup5.Name = "tableGroup3";
            tableGroup5.ReportItem = this.textBox15;
            this.table2.ColumnGroups.Add(tableGroup4);
            this.table2.ColumnGroups.Add(tableGroup5);
            this.table2.DataSource = this.dsRubros;
            this.table2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox16,
            this.textBox22,
            this.textBox23,
            this.textBox11,
            this.textBox15});
            this.table2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.7000396251678467D), Telerik.Reporting.Drawing.Unit.Inch(0.0999603271484375D));
            this.table2.Name = "table2";
            tableGroup7.Name = "group";
            tableGroup6.ChildGroups.Add(tableGroup7);
            tableGroup6.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup6.Name = "detailTableGroup1";
            tableGroup9.Name = "group3";
            tableGroup8.ChildGroups.Add(tableGroup9);
            tableGroup8.Name = "group2";
            this.table2.RowGroups.Add(tableGroup6);
            this.table2.RowGroups.Add(tableGroup8);
            this.table2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.7979164123535156D), Telerik.Reporting.Drawing.Unit.Inch(0.60841244459152222D));
            // 
            // textBox13
            // 
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0604162216186523D), Telerik.Reporting.Drawing.Unit.Inch(0.20003965497016907D));
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.TextWrap = false;
            this.textBox13.Value = "= Fields.RubroNav.Denoninacion";
            // 
            // textBox16
            // 
            this.textBox16.Format = "{0:C2}";
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.73750007152557373D), Telerik.Reporting.Drawing.Unit.Inch(0.20003965497016907D));
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "= Fields.ValorRedondeado * Fields.Origen";
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.0604162216186523D), Telerik.Reporting.Drawing.Unit.Inch(0.20833331346511841D));
            this.textBox22.Style.Font.Bold = false;
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox22.StyleName = "";
            this.textBox22.Value = "Total";
            // 
            // textBox23
            // 
            this.textBox23.Format = "{0:C2}";
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.73750030994415283D), Telerik.Reporting.Drawing.Unit.Inch(0.20833331346511841D));
            this.textBox23.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.Font.Bold = false;
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(12D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.textBox23.StyleName = "";
            this.textBox23.Value = "= Sum(Fields.ValorRedondeado * Fields.Origen)";
            // 
            // dsRubros
            // 
            this.dsRubros.DataMember = "PlanillaPagadaRubros";
            this.dsRubros.DataSource = typeof(Intelligob.Cliente.Depositos.RecaudacionesDep);
            this.dsRubros.Name = "dsRubros";
            this.dsRubros.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("pPlanilla", typeof(int), "=Parameters.pPlanilla.Value")});
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.40000024437904358D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox19,
            this.barcode1});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.299999862909317D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.2999999523162842D), Telerik.Reporting.Drawing.Unit.Inch(0.19999997317790985D));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox19.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "Recaudacion";
            // 
            // barcode1
            // 
            code128Encoder1.ShowText = false;
            this.barcode1.Encoder = code128Encoder1;
            this.barcode1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.9000003337860107D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.barcode1.Name = "barcode1";
            this.barcode1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6999603509902954D), Telerik.Reporting.Drawing.Unit.Inch(0.39996087551116943D));
            this.barcode1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcode1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.barcode1.Value = "= Fields.Concepto + \'-\' + Fields.Id";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Inch(0.800000011920929D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.panel2});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // panel2
            // 
            this.panel2.Anchoring = ((Telerik.Reporting.AnchoringStyles)(((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Left) 
            | Telerik.Reporting.AnchoringStyles.Right)));
            this.panel2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox30,
            this.textBox29,
            this.textBox28,
            this.textBox27,
            this.textBox31,
            this.textBox26,
            this.textBox25,
            this.textBox24});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9378803194267675E-05D), Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4999213218688965D), Telerik.Reporting.Drawing.Unit.Inch(0.7999606728553772D));
            this.panel2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox30
            // 
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.929932790924795E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "Contribuyente";
            // 
            // textBox29
            // 
            this.textBox29.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Left)));
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0001181364059448D), Telerik.Reporting.Drawing.Unit.Inch(0.1000000610947609D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4997634887695312D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox29.Style.Font.Bold = true;
            this.textBox29.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox29.Value = "= Fields.ContribuyentesCadena";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9219856262207031E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.29996061325073242D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "Concepto";
            // 
            // textBox27
            // 
            this.textBox27.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Left)));
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0001181364059448D), Telerik.Reporting.Drawing.Unit.Inch(0.29996061325073242D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.299842357635498D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "= Fields.ConceptoNav.Denominacion";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.3998818397521973D), Telerik.Reporting.Drawing.Unit.Inch(0.29996061325073242D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(0.49992117285728455D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "Fecha";
            // 
            // textBox26
            // 
            this.textBox26.Format = "{0:d}";
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(4.8998818397521973D), Telerik.Reporting.Drawing.Unit.Inch(0.29996061325073242D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6000000238418579D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox26.Style.Font.Bold = true;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "= Fields.FechaCancelacion";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.929932790924795E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.49996057152748108D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "Comentarios";
            // 
            // textBox24
            // 
            this.textBox24.Anchoring = ((Telerik.Reporting.AnchoringStyles)((Telerik.Reporting.AnchoringStyles.Top | Telerik.Reporting.AnchoringStyles.Left)));
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.0001181364059448D), Telerik.Reporting.Drawing.Unit.Inch(0.49996057152748108D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(5.4997634887695312D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.textBox24.Style.Font.Bold = false;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.TextWrap = false;
            this.textBox24.Value = "= Fields.Comentarios";
            // 
            // Planilla
            // 
            this.DataSource = this.dsPlanilla;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail,
            this.pageHeaderSection1,
            this.pageFooterSection1});
            this.Name = "Planilla";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(0.40000000596046448D), Telerik.Reporting.Drawing.Unit.Inch(0.30000001192092896D), Telerik.Reporting.Drawing.Unit.Inch(1.9600000381469727D), Telerik.Reporting.Drawing.Unit.Inch(0.60000002384185791D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(8.34000015258789D), Telerik.Reporting.Drawing.Unit.Inch(8.3000001907348633D));
            reportParameter1.Name = "pPlanilla";
            reportParameter1.Text = "Planilla";
            reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            reportParameter1.Value = "0";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(58)))), ((int)(((byte)(112)))));
            styleRule1.Style.Font.Name = "Tahoma";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(58)))), ((int)(((byte)(112)))));
            styleRule2.Style.Color = System.Drawing.Color.White;
            styleRule2.Style.Font.Name = "Tahoma";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Color = System.Drawing.Color.Black;
            styleRule3.Style.Font.Name = "Tahoma";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.Black;
            styleRule4.Style.Font.Name = "Tahoma";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.UnitOfMeasure = Telerik.Reporting.Drawing.UnitType.Inch;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.5D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource dsPlanilla;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.ObjectDataSource dsRubros;
        private Telerik.Reporting.ObjectDataSource dsAtributos;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.Table table2;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.Barcode barcode1;

    }
}