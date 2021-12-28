namespace Intelligob.Reportes.Recaudaciones
{

    /// <summary>
    /// Summary description for ConsultaDeuda.
    /// </summary>
    public partial class ConsultaDeuda : Telerik.Reporting.Report
    {
        public ConsultaDeuda()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

        }

        private void reportNameTextBox_ItemDataBinding(object sender, System.EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            Intelligob.Cliente.Depositos.TablasDep td = new Intelligob.Cliente.Depositos.TablasDep();
            txt.Value = td.NombreEmpresa;
        }
    }
}