namespace Intelligob.Reportes.Recaudaciones
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for CuentaCorrienteMercadoPuesto.
    /// </summary>
    public partial class CuentaCorrienteMercadoPuesto : Telerik.Reporting.Report
    {
        public CuentaCorrienteMercadoPuesto()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void textBox6_ItemDataBinding(object sender, EventArgs e)
        {

        }

        private void pictureBox1_ItemDataBinding(object sender, EventArgs e)
        {

        }
    }
}