namespace Intelligob.Reportes.Emisiones
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for DiarioBajasFecha.
    /// </summary>
    public partial class DiarioBajasFecha : Telerik.Reporting.Report
    {
        public DiarioBajasFecha()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

        }

        private void textBox6_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            Intelligob.Cliente.Depositos.TablasDep td = new Intelligob.Cliente.Depositos.TablasDep();
            txt.Value = td.NombreEmpresa;
        }

        private void txtUsuario_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            string usr = "jmbarberan";
            if (!Intelligob.Cliente.SesionUtiles.Instance.EsDesarrollador)
                usr = Intelligob.Cliente.SesionUtiles.Instance.UsuarioActivo.Nombres;
            txt.Value = usr;
        }
    }
}