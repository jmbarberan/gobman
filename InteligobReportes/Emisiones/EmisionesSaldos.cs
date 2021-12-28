namespace Intelligob.Reportes.Emisiones
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for EmisionesSaldos.
    /// </summary>
    public partial class EmisionesSaldos : Telerik.Reporting.Report
    {
        public EmisionesSaldos()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

        }

        private void txtUsuario_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            string usr = "jmbarberan";
            if (!Intelligob.Cliente.SesionUtiles.Instance.EsDesarrollador)
                usr = Intelligob.Cliente.SesionUtiles.Instance.UsuarioActivo.Nombres;
            txt.Value = usr;
        }

        private void txtInstitucion_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            Intelligob.Cliente.Depositos.TablasDep td = new Intelligob.Cliente.Depositos.TablasDep();
            txt.Value = td.NombreEmpresa;
        }

        private void pictureBox1_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.PictureBox pb = (Telerik.Reporting.Processing.PictureBox)sender;
            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
            byte[] btlogo = td.LogoX48().Foto;
            if (btlogo != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(btlogo);
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(ms);
                pb.Image = b;
            }
        }
    }
}