namespace Intelligob.Reportes.Catastros
{
    using System;

    /// <summary>
    /// Summary description for CertificadoAvaluos.
    /// </summary>
    public partial class CertificadoAvaluos : Telerik.Reporting.Report
    {
        public CertificadoAvaluos()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

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

        private void textBox1_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            Intelligob.Cliente.Depositos.TablasDep td = new Intelligob.Cliente.Depositos.TablasDep();
            txt.Value = td.NombreEmpresa;
        }
    }
}