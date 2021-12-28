namespace Intelligob.Reportes.Agua
{
    /// <summary>
    /// Summary description for AguaCuentaFicha.
    /// </summary>
    public partial class AguaCuentaFicha : Telerik.Reporting.Report
    {
        public AguaCuentaFicha()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
        }

        private void imLogo_ItemDataBinding(object sender, System.EventArgs e)
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