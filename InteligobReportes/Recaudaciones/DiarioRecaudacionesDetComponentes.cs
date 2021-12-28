namespace Intelligob.Reportes.Recaudaciones
{
    using System;

    /// <summary>
    /// Summary description for Report1.
    /// </summary>
    public partial class DiarioRecaudacionesDetComponentes : Telerik.Reporting.Report
    {
        public DiarioRecaudacionesDetComponentes()
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
            Telerik.Reporting.Processing.TextBox txt = (Telerik.Reporting.Processing.TextBox)sender;
            Intelligob.Cliente.Depositos.TablasDep td = new Intelligob.Cliente.Depositos.TablasDep();
            txt.Value = td.NombreEmpresa;
        }

        private void pictureBox1_ItemDataBinding(object sender, EventArgs e)
        {
            Telerik.Reporting.Processing.PictureBox pb = (Telerik.Reporting.Processing.PictureBox)sender;
            Cliente.Depositos.TablasDep td = new Cliente.Depositos.TablasDep();
            Cliente.Referencia.PredioFotoDto p = td.LogoX48();
            if (p != null)
            {
                byte[] btlogo = p.Foto;
                if (btlogo != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(btlogo);
                    System.Drawing.Bitmap b = new System.Drawing.Bitmap(ms);
                    pb.Image = b;
                }
            }
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