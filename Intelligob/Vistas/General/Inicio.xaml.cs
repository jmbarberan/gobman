using Intelligob.Utiles;
using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Intelligob.Escritorio.Vistas.General
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Page
    {
        public Inicio()
        {
            InitializeComponent();
            if (!String.IsNullOrWhiteSpace(Configuracion.ImagenInicio))
            {
                if (System.IO.File.Exists(Configuracion.ImagenInicio))
                    this.imgInicio.Source = new BitmapImage(new Uri(Configuracion.ImagenInicio));
            }   
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Seleccione una foto";
            op.Filter = "Todas las imagenes |*.jpg;*.jpeg; *.png; | " +
                "JPEG (*.jpg,*.jpeg) |*.jpg;*.jpeg | PNG (*.png) | *.png";
            op.ShowDialog();
            if (!String.IsNullOrWhiteSpace(op.FileName))
            {
                Configuracion.ImagenInicio = op.FileName;
                this.imgInicio.Source = new BitmapImage(new Uri(op.FileName));                
            }
        }
    }
}
