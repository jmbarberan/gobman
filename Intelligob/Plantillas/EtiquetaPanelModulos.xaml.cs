using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Intelligob.Escritorio.Plantillas
{
    /// <summary>
    /// Lógica de interacción para EtiquetaPanelModulos.xaml
    /// </summary>
    public partial class EtiquetaPanelModulos : UserControl
    {
        public EtiquetaPanelModulos()
        {
            InitializeComponent();
        }

        public void CambiarIcono(Boolean pExpandido)
        {
            if (pExpandido)
            {
                imgIcono.Source = new BitmapImage(new Uri("pack://application:,,,/Plantillas/fcDerechag.png"));
            }
            else
            {
                imgIcono.Source = new BitmapImage(new Uri("pack://application:,,,/Plantillas/fcIzquierdag.png"));
            }
        }
    }
}
