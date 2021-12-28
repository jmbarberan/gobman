using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Intelligob.Escritorio.Plantillas
{
    /// <summary>
    /// Lógica de interacción para EtiquetaPanelFunciones.xaml
    /// </summary>
    public partial class EtiquetaPanelFunciones : UserControl
    {
        public EtiquetaPanelFunciones()
        {
            InitializeComponent();
        }

        public void CambiarIcono(Boolean pExpandido)
        {
            if (pExpandido)
            {
                imgIcono.Source = new BitmapImage(new Uri("pack://application:,,,/Plantillas/fcIzquierdag.png"));
            }
            else
            {
                imgIcono.Source = new BitmapImage(new Uri("pack://application:,,,/Plantillas/fcDerechag.png"));
            }
        }
    }
}
