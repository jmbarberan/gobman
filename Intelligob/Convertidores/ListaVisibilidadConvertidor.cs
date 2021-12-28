using System;
using System.Globalization;
using System.Linq;

namespace Intelligob.Escritorio.Convertidores
{
    public class ListaVisibilidadConvertidor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Visibility ret = System.Windows.Visibility.Collapsed;
            if (value == null || value.GetType() != typeof(System.Collections.Generic.IList<Utiles.ElementoSeleccion>) && targetType != typeof(System.Windows.Visibility))
            {
                ret = System.Windows.Visibility.Collapsed;
            }
            if (value != null && ((System.Collections.Generic.List<Utiles.ElementoSeleccion>)value).Count > 0)
            {
                ret = System.Windows.Visibility.Visible;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
