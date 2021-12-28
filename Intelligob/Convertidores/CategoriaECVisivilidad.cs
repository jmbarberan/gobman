using System;
using System.Globalization;
using System.Linq;

namespace Intelligob.Escritorio.Convertidores
{
    public class CategoriaECVisivilidad : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Visibility ret = System.Windows.Visibility.Collapsed;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(System.Windows.Visibility))
            {
                ret = System.Windows.Visibility.Collapsed;
            }
            if (value != null && (int)value > 0)
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
