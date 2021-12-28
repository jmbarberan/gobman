using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Intelligob.Escritorio.Convertidores
{
    public class ConvertidorLogicoEditable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = false;
            }
            if ((int)value == 0)
            {
                ret = true;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret = 1;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = 1;
            }
            if ((Boolean)parameter == true)
                ret = 0;
            return ret;
        }
    }
}
