using System;
using System.Globalization;
using System.Linq;

namespace Intelligob.Escritorio.Convertidores
{
    class OrigenNegativoConvertidor
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = false;
            }
            if ((int)value == 1)
            {
                ret = true;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret = 0;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = 0;
            }
            if ((Boolean)parameter == true)
                ret = 1;
            return ret;
        }
    }
}
