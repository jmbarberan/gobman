using System;
using System.Globalization;
using System.Windows.Data;

namespace Intelligob.Escritorio.Convertidores
{
    public class NegativoConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(double) && targetType != typeof(double))
            {
                return false;
            } 
            return -(double)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(double) && targetType != typeof(double))
            {
                return false;
            }
            return -(double)value;
        }
    }

    public class NegativoLogicoConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(bool) && targetType != typeof(bool))
            {
                return true;
            }
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(bool) && targetType != typeof(bool))
            {
                return true;
            }
            return !(bool)value;
        }
    }

}
