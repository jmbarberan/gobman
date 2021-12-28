using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Intelligob.Escritorio.Convertidores
{
    public class LogicoNegritaConvertidor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontWeight ret = FontWeights.Normal;
            if (value != null && value.GetType() == typeof(Boolean) && targetType == typeof(FontWeight))
            {
                if ((Boolean)value == true)
                    ret = FontWeights.Bold;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value != null && value.GetType() == typeof(FontWeight) && targetType != typeof(Boolean))
            {
                if ((FontWeight)value == FontWeights.Bold)
                    ret = true;
            }
            return ret;
        }
    }

    public class LogicoColorConvertidor : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Windows.Media.Brush ret = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DimGray);
            if (value != null && value.GetType() == typeof(Boolean) && targetType == typeof(System.Windows.Media.Brush))
            {
                if ((Boolean)value == true)
                    ret = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.RoyalBlue);
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value != null && value.GetType() == typeof(System.Windows.Media.Brush) && targetType != typeof(Boolean))
            {
                if ((System.Windows.Media.Brush)value == new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.RoyalBlue))
                    ret = true;
            }
            return ret;
        }
    }
}
