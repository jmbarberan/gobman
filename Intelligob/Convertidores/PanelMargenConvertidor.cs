using System;
using System.Windows;
using System.Windows.Data;

namespace Intelligob.Escritorio.Convertidores
{
    public class PanelMargenConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double width = (double)value;
                Thickness panelMarginThickness = new Thickness(width * 1, 0, 0, 0);
                return panelMarginThickness;
            }

            throw new NotImplementedException();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PanelMargenNegativoConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double width = (double)value;
                Thickness panelMarginThickness = new Thickness(width * 1, 0, 0, 0);
                return panelMarginThickness;
            }

            throw new NotImplementedException();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Thickness)
            {
                double r = ((Thickness)value).Left;
                return (double)r;
            }

            throw new NotImplementedException();
        }
    }
}
