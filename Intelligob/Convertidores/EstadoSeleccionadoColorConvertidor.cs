using System;
using System.Linq;

namespace Intelligob.Escritorio.Convertidores
{
    public class EstadoSeleccionadoColorConvertidor : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
               object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.Brush res = System.Windows.SystemColors.ControlTextBrush;
            if (values[0] is int && values[1] is bool)
            {
                if ((bool)values[1] == true)
                {
                    if ((int)values[0] == 1)
                        res = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    else
                        res = System.Windows.SystemColors.HighlightTextBrush;
                }
                else
                {
                    if ((int)values[0] == 1)
                        res = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.RoyalBlue);
                }
            }
            
            return res;
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
               object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }
    }
}
