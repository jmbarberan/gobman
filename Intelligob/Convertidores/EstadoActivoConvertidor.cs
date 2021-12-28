using System;
using System.Globalization;
using System.Windows.Data;

namespace Intelligob.Escritorio.Convertidores
{
    public class EstadoActivoConvertidor : IValueConverter
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
            int ret = 2;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = 2;
            }
            if ((Boolean)parameter == true)
                ret = 0;
            return ret;
        }
    }

    public class EstadoEliminadoConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = false;
            }
            if ((int)value == 2)
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
                ret = 2;
            return ret;
        }
    }

    public class EstadoSuspendidoConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean ret = false;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = false;
            }
            if ((int)value == 2 || (int)value == 1)
            {
                ret = true;
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret = 2;
            if (value == null || value.GetType() != typeof(int) && targetType != typeof(Boolean))
            {
                ret = 2;
            }
            if ((Boolean)parameter == true)
                ret = 0;
            return ret;
        }
    }    

    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
