using System;
using System.Globalization;
using Xamarin.Forms;

namespace TechnoRely.Converters
{
    public class ListenersToFormattedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long val)
                return $"({val} listeners)";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}