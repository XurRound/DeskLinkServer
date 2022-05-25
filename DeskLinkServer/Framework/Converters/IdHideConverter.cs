using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DeskLinkServer.Framework.Converters
{
    public class IdHideConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string).Substring(0, 12) + "********";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
