using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ItmView.Views
{
    public class BooleanToYesNoConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }
            return "No";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.Equals("Yes", StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
} 