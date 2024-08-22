using System.Globalization;

namespace MauiPersianToolkit.Converters;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool)
            return !(bool)value;

        if (value is string)
            return string.IsNullOrEmpty(value?.ToString());

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
