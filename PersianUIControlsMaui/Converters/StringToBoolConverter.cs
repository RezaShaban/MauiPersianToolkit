using System.Globalization;

namespace PersianUIControlsMaui.Converters;

public class StringToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var stringValue = ((value ?? "") as string).Replace(" ", "");

        return !string.IsNullOrEmpty(stringValue);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
