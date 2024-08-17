using System.Globalization;

namespace MauiPersianToolkit.Converters;

public class FetchPropertyValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return value;

        if (value is object && parameter is Binding binding && binding.Source is IText lbl)
            return value.GetType().GetProperty(lbl.Text).GetValue(value);

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
