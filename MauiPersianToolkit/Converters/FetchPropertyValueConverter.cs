using System.Globalization;

namespace MauiPersianToolkit.Converters;

public class FetchPropertyValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return value;

        if (value is object && parameter is Binding binding && binding.Source is not null)
        {
            var bindingSource = ((Binding)parameter).Source.GetType();
            var prop = bindingSource.GetProperty(((Binding)parameter).Path).GetValue(((Binding)parameter).Source);
            return value.GetType().GetProperty(prop.ToString()).GetValue(value);
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
