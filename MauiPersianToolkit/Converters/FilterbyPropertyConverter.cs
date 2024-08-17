using System.Collections;
using System.Globalization;

namespace MauiPersianToolkit.Converters;

public class FilterbyPropertyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
            return value;

        if (value is IList<object> list && parameter is Binding binding && binding.Source is IText lbl)
            return list.Where(x => x.GetType().GetProperty(parameter.ToString()).GetValue(x) == null)
                .GetType()
                .GetProperty(lbl.Text)
                .GetValue(value);

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}
