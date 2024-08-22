using System.Globalization;

namespace MauiPersianToolkit.Converters;

public class PersianDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            string persianDate = date.ToPersianDate();

            return persianDate;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string persianDate && !string.IsNullOrEmpty(value?.ToString()))
        {
            DateTime date = persianDate.ToDateTime();
            return date;
        }

        return value;
    }
}
