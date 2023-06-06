using System.Globalization;

namespace PersianUIControlsMaui.Converters;

public class PersianDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            string persianDate = date.ToPersianDateTime();

            return persianDate;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string persianDate)
        {
            DateTime date = persianDate.ToDateTime();

            return date;
        }

        return value;
    }
}
