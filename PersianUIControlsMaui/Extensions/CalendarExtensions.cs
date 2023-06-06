using System.Globalization;

namespace PersianUIControlsMaui;
public static class CalendarExtensions
{
    public static string ToPersianDate(this DateTime _date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            var year = pc.GetYear(_date);
            var month = pc.GetMonth(_date);
            var day = pc.GetDayOfMonth(_date);
            return $"{year}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')}";
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string ToPersianDateTime(this DateTime _date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            var year = pc.GetYear(_date);
            var month = pc.GetMonth(_date);
            var day = pc.GetDayOfMonth(_date);
            return $"{year}/{month.ToString().PadLeft(2, '0')}/{day.ToString().PadLeft(2, '0')} {_date.Hour.ToString().PadLeft(2, '0')}:{_date.Minute.ToString().PadLeft(2, '0')}";
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static DateTime ToDateTime(this string persianDate)
    {
        try
        {
            int hour = 0; int minute = 0;
            if (!persianDate.Contains("/") || persianDate.Split('/').Length != 3)
                return DateTime.Now;
            if (persianDate.Contains(':'))
            {
                persianDate = persianDate.Split(' ').Length > 0 ? persianDate.Split(' ')[0] : persianDate;
                try
                {
                    var time = persianDate.Split(' ').Length > 0 ? persianDate.Split(' ')[1] : "";
                    hour = int.Parse(time.Split(':')[0]);
                    minute = int.Parse(time.Split(':')[1]);
                }
                catch { }
            }
            PersianCalendar pc = new PersianCalendar();
            var date = persianDate.Split('/');

            return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), hour, minute, 0, pc);
        }
        catch (Exception) { return DateTime.Now; }
    }

    public static string GetPersianBeginningMonth(this DateTime date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);

            return $"{year}/{month.ToString().PadLeft(2, '0')}/01";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public static DayOfWeek GetPersianDay(this DateTime date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDayOfWeek(date);
        }
        catch (Exception)
        {
            return DayOfWeek.Friday;
        }
    }

    public static string GetPersianYear(this DateTime date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            var year = pc.GetYear(date);
            return $"{year}";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public static int GetPersianMonth(this DateTime date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonth(date);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static int GetPersianDayOfMonth(this DateTime date)
    {
        try
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetDayOfMonth(date);
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
