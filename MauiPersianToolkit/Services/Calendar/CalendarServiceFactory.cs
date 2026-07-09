using MauiPersianToolkit.Enums;

namespace MauiPersianToolkit.Services.Calendar;

/// <summary>
/// Factory for creating calendar service instances
/// Implements Factory Pattern
/// </summary>
public class CalendarServiceFactory
{
    private static readonly Dictionary<CalendarType, ICalendarService> _services = new();

    static CalendarServiceFactory()
    {
        _services[CalendarType.Persian] = new PersianCalendarService();
        _services[CalendarType.Gregorian] = new GregorianCalendarService();
        _services[CalendarType.Hijri] = new HijriCalendarService();
    }

    /// <summary>
    /// Gets the calendar service for the specified calendar type
    /// </summary>
    public static ICalendarService GetService(CalendarType calendarType)
    {
        if (!_services.TryGetValue(calendarType, out var service))
        {
            throw new NotSupportedException($"Calendar type '{calendarType}' is not supported.");
        }
        return service;
    }

    /// <summary>
    /// Registers a custom calendar service
    /// </summary>
    public static void RegisterService(CalendarType calendarType, ICalendarService service)
    {
        _services[calendarType] = service ?? throw new ArgumentNullException(nameof(service));
    }
}
