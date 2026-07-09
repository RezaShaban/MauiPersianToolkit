using MauiPersianToolkit.Enums;
using MauiPersianToolkit.Services.Calendar;
using MauiPersianToolkit.Models;
using MauiPersianToolkit.ViewModels;
using SelectionMode = MauiPersianToolkit.Enums.SelectionMode;

namespace MauiPersianToolkit.Examples;

/// <summary>
/// Example usage of the calendar system
/// </summary>
public class CalendarServiceExamples
{
    /// <summary>
    /// Example 1: Using extension methods (simplest approach)
    /// </summary>
    public static void ExtensionMethodsExample()
    {
        DateTime now = DateTime.Now;

        // Convert to Persian date
        string persianDate = now.ToPersianDate();
        Console.WriteLine($"Persian: {persianDate}");

        // Convert back
        DateTime parsed = persianDate.ToDateTime();
        Console.WriteLine($"Parsed: {parsed}");
    }

    /// <summary>
    /// Example 2: Using calendar service factory directly
    /// </summary>
    public static void CalendarServiceFactoryExample()
    {
        DateTime now = DateTime.Now;

        // Get Persian calendar service
        var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
        Console.WriteLine($"Persian Date: {persianService.ToCalendarDate(now)}");
        Console.WriteLine($"Year: {persianService.GetYear(now)}");
        Console.WriteLine($"Month: {persianService.GetMonthName(persianService.GetMonth(now))}");

        // Get Gregorian calendar service
        var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
        Console.WriteLine($"Gregorian Date: {gregorianService.ToCalendarDate(now)}");

        // Get Hijri calendar service
        var hijriService = CalendarServiceFactory.GetService(CalendarType.Hijri);
        Console.WriteLine($"Hijri Date: {hijriService.ToCalendarDate(now)}");
    }

    /// <summary>
    /// Example 3: Converting between different calendar types
    /// </summary>
    public static void ConversionBetweenCalendarTypesExample()
    {
        DateTime today = DateTime.Now;

        // Convert to different calendar types
        string persian = today.ToCalendarDate(CalendarType.Persian);
        string gregorian = today.ToCalendarDate(CalendarType.Gregorian);
        string hijri = today.ToCalendarDate(CalendarType.Hijri);

        Console.WriteLine($"Today in Persian: {persian}");
        Console.WriteLine($"Today in Gregorian: {gregorian}");
        Console.WriteLine($"Today in Hijri: {hijri}");
    }

    /// <summary>
    /// Example 4: Working with DatePicker control
    /// </summary>
    public static void DatePickerControlExample()
    {
        // Create calendar options for different calendar types
        var persianOptions = new CalendarOptions
        {
            CalendarType = CalendarType.Persian,
            SelectionMode = SelectionMode.Single,
            SelectDateMode = SelectionDateMode.Day
        };

        var gregorianOptions = new CalendarOptions
        {
            CalendarType = CalendarType.Gregorian,
            SelectionMode = SelectionMode.Single,
            SelectDateMode = SelectionDateMode.Day
        };

        var hijriOptions = new CalendarOptions
        {
            CalendarType = CalendarType.Hijri,
            SelectionMode = SelectionMode.Single,
            SelectDateMode = SelectionDateMode.Day
        };

        // Each calendar type will work automatically with the DatePickerViewModel
        var persianViewModel = new DatePickerViewModel(persianOptions);
        var gregorianViewModel = new DatePickerViewModel(gregorianOptions);
        var hijriViewModel = new DatePickerViewModel(hijriOptions);
    }

    /// <summary>
    /// Example 5: Registering a custom calendar service
    /// </summary>
    public static void CustomCalendarServiceExample()
    {
        // Create your custom calendar service
        // var customService = new MyCustomCalendarService();

        // Register it
        // CalendarServiceFactory.RegisterService(CalendarType.Hijri, customService);

        // Now use it
        // var service = CalendarServiceFactory.GetService(CalendarType.Hijri);
    }

    /// <summary>
    /// Example 6: Getting month and year lists
    /// </summary>
    public static void MonthAndYearListsExample()
    {
        var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
        var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);

        // Get all month names for Persian calendar
        var persianMonths = persianService.GetAllMonthNames();
        Console.WriteLine("Persian Months:");
        foreach (var month in persianMonths)
        {
            Console.WriteLine($"  - {month}");
        }

        // Get all month names for Gregorian calendar
        var gregorianMonths = gregorianService.GetAllMonthNames();
        Console.WriteLine("Gregorian Months:");
        foreach (var month in gregorianMonths)
        {
            Console.WriteLine($"  - {month}");
        }
    }

    /// <summary>
    /// Example 7: Month calculations
    /// </summary>
    public static void MonthCalculationsExample()
    {
        var service = CalendarServiceFactory.GetService(CalendarType.Persian);
        var date = DateTime.Now;

        // Get beginning and ending of month
        string monthStart = service.GetMonthBeginning(date);
        string monthEnd = service.GetMonthEnding(date);

        Console.WriteLine($"Month starts at: {monthStart}");
        Console.WriteLine($"Month ends at: {monthEnd}");

        // Get days in month
        int year = service.GetYear(date);
        int month = service.GetMonth(date);
        int daysInMonth = service.GetDaysInMonth(year, month);

        Console.WriteLine($"Days in month: {daysInMonth}");
    }

    /// <summary>
    /// Example 8: Holiday detection
    /// </summary>
    public static void HolidayDetectionExample()
    {
        // Persian and Hijri calendars have Friday as holiday
        var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
        var holidayDay = persianService.GetLastDayOfWeek();
        Console.WriteLine($"Persian calendar holiday: {persianService.GetDayOfWeekName(holidayDay)}");

        // Gregorian calendar has Sunday as holiday
        var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
        var gregorianHoliday = gregorianService.GetLastDayOfWeek();
        Console.WriteLine($"Gregorian calendar holiday: {gregorianService.GetDayOfWeekName(gregorianHoliday)}");
    }
}
