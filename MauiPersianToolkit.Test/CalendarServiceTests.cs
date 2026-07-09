using MauiPersianToolkit.Enums;
using MauiPersianToolkit.Models;
using MauiPersianToolkit.Services.Calendar;
using MauiPersianToolkit.ViewModels;
using Xunit;

namespace MauiPersianToolkit.Test;

/// <summary>
/// Test cases for calendar services with different calendar types
/// </summary>
public class CalendarServiceTests
{
    private readonly ICalendarService _persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
    private readonly ICalendarService _gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
    private readonly ICalendarService _hijriService = CalendarServiceFactory.GetService(CalendarType.Hijri);

    /// <summary>
    /// Test date conversion roundtrip (DateTime -> String -> DateTime)
    /// </summary>
    [Fact]
    public void TestDateConversionRoundtrip()
    {
        var testDate = new DateTime(2024, 8, 15);

        // Persian
        var persianStr = _persianService.ToCalendarDate(testDate);
        var persianConverted = _persianService.ToGregorianDate(persianStr);
        Assert.Equal(testDate.Date, persianConverted.Date);

        // Gregorian
        var gregorianStr = _gregorianService.ToCalendarDate(testDate);
        var gregorianConverted = _gregorianService.ToGregorianDate(gregorianStr);
        Assert.Equal(testDate.Date, gregorianConverted.Date);

        // Hijri
        var hijriStr = _hijriService.ToCalendarDate(testDate);
        var hijriConverted = _hijriService.ToGregorianDate(hijriStr);
        Assert.Equal(testDate.Date, hijriConverted.Date);
    }

    /// <summary>
    /// Test month boundaries
    /// </summary>
    [Fact]
    public void TestMonthBoundaries()
    {
        var testDate = new DateTime(2024, 8, 15);

        // Persian
        var persianStart = _persianService.GetMonthBeginning(testDate);
        var persianEnd = _persianService.GetMonthEnding(testDate);
        Assert.NotEmpty(persianStart);
        Assert.NotEmpty(persianEnd);

        // Gregorian
        var gregorianStart = _gregorianService.GetMonthBeginning(testDate);
        var gregorianEnd = _gregorianService.GetMonthEnding(testDate);
        Assert.NotEmpty(gregorianStart);
        Assert.NotEmpty(gregorianEnd);

        // Hijri
        var hijriStart = _hijriService.GetMonthBeginning(testDate);
        var hijriEnd = _hijriService.GetMonthEnding(testDate);
        Assert.NotEmpty(hijriStart);
        Assert.NotEmpty(hijriEnd);
    }

    /// <summary>
    /// Test holiday detection
    /// </summary>
    [Fact]
    public void TestHolidayDetection()
    {
        // Persian calendar: Friday
        var persianHoliday = _persianService.GetLastDayOfWeek();
        Assert.Equal(DayOfWeek.Friday, persianHoliday);

        // Gregorian calendar: Saturday
        var gregorianHoliday = _gregorianService.GetLastDayOfWeek();
        Assert.Equal(DayOfWeek.Saturday, gregorianHoliday);

        // Hijri calendar: Friday
        var hijriHoliday = _hijriService.GetLastDayOfWeek();
        Assert.Equal(DayOfWeek.Friday, hijriHoliday);
    }

    /// <summary>
    /// Test month names
    /// </summary>
    [Fact]
    public void TestMonthNames()
    {
        // Persian
        var persianMonths = _persianService.GetAllMonthNames();
        Assert.Equal(12, persianMonths.Count());

        // Gregorian
        var gregorianMonths = _gregorianService.GetAllMonthNames();
        Assert.Equal(12, gregorianMonths.Count());

        // Hijri
        var hijriMonths = _hijriService.GetAllMonthNames();
        Assert.Equal(12, hijriMonths.Count());
    }

    /// <summary>
    /// Test leap year
    /// </summary>
    [Fact]
    public void TestLeapYear()
    {
        // Persian calendar leap years
        var persianLeap = _persianService.IsLeapYear(1403);
        
        // Gregorian calendar leap year
        var gregorianLeap = _gregorianService.IsLeapYear(2024);
        Assert.True(gregorianLeap);

        // Hijri calendar leap year
        var hijriLeap = _hijriService.IsLeapYear(1446);
    }

    /// <summary>
    /// Test DatePickerViewModel with different calendar types
    /// </summary>
    [Fact]
    public void TestDatePickerViewModelWithDifferentCalendars()
    {
        // Persian DatePicker
        var persianOptions = new CalendarOptions 
        { 
            CalendarType = CalendarType.Persian 
        };
        var persianViewModel = new DatePickerViewModel(persianOptions);
        Assert.NotEmpty(persianViewModel.CurrentMonth);
        Assert.True(persianViewModel.CurrentYear > 1000);

        // Gregorian DatePicker
        var gregorianOptions = new CalendarOptions 
        { 
            CalendarType = CalendarType.Gregorian 
        };
        var gregorianViewModel = new DatePickerViewModel(gregorianOptions);
        Assert.NotEmpty(gregorianViewModel.CurrentMonth);
        Assert.True(gregorianViewModel.CurrentYear > 2000);

        // Hijri DatePicker
        var hijriOptions = new CalendarOptions 
        { 
            CalendarType = CalendarType.Hijri 
        };
        var hijriViewModel = new DatePickerViewModel(hijriOptions);
        Assert.NotEmpty(hijriViewModel.CurrentMonth);
        Assert.True(hijriViewModel.CurrentYear > 1000);
    }

    /// <summary>
    /// Test date formatting with different calendars
    /// </summary>
    [Fact]
    public void TestDateFormatting()
    {
        var testDate = new DateTime(2024, 8, 15);

        // Persian
        var persianDate = _persianService.ToCalendarDate(testDate);
        Assert.Contains("/", persianDate);
        Assert.Equal(3, persianDate.Split('/').Length);

        // Gregorian
        var gregorianDate = _gregorianService.ToCalendarDate(testDate);
        Assert.Contains("/", gregorianDate);
        Assert.Equal(3, gregorianDate.Split('/').Length);

        // Hijri
        var hijriDate = _hijriService.ToCalendarDate(testDate);
        Assert.Contains("/", hijriDate);
        Assert.Equal(3, hijriDate.Split('/').Length);
    }
}
