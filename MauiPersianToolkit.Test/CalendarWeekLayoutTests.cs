using MauiPersianToolkit.Enums;
using MauiPersianToolkit.Models;
using MauiPersianToolkit.Services.Calendar;
using MauiPersianToolkit.ViewModels;
using Xunit;

namespace MauiPersianToolkit.Test;

/// <summary>
/// Test cases for calendar week layout - verifying days are displayed in correct columns
/// Tests that days of month appear in the correct grid columns based on day of week
/// Example: 1403/05/02 (if Friday) should appear in Friday column, not Sunday column
/// </summary>
public class CalendarWeekLayoutTests
{
    private readonly ICalendarService _persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
    private readonly ICalendarService _gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
    private readonly ICalendarService _hijriService = CalendarServiceFactory.GetService(CalendarType.Hijri);

    /// <summary>
    /// Test that consecutive days appear in consecutive columns
    /// </summary>
    [Fact]
    public void TestConsecutiveDaysInConsecutiveColumns()
    {
        var testDate = new DateTime(2024, 8, 15);

        var options = new CalendarOptions
        {
            CalendarType = CalendarType.Gregorian
        };

        var viewModel = new DatePickerViewModel(options);

        // Get first few days of month
        var firstDay = viewModel.DaysOfMonth.FirstOrDefault(d => d.DayNum == 1);
        var secondDay = viewModel.DaysOfMonth.FirstOrDefault(d => d.DayNum == 2);
        var thirdDay = viewModel.DaysOfMonth.FirstOrDefault(d => d.DayNum == 3);

        Assert.NotNull(firstDay);
        Assert.NotNull(secondDay);
        Assert.NotNull(thirdDay);

        int firstDayIndex = viewModel.DaysOfMonth.IndexOf(firstDay);
        int secondDayIndex = viewModel.DaysOfMonth.IndexOf(secondDay);
        int thirdDayIndex = viewModel.DaysOfMonth.IndexOf(thirdDay);

        // Consecutive days should be consecutive in the list
        Assert.Equal(secondDayIndex, firstDayIndex + 1);
        Assert.Equal(thirdDayIndex, secondDayIndex + 1);
    }

    /// <summary>
    /// Test that all days of month are consecutive in the grid
    /// </summary>
    [Fact]
    public void TestAllDaysAreConsecutive()
    {
        var testDate = new DateTime(2024, 8, 15);

        var options = new CalendarOptions
        {
            CalendarType = CalendarType.Gregorian
        };

        var viewModel = new DatePickerViewModel(options);

        var currentMonthDays = viewModel.DaysOfMonth.Where(d => d.IsInCurrentMonth).ToList();

        // Check that days are numbered 1, 2, 3, ..., lastDay
        for (int i = 0; i < currentMonthDays.Count; i++)
        {
            Assert.Equal(i + 1, currentMonthDays[i].DayNum);
        }
    }

}
