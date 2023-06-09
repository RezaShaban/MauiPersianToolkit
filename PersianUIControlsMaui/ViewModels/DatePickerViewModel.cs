using PersianUIControlsMaui.Enums;
using PersianUIControlsMaui.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersianUIControlsMaui.ViewModels;

public class DatePickerViewModel : ObservableObject
{
    #region Propertie's

    #region Field's
    private int currentYear;
    private string currentMonth;
    private SelectionDateMode selectDateMode;
    private CalendarOptions options;
    #endregion

    public int CurrentYear { get => currentYear; set => SetProperty(ref currentYear, value); }
    public string CurrentMonth { get => currentMonth; set => SetProperty(ref currentMonth, value); }
    public SelectionDateMode SelectDateMode { get => selectDateMode; set => SetProperty(ref selectDateMode, value); }
    public CalendarOptions Options { get => options; set => SetProperty(ref options, value); }
    #endregion

    #region Collection's

    #region Field's
    private List<string> daysOfWeek;
    private List<DayOfMonth> daysOfMonth;
    #endregion

    public List<string> DaysOfWeek { get => daysOfWeek; set => SetProperty(ref daysOfWeek, value); }
    public List<DayOfMonth> DaysOfMonth { get => daysOfMonth; set => SetProperty(ref daysOfMonth, value); }
    #endregion

    #region Command's

    #region Field's
    private Command nextMonthCommand;
    private Command prevMonthCommand;
    private Command nextYearCommand;
    private Command prevYearCommand;
    private Command initCalendarDaysCommand;
    private Command selectDateCommand;
    #endregion

    public Command NextMonthCommand { get { nextMonthCommand ??= new Command(NextMonth); return nextMonthCommand; } }
    public Command PrevMonthCommand { get { prevMonthCommand ??= new Command(PrevMonth); return prevMonthCommand; } }
    public Command NextYearCommand { get { nextYearCommand ??= new Command(NextYear); return nextYearCommand; } }
    public Command PrevYearCommand { get { prevYearCommand ??= new Command(PrevYear); return prevYearCommand; } }
    public Command InitCalendarDaysCommand { get { initCalendarDaysCommand ??= new Command(InitCalendarDays); return initCalendarDaysCommand; } }
    public Command SelectDateCommand { get { selectDateCommand ??= new Command(SelectDate); return selectDateCommand; } }
    #endregion

    public DatePickerViewModel(CalendarOptions options)
    {
        SelectDateMode = options.SelectDateMode;
        Options = options;

        if (this.DaysOfWeek == null)
            FillDaysOfWeek();

        InitCalendarDays(options.SelectedPersianDate.ToDateTime());
    }

    private void FillDaysOfWeek()
    {
        this.DaysOfWeek = typeof(PersianDayOfWeek).GetMembers()
            .Where(x => x.MemberType == MemberTypes.Field)
            .Select(x => ((DisplayAttribute)x.GetCustomAttribute(typeof(DisplayAttribute)))?.Name)
            .Where(x => !string.IsNullOrEmpty(x)).ToList();
    }

    private void InitCalendarDays(object obj)
    {
        if (obj is not DateTime date)
            return;

        CurrentMonth = Enum.GetNames(typeof(PersianMonthNames))[date.GetPersianMonth() - 1];
        CurrentYear = date.GetPersianYear();

        var firstDayOfMonth = date.GetPersianBeginningMonth().ToDateTime();
        var endDayOfMonth = date.GetPersianEndingMonth().ToDateTime();

        var persianDayOfWeek = (int)firstDayOfMonth.GetPersianDay();
        int startDayMonth = ((persianDayOfWeek + 1) == 7 ? 0 : (persianDayOfWeek + 1));
        var monthDaysCount = Enumerable.Range(-startDayMonth, (endDayOfMonth - firstDayOfMonth).Days + startDayMonth + 1).ToList();

        DaysOfMonth = new List<DayOfMonth>(monthDaysCount.Count);
        DaysOfMonth.AddRange(GetDaysOfMonth(monthDaysCount, firstDayOfMonth, date));
    }

    private List<DayOfMonth> GetDaysOfMonth(List<int> monthDaysCount, DateTime firstDayOfMonth, DateTime date)
    {
        return monthDaysCount.Select(x =>
        {
            var currentDate = firstDayOfMonth.AddDays(x);
            bool isHoliday = currentDate.GetPersianDay() == DayOfWeek.Friday;
            var day = new DayOfMonth()
            {
                DayNum = currentDate.GetPersianDayOfMonth(),
                GregorianDate = currentDate,
                PersianDate = currentDate.ToPersianDate(),
                IsSelected = date.Date == currentDate.Date,
                IsInCurrentMonth = x >= 0,
                IsHoliday = isHoliday,
                DayOfWeek = (PersianDayOfWeek)currentDate.GetPersianDay(),
                CanSelect = (currentDate >= Options.MinDateCanSelect || Options.MinDateCanSelect is null)
                && (currentDate <= Options.MaxDateCanSelect || Options.MaxDateCanSelect is null)
                && (!isHoliday || Options.CanSelectHolidays)
            };
            return day;
        }).ToList();
    }

    private void SelectDate(object obj)
    {
        if (obj is not DayOfMonth dayOfMonth || !dayOfMonth.CanSelect)
            return;

        foreach (DayOfMonth day in DaysOfMonth)
            day.IsSelected = day.PersianDate == dayOfMonth.PersianDate;
    }

    private void NextMonth(object obj)
    {
        InitCalendarDays(DaysOfMonth.FirstOrDefault(x => x.IsSelected).GregorianDate.AddMonths(1));
    }

    private void PrevMonth(object obj)
    {
        InitCalendarDays(DaysOfMonth.FirstOrDefault(x => x.IsSelected).GregorianDate.AddMonths(-1));
    }

    private void NextYear(object obj)
    {
        InitCalendarDays(DaysOfMonth.FirstOrDefault(x => x.IsSelected).GregorianDate.AddYears(1));
    }

    private void PrevYear(object obj)
    {
        InitCalendarDays(DaysOfMonth.FirstOrDefault(x => x.IsSelected).GregorianDate.AddYears(-1));
    }
}