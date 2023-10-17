using PersianUIControlsMaui.Enums;
using PersianUIControlsMaui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PersianUIControlsMaui.Extensions;
using CommunityToolkit.Maui.Core.Extensions;

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
    private ObservableCollection<PuiTuple> persianMonths;
    private ObservableCollection<DayOfMonth> selectedDays;
    #endregion

    public List<string> DaysOfWeek { get => daysOfWeek; set => SetProperty(ref daysOfWeek, value); }
    public List<DayOfMonth> DaysOfMonth { get => daysOfMonth; set => SetProperty(ref daysOfMonth, value); }
    public ObservableCollection<DayOfMonth> SelectedDays { get => selectedDays; set => SetProperty(ref selectedDays, value); }
    public ObservableCollection<PuiTuple> PersianMonths { get => persianMonths; set => SetProperty(ref persianMonths, value); }
    #endregion

    #region Command's

    #region Field's
    private Command nextMonthCommand;
    private Command prevMonthCommand;
    private Command nextYearCommand;
    private Command prevYearCommand;
    private Command initCalendarDaysCommand;
    private Command selectDateCommand;
    private Command gotoTodayCommand;
    private Command switchModeCommand;
    private Command selectMonthCommand;
    #endregion

    public Command NextMonthCommand { get { nextMonthCommand ??= new Command(NextMonth); return nextMonthCommand; } }
    public Command PrevMonthCommand { get { prevMonthCommand ??= new Command(PrevMonth); return prevMonthCommand; } }
    public Command NextYearCommand { get { nextYearCommand ??= new Command(NextYear); return nextYearCommand; } }
    public Command PrevYearCommand { get { prevYearCommand ??= new Command(PrevYear); return prevYearCommand; } }
    public Command SwitchModeCommand { get { switchModeCommand ??= new Command(SwitchMode); return switchModeCommand; } }
    public Command SelectMonthCommand { get { selectMonthCommand ??= new Command(SelectMonth); return selectMonthCommand; } }
    public Command GotoTodayCommand { get { gotoTodayCommand ??= new Command(GotoToday); return gotoTodayCommand; } }
    public Command InitCalendarDaysCommand { get { initCalendarDaysCommand ??= new Command(InitCalendarDays); return initCalendarDaysCommand; } }
    public Command SelectDateCommand { get { selectDateCommand ??= new Command(SelectDate); return selectDateCommand; } }
    #endregion

    public DatePickerViewModel(CalendarOptions options)
    {
        Options = options;
        SelectedDays = new ObservableCollection<DayOfMonth>(GetSelectedDates(options.SelectedPersianDates));
        PersianMonths = new ObservableCollection<PuiTuple>();
        SelectDateMode = options.SelectDateMode;

        if (this.DaysOfWeek == null)
            FillDaysOfWeek();

        InitCalendarDays(options.SelectedPersianDate.ToDateTime());
    }

    private IEnumerable<DayOfMonth> GetSelectedDates(List<string> selectedPersianDates)
    {
        return (selectedPersianDates ?? new List<string>()).Select(x =>
        {
            var currentDate = x.ToDateTime();
            bool isHoliday = currentDate.GetPersianDay() == DayOfWeek.Friday;
            var day = new DayOfMonth()
            {
                DayNum = currentDate.GetPersianDayOfMonth(),
                GregorianDate = currentDate,
                PersianDate = currentDate.ToPersianDate(),
                PersianDateNo = currentDate.ToPersianDate().Replace("/", "").ToInt(),
                IsSelected = true,
                IsInRange = false,
                IsInCurrentMonth = false,
                IsHoliday = isHoliday,
                IsToday = currentDate.Date == DateTime.Now.Date,
                DayOfWeek = (PersianDayOfWeek)currentDate.GetPersianDay(),
                CanSelect = GetCanSelect(currentDate),
            };
            return day;
        }).ToList();
    }

    private void FillDaysOfWeek()
    {
        this.DaysOfWeek = typeof(PersianDayOfWeek).GetMembers()
            .Where(x => x.MemberType == MemberTypes.Field)
            .Select(x => ((DisplayAttribute)x.GetCustomAttribute(typeof(DisplayAttribute)))?.Name.ToCharArray().FirstOrDefault().ToString())
            .Where(x => !string.IsNullOrEmpty(x)).ToList();
    }

    private void InitCalendarDays(object obj)
    {
        if (obj is not DateTime date)
            return;

        CurrentMonth = typeof(PersianMonthNames).GetDisplay(date.GetPersianMonth() - 1);
        PersianMonths = GetMonths();
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
                PersianDateNo = currentDate.ToPersianDate().Replace("/", "").ToInt(),
                IsSelected = GetIsSelected(currentDate, date),
                IsInRange = GetIsInRange(currentDate),
                IsInCurrentMonth = x >= 0,
                IsHoliday = isHoliday,
                IsToday = currentDate.Date == DateTime.Now.Date,
                DayOfWeek = (PersianDayOfWeek)currentDate.GetPersianDay(),
                CanSelect = GetCanSelect(currentDate),
            };
            return day;
        }).ToList();
    }

    private ObservableCollection<PuiTuple> GetMonths()
    {
        var members = typeof(PersianMonthNames).GetFields(BindingFlags.Static | BindingFlags.Public);
        var items = members.Select(x => new PuiTuple(x.Name, x.GetCustomAttribute<DisplayAttribute>().Name)).ToObservableCollection();
        return items;
    }

    private void SelectDate(object obj)
    {
        if (obj is not DayOfMonth dayOfMonth || !dayOfMonth.CanSelect)
            return;
        if (Options.SelectionMode == Enums.SelectionMode.Single)
            DaysOfMonth.ForEach(day => { day.IsSelected = day.PersianDate == dayOfMonth.PersianDate; });
        else if (Options.SelectionMode == Enums.SelectionMode.Multiple)
            ToggleMultipleDates(dayOfMonth);
        else if (Options.SelectionMode == Enums.SelectionMode.Range)
        {
            if (dayOfMonth.PersianDateNo <= SelectedDays.FirstOrDefault(x => x.IsSelected)?.PersianDateNo)
            {
                DaysOfMonth.ForEach(x =>
                {
                    x.IsSelected = false; x.IsInRange = false;
                    x.CanSelect = GetCanSelect(x.GregorianDate);
                });
                SelectedDays.Clear();
            }
            ToggleRangeDates(dayOfMonth);
        }
    }

    private void ToggleRangeDates(DayOfMonth dayOfMonth)
    {
        if (SelectedDays.Count == 2)
        {
            SelectedDays.Clear();
            DaysOfMonth.ForEach(x => { x.IsSelected = false; x.IsInRange = false; });
        }

        if (SelectedDays.Any())
        {
            var selectedDate = DaysOfMonth.FirstOrDefault(x => x.PersianDateNo == dayOfMonth.PersianDateNo);
            {
                selectedDate.IsSelected = selectedDate.PersianDate == dayOfMonth.PersianDate;

                if (selectedDate.IsSelected)
                    SelectedDays.Add(selectedDate);
            }
            DaysOfMonth.Where(x => x.PersianDateNo > SelectedDays.FirstOrDefault().PersianDateNo
                                    && x.PersianDateNo < SelectedDays.LastOrDefault().PersianDateNo).ToList()
                        .ForEach(x => x.IsInRange = true);
        }

        if (!SelectedDays.Any())
        {
            var selectedDate = DaysOfMonth.FirstOrDefault(x => x.PersianDateNo == dayOfMonth.PersianDateNo);
            {
                selectedDate.IsSelected = selectedDate.PersianDateNo == dayOfMonth.PersianDateNo;
                selectedDate.CanSelect = GetCanSelect(selectedDate.GregorianDate);

                if (selectedDate.IsSelected)
                    SelectedDays.Add(selectedDate);
            }
        }
    }

    private void ToggleMultipleDates(DayOfMonth dayOfMonth)
    {
        var selectedDate = DaysOfMonth.FirstOrDefault(x => x.PersianDateNo == dayOfMonth.PersianDateNo);
        if (!selectedDate.IsSelected)
        {
            selectedDate.IsSelected = !selectedDate.IsSelected;
            SelectedDays.Add(selectedDate);
        }
        else
        {
            if (SelectedDays.Count > 0)
                SelectedDays.Remove(selectedDate);
            selectedDate.IsSelected = !selectedDate.IsSelected;
        }
    }

    private bool GetCanSelect(DateTime currentDate)
    {
        bool isHoliday = currentDate.GetPersianDay() == DayOfWeek.Friday;
        return (currentDate >= Options.MinDateCanSelect || Options.MinDateCanSelect is null)
                && (currentDate <= Options.MaxDateCanSelect || Options.MaxDateCanSelect is null)
                && (!isHoliday || Options.CanSelectHolidays);
    }

    private bool GetIsSelected(DateTime currentDate, DateTime date)
    {
        if (Options.SelectionMode == Enums.SelectionMode.Single)
            return date.Date == currentDate.Date;
        if (Options.SelectionMode == Enums.SelectionMode.Multiple || Options.SelectionMode == Enums.SelectionMode.Range)
            return SelectedDays.Any(x => x.GregorianDate.Date == currentDate.Date);

        return false;
    }

    private bool GetIsInRange(DateTime currentDate)
    {
        if (Options.SelectionMode == Enums.SelectionMode.Range && SelectedDays.Count > 0)
            return SelectedDays.FirstOrDefault().GregorianDate.Date < currentDate.Date && SelectedDays.LastOrDefault().GregorianDate.Date > currentDate.Date;

        return false;
    }

    private void NextMonth(object obj)
    {
        var date = GetSelectedDate();
        InitCalendarDays(date.AddMonths(1));
    }

    private void PrevMonth(object obj)
    {
        var date = GetSelectedDate();
        InitCalendarDays(date.AddMonths(-1));
    }

    private void NextYear(object obj)
    {
        var date = GetSelectedDate();
        InitCalendarDays(date.AddYears(1));
    }

    private void PrevYear(object obj)
    {
        var date = GetSelectedDate();
        InitCalendarDays(date.AddYears(-1));
    }

    private void GotoToday(object obj)
    {
        var date = DateTime.Now.Date;
        InitCalendarDays(date);
    }

    private void SwitchMode(object obj) =>
        SelectDateMode = SelectionDateMode.Month;

    private void SelectMonth(object obj)
    {
        if (obj is not PuiTuple value)
            return;

        var month = Enum.Parse<PersianMonthNames>(value.Key);
        var date = $"{CurrentYear}/{(int)month + 1}/01".ToDateTime();
        SelectDateMode = SelectionDateMode.Day;
        InitCalendarDays(date);
    }

    private DateTime GetSelectedDate()
    {
        DateTime date = DateTime.Now;
        if (DaysOfMonth.Any(x => x.IsSelected))
            date = DaysOfMonth.FirstOrDefault(x => x.IsSelected).GregorianDate;
        else
            date = DaysOfMonth.Skip(DaysOfMonth.Count / 2).Take(1).FirstOrDefault().GregorianDate;
        return date;
    }

    public bool CanClose(DayOfMonth selectedDate)
    {
        return (Options.AutoCloseAfterSelectDate && Options.SelectionMode == Enums.SelectionMode.Range && SelectedDays.Count == 2)
            || selectedDate.CanSelect && Options.AutoCloseAfterSelectDate && Options.SelectionMode == Enums.SelectionMode.Single;
    }
}