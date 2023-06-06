using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePickerView : Popup
{
    #region Field's
    int month = 0;
    int year = 0;
    internal bool DailySelectionMode { get; set; }
    internal bool MonthlySelectionMode { get; set; }
    #endregion

    #region Propertie's
    public static readonly BindableProperty SelectDateCommandProperty = BindableProperty.Create(nameof(SelectDateCommand), typeof(Command), typeof(DatePickerView), default(Command), BindingMode.TwoWay);
    public Command SelectDateCommand
    {
        get { return (Command)GetValue(SelectDateCommandProperty); }
        set { SetValue(SelectDateCommandProperty, value); }
    }
    public static readonly BindableProperty SelectDateModeProperty = BindableProperty.Create(nameof(SelectDateMode), typeof(SelectionDateMode), typeof(DatePickerView), SelectionDateMode.Day, BindingMode.TwoWay);
    public SelectionDateMode SelectDateMode
    {
        get { return (SelectionDateMode)GetValue(SelectDateModeProperty); }
        set { SetValue(SelectDateModeProperty, value); }
    }
    public static readonly BindableProperty SelectDayColorProperty = BindableProperty.Create(nameof(SelectDayColor), typeof(Color), typeof(DatePickerView), Colors.DeepSkyBlue, BindingMode.TwoWay);
    public Color SelectDayColor
    {
        get { return (Color)GetValue(SelectDayColorProperty); }
        set { SetValue(SelectDayColorProperty, value); }
    }
    public static readonly BindableProperty SelectedPersianDateProperty = BindableProperty.Create(nameof(SelectedPersianDate), typeof(object), typeof(DatePickerView), default(string), BindingMode.TwoWay);
    public string SelectedPersianDate
    {
        get { return (string)GetValue(SelectedPersianDateProperty); }
        set { SetValue(SelectedPersianDateProperty, value); }
    }
    #endregion

    public DatePickerView(string selectedPersianDate)
    {
        InitializeComponent();

        SelectedPersianDate = selectedPersianDate;
        InitCalendarDays(SelectedPersianDate.ToDateTime());
        grdDayList.IsVisible = SelectDateMode == SelectionDateMode.Day;
        grdMonthList.IsVisible = SelectDateMode == SelectionDateMode.Month;
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if(propertyName == SelectDateModeProperty.PropertyName)
        {
            grdDayList.IsVisible = SelectDateMode == SelectionDateMode.Day;
            grdMonthList.IsVisible = SelectDateMode == SelectionDateMode.Month;
        }
    }

    private void InitCalendarDays(DateTime date)
    {
        var currentMonth = date.Month;
        var firstDayOfMonth = date.GetPersianBeginningMonth().ToDateTime();
        var firstDayNameOfMonth = firstDayOfMonth.GetPersianDay(); //.Date.AddDays((date.Day - 1) * -1).DayOfWeek;
        btnCurrentYear.Text = date.GetPersianYear();
        btnCurrentYea.Text = date.GetPersianYear();
        int day = -2;
        bool started = false;
        btnCurrentMonth.Text = Enum.GetNames(typeof(PersianMonthNames))[date.GetPersianMonth() - 1];
        foreach (var item in grdDayList.Children.Where(x => x.GetType().Equals(typeof(Button))))
        {
            if (item is not Button btn)
                continue;

            day++;
            if (((int)firstDayNameOfMonth) > day && !started)
            {
                btn.IsVisible = false;
                continue;
            }
            else if (!started)
            {
                started = true;
                day = 0;
            }

            var currentDate = firstDayOfMonth.AddDays(day);

            btn.IsVisible = currentDate.GetPersianMonth() == date.GetPersianMonth();
            btn.Text = currentDate.GetPersianDayOfMonth().ToString();//.Day.ToString();
            btn.CornerRadius = 0;
            btn.BackgroundColor = Colors.White;
            btn.TextColor = Colors.Black;
            btn.ClassId = currentDate.ToPersianDate(); //.Date.ToString("yyyy-MM-dd");

            if (btn.ClassId.Split('/')[2].ToInt() == date.GetPersianDayOfMonth())
            {
                btn.CornerRadius = 100;
                btn.BackgroundColor = SelectDayColor;
                btn.TextColor = Colors.White;
            }

            if (currentDate.DayOfWeek == DayOfWeek.Friday)
                btn.TextColor = Colors.Red;
        }
    }

    private void btnDay_Clicked(object sender, EventArgs e)
    {
        foreach (var item in grdDayList.Children)
        {
            if (!(item is Button btn) || !btn.IsEnabled)
                continue;

            btn.CornerRadius = 0;
            btn.BackgroundColor = Colors.White;
            btn.TextColor = Colors.Black;
            if (btn.ClassId.ToDateTime().GetPersianDay() == DayOfWeek.Friday)
                btn.TextColor = Colors.Red;

        }
        var currentBtn = ((Button)sender);
        SelectedPersianDate = currentBtn.ClassId;
        currentBtn.BackgroundColor = SelectDayColor;
        currentBtn.TextColor = Colors.White;
        currentBtn.CornerRadius = 100;
        if (SelectDateCommand != null)
            SelectDateCommand.Execute(SelectedPersianDate);
    }

    private void btnNextMonth_Clicked(object sender, EventArgs e)
    {
        month += 1;
        InitCalendarDays(DateTime.Now.AddYears(year).AddMonths(month));
    }

    private void btnPrevMonth_Clicked(object sender, EventArgs e)
    {
        month -= 1;
        InitCalendarDays(DateTime.Now.AddYears(year).AddMonths(month));
    }

    private void btnNextYear_Clicked(object sender, EventArgs e)
    {
        year += 1;
        InitCalendarDays(DateTime.Now.AddYears(year).AddMonths(month));
    }

    private void btnPrevYear_Clicked(object sender, EventArgs e)
    {
        year -= 1;
        InitCalendarDays(DateTime.Now.AddYears(year).AddMonths(month));
    }

    private void btnMonth_Clicked(object sender, EventArgs e)
    {
        foreach (var item in grdMonthList.Children)
        {
            if (!(item is Button btn) || !btn.IsEnabled)
                continue;

            btn.CornerRadius = 0;
            btn.BackgroundColor = Colors.White;
            btn.TextColor = Colors.Black;
        }
        var currentBtn = ((Button)sender);
        SelectedPersianDate = $"{btnCurrentYea.Text}/{currentBtn.ClassId.PadLeft(2, '0')}/01";
        currentBtn.BackgroundColor = SelectDayColor;
        currentBtn.TextColor = Colors.White;
        currentBtn.CornerRadius = 10;
        if (SelectDateCommand != null)
            SelectDateCommand.Execute(SelectedPersianDate);
    }
}

enum PersianMonthNames
{
    فروردین = 0,
    اردیبهشت,
    خرداد,
    تیر,
    مرداد,
    شهریور,
    مهر,
    آبان,
    آذر,
    دی,
    بهمن,
    اسفند
}

public enum SelectionDateMode
{
    Month,
    Day
}