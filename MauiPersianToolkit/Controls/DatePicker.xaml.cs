using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using MauiPersianToolkit.Enums;
using MauiPersianToolkit.Models;
using System.Runtime.CompilerServices;

namespace MauiPersianToolkit.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePicker : ContentView
{
    #region Fields

    private ContentPage _parentPage;
    private DatePickerView _pickerView;
    private bool _isInitializing;

    #endregion

    #region Properties

    public static readonly BindableProperty CalendarOptionProperty = BindableProperty.Create(
        nameof(CalendarOption), typeof(CalendarOptions), typeof(DatePicker),
        new CalendarOptions(), BindingMode.TwoWay);

    public CalendarOptions CalendarOption
    {
        get => (CalendarOptions)GetValue(CalendarOptionProperty);
        set => SetValue(CalendarOptionProperty, value);
    }

    public static readonly BindableProperty SelectedPersianDateProperty = BindableProperty.Create(
        nameof(SelectedPersianDate), typeof(string), typeof(DatePicker),
        default(string), BindingMode.TwoWay);

    public string SelectedPersianDate
    {
        get => (string)GetValue(SelectedPersianDateProperty);
        set => SetValue(SelectedPersianDateProperty, value);
    }

    public static readonly BindableProperty FormattedDateProperty = BindableProperty.Create(
        nameof(FormattedDate), typeof(string), typeof(DatePicker),
        default(string), BindingMode.TwoWay);

    public string FormattedDate
    {
        get => (string)GetValue(FormattedDateProperty);
        set => SetValue(FormattedDateProperty, value);
    }

    public static readonly BindableProperty BadgeDatesProperty = BindableProperty.Create(
        nameof(BadgeDates), typeof(List<string>), typeof(DatePicker),
        new List<string>(), BindingMode.TwoWay);

    public List<string> BadgeDates
    {
        get => (List<string>)GetValue(BadgeDatesProperty);
        set => SetValue(BadgeDatesProperty, value);
    }

    public static readonly BindableProperty DateSeparatorProperty = BindableProperty.Create(
        nameof(DateSeparator), typeof(char), typeof(DatePicker),
        '/', BindingMode.TwoWay);

    public char DateSeparator
    {
        get => (char)GetValue(DateSeparatorProperty);
        set => SetValue(DateSeparatorProperty, value);
    }

    public static readonly BindableProperty DisplayFormatProperty = BindableProperty.Create(
        nameof(DisplayFormat), typeof(string), typeof(DatePicker),
        "yyyy/MM/dd", BindingMode.TwoWay);

    public string DisplayFormat
    {
        get => (string)GetValue(DisplayFormatProperty);
        set => SetValue(DisplayFormatProperty, value);
    }

    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(
        nameof(PlaceHolderColor), typeof(Color), typeof(DatePicker),
        Colors.Gray, BindingMode.TwoWay);

    public Color PlaceHolderColor
    {
        get => (Color)GetValue(PlaceHolderColorProperty);
        set => SetValue(PlaceHolderColorProperty, value);
    }

    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(
        nameof(ActivePlaceHolderColor), typeof(Color), typeof(DatePicker),
        Colors.Gray, BindingMode.TwoWay);

    public Color ActivePlaceHolderColor
    {
        get => (Color)GetValue(ActivePlaceHolderColorProperty);
        set => SetValue(ActivePlaceHolderColorProperty, value);
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor), typeof(Color), typeof(DatePicker),
        Colors.Black, BindingMode.TwoWay);

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
        nameof(PlaceHolder), typeof(string), typeof(DatePicker),
        default(string), BindingMode.TwoWay);

    public string PlaceHolder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }

    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
        nameof(ErrorMessage), typeof(string), typeof(DatePicker),
        default(string), BindingMode.TwoWay);

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
        nameof(IsValid), typeof(bool), typeof(DatePicker),
        false, BindingMode.TwoWay);

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
        nameof(IsLoading), typeof(bool), typeof(DatePicker),
        false, BindingMode.TwoWay);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon), typeof(string), typeof(DatePicker),
        default(string), BindingMode.TwoWay);

    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty OnChangeDateCommandProperty = BindableProperty.Create(
        nameof(OnChangeDateCommand), typeof(Command), typeof(DatePicker),
        default(Command), BindingMode.TwoWay);

    public Command OnChangeDateCommand
    {
        get => (Command)GetValue(OnChangeDateCommandProperty);
        set => SetValue(OnChangeDateCommandProperty, value);
    }

    public static readonly BindableProperty OnOpenedCommandProperty = BindableProperty.Create(
        nameof(OnOpenedCommand), typeof(Command), typeof(DatePicker),
        default(Command), BindingMode.TwoWay);

    public Command OnOpenedCommand
    {
        get => (Command)GetValue(OnOpenedCommandProperty);
        set => SetValue(OnOpenedCommandProperty, value);
    }

    #endregion

    public DatePicker()
    {
        InitializeComponent();
        AttachGestureRecognizer();
    }

    private void AttachGestureRecognizer()
    {
        var gestureRecognizer = new TapGestureRecognizer
        {
            Command = new Command(OnDatePickerTapped)
        };
        container.GestureRecognizers.Add(gestureRecognizer);
    }

    private async Task InitializePickerViewAsync()
    {
        if (_isInitializing)
            return;

        _isInitializing = true;

        try
        {
            IsLoading = true;

            ConfigureCalendarOptions();
            _pickerView = new DatePickerView(CalendarOption);
            AttachPickerViewEventHandlers();
        }
        finally
        {
            IsLoading = false;
            _isInitializing = false;
        }
    }

    private void ConfigureCalendarOptions()
    {
        CalendarOption.SelectedPersianDate = SelectedPersianDate ?? DateTime.Now.ToCalendarDate(CalendarOption.CalendarType);
        CalendarOption.SelectedPersianDates = BadgeDates;
        CalendarOption.AutoCloseAfterSelectDate = CalendarOption.SelectionMode != Enums.SelectionMode.Multiple 
            && CalendarOption.AutoCloseAfterSelectDate;
    }

    private void AttachPickerViewEventHandlers()
    {
        _pickerView.SelectedDateChanged += OnPickerViewSelectedDateChanged;
        _pickerView.Opened += OnPickerViewOpened;
        _pickerView.Closed += OnPickerViewClosed;
    }

    private async void OnPickerViewSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
    {
        if (CalendarOption.SelectionMode == Enums.SelectionMode.Single)
        {
            SelectedPersianDate = e.SelectedDate.PersianDate.ToString();
            UpdateFormattedDate();
            OnChangeDateCommand?.Execute(SelectedPersianDate);
        }
    }

    private void OnPickerViewOpened(object sender, EventArgs e)
    {
        OnOpenedCommand?.Execute(e);
    }

    private void OnPickerViewClosed(object sender, EventArgs e)
    {
        DetachPickerViewEventHandlers();
        _pickerView = null;
    }

    private void DetachPickerViewEventHandlers()
    {
        if (_pickerView == null)
            return;

        _pickerView.SelectedDateChanged -= OnPickerViewSelectedDateChanged;
        _pickerView.Opened -= OnPickerViewOpened;
        _pickerView.Closed -= OnPickerViewClosed;
    }

    #region Event Handlers

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            case nameof(IsEnabled):
                PlaceHolderColor = IsEnabled ? PlaceHolderColor : Colors.Gray;
                break;

            case nameof(SelectedPersianDate):
                if (!string.IsNullOrEmpty(SelectedPersianDate))
                {
                    UpdateFormattedDate();
                    _ = InitializePickerViewAsync();
                }
                break;
        }
    }

    private void ucDatePicker_Loaded(object sender, EventArgs e)
    {
        _ = InitializePickerViewAsync();
    }

    #endregion

    #region Private Methods

    private void UpdateFormattedDate()
    {
        if (string.IsNullOrEmpty(SelectedPersianDate))
            return;

        var dateParts = SelectedPersianDate.Replace('/', DateSeparator).Split(DateSeparator);

        if (dateParts.Length == 0)
            return;

        FormattedDate = ApplyYearFormat(DisplayFormat, dateParts);
        FormattedDate = ApplyMonthFormat(FormattedDate, dateParts);
        FormattedDate = ApplyDayFormat(FormattedDate, dateParts);
    }

    private string ApplyYearFormat(string format, string[] dateParts)
    {
        if (dateParts.Length <= 0)
            return format;

        var yearValue = dateParts[0];
        return format
            .Replace("yyyy", yearValue)
            .Replace("yy", yearValue.Length >= 2 ? yearValue.Substring(yearValue.Length - 2) : yearValue);
    }

    private string ApplyMonthFormat(string format, string[] dateParts)
    {
        if (dateParts.Length < 2)
            return format;

        var monthValue = dateParts[1];
        var monthNumber = monthValue.ToInt();

        return format
            .Replace("MMM", Enum.GetName(typeof(PersianMonthNames), monthNumber - 1))
            .Replace("MM", monthValue)
            .Replace("M", monthNumber.ToString());
    }

    private string ApplyDayFormat(string format, string[] dateParts)
    {
        if (dateParts.Length < 3)
            return format;

        var dayValue = dateParts[2];
        var dayNumber = dayValue.ToInt();

        return format
            .Replace("dd", dayValue)
            .Replace("d", dayNumber.ToString())
            .Replace("DD", dayValue);
    }

    private async void OnDatePickerTapped(object sender)
    {
        if (_pickerView == null)
        {
            await InitializePickerViewAsync();
        }

        _parentPage ??= FindParentContentPage();

        if (_parentPage == null)
            return;

        IsLoading = true;
        try
        {
            await _parentPage.ShowPopupAsync(_pickerView, new PopupOptions
            {
                Shape = null,
                Shadow = null
            });
        }
        finally
        {
            IsLoading = false;
        }
    }

    private ContentPage FindParentContentPage()
    {
        var parent = Parent;
        while (parent is not null and not ContentPage)
        {
            parent = parent.Parent;
        }

        return parent as ContentPage;
    }

    #endregion
}