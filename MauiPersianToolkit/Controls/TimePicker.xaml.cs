using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TimePicker : ContentView
{
    #region Field's
    Color _color;
    #endregion
    #region Propertei's
    public static readonly BindableProperty SelectDateModeProperty = BindableProperty.Create(nameof(SelectDateMode), typeof(SelectionDateMode), typeof(DatePicker), SelectionDateMode.Day, BindingMode.TwoWay);
    public SelectionDateMode SelectDateMode
    {
        get { return (SelectionDateMode)GetValue(SelectDateModeProperty); }
        set { SetValue(SelectDateModeProperty, value); }
    }
    public static readonly BindableProperty SelectedTimeProperty = BindableProperty.Create(nameof(SelectedTime), typeof(TimeSpan), typeof(TimePicker), DateTime.Now.TimeOfDay, BindingMode.TwoWay);
    public TimeSpan SelectedTime
    {
        get { return (TimeSpan)GetValue(SelectedTimeProperty); }
        set { SetValue(SelectedTimeProperty, value); }
    }
    public static readonly BindableProperty FormattedTimeProperty = BindableProperty.Create(nameof(FormattedTime), typeof(string), typeof(TimePicker), default(string), BindingMode.TwoWay);
    public string FormattedTime
    {
        get { return (string)GetValue(FormattedTimeProperty); }
        set { SetValue(FormattedTimeProperty, value); }
    }
    public static readonly BindableProperty TimeSeparatorProperty = BindableProperty.Create(nameof(TimeSeparator), typeof(char), typeof(TimePicker), ':', BindingMode.TwoWay);
    public char TimeSeparator
    {
        get { return (char)GetValue(TimeSeparatorProperty); }
        set { SetValue(TimeSeparatorProperty, value); }
    }
    public static readonly BindableProperty DisplayFormatProperty = BindableProperty.Create(nameof(DisplayFormat), typeof(string), typeof(DatePicker), "hh:mm:ss", BindingMode.TwoWay);
    public string DisplayFormat
    {
        get { return (string)GetValue(DisplayFormatProperty); }
        set { SetValue(DisplayFormatProperty, value); }
    }

    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(PickerView), Colors.Gray, BindingMode.TwoWay);
    public Color PlaceHolderColor
    {
        get { return (Color)GetValue(PlaceHolderColorProperty); }
        set { SetValue(PlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(nameof(ActivePlaceHolderColor), typeof(Color), typeof(PickerView), Colors.Gray, BindingMode.TwoWay);
    public Color ActivePlaceHolderColor
    {
        get { return (Color)GetValue(ActivePlaceHolderColorProperty); }
        set { SetValue(ActivePlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PickerView), Colors.Black, BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string ErrorMessage
    {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(PickerView), default(bool), BindingMode.TwoWay);
    public bool IsValid
    {
        get { return (bool)GetValue(IsValidProperty); }
        set { SetValue(IsValidProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    #endregion
    public TimePicker()
    {
        InitializeComponent();
    }

    //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    //{
    //var view = new DatePickerView(this.SelectedTime)
    //{
    //    SelectDateMode = this.SelectDateMode,
    //    SelectDateCommand = new Command(async (date) =>
    //    {
    //        this.SelectedPersianDate = date.ToString();
    //        SetFormattedDate();
    //        await PopupNavigation.Instance.PopAsync();
    //    })
    //};
    //await PopupNavigation.Instance.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage()
    //{
    //    CloseWhenBackgroundIsClicked = true,
    //    Animation = new ScaleAnimation(Rg.Plugins.Popup.Enums.MoveAnimationOptions.Center, Rg.Plugins.Popup.Enums.MoveAnimationOptions.Center)
    //    {
    //        DurationIn = 250,
    //        DurationOut = 250,
    //        ScaleIn = 1,
    //        ScaleOut = 1,
    //    },
    //    Content = new Frame()
    //    {
    //        VerticalOptions = LayoutOptions.Center,
    //        Padding = new Thickness(0),
    //        Margin = new Thickness(20),
    //        HasShadow = true,
    //        CornerRadius = 0,
    //        IsClippedToBounds = true,
    //        BorderColor = Color.Transparent,
    //        BackgroundColor = ((Color)App.Current.Resources["PrimaryLight"]),
    //        Content = view
    //    }
    //});
    //}


    #region Event's

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
            PlaceHolderColor = this.IsEnabled ? PlaceHolderColor : Colors.Gray;

        if (propertyName == IconProperty.PropertyName)
        {
            lblIcon.IsVisible = !string.IsNullOrEmpty(Icon);
        }

        if (propertyName == SelectedTimeProperty.PropertyName)
        {
            if (SelectedTime.TotalMilliseconds > 0)
                SetFormattedDate();

            if (string.IsNullOrEmpty(txtEntry.Text))
                PullDownPlaceHolder();
            else
            {
                SetFormattedDate();
                PullUpPlaceHolder();
            }
        }

        if (propertyName == WidthProperty.PropertyName)
        {
            rectangle.WidthRequest = this.Width;
            rectangle.Stroke = new SolidColorBrush(Color.FromArgb("#a4a6a9"));
        }
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        PullUpPlaceHolder();
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEntry.Text))
            PullDownPlaceHolder();
    }

    private void txtEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEntry.Text))
            PullDownPlaceHolder();
        else
            PullUpPlaceHolder();
    }

    #endregion

    #region Method's

    void SetFormattedDate()
    {
        try
        {
            FormattedTime = DateTime.Today.Add(SelectedTime).ToString(DisplayFormat, new System.Globalization.CultureInfo("fa-IR")); //.Replace('/', DateSeparator).Split(DateSeparator);
        }
        catch (Exception)
        {
            //Dialogs.Instance.ShowException(ex);
        }
        //if (dateParts.Length > 0)
        //    FormattedDate = DisplayFormat.Replace("yyyy", dateParts[0]).Replace("yy", dateParts[0].Substring(1, 2));

        //if (dateParts.Length >= 2)
        //    FormattedDate = FormattedDate.Replace("MMM", Enum.GetName(typeof(PersianMonthNames), dateParts[1].ToInt() - 1))
        //        .Replace("MM", dateParts[1]).Replace("M", dateParts[1].ToInt().ToString());

        //if (dateParts.Length >= 3)
        //    FormattedDate = FormattedDate.Replace("dd", dateParts[2])
        //        .Replace("d", dateParts[2].ToInt().ToString())
        //        .Replace("DD", dateParts[2]);
    }
    void PullUpPlaceHolder()
    {
        lblPlaceholder.TranslateTo(0, -28);
        if (PlaceHolderColor != ActivePlaceHolderColor)
            _color = PlaceHolderColor; //Application.Current.Resources[$"Primary{Application.Current.RequestedTheme}"];
        if (this.txtEntry.IsFocused)
        {
            var activeColor = ((Color)Application.Current.Resources[$"Primary{Application.Current.RequestedTheme}"]);
            this.PlaceHolderColor = activeColor; //this.ActivePlaceHolderColor;
            rectangle.Stroke = new SolidColorBrush(activeColor);
        }
        else
        {
            this.PlaceHolderColor = Color.FromArgb("#a4a6a9");
            rectangle.Stroke = new SolidColorBrush(Color.FromArgb("#a4a6a9"));
        }
        lblPlaceholder.BackgroundColor = Colors.White;
        //PlaceHolderColor = ActivePlaceHolderColor;
    }
    void PullDownPlaceHolder()
    {
        lblPlaceholder.TranslateTo(0, 0);
        ActivePlaceHolderColor = PlaceHolderColor;
        PlaceHolderColor = _color;
        if (!this.txtEntry.IsFocused)
        {
            rectangle.Stroke = new SolidColorBrush(Color.FromArgb("#a4a6a9"));
            ActivePlaceHolderColor = Color.FromArgb("#a4a6a9");// PlaceHolderColor;
            PlaceHolderColor = Color.FromArgb("#a4a6a9");// _color;
        }
    }

    #endregion
}