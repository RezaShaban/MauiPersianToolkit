using CommunityToolkit.Maui.Views;
using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePicker : ContentView
{
    #region Field's
    Color _color;
    ContentPage parentPage;
    DatePickerView view = null;
    Task initedView;
    #endregion

    #region Propertei's
    public static readonly BindableProperty SelectDateModeProperty = BindableProperty.Create(nameof(SelectDateMode), typeof(SelectionDateMode), typeof(DatePicker), SelectionDateMode.Day, BindingMode.TwoWay);
    public SelectionDateMode SelectDateMode
    {
        get { return (SelectionDateMode)GetValue(SelectDateModeProperty); }
        set { SetValue(SelectDateModeProperty, value); }
    }
    public static readonly BindableProperty SelectedPersianDateProperty = BindableProperty.Create(nameof(SelectedPersianDate), typeof(object), typeof(DatePicker), default(string), BindingMode.TwoWay);
    public string SelectedPersianDate
    {
        get { return (string)GetValue(SelectedPersianDateProperty); }
        set { SetValue(SelectedPersianDateProperty, value); }
    }
    public static readonly BindableProperty FormattedDateProperty = BindableProperty.Create(nameof(FormattedDate), typeof(object), typeof(DatePicker), default(string), BindingMode.TwoWay);
    public string FormattedDate
    {
        get { return (string)GetValue(FormattedDateProperty); }
        set { SetValue(FormattedDateProperty, value); }
    }
    public static readonly BindableProperty DateSeparatorProperty = BindableProperty.Create(nameof(DateSeparator), typeof(char), typeof(DatePicker), '/', BindingMode.TwoWay);
    public char DateSeparator
    {
        get { return (char)GetValue(DateSeparatorProperty); }
        set { SetValue(DateSeparatorProperty, value); }
    }
    public static readonly BindableProperty DisplayFormatProperty = BindableProperty.Create(nameof(DisplayFormat), typeof(object), typeof(DatePicker), "yyyy/MM/dd", BindingMode.TwoWay);
    public string DisplayFormat
    {
        get { return (string)GetValue(DisplayFormatProperty); }
        set { SetValue(DisplayFormatProperty, value); }
    }
    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(DatePicker), SelectionMode.Single, BindingMode.TwoWay);
    public SelectionMode SelectionMode
    {
        get { return (SelectionMode)GetValue(SelectionModeProperty); }
        set { SetValue(SelectionModeProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(DatePicker), Colors.Gray, BindingMode.TwoWay);
    public Color PlaceHolderColor
    {
        get { return (Color)GetValue(PlaceHolderColorProperty); }
        set { SetValue(PlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(nameof(ActivePlaceHolderColor), typeof(Color), typeof(DatePicker), Colors.Gray, BindingMode.TwoWay);
    public Color ActivePlaceHolderColor
    {
        get { return (Color)GetValue(ActivePlaceHolderColorProperty); }
        set { SetValue(ActivePlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(DatePicker), Colors.Black, BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(DatePicker), default(string), BindingMode.TwoWay);
    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(DatePicker), default(string), BindingMode.TwoWay);
    public string ErrorMessage
    {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(DatePicker), default(bool), BindingMode.TwoWay);
    public bool IsValid
    {
        get { return (bool)GetValue(IsValidProperty); }
        set { SetValue(IsValidProperty, value); }
    }
    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(DatePicker), default(bool), BindingMode.TwoWay);
    public bool IsLoading
    {
        get { return (bool)GetValue(IsLoadingProperty); }
        set { SetValue(IsLoadingProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(DatePicker), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand), typeof(Command), typeof(DatePicker), default(Command), BindingMode.TwoWay);
    public Command AcceptCommand
    {
        get { return (Command)GetValue(AcceptCommandProperty); }
        set { SetValue(AcceptCommandProperty, value); }
    }
    public static readonly BindableProperty ChangeDateCommandProperty = BindableProperty.Create(nameof(ChangeDateCommand), typeof(Command), typeof(DatePicker), default(Command), BindingMode.TwoWay);
    public Command ChangeDateCommand
    {
        get { return (Command)GetValue(ChangeDateCommandProperty); }
        set { SetValue(ChangeDateCommandProperty, value); }
    }
    public static readonly BindableProperty OnOpenCommandProperty = BindableProperty.Create(nameof(OnOpenCommand), typeof(Command), typeof(DatePicker), default(Command), BindingMode.TwoWay);
    public Command OnOpenCommand
    {
        get { return (Command)GetValue(OnOpenCommandProperty); }
        set { SetValue(OnOpenCommandProperty, value); }
    }
    #endregion

    public DatePicker()
    {
        InitializeComponent();
        this.initedView = this.InitPickerView();
    }

    private Task InitPickerView()
    {
        return Task.Run(() =>
        {
            this.IsLoading = true;
            this.view = new DatePickerView(this.SelectedPersianDate)
            {
                SelectDateMode = this.SelectDateMode,
                SelectDateCommand = new Command((date) =>
                {
                    this.SelectedPersianDate = date.ToString();
                    SetFormattedDate();
                    if (ChangeDateCommand != null)
                        ChangeDateCommand.Execute(SelectedPersianDate);
                    this.view.Close();
                })
            };
            this.view.Closed += (object sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e) =>
            {
                this.view = null;
                initedView = InitPickerView();
            };
            this.IsLoading = false;
        });
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (this.view == null)
            this.initedView.GetAwaiter().GetResult();
        this.IsLoading = true;
        if (this.parentPage is null)
        {
            var contentPage = this.Parent;
            while (contentPage is not ContentPage)
                contentPage = contentPage.Parent;
            this.parentPage = (ContentPage)contentPage;
        }
        await parentPage.ShowPopupAsync(this.view);
        this.IsLoading = false;
    }


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

        if (propertyName == SelectedPersianDateProperty.PropertyName)
        {
            if (!string.IsNullOrEmpty(SelectedPersianDate))
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
        var miladiDate = SelectedPersianDate.ToDateTime();
        var dateParts = SelectedPersianDate.Replace('/', DateSeparator).Split(DateSeparator);

        if (dateParts.Length > 0)
            FormattedDate = DisplayFormat.Replace("yyyy", dateParts[0]).Replace("yy", dateParts[0].Substring(1, 2));

        if (dateParts.Length >= 2)
            FormattedDate = FormattedDate.Replace("MMM", Enum.GetName(typeof(PersianMonthNames), dateParts[1].ToInt() - 1))
                .Replace("MM", dateParts[1]).Replace("M", dateParts[1].ToInt().ToString());

        //.Replace("dddd", App.NativeTools.GetPersianDay(miladiDate))
        if (dateParts.Length >= 3)
            FormattedDate = FormattedDate.Replace("dd", dateParts[2])
                .Replace("d", dateParts[2].ToInt().ToString())
                .Replace("DD", dateParts[2]);
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