namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CheckBoxView : ContentView
{
    #region Propertie's
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBoxView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(Command), typeof(CheckBoxView), default(Command), BindingMode.TwoWay);
    public Command TappedCommand
    {
        get { return (Command)GetValue(TappedCommandProperty); }
        set { SetValue(TappedCommandProperty, value); }
    }
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CheckBoxView), default(object), BindingMode.TwoWay);
    public object CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBoxView), default(Color), BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(CheckBoxView), default(Color), BindingMode.TwoWay);
    public Color IconColor
    {
        get { return (Color)GetValue(IconColorProperty); }
        set { SetValue(IconColorProperty, value); }
    }
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckBoxView), 14d, BindingMode.TwoWay);
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxView), default(bool), BindingMode.TwoWay);
    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set { SetValue(IsCheckedProperty, value); }
    }
    #endregion
    public CheckBoxView()
    {
        InitializeComponent();

        var tapped = new TapGestureRecognizer();
        tapped.Command = new Command(lblText_Tapped);
        lblText.GestureRecognizers.Add(tapped);
    }

    private void lblText_Tapped(object sender)
    {
        IsChecked = !IsChecked;

        if (TappedCommand == null)
            return;
        TappedCommand.Execute(CommandParameter);
    }
}