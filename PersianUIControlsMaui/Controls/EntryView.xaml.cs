using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EntryView : ContentView
{
    #region Field's
    Color _color;
    #endregion

    #region Propertie's
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty AppendTextProperty = BindableProperty.Create(nameof(AppendTextProperty), typeof(string), typeof(EntryView), default(string), BindingMode.OneWay);
    public string AppendText
    {
        get { return (string)GetValue(AppendTextProperty); }
        set { SetValue(AppendTextProperty, value); }
    }
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(EntryView), TextAlignment.Start, BindingMode.TwoWay);
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set { SetValue(HorizontalTextAlignmentProperty, value); }
    }
    public static readonly BindableProperty EntryFlowDirectionProperty = BindableProperty.Create(nameof(EntryFlowDirection), typeof(FlowDirection), typeof(EntryView), FlowDirection.RightToLeft, BindingMode.TwoWay);
    public FlowDirection EntryFlowDirection
    {
        get { return (FlowDirection)GetValue(EntryFlowDirectionProperty); }
        set { SetValue(EntryFlowDirectionProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(EntryView), Colors.Gray, BindingMode.OneWay);
    public Color PlaceHolderColor
    {
        get { return (Color)GetValue(PlaceHolderColorProperty); }
        set { SetValue(PlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty ErrorMessageColorProperty = BindableProperty.Create(nameof(ErrorMessageColor), typeof(Color), typeof(EntryView), Colors.OrangeRed, BindingMode.OneWay);
    public Color ErrorMessageColor
    {
        get { return (Color)GetValue(ErrorMessageColorProperty); }
        set { SetValue(ErrorMessageColorProperty, value); }
    }
    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(nameof(ActivePlaceHolderColor), typeof(Color), typeof(EntryView), Colors.Gray, BindingMode.OneWay);
    public Color ActivePlaceHolderColor
    {
        get { return (Color)GetValue(ActivePlaceHolderColorProperty); }
        set { SetValue(ActivePlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryView), Colors.Black, BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(EntryView), default(string), BindingMode.TwoWay);
    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(EntryView), default(string), BindingMode.TwoWay);
    public string ErrorMessage
    {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryView), default(bool), BindingMode.TwoWay);
    public bool IsValid
    {
        get { return (bool)GetValue(IsValidProperty); }
        set { SetValue(IsValidProperty, value); }
    }
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(EntryView), default(bool), BindingMode.TwoWay);
    public bool IsPassword
    {
        get { return (bool)GetValue(IsPasswordProperty); }
        set { SetValue(IsPasswordProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(EntryView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(EntryView), default(ReturnType), BindingMode.TwoWay);
    public ReturnType ReturnType
    {
        get { return (ReturnType)GetValue(ReturnTypeProperty); }
        set { SetValue(ReturnTypeProperty, value); }
    }
    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(EntryView), default(ICommand), BindingMode.TwoWay);
    public ICommand ReturnCommand
    {
        get { return (ICommand)GetValue(ReturnCommandProperty); }
        set { SetValue(ReturnCommandProperty, value); }
    }
    public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(EntryView), default(ICommand), BindingMode.TwoWay);
    public ICommand TextChangedCommand
    {
        get { return (ICommand)GetValue(TextChangedCommandProperty); }
        set { SetValue(TextChangedCommandProperty, value); }
    }
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EntryView), default(Keyboard), BindingMode.TwoWay);
    public Keyboard Keyboard
    {
        get { return (Keyboard)GetValue(KeyboardProperty); }
        set { SetValue(KeyboardProperty, value); }
    }
    #endregion

    public event EventHandler<TextChangedEventArgs> TextChanged;

    public EntryView()
    {
        InitializeComponent();

        if (this._color == null)
            this._color = this.PlaceHolderColor;
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
            PlaceHolderColor = this.IsEnabled ? PlaceHolderColor : Colors.Gray;

        if (propertyName == AppendTextProperty.PropertyName)
            append.Text = AppendText;

        if (propertyName == IsValidProperty.PropertyName)
            this.entry.Focus();
    }

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (TextChangedCommand != null)
            TextChangedCommand.Execute(e.NewTextValue);
        if (TextChanged != null)
            TextChanged.Invoke(sender, e);
    }
}