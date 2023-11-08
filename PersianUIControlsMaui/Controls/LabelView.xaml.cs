using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LabelView : ContentView
{
    public LabelView()
    {
        InitializeComponent();
        var tapped = new TapGestureRecognizer()
        {
            CommandParameter = this.TapCommandParameter,
            Command = TapCommand,
        };
        tapped.Tapped += (object sender, EventArgs e) => Tapped?.Invoke(this, e);
        grdPattern.GestureRecognizers.Add(tapped);
    }

    #region Propertie's

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(LabelView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(LabelView), default(TextAlignment), BindingMode.TwoWay);
    public TextAlignment HorizontalTextAlignment
    {
        get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
        set { SetValue(HorizontalTextAlignmentProperty, value); }
    }

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(nameof(TapCommand), typeof(Command), typeof(LabelView), default(Command), BindingMode.TwoWay);
    public Command TapCommand
    {
        get { return (Command)GetValue(TapCommandProperty); }
        set { SetValue(TapCommandProperty, value); }
    }

    public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(LabelView), default(object), BindingMode.TwoWay);
    public object TapCommandParameter
    {
        get { return (object)GetValue(TapCommandParameterProperty); }
        set { SetValue(TapCommandParameterProperty, value); }
    }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(LabelView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly BindableProperty IconFontSizeProperty = BindableProperty.Create(nameof(IconFontSize), typeof(double), typeof(LabelView), 14d, BindingMode.TwoWay);
    public double IconFontSize
    {
        get { return (double)GetValue(IconFontSizeProperty); }
        set { SetValue(IconFontSizeProperty, value); }
    }

    public static readonly BindableProperty TextDecorationProperty = BindableProperty.Create(nameof(TextDecoration), typeof(TextDecorations), typeof(LabelView), TextDecorations.None, BindingMode.TwoWay);
    public TextDecorations TextDecoration
    {
        get { return (TextDecorations)GetValue(TextDecorationProperty); }
        set { SetValue(TextDecorationProperty, value); }
    }

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(LabelView), 14d, BindingMode.TwoWay);
    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    public static readonly BindableProperty FontAttributeProperty = BindableProperty.Create(nameof(FontAttribute), typeof(FontAttributes), typeof(LabelView), default(FontAttributes), BindingMode.TwoWay);
    public FontAttributes FontAttribute
    {
        get { return (FontAttributes)GetValue(FontAttributeProperty); }
        set { SetValue(FontAttributeProperty, value); }
    }

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(LabelView), Colors.Black, BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }

    #endregion

    public event EventHandler<EventArgs> Tapped;
    //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    base.OnPropertyChanged(propertyName);

    //    if (IconProperty.PropertyName == propertyName)
    //        lblIcon.IsVisible = !string.IsNullOrEmpty(Icon);
    //}
}