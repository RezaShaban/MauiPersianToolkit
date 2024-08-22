namespace MauiPersianToolkit.Controls;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EditorView : ContentView
{
    #region Propertie's
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EditorView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(EditorView), default(string), BindingMode.TwoWay);
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(nameof(PlaceHolderColor), typeof(Color), typeof(EditorView), Colors.Gray, BindingMode.TwoWay);
    public Color PlaceHolderColor
    {
        get { return (Color)GetValue(PlaceHolderColorProperty); }
        set { SetValue(PlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(nameof(ActivePlaceHolderColor), typeof(Color), typeof(EditorView), Colors.Gray, BindingMode.TwoWay);
    public Color ActivePlaceHolderColor
    {
        get { return (Color)GetValue(ActivePlaceHolderColorProperty); }
        set { SetValue(ActivePlaceHolderColorProperty, value); }
    }
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EditorView), Colors.Black, BindingMode.TwoWay);
    public Color TextColor
    {
        get { return (Color)GetValue(TextColorProperty); }
        set { SetValue(TextColorProperty, value); }
    }
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(EditorView), default(string), BindingMode.TwoWay);
    public string PlaceHolder
    {
        get { return (string)GetValue(PlaceHolderProperty); }
        set { SetValue(PlaceHolderProperty, value); }
    }
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(EditorView), default(string), BindingMode.TwoWay);
    public string ErrorMessage
    {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EditorView), default(bool), BindingMode.TwoWay);
    public bool IsValid
    {
        get { return (bool)GetValue(IsValidProperty); }
        set { SetValue(IsValidProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(EditorView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(EditorView), default(Keyboard), BindingMode.TwoWay);
    public Keyboard Keyboard
    {
        get { return (Keyboard)GetValue(KeyboardProperty); }
        set { SetValue(KeyboardProperty, value); }
    }
    #endregion

    private Color placeholderColor;

    public EditorView()
    {
        InitializeComponent();
        txtEditor.Focused += Editor_Focused;
        txtEditor.Unfocused += Editor_Unfocused;
    }

    private void Editor_Focused(object sender, FocusEventArgs e)
    {
        var activeColor = this.ActivePlaceHolderColor; // (Color)Application.Current.Resources[$"Primary{Application.Current.RequestedTheme}"];
        this.placeholderColor = this.PlaceHolderColor;
        this.PlaceHolderColor = activeColor;
        this.ActivePlaceHolderColor = activeColor;
        rectangle.Stroke = new SolidColorBrush(activeColor);
    }

    private void Editor_Unfocused(object sender, FocusEventArgs e)
    {
        PlaceHolderColor = this.placeholderColor;
        rectangle.Stroke = new SolidColorBrush(Color.FromArgb("#a4a6a9"));
    }
}
