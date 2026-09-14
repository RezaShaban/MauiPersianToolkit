namespace MauiPersianToolkit.Controls;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class EditorView : PersianInputBase
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
        rectangle.Stroke = new SolidColorBrush(ThemeColors.Outline);
    }
}
