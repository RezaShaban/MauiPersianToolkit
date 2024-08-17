namespace MauiPersianToolkit.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabItemView : ContentView
{
    #region Propertie's
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(TabItemView), default(string), BindingMode.TwoWay);
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(TabItemView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(string), typeof(TabItemView), default(string), BindingMode.TwoWay);
    public string Key
    {
        get { return (string)GetValue(KeyProperty); }
        set { SetValue(KeyProperty, value); }
    }
    #endregion
    public TabItemView()
    {
        InitializeComponent();
    }
}