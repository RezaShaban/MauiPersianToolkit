using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
using System.Data;
using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabView : Microsoft.Maui.Controls.ContentView
{
    #region Field's
    private bool IsGeneratedTabs = false;
    #endregion
    #region Propertie's
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(List<TabItemView>), typeof(TabView), new List<TabItemView>(), BindingMode.TwoWay);
    public List<TabItemView> ItemsSource
    {
        get { return (List<TabItemView>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public static readonly BindableProperty UnSelectedTabColorProperty = BindableProperty.Create(nameof(UnSelectedTabColor), typeof(Color), typeof(TabView), Color.FromArgb("#b5b5b5"), BindingMode.TwoWay);
    public Color UnSelectedTabColor
    {
        get { return (Color)GetValue(UnSelectedTabColorProperty); }
        set { SetValue(UnSelectedTabColorProperty, value); }
    }
    public static readonly BindableProperty SelectedTabColorProperty = BindableProperty.Create(nameof(SelectedTabColor), typeof(Color), typeof(TabView), Colors.Gray, BindingMode.TwoWay);
    public Color SelectedTabColor
    {
        get { return (Color)GetValue(SelectedTabColorProperty); }
        set { SetValue(SelectedTabColorProperty, value); }
    }

    public static readonly BindableProperty SelectedTabProperty = BindableProperty.Create(nameof(SelectedTab), typeof(int), typeof(TabView), default(int), BindingMode.TwoWay);
    public int SelectedTab
    {
        get { return (int)GetValue(SelectedTabProperty); }
        set { SetValue(SelectedTabProperty, value); }
    }

    public static readonly BindableProperty AnimateCaptionsProperty = BindableProperty.Create(nameof(AnimateCaptions), typeof(bool), typeof(TabView), default(bool), BindingMode.TwoWay);
    public bool AnimateCaptions
    {
        get { return (bool)GetValue(AnimateCaptionsProperty); }
        set { SetValue(AnimateCaptionsProperty, value); }
    }

    public static readonly BindableProperty ChangedTabCommandProperty = BindableProperty.Create(nameof(ChangedTabCommand), typeof(Command), typeof(TabView), default(Command), BindingMode.TwoWay);
    public Command ChangedTabCommand
    {
        get { return (Command)GetValue(ChangedTabCommandProperty); }
        set { SetValue(ChangedTabCommandProperty, value); }
    }
    #endregion
    public TabView()
    {
        InitializeComponent();
        IsGeneratedTabs = false;
        ItemsSource = new List<TabItemView>();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        GenerateTabPages();
    }

    private void GenerateTabPages()
    {
        try
        {
            if (ItemsSource == null || ItemsSource.Count == 0 || IsGeneratedTabs || this.tabPages == null)
                return;
            IsGeneratedTabs = true;
            var currentTab = new Button();
            this.tabButtons.ColumnDefinitions.Clear();
            double width = DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ?
                    DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density :
                    DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            foreach (TabItemView item in ItemsSource.Where(x => x.IsVisible))
            {
                tabPages.Add(item, 0, 0);
                var tabButton = new Button()
                {
                    IsEnabled = item.IsEnabled,
                    Text = item.Icon,
                    FontFamily = "FontAwesome",
                    FontSize = 24,
                    Padding = new Thickness(0, 10, 0, (AnimateCaptions ? 5 : 24)),
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    TextColor = UnSelectedTabColor,
                    BackgroundColor = Colors.White,
                    HeightRequest = 64
                };
                
                var label = new Label()
                {
                    IsEnabled = item.IsEnabled,
                    Text = item.Title,
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center,
                    InputTransparent = true,
                    TextColor = UnSelectedTabColor,
                    Margin = new Thickness(0, 0, 5, 5),
                    Scale = AnimateCaptions ? 0 : 1,
                    FontFamily = "IranianSans"
                };

                var shape = new Microsoft.Maui.Controls.Shapes.Path()
                {
                    ClassId = ItemsSource.IndexOf(item).ToString(),
                    Data = (Geometry)new PathGeometryConverter().ConvertFromInvariantString("M53.5 12H16.5C16.5 14.2091 18.2909 16 20.5 16H49.5C51.7091 16 53.5 14.2091 53.5 12ZM33.3257 19.3366C33.8419 20.2986 36.1581 20.2986 36.6743 19.3366C37.6164 17.5805 38.9944 16 40.9025 16L29.0975 16C31.0056 16 32.3835 17.5805 33.3257 19.3366Z"),
                    VerticalOptions = LayoutOptions.Start,
                    Fill = new SolidColorBrush(Colors.Transparent),
                    HorizontalOptions = LayoutOptions.Fill,
                    IsVisible = true,
                    InputTransparent = true,
                    HeightRequest = 8,
                    TranslationY = 0,
                };
                shape.TranslationX = ((width / ItemsSource.Count) / 2) - (37 / 2);
                //if (AnimateCaptions)
                //    label.ScaleTo(0);
                tabButton.Command = new Command(() => ExecuteChangeTabCommand(item, tabButton, label, shape));
                this.tabButtons.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100 / ItemsSource.Count, GridUnitType.Star) });
                this.tabButtons.Add(tabButton, ItemsSource.IndexOf(item), 0);
                this.tabButtons.Add(shape, ItemsSource.IndexOf(item), 0);
                this.tabButtons.Add(label, ItemsSource.IndexOf(item), 0);
                if (ItemsSource.IndexOf(item) == SelectedTab)
                    currentTab = tabButton;
            }
            if (currentTab != null && currentTab.Command != null)
                currentTab.Command.Execute(null);
        }
        catch (Exception)
        {
        }
    }

    private void ExecuteChangeTabCommand(object control, Button currentBtn, Label currentLabel, Microsoft.Maui.Controls.Shapes.Path shape)
    {
        if (control is not TabItemView tab)
            return;

        foreach (var item in this.tabPages.Children)
            if (item is TabItemView tabItem)
                tabItem.IsVisible = false;

        foreach (var item in this.tabButtons.Children)
        {
            if (item is Button btn)
            {
                if (AnimateCaptions)
                    btn.Padding = new Thickness(0, 5);
                btn.TextColor = this.UnSelectedTabColor;
            }
            if (item is Label lbl)
            {
                if (AnimateCaptions)
                    lbl.Scale = (0);
                lbl.TextColor = this.UnSelectedTabColor;
            }
            if (item is Microsoft.Maui.Controls.Shapes.Path _shape)
            {
                if (AnimateCaptions)
                    _shape.Scale = (0);
                _shape.Fill = new SolidColorBrush(Colors.Transparent);
                _shape.Shadow = new Shadow()
                {
                    Brush = new SolidColorBrush(Colors.Black),
                    Offset = new Point(0, 10),
                    Radius = 80,
                };
                //_shape.IsVisible = _shape.ClassId == shape.ClassId;
            }
        }

        currentBtn.TextColor = SelectedTabColor;
        if (AnimateCaptions)
            currentBtn.Padding = new Thickness(0, 5, 0, 23);

        currentLabel.TextColor = SelectedTabColor;
        if (AnimateCaptions)
            currentLabel.Scale = (1);// = 0;

        shape.Fill = new SolidColorBrush(SelectedTabColor);
        if (AnimateCaptions)
            shape.Scale = (1);// = 0;

        tab.IsVisible = true;

        if (ChangedTabCommand != null)
            ChangedTabCommand.Execute(tab.Key);
    }
}