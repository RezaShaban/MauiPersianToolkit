using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TabView : ContentView
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
    public static readonly BindableProperty SelectedTabColorProperty = BindableProperty.Create(nameof(SelectedTabColor), typeof(Color), typeof(TabView), Colors.White, BindingMode.TwoWay);
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
            foreach (TabItemView item in ItemsSource.Where(x => x.IsVisible))
            {
                tabPages.Add(item, 0, 0);
                var tabButton = new Button()
                {
                    IsEnabled = item.IsEnabled,
                    Text = item.Icon,
                    StyleId = "icon",
                    FontFamily = "FontAwesome",
                    FontSize = 24,
                    Padding = new Thickness(0, 5, 0, (AnimateCaptions ? 5 : 23)),
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill,
                    TextColor = UnSelectedTabColor,
                    BackgroundColor = Colors.White
                };
                var label = new Label()
                {
                    IsEnabled = item.IsEnabled,
                    Text = item.Title,
                    FontSize = 13,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center,
                    InputTransparent = true,
                    TextColor = UnSelectedTabColor,
                    Margin = new Thickness(0, 0, 5, 0),
                    Scale = AnimateCaptions ? 0 : 1
                };
                //if (AnimateCaptions)
                //    label.ScaleTo(0);
                tabButton.Command = new Command(() => ExecuteChangeTabCommand(item, tabButton, label));
                this.tabButtons.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100 / ItemsSource.Count, GridUnitType.Star) });
                this.tabButtons.Add(tabButton, ItemsSource.IndexOf(item), 0);
                this.tabButtons.Add(label, ItemsSource.IndexOf(item), 0);
                if (ItemsSource.IndexOf(item) == SelectedTab)
                    currentTab = tabButton;
            }
            if (currentTab != null && currentTab.Command != null)
                currentTab.Command.Execute(null);
        }
        catch (Exception ex)
        {
        }
    }

    private Color GetFromResource(string key)
    {
        try
        {
            return Colors.Black; //(Color)App.Current.Resources[key];
        }
        catch (Exception)
        {
            return Colors.Red;
        }
    }
    private void ExecuteChangeTabCommand(object control, object button, object label)
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
                btn.TextColor = UnSelectedTabColor;
            }
            if (item is Label lbl)
            {
                if (AnimateCaptions)
                    lbl.ScaleTo(0);
                lbl.TextColor = UnSelectedTabColor;
            }
        }

        var currentBtn = ((Button)button);
        currentBtn.TextColor = SelectedTabColor;
        if (AnimateCaptions)
            currentBtn.Padding = new Thickness(0, 5, 0, 23);
        var currentLabel = ((Label)label);
        currentLabel.TextColor = SelectedTabColor;
        if (AnimateCaptions)
            currentLabel.ScaleTo(1);// = 0;
        tab.IsVisible = true;

        if (ChangedTabCommand != null)
            ChangedTabCommand.Execute(tab.Key);
    }
}