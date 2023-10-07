using CommunityToolkit.Maui.Views;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using static Microsoft.Maui.Controls.VisualStateManager;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PickerView : ContentView
{
    #region Propertie's

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty SelectedItemColorProperty = BindableProperty.Create(nameof(SelectedItemColor), typeof(Color), typeof(PickerView), Colors.Orange, BindingMode.TwoWay);
    public Color SelectedItemColor
    {
        get { return (Color)GetValue(SelectedItemColorProperty); }
        set { SetValue(SelectedItemColorProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(PickerView), "انصراف", BindingMode.TwoWay);
    public string CancelText
    {
        get { return (string)GetValue(CancelTextProperty); }
        set { SetValue(CancelTextProperty, value); }
    }

    public static readonly BindableProperty AcceptTextProperty = BindableProperty.Create(nameof(AcceptText), typeof(string), typeof(PickerView), "تایید", BindingMode.TwoWay);
    public string AcceptText
    {
        get { return (string)GetValue(AcceptTextProperty); }
        set { SetValue(AcceptTextProperty, value); }
    }

    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(SelectionMode), typeof(PickerView), SelectionMode.Single, BindingMode.TwoWay);
    public SelectionMode SelectionMode
    {
        get { return (SelectionMode)GetValue(SelectionModeProperty); }
        set { SetValue(SelectionModeProperty, value); }
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

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
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

    public static readonly BindableProperty SelectionChangedCommandProperty = BindableProperty.Create(nameof(SelectionChangedCommand), typeof(Command), typeof(PickerView), default(Command), BindingMode.TwoWay);
    public Command SelectionChangedCommand
    {
        get { return (Command)GetValue(SelectionChangedCommandProperty); }
        set { SetValue(SelectionChangedCommandProperty, value); }
    }

    public static readonly BindableProperty SelectionChangedCommandParameterProperty = BindableProperty.Create(nameof(SelectionChangedCommandParameter), typeof(object), typeof(PickerView), null, BindingMode.TwoWay);
    public object SelectionChangedCommandParameter
    {
        get { return (object)GetValue(SelectionChangedCommandParameterProperty); }
        set { SetValue(SelectionChangedCommandParameterProperty, value); }
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(PickerView), default(IList), BindingMode.TwoWay);
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly BindableProperty AdditionButtonsProperty = BindableProperty.Create(nameof(AdditionButtons), typeof(IList<PickerButton>), typeof(PickerView), new List<PickerButton>(), BindingMode.TwoWay);
    public IList<PickerButton> AdditionButtons
    {
        get { return (IList<PickerButton>)GetValue(AdditionButtonsProperty); }
        set { SetValue(AdditionButtonsProperty, value); }
    }

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(PickerView), default(DataTemplate), BindingMode.TwoWay);
    public DataTemplate ItemTemplate
    {
        get { return (DataTemplate)GetValue(ItemTemplateProperty); }
        set { SetValue(ItemTemplateProperty, value); }
    }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(PickerView), default(object), BindingMode.TwoWay);
    public object SelectedItem
    {
        get { return (object)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(PickerView), default(object), BindingMode.TwoWay);
    public object SelectedValue
    {
        get { return (object)GetValue(SelectedValueProperty); }
        set { SetValue(SelectedValueProperty, value); }
    }

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(ObservableCollection<object>), typeof(PickerView), new ObservableCollection<object>(), BindingMode.TwoWay);
    public ObservableCollection<object> SelectedItems
    {
        get { return (ObservableCollection<object>)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(PickerView), null, BindingMode.TwoWay);
    public int SelectedIndex
    {
        get { return (int)GetValue(SelectedIndexProperty); }
        set { SetValue(SelectedIndexProperty, value); }
    }

    public static readonly BindableProperty DisplayPropertyProperty = BindableProperty.Create(nameof(DisplayProperty), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string DisplayProperty
    {
        get { return (string)GetValue(DisplayPropertyProperty); }
        set { SetValue(DisplayPropertyProperty, value); }
    }

    public static readonly BindableProperty ValueMemberProperty = BindableProperty.Create(nameof(ValueMember), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string ValueMember
    {
        get { return (string)GetValue(ValueMemberProperty); }
        set { SetValue(ValueMemberProperty, value); }
    }

    public static readonly BindableProperty RowIconPropertyProperty = BindableProperty.Create(nameof(RowIconProperty), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string RowIconProperty
    {
        get { return (string)GetValue(RowIconPropertyProperty); }
        set { SetValue(RowIconPropertyProperty, value); }
    }

    //public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(PickerView), default(int), BindingMode.TwoWay);
    //public int CornerRadius
    //{
    //    get { return (int)GetValue(CornerRadiusProperty); }
    //    set { SetValue(CornerRadiusProperty, value); }
    //}

    public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand), typeof(Command), typeof(PickerView), default(Command), BindingMode.TwoWay);
    public Command AcceptCommand
    {
        get { return (Command)GetValue(AcceptCommandProperty); }
        set { SetValue(AcceptCommandProperty, value); }
    }

    public static readonly BindableProperty OnOpenCommandProperty = BindableProperty.Create(nameof(OnOpenCommand), typeof(Command), typeof(PickerView), default(Command), BindingMode.TwoWay);

    public Command OnOpenCommand
    {
        get { return (Command)GetValue(OnOpenCommandProperty); }
        set { SetValue(OnOpenCommandProperty, value); }
    }
    #endregion

    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
    Page mainPage;

    public PickerView()
    {
        InitializeComponent();
    }
    #region Event's

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (propertyName == IsEnabledProperty.PropertyName)
            PlaceHolderColor = this.IsEnabled ? PlaceHolderColor : Colors.Gray;

        if (propertyName == IconProperty.PropertyName)
            lblIcon.IsVisible = !string.IsNullOrEmpty(Icon);

        if (propertyName == SelectedIndexProperty.PropertyName)
            if (ItemsSource != null && ItemsSource.Count > SelectedIndex && SelectedIndex >= 0)
                SelectedItem = ItemsSource[SelectedIndex];

        if (propertyName == SelectedValueProperty.PropertyName && ItemsSource != null && !string.IsNullOrEmpty(ValueMember) && SelectedValue != null)
            foreach (var item in ItemsSource)
            {
                if (SelectedValue.Equals(item.GetType().GetProperty(ValueMember).GetValue(item)))
                    SelectedItem = item;
            }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        OnOpenCommand?.Execute(null);

        SetMainPage();
        var popupPage = this.popupPage;

        var ItemsList = this.GetListItems(popupPage);

        //if (HasSearchbar)
        //    ItemsList.Margin = new Thickness(0, 70, 0, 0);

        #region Title Layout
        var titleLayout = TitleLayout();
        if (AdditionButtons != null)
        {
            foreach (var x in AdditionButtons)
                titleLayout.Children.Add(x.ShallowCopy());

            var hasWidth = AdditionButtons.Sum(x => x.WidthRequest);
            var first = (Label)titleLayout.Children.FirstOrDefault();
            first.WidthRequest = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) - 45 - hasWidth;
        }
        #endregion

        #region Buttons Layout

        var buttonLayout = ButtonLayout;
        if (SelectionMode == SelectionMode.Single)
            buttonLayout.ColumnDefinitions.Clear();

        popupPage.Content = new Frame()
        {
            VerticalOptions = LayoutOptions.End,
            Padding = new Thickness(0),
            Margin = new Thickness(0),
            HasShadow = true,
            CornerRadius = 7,
            IsClippedToBounds = true,
            BorderColor = Colors.Transparent,
            BackgroundColor = Colors.White,
#if ANDROID
            WidthRequest = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) - 30,
#endif
            Content = new StackLayout()
            {
                BackgroundColor = Colors.White,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 15, 0, 0),
                Children =
                    {
                        titleLayout,
                        new StackLayout()
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            BackgroundColor = Colors.White,
                            VerticalOptions = LayoutOptions.Fill,
                            Children = { ItemsList }
                        },
                        buttonLayout
                    }
            }
        };

        buttonLayout.Add(new Button()
        {
            Text = CancelText,
            TextColor = Colors.OrangeRed,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Colors.Transparent,
            FontFamily = "IranianSans",
            Command = new Command(() =>
            {
                SelectedItems.Clear();
                try { popupPage.Close(); } catch { }
            })
        }, SelectionMode == SelectionMode.Single ? 0 : 1, 0);

        if (SelectionMode == SelectionMode.Multiple)
            buttonLayout.Add(new Button()
            {
                IsVisible = SelectionMode == SelectionMode.Multiple,
                Text = AcceptText,
                TextColor = Colors.Green,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Transparent,
                FontFamily = "IranianSans",
                Command = new Command(() =>
                {
                    AcceptCommand?.Execute(SelectedItems);
                    popupPage.Close();
                })
            }, 0, 0);
        #endregion

        try { this.mainPage.ShowPopup(popupPage); } catch { }
    }

    private void SetMainPage()
    {
        if (mainPage is null)
            mainPage = Application.Current.MainPage;
    }

    private Grid ButtonLayout => new()
    {
        BackgroundColor = Color.FromArgb("#f5f5f5"),
        HorizontalOptions = LayoutOptions.Fill,
        RowDefinitions = new RowDefinitionCollection()
        {
            new RowDefinition(){ Height = 50 }
        },
        ColumnDefinitions = new ColumnDefinitionCollection()
        {
            new ColumnDefinition(){ Width = new GridLength(50,GridUnitType.Star) },
            new ColumnDefinition(){ Width = new GridLength(50,GridUnitType.Star) }
        },
    };

    private HorizontalStackLayout TitleLayout() => new HorizontalStackLayout()
    {
        HorizontalOptions = LayoutOptions.Fill,
        Padding = new Thickness(0, 0, 0, 5),
        FlowDirection = FlowDirection.RightToLeft,
        Children =
        {
            new Label()
            {
                Text = string.IsNullOrEmpty(Title) ? PlaceHolder : Title,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "IranianSans",
                FontSize = 15,
                TextColor = Colors.Gray,
                Padding = new Thickness(15, 0)
            }
        }
    };

    private Popup popupPage => new()
    {
        VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.End,
        HorizontalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Fill,
        Color = Colors.Transparent,
        Parent = null
    };

    #endregion

    #region Method's

    private CollectionView GetListItems(Popup popupPage)
    {
        try
        {
            var list = new CollectionView()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                SelectionMode = SelectionMode,
                HeightRequest = 300,
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    ItemSpacing = 0
                },
                ItemsSource = this.ItemsSource,
                SelectedItem = this.SelectedItem,
                SelectedItems = this.SelectedItems,
                ItemTemplate = ItemTemplate ?? DefaultItemTemplate
            };

            list.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                if (((CollectionView)sender).SelectionMode == SelectionMode.Single)
                {
                    if (e.CurrentSelection.Count > 0)
                        SelectedItem = e.CurrentSelection[0];
                    if (SelectedItem == null)
                        return;

                    if (SelectionChanged != null)
                        SelectionChanged.Invoke(this, e);
                    if (SelectionChangedCommand != null)
                        SelectionChangedCommand.Execute(SelectionChangedCommandParameter ?? SelectedItem);

                    try { popupPage.Close(); } catch { }
                }
            };

            if (!list.Resources.Any(x => x.Key == "Microsoft.Maui.Controls.StackLayout"))
            {
                Setter backgroundColorSetter = new() { Property = BackgroundColorProperty, Value = SelectedItemColor };
                VisualState stateSelected = new() { Name = CommonStates.Selected, Setters = { backgroundColorSetter } };
                VisualState stateNormal = new() { Name = CommonStates.Normal };
                VisualStateGroup visualStateGroup = new() { Name = nameof(CommonStates), States = { stateSelected, stateNormal } };
                VisualStateGroupList visualStateGroupList = new() { visualStateGroup };
                Setter vsgSetter = new() { Property = VisualStateGroupsProperty, Value = visualStateGroupList };
                Style style = new(typeof(StackLayout)) { Setters = { vsgSetter }, BaseResourceKey = "collectionItem" };

                // Add the style to the resource dictionary
                list.Resources.Add(style);
            }

            return list;
        }
        catch (Exception)
        {
            return new CollectionView();
        }
    }

    private DataTemplate DefaultItemTemplate => new(() =>
    {
        var label = new LabelView()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 14,
            IconFontSize = 16,
            TextColor = Colors.Black,
            InputTransparent = true
        };
        if (string.IsNullOrEmpty(DisplayProperty))
            label.SetBinding(LabelView.TextProperty, ".");
        else
            label.SetBinding(LabelView.TextProperty, DisplayProperty);
        if (!string.IsNullOrEmpty(RowIconProperty))
            label.SetBinding(LabelView.IconProperty, RowIconProperty);
        else
            label.Icon = "";

        var rowLayout = new StackLayout()
        {
            Padding = new Thickness(15, 10),
            Orientation = StackOrientation.Horizontal,
            FlowDirection = FlowDirection.RightToLeft,
            Children = { label }
        };

        return rowLayout;
    });

    #endregion

}

public class PickerButton : Button
{
    public PickerButton()
    {
        FontSize = 24;
        BackgroundColor = Colors.White;
        HorizontalOptions = LayoutOptions.End;
        WidthRequest = 32;
        HeightRequest = 32;
        Padding = 0;
        TextColor = Color.FromArgb("#666");
        FontFamily = "FontAwesome";
    }

    public PickerButton ShallowCopy()
    {
        return (PickerButton)this.MemberwiseClone();
    }
}