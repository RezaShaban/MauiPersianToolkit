using System.Collections;
using System.Runtime.CompilerServices;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PickerView : ContentView
{
    #region Field's
    Color _color;
    #endregion

    #region Propertie's

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty HasSearchbarProperty = BindableProperty.Create(nameof(HasSearchbar), typeof(bool), typeof(PickerView), default(bool), BindingMode.TwoWay);
    public bool HasSearchbar
    {
        get { return (bool)GetValue(HasSearchbarProperty); }
        set { SetValue(HasSearchbarProperty, value); }
    }
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
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
    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(PickerView), default(bool), BindingMode.TwoWay);
    public bool IsPassword
    {
        get { return (bool)GetValue(IsPasswordProperty); }
        set { SetValue(IsPasswordProperty, value); }
    }
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(PickerView), default(string), BindingMode.TwoWay);
    public string Icon
    {
        get { return (string)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(PickerView), default(ReturnType), BindingMode.TwoWay);
    public ReturnType ReturnType
    {
        get { return (ReturnType)GetValue(ReturnTypeProperty); }
        set { SetValue(ReturnTypeProperty, value); }
    }
    public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(Command), typeof(PickerView), default(Command), BindingMode.TwoWay);
    public Command ReturnCommand
    {
        get { return (Command)GetValue(ReturnCommandProperty); }
        set { SetValue(ReturnCommandProperty, value); }
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
    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(PickerView), default(Keyboard), BindingMode.TwoWay);
    public Keyboard Keyboard
    {
        get { return (Keyboard)GetValue(KeyboardProperty); }
        set { SetValue(KeyboardProperty, value); }
    }
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(PickerView), default(IList), BindingMode.TwoWay);
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly BindableProperty AdditionButtonsProperty = BindableProperty.Create(nameof(AdditionButtons), typeof(IList<PickerButton>), typeof(PickerView), default(IList<PickerButton>), BindingMode.TwoWay);
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

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(List<object>), typeof(PickerView), default(List<object>), BindingMode.TwoWay);
    public List<object> SelectedItems
    {
        get { return (List<object>)GetValue(SelectedItemsProperty); }
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

    public static readonly BindableProperty ShowAsButtonProperty = BindableProperty.Create(nameof(ShowAsButton), typeof(bool), typeof(PickerView), default(bool), BindingMode.TwoWay);
    public bool ShowAsButton
    {
        get { return (bool)GetValue(ShowAsButtonProperty); }
        set { SetValue(ShowAsButtonProperty, value); }
    }

    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(PickerView), default(int), BindingMode.TwoWay);
    public int CornerRadius
    {
        get { return (int)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

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
        {
            lblIcon.IsVisible = !string.IsNullOrEmpty(Icon);
        }

        if (propertyName == TextProperty.PropertyName)
        {
            if (string.IsNullOrEmpty(txtEntry.Text))
                PullDownPlaceHolder();
            else
                PullUpPlaceHolder();
        }

        if (propertyName == WidthProperty.PropertyName)
        {
            rectangle.WidthRequest = this.Width;
            rectangle.Stroke = new SolidColorBrush(Color.FromArgb("#a4a6a9"));
        }

        if (propertyName == DisplayPropertyProperty.PropertyName)
        {
            if (string.IsNullOrEmpty(DisplayProperty))
            {
                DisplayProperty = ".";
            }
            txtEntry.SetBinding(Entry.TextProperty, DisplayProperty);
        }

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

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        PullUpPlaceHolder();
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(txtEntry.Text))
            PullDownPlaceHolder();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (OnOpenCommand != null)
            OnOpenCommand.Execute(null);

        var ItemsList = GetListItems();

        if (HasSearchbar)
            ItemsList.Margin = new Thickness(0, 70, 0, 0);

        #region Title Layout
        var titleLayout = new StackLayout()
        {
            Orientation = StackOrientation.Horizontal,
            HorizontalOptions = LayoutOptions.Fill,
            // BackgroundColor = ((Color)App.Current.Resources["PrimaryLight"]),
            Padding = new Thickness(0),
            FlowDirection = FlowDirection.RightToLeft,
        };
        titleLayout.Children.Add(new Label()
        {
            Text = (string.IsNullOrEmpty(Title) ? PlaceHolder : Title),
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 15
        });
        if (AdditionButtons != null)
            foreach (var x in AdditionButtons)
            {
                x.FontFamily = "FontAwesome.ttf#FontAwesome";
                x.Padding = 0;
                titleLayout.Children.Add(x);
            }
        #endregion

        #region Buttons Layout
        var buttonLayout = new Grid()
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

        if (SelectionMode == SelectionMode.Single)
            buttonLayout.ColumnDefinitions.Clear();

        buttonLayout.Add(new Button()
        {
            Text = "انصراف",
            TextColor = (Color)App.Current.Resources["CancelButton"],
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Colors.Transparent, //.FromHex("#ff4081"),
            Command = new Command(() =>
            {
                    //TODO: use popup navigation
                //PopupNavigation.Instance.PopAsync();
            })
        }, SelectionMode == SelectionMode.Single ? 0 : 1, 0);

        if (SelectionMode == SelectionMode.Multiple)
            buttonLayout.Add(new Button()
            {
                IsVisible = SelectionMode == SelectionMode.Multiple,
                Text = "تایید",
                TextColor = (Color)App.Current.Resources["AcceptButton"],
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Transparent, //.FromHex("#ff4081"),
                Command = new Command(() =>
                {
                    SelectedItems = new List<object>();
                    SelectedItems.AddRange(ItemsList.SelectedItems);

                    if (AcceptCommand != null)
                        AcceptCommand.Execute(SelectedItems);

                    //TODO: use popup navigation
                    //PopupNavigation.Instance.PopAsync();
                })
            }, 0, 0);
        #endregion


        //TODO: use popup navigation
        //await PopupNavigation.Instance.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage()
        //{
        //    CloseWhenBackgroundIsClicked = true,
        //    Animation = new ScaleAnimation(Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom, Rg.Plugins.Popup.Enums.MoveAnimationOptions.Bottom)
        //    {
        //        DurationIn = 250,
        //        DurationOut = 250,
        //        ScaleIn = 1,
        //        ScaleOut = 1,
        //    },
        //    Content = new Frame()
        //    {
        //        VerticalOptions = LayoutOptions.End,
        //        Padding = new Thickness(0),
        //        Margin = new Thickness(0),
        //        HasShadow = true,
        //        CornerRadius = 0,
        //        IsClippedToBounds = true,
        //        BorderColor = Colors.Transparent,
        //        BackgroundColor = ((Color)App.Current.Resources["PrimaryLight"]),
        //        Content = new StackLayout()
        //        {
        //            BackgroundColor = Colors.White,
        //            VerticalOptions = LayoutOptions.FillAndExpand,
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            Padding = new Thickness(15, 15, 15, 0),
        //            Children =
        //            {
        //                //Dialog Title
        //                titleLayout,

        //                //Dialog message
        //                new StackLayout()
        //                {
        //                    HorizontalOptions = LayoutOptions.FillAndExpand,
        //                    BackgroundColor = Colors.White,
        //                    VerticalOptions = LayoutOptions.FillAndExpand,
        //                    Children =
        //                    {
        //                        ItemsList
        //                    }
        //                },

        //                //Dialog buttons
        //                buttonLayout
        //            }
        //        }
        //    }
        //});
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

    private CollectionView GetListItems()
    {
        try
        {
            var defaultTemplate = new DataTemplate(() =>
                {
                    var label = new LabelView()
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 14,
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
                        Children =
                        {
                            label
                        }
                    };

                    return rowLayout;
                });
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
                //HasUnevenRows = true,
                ItemsSource = this.ItemsSource,
                //IsPullToRefreshEnabled = false,
                ItemTemplate = ItemTemplate ?? defaultTemplate
            };
            list.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                if (SelectionMode == SelectionMode.Single)
                {
                    if (e.CurrentSelection.Count > 0)
                        SelectedItem = e.CurrentSelection[0];
                    if (SelectedItem == null)
                        return;
                    //TODO: use popup navigation
                    //PopupNavigation.Instance.PopAsync();

                    if (SelectionChanged != null)
                        SelectionChanged.Invoke(this, e);
                    if (SelectionChangedCommand != null)
                        SelectionChangedCommand.Execute(SelectionChangedCommandParameter ?? SelectedItem);
                }
            };

            return list;
        }
        catch (Exception)
        {
            return new CollectionView();
        }
    }

    void PullUpPlaceHolder()
    {
        lblPlaceholder.TranslateTo(0, -23);
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

    public void ShowDialog()
    {
        TapGestureRecognizer_Tapped(null, null);
    }

    #endregion

}

public class PickerButton : Button
{
    public PickerButton()
    {
        StyleId = "plus";
        FontSize = 24;
        BackgroundColor = Colors.White;
        HorizontalOptions = LayoutOptions.End;
        WidthRequest = 32;
        HeightRequest = 32;
        TextColor = Color.FromArgb("#666");
    }
}