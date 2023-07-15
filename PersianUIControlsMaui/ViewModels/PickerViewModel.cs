using PersianUIControlsMaui.Services.Dialog;

namespace PersianUIControlsMaui.ViewModels;

public class PickerViewModel: ObservableObject
{
    private readonly IDialogService dialogService;

    public PickerViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    private void OpenPicker(object obj)
    {
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
            //TextColor = (Color)App.Current.Resources["CancelButton"], TODO: set color
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
                //TextColor = (Color)App.Current.Resources["AcceptButton"], TODO: set color
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

        dialogService.CustomDialog(new Models.CustomDialogConfig()
        {
            Content = new Frame()
            {
                VerticalOptions = LayoutOptions.End,
                Padding = new Thickness(0),
                Margin = new Thickness(0),
                HasShadow = true,
                CornerRadius = 0,
                IsClippedToBounds = true,
                BorderColor = Colors.Transparent,
                //BackgroundColor = ((Color)App.Current.Resources["PrimaryLight"]), TODO: set color
                Content = new StackLayout()
                {
                    BackgroundColor = Colors.White,
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    Padding = new Thickness(15, 15, 15, 0),
                    Children =
                        {
                            //Dialog Title
                            titleLayout,

                            //Dialog message
                            new StackLayout()
                            {
                                HorizontalOptions = LayoutOptions.Fill,
                                BackgroundColor = Colors.White,
                                VerticalOptions = LayoutOptions.Fill,
                                Children =
                                {
                                    ItemsList
                                }
                            },

                            //Dialog buttons
                            buttonLayout
                        }
                }
            }
        });
    }

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
}
