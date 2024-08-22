using System.Runtime.CompilerServices;

namespace MauiPersianToolkit.Controls;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TreeViewNode : ContentView
{
    public event EventHandler<TreeViewItem> SelectedItemChanged;
    public TreeViewNode()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ShowItemProperty = BindableProperty.Create(nameof(ShowItem), typeof(TreeViewItem), typeof(TreeViewNode), default(TreeViewItem), BindingMode.TwoWay);
    public TreeViewItem ShowItem
    {
        get { return (TreeViewItem)GetValue(ShowItemProperty); }
        set { SetValue(ShowItemProperty, value); }
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList<TreeViewItem>), typeof(TreeViewNode), default(IList<TreeViewItem>), BindingMode.TwoWay);
    public IList<TreeViewItem> ItemsSource
    {
        get { return (IList<TreeViewItem>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(TreeViewNode), default(bool), BindingMode.TwoWay);
    public bool IsExpanded
    {
        get { return (bool)GetValue(IsExpandedProperty); }
        set { SetValue(IsExpandedProperty, value); }
    }

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(ControlTemplate), typeof(TreeViewNode), default(ControlTemplate), BindingMode.TwoWay);
    public ControlTemplate ItemTemplate
    {
        get => (ControlTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == nameof(IsExpanded))
        {
            if (ChildItems.Children.Count == 0 && IsExpanded)
            {
                var childItemsSource = this.ItemsSource.Where(x => object.Equals(x.ParentId, ShowItem.Id)).ToList();
                double destinationHeight = childItemsSource.Count * 32;
                bool canCollapse = ChildItems.HeightRequest >= destinationHeight;

                foreach (var item in childItemsSource)
                {
                    item.ChildItems = ItemsSource.Where(x => object.Equals(x.ParentId, item.Id)).ToList();

                    var treeViewNode = new TreeViewNode()
                    {
                        ShowItem = item,
                        ItemTemplate = this.ItemTemplate,
                        ItemsSource = this.ItemsSource
                    };
                    treeViewNode.grdItem.RowDefinitions[0].Height = this.grdItem.RowDefinitions[0].Height;
                    treeViewNode.SelectedItemChanged += this.SelectedItemChanged;
                    if (item.SelectionMode == Enums.TreeViewSelectionMode.Single)
                        treeViewNode.rdo.IsChecked = item.IsSelected;
                    if (item.SelectionMode == Enums.TreeViewSelectionMode.Multiple)
                        treeViewNode.chk.IsChecked = item.IsSelected;
                    ChildItems.Children.Add(treeViewNode);
                }
            }
            else
                ChildItems.Children.Clear();
        }

        if(propertyName == nameof(ItemTemplate))
            defaultTemplate.IsVisible = ItemTemplate is null;

        base.OnPropertyChanged(propertyName);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button expandButton ||
            expandButton.CommandParameter is not VerticalStackLayout childItems)
            return;

        IsExpanded = !IsExpanded;
        return;

        var childItemsSource = this.ItemsSource.Where(x => object.Equals(x.ParentId, ShowItem.Id)).ToList();
        double destinationHeight = childItemsSource.Count * 32;
        bool canCollapse = childItems.HeightRequest >= destinationHeight;

        if (childItems.Children.Count == 0)
        {
            IsExpanded = true;
            foreach (var item in childItemsSource)
            {
                item.ChildItems = ItemsSource.Where(x => object.Equals(x.ParentId, item.Id)).ToList();

                var treeViewNode = new TreeViewNode()
                {
                    ShowItem = item,
                    ItemsSource = this.ItemsSource
                };
                treeViewNode.SelectedItemChanged += this.SelectedItemChanged;
                if (item.SelectionMode == Enums.TreeViewSelectionMode.Single)
                    treeViewNode.rdo.IsChecked = item.IsSelected;
                if (item.SelectionMode == Enums.TreeViewSelectionMode.Multiple)
                    treeViewNode.chk.IsChecked = item.IsSelected;
                childItems.Children.Add(treeViewNode);
            }
        }
        else
        {
            childItems.Children.Clear();
            IsExpanded = false;
        }

        var animation = new Animation((arg) => { },
            canCollapse ? destinationHeight : 0,
            canCollapse ? 0 : destinationHeight, Easing.Linear, null);
        childItems.Animate("expanding", animation, 1, 100, Easing.Linear);
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (SelectedItemChanged is null || this.ItemsSource is null)
            return;

        this.ShowItem.IsSelected = e.Value;
        var item = this.ItemsSource.FirstOrDefault(x => object.Equals(x.Id, this.ShowItem.Id));
        if (item is not null)
            item.IsSelected = e.Value;
        SelectedItemChanged.Invoke(sender, this.ShowItem);
    }
}