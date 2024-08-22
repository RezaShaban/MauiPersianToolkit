using MauiPersianToolkit.Enums;
using MauiPersianToolkit.ViewModels;
using System.Collections;
using System.Runtime.CompilerServices;

namespace MauiPersianToolkit.Controls;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class TreeView : ContentView
{
    public TreeView()
    {
        InitializeComponent();
    }

    #region Propertie's
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TreeView), default(IList), BindingMode.TwoWay);
    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(TreeView), default(IList), BindingMode.TwoWay);
    public IList SelectedItems
    {
        get { return (IList)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public static readonly BindableProperty DisplayPropertyProperty = BindableProperty.Create(nameof(DisplayProperty), typeof(string), typeof(TreeView), default(string), BindingMode.TwoWay);
    public string DisplayProperty
    {
        get { return (string)GetValue(DisplayPropertyProperty); }
        set { SetValue(DisplayPropertyProperty, value); }
    }

    public static readonly BindableProperty KeyPropertyProperty = BindableProperty.Create(nameof(KeyProperty), typeof(string), typeof(TreeView), default(string), BindingMode.TwoWay);
    public string KeyProperty
    {
        get { return (string)GetValue(KeyPropertyProperty); }
        set { SetValue(KeyPropertyProperty, value); }
    }

    public static readonly BindableProperty SelectionModeProperty = BindableProperty.Create(nameof(SelectionMode), typeof(TreeViewSelectionMode), typeof(TreeView), default(TreeViewSelectionMode), BindingMode.TwoWay);
    public TreeViewSelectionMode SelectionMode
    {
        get { return (TreeViewSelectionMode)GetValue(SelectionModeProperty); }
        set { SetValue(SelectionModeProperty, value); }
    }

    public static readonly BindableProperty ParentChildPropertyProperty = BindableProperty.Create(nameof(ParentChildProperty), typeof(string), typeof(TreeView), default(string), BindingMode.TwoWay);
    public string ParentChildProperty
    {
        get { return (string)GetValue(ParentChildPropertyProperty); }
        set { SetValue(ParentChildPropertyProperty, value); }
    }

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(ControlTemplate), typeof(TreeView), default(ControlTemplate), BindingMode.TwoWay);
    public ControlTemplate ItemTemplate
    {
        get => (ControlTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(int), typeof(TreeView), 32, BindingMode.TwoWay);
    public int ItemHeight
    {
        get { return (int)GetValue(ItemHeightProperty); }
        set { SetValue(ItemHeightProperty, value); }
    }
    #endregion

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        if (propertyName == nameof(ItemsSource))
        {
            items.Children.Clear();
            var parentItems = ItemsSource.OfType<object>().Where(x => x.GetPropertyValue(ParentChildProperty) == null).ToList();
            foreach (var item in parentItems)
            {
                var treeViewNode = new TreeViewNode()
                {
                    ItemTemplate = this.ItemTemplate,
                    ItemsSource = new List<TreeViewItem>(
                        ItemsSource.OfType<object>() //.Where(x => !object.Equals(x.GetType().GetProperty(ParentChildProperty).GetValue(x), null))
                        .Select(x => new TreeViewItem()
                        {
                            Id = x.GetPropertyValue(KeyProperty),
                            ParentId = x.GetPropertyValue(ParentChildProperty),
                            OriginalItem = x,
                            SelectionMode = this.SelectionMode,
                            IsSelected = SelectedItems?.OfType<object>().Any(s => object.Equals(s.GetPropertyValue(KeyProperty), x.GetPropertyValue(KeyProperty))) ?? false,
                            Title = x.GetPropertyValue(DisplayProperty).ToString()
                        }).ToList()),
                    ShowItem = new TreeViewItem()
                    {
                        Id = item.GetPropertyValue(KeyProperty),
                        ParentId = item.GetPropertyValue(ParentChildProperty),
                        OriginalItem = item,
                        SelectionMode = this.SelectionMode,
                        IsSelected = SelectedItems?.OfType<object>().Any(s => object.Equals(s.GetPropertyValue(KeyProperty), item.GetPropertyValue(KeyProperty))) ?? false,
                        Title = item.GetPropertyValue(DisplayProperty).ToString(),
                        ChildItems = ItemsSource.OfType<object>().Where(x => object.Equals(x.GetPropertyValue(ParentChildProperty), item.GetPropertyValue(KeyProperty)))
                        .Select(x => new TreeViewItem()
                        {
                            Id = x.GetPropertyValue(KeyProperty),
                            ParentId = x.GetPropertyValue(ParentChildProperty),
                            OriginalItem = x,
                            IsSelected = SelectedItems?.OfType<object>().Any(s => object.Equals(s.GetPropertyValue(KeyProperty), x.GetPropertyValue(KeyProperty))) ?? false,
                            Title = x.GetPropertyValue(DisplayProperty).ToString()
                        }).ToList()
                    }
                };
                treeViewNode.FindByName<CheckBox>("chk").IsChecked = SelectedItems?.OfType<object>().Any(s => object.Equals(s.GetPropertyValue(KeyProperty), item.GetPropertyValue(KeyProperty))) ?? false;
                treeViewNode.FindByName<RadioButton>("rdo").IsChecked = SelectedItems?.OfType<object>().Any(s => object.Equals(s.GetPropertyValue(KeyProperty), item.GetPropertyValue(KeyProperty))) ?? false;
                treeViewNode.FindByName<Grid>("grdItem").RowDefinitions[0].Height = this.ItemHeight;
                treeViewNode.SelectedItemChanged += TreeViewNode_SelectedItemChanged;
                items.Children.Add(treeViewNode);
            }
        }

        base.OnPropertyChanged(propertyName);
    }

    private void TreeViewNode_SelectedItemChanged(object sender, TreeViewItem e)
    {
        this.SelectedItems ??= new List<object>();

        object item = this.SelectedItems.OfType<object>().FirstOrDefault(x => object.Equals(x.GetPropertyValue(KeyProperty), e.OriginalItem.GetPropertyValue(KeyProperty)));
        if (e.IsSelected && item is null)
        {
            if (SelectionMode == TreeViewSelectionMode.Single)
                this.SelectedItems.Clear();
            this.SelectedItems.Add(e.OriginalItem);
        }
        if (!e.IsSelected && item is not null)
            this.SelectedItems.Remove(item);
    }
}

public class TreeViewItem : ObservableObject
{
    private bool _isSelected;
    public object Id { get; set; }
    public object ParentId { get; set; }
    public string Title { get; set; }
    public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }
    public TreeViewSelectionMode SelectionMode { get; set; }
    public object OriginalItem { get; set; }
    public List<TreeViewItem> ChildItems { get; set; } = new List<TreeViewItem>();
}