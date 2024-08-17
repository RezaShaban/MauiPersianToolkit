using System.Runtime.CompilerServices;

namespace MauiPersianToolkit.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CircleImageView : ContentView
{
    #region Propertie's
    public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CircleImageView), default(int), BindingMode.OneWay);
    public int ImageWidth
    {
        get { return (int)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }
    public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CircleImageView), default(int), BindingMode.OneWay);
    public int ImageHeight
    {
        get { return (int)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(CircleImageView), default(ImageSource), BindingMode.TwoWay);
    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }
    public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CircleImageView), default(int), BindingMode.OneWay);
    public int BorderThickness
    {
        get { return (int)GetValue(BorderThicknessProperty); }
        set { SetValue(BorderThicknessProperty, value); }
    }
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CircleImageView), default(Color), BindingMode.OneWay);
    public Color BorderColor
    {
        get { return (Color)GetValue(BorderColorProperty); }
        set { SetValue(BorderColorProperty, value); }
    }

    private int ContainerWidth;
    private int ContainerHeight;

    #endregion
    public CircleImageView()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        try
        {
            if (propertyName == BorderColorProperty.PropertyName)
            {
                ellipse.Stroke = new SolidColorBrush(BorderColor);
            }
            if (propertyName == ImageWidthProperty.PropertyName || propertyName == BorderThicknessProperty.PropertyName)
            {
                imgClip.Center = new Point(ImageWidth / 2, ImageWidth / 2);
                imgClip.RadiusX = ImageWidth / 2;
                imgClip.RadiusY = ImageWidth / 2;
                //}

                //if (propertyName == BorderThicknessProperty.PropertyName)
                //{
                ContainerHeight = ImageHeight + (BorderThickness * 2);
                ContainerWidth = ImageWidth + (BorderThickness * 2);

                grid.HeightRequest = ContainerHeight;
                grid.WidthRequest = ContainerWidth;
                ellipse.WidthRequest = ContainerWidth;
                ellipse.HeightRequest = ContainerHeight;
            }
        }
        catch (Exception) { }
    }
}