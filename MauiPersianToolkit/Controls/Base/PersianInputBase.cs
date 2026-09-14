namespace MauiPersianToolkit.Controls;

/// <summary>
/// Shared surface of the toolkit's data-entry controls: a floating placeholder, a leading
/// glyph, and inline validation feedback.
/// </summary>
/// <remarks>
/// Derive from this rather than from <see cref="ContentView"/> so a new input control picks
/// up the common bindable properties, and so styles targeting the base apply to all of them.
/// </remarks>
public abstract class PersianInputBase : ContentView
{
    protected PersianInputBase()
    {
        // Library XAML StaticResource cannot reliably see Application MergedDictionaries.
        PersianTheme.SeedControlResources(Resources);
    }

    /// <summary>Backing store for <see cref="PlaceHolder"/>.</summary>
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(
        nameof(PlaceHolder), typeof(string), typeof(PersianInputBase), default(string), BindingMode.TwoWay);

    /// <summary>Backing store for <see cref="PlaceHolderColor"/>.</summary>
    public static readonly BindableProperty PlaceHolderColorProperty = BindableProperty.Create(
        nameof(PlaceHolderColor), typeof(Color), typeof(PersianInputBase), null, BindingMode.TwoWay,
        defaultValueCreator: _ => ThemeColors.Muted);

    /// <summary>Backing store for <see cref="ActivePlaceHolderColor"/>.</summary>
    public static readonly BindableProperty ActivePlaceHolderColorProperty = BindableProperty.Create(
        nameof(ActivePlaceHolderColor), typeof(Color), typeof(PersianInputBase), null, BindingMode.TwoWay,
        defaultValueCreator: _ => ThemeColors.Muted);

    /// <summary>Backing store for <see cref="TextColor"/>.</summary>
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor), typeof(Color), typeof(PersianInputBase), null, BindingMode.TwoWay,
        defaultValueCreator: _ => ThemeColors.OnSurface);

    /// <summary>Backing store for <see cref="Icon"/>.</summary>
    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon), typeof(string), typeof(PersianInputBase), string.Empty, BindingMode.TwoWay);

    /// <summary>Backing store for <see cref="ErrorMessage"/>.</summary>
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
        nameof(ErrorMessage), typeof(string), typeof(PersianInputBase), default(string), BindingMode.TwoWay);

    /// <summary>Backing store for <see cref="IsValid"/>.</summary>
    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
        nameof(IsValid), typeof(bool), typeof(PersianInputBase), default(bool), BindingMode.TwoWay);

    /// <summary>
    /// Label shown above the input while it is empty.
    /// </summary>
    public string PlaceHolder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }

    /// <summary>
    /// Color of the placeholder while the input is not focused.
    /// </summary>
    public Color PlaceHolderColor
    {
        get => (Color)GetValue(PlaceHolderColorProperty);
        set => SetValue(PlaceHolderColorProperty, value);
    }

    /// <summary>
    /// Color of the placeholder while the input is focused.
    /// </summary>
    public Color ActivePlaceHolderColor
    {
        get => (Color)GetValue(ActivePlaceHolderColorProperty);
        set => SetValue(ActivePlaceHolderColorProperty, value);
    }

    /// <summary>
    /// Color of the value the control displays.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Leading glyph, expressed as a character of the toolkit's icon font.
    /// </summary>
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Validation message shown beneath the input.
    /// </summary>
    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }

    /// <summary>
    /// Whether the control is currently showing its validation message.
    /// </summary>
    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }
}
