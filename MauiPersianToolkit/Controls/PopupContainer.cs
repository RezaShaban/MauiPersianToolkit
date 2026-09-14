using MauiPersianToolkit.Core;

namespace MauiPersianToolkit.Controls;

/// <summary>
/// Full-window layout the platform popup hosts render. It paints the dimmed backdrop,
/// positions the popup according to its own layout options, and turns a tap on the
/// backdrop into a dismissal.
/// </summary>
/// <remarks>
/// Keeping the backdrop as a sibling of the popup rather than as the container's own
/// background means taps on the popup content never reach the dismiss handler.
/// </remarks>
internal sealed class PopupContainer : Grid
{
    private readonly Popup _popup;
    private readonly View _surface;

    public PopupContainer(Popup popup, PopupOptions options)
    {
        _popup = popup;

        IgnoreSafeArea = true;
        BackgroundColor = Colors.Transparent;
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Fill;

        var backdrop = new BoxView
        {
            Color = options.BackgroundColor,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill
        };
        backdrop.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(OnBackdropTapped) });

        _surface = BuildSurface(popup, options);
        PlaceSurface(popup.HorizontalOptions, popup.VerticalOptions);

        Children.Add(backdrop);
        Children.Add(_surface);

        // Dim the whole window, including the area behind a bottom sheet.
        if (RowDefinitions.Count > 1)
            SetRowSpan((BindableObject)backdrop, RowDefinitions.Count);
    }

    /// <summary>
    /// Anchors the surface with a real grid row for End/Start so bottom sheets sit flush
    /// against the window edge. Center/Fill keep a single full-window cell.
    /// </summary>
    private void PlaceSurface(LayoutOptions horizontal, LayoutOptions vertical)
    {
        _surface.HorizontalOptions = horizontal;

        switch (vertical.Alignment)
        {
            case LayoutAlignment.End:
                RowDefinitions = new RowDefinitionCollection
                {
                    new(GridLength.Star),
                    new(GridLength.Auto)
                };
                _surface.VerticalOptions = LayoutOptions.Fill;
                SetRow((BindableObject)_surface, 1);
                break;

            case LayoutAlignment.Start:
                RowDefinitions = new RowDefinitionCollection
                {
                    new(GridLength.Auto),
                    new(GridLength.Star)
                };
                _surface.VerticalOptions = LayoutOptions.Fill;
                SetRow((BindableObject)_surface, 0);
                break;

            default:
                // Center or Fill: one cell covering the window; the surface's own
                // VerticalOptions places it inside that cell.
                _surface.VerticalOptions = vertical;
                break;
        }
    }

    /// <summary>
    /// Wraps the popup in a <see cref="Border"/> only when the options actually ask for a
    /// shape or a shadow, so callers passing neither get the popup's own visuals untouched.
    /// </summary>
    private static View BuildSurface(Popup popup, PopupOptions options)
    {
        if (options.Shape is null && options.Shadow is null)
            return popup;

        return new Border
        {
            Content = popup,
            Padding = 0,
            StrokeThickness = 0,
            BackgroundColor = Colors.Transparent,
            StrokeShape = options.Shape,
            Shadow = options.Shadow
        };
    }

    private void OnBackdropTapped()
    {
        if (!_popup.ResolveDismissOnTappingOutside())
            return;

        _ = _popup.DismissAsync();
    }

    /// <summary>
    /// Removes the popup from this container so the same popup instance can be shown again.
    /// </summary>
    public void Release(Popup popup)
    {
        if (_surface is Border border)
            border.Content = null;
        else
            Children.Remove(popup);

        Parent = null;
    }
}
