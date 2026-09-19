namespace MauiPersianToolkit;

/// <summary>
/// Resolves toolkit semantic colors for the current <see cref="AppTheme"/>.
/// Prefer <c>AppThemeBinding</c> in XAML; use this from C# builders and defaults.
/// </summary>
public static class ThemeColors
{
    private static readonly object CacheSync = new();
    private static AppTheme _cachedTheme = (AppTheme)(-1);
    private static readonly Dictionary<string, Color> Cache = new(StringComparer.Ordinal);

    public static bool IsDark =>
        Application.Current?.RequestedTheme == AppTheme.Dark;

    public static Color Page => Resolve(PersianThemeKeys.PageLight, PersianThemeKeys.PageDark, Color.FromArgb("#F5F5F5"), Color.FromArgb("#121212"));
    public static Color Surface => Resolve(PersianThemeKeys.SurfaceLight, PersianThemeKeys.SurfaceDark, Colors.White, Color.FromArgb("#1E1E1E"));
    public static Color InputFill => Resolve(PersianThemeKeys.InputFillLight, PersianThemeKeys.InputFillDark, Color.FromArgb("#FDFDFD"), Color.FromArgb("#2C2C2C"));
    public static Color Outline => Resolve(PersianThemeKeys.OutlineLight, PersianThemeKeys.OutlineDark, Color.FromArgb("#DCDCDC"), Color.FromArgb("#555555"));
    public static Color OnSurface => Resolve(PersianThemeKeys.OnSurfaceLight, PersianThemeKeys.OnSurfaceDark, Color.FromArgb("#121212"), Color.FromArgb("#F2F2F2"));
    public static Color Muted => Resolve(PersianThemeKeys.MutedLight, PersianThemeKeys.MutedDark, Color.FromArgb("#666666"), Color.FromArgb("#AAAAAA"));
    public static Color Footer => Resolve(PersianThemeKeys.FooterLight, PersianThemeKeys.FooterDark, Color.FromArgb("#F5F5F5"), Color.FromArgb("#2A2A2A"));
    public static Color Disabled => Resolve(PersianThemeKeys.DisabledLight, PersianThemeKeys.DisabledDark, Color.FromArgb("#999999"), Color.FromArgb("#777777"));
    public static Color DayButton => Resolve(PersianThemeKeys.DayButtonLight, PersianThemeKeys.DayButtonDark, Colors.White, Color.FromArgb("#2C2C2C"));

    public static Color Accept => Resolve(PersianThemeKeys.Accept, PersianThemeKeys.Accept, Color.FromArgb("#1F883D"), Color.FromArgb("#1F883D"));
    public static Color Cancel => Resolve(PersianThemeKeys.Cancel, PersianThemeKeys.Cancel, Color.FromArgb("#FF4500"), Color.FromArgb("#FF4500"));
    public static Color Accent => Resolve(PersianThemeKeys.Accent, PersianThemeKeys.Accent, Color.FromArgb("#5B2BDF"), Color.FromArgb("#5B2BDF"));

    public static Color AlertBackground => Resolve(PersianThemeKeys.AlertBgLight, PersianThemeKeys.AlertBgDark, Color.FromArgb("#CC323232"), Color.FromArgb("#E8E8E8"));
    public static Color AlertForeground => Resolve(PersianThemeKeys.AlertFgLight, PersianThemeKeys.AlertFgDark, Colors.White, Color.FromArgb("#121212"));

    /// <summary>Drops the resolved-color cache (theme option changes / host overrides).</summary>
    public static void InvalidateCache()
    {
        lock (CacheSync)
        {
            Cache.Clear();
            _cachedTheme = (AppTheme)(-1);
        }
    }

    private static Color Resolve(string lightKey, string darkKey, Color lightFallback, Color darkFallback)
    {
        PersianTheme.EnsureApplicationStyles();

        var theme = Application.Current?.RequestedTheme ?? AppTheme.Unspecified;
        var key = theme == AppTheme.Dark ? darkKey : lightKey;

        lock (CacheSync)
        {
            if (_cachedTheme != theme)
            {
                Cache.Clear();
                _cachedTheme = theme;
            }

            if (Cache.TryGetValue(key, out var cached))
                return cached;

            Color resolved;
            if (Application.Current?.Resources.TryGetValue(key, out var value) == true && value is Color color)
                resolved = color;
            else
                resolved = theme == AppTheme.Dark ? darkFallback : lightFallback;

            Cache[key] = resolved;
            return resolved;
        }
    }
}
