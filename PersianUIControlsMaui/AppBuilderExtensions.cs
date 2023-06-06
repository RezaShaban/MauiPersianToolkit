using CommunityToolkit.Maui;

namespace PersianUIControlsMaui;

// All the code in this file is included in all platforms.
public static class AppBuilderExtensions
{
    public static MauiAppBuilder UsePersianUIControls(this MauiAppBuilder builder)
    {
        builder
            .ConfigureFonts(fonts =>
            {
                //fonts.AddEmbeddedResourceFont(typeof(AppBuilderExtensions).Assembly, "materialdesignicons-webfont.ttf", "MDI");
                fonts.AddEmbeddedResourceFont(typeof(AppBuilderExtensions).Assembly, "IranianSans.ttf", "IranianSans");
            });

        return builder;
    }
}