using CommunityToolkit.Maui;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

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
                fonts.AddEmbeddedResourceFont(typeof(AppBuilderExtensions).Assembly, "FontAwesome.ttf", "FontAwesome");
            });
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("CustomEditor", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Bottom = 0;
            Android.Graphics.Drawables.GradientDrawable gd = new Android.Graphics.Drawables.GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
            handler.PlatformView.SetBackgroundDrawable(gd);
#endif
        });
        // Remove Entry control underline
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CustomEntry", (handler, view) =>
        {
#if IOS || MACCATALYST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
        });
        return builder;
    }
}