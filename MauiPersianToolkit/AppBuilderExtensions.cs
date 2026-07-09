using MauiPersianToolkit.Services.Dialog;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace MauiPersianToolkit;

// All the code in this file is included in all platforms.
public static class AppBuilderExtensions
{
    /// <summary>
    /// Configures the application to use Persian UI controls. This method is obsolete; use <see cref="UseMauiPersianToolkit"/>
    /// instead.
    /// </summary>
    /// <remarks>This method has been replaced by <see cref="UseMauiPersianToolkit"/>. It is recommended to update existing
    /// code to use the new method for future compatibility.</remarks>
    /// <param name="builder">The Maui application builder to configure with Persian UI controls.</param>
    /// <returns>The same MauiAppBuilder instance, configured to use Persian UI controls.</returns>
    [Obsolete("Use UseMauiPersianToolkit instead.")]
    public static MauiAppBuilder UsePersianUIControls(this MauiAppBuilder builder)
        => UseMauiPersianToolkit(builder);

    public static MauiAppBuilder UseMauiPersianToolkit(this MauiAppBuilder builder)
    {
        builder
            .ConfigureFonts(fonts =>
            {
                fonts.AddEmbeddedResourceFont(typeof(AppBuilderExtensions).Assembly, "IranianSans.ttf", "IranianSans");
                fonts.AddEmbeddedResourceFont(typeof(AppBuilderExtensions).Assembly, "FontAwesome.ttf", "FontAwesome");
            })
            .Services.AddSingleton<IDialogService, DialogService>();

        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("CustomEditor", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Bottom = 0;
            Android.Graphics.Drawables.GradientDrawable gd = new Android.Graphics.Drawables.GradientDrawable();
            gd.SetColor(global::Android.Graphics.Color.Transparent);
            handler.PlatformView.SetBackgroundDrawable(gd);

            // Either transparent or the provided background color
            var backgroundTint = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            handler.PlatformView.BackgroundTintList = backgroundTint;
#endif
        });
        // Config Entry control --underline, focus
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CustomEntry", (handler, view) =>
        {
#if IOS || MACCATALYST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            handler.PlatformView.EditingDidBegin += (s, e) => 
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
#elif ANDROID
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            handler.PlatformView.SetSelectAllOnFocus(true);            
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) => handler.PlatformView.SelectAll();
#endif
        });
        return builder;
    }
}