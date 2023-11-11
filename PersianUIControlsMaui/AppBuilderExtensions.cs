using PersianUIControlsMaui.Services.Dialog;
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
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            handler.PlatformView.SetSelectAllOnFocus(true);            
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) => handler.PlatformView.SelectAll();
#endif
        });
        return builder;
    }
}