using MauiPersianToolkit;
using PersianUISamples.ViewModels;

namespace PersianUISamples;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .UseMauiPersianToolkit(options =>
            {
                //options.Localization.Culture = "en";
                
                // Optional: override toolkit light/dark colors for the whole app.
                // options.Theme.SurfaceDark = Color.FromArgb("#0D1117");
                // options.Theme.Accent = Colors.Teal;
                // options.Theme.Accept = Color.FromArgb("#2EA043");
            })
            .Services.AddScoped<MainPage>().AddScoped<MainViewModel>();
        return builder.Build();
    }
}
