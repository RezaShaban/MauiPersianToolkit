using CommunityToolkit.Maui;
using MauiPersianToolkit;
using PersianUISamples.ViewModels;

namespace PersianUISamples
{
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
                .UseMauiCommunityToolkit()
                .UsePersianUIControls()
                .Services.AddScoped<MainPage>().AddScoped<MainViewModel>();
            return builder.Build();
        }
    }
}