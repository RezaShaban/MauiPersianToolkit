using CommunityToolkit.Maui;
using PersianUIControlsMaui;

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
                .UsePersianUIControls();
            return builder.Build();
        }
    }
}