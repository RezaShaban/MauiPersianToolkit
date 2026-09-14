using MauiPersianToolkit;
using MauiPersianToolkit.Services.Dialog;
using PersianUISamples.ViewModels;

namespace PersianUISamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Optional: also auto-runs on first toolkit control; applies Theme options early.
            PersianTheme.EnsureApplicationStyles();

            try
            {
                MainPage = new MainPage(new MainViewModel(new DialogService()));
            }
            catch (Exception ex)
            {
                MainPage = new ContentPage()
                {
                    Background = Colors.White,
                    Content = new Label() { Text = ex.ToString(), TextColor = Colors.Black },
                };
            }
        }
    }
}
