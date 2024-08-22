using MauiPersianToolkit.Services.Dialog;

namespace PersianUISamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            try
            {
                MainPage = new MainPage(new ViewModels.MainViewModel(new DialogService()));
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