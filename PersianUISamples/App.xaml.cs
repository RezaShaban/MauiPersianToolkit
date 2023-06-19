namespace PersianUISamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            try
            {
                MainPage = new MainPage();
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