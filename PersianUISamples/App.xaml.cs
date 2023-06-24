namespace PersianUISamples
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            try
            {
                MainPage = mainPage;
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