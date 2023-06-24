using PersianUISamples.ViewModels;

namespace PersianUISamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }
}