using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Models;

namespace PersianUIControlsMaui.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AlertPage : Popup
{
    public AlertPage(AlertConfig config)
    {
        InitializeComponent();
        BindingContext = config;
    }

    private async void btnAccept_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(((AlertConfig)this.BindingContext).Message);
        this.Close();
    }
}