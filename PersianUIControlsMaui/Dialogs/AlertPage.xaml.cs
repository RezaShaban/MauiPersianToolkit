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
        SetDialogProperties();
    }

    private void SetDialogProperties()
    {
        double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        container.MaximumWidthRequest = width;
    }

    private async void btnAccept_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(((AlertConfig)this.BindingContext).Message);
        this.Close();
    }
}