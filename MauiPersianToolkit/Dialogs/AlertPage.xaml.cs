using CommunityToolkit.Maui.Views;
using MauiPersianToolkit.Models;

namespace MauiPersianToolkit.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AlertPage : Popup
{
    public AlertPage(AlertConfig config)
    {
        InitializeComponent();
        SetDialogProperties();
        BindingContext = config;
    }

    private void SetDialogProperties()
    {
        double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        double height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        this.container.WidthRequest = width;
        scrollView.WidthRequest = width - 40;
        lblMessage.WidthRequest = scrollView.WidthRequest;
        lblTitle.WidthRequest = width;
        //this.container.MaximumHeightRequest = (height * 0.8) - 50;
    }

    private async void btnAccept_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }

    private void MeasureText(string message)
    {
        //locEditor.MaximumWidthRequest = container.MaximumWidthRequest;
        //locEditor.Text = message;
        //locEditor.FontFamily = "IranianSans";
        //var locSize = locEditor.Handler.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);

        //scrollView.HeightRequest = locSize.Height;
        //container.Children.Remove(locEditor);
        //grdBody.HeightRequest = locSize.Height + 140;
    }

    private void Popup_HandlerChanged(object sender, EventArgs e)
    {
        //MeasureText(((AlertConfig)BindingContext).Message);
    }
}