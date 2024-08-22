using CommunityToolkit.Maui.Views;
using MauiPersianToolkit.Models;

namespace MauiPersianToolkit.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ConfirmPage : Popup
{
    ConfirmConfig config;
    public ConfirmPage(ConfirmConfig _config)
    {
        InitializeComponent();
        config = _config;
        SetDialogProperties();
        BindingContext = config;

    }

    private void SetDialogProperties()
    {
        double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        double height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        this.container.WidthRequest = width;
        scrollView.WidthRequest = width - 40;
        //lblMessage.WidthRequest = scrollView.WidthRequest;
        lblTitle.WidthRequest = width;
        this.container.MaximumHeightRequest = (height * 0.8) - 50;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        config.OnAction?.Invoke(false);
        this.Close();
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        config.OnAction?.Invoke(true);
        this.Close();
    }

    //private void MeasureText(string message)
    //{
    //    locEditor.MaximumWidthRequest = container.MaximumWidthRequest;
    //    locEditor.Text = message;
    //    locEditor.FontFamily = "IranianSans";
    //    var locSize = locEditor.Handler.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);

    //    scrollView.HeightRequest = locSize.Height+50;
    //    container.Children.Remove(locEditor);
    //    grdBody.HeightRequest = locSize.Height + 190;
    //}

    //private void Popup_HandlerChanged(object sender, EventArgs e)
    //{
    //    MeasureText(config.Message);
    //}
}