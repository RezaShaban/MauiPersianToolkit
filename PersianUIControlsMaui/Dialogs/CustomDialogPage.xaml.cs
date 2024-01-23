using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Models;

namespace PersianUIControlsMaui.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomDialogPage : Popup
{
    CustomDialogConfig _config;
    public CustomDialogPage(CustomDialogConfig config)
    {
        InitializeComponent();
        _config = config;
        BindingContext = _config;

        if (config.Message != null && config.Message.Length > 0 && config.Message.Contains("#"))
        {
            lblMessage.FormattedText = new FormattedString();
            foreach (var str in config.Message.Split('#'))
            {
                lblMessage.FormattedText.Spans.Add(new Span()
                {
                    Text = str.Replace("#", ""),
                    TextColor = config.Message.Contains('#' + str.Trim() + '#') ? Color.FromArgb("#ff7800") : Colors.Gray,
                    FontAttributes = FontAttributes.Bold,
                    FontFamily = "IranianSans"
                });
            }
        }
        SetDialogProperties();
    }

    private void SetDialogProperties()
    {
        double width = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        double height = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        container.MaximumWidthRequest = width;
        this.container.MaximumHeightRequest = (height * 0.8) - 50;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        _config.OnAction?.Invoke(false);
        this.Close();
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        _config.OnAction?.Invoke(true);
        if (_config.CloseAfterAccept)
            this.Close();
    }

    private void MeasureText()
    {
        locEditor.MaximumWidthRequest = container.MaximumWidthRequest;
        locEditor.Text = _config.Message;
        locEditor.FontFamily = "IranianSans";
        var locSize = locEditor.Handler.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);
        var content = _config.Content.Measure(double.PositiveInfinity, double.PositiveInfinity);
        scrollView.HeightRequest = locSize.Height + content.Minimum.Height + 65;
        container.Children.Remove(locEditor);
        grdBody.HeightRequest = locSize.Height + content.Minimum.Height + 190;
    }

    private void Popup_HandlerChanged(object sender, EventArgs e)
    {
        MeasureText();
    }
}