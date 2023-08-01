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
        container.MaximumWidthRequest = width;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        if (_config.OnAction != null)
            _config.OnAction.Invoke(false);

        this.Close();
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        if (_config.OnAction != null)
            _config.OnAction.Invoke(true);
        if (_config.CloseAfterAccept)
            this.Close();
    }
}