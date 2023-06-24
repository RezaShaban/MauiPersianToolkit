using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Models;

namespace PersianUIControlsMaui.Dialogs;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PromptPage : Popup
{
    PromptConfig _config;
    public PromptPage(PromptConfig config)
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
                });
            }
        }
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        if (_config.OnAction != null)
            _config.OnAction.Invoke(new PromptResult() { IsOk = false, Value = _config.DefaultValue });

        this.Close();
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        if (_config.OnAction != null)
            _config.OnAction.Invoke(new PromptResult() { IsOk = true, Value = _config.DefaultValue });
        if (_config.CloseAfterAccept)
            this.Close();
    }
}