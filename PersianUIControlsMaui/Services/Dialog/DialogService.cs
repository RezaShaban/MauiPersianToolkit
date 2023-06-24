using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Dialogs;
using PersianUIControlsMaui.Models;

namespace PersianUIControlsMaui.Services.Dialog;

public class DialogService : IDialogService
{
    Page mainPage;

    private void SetMainPage()
    {
        if (mainPage is null)
            mainPage = Application.Current.MainPage;
    }

    public void Alert(string message, string title = "", MessageIcon icon = MessageIcon.ACCEPT, string acceptText = "باشه")
    {
        AlertConfig config = new AlertConfig()
        {
            Icon = icon,
            Title = title,
            Message = message,
            AcceptText = acceptText
        };
        Alert(config);
    }

    public void Alert(AlertConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            SetMainPage();
            var alertPage = new AlertPage(config);
            await mainPage.ShowPopupAsync(alertPage);
        });
    }

    public void ShowException(Exception ex)
    {
#if DEBUG
        Alert(new AlertConfig()
        {
            Message = ex.ToString(),
            AcceptIcon = MessageIcon.ACCEPT,
            Icon = MessageIcon.ERROR,
            Title = "خطای سیستمی",
            AcceptText = "باشه"
        });
#endif
    }

    public void Toast(ToastConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //await PopupNavigation.Instance.PushAsync(new ToastPage(config)
            //{
            //    BackgroundInputTransparent = true,
            //    BackgroundColor = Color.Transparent,
            //    CloseWhenBackgroundIsClicked = false
            //});
            //Device.StartTimer(new TimeSpan(0, 0, config.Duration), new Func<bool>(() =>
            //{
            //    if (PopupNavigation.Instance.PopupStack.Count > 0)
            //        PopupNavigation.Instance.PopAsync();
            //    return false;
            //}));
        });
    }

    public async void Confirm(ConfirmConfig config)
    {
        SetMainPage();
        var confirmPage = new ConfirmPage(config);
        await mainPage.ShowPopupAsync(confirmPage);
    }

    public void CustomDialog(CustomDialogConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //await PopupNavigation.Instance.PushAsync(new CustomDialogPage(config) { BackgroundColor = config.BackgrounColor, CloseWhenBackgroundIsClicked = config.CloseAfterAccept });
        });
    }

    public void Prompt(PromptConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            //await PopupNavigation.Instance.PushAsync(new PromptPage(config) { BackgroundColor = config.BackgrounColor, CloseWhenBackgroundIsClicked = config.CloseAfterAccept });
        });
    }
}
