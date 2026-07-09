using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using MauiPersianToolkit.Dialogs;
using MauiPersianToolkit.Models;

namespace MauiPersianToolkit.Services.Dialog;

public class DialogService : IDialogService
{
    Page mainPage;

    private void SetMainPage()
    {
        mainPage ??= Application.Current.MainPage;
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
            var alertPage = new AlertPage(config) { CanBeDismissedByTappingOutsideOfPopup = config.CloseWhenBackgroundIsClicked };
            await mainPage.ShowPopupAsync(alertPage, new PopupOptions
            {
                Shape = null,
                Shadow = null
            });
        });
    }

    public void ShowException(Exception ex)
    {
        Alert(new AlertConfig()
        {
            Message = ex.ToString(),
            AcceptIcon = MessageIcon.ACCEPT,
            Icon = MessageIcon.ERROR,
            Title = "خطای سیستمی",
            AcceptText = "باشه"
        });
    }

    public void Toast(ToastConfig config)
    {
        CommunityToolkit.Maui.Alerts.Toast.Make(config.Message, config.Duration);
    }



    public async void Confirm(ConfirmConfig config)
    {
        SetMainPage();
        var confirmPage = new ConfirmPage(config) { CanBeDismissedByTappingOutsideOfPopup = config.CloseWhenBackgroundIsClicked };
        await mainPage.ShowPopupAsync(confirmPage, new PopupOptions
        {
            Shape = null,
            Shadow = null
        });
    }

    public void CustomDialog(CustomDialogConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            SetMainPage();
            var customPage = new CustomDialogPage(config) { CanBeDismissedByTappingOutsideOfPopup = config.CloseWhenBackgroundIsClicked };
            await mainPage.ShowPopupAsync(customPage, new PopupOptions
            {
                Shape = null,
                Shadow = null
            });
        });
    }

    public void Prompt(PromptConfig config)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            SetMainPage();
            var confirmPage = new PromptPage(config) { CanBeDismissedByTappingOutsideOfPopup = config.CloseAfterAccept };
            await mainPage.ShowPopupAsync(confirmPage, new PopupOptions { Shape = null, Shadow = null });
        });
    }

    public void Snackbar(SnackbarConfig config)
    {
        CommunityToolkit.Maui.Alerts.Snackbar.Make(config.Message, config.OnAction, config.AcceptText, config.Duration,
            new CommunityToolkit.Maui.Core.SnackbarOptions()
            {
                CornerRadius = 7
            });
    }
}
