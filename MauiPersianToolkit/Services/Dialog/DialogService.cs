using MauiPersianToolkit.Alerts;
using MauiPersianToolkit.Core;
using MauiPersianToolkit.Dialogs;
using MauiPersianToolkit.Extensions;
using MauiPersianToolkit.Hosting;
using MauiPersianToolkit.Localization;
using MauiPersianToolkit.Models;

namespace MauiPersianToolkit.Services.Dialog;

public class DialogService : IDialogService
{
    /// <summary>
    /// Options shared by every dialog: the dialog pages draw their own card, so the popup
    /// surface itself must stay unshaped and unshadowed.
    /// </summary>
    private static PopupOptions DialogOptions(bool canBeDismissedByTappingOutside) => new()
    {
        Shape = null,
        Shadow = null,
        CanBeDismissedByTappingOutsideOfPopup = canBeDismissedByTappingOutside
    };

    /// <summary>
    /// Page dialogs are presented over. Resolved lazily because the window is not available
    /// while the service is being constructed.
    /// </summary>
    private static Page CurrentPage =>
        Application.Current?.Windows.FirstOrDefault()?.Page
        ?? throw new InvalidOperationException("The application has no page to show a dialog over.");

    public void Alert(string message, string title = "", MessageIcon icon = MessageIcon.ACCEPT, string? acceptText = null)
    {
        AlertConfig config = new()
        {
            Icon = icon,
            Title = title,
            Message = message,
            AcceptText = acceptText ?? PersianToolkitOptions.Current.ResolveConfirmText()
        };
        Alert(config);
    }

    public void Alert(AlertConfig config) =>
        Present(() => new AlertPage(config), config.CloseWhenBackgroundIsClicked);

    public void ShowException(Exception ex)
    {
        Alert(new AlertConfig()
        {
            Message = ex.ToString(),
            AcceptIcon = MessageIcon.ACCEPT,
            Icon = MessageIcon.ERROR,
            Title = PersianToolkitStrings.SystemErrorTitle,
            AcceptText = PersianToolkitOptions.Current.ResolveConfirmText()
        });
    }

    public void Confirm(ConfirmConfig config) =>
        Present(() => new ConfirmPage(config), config.CloseWhenBackgroundIsClicked);

    public void CustomDialog(CustomDialogConfig config) =>
        Present(() => new CustomDialogPage(config), config.CloseWhenBackgroundIsClicked);

    public void Prompt(PromptConfig config) =>
        Present(() => new PromptPage(config), config.CloseAfterAccept);

    public void Toast(ToastConfig config) =>
        MainThread.BeginInvokeOnMainThread(() =>
            _ = Alerts.Toast.Make(config.Message, config.Duration).Show());

    public void Snackbar(SnackbarConfig config) =>
        MainThread.BeginInvokeOnMainThread(() =>
            _ = Alerts.Snackbar.Make(
                config.Message,
                config.OnAction,
                config.AcceptText,
                config.Duration,
                new SnackbarOptions { CornerRadius = new CornerRadius(7) }).Show());

    /// <summary>
    /// Builds and shows a dialog on the UI thread. The popup is constructed inside the
    /// callback because its XAML touches display metrics that must be read there.
    /// </summary>
    private static void Present(Func<Controls.Popup> factory, bool canBeDismissedByTappingOutside) =>
        MainThread.BeginInvokeOnMainThread(() =>
            _ = CurrentPage.ShowPopupAsync(factory(), DialogOptions(canBeDismissedByTappingOutside)));
}
