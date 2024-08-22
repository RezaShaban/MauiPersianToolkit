using MauiPersianToolkit.Models;

namespace MauiPersianToolkit.Services.Dialog;

public interface IDialogService
{
    void Alert(string message, string title = "", MessageIcon icon = MessageIcon.ACCEPT, string acceptText = "باشه");
    void Alert(AlertConfig config);
    void Confirm(ConfirmConfig config);
    void ShowException(Exception ex);
    void CustomDialog(CustomDialogConfig config);
    void Prompt(PromptConfig config);
    void Toast(ToastConfig config);
    void Snackbar(SnackbarConfig config); 
}
