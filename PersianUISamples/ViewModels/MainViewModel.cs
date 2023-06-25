using CommunityToolkit.Maui.Alerts;
using PersianUIControlsMaui.Models;
using PersianUIControlsMaui.Services.Dialog;
using PersianUIControlsMaui.ViewModels;

namespace PersianUISamples.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string persianDate;
        private CalendarOptions calendarOption;
        private Command onChangeDateCommand;
        private Command showAlertCommand;
        private Command showConfirmCommand;
        private Command showPromptCommand;
        private readonly IDialogService dialogService;

        public string PersianDate { get => persianDate; set => SetProperty(ref persianDate, value); }
        public CalendarOptions CalendarOption { get => calendarOption; set => SetProperty(ref calendarOption, value); }

        public Command OnChangeDateCommand { get { onChangeDateCommand ??= new Command(OnDateChanged); return onChangeDateCommand; } }
        public Command ShowAlertCommand { get { showAlertCommand ??= new Command(ShowAlert); return showAlertCommand; } }
        public Command ShowConfirmCommand { get { showConfirmCommand ??= new Command(ShowConfirm); return showConfirmCommand; } }
        public Command ShowPromptCommand { get { showPromptCommand ??= new Command(ShowPrompt); return showPromptCommand; } }

        public MainViewModel(IDialogService dialogService)
        {
            CalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Multiple,
                SelectDayColor = Colors.Orange,
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                //MinDateCanSelect = DateTime.Now.AddDays(-10),
                //MaxDateCanSelect = DateTime.Now.AddDays(10),
                CanSelectHolidays = true
            };
            this.dialogService = dialogService;
        }

        private void OnAcceptDate(object obj)
        {
            if (obj is not List<DayOfMonth> dates)
                return;

            this.PersianDate = dates.FirstOrDefault()?.PersianDate + " - " + dates.LastOrDefault()?.PersianDate;
        }

        private void OnDateChanged(object obj)
        {

        }

        private void ShowAlert(object obj)
        {
            dialogService.Alert("این یک متن برای نمایش به صورت هشدار هست", "هشدار");
        }

        private void ShowConfirm(object obj)
        {
            dialogService.Confirm(new ConfirmConfig()
            {
                Title = "حذف کالا",
                AcceptText = "آره",
                CancelText = "نه",
                Message = "نسبت به حذف آیتم انتخابی اطمینان دارید؟",
                Icon = MessageIcon.QUESTION,
                OnAction = new Action<bool>((arg) => { }),
            });
        }

        private void ShowPrompt(object obj)
        {
            dialogService.Prompt(new PromptConfig()
            {
                Title = "ثبت اطلاعات",
                AcceptText = "ثبت",
                CancelText = "انصراف",
                Message = $"اطلاعات خود را جهت بررسی در زیر وارد کنید",
                BackgrounColor = Colors.DeepPink,
                Placeholder = "اطلاعات",
                Icon = MessageIcon.QUESTION,
                OnAction = new Action<PromptResult>((arg) => { }),
            });
        }
    }
}
