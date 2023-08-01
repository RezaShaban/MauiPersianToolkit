using PersianUIControlsMaui.Controls;
using PersianUIControlsMaui.Models;
using PersianUIControlsMaui.Services.Dialog;
using PersianUIControlsMaui.ViewModels;

namespace PersianUISamples.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string persianDate;
        private string persianDateRange;
        private string persianDateMultiple;
        private List<string> badgeDates;
        private List<string> multipleBadgeDates;
        private CalendarOptions calendarOption;
        private CalendarOptions rangeCalendarOption;
        private CalendarOptions multipleCalendarOption;
        private Command onChangeDateCommand;
        private Command showAlertCommand;
        private Command showConfirmCommand;
        private Command showPromptCommand;
        private Command showCustomCommand;
        private readonly IDialogService dialogService;

        public string PersianDate { get => persianDate; set => SetProperty(ref persianDate, value); }
        public string PersianDateRange { get => persianDateRange; set => SetProperty(ref persianDateRange, value); }
        public string PersianDateMultiple { get => persianDateMultiple; set => SetProperty(ref persianDateMultiple, value); }
        public List<string> BadgeDates { get => badgeDates; set => SetProperty(ref badgeDates, value); }
        public List<string> MultipleBadgeDates { get => multipleBadgeDates; set => SetProperty(ref multipleBadgeDates, value); }
        public CalendarOptions CalendarOption { get => calendarOption; set => SetProperty(ref calendarOption, value); }
        public CalendarOptions RangeCalendarOption { get => rangeCalendarOption; set => SetProperty(ref rangeCalendarOption, value); }
        public CalendarOptions MultipleCalendarOption { get => multipleCalendarOption; set => SetProperty(ref multipleCalendarOption, value); }

        public Command OnChangeDateCommand { get { onChangeDateCommand ??= new Command(OnDateChanged); return onChangeDateCommand; } }
        public Command ShowAlertCommand { get { showAlertCommand ??= new Command(ShowAlert); return showAlertCommand; } }
        public Command ShowConfirmCommand { get { showConfirmCommand ??= new Command(ShowConfirm); return showConfirmCommand; } }
        public Command ShowPromptCommand { get { showPromptCommand ??= new Command(ShowPrompt); return showPromptCommand; } }
        public Command ShowCustomCommand { get { showCustomCommand ??= new Command(ShowCustom); return showCustomCommand; } }

        public MainViewModel(IDialogService dialogService)
        {
            CalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Single,
                SelectDayColor = Colors.Orange,
                MinDateCanSelect = DateTime.Now.AddDays(-10),
                MaxDateCanSelect = DateTime.Now.AddDays(10),
            };
            RangeCalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Range,
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                CanSelectHolidays = true
            };
            MultipleCalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Multiple,
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDateMultiple,
                OnCancel = new Action(() => { }),
                CanSelectHolidays = true
            };
            this.dialogService = dialogService;
        }

        private void OnAcceptDate(object obj)
        {
            if (obj is not List<DayOfMonth> dates)
                return;
            BadgeDates = dates.Select(x => x.PersianDate).ToList();
            this.PersianDateRange = dates.FirstOrDefault()?.PersianDate;
        }

        private void OnAcceptDateMultiple(object obj)
        {
            if (obj is not List<DayOfMonth> dates)
                return;
            MultipleBadgeDates = dates.Select(x => x.PersianDate).ToList();
            this.PersianDateMultiple = dates.FirstOrDefault()?.PersianDate;
        }

        private void OnDateChanged(object obj)
        {

        }

        private void ShowAlert(object obj)
        {
            dialogService.Alert("لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.", "هشدار");
        }

        private void ShowConfirm(object obj)
        {
            dialogService.Confirm(new ConfirmConfig()
            {
                Title = "حذف کالا",
                AcceptText = "آره",
                CancelText = "نه",
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
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
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                Placeholder = "اطلاعات",
                Icon = MessageIcon.QUESTION,
                OnAction = new Action<PromptResult>((arg) => { }),
            });
        }

        private void ShowCustom(object obj)
        {
            dialogService.CustomDialog(new CustomDialogConfig()
            {
                Title = "ثبت اطلاعات",
                AcceptText = "ثبت",
                CancelText = "انصراف",
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. کتابهای زیادی در شصت و سه درصد گذشته، حال و آینده شناخت فراوان جامعه و متخصصان را می طلبد تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
                Icon = MessageIcon.QUESTION,
                AcceptIcon = MessageIcon.QUESTION,
                Cancelable = true,
                CancelIcon = MessageIcon.ERROR,
                DialogColor = Colors.DeepPink,
                CloseWhenBackgroundIsClicked = true,
                CloseAfterAccept = true,
                OnAction = new Action<bool>((arg) => { }),
                Content = new StackLayout()
                {
                    Children =
                    {
                        new EntryView(){ PlaceHolder = "نام" },
                        new PersianUIControlsMaui.Controls.DatePicker(){ PlaceHolder = "تاریخ تولد" }
                    }
                }
            });
        }
    }
}
