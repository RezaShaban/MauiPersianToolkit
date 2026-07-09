using MauiPersianToolkit.Controls;
using MauiPersianToolkit.Models;
using MauiPersianToolkit.Services.Dialog;
using MauiPersianToolkit.ViewModels;
using System.Collections.ObjectModel;

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
        ObservableCollection<TreeViewModel> treeItems;
        ObservableCollection<TreeViewModel> selectedItemsTree;
        ObservableCollection<PickerItem> pickerMultipleItems;
        ObservableCollection<PickerItem> pickerItems;
        private Command onChangeDateCommand;
        private Command showAlertCommand;
        private Command showConfirmCommand;
        private Command showPromptCommand;
        private Command showCustomCommand;
        private Command registerInCommand;
        private readonly IDialogService dialogService;

        public string PersianDate { get => persianDate; set => SetProperty(ref persianDate, value); }
        public string PersianDateRange { get => persianDateRange; set => SetProperty(ref persianDateRange, value); }
        public string PersianDateMultiple { get => persianDateMultiple; set => SetProperty(ref persianDateMultiple, value); }
        public List<string> BadgeDates { get => badgeDates; set => SetProperty(ref badgeDates, value); }
        public List<string> MultipleBadgeDates { get => multipleBadgeDates; set => SetProperty(ref multipleBadgeDates, value); }
        public CalendarOptions CalendarOption { get => calendarOption; set => SetProperty(ref calendarOption, value); }
        public CalendarOptions RangeCalendarOption { get => rangeCalendarOption; set => SetProperty(ref rangeCalendarOption, value); }
        public CalendarOptions MultipleCalendarOption { get => multipleCalendarOption; set => SetProperty(ref multipleCalendarOption, value); }
        public ObservableCollection<PickerItem> PickerItems { get => pickerItems; set => SetProperty(ref pickerItems, value); }
        public ObservableCollection<TreeViewModel> TreeItems { get => treeItems; set => SetProperty(ref treeItems, value); }
        public ObservableCollection<TreeViewModel> SelectedItemsTree { get => selectedItemsTree; set => SetProperty(ref selectedItemsTree, value); }
        public ObservableCollection<PickerItem> PickerMultipleItems { get => pickerMultipleItems; set => SetProperty(ref pickerMultipleItems, value); }
        public ObservableCollection<PickerButton> PickerAdditionButtons { get; set; }
        public Command OnChangeDateCommand { get { onChangeDateCommand ??= new Command(OnDateChanged); return onChangeDateCommand; } }
        public Command ShowAlertCommand { get { showAlertCommand ??= new Command(ShowAlert); return showAlertCommand; } }
        public Command ShowConfirmCommand { get { showConfirmCommand ??= new Command(ShowConfirm); return showConfirmCommand; } }
        public Command ShowPromptCommand { get { showPromptCommand ??= new Command(ShowPrompt); return showPromptCommand; } }
        public Command ShowCustomCommand { get { showCustomCommand ??= new Command(ShowCustom); return showCustomCommand; } }
        public Command RegisterInCommand { get { registerInCommand ??= new Command(RegisterIn); return registerInCommand; } }
        public MainViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            InitData();
        }

        private void InitData()
        {
            SelectedItemsTree = new ObservableCollection<TreeViewModel>()
            {
                new TreeViewModel(){ Id = 1, Title = "سطح 1-1", ParentId = null },
                new TreeViewModel(){ Id = 8, Title = "سطح 4-1", ParentId = 6 }
            };
            CalendarOption = new CalendarOptions()
            {
                CalendarType = MauiPersianToolkit.Enums.CalendarType.Hijri,
                SelectDateMode = MauiPersianToolkit.Enums.SelectionDateMode.Day,
                SelectionMode = MauiPersianToolkit.Enums.SelectionMode.Single,
                SelectDayColor = Color.FromArgb("#5B2BDF"),
                MinDateCanSelect = DateTime.Now.AddDays(-10),
                MaxDateCanSelect = DateTime.Now.AddDays(10),
                AutoCloseAfterSelectDate = true,
            };
            RangeCalendarOption = new CalendarOptions()
            {
                CalendarType = MauiPersianToolkit.Enums.CalendarType.Persian,
                SelectDateMode = MauiPersianToolkit.Enums.SelectionDateMode.Day,
                SelectionMode = MauiPersianToolkit.Enums.SelectionMode.Range,
                SelectDayColor = Color.FromArgb("#5B2BDF"),
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                MinDateCanSelect = DateTime.Now.Date,
                InactiveDays =
                [
                    DateTime.Now.AddDays(2),
                    DateTime.Now.AddDays(5),
                    DateTime.Now.AddDays(7),
                    DateTime.Now.AddDays(8)
                ],
                CanSelectHolidays = true
            };
            MultipleCalendarOption = new CalendarOptions()
            {
                CalendarType = MauiPersianToolkit.Enums.CalendarType.Gregorian,
                SelectDateMode = MauiPersianToolkit.Enums.SelectionDateMode.Day,
                SelectionMode = MauiPersianToolkit.Enums.SelectionMode.Multiple,
                SelectDayColor = Color.FromArgb("#5B2BDF"),
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDateMultiple,
                OnCancel = new Action(() => { }),
                MinDateCanSelect = DateTime.Now.Date,
                InactiveDays =
                [
                    DateTime.Now.AddDays(2),
                    DateTime.Now.AddDays(5),
                    DateTime.Now.AddDays(7),
                    DateTime.Now.AddDays(8)
                ],
                CanSelectHolidays = true
            };
            PickerMultipleItems = new ObservableCollection<PickerItem>
            {
                new PickerItem() { Id = 1, Title = "انتخاب اول", Icon = "\uf027"},
                new PickerItem() { Id = 2, Title = "انتخاب دوم", Icon = "\uf037"},
                new PickerItem() { Id = 3, Title = "انتخاب سوم", Icon = "\uf047"},
                new PickerItem() { Id = 4, Title = "انتخاب چهارم", Icon = "\uf057"}
            };
            PickerItems = new ObservableCollection<PickerItem>
            {
                new PickerItem() { Id = 1, Title = "گزینه اول", Icon = "\uf027" },
                new PickerItem() { Id = 2, Title = "گزینه دوم", Icon = "\uf037" },
                new PickerItem() { Id = 3, Title = "گزینه سوم", Icon = "\uf047" }
            };
            PickerAdditionButtons = new ObservableCollection<PickerButton>() {
                new PickerButton() { Text = "\uf067" },
                new PickerButton() { Text = "\uf057" }
            };
            TreeItems = new ObservableCollection<TreeViewModel>()
            {
                new TreeViewModel(){ Id = 1, Title = "سطح 1-1", ParentId = null },
                new TreeViewModel(){ Id = 2, Title = "سطح 2-1", ParentId = 1 },
                new TreeViewModel(){ Id = 3, Title = "سطح 2-2", ParentId = 1 },
                new TreeViewModel(){ Id = 4, Title = "سطح 1-2", ParentId = null },
                new TreeViewModel(){ Id = 5, Title = "سطح 2-1", ParentId = 4 },
                new TreeViewModel(){ Id = 6, Title = "سطح 3-1", ParentId = 5 },
                new TreeViewModel(){ Id = 7, Title = "سطح 3-1", ParentId = 5 },
                new TreeViewModel(){ Id = 8, Title = "سطح 4-1", ParentId = 6 },
                new TreeViewModel(){ Id = 9, Title = "سطح 4-1", ParentId = 7 },
            };
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

        private void RegisterIn()
        {
            ShowAlert(null);
        }

        private void ShowAlert(object obj)
        {
            dialogService.Alert("لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد." +
                "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد." +
                "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.", "هشدار");
        }

        private void ShowConfirm(object obj)
        {
            dialogService.Confirm(new ConfirmConfig()
            {
                Title = "حذف کالا",
                AcceptText = "آره",
                CancelText = "نه",
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
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
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
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
                Message = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و تا با نرم افزارها شناخت بیشتری را برای طراحان رایانه ای علی الخصوص طراحان خلاقی و فرهنگ پیشرو در زبان فارسی ایجاد کرد. در این صورت می توان امید داشت که تمام و دشواری موجود در ارائه راهکارها و شرایط سخت تایپ به پایان رسد وزمان مورد نیاز شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار گیرد.",
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
                        new MauiPersianToolkit.Controls.DatePicker(){ PlaceHolder = "تاریخ تولد" }
                    }
                }
            });
        }
    }

    public class PickerItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; } = "\uf064";
        public int? ParentId { get; set; }
    }

    public class TreeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
    }
}
