# Persian UI Controls Maui

Persian Calendar &amp; some other controls for .NET MAUI

To use this package in your MAUI project use the below code in your MauiProgram.cs file


## Controls

- Persian DatePicker ```Single, Multiple, Range```
- TreeView ```None, Single, Multiple```
- TabView
- SlideButton
- Picker ```Single, Multiple```
- Dialogs ```Alter, Confirm, Prompt, Custom```
- Expander
- Entry
- Editor


## Deployment

To deploy this project run

```bash
  public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    })
                .UseMauiCommunityToolkit()
                .UsePersianUIControls();
            return builder.Build();
        }
    }
```

XAML:

```bash
xmlns:persian="clr-namespace:PersianUIControlsMaui.Controls;assembly=PersianUIControlsMaui"

<persian:DatePicker PlaceHolder="تاریخ شروع" SelectedPersianDate="{Binding PersianDate}" 
CalendarOption="{Binding CalendarOption}" DisplayFormat="yyyy/MM/dd" 
OnChangeDateCommand="{Binding OnChangeDateCommand}"/>
```

CalendarOption:
```bash
CalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Single,
                SelectDayColor = Colors.Orange,
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                MinDateCanSelect = DateTime.Now.AddDays(-3),
                MaxDateCanSelect = DateTime.Now.AddDays(4),
                CanSelectHolidays = false
            };
```

Use Dialogs:
```bash
inject in constructor => IDialogService dialogService

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
```
## Screenshots

![App Screenshot](https://raw.githubusercontent.com/RezaShaban/PersianUIControlsMaui/master/PersianUISamples/date-picker-demo.png)