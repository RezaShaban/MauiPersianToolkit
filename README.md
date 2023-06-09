# Persian UI Controls Maui

Persian Calendar &amp; some other controls for .NET MAUI

To use this package in your MAUI project use the below code in your MauiProgram.cs file




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
## Screenshots

![App Screenshot](https://raw.githubusercontent.com/RezaShaban/DesignPatterns/67627cea2105fdbf301fcd722bc73444535ef42f/DesignPatternTutorial/Screenshot%202023-06-05%20230750.png)
