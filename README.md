# Maui Persian Toolkit

[![NuGet](https://img.shields.io/nuget/v/MauiPersianToolkit.svg)](https://www.nuget.org/packages/MauiPersianToolkit/)
[![License](https://img.shields.io/github/license/RezaShaban/MauiPersianToolkit)](LICENSE)
[![Build](https://github.com/RezaShaban/MauiPersianToolkit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/RezaShaban/MauiPersianToolkit/actions)
[![Tests](https://img.shields.io/badge/tests-20%2B%20passing-brightgreen)](https://github.com/RezaShaban/MauiPersianToolkit/actions)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/)

`MauiPersianToolkit` is a comprehensive library for **.NET MAUI** that provides a variety of Persian language UI controls and components with full support for multiple calendar systems. This library is designed to help developers create modern, cross-platform applications with support for Persian language and right-to-left (RTL) layouts.

## ✨ Key Features

### 📅 Advanced Calendar System
- **Multiple Calendar Support**: Persian (Jalali), Gregorian, and Islamic (Hijri) calendars
- **Flexible DatePicker**: Single, Multiple, and Range selection modes
- **Calendar Service Architecture**: Strategy pattern for extensible calendar implementations
- **Full Calendar Validation**: Automatic week alignment and day-of-week positioning
- **20+ Unit Tests**: Comprehensive test coverage for calendar functionality

### 🎨 UI Controls
- **Persian DatePicker**: Customizable date picker with multiple selection modes
- **TreeView**: Supports None, Single, and Multiple selection modes with hierarchy support
- **TabView**: Customizable tab control with multiple tabs and dynamic content
- **SlideButton**: Interactive slideable button for confirmation actions
- **Picker**: Single and Multiple selection pickers with enhanced UI
- **Entry & Editor**: Enhanced text entry controls with Persian language and RTL support
- **Expander**: Expandable and collapsible container for dynamic content
- **CheckBox, Button, Circle Image**: Custom-styled UI components

### 💬 Dialog System
- **Alert Dialog**: Simple message dialog
- **Confirm Dialog**: Dialog with confirmation options
- **Prompt Dialog**: Dialog to capture user input
- **Custom Dialog**: Fully customizable dialog with any content

### 🛠️ Developer Tools
- **Converters**: PersianDateConverter, PersianDateTimeConverter, and more
- **Extensions**: Calendar extensions for easy date manipulation
- **Custom Fonts**: Built-in support for IranianSans and FontAwesome fonts
- **RTL Support**: Full right-to-left layout support for all controls

## 📋 Project Statistics

- **Version**: 2.0.7
- **Target Framework**: .NET 8.0+
- **Platforms**: Windows, iOS, Android
- **License**: MIT
- **Tests**: 20+ unit tests (Calendar Service + Week Layout)
- **Code Quality**: CI/CD with automated testing on every PR

## 🚀 Installation

You can install the `MauiPersianToolkit` package via NuGet Package Manager or .NET CLI:

### NuGet Package Manager
```powershell
Install-Package MauiPersianToolkit
```

### .NET CLI
```bash
dotnet add package MauiPersianToolkit
```

## 🎯 Getting Started

### 1. Startup Configuration

Add the Persian Toolkit to your MauiApp in `MauiProgram.cs`:

```csharp
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
                fonts.AddFont("IranianSans.ttf", "IranianSans");
            })
            .UseMauiCommunityToolkit()
            .UsePersianUIControls();  // Add this line
        
        return builder.Build();
    }
}
```

### 2. Basic XAML Usage

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:persian="clr-namespace:MauiPersianToolkit.Controls;assembly=MauiPersianToolkit"
             x:Class="YourApp.MainPage"
             Title="Persian Toolkit Demo">

    <ScrollView>
        <StackLayout Padding="20" Spacing="15">
            
            <!-- Persian DatePicker - Single Selection -->
            <Label Text="Persian DatePicker (Single)" FontSize="16" FontAttributes="Bold"/>
            <persian:DatePicker 
                PlaceHolder="Select Date" 
                SelectedPersianDate="{Binding SelectedDate}"
                CalendarType="Persian"
                DisplayFormat="yyyy/MM/dd" />
            
            <!-- Gregorian DatePicker -->
            <Label Text="Gregorian DatePicker" FontSize="16" FontAttributes="Bold"/>
            <persian:DatePicker 
                PlaceHolder="Select Date" 
                SelectedPersianDate="{Binding SelectedGregorianDate}"
                CalendarType="Gregorian"
                DisplayFormat="yyyy/MM/dd" />
            
            <!-- Islamic (Hijri) DatePicker -->
            <Label Text="Islamic DatePicker" FontSize="16" FontAttributes="Bold"/>
            <persian:DatePicker 
                PlaceHolder="Select Date" 
                SelectedPersianDate="{Binding SelectedHijriDate}"
                CalendarType="Hijri"
                DisplayFormat="yyyy/MM/dd" />

            <!-- Entry Control -->
            <Label Text="Entry Field" FontSize="16" FontAttributes="Bold"/>
            <persian:EntryView 
                PlaceHolder="Enter your name" 
                Text="{Binding UserName}" />

            <!-- Expander Control -->
            <Label Text="Expandable Section" FontSize="16" FontAttributes="Bold"/>
            <persian:Expander IsExpanded="False" Header="Click to expand">
                <Label Text="This content is hidden until expanded" Padding="10"/>
            </persian:Expander>

        </StackLayout>
    </ScrollView>
</ContentPage>
```

### 3. Calendar System Usage

#### Using Calendar Services Directly

```csharp
using MauiPersianToolkit.Services.Calendar;
using MauiPersianToolkit.Enums;

// Get calendar service
var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
var hijriService = CalendarServiceFactory.GetService(CalendarType.Hijri);

// Convert dates
var today = DateTime.Now;
string persianDate = persianService.ToCalendarDate(today);      // "1403/05/25"
string gregorianDate = gregorianService.ToCalendarDate(today);  // "2024/08/16"
string hijriDate = hijriService.ToCalendarDate(today);          // "1446/02/21"

// Parse dates back
var parsed = persianService.ToGregorianDate("1403/05/25");

// Get calendar information
int year = persianService.GetYear(today);
int month = persianService.GetMonth(today);
string monthName = persianService.GetMonthName(month);
bool isLeap = persianService.IsLeapYear(year);
DayOfWeek holiday = persianService.GetLastDayOfWeek();  // Friday for Persian
```

#### Using Extension Methods (Backward Compatible)

```csharp
using MauiPersianToolkit;

DateTime today = DateTime.Now;

// Convert to Persian (default)
string persianDate = today.ToPersianDate();  // "1403/05/25"

// Convert to other calendars
string gregorianDate = today.ToCalendarDate(CalendarType.Gregorian);
string hijriDate = today.ToCalendarDate(CalendarType.Hijri);

// Parse back
var parsed = "1403/05/25".ToDateTime();
var gregorianParsed = "2024/08/16".ToDateTime(CalendarType.Gregorian);
```

### 4. Dialog Usage

```csharp
using MauiPersianToolkit.Services.Dialog;
using MauiPersianToolkit.Models;

public partial class MainPage : ContentPage
{
    private readonly IDialogService _dialogService;
    
    public MainPage(IDialogService dialogService)
    {
        InitializeComponent();
        _dialogService = dialogService;
    }

    // Alert Dialog
    private async void ShowAlert()
    {
        await _dialogService.Alert("This is an alert message");
    }

    // Confirm Dialog
    private async void ShowConfirm()
    {
        var config = new ConfirmConfig
        {
            Title = "Confirm Action",
            Message = "Are you sure?",
            AcceptText = "Yes",
            CancelText = "No",
            OnAction = (result) =>
            {
                if (result)
                    Debug.WriteLine("User confirmed");
                else
                    Debug.WriteLine("User cancelled");
            }
        };
        
        await _dialogService.Confirm(config);
    }

    // Prompt Dialog
    private async void ShowPrompt()
    {
        var config = new PromptConfig
        {
            Title = "Enter Name",
            Message = "Please enter your name:",
            Placeholder = "Name",
            AcceptText = "OK",
            CancelText = "Cancel",
            OnAction = (result) =>
            {
                if (result.IsOk)
                    Debug.WriteLine($"User entered: {result.Input}");
            }
        };
        
        await _dialogService.Prompt(config);
    }

    // Custom Dialog
    private async void ShowCustomDialog()
    {
        var customContent = new StackLayout
        {
            Children =
            {
                new EntryView { PlaceHolder = "Name" },
                new EntryView { PlaceHolder = "Email" },
                new persian:DatePicker { PlaceHolder = "Birth Date" }
            }
        };

        var config = new CustomDialogConfig
        {
            Title = "Register",
            Message = "Enter your information",
            Content = customContent,
            AcceptText = "Register",
            CancelText = "Cancel",
            OnAction = (result) =>
            {
                Debug.WriteLine($"Dialog result: {result}");
            }
        };
        
        await _dialogService.CustomDialog(config);
    }
}
```

## 📚 Examples and Documentation

### Calendar Service Examples
See `MauiPersianToolkit/Examples/CalendarServiceExamples.cs` for:
- Extension methods usage
- Calendar service factory usage
- Calendar conversions
- DatePicker control usage
- Custom calendar registration

### Calendar Tests
The project includes comprehensive unit tests:

**CalendarServiceTests** - 7 tests covering:
- Date conversion roundtrip
- Month boundaries
- Holiday detection
- Month names
- Leap year validation
- DatePickerViewModel integration
- Date formatting

**CalendarWeekLayoutTests** - 13+ tests covering:
- Consecutive days alignment
- Week structure (7 columns per week)
- Persian calendar day positioning
- Gregorian calendar day positioning
- Empty cells before first day
- End of month positioning
- Multiple month validation
- Holiday day placement
- Specific date column placement

Run tests:
```bash
dotnet test ./MauiPersianToolkit.Test/MauiPersianToolkit.Test.csproj
```

## 🔧 Customization

All controls are designed to be easily customizable:

### Custom Calendar Type

Implement `ICalendarService` for custom calendar support:

```csharp
public class MyCustomCalendarService : ICalendarService
{
    // Implement interface methods
    public string ToCalendarDate(DateTime gregorianDate) { /* ... */ }
    public DateTime ToGregorianDate(string calendarDate) { /* ... */ }
    // ... other methods
}

// Register with factory
CalendarServiceFactory.RegisterService(
    CalendarType.Custom, 
    new MyCustomCalendarService()
);
```

### Style Customization

Customize colors, fonts, and behaviors:

```xml
<persian:DatePicker
    PlaceHolder="Select Date"
    SelectDayColor="Blue"
    CanSelectHolidays="False"
    DisplayFormat="yyyy/MM/dd" />
```

## 🧪 Testing

The project includes a comprehensive test suite:

```
MauiPersianToolkit.Test/
├── CalendarServiceTests.cs (7 tests)
├── CalendarWeekLayoutTests.cs (13+ tests)
└── CalendarWeekLayoutTests.README.md (documentation)
```

### GitHub Actions CI/CD

Automated testing on every commit:
- ✅ Build validation
- ✅ Unit tests execution
- ✅ Calendar-specific tests
- ✅ Code coverage reports

See `.github/workflows/` for CI/CD configuration.

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 🤝 Contributing

We welcome contributions! Follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/YourFeature`)
3. **Commit** your changes (`git commit -m 'Add YourFeature'`)
4. **Push** to the branch (`git push origin feature/YourFeature`)
5. **Open** a Pull Request

### Contribution Guidelines

- Follow existing code style and conventions
- Add unit tests for new features
- Update documentation as needed
- Ensure all tests pass locally
- Provide clear PR description

## 🙏 Acknowledgments

- .NET MAUI Community for excellent framework and tools
- CommunityToolkit.Maui for reusable components
- All contributors and users for feedback and suggestions

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/RezaShaban/MauiPersianToolkit/issues)
- **Discussions**: [GitHub Discussions](https://github.com/RezaShaban/MauiPersianToolkit/discussions)
- **Website**: [Project Homepage](https://rezashaban.github.io/MauiPersianToolkit)

## 🔗 Quick Links

- [NuGet Package](https://www.nuget.org/packages/MauiPersianToolkit/)
- [GitHub Repository](https://github.com/RezaShaban/MauiPersianToolkit)
- [Documentation](https://github.com/RezaShaban/MauiPersianToolkit/wiki)
- [Issues & Bugs](https://github.com/RezaShaban/MauiPersianToolkit/issues)

## 📊 Project Status

| Component | Status | Coverage |
|-----------|--------|----------|
| Core Library | ✅ Stable | Mature |
| Calendar System | ✅ Stable | 3 calendars |
| UI Controls | ✅ Stable | 10+ controls |
| Dialog System | ✅ Stable | 4 types |
| Unit Tests | ✅ Complete | 20+ tests |
| Documentation | ✅ Complete | Full |

---

**Happy Coding! 🚀**

Start building beautiful Persian-enabled applications with MauiPersianToolkit today!