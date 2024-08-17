# Maui Persian Toolkit

[![NuGet](https://img.shields.io/nuget/v/PersianUIControlsMaui.svg)](https://www.nuget.org/packages/PersianUIControlsMaui/)
[![License](https://img.shields.io/github/license/RezaShaban/PersianUIControlsMaui)](LICENSE)
[![Build](https://github.com/RezaShaban/PersianUIControlsMaui/actions/workflows/build.yml/badge.svg)](https://github.com/RezaShaban/PersianUIControlsMaui/actions)

`MauiPersianTookit` is a comprehensive library for .NET MAUI that provides a variety of Persian language UI controls and components. This library is designed to help developers create modern, cross-platform applications with support for Persian language and right-to-left (RTL) layouts.

## Features

- **Persian DatePicker**: Single, Multiple, and Range selection modes.
- **TreeView**: None, Single, and Multiple selection modes.
- **TabView**: Customizable tab control with multiple tabs.
- **SlideButton**: Slideable button for interactive UI elements.
- **Picker**: Single and Multiple selection pickers.
- **Dialogs**: Alert, Confirm, Prompt, and Custom dialogs for user interactions.
- **Expander**: Expandable and collapsible container for content.
- **Entry & Editor**: Enhanced text entry controls with Persian language support.
- **Converters**: Various converters to simplify data binding.

## Installation

You can install the `PersianUIControlsMaui` package via NuGet Package Manager or .NET CLI:

### NuGet Package Manager

```bash
Install-Package PersianUIControlsMaui
```
### .NET CLI
```basb
dotnet add package PersianUIControlsMaui
```

# Getting Started

### Startup
```basb
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

After installing the package, you can start using the controls by adding the appropriate namespaces to your XAML or C# files.

### Example Usage in XAML
```basb
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:persian="clr-namespace:PersianUIControlsMaui.Controls;assembly=PersianUIControlsMaui"
             x:Class="YourAppNamespace.MainPage">

    <StackLayout>
        <!-- Persian DatePicker Single Selection -->
        <persian:DatePicker PlaceHolder="تاریخ شروع" SelectedPersianDate="{Binding PersianDate}" 
CalendarOption="{Binding CalendarOption}" DisplayFormat="yyyy/MM/dd" 
OnChangeDateCommand="{Binding OnChangeDateCommand}"/>

        <!-- TreeView with Multiple Selection -->
        <persianControls:TreeView x:Name="treeView"
                                  SelectionMode="Multiple" />

        <!-- TabView -->
        <persianControls:TabView x:Name="tabView">
            <persianControls:TabViewItem Title="Tab 1">
                <Label Text="Content for Tab 1" />
            </persianControls:TabViewItem>
            <persianControls:TabViewItem Title="Tab 2">
                <Label Text="Content for Tab 2" />
            </persianControls:TabViewItem>
        </persianControls:TabView>

        <!-- SlideButton -->
        <persianControls:SlideButton x:Name="slideButton"
                                     Text="Slide to Confirm" />

        <!-- Picker with Multiple Selection -->
        <persianControls:Picker x:Name="multiPicker"
                                SelectionMode="Multiple" />

        <!-- Expander -->
        <persianControls:Expander x:Name="expander"
                                  IsExpanded="False"
                                  Header="Click to expand">
            <Label Text="This is the expandable content" />
        </persianControls:Expander>

        <!-- Entry and Editor -->
        <persianControls:Entry Placeholder="Enter text here" />
        <persianControls:Editor Placeholder="Enter more detailed text here" />
    </StackLayout>
</ContentPage>
```
### Example Usage in C#
```basb
using PersianUIControlsMaui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Using Persian DatePicker with Single Selection
        PersianDatePicker singleDatePicker = new PersianDatePicker
        {
            SelectionMode = DatePickerSelectionMode.Single,
            Format = "yyyy/MM/dd"
        };

        // Using TreeView with Multiple Selection
        TreeView treeView = new TreeView
        {
            SelectionMode = SelectionMode.Multiple
        };

        // Configuring TabView
        TabView tabView = new TabView();
        tabView.Items.Add(new TabViewItem { Title = "Tab 1", Content = new Label { Text = "Content for Tab 1" } });
        tabView.Items.Add(new TabViewItem { Title = "Tab 2", Content = new Label { Text = "Content for Tab 2" } });

        // Using SlideButton
        SlideButton slideButton = new SlideButton
        {
            Text = "Slide to Confirm"
        };

        // Configuring Picker with Multiple Selection
        Picker multiPicker = new Picker
        {
            SelectionMode = PickerSelectionMode.Multiple
        };

        // Using Expander
        Expander expander = new Expander
        {
            IsExpanded = false,
            Header = "Click to expand",
            Content = new Label { Text = "This is the expandable content" }
        };

        // Adding controls to the layout
        var stackLayout = new StackLayout
        {
            Children = { singleDatePicker, treeView, tabView, slideButton, multiPicker, expander }
        };

        this.Content = stackLayout;
    }
}
```
## Dialogs

PersianUIControlsMaui includes several types of dialogs:

Alert Dialog: Simple message dialog.
Confirm Dialog: Dialog with confirmation options.
Prompt Dialog: Dialog to capture user input.
Custom Dialog: Fully customizable dialog to suit your needs.
```basb
// Show Alert Dialog
dialogService.Alert("This is an alert message.");

// Show Confirm Dialog
dialogService.Confirm(new ConfirmConfig()
{
    Title = "Remove Item",
    AcceptText = "Yes",
    CancelText = "No",
    Message = "Are you sure you want to proceed?",
    Icon = MessageIcon.QUESTION,
    OnAction = new Action<bool>((arg) => {
        if(!arg) return;
    }),
});

// Show Prompt Dialog
dialogService.Prompt(new PromptConfig()
{
    Title = "Regiser Name",
    AcceptText = "Register",
    CancelText = "Cancel",
    Message = "Enter your name:",
    Placeholder = "name",
    Icon = MessageIcon.QUESTION,
    OnAction = new Action<PromptResult>((arg) => {
        if(!arg.IsOk) return;
    }),
});

// Custom Dialog
dialogService.CustomDialog(new CustomDialogConfig()
{
    Title = "Register Information",
    AcceptText = "Register",
    CancelText = "Cancle",
    Message = "Enter Your Info",
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
            new EntryView(){ PlaceHolder = "Name" },
            new MauiPersianToolkit.Controls.DatePicker(){ PlaceHolder = "BirthDate" }
        }
    }
});
```

## Converters
This library also includes several converters to assist with data binding in your MAUI applications.

### Example Usage of Converters
```basb
<Label Text="{Binding Date, Converter={StaticResource PersianDateConverter}}" />
```
## Customization
All controls in PersianUIControlsMaui are designed to be easily customizable to match the look and feel of your application. You can adjust properties such as colors, fonts, and behaviors through XAML or C#.

## Contributing
We welcome contributions! If you have ideas, suggestions, or issues to report, please feel free to open an issue or submit a pull request.

### Steps to Contribute
Fork this repository.
Create a new branch (git checkout -b feature/NewFeature).
Commit your changes (git commit -m 'Add new feature').
Push to the branch (git push origin feature/NewFeature).
Open a Pull Request.

## License
This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments
Special thanks to the .NET MAUI community and all contributors for their support and contributions to this project.


Feel free to explore and use PersianUIControlsMaui in your MAUI projects. We are excited to see what you will create with these powerful Persian language UI controls!


### توضیحات درباره‌ی فایل `README.md`:
- **Features**: معرفی ویژگی‌های کلیدی کتابخانه و کنترل‌های موجود.
- **Installation**: دستورالعمل نصب از طریق NuGet و .NET CLI.
- **Getting Started**: مثال‌های کد برای نحوه استفاده از کنترل‌ها در XAML و C#.
- **Dialogs**: توضیح درباره‌ی دیالوگ‌های مختلف موجود و نحوه استفاده از آن‌ها.
- **Converters**: اشاره به وجود Converters برای کمک به Data Binding.
- **Customization**: اشاره به قابلیت سفارشی‌سازی کنترل‌ها.
- **Contributing**: دستورالعمل‌هایی برای مشارکت در توسعه‌ی پروژه.
- **License**: اطلاعات مربوط به مجوز پروژه.
- **Acknowledgments**: قدردانی از جامعه و مشارکت‌کنندگان.

این فایل به شما کمک می‌کند تا مستندات ریپازیتوری خود را به صورت کامل و دقیق برای کاربران و توسعه‌دهندگان دیگر ارائه دهید.

## Screenshots

![App Screenshot](https://raw.githubusercontent.com/RezaShaban/MauiPersianToolkit/master/PersianUISamples/date-picker-demo.png)