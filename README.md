# Maui Persian Toolkit

[![NuGet](https://img.shields.io/nuget/v/MauiPersianToolkit.svg)](https://www.nuget.org/packages/MauiPersianToolkit/)
[![License](https://img.shields.io/github/license/RezaShaban/MauiPersianToolkit)](LICENSE)
[![Build](https://github.com/RezaShaban/MauiPersianToolkit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/RezaShaban/MauiPersianToolkit/actions)
[![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)](https://dotnet.microsoft.com/)

`MauiPersianToolkit` is a **.NET MAUI** UI toolkit for Persian / RTL apps: calendars, inputs, pickers, dialogs, and theming — with **native popups**, **light/dark themes**, and **built-in fa / en / ar** chrome strings. **No CommunityToolkit dependency.**

## Key features

### Calendar
- Persian (Jalali), Gregorian, and Hijri calendars
- Correct week starts (Sat→Fri for Persian/Hijri, Sun→Sat for Gregorian)
- `DatePicker` with Single / Multiple / Range via `CalendarOptions`
- Inline `DatePickerView` or popup field (popup content is created **lazily on first open**)
- Pluggable `ICalendarService` + unit tests

### Controls
| Area | Controls |
|------|----------|
| Inputs | `EntryView`, `EditorView`, `LabelView`, `CheckBoxView`, `ButtonView`, `ToggleButton` |
| Suggest | `AutoCompleteView` (+ native suggest on Android/Windows via `PersianAutoSuggest`) |
| Pickers | `PickerView` (single/multi), `DatePicker`, `TimePicker` (24h / 12h + seconds) |
| Layout | `TabView`, `ContainerView`, `Expander`, `SlideButton`, `CircleImageView` |
| Data | `TreeView` (virtualized rows, none/single/multi) |

### Dialogs & overlays (native)
- Alert / Confirm / Prompt / Custom — sync + `*Async` APIs on `IDialogService`
- Toast & Snackbar (Android, iOS, Windows)
- `Popup` host API — no third-party popup package

### Theme
- Follows `AppTheme` (system / light / dark)
- Semantic `Pdt*` tokens + `AppThemeBinding`
- Override via `UseMauiPersianToolkit`, `App.xaml`, or `PersianTheme.Configure`

### Localization
- Built-in catalogs: **fa**, **en**, **ar**
- `PersianToolkitStrings` / `PersianToolkitStringId` for chrome (Accept, Today, SelectTime, …)
- Extra languages via `AdditionalCatalogs`; XAML `{mpt:Localize Today}`

### Performance notes
- Date/time popup sheets are created on **first tap**, not on page `Loaded`
- Theme seeding uses a cached snapshot (fast path if already seeded)
- Tree rows avoid per-cell theme copies; gallery shadows deferred where possible

## Project stats

- **Version**: 3.0.0  
- **TFMs**: `net10.0`, Android, iOS, Windows  
- **Dependencies**: `Microsoft.Maui.Controls` only  
- **License**: MIT  
- **Sample**: `PersianUISamples` — categorized gallery with theme + language switcher  

## Installation

```powershell
Install-Package MauiPersianToolkit
```

```bash
dotnet add package MauiPersianToolkit
```

## Getting started

### 1. Register in `MauiProgram.cs`

```csharp
builder
    .UseMauiApp<App>()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
    })
    .UseMauiPersianToolkit(options =>
    {
        options.DefaultCalendarType = CalendarType.Persian;
        options.Localization.Culture = "fa"; // fa | en | ar

        // Optional theme overrides
        // options.Theme.Accent = Colors.Teal;
    });
```

Fonts (IranianSans, FontAwesome), handlers, dialog service, and theme defaults are registered for you.

### 2. Theme styles in `App.xaml` (recommended)

```xml
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:persian="clr-namespace:MauiPersianToolkit.Resources;assembly=MauiPersianToolkit"
             x:Class="YourApp.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <persian:PersianStyles/>
            </ResourceDictionary.MergedDictionaries>
            <!-- Optional overrides after merge -->
            <!-- <Color x:Key="PdtAccent">#2DD4BF</Color> -->
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

If you skip merging, `UseMauiPersianToolkit` still injects defaults when the app or first control starts. You can also call `PersianTheme.EnsureApplicationStyles()` early in `App`.

### 3. XAML namespace

```xml
xmlns:pui="clr-namespace:MauiPersianToolkit.Controls;assembly=MauiPersianToolkit"
xmlns:mpt="http://schemas.mauipersiantoolkit.com/2026/toolkit"
```

### 4. Quick control examples

**DatePicker** (calendar type & selection live on `CalendarOptions`):

```xml
<pui:DatePicker PlaceHolder="تاریخ"
                DisplayFormat="yyyy/MM/dd"
                Icon="&#xf073;"
                SelectedPersianDate="{Binding SelectedDate}"
                CalendarOption="{Binding CalendarOption}"/>
```

```csharp
public CalendarOptions CalendarOption { get; } = new()
{
    CalendarType = CalendarType.Persian,      // or Gregorian / Hijri
    SelectionMode = SelectionMode.Single,     // Single | Multiple | Range
    AutoCloseAfterSelectDate = true,
    SelectDayColor = Color.FromArgb("#5B2BDF"),
};
```

**TimePicker**:

```xml
<pui:TimePicker PlaceHolder="ساعت جلسه"
                SelectedTime="{Binding MeetingTime}"
                DisplayFormat="HH:mm"
                Is24Hour="True"
                MinuteInterval="5"/>
```

**AutoCompleteView**:

```xml
<pui:AutoCompleteView PlaceHolder="شهر"
                     ItemsSource="{Binding Cities}"
                     DisplayProperty="Title"
                     Text="{Binding CityText}"
                     SelectedItem="{Binding SelectedCity}"
                     Options="{Binding CityOptions}"/>
```

```csharp
public AutoCompleteOptions CityOptions { get; } = new()
{
    FilterMode = AutoCompleteFilterMode.Contains,
    MinimumPrefixLength = 1,
    MaxSuggestions = 8,
    UseNativeSuggestions = true,
    ShowClearButton = true,
    OpenOnFocus = true,
};
```

**Inputs / layout**:

```xml
<pui:EntryView PlaceHolder="نام" Icon="&#xf007;" Text="{Binding Name}"/>
<pui:EditorView Title="شرح" PlaceHolder="متن…" Text="{Binding Notes}"/>
<pui:PickerView PlaceHolder="انتخاب" ItemsSource="{Binding Items}"
                DisplayProperty="Title" SelectionMode="Single"/>
<pui:TabView SelectedTabColor="{StaticResource PdtAccent}">
    <pui:TabView.ItemsSource>
        <pui:TabItemView Title="اول" Icon="&#xf015;">…</pui:TabItemView>
        <pui:TabItemView Title="دوم" Icon="&#xf1b2;">…</pui:TabItemView>
    </pui:TabView.ItemsSource>
</pui:TabView>
<pui:Expander IsExpanded="False">
    <pui:Expander.Header>
        <Label Text="Header" FontFamily="IranianSans"/>
    </pui:Expander.Header>
    <Label Text="Body"/>
</pui:Expander>
```

## Calendar APIs

```csharp
var persian = CalendarServiceFactory.GetService(CalendarType.Persian);
string date = persian.ToCalendarDate(DateTime.Now);       // e.g. 1403/…
DateTime g = persian.ToGregorianDate("1403/05/25");

DateTime.Now.ToPersianDate();
DateTime.Now.ToCalendarDate(CalendarType.Hijri);
"1403/05/25".ToDateTime();
```

Custom calendars: implement `ICalendarService` and register with `CalendarServiceFactory.RegisterService(...)`.

## Dialogs (`IDialogService`)

Registered as a singleton by `UseMauiPersianToolkit`. Prefer async APIs when you need a result:

```csharp
await dialogs.AlertAsync("پیام", "عنوان");

bool ok = await dialogs.ConfirmAsync(new ConfirmConfig
{
    Title = "حذف",
    Message = "ادامه می‌دهید؟",
    Icon = MessageIcon.QUESTION,
});

PromptResult prompt = await dialogs.PromptAsync(new PromptConfig
{
    Title = "نام",
    Placeholder = "…",
});

dialogs.Toast(new ToastConfig { Message = "ذخیره شد" });
dialogs.Snackbar(new SnackbarConfig
{
    Message = "حذف شد",
    AcceptText = "بازگردانی",
    Duration = TimeSpan.FromSeconds(5),
});
```

## Toast / Snackbar (static helpers)

```csharp
await Toast.Make("Saved", ToastDuration.Short).Show();
await Snackbar.Make("Deleted", () => { /* undo */ }, "Undo", TimeSpan.FromSeconds(3)).Show();
```

## Theme tokens

| Key | Role |
|-----|------|
| `PdtPageLight` / `PdtPageDark` | Page background |
| `PdtSurfaceLight` / `PdtSurfaceDark` | Cards, dialogs, containers |
| `PdtInputFillLight` / `PdtInputFillDark` | Input fill |
| `PdtOutlineLight` / `PdtOutlineDark` | Borders |
| `PdtOnSurfaceLight` / `PdtOnSurfaceDark` | Primary text |
| `PdtMutedLight` / `PdtMutedDark` | Secondary text |
| `PdtFooterLight` / `PdtFooterDark` | Dialog footers |
| `PdtDisabledLight` / `PdtDisabledDark` | Disabled |
| `PdtDayButtonLight` / `PdtDayButtonDark` | Calendar day cells |
| `PdtAccept` / `PdtCancel` / `PdtAccent` | Actions |
| `PdtAlertBg*` / `PdtAlertFg*` | Toast / snackbar |

Constants: `PersianThemeKeys`. Defaults: `PersianStyles`.

```csharp
PersianTheme.Configure(theme =>
{
    theme.SurfaceDark = Color.FromArgb("#111827");
    theme.Accent = Colors.Orange;
});
```

## Localization

Built-in: **fa**, **en**, **ar**.

```csharp
builder.UseMauiPersianToolkit(options =>
{
    options.Localization.Culture = "ar";
    options.Localization.StringOverrides[PersianToolkitStringId.Ok] = "متوجه شدم";
});

// Runtime
PersianToolkitStrings.SetCulture("en");
```

| Key | fa | en | ar |
|-----|----|----|-----|
| `Accept` | ثبت | Save | حفظ |
| `Cancel` | انصراف | Cancel | إلغاء |
| `Confirm` | تایید | OK | تأكيد |
| `Ok` | باشه | OK | حسناً |
| `SelectDate` | انتخاب تاریخ | Select date | اختر التاريخ |
| `SelectTime` | انتخاب زمان | Select time | اختر الوقت |
| `Today` | امروز | Today | اليوم |
| `Now` | الان | Now | الآن |

XAML: `{mpt:Localize Today}`. After a runtime culture change, recreate or navigate back to the page so bindings refresh.

## Sample app (`PersianUISamples`)

Full gallery of every control, grouped by category:

- Shell flyout + home overview  
- Theme switcher: System / Light / Dark  
- Language switcher: فارسی / English / العربية  
- Each page shows real BindableProperty configuration and config hints  

Run:

```bash
dotnet build ./PersianUISamples/PersianUISamples.csproj -f net10.0-android
```

## Tests

```bash
dotnet test ./MauiPersianToolkit.Test/MauiPersianToolkit.Test.csproj
```

Coverage includes calendar conversion, week layout, Hijri display, localization, AutoComplete filtering, TimePicker, dialogs, expander, tree flatten, and popup options.

## Contributing

1. Fork → feature branch → PR  
2. Match existing style; add tests for new behavior  
3. Update this README when APIs or controls change  

## License

MIT — see [LICENSE](LICENSE).

## Links

- [NuGet](https://www.nuget.org/packages/MauiPersianToolkit/)  
- [Repository](https://github.com/RezaShaban/MauiPersianToolkit)  
- [Issues](https://github.com/RezaShaban/MauiPersianToolkit/issues)  
- [Homepage](https://rezashaban.github.io/MauiPersianToolkit)  

## Status

| Area | Status |
|------|--------|
| Core / .NET 10 | Stable |
| Calendars (fa / en / Hijri) | Stable |
| Inputs, pickers, tabs, tree | Stable |
| AutoComplete + TimePicker | Stable |
| Native popup / toast / snackbar | Stable |
| Theme (`Pdt*`) | Stable |
| Localization fa / en / ar | Stable |
| Sample gallery | Updated |
| Unit tests + CI | Active |
