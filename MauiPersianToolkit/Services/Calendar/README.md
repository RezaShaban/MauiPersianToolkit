# Calendar System Documentation

## Overview

The calendar system uses the **Strategy Pattern** to support multiple calendar types (Persian, Gregorian, and Hijri) in a flexible and extensible manner.

## Architecture

### Core Components

#### 1. **ICalendarService Interface**
The main contract for calendar operations.

```csharp
public interface ICalendarService
{
    // Conversion methods
    string ToCalendarDate(DateTime gregorianDate);
    DateTime ToGregorianDate(string calendarDate);
    
    // Date component methods
    int GetYear(DateTime date);
    int GetMonth(DateTime date);
    int GetDayOfMonth(DateTime date);
    DayOfWeek GetDayOfWeek(DateTime date);
    
    // Month and year operations
    string GetMonthBeginning(DateTime date);
    string GetMonthEnding(DateTime date);
    IEnumerable<string> GetAllMonthNames();
    string GetMonthName(int monthNumber);
    
    // Holiday and week operations
    DayOfWeek GetHolidayDayOfWeek();
    string GetDayOfWeekName(DayOfWeek dayOfWeek);
    
    // Utility methods
    bool IsLeapYear(int year);
    int GetDaysInMonth(int year, int month);
    int GetMonthsInYear();
}
```

#### 2. **Calendar Service Implementations**

- **PersianCalendarService**: Persian (Jalali) calendar
- **GregorianCalendarService**: Gregorian calendar
- **HijriCalendarService**: Islamic (Hijri) calendar

Each implementation provides:
- Date conversion to/from Gregorian
- Month and year calculations
- Holiday detection (Friday for Persian/Hijri, Sunday for Gregorian)
- Month and day names

#### 3. **CalendarServiceFactory**
Factory pattern implementation for creating and managing calendar services.

```csharp
// Get a calendar service
var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);

// Register custom calendar service
CalendarServiceFactory.RegisterService(CalendarType.Persian, customService);
```

## Usage Examples

### Basic Usage

#### Using Extension Methods (Backward Compatible)
```csharp
// Using extension methods (default: Persian calendar)
DateTime now = DateTime.Now;
string persianDate = now.ToPersianDate(); // "1403/05/15"
DateTime parsedDate = "1403/05/15".ToDateTime();
```

#### Using Calendar Service Directly
```csharp
// For Persian calendar
var persianService = CalendarServiceFactory.GetService(CalendarType.Persian);
string persianDate = persianService.ToCalendarDate(DateTime.Now);
int year = persianService.GetYear(DateTime.Now);
string monthName = persianService.GetMonthName(5);

// For Gregorian calendar
var gregorianService = CalendarServiceFactory.GetService(CalendarType.Gregorian);
string gregorianDate = gregorianService.ToCalendarDate(DateTime.Now);

// For Hijri calendar
var hijriService = CalendarServiceFactory.GetService(CalendarType.Hijri);
string hijriDate = hijriService.ToCalendarDate(DateTime.Now);
```

### DatePicker Control

The DatePicker control automatically selects the correct calendar service based on `CalendarOptions.CalendarType`.

```xml
<persian:DatePicker 
    SelectedPersianDate="{Binding SelectedDate}"
    PlaceHolder="Select Date"
    CalendarType="Persian" />
```

```csharp
var options = new CalendarOptions 
{
    CalendarType = CalendarType.Persian,
    SelectionMode = SelectionMode.Single
};
```

### Working with Different Calendar Types

```csharp
// Convert date to different calendar types
DateTime gregorianDate = DateTime.Now;

// To Persian
string persianDate = gregorianDate.ToCalendarDate(CalendarType.Persian);

// To Gregorian
string gregorianStr = gregorianDate.ToCalendarDate(CalendarType.Gregorian);

// To Hijri
string hijriDate = gregorianDate.ToCalendarDate(CalendarType.Hijri);

// Parse back
DateTime parsedPersian = "1403/05/15".ToDateTime(CalendarType.Persian);
DateTime parsedGregorian = "2024/08/06".ToDateTime(CalendarType.Gregorian);
DateTime parsedHijri = "1446/02/20".ToDateTime(CalendarType.Hijri);
```

## Design Patterns Used

### 1. **Strategy Pattern**
- `ICalendarService` defines the strategy interface
- Each calendar type implements the strategy
- Client (DatePickerViewModel) uses the strategy without knowing implementation details

### 2. **Factory Pattern**
- `CalendarServiceFactory` creates calendar service instances
- Simplifies calendar service instantiation
- Allows registering custom implementations

### 3. **Extension Methods**
- Provides backward compatibility with existing code
- Offers convenient shortcuts for common operations
- Defaults to Persian calendar for historical compatibility

## Extending the System

### Adding a New Calendar Type

1. **Create a new enum value** (if not already exists):
```csharp
public enum CalendarType
{
    Gregorian = 0,
    Persian = 1,
    Hijri = 2,
    Custom = 3  // New calendar type
}
```

2. **Implement ICalendarService**:
```csharp
public class CustomCalendarService : ICalendarService
{
    // Implement all interface methods
}
```

3. **Register in factory** (in CalendarServiceFactory constructor or dynamically):
```csharp
CalendarServiceFactory.RegisterService(CalendarType.Custom, new CustomCalendarService());
```

4. **Use in DatePicker**:
```csharp
var options = new CalendarOptions 
{
    CalendarType = CalendarType.Custom
};
```

## Performance Considerations

- Calendar services are singleton instances created once and reused
- Date conversions are cached implicitly by the .NET framework
- Month names and day names are generated on-demand from enum metadata

## Backward Compatibility

The `CalendarExtensions` class maintains backward compatibility:
- `ToPersianDate()` and `ToDateTime()` continue to work as before
- All existing code using these extensions will work without modification
- New code can use the more explicit calendar-type-specific methods

## Testing

Each calendar service should be tested for:
- Correct date conversion (round-trip: DateTime -> String -> DateTime)
- Month and year calculations
- Leap year detection
- Holiday (weekend) detection
- Boundary conditions (first/last days of month)
