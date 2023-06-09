using PersianUIControlsMaui.Enums;
using PersianUIControlsMaui.ViewModels;

namespace PersianUIControlsMaui.Models;

public class DayOfMonth : ObservableObject
{
    bool isSelected;
    bool isInCurrentMonth;
    int dayNum;
    PersianDayOfWeek dayOfWeek;
    string persianDate;
    DateTime gregorianDate;
    bool isHoliday;
    bool canSelect;

    public PersianDayOfWeek DayOfWeek { get => dayOfWeek; set => SetProperty(ref dayOfWeek, value); }
    public int DayNum { get => dayNum; set => SetProperty(ref dayNum, value); }
    public bool IsInCurrentMonth { get => isInCurrentMonth; set => SetProperty(ref isInCurrentMonth, value); }
    public bool CanSelect { get => canSelect; set => SetProperty(ref canSelect, value); }
    public bool IsSelected { get => isSelected; set => SetProperty(ref isSelected, value); }
    public string PersianDate { get => persianDate; set => SetProperty(ref persianDate, value); }
    public DateTime GregorianDate { get => gregorianDate; set => SetProperty(ref gregorianDate, value); }
    public bool IsHoliday { get => isHoliday; set => SetProperty(ref isHoliday, value); }
}
