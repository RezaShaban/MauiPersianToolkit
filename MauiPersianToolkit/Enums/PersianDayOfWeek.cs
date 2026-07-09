using System.ComponentModel.DataAnnotations;

namespace MauiPersianToolkit.Enums;

public enum PersianDayOfWeek
{
    [Display(Name = "ش")]
    Saturday,
    [Display(Name = "ی")]
    Sunday,
    [Display(Name = "د")]
    Monday,
    [Display(Name = "س")]
    Tuesday,
    [Display(Name = "چ")]
    Wednesday,
    [Display(Name = "پ")]
    Thursday,
    [Display(Name = "ج")]
    Friday
}
