using System.ComponentModel.DataAnnotations;

namespace MauiPersianToolkit.Enums;

public enum GregorianDayOfWeek
{
    [Display(Name = "Mo")]
    Monday,
    [Display(Name = "Tu")]
    Tuesday,
    [Display(Name = "We")]
    Wednesday,
    [Display(Name = "Th")]
    Thursday,
    [Display(Name = "Fr")]
    Friday,
    [Display(Name = "Sa")]
    Saturday,
    [Display(Name = "Su")]
    Sunday,
}