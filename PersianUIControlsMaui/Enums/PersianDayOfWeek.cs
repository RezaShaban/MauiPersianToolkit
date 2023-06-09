using System.ComponentModel.DataAnnotations;

namespace PersianUIControlsMaui.Enums;

public enum PersianDayOfWeek
{
    [Display(Name = "شنبه")]
    Saturday,
    [Display(Name = "یکشنبه")]
    Sunday,
    [Display(Name = "دوشنبه")]
    Monday,
    [Display(Name = "سه شنبه")]
    Tuesday,
    [Display(Name = "چهارشنبه")]
    Wednesday,
    [Display(Name = "پنجشنبه")]
    Thursday,
    [Display(Name = "جمعه")]
    Friday
}
