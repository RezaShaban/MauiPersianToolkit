using System.ComponentModel.DataAnnotations;

namespace MauiPersianToolkit.Enums;

public enum PersianMonthNames
{
    [Display(Name = "فروردین")]
    Farvardin = 0,
    [Display(Name = "اردیبهشت")]
    Ordibehesht,
    [Display(Name = "خرداد")]
    Khordad,
    [Display(Name = "تیر")]
    Tir,
    [Display(Name = "مرداد")]
    Mordad,
    [Display(Name = "شهریور")]
    Shahrivar,
    [Display(Name = "مهر")]
    Mehr,
    [Display(Name = "آبان")]
    Aban,
    [Display(Name = "آذر")]
    Azar,
    [Display(Name = "دی")]
    Day,
    [Display(Name = "بهمن")]
    Bahman,
    [Display(Name = "اسفند")]
    Esfand
}
