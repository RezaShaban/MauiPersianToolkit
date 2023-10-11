using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersianUIControlsMaui.Extensions;

public static class EnumExtensions
{
    public static string GetDisplay(this Type type, int value)
    {
        var enumItem = Enum.Parse(type, value.ToString());
        var displayName = type.GetField(enumItem.ToString()).GetCustomAttribute<DisplayAttribute>().Name;
        return displayName;
    }
}
