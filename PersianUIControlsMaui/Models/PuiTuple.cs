using PersianUIControlsMaui.ViewModels;

namespace PersianUIControlsMaui.Models;

public class PuiTuple : ObservableObject
{
    string key;
    string value;
    public string Key { get => key; set => SetProperty(ref key, value); }
    public string Value { get => value; set => SetProperty(ref this.value, value); }

    public PuiTuple() { }
    public PuiTuple(string _key, string _value)
    {
        Key = _key;
        Value = _value;
    }
}
