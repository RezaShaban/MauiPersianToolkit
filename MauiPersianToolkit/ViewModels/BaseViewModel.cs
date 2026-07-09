using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiPersianToolkit.ViewModels;
public abstract partial class ObservableObject : INotifyPropertyChanged
{
    private static readonly Dictionary<string, PropertyChangedEventArgs> _eventArgsCache = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null, Action? onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;

        onChanged?.Invoke();

        OnPropertyChanged(propertyName);

        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null)
            return;

        if (!_eventArgsCache.TryGetValue(propertyName, out var args))
        {
            args = new PropertyChangedEventArgs(propertyName);
            _eventArgsCache[propertyName] = args;
        }

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged(params string[] propertyNames)
    {
        foreach (var propertyName in propertyNames)
            OnPropertyChanged(propertyName);
    }
}