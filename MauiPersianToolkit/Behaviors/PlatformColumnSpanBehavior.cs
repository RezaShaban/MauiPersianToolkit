namespace MauiPersianToolkit.Behaviors;

public class PlatformColumnSpanBehavior : Behavior<View>
{
    public int Android { get; set; }
    public int iOS { get; set; }
    public int WinUI { get; set; }

    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);

        if (DeviceInfo.Platform == DevicePlatform.Android)
            Grid.SetColumnSpan(bindable, Android);
        if (DeviceInfo.Platform == DevicePlatform.iOS)
            Grid.SetColumnSpan(bindable, iOS);
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            Grid.SetColumnSpan(bindable, WinUI);
    }
}
