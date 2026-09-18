using MauiPersianToolkit.Controls;
using PersianUISamples.ViewModels;

namespace PersianUISamples.Pages;

public partial class DateTimePage : ContentPage
{
    public DateTimePage() : this(ServiceHelper.GetRequiredService<DateTimeGalleryViewModel>()) { }

    public DateTimePage(DateTimeGalleryViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void OnLoadInlineCalendar(object? sender, EventArgs e)
    {
        if (BindingContext is not DateTimeGalleryViewModel vm)
            return;

        if (inlineCalendarHost.Content is not null)
            return;

        inlineCalendarHost.Content = new DatePickerView(vm.HijriSingle)
        {
            HeightRequest = 360
        };
        loadCalendarButton.IsVisible = false;
    }
}
