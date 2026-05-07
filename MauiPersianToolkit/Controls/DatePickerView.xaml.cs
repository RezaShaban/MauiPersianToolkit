using CommunityToolkit.Maui.Views;
using MauiPersianToolkit.Models;
using MauiPersianToolkit.ViewModels;

namespace MauiPersianToolkit.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePickerView : Popup
{
    private DayOfMonth _selectedDate;
    private DatePickerViewModel _viewModel;

    public event EventHandler<SelectedDateChangedEventArgs> SelectedDateChanged;

    public DatePickerView(CalendarOptions options)
    {
        InitializeComponent();
        InitializeView(options);
    }

    private void InitializeView(CalendarOptions options)
    {
        try
        {
            btnAccept.Clicked += BtnAccept_Clicked;
            btnCancel.Clicked += BtnCancel_Clicked;
            
            _viewModel = new DatePickerViewModel(options);
            this.BindingContext = _viewModel;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing DatePickerView: {ex.Message}");
            throw;
        }
    }

    private void BtnDay_Clicked(object sender, EventArgs e)
    {
        if (((Button)sender).CommandParameter is not DayOfMonth selectedDayOfMonth || !selectedDayOfMonth.CanSelect)
            return;

        _viewModel.SelectDateCommand.Execute(selectedDayOfMonth);
        _selectedDate = selectedDayOfMonth;

        if (SelectedDateChanged != null && _viewModel.CanClose(_selectedDate))
        {
            _viewModel.Options.OnAccept?.Invoke(_viewModel.SelectedDays);
            SelectedDateChanged.Invoke(sender, new SelectedDateChangedEventArgs
            {
                SelectedDate = _selectedDate,
                SelectedDates = _viewModel.SelectedDays.ToList()
            });
            this.Close();
        }
    }

    private void BtnAccept_Clicked(object sender, EventArgs e)
    {
        var dates = _viewModel.SelectedDays.Where(x => x.IsSelected).ToList();
        _viewModel.Options.OnAccept?.Invoke(dates);
        this.Close();
    }

    private void BtnCancel_Clicked(object sender, EventArgs e)
    {
        _viewModel.Options.OnCancel?.Invoke();
        this.Close();
    }
}

public class SelectedDateChangedEventArgs : EventArgs
{
    public DayOfMonth SelectedDate { get; set; }
    public List<DayOfMonth> SelectedDates { get; set; }
}