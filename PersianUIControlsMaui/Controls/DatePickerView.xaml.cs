using CommunityToolkit.Maui.Views;
using PersianUIControlsMaui.Models;
using PersianUIControlsMaui.ViewModels;

namespace PersianUIControlsMaui.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DatePickerView : Popup
{
    DayOfMonth selectedDate;
    DatePickerViewModel viewModel;


    public event EventHandler<SelectedDateChangedEventArgs> SelectedDateChanged;

    public DatePickerView(CalendarOptions options)
    {
        InitializeComponent();
        viewModel = new DatePickerViewModel(options);
        this.BindingContext = viewModel;
    }

    private void btnMonth_Clicked(object sender, EventArgs e)
    {
        
    }

    private void btnDay_Clicked(object sender, EventArgs e)
    {
        if (((Button)sender).CommandParameter is not DayOfMonth _selectedDate || !_selectedDate.CanSelect)
            return;

        viewModel.SelectDateCommand.Execute(_selectedDate);

        selectedDate = _selectedDate;

        if (SelectedDateChanged != null && selectedDate.CanSelect && viewModel.Options.AutoCloseAfterSelectDate)
            SelectedDateChanged.Invoke(sender, new SelectedDateChangedEventArgs()
            {
                SelectedDate = selectedDate
            });
    }

    private void btnAccept_Clicked(object sender, EventArgs e)
    {
        if (selectedDate is null)
            selectedDate = viewModel.DaysOfMonth.FirstOrDefault(x => x.IsSelected);
        viewModel.Options.OnAccept?.Invoke(selectedDate);
        this.Close();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        viewModel.Options.OnCancel?.Invoke();
        this.Close();
    }
}

public class SelectedDateChangedEventArgs : EventArgs
{
    public DayOfMonth SelectedDate { get; set; }
}