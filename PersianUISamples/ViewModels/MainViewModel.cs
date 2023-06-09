using PersianUIControlsMaui.Models;
using PersianUIControlsMaui.ViewModels;

namespace PersianUISamples.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string persianDate;
        private CalendarOptions calendarOption;
        private Command onChangeDateCommand;

        public string PersianDate { get => persianDate; set => SetProperty(ref persianDate, value); }
        public CalendarOptions CalendarOption { get => calendarOption; set => SetProperty(ref calendarOption, value); }

        public Command OnChangeDateCommand { get { onChangeDateCommand ??= new Command(OnDateChanged); return onChangeDateCommand; } }

        public MainViewModel()
        {
            CalendarOption = new CalendarOptions()
            {
                SelectDateMode = PersianUIControlsMaui.Enums.SelectionDateMode.Day,
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Single,
                SelectDayColor = Colors.Orange,
                AutoCloseAfterSelectDate = false,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                MinDateCanSelect = DateTime.Now.AddDays(-3),
                MaxDateCanSelect = DateTime.Now.AddDays(4),
                CanSelectHolidays = false
            };
        }

        private void OnAcceptDate(DayOfMonth obj)
        {
            this.PersianDate = obj.PersianDate;
        }

        private void OnDateChanged(object obj)
        {

        }
    }
}
