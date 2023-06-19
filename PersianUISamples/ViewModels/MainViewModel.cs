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
                SelectionMode = PersianUIControlsMaui.Enums.SelectionMode.Multiple,
                SelectDayColor = Colors.Orange,
                AutoCloseAfterSelectDate = true,
                OnAccept = OnAcceptDate,
                OnCancel = new Action(() => { }),
                MinDateCanSelect = DateTime.Now.AddDays(-10),
                MaxDateCanSelect = DateTime.Now.AddDays(10),
                CanSelectHolidays = false
            };
        }

        private void OnAcceptDate(object obj)
        {
            if (obj is not List<DayOfMonth> dates)
                return;

            this.PersianDate = dates.FirstOrDefault()?.PersianDate + " - " + dates.LastOrDefault()?.PersianDate;
        }

        private void OnDateChanged(object obj)
        {

        }
    }
}
