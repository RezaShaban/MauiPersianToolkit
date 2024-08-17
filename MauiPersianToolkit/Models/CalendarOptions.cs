using MauiPersianToolkit.Enums;
using MauiPersianToolkit.ViewModels;

namespace MauiPersianToolkit.Models
{
    public class CalendarOptions : ObservableObject
    {
        private SelectionDateMode selectDateMode;
        private Enums.SelectionMode selectionMode;
        private string selectedPersianDate;
        private List<string> selectedPersianDates;
        private DateTime? minDateCanSelect;
        private DateTime? maxDateCanSelect;
        private bool canSelectHolidays;
        private Color selectDayColor;
        private bool autoCloseAfterSelectDate;

        public CalendarOptions()
        {
            selectDateMode = SelectionDateMode.Day;
            selectionMode = Enums.SelectionMode.Single;
            selectedPersianDate = DateTime.Now.ToPersianDate();
            selectedPersianDates = new List<string>();
            canSelectHolidays = true;
            selectDayColor = Colors.DeepSkyBlue;
            autoCloseAfterSelectDate = true;
        }
        /// <summary>
        /// default is Day
        /// </summary>
        public SelectionDateMode SelectDateMode { get => selectDateMode; set => SetProperty(ref selectDateMode, value); }

        /// <summary>
        /// 
        /// </summary>
        public Enums.SelectionMode SelectionMode { get { return selectionMode; } set { selectionMode = value; } }

        /// <summary>
        /// default is Now
        /// </summary>
        internal string SelectedPersianDate { get => selectedPersianDate; set => SetProperty(ref selectedPersianDate, value); }

        /// <summary>
        /// default is Now
        /// </summary>
        internal List<string> SelectedPersianDates { get => selectedPersianDates; set => SetProperty(ref selectedPersianDates, value); }

        /// <summary>
        /// default is null
        /// </summary>
        public DateTime? MinDateCanSelect { get => minDateCanSelect; set => SetProperty(ref minDateCanSelect, value); }

        /// <summary>
        /// default is null
        /// </summary>
        public DateTime? MaxDateCanSelect { get => maxDateCanSelect; set => SetProperty(ref maxDateCanSelect, value); }

        /// <summary>
        /// default is true
        /// </summary>
        public bool CanSelectHolidays { get => canSelectHolidays; set => SetProperty(ref canSelectHolidays, value); }

        /// <summary>
        /// default is DeepSkyBlue
        /// </summary>
        public Color SelectDayColor { get => selectDayColor; set { SetProperty(ref selectDayColor, value); OnPropertyChanged(nameof(InRangeDayColor)); } }
        public Color InRangeDayColor { get => selectDayColor.MultiplyAlpha(0.3f); }

        /// <summary>
        /// default is true
        /// </summary>
        public bool AutoCloseAfterSelectDate { get { return autoCloseAfterSelectDate; } set { autoCloseAfterSelectDate = value; } }

        /// <summary>
        /// invoke at accept selected date
        /// </summary>
        public Action<object> OnAccept { get; set; }

        /// <summary>
        /// invoke at cancel selected date
        /// </summary>
        public Action OnCancel { get; set; }
    }
}
