using System.ComponentModel;

namespace ListViewMAUI
{
    // This is for appointment mapping, Using this to show present and absent count as appointments;
    public class StudentAttendance :INotifyPropertyChanged
    {
        private string eventName;
        private int presentCount;
        private int absentCount;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsAllDay { get; set; }
        public string EventName
        {
            get
            {
                return eventName;
            }
            set
            {
                eventName = value;
                OnPropertyChanged(nameof(EventName));
            }
        }
        public TimeZoneInfo StartTimeZone { get; set; }
        public TimeZoneInfo EndTimeZone { get; set; }
        public Brush Background { get; set; }
        public Color TextColor { get; set; } = Colors.White;        
        internal int PresentCount
        {
            get { return presentCount; }
            set
            {
                presentCount = value;
                OnPropertyChanged(nameof(PresentCount));
            }
        }

        internal int AbsentCount
        {
            get { return absentCount; }
            set
            {
                absentCount = value;
                OnPropertyChanged(nameof(AbsentCount));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        internal void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
