using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;

namespace ListViewMAUI
{
    public class AttendanceInfoByMonth : INotifyPropertyChanged
    {
        private DateTime? attendanceDate;
        private ObservableCollection<ExpandoObject> studentAttendanceCollection;
        public ObservableCollection<ExpandoObject> StudentAttendanceCollection
        {
            get
            {
                return studentAttendanceCollection;
            }
            set
            {
                studentAttendanceCollection = value;
                OnPropertyChanged(nameof(StudentAttendanceCollection));
            }
        }

        public DateTime? AttendanceDate
        {
            get
            {
                return attendanceDate;
            }
            set
            {
                attendanceDate = value;
                OnPropertyChanged(nameof(AttendanceDate));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
