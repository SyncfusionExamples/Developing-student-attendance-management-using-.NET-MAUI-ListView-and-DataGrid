using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ListViewMAUI
{
    public class AttendanceInfoByDate : INotifyPropertyChanged
    {
        private string selectedItem;
        private List<string> status;
        private DateTime? attendanceDate;
        private ObservableCollection<StudentInfo> students;
        private StudentAttendance trackStudenntAttendance;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                UpdateStatus(SelectedItem);
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

        public ObservableCollection<StudentInfo> Students
        {
            get
            {
                return students;
            }
            set
            {
                students = value;
                OnPropertyChanged(nameof(Students));
            }
        }        

        public List<string> Status
        {
            get { return status; }
            set
            {
                status = value; OnPropertyChanged(nameof(Status));
            }
        }

        public StudentAttendance TrackStudentAttendance
        {
            get
            {
                return trackStudenntAttendance;
            }
            set
            {
                trackStudenntAttendance = value;
                OnPropertyChanged(nameof(TrackStudentAttendance));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public AttendanceInfoByDate()
        {
            this.SaveCommand = new Command(ExecuteSaveCommand);
            this.DeleteCommand = new Command(ExecuteDeleteCommand);
        }

        private void ExecuteSaveCommand()
        {
            DoTrackAttendance();
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ExecuteDeleteCommand()
        {
            TrackStudentAttendance = null;
            AttendanceDate = null;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void UpdateStatus(string status)               
        {
            foreach (var student in Students)
                student.Status = status;
        }

        private void DoTrackAttendance()
        {
            var presentCount = Students.Where(o => o.Status.Equals("Present")).Count();
            var absentCount = Students.Where(o => o.Status.Equals("Absent")).Count();
            if (TrackStudentAttendance == null)
                TrackStudentAttendance = new StudentAttendance() { IsAllDay = true, PresentCount = presentCount, AbsentCount = absentCount, From = AttendanceDate.Value.Date, To = AttendanceDate.Value.Date };
            else
            {
                TrackStudentAttendance.PresentCount = presentCount;
                TrackStudentAttendance.AbsentCount = absentCount;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
