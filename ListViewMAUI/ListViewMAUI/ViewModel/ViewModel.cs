using ListViewMAUI.Model;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Windows.Input;

namespace ListViewMAUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private StudentInfoRepository _studentInfoRepository;        
        private AttendanceInfoByDate attendanceInfo;
        private AttendanceInfoByMonth attendanceInfoByMonth;
        private ObservableCollection<StudentAttendance> _appointments;
        private MainPage mainPage;
        private AttendanceReportPage attendanceReportPage;               

        public ICommand SchedulerTappedCommand { get; set; }
        public ICommand ShowReportCommand { get; set; }
        public ICommand SchedulerViewChangedCommand { get; set; }
        public AttendanceInfoByDate AttendanceInfoByDate
        {
            get { return attendanceInfo; }
            set
            {
                attendanceInfo = value;
                OnPropertyChanged(nameof(AttendanceInfoByDate));
            }
        }
        public AttendanceInfoByMonth AttendanceInfoByMonth
        {
            get { return  attendanceInfoByMonth; }
            set
            {
                attendanceInfoByMonth = value;
                OnPropertyChanged(nameof(AttendanceInfoByMonth));
            }
        }
        public ObservableCollection<StudentAttendance> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        // Collection based on date.
        internal ObservableCollection<AttendanceInfoByDate> AttendanceInfoCollectionByDate { get; set; }
        internal ObservableCollection<AttendanceInfoByMonth> AttendanceInfoCollectionByMonth { get; set; }


        public ViewModel() 
        {            
            InitializeRepo();
            InitializeCollection();
            InitializeCommands();
            InitializePages();            
        }

        private void InitializePages()
        {
            mainPage = new MainPage();
            attendanceReportPage = new AttendanceReportPage() { BindingContext = this };
        }
        private void InitializeCollection()
        {
            this.AttendanceInfoCollectionByDate = new ObservableCollection<AttendanceInfoByDate>();
            this.AttendanceInfoCollectionByMonth = new ObservableCollection<AttendanceInfoByMonth>();
            this.Appointments = new ObservableCollection<StudentAttendance>();
        }

        private void InitializeCommands()
        {
            this.SchedulerTappedCommand = new Command<SchedulerTappedEventArgs>(ExecuteTapped);
            this.ShowReportCommand = new Command(ExecuteShowReport);
            this.SchedulerViewChangedCommand = new Command<SchedulerViewChangedEventArgs>(ExecuteViewChanged);
        }
        private void PrepareAttendanceReport(DateTime currentMonthDate)
        {
            var StudentAttendanceCollection = new ObservableCollection<ExpandoObject>();
            var students = _studentInfoRepository.GetStudentNames(100);
            var studentsRollNumbers = _studentInfoRepository.GetStudentsRollNum(100);
            for (int i=0;i<100;i++)
            {
                var student = students[i];
                var rollNum = studentsRollNumbers[i];
                dynamic expando = new ExpandoObject();                
                expando.Name = student; 
                expando.RollNum = rollNum;
                
                // Initialize days 1 to 31
                for (int day = 1; day <= 31; day++)
                {
                    ((IDictionary<string, object>)expando)[day.ToString()] = string.Empty;
                }

                
                StudentAttendanceCollection.Add(expando);
            }

            AttendanceInfoByMonth = new AttendanceInfoByMonth() { AttendanceDate = currentMonthDate, StudentAttendanceCollection = StudentAttendanceCollection };
            AttendanceInfoCollectionByMonth.Add(AttendanceInfoByMonth);
        }

        private void UpdateAttendanceReport(bool isDelete = false)
        {
            if (AttendanceInfoByDate == null || AttendanceInfoByDate.AttendanceDate.Value.Month != AttendanceInfoByMonth.AttendanceDate.Value.Month)
                return;

            var StudentAttendanceCollection = AttendanceInfoCollectionByMonth.FirstOrDefault(o => o.AttendanceDate == AttendanceInfoByMonth.AttendanceDate).StudentAttendanceCollection;
            foreach (dynamic expando in StudentAttendanceCollection)
            {
                var expandoDict = (IDictionary<string, object>)expando;
                var propertyName = AttendanceInfoByDate.AttendanceDate.Value.Day;
                if (expandoDict.ContainsKey(propertyName.ToString()))
                {
                    // Update the value of the existing property
                    expandoDict[propertyName.ToString()] = isDelete ? string.Empty : AttendanceInfoByDate.Students.First(o => o.Name == expandoDict["Name"]).Status.First().ToString();
                }
                else
                {
                    // Optionally handle the case where the property does not exist
                    Console.WriteLine($"Property {propertyName} does not exist.");
                }
            }
        }

        private void TrackStudent_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AttendanceInfoByDate.TrackStudentAttendance))
            {
                if(AttendanceInfoByDate.TrackStudentAttendance != null)
                    AttendanceInfoByDate.TrackStudentAttendance.PropertyChanged += TrackStudentAttendance_PropertyChanged;               
                UpdateAppointments();
            }
        }

        private void TrackStudentAttendance_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            UpdateAppointments();
        }

        private void UpdateAppointments()
        {
            if (AttendanceInfoByDate.TrackStudentAttendance == null)
            {
                Appointments.Remove(Appointments.FirstOrDefault(o => o.From.Date == AttendanceInfoByDate.AttendanceDate));
                Appointments.Remove(Appointments.FirstOrDefault(o => o.From.Date == AttendanceInfoByDate.AttendanceDate));
                UpdateAttendanceReport(true);
                AttendanceInfoByDate = null;
                return;
            }
            var appointments = Appointments.Where(o => o.From.Date == AttendanceInfoByDate.AttendanceDate).ToList();
            var attendanceAsAppointment = AttendanceInfoByDate.TrackStudentAttendance;
            if (!appointments.Any())
            {                
                Appointments.Add(new StudentAttendance() { From = attendanceAsAppointment.From, To = attendanceAsAppointment.To, Background = Colors.Green, EventName = "P :" + attendanceAsAppointment.PresentCount });
                Appointments.Add(new StudentAttendance() { From = attendanceAsAppointment.From, To = attendanceAsAppointment.To, Background = Colors.Red, EventName = "A : " + attendanceAsAppointment.AbsentCount });
            }
            else
            {                
                appointments[0].EventName = "P :" + attendanceAsAppointment.PresentCount;
                appointments[1].EventName = "A :" + attendanceAsAppointment.AbsentCount;
            }

            UpdateAttendanceReport();
        }
        #region Commands

        private void ExecuteViewChanged(SchedulerViewChangedEventArgs obj)
        {
            var currentMonthDate = obj.NewVisibleDates[obj.NewVisibleDates.Count/2].Date;
            if(AttendanceInfoCollectionByMonth.Count>0)
            {
                var currentCollection = AttendanceInfoCollectionByMonth.FirstOrDefault(o=>o.AttendanceDate == currentMonthDate);
                if (currentCollection == null)
                    PrepareAttendanceReport(currentMonthDate);
                else
                {
                    AttendanceInfoByMonth = currentCollection;
                    UpdateAttendanceReport();
                }
            }
            else if(AttendanceInfoCollectionByMonth.Count == 0)
            {
                PrepareAttendanceReport(currentMonthDate);
            }            
        }

        private void ExecuteShowReport()
        {            
            Application.Current.MainPage.Navigation.PushAsync(attendanceReportPage);
        }

        private void ExecuteTapped(SchedulerTappedEventArgs obj)
        {
            if (obj.Element != SchedulerElement.SchedulerCell)
                return;

            var selectedDate = obj.Date;
            AttendanceInfoByDate = this.AttendanceInfoCollectionByDate.FirstOrDefault(o => o.AttendanceDate == selectedDate);
            if (AttendanceInfoByDate == null)
            {                
                var students = _studentInfoRepository.GetStudentDetails(100);
                AttendanceInfoByDate = new AttendanceInfoByDate() { AttendanceDate = selectedDate, Students = students, Status = new List<string>() { "Present", "Absent" } };
                AttendanceInfoByDate.PropertyChanged += TrackStudent_PropertyChanged;
                this.AttendanceInfoCollectionByDate.Add(AttendanceInfoByDate);                               
            }
            mainPage.BindingContext = AttendanceInfoByDate;            
            Application.Current.MainPage.Navigation.PushAsync(mainPage);
        }

        public void InitializeRepo()
        {
            _studentInfoRepository = new();
        }

        #endregion               
        private void OnPropertyChanged (string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
