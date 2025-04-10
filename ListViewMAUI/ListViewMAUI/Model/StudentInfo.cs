using System.ComponentModel;

namespace ListViewMAUI
{
    public class StudentInfo : INotifyPropertyChanged
    {
        private DateTime? _date;
        private ImageSource _image;
        private string _name;
        private string _rollNum;
        private string _status = string.Empty;

        public ImageSource Image
        {
            get { return _image; }
            set { 
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string RollNum
        {
            get { return _rollNum; }
            set
            {
                _rollNum = value;
                OnPropertyChanged(nameof(RollNum));
            }
        }

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
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
