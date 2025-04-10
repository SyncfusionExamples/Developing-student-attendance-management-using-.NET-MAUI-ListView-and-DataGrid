using Syncfusion.Maui.DataGrid;
using System.Globalization;
using System.Security.AccessControl;

namespace ListViewMAUI
{
    public class ForeColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo info)
        {
            var gridCell = value as DataGridCell;
            if (gridCell == null || gridCell.DataColumn == null)
                return null;

            if (gridCell.DataColumn.DataGridColumn.MappingName == "[Name]" || gridCell.DataColumn.DataGridColumn.MappingName == "[RollNum]")
                return null;

            var cellValue = gridCell.DataColumn.CellValue;
            if (cellValue != null)
            {
                if (cellValue.Equals("P"))
                    return Colors.Green;
            }
            return Colors.Red;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MonthConverter : IValueConverter
    {
        string[] months;
        public MonthConverter()
        {
            months = new string[]
          {
           " ", "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
          };
        }
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo info)
        {
            var attendanceDate = value as AttendanceInfoByMonth;
            if (attendanceDate == null)
                return string.Empty;
            return months[attendanceDate.AttendanceDate.Value.Month] + "  " + attendanceDate.AttendanceDate.Value.Year;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
