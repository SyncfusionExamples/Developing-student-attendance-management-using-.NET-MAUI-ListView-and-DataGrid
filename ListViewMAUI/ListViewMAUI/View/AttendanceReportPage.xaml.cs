using Syncfusion.Maui.DataGrid;

namespace ListViewMAUI
{  
    public partial class AttendanceReportPage : ContentPage
    {
        public AttendanceReportPage()
        {
            InitializeComponent();
        }

private void SfDataGrid_AutoGeneratingColumn(object sender, Syncfusion.Maui.DataGrid.DataGridAutoGeneratingColumnEventArgs e)
{
    if (!(e.Column.MappingName == "Name" || e.Column.MappingName == "RollNum"))
        e.Column.ColumnWidthMode = ColumnWidthMode.FitByHeader;
    e.Column.MappingName = "[" + e.Column.MappingName + "]";
}

        private void SfDataGrid_Loaded(object sender, EventArgs e)
        {
            (sender as SfDataGrid).Refresh();
        }
    }
}