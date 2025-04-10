namespace ListViewMAUI
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MDAxQDMyMzkyZTMwMmUzMDNiMzIzOTNiaEJ0cWhtUVpBOXpnNXk3SDBjNkwybGJHQTRSU2xMZHFtbGM4ZkxjR3pndz0=");

            InitializeComponent();

            MainPage = new NavigationPage(new AttendanceTrackingPage());
        }
    }
}
