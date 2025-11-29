using System.Windows;
using Syncfusion.Licensing;

namespace FrontB
{
    public partial class App : Application
    {
        public App()
        {           
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF5cXGRCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWH5cdXRSQ2deU0NzW0NWYEg=");
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}