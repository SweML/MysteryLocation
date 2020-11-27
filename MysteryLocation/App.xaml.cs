
using MysteryLocation.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("Helvetica93.ttf", Alias = "Helvetica")]
[assembly: ExportFont("WalkWay_Semibold.ttf", Alias = "WalkWay")]
[assembly: ExportFont("Lato-Light.ttf", Alias = "Latow")]
[assembly: ExportFont("Lato-Black.ttf", Alias = "Latob")]
[assembly: ExportFont("AndersonGrotesk-Ultrabold.otf", Alias = "Grotesk")]
namespace MysteryLocation
{
    public partial class App : Application
    {

        public User user;
        APIConnection conn;
        public static double ScreenWidth;
        public static double ScreenHeight;
        public App()
        {
            
            conn = new APIConnection();
            user = new User(true, 329, conn);
            GPSUpdater gps = new GPSUpdater(user);
            gps.startTimer(1);
            Label gpsLabel = new Label();

            /*  Task.Run(async () =>
             {
                 await user.updatePosts();
             });*/
            InitializeComponent();
            MainPage = new NavigationBar(user, conn);
            //MainPage = new SettingsPage();
            //MainPage = new PhotoPage();
            //NavigationPage navigation = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
