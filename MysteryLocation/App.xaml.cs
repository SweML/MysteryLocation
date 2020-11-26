
using MysteryLocation.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    public partial class App : Application
    {

        public User user;
        APIConnection conn;
        public App()
        {
            InitializeComponent();
            Label gpsLabel = new Label();
            conn = new APIConnection();
            user = new User(true, 329, conn);
            Task.Run(async () =>
           {
               await user.updatePosts();
           });
            GPSUpdater gps = new GPSUpdater(user);
            gps.startTimer(5);
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
