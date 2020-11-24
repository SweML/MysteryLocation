
using MysteryLocation.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Label gpsLabel = new Label();
            APIConnection conn = new APIConnection();
            User user = new User(true, 329, conn);
            GPSUpdater gps = new GPSUpdater(gpsLabel, user);
            gps.startTimer(1);
            Console.WriteLine("The user has been created.");

            MainPage = new NavigationBar(user, gpsLabel, conn);

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
