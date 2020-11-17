
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

            UserHolder.user = new User(null, null, null, true, 329);
            Console.WriteLine("The user has been created.");

            MainPage = new NavigationBar();

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
