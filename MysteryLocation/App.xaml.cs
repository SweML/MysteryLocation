
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

            User user = new User(true, 329);
            Console.WriteLine("The user has been created.");

            MainPage = new NavigationBar(user);

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
