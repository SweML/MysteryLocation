using MysteryLocation.Model;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MysteryLocation
{

    public partial class CompassPage : ContentPage
    {
        ViewCompass vm;       
        public CompassPage(User user)
        {
            InitializeComponent();

            ///versionsTracking Hadi
            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver)
            {
                termsLayout.IsVisible = true;
                layout.IsVisible = false;
            }

            else
            {
                termsLayout.IsVisible = false;
                layout.IsVisible = true;
                ///versionsTracking Hadi


                ///Milads
                BindingContext = vm = new ViewCompass();
                try
                {
                    currentGPS.BindingContext = user;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        Console.WriteLine(e.InnerException.Message);
                }
                vm.Start();
            }
        }



        ///the button aDDED
        public void onAgree(object sender, EventArgs e)
        {
            termsLayout.IsVisible = false;
            layout.IsVisible = true;
        }

    }
}