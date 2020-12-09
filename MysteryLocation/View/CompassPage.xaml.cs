using MysteryLocation.Model;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MysteryLocation
{

    public partial class CompassPage : ContentPage
    {
        ViewCompass vm;
        User user;
        public CompassPage(User user)
        {
            this.user = user;
            vm = new ViewCompass(user);
            user.vc = vm;
            GPSFetcher.vc = vm;
            InitializeComponent();
            this.BindingContext = vm;

            vm.Start();
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