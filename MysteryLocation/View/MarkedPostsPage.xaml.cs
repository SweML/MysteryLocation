using MysteryLocation.Model;
using Plugin.Toast;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkedPostsPage : ContentPage
    {
        public User user;
        public MarkedPostsPage(User user)
        {
            this.user = user;
            GPSFetcher.mvm = GlobalFuncs.mvm;
            this.BindingContext = GlobalFuncs.mvm;
            InitializeComponent();
        }

        private async void Track(object sender, EventArgs e)
        {
            if (GlobalFuncs.mvm.tracked != null)
                GlobalFuncs.mvm.tracked.Color = "#404040";
            int obsID = int.Parse((sender as Button).AutomationId);
            GlobalFuncs.addTracker(obsID);
            GlobalFuncs.mvm.setMyPos(GPSFetcher.currentPosition);

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    DependencyService.Get<SnackInterface>().SnackbarShow("The post #" + obsID + " is now being tracked");
                    break;
                case Device.iOS:
                    CrossToastPopUp.Current.ShowToastMessage("The post #" + obsID + " is now being tracked");
                    break;
                default:
                    break;
            }
        }



    }
}