using MysteryLocation.Model;
using MysteryLocation.View;
using MysteryLocation.ViewModel;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkedPostsPage : ContentPage
    {
        public User user;
       // private Button prevButton;
        //public MarkedViewModel mvm;
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
            Console.WriteLine("It registers a tap on label Track " + sender.ToString() + " " + e.ToString());
            int obsID = int.Parse((sender as Button).AutomationId);
           // prevButton = sender as Button;
           // prevButton.BackgroundColor = Color.FromHex("#1f9f64");
            Console.WriteLine("OBSAD" + obsID);
            //add if true -> then notify?
            GlobalFuncs.addTracker(obsID);
            GlobalFuncs.mvm.setMyPos(GPSFetcher.currentPosition);

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    DependencyService.Get<SnackInterface>().SnackbarShowIndefininte("The post #" + obsID + "is now being tracked");
                    break;
                case Device.iOS:
                    CrossToastPopUp.Current.ShowToastMessage("The post #" + obsID + "is now being tracked");
                    break;
                default:
                    break;
            }
        }



    }
}