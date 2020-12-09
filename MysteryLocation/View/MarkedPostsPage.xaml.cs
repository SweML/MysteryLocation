using MysteryLocation.Model;
using MysteryLocation.View;
using MysteryLocation.ViewModel;
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
        private Button prevButton;
        //public MarkedViewModel mvm;
        public MarkedPostsPage(User user)
        {
            this.user = user;
           // mvm = new MarkedViewModel(user, this);
            //user.mvm = mvm;
            GPSFetcher.mvm = GlobalFuncs.mvm;
            this.BindingContext = GlobalFuncs.mvm;
            InitializeComponent();
            
            
            //currentGPS.BindingContext = user;
            
        }

      
        

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }

        private async void Track(object sender, EventArgs e)
        {
            if(prevButton != null)
            {
                prevButton.BackgroundColor = Color.FromHex("#404040"); ;
            }
            Console.WriteLine("It registers a tap on label Track " + sender.ToString() + " " + e.ToString());
            int obsID = int.Parse((sender as Button).AutomationId);
            prevButton = sender as Button;
            prevButton.BackgroundColor = Color.FromHex("#1f9f64");
            Console.WriteLine("OBSAD" + obsID);
            //add if true -> then notify?
            GlobalFuncs.addTracker(obsID);
            DependencyService.Get<SnackInterface>().SnackbarShow("The post #" + obsID + "is now being tracked");
        }

        

    }
}