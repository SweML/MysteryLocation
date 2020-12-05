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
        public MarkedViewModel mvm;
        public MarkedPostsPage(User user)
        {
            this.user = user;
            mvm = new MarkedViewModel(user, this);
            user.mvm = mvm;
            GPSFetcher.mvm = mvm;
            this.BindingContext = mvm;
            InitializeComponent();
            
            
            //currentGPS.BindingContext = user;
            
        }

        public void updateBindingContext()
        {
            this.BindingContext = mvm;
        }

        

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }

        private async void testLabelButton(object sender, EventArgs e)
        {
            Console.WriteLine("It registers a tap on label Track " + sender.ToString() + " " + e.ToString());
            int obsID = int.Parse((sender as Button).AutomationId);
            user.addTracker(obsID);
            await Navigation.PushAsync(new CompassPage(user), false);
        }
    }
}