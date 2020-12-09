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
        public MarkedPostsPage(User user)
        {
            this.user = user;
            GPSFetcher.mvm = GlobalFuncs.mvm;
            this.BindingContext = GlobalFuncs.mvm;
            InitializeComponent();
        }

      
        

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }
        private async void testLabelButton(object sender, EventArgs e)
        {
            int obsID = int.Parse((sender as Button).AutomationId); // Should we save the sorted posts in User as well? user.addTracker(int obsId){ foreach(Post x in memory) {if(x.getId() == obsID) tracking = x;} }
                                                                    //TODO

            //  user.addTracker(obsID);
            await Navigation.PushAsync(new CompassPage(user), false);
        }
    }
}