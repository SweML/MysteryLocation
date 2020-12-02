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
            mvm = new MarkedViewModel(user);
            InitializeComponent();
            this.BindingContext = mvm;
            user.mvm = mvm;
            //currentGPS.BindingContext = user;

        }

        

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }
    }
}