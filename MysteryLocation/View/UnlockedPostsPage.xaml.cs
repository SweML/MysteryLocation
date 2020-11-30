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
    public partial class UnlockedPostsPage : ContentPage
    {
        public User user;
        public UnlockedViewModel uvm;
        public UnlockedPostsPage(User user)
        {
            this.user = user;
            uvm = new UnlockedViewModel(user);
            InitializeComponent();
            currentGPS.BindingContext = user;
            listview.BindingContext = uvm;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(user), true);
        }
    }
}