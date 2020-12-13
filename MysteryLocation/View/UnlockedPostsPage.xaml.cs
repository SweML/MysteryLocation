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
        //  public UnlockedViewModel uvm;
        public UnlockedPostsPage(User user)
        {
            this.user = user;
            //uvm = new UnlockedViewModel(user);
            this.BindingContext = GlobalFuncs.uvm;
            InitializeComponent();
            user.uvm = GlobalFuncs.uvm;
            GPSFetcher.uvm = GlobalFuncs.uvm;
            //currentGPS.BindingContext = user;

        }

        Boolean _istapped = false;
        private async void Details_Clicked(object sender, EventArgs e)
        {
            
            if (_istapped)
                return;

            _istapped = true;
            int temp = int.Parse((sender as Button).AutomationId);

            await Navigation.PushModalAsync(new DetailsPage(user, temp), true);

            _istapped = false;
        }
    }
}