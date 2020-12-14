using MysteryLocation.Model;
using MysteryLocation.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnlockedPostsPage : ContentPage
    {
        public User user;
        public UnlockedPostsPage(User user)
        {
            this.user = user;

            this.BindingContext = GlobalFuncs.uvm;
            InitializeComponent();
            user.uvm = GlobalFuncs.uvm;
            GPSFetcher.uvm = GlobalFuncs.uvm;

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