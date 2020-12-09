using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.ViewModel;
using MysteryLocation.Model;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardViewTemp : ContentPage
    {
        public CardViewTemp(User user)
        {
            GPSFetcher.fvm = GlobalFuncs.fvm;
            InitializeComponent();
            this.BindingContext = GlobalFuncs.fvm;
        }
  
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          
            // then pass it to your page
            await Navigation.PushAsync(new DetailsPage(App.user), true);
        }

        private void Mark_Clicked(object sender, EventArgs e)
        {
            int temp = int.Parse((sender as Button).AutomationId);
            PostListElement refElement = GlobalFuncs.fvm.RemovePost(temp);
            GPSFetcher.mvm.AddPost(refElement);

        }

        //Metod vid refresh.
        private async void updateContents(object sender, EventArgs e)
        {
            List<Post> posts = await App.conn.getDataAsync();
            GlobalFuncs.fvm.updateListElements(posts);
        }
    }
}