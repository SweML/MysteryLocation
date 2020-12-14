using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.Model;
using Plugin.Toast;

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
  

        private void Mark_Clicked(object sender, EventArgs e)
        {
            try
            {
                int temp = int.Parse((sender as Button).AutomationId);
                PostListElement refElement = GlobalFuncs.fvm.RemovePost(temp);
                GPSFetcher.mvm.AddPost(refElement);

                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        DependencyService.Get<SnackInterface>().SnackbarShow("Post #" + temp + " is now marked");
                        break;
                    case Device.iOS:
                        CrossToastPopUp.Current.ShowToastMessage("Post #" + temp + " is now marked");
                        break;
                    default:
                        break;
                }
            }
            catch(ArgumentNullException error)
            {
            }


        }

        private async void updateContents(object sender, EventArgs e)
        {
            List<Post> posts = await App.conn.getDataAsync();
        }
    }
}