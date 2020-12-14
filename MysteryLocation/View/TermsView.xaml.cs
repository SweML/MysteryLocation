using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsView : ContentPage
    {
        public TermsView()
        {
            InitializeComponent();
        }

        private async void agreeButton_Clicked(object sender, EventArgs e)
        {
            App.user.termsFlag = 1;
            await Navigation.PopModalAsync(true);
        }
    }
}