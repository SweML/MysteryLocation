using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderView : ContentView
    {
        bool _isTapped;
        public HeaderView()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (_isTapped)
                return;

            _isTapped = true;
            GPSFetcher.pvm.startTimer();
            await Navigation.PushModalAsync(new NavigationPage(new PublishPage()));
            _isTapped = false;
        }
    }
}