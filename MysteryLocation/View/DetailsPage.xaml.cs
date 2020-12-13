using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MysteryLocation.Model;
using System.IO;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;

namespace MysteryLocation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        User user;
        int id;
        public PostListElement temp;
        public ImageSource Img;
        public GPSFetcher tpw;
        string location;
        public DetailsPage(User user, int id)
        {
            this.id = id;
            this.user = user;
            //uvm = new UnlockedViewModel(user);


            /**/
            
            InitializeComponent();
            foreach (PostListElement x in GlobalFuncs.uvm.Items)
            {
                if (x.Id == id)
                {
                    
                    temp = x;
                    image.Source = x.Img;
                    Title.Text = x.Subject;
                    Body.Text = x.Body;
                    Latitude.Text = x.Position.Latitude.ToString();
                    Longitude.Text = x.Position.Longitude.ToString();
                    Date.Text = x.Created;
                    Task.Run(async () => {
                        await set(x);
                        Location.Text = location;
                    });
                    

                    //More to be added.
                }
            }
        }

        public async Task set(PostListElement x)
        {
           location = await WritePositionWithLocation(x.Position);
        }

        public async Task<String> WritePositionWithLocation(Position p)
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(p.Latitude, p.Longitude);

            var placemark = placemarks?.FirstOrDefault();


            StringBuilder sb = new StringBuilder();

            if (placemark != null)
            {
                if(placemark.CountryName != null)
                {
                    sb.Append(placemark.CountryName);
                }
                if(placemark.CountryCode != null)
                {
                    sb.Append(", " + placemark.CountryCode);
                }
                if(placemark.AdminArea != null)
                {
                    sb.Append(", " + placemark.AdminArea);
                }
                if(placemark.SubAdminArea != null)
                {
                    sb.Append(", " + placemark.SubAdminArea);
                }
                if (placemark.Locality != null)
                {
                    sb.Append(", " + placemark.Locality);
                }
                if (placemark.SubLocality != null)
                {
                    sb.Append(", " + placemark.SubLocality);
                }
                if (placemark.Thoroughfare != null)
                {
                    sb.Append(", " + placemark.Thoroughfare);
                }
                if (placemark.SubThoroughfare != null)
                {
                    sb.Append(", " + placemark.SubThoroughfare);
                }

                return sb.ToString();
               
            }
            else
            {
                return "";
            }
        }


        Boolean _istapped = false;
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (_istapped)
                return;

            _istapped = true;

            await Navigation.PopModalAsync(true);

            _istapped = false;
        }
    }
}