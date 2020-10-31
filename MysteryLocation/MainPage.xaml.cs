using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            APIConnection api = new APIConnection(Content);
            defaultActivityIndicator.IsRunning = true;
            api.RefreshDataAsync();
            //defaultActivityIndicator.IsRunning = false;
        }

        async void FeedButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Feed button works.");
            
        }

        async void SettingsButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Settings button works.");
        }

        async void MarkedButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Marked button works.");
        }

        async void UnlockedButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Unlocked button works.");
        }

        async void NewButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("New button works.");

        }

        async void getLocation(object sender, EventArgs args)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Content.Text = $"{location.Latitude}, {location.Longitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Camera button works at least");
            try
            {
                await CrossMedia.Current.Initialize();

                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = CameraDevice.Rear,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });

                if (photo != null)
                    imgCam.Source = ImageSource.FromStream(() => { return photo.GetStream(); });

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }

    }

}





