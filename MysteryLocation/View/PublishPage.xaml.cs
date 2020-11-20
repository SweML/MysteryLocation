using MysteryLocation.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class PublishPage : ContentPage
    {
        User user;
        public PublishPage(User user)
        {
            this.user = user;
            InitializeComponent();
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
        private async void PublishButton_Clicked(object sender, EventArgs e)
        {

            Console.WriteLine("Publish button works");
            await Navigation.PopAsync(false);


        }
    }
}