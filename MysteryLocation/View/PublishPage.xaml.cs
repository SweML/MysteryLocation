using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
        APIConnection conn;
        Stream imgSource;
        public PublishPage(User user, APIConnection conn)
        {
            this.user = user;
            this.conn = conn;
            imgSource = null;
            InitializeComponent();
            currentGPS.BindingContext = user;
        }
        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            spin();
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
                {
                    imgSource = photo.GetStream();
                    imgCam.Source = ImageSource.FromStream(() => { return photo.GetStream(); });  
                }
               // Console.WriteLine(imgCam.Source.ToString() + "TestingCam"); //Xamarin.Forms.StreamImageSource

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
        private async void PublishButton_Clicked(object sender, EventArgs e)
        {
            spin();
            Console.WriteLine("Publish button works");
            if(entrySubject.Text.Length > 0 && entryBody.Text.Length > 0 && imgSource != null)
            {
                // Först publicera vanlig createPost
                createPost pubPost = new createPost(entrySubject.Text, entryBody.Text, user.currentPos);
                int status = -2;
                status = await conn.testPublishPosts(pubPost);
                // Sen skapa PostAttachment och publicera den
                if(status >= 0)
                {
                    PostAttachment attach = new PostAttachment(status, imgSource);
                    conn.publishAttachment(attach);
                    Console.WriteLine(status + " should be 321 and id should be 321 as well");

                }
                else
                {
                    Console.WriteLine("Adding attachment failed. Status < 0");
                }


                
            }
            await Navigation.PopAsync(false);
        }


        public async void spin()
        {
            defaultActivityIndicator.IsRunning = true;
            await Task.Delay(1000);
            defaultActivityIndicator.IsRunning = false;
        }


    }
}