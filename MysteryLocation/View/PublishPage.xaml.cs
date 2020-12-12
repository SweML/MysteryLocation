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
        byte[] bytes;
        bool published;
        public PublishPage()
        {
            this.user = App.user;
            this.conn = App.conn;
            InitializeComponent();
            entryBody.Text = "";
        }
        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = CameraDevice.Rear,
                    Directory = "Xamarin",
                    SaveToAlbum = true,
                    PhotoSize = PhotoSize.Small
                });

                if (photo != null)
                {

                    bytes = streamToArray(photo.GetStream());

                    imgCam.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }
        private async void BrowseButton_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            try
            {
                
                bytes = streamToArray(await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync());
                if (bytes.Length != 0 || bytes == null)
                {
                    imgCam.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                }
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Nullref exception, you didn't choose anything in browse");
            }
            (sender as Button).IsEnabled = true;
        }
        private async void PublishButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Publish button is pushed");
            try
            {
                if (entrySubject.SelectedIndex > -1)
                {
                    // Först publicera vanlig createPost
                    spinOn();

                    ///hadi 
                    published = false;
                    startTiming();
                    await Task.Delay(10000);   /// only to test  the  7 seconds delay
                    //hadi

                    PostAttachment attach = null;
                    if (bytes != null)
                    {
                        try
                        {
                            attach = new PostAttachment(0, bytes);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                    string title = entrySubject.Items[entrySubject.SelectedIndex];
                    if (GPSFetcher.currentPosition != null && attach != null && attach.description.Length < 3000000)
                        title += "*ML";
                    createPost pubPost = new createPost(title, entryBody.Text, GPSFetcher.currentPosition);
                    int status = -2;
                    status = await conn.testPublishPosts(pubPost);
                    Console.WriteLine("post was much success");
                    // Sen skapa PostAttachment och publicera den
                    if (status >= 0 && (bytes.Length != 0 || bytes != null))
                    {
                        attach.obsID = status;
                        bool success = await conn.publishAttachment(attach);
                        if (success)
                        {
                            imgCam.Source = null;
                            entryBody.Text = "";

                            ////
                            published = true;
                            spinOff();
                            ////
                        }
                        else // Publication of attachment failed.
                        {
                            spinOff();
                        }
                    }
                    else // publish failed.
                    {
                        Console.WriteLine("Adding attachment failed. Status < 0");
                        spinOff();
                    }



                }
                else // Please choose a category for your post.
                {

                }
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine("nullreference in publishButton: " + error);
            }
            await Navigation.PopModalAsync(true);
        }

        private byte[] streamToArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async void ClosingPP(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }





        /// hadis  timer  
        public void startTiming()
        {
            Device.StartTimer(new TimeSpan(0, 0, 7), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!published)
                    {
                        // labetoShow.IsVisible = true;
                        DependencyService.Get<SnackInterface>().SnackbarShow("Publishing is taking longer than usual. Please wait ...");
                    }


                });
                return false; // runs again, or false to stop
            });
        }
        /// hadis Timer 



        public async void spin()
        {
            defaultActivityIndicator.IsRunning = true;
            await Task.Delay(1000);
            defaultActivityIndicator.IsRunning = false;
        }

        public void spinOn()
        {
            defaultActivityIndicator.IsRunning = true;
        }

        public void spinOff()
        {
            defaultActivityIndicator.IsRunning = false;
        }
    }
}