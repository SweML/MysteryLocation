using MysteryLocation.Model;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Toast;
using System;
using System.IO;
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
        Boolean _isTapped = false;
        public PublishPage()
        {
            this.user = App.user;
            this.conn = App.conn;
            InitializeComponent();
            entryBody.Text = "";
            this.BindingContext = GPSFetcher.pvm;
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
                
            }
            (sender as Button).IsEnabled = true;
        }

        

        private async void PublishButton_Clicked(object sender, EventArgs e)
        {
         
            if (_isTapped)
                return;

            _isTapped = true;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (entrySubject.SelectedIndex > -1)
                    {
                        spinOn();
                        published = false;
                        startTiming();

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
                            title += GlobalFuncs.marker;
                        createPost pubPost = new createPost(title, entryBody.Text, GPSFetcher.currentPosition);
                        int status = -2;
                        status = await conn.testPublishPosts(pubPost);

                        // Sen skapa PostAttachment och publicera den
                        if (status >= 0 && bytes != null && attach != null)
                        {
                            attach.obsID = status;
                            bool success = await conn.publishAttachment(attach);
                            if (success)
                            {
                                imgCam.Source = null;
                                entryBody.Text = "";

                                published = true;
                                spinOff();
                            }
                            else // Publication of attachment failed.
                            {
                                spinOff();
                                switch (Device.RuntimePlatform)
                                {
                                    case Device.Android:
                                        DependencyService.Get<SnackInterface>().SnackbarShow("Publish failed");
                                        break;
                                    case Device.iOS:
                                        CrossToastPopUp.Current.ShowToastMessage("Publish failed");
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }
                        else // publish failed.
                        {
                            switch (Device.RuntimePlatform)
                            {
                                case Device.Android:
                                    DependencyService.Get<SnackInterface>().SnackbarShow("Initial publish successful - no image attached therefore not displayed");
                                    break;
                                case Device.iOS:
                                    CrossToastPopUp.Current.ShowToastMessage("Initial publish successful - no image attached therefore not displayed");
                                    break;
                                default:
                                    break;
                            }
                            spinOff();
                        }



                    }
                    else // Please choose a category for your post.
                    {
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                DependencyService.Get<SnackInterface>().SnackbarShow("Please choose a category before publishing");
                                break;
                            case Device.iOS:
                                CrossToastPopUp.Current.ShowToastMessage("Please choose a category before publishing");
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (NullReferenceException error)
                {
                }




                if (published)
                {
                    await Navigation.PopModalAsync(true);
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShow("The post was successfully published");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("The post was successfully published");
                            break;
                        default:
                            break;
                    }

                }
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        DependencyService.Get<SnackInterface>().SnackbarShow("Internet is not available");
                        break;
                    case Device.iOS:
                        CrossToastPopUp.Current.ShowToastMessage("Internet is not available");
                        break;
                    default:
                        break;
                }
            }
          
            

            _isTapped = false;
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
            if (_isTapped)
                return;

            _isTapped = true;

            GPSFetcher.pvm.stopTimer();
            await Navigation.PopModalAsync(true);

            _isTapped = false;
        }





        public void startTiming()
        {
            Device.StartTimer(new TimeSpan(0, 0, 7), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!published)
                    {
                        // labetoShow.IsVisible = true;
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                DependencyService.Get<SnackInterface>().SnackbarShow("Publishing is taking longer than usual, please wait ...");
                                break;
                            case Device.iOS:
                                CrossToastPopUp.Current.ShowToastMessage("Publishing is taking longer than usual, please wait ...");
                                break;
                            default:
                                break;
                        }
                    }

                });
                return false; // runs again, or false to stop
            });
        }



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

        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                    GPSFetcher.pvm.stopTimer();
                    base.OnBackButtonPressed();

                if (_isTapped)
                    return;

                _isTapped = true;
                await Navigation.PopModalAsync();

                _isTapped = false;
                
            });

            return true;
        }
    }
}