
using MysteryLocation.Model;
using Plugin.Connectivity;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: ExportFont("Lato-Light.ttf", Alias = "Latow")]
[assembly: ExportFont("Lato-Black.ttf", Alias = "Latob")]
[assembly: ExportFont("AndersonGrotesk-Ultrabold.otf", Alias = "Grotesk")]
[assembly: ExportFont("CircularStd-Black.ttf", Alias = "Circular")]
[assembly: ExportFont("CircularStd-Book.ttf", Alias = "Circularbook")]
[assembly: ExportFont("coolvetica.compressed-regular.ttf", Alias = "coolvetica")]

namespace MysteryLocation
{
    public partial class App : Application
    {

        public static User user;
        public static APIConnection conn;
        private GPSFetcher gps;

        public App() // App starts here
        {
            
            conn = new APIConnection();

            // Instansiate the user object. The user object will try to read previous settings.
            user = new User(conn);

            // Initialize the viewmodels
            GlobalFuncs.fvm = new ViewModel.FeedViewModel(null);
            GlobalFuncs.mvm = new ViewModel.MarkedViewModel(null);
            GlobalFuncs.uvm = new ViewModel.UnlockedViewModel(null);
            GlobalFuncs.svm = new ViewModel.CategoryViewModel();
            GPSFetcher.pvm = new ViewModel.PublishViewModel();


            MainPage = new NavigationBar(user, conn);


        }


        protected override void OnStart()  // This will be executed after the app constructor.
        {
            
            gps = new GPSFetcher();
            startGPS();
            Task.Run(async() => 
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShowIndefininte("Internet is not available");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("Internet is not available");
                            break;
                        default:
                            break;
                    }

                    while (!CrossConnectivity.Current.IsConnected)
                    {
                        await Task.Delay(100);
                    }

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShow("Internet connection has been established");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("Internet connection has been established");
                            break;
                        default:
                            break;
                    }
                }

                
                List<Post> posts = await conn.getDataAsync();
                posts = GlobalFuncs.filterInvaliedPosts(posts);
                // Does not care about distance nor *ML
               while (!GlobalFuncs.gpsOn || !GlobalFuncs.settingsActive)
                {
                   await Task.Delay(25);
               }
               GlobalFuncs.fvm.updateListElements(posts);
               GlobalFuncs.mvm.updateListElements(posts); // Does not care about *ML only distance
               GlobalFuncs.uvm.updateListElements(posts);
                // Cares about both *ML and distance
            });

        }

        protected override void OnSleep()
        {
            stopGPS();
            user.SaveUser();
        }

        protected override void OnResume()
        {
            if (gps == null) { 
                gps = new GPSFetcher();
            }
            startGPS();
        }

        

        public  async void startGPS()
        {
            await gps.StartListening();
        }

        public async void stopGPS()
        {
            await gps.StopListening();
        }
    }
}
