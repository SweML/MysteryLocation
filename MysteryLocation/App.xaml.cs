
using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("Helvetica93.ttf", Alias = "Helvetica")]
[assembly: ExportFont("WalkWay_Semibold.ttf", Alias = "WalkWay")]
[assembly: ExportFont("Lato-Light.ttf", Alias = "Latow")]
[assembly: ExportFont("Lato-Black.ttf", Alias = "Latob")]
[assembly: ExportFont("AndersonGrotesk-Ultrabold.otf", Alias = "Grotesk")]
[assembly: ExportFont("CircularStd-Black.ttf", Alias = "Circular")]
namespace MysteryLocation
{
    public partial class App : Application
    {

        public static User user;
        public static APIConnection conn;
        private GPSFetcher gps;

        //  public static double ScreenWidth;
        //   public static double ScreenHeight;
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
            
            InitializeComponent();
            MainPage = new NavigationBar(user, conn);
           
            
        }


        protected override void OnStart()  // This will be executed after the app constructor.
        {
            
            gps = new GPSFetcher();
            startGPS();
            Task.Run(async() => 
            {
                List<Post> posts = await conn.getDataAsync();
                posts = GlobalFuncs.filterInvaliedPosts(posts);
                GlobalFuncs.uvm.updateListElements(posts); // Does not care about distance nor *ML
                while (!GlobalFuncs.gpsOn)
                {
                    await Task.Delay(25);
                }
                GlobalFuncs.mvm.updateListElements(posts); // Does not care about *ML only distance
                while (!GlobalFuncs.settingsActive)
                {
                    await Task.Delay(25);
                }
                GlobalFuncs.fvm.updateListElements(posts); // Cares about both *ML and distance.
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
                Console.WriteLine("New GPSFetcher onResume. If this happens we have to do something else with the fetches references to vm:s");
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
