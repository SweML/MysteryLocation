
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
namespace MysteryLocation
{
    public partial class App : Application
    {

        public static User user;
        public static APIConnection conn;
        private GPSFetcher gps;

        //  public static double ScreenWidth;
        //   public static double ScreenHeight;
        public App()
        {
            conn = new APIConnection();
            user = new User(true, 329, conn);
            GlobalFuncs.fvm = new ViewModel.FeedViewModel(null);
            GlobalFuncs.mvm = new ViewModel.MarkedViewModel(null);
            GlobalFuncs.uvm = new ViewModel.UnlockedViewModel(null);
            GlobalFuncs.svm = new ViewModel.CategoryViewModel();
            //GPSUpdater gps = new GPSUpdater(user);

            // Starts the gps.
            //gps.startTimer(10);
            InitializeComponent();
            MainPage = new NavigationBar(user, conn);
           
            
        }


        protected override void OnStart()
        {
            
            gps = new GPSFetcher();
            startGPS();
            user.ReadUser();
            Task.Run(async() => 
            {
                List<Post> posts = await conn.getDataAsync();
                posts = GlobalFuncs.filterInvaliedPosts(posts);
                GlobalFuncs.uvm.updateListElements(posts);
                while (!GlobalFuncs.gpsOn)
                {
                    await Task.Delay(25);
                }
                GlobalFuncs.mvm.updateListElements(posts);
                while (!GlobalFuncs.settingsActive)
                {
                    await Task.Delay(25);
                }
                GlobalFuncs.fvm.updateListElements(posts);
                
                
            });

            //Task.Run(async() =>
            //{
               // await Task.Delay(20000);
                //Console.WriteLine("Test----------------------------------------------------------------------------------------------------------");
                //await GlobalFuncs.unlockTracker();
           // });
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
