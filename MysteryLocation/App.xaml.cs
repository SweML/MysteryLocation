
using MysteryLocation.Model;
using System;
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

        
        
         
      //  public static double ScreenWidth;
     //   public static double ScreenHeight;
        public App()
        {
            APIConnection conn = new APIConnection();
            User user = new User(true, 329, conn);
            GPSUpdater gps = new GPSUpdater(user);
         
            // Starts the gps.
            gps.startTimer(10);
            InitializeComponent();
            MainPage = new NavigationBar(user, conn);
           
            
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
