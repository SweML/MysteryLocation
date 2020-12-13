using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using Plugin.Connectivity;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation
{
    public class GlobalFuncs
    {
        public static FeedViewModel fvm;

        public static MarkedViewModel mvm;

        public static UnlockedViewModel uvm;

        public static ViewCompass vc;

        public static CategoryViewModel svm;

        public const string marker = "*MT";

        public static bool gpsOn = false;

        public static bool settingsActive = false;
        private static int prevUnlock = -1;


        public static double calcDist(Position p1, Position p2)
        {
            Double R = 6371e3; // metres
            Double φ1 = p1.Latitude * Math.PI / 180; // φ, λ in radians
            Double φ2 = p2.Latitude * Math.PI / 180;
            Double Δφ = (p2.Latitude - p1.Latitude) * Math.PI / 180;
            Double Δλ = (p2.Longitude - p1.Longitude) * Math.PI / 180;

            Double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                     Math.Cos(φ1) * Math.Cos(φ2) *
                     Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            Double d = R * c; // in metres
                              // For testing

            return Math.Round(d, 1);
        }

        /*
         *  Filter out posts that do not have a Coordinate nor a marker. *ML
         */
        public static List<Post> filterInvaliedPosts(List<Post> list)
        {
            List<Post> temp = new List<Post>();
            string markerCheck;
            foreach(Post x in list)
            {
                markerCheck = x.getSubject();
                if(markerCheck.Length > 4)
                {
                    markerCheck = markerCheck.Substring(x.getSubject().Length - 3);
                    if (x.getCoordinate() != null && markerCheck == marker)
                        temp.Add(x);
                }
            }
            return temp;
        }

        public static async Task unlockTracker()
        {
            
            if (App.user != null && App.conn != null && GlobalFuncs.mvm.tracked != null && prevUnlock != GlobalFuncs.mvm.tracked.Id)
            {
                
                await Task.Run(async () => {
                   
                    prevUnlock = GlobalFuncs.mvm.tracked.Id;
                    int temp = GlobalFuncs.mvm.tracked.Id;
                    if (!CrossConnectivity.Current.IsConnected)
                    {

                        DependencyService.Get<SnackInterface>().SnackbarShowIndefininte("Internet is not available");

                        while (!CrossConnectivity.Current.IsConnected)
                        {
                            await Task.Delay(100);
                        }
                        DependencyService.Get<SnackInterface>().SnackbarShow("Internet connection has been established ");
                    }
                    

                    UnlockedPosts attachment = await App.conn.getPostAttachmentAsync(temp);
                    uvm.addUnlockedPost(mvm.RemovePost(temp), attachment);
                    GlobalFuncs.mvm.tracked = null;

                });


                //DependencyService.Get<SnackInterface>().SnackbarShow("Post cannot be unlocked - no image available");


            }


        }

        public static void addTracker(int observationId)
        {
            Console.WriteLine("Entering Add Tracker");
            foreach (PostListElement x in mvm.Items)
            {
                if (x.Id == observationId)
                {
                    mvm.tracked = x;
                    App.user.tracker = observationId;
                    mvm.tracked.Color = "#1f9f64";
                    Console.WriteLine(x.Position.Latitude + x.Position.Longitude);
                    //marked.Remove(x);
                }
            }
        }

     

    }
}
