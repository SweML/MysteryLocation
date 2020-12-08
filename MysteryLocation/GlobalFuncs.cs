using MysteryLocation.Model;
using MysteryLocation.ViewModel;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MysteryLocation
{
    public class GlobalFuncs
    {
        public static FeedViewModel fvm;

        public static MarkedViewModel mvm;

        public static UnlockedViewModel uvm;

        public static CategoryViewModel svm;

        public static bool gpsOn = false;

        public static bool settingsActive = false;
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

            return d;
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
                    if (x.getCoordinate() != null && markerCheck == "*ML")
                        temp.Add(x);
                }
            }
            return temp;
        }

        public static async Task unlockTracker()
        {
            if (App.user != null && App.conn != null && App.user.tracking != null)
            {
                UnlockedPosts attachment = await App.conn.getPostAttachmentAsync(App.user.tracking.getId());
                uvm.addUnlockedPost(App.user.tracking, attachment);
                App.user.tracking = null;
            }
        }
    }
}
