using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MysteryLocation
{
    class GPSUpdater // Denna klassen kan vi nog förbättra med att göra den till en egen tråd.
    {
        System.Threading.Timer myTimer;
        static int counter;
        User user;
        bool firstUpdate = true;
        Stopwatch stop = new Stopwatch();

        public GPSUpdater(User user)
        {
            this.user = user;
            counter = 0;
        }

        public void startTimer(int interval)
        {
           
               this.myTimer = new System.Threading.Timer((e) =>
                {
                    if (!firstUpdate)
                         getLocationBest();
                    else
                        getLocationMedium();
                    counter++;
                    Console.WriteLine("Executing timer block in GPSUpdater " + counter);
                }, null,
                TimeSpan.FromSeconds(0),
                TimeSpan.FromSeconds(interval));
         
        }

        public void stopTimer()
        {
            myTimer = null;
        }

        async void getLocationBest()
        {
            try
            {
                //var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync(); //Ger en cachad version
                stop.Start();
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(new Xamarin.Essentials.GeolocationRequest
                {
                    DesiredAccuracy = Xamarin.Essentials.GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(9)
                });
                Console.WriteLine("Trying to get new position");

                if (location != null)
                {
                    user.setPosition(new Coordinate(location.Longitude, location.Latitude));
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                stop.Stop();
                Console.WriteLine("The time it takes for best is " + stop.Elapsed);
                stop.Reset();
            }
            catch (Exception ex)
            {

            }
        }

        async void getLocationMedium()
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync(); //Ger en cachad version
                stop.Start();
               /*var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(new Xamarin.Essentials.GeolocationRequest
                {
                    DesiredAccuracy = Xamarin.Essentials.GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(9)
                });*/
                Console.WriteLine("Trying to get new position");

                if (location != null)
                {
                    user.setPosition(new Coordinate(location.Longitude, location.Latitude));
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    firstUpdate = false;
                }
                stop.Stop();
                Console.WriteLine("The time it takes for medium is " + stop.Elapsed);
                stop.Reset();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
