using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using Xamarin.Forms;

namespace MysteryLocation
{
    class GPSUpdater
    {
        System.Threading.Timer myTimer;
        static int counter;
        User user;

        public GPSUpdater(User user)
        {
            this.user = user;
            counter = 0;
        }

        public void startTimer(int interval)
        {
            this.myTimer = new System.Threading.Timer((e) =>
            {
                getLocation();
                counter++;
            }, null,
            TimeSpan.FromSeconds(0),
            TimeSpan.FromSeconds(interval));
        }

        public void stopTimer()
        {
            myTimer = null;
        }

        async void getLocation()
        {
            try
            {
                 var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync(); //Ger en cachad version
               /* var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(new Xamarin.Essentials.GeolocationRequest
                {
                    DesiredAccuracy = Xamarin.Essentials.GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(9)
                });*/
                Console.WriteLine("Trying to get new position");

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    user.setPosition(new Coordinate(location.Longitude, location.Latitude));
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
