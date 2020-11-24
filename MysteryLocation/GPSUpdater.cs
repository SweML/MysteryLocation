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
        Label gpsLabel;
        System.Threading.Timer myTimer;
        static int counter;
        User user;

        public GPSUpdater(Label gpsLabel, User user)
        {
            this.gpsLabel = gpsLabel;
            this.user = user;
            counter = 0;
        }

        public void startTimer(int interval)
        {
            this.myTimer = new System.Threading.Timer((e) =>
            {
                getLocation();
                counter++;
                gpsLabel.Text += "  Counter is; " + counter.ToString();
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
                var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
                Console.WriteLine("Trying to get new position");

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    gpsLabel.Text = $"{location.Latitude}, {location.Longitude}";
                    user.setPosition(new Coordinate(location.Longitude, location.Latitude));
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
