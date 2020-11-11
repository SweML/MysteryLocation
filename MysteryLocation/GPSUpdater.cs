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

        public GPSUpdater(Label gpsLabel)
        {
            this.gpsLabel = gpsLabel;
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

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    gpsLabel.Text = $"{location.Latitude}, {location.Longitude}";
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
