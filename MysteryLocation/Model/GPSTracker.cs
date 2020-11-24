using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation.Model
{
    class GPSTracker
    {
        User user;
        public GPSTracker(User user)
        {
            this.user = user;
        }

        public async void getPostLocation()
        {
            try
            {
                var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    user.currentPos = new Coordinate(location.Longitude, location.Latitude);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
