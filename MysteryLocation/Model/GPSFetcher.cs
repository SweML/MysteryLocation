using MysteryLocation.ViewModel;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation.Model
{
    public class GPSFetcher
    {
        Boolean withinThousand = false;
        Boolean withinFive = false;
        Boolean withinTwentyFive = false;
        public static FeedViewModel fvm;
        public static MarkedViewModel mvm;
        public static UnlockedViewModel uvm;
        public static CategoryViewModel cavm;
        public static PublishViewModel pvm;
        public static Position currentPosition;
        public static ViewCompass vc;

        public GPSFetcher()
        {

        }

        public async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {

            //If updating the UI, ensure you invoke on main thread
            /*var position = e.Position;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";*/
            setPositions(e.Position);
            
            //Console.WriteLine(output);
        }

        private void setPositions(Position position)
        {
            currentPosition = position;
            GlobalFuncs.gpsOn = true;
            string latitude = "" + Math.Round(position.Latitude, 5);
            string longitude = "" + Math.Round(position.Longitude, 5);
            /*if (latitude.Length > 8)
                latitude = latitude.Substring(0, 8);
            if (longitude.Length > 8)
                longitude = longitude.Substring(0, 8);*/
            string writePosition = latitude + " , " + longitude;
            // string writePositionLocation = await WritePositionWithLocation(position) + latitude + ", " + longitude;
            if (fvm != null)
            {
                fvm.Position = writePosition;
                fvm.PositionLocation = writePosition;
            }
            if (mvm != null)
            {
                mvm.Position = writePosition;
                mvm.PositionLocation = writePosition;
            }

            if (uvm != null) 
            {  
                uvm.Position = writePosition;
                uvm.PositionLocation = writePosition;
            }
            if (cavm != null)
            {
                cavm.Position = writePosition;
                cavm.PositionLocation = writePosition;
            }
            if (vc != null)
            {
                vc.Position = writePosition;
                vc.PositionLocation = writePosition;
                vc.Distance = getDistance();
            }
               
            

            if (cavm != null) cavm.PositionLocation = writePosition;

            fvm.RecalculateDistance();
            mvm.RecalculateDistance();
            check(position);
        }

        private void check(Position p)
        {
            if (GlobalFuncs.mvm.tracked != null) {
                double distance = GlobalFuncs.calcDist(p, GlobalFuncs.mvm.tracked.Position);
                if (distance >= 1000 || distance >= 500)
                {
                    withinThousand = false;
                    withinFive = false;
                }
                else if (distance <= 25 && !withinTwentyFive)
                {
                    DependencyService.Get<SnackInterface>().SnackbarShow("Post nr: " + GlobalFuncs.mvm.tracked.Id + " is unlocked");
                    //Stop tracking
                    //Remove from mList
                    Task.Run(async () => {
                        withinTwentyFive = true;
                        await GlobalFuncs.unlockTracker();
                        withinTwentyFive = false;
                    });
                    // PostListElement refElement = GlobalFuncs.mvm.RemovePost(GlobalFuncs.mvm.tracked.Id);

                    //Add in unlocked
                }
                else if (distance <= 500 && !withinFive)
                {
                    DependencyService.Get<SnackInterface>().SnackbarShow("You are now within 500m of target");
                    withinFive = true;
                }
                else if(distance <= 1000 && !withinThousand)
                {
                    DependencyService.Get<SnackInterface>().SnackbarShow("You are now within 1 km of target");
                    withinThousand = true;
                }
                
                
            }   
        }

        private string getDistance()
        {
            if(GlobalFuncs.mvm.tracked != null)
            {
                return GlobalFuncs.mvm.tracked.Dist;
            }
            else
            {
                return null;
            }
            
        }

        private async Task<String> WritePositionWithLocation(Position p)
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(p.Latitude, p.Longitude);

            var placemark = placemarks?.FirstOrDefault();
            
            if (placemark != null)
            {
                if (placemark.Locality != null) 
                 { return placemark.Locality + ", " + placemark.CountryName + " - "; }
                 else if (placemark.Locality == null && placemark.SubLocality != null) 
                 { return placemark.SubLocality + ", " + placemark.CountryName + " - "; }
                else if (placemark.Locality == null && placemark.SubLocality == null && placemark.SubAdminArea != null && placemark.AdminArea != null)
                { return placemark.AdminArea + ", " + placemark.SubAdminArea + " - "; }
                else if (placemark.Locality == null && placemark.SubLocality == null && placemark.Thoroughfare != null && placemark.SubThoroughfare != null)
                { return placemark.Thoroughfare + " " + placemark.SubThoroughfare + ", " + placemark.CountryName + " - "; }
                else { return ""; } 
            }
            else
            {
                return "";
            }
        }

        private void PositionError(object sender, PositionErrorEventArgs e)
        {
            Debug.WriteLine(e.Error);
            //Handle event here for errors
        }

        public async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StopListeningAsync();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }
    }
}
