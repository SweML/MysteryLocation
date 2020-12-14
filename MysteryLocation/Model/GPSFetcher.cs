using MysteryLocation.ViewModel;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Toast;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public string writePositionLocation;
        double a;
        double b;
        public bool getNewPos = true;
        public Position pos;
        public GPSFetcher()
        {

        }

        public async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;
            try
            {
                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
            }
            catch(GeolocationException g1)
            {
                Console.WriteLine("Permission error: " + g1.Message);
                
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        
                        DependencyService.Get<SnackInterface>().SnackbarShow("To use our app you have to grant us the permission to use your GPS");
                        break;
                    case Device.iOS:
                        CrossToastPopUp.Current.ShowToastMessage("To use our app you have to grant us the permission to use your GPS");
                        break;
                    default:
                        break;
                }
                try
                {
                    await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

                }
                catch (GeolocationException g2)
                {
                    Environment.Exit(0);
                }


            }
            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {

            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;
            
            setPositions(position);
            
            //Console.WriteLine(output);
        }

        private void setPositions(Position position)
        {
            currentPosition = position;
            GlobalFuncs.gpsOn = true;
            string latitude = "" + Math.Round(position.Latitude, 5);
            string longitude = "" + Math.Round(position.Longitude, 5);
         
            string writePosition = latitude + ", " + longitude;
            if (getNewPos) { 
            Task.Run(async () => {
                await WritePositionWithLocation(position);
                
            });
            }
            if (fvm != null)
            {
                fvm.Position = writePosition;
                fvm.PositionLocation = writePositionLocation;
            }
            if (mvm != null)
            {
                mvm.Position = writePosition;
                mvm.PositionLocation = writePositionLocation; 
            }

            if (uvm != null) 
            {  
                uvm.Position = writePosition;
                uvm.PositionLocation = writePositionLocation; 
            }
            if (cavm != null)
            {
                cavm.Position = writePosition;
                cavm.PositionLocation = writePositionLocation;
            }
            if(pvm != null)
            {
                pvm.Latitude = position.Latitude.ToString();
                pvm.Longitude = position.Longitude.ToString();
                pvm.Altitude = position.Altitude.ToString() + " m";
                pvm.PositionLocation = writePositionLocation;
            }

            if (vc != null)
            {
                vc.Position = writePosition;
                vc.PositionLocation = writePositionLocation;
                vc.Distance = getDistance();
                if (GlobalFuncs.mvm.tracked != null)
                {

                    a = GlobalFuncs.calcDist(GlobalFuncs.mvm.returnmypos(), GlobalFuncs.mvm.tracked.Position);
                    b = GlobalFuncs.calcDist(currentPosition, GlobalFuncs.mvm.tracked.Position);
                    if (b >= a)
                    {
                        vc.DecimalD = 0.0;
                    }
                    else
                    {
                        //TODO
                        vc.DecimalD = 1.0 - (b / a);
                    }

                }
                else
                {
                    vc.DecimalD = 0;
                }
            }
               
           

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

                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShow("Post #" + GlobalFuncs.mvm.tracked.Id + " is unlocked");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("Post #" + GlobalFuncs.mvm.tracked.Id + " is unlocked");
                            break;
                        default:
                            break;
                    }


                    Task.Run(async () => {
                        withinTwentyFive = true;
                        await GlobalFuncs.unlockTracker();
                        withinTwentyFive = false;
                    });
                  

                    //Add in unlocked
                }
                else if (distance <= 500 && !withinFive)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShow("You're now within 500m of the target");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("You're now within 500m of the target");
                            break;
                        default:
                            break;
                    }
                    withinFive = true;
                }
                else if(distance <= 1000 && !withinThousand)
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.Android:
                            DependencyService.Get<SnackInterface>().SnackbarShow("You're now within 1km of the target");
                            break;
                        case Device.iOS:
                            CrossToastPopUp.Current.ShowToastMessage("You're now within 1km of the target");
                            break;
                        default:
                            break;
                    }
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

        
           
       

        public async Task WritePositionWithLocation(Position p)
        {
            getNewPos = false;
            var placemarks = await Geocoding.GetPlacemarksAsync(p.Latitude, p.Longitude);

            var placemark = placemarks?.FirstOrDefault();


            StringBuilder sb = new StringBuilder();

            if (placemark != null)
            {
                if (placemark.CountryName != null)
                {
                    sb.Append(placemark.CountryName);
                }
                if (placemark.CountryCode != null)
                {
                    sb.Append(", " + placemark.CountryCode);
                }
                if (placemark.AdminArea != null)
                {
                    sb.Append(", " + placemark.AdminArea);
                }
                if (placemark.SubAdminArea != null)
                {
                    sb.Append(", " + placemark.SubAdminArea);
                }
                if (placemark.Locality != null)
                {
                    sb.Append(", " + placemark.Locality);
                }
                if (placemark.SubLocality != null)
                {
                    sb.Append(", " + placemark.SubLocality);
                }
                if (placemark.Thoroughfare != null)
                {
                    sb.Append(", " + placemark.Thoroughfare);
                }
                if (placemark.SubThoroughfare != null)
                {
                    sb.Append(", " + placemark.SubThoroughfare);
                }
                writePositionLocation = sb.ToString();
                

            }
            else
            {
                writePositionLocation = null;
            }
            getNewPos = true;
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
