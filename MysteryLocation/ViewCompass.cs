﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MysteryLocation
{

    public class ViewCompass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        double beta;
        double y;
        double x;

        public ViewCompass()
        {

        }
        string headingDisplay;
        public string HeadingDisplay
        {
            get => headingDisplay;
            set => SetProperty(ref headingDisplay, value);
        }

        double heading = 0;

        public double Heading
        {
            get => heading;
            set => SetProperty(ref heading, value);
        }

        public double Beta
        {
            get => beta;
            set => SetProperty(ref beta, value);
        }

        public void Stop()
        {
            if (!Compass.IsMonitoring)
                return;

            Compass.ReadingChanged -= Compass_ReadingChanged;
            Compass.Stop();
        }

        public void Start()
        {
            if (Compass.IsMonitoring)
                return;

            Compass.ReadingChanged += Compass_ReadingChanged;
            Compass.Start(SensorSpeed.UI);

        }

        private async void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {

            Heading = e.Reading.HeadingMagneticNorth * -1;
            int displayHeading = (int)Heading;
            displayHeading *= -1;
            HeadingDisplay = $" {displayHeading.ToString()}°";

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                Beta = Heading + Bearing(location.Latitude, location.Longitude, 21.422512, 39.826198);
               
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        //Bäringsvinkel - Metodens sista 2 parametrar ska matas in med koordinater av ett valt inlägg
        public double Bearing(double myLA, double myLO, double destLatitude, double destLongitude)
        {
            double latA = DtR(myLA);
            double longA = DtR(myLO);
            double latB = DtR(destLatitude);
            double longB = DtR(destLongitude);
            double dL = longB - longA;

            x = Math.Cos(latB) * Math.Sin(dL);
            y = Math.Cos(latA) * Math.Sin(latB) - Math.Sin(latA) * Math.Cos(latB) * Math.Cos(dL);

            beta = Math.Atan2(x, y);
            beta = beta * 180;
            beta = beta / Math.PI;
            int aBeta = (int)Math.Round(beta);

            return aBeta;

        }
        //Degrees to Radians
        public static double DtR(double angle)
        {
            return angle * Math.PI / 180.0d;
        }

        //Hjälpmetoder
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}