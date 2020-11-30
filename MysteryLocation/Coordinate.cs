using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation
{
    public class Coordinate
    {
        public double longitude { get; set; }
        public double latitude { get; set; }

        public Coordinate(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public String toString()
        {
            return longitude + ", " + latitude;
        }
        /** Method to calculate the distance between two decimal coordinates
         *  Does not account for the earth's ellipsoidal shape meaning that we will have errors.
         */
        public Double getDistance(Coordinate other)
        {
             Double R = 6371e3; // metres
             Double φ1 = this.latitude * Math.PI / 180; // φ, λ in radians
             Double φ2 = other.latitude * Math.PI / 180;
             Double Δφ = (other.latitude - this.latitude) * Math.PI / 180;
             Double Δλ = (other.longitude - this.longitude) * Math.PI / 180;

             Double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                      Math.Cos(φ1) * Math.Cos(φ2) *
                      Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
             Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

             Double d = R * c; // in metres
                               // For testing
           
            return d;
        }
    }
}
