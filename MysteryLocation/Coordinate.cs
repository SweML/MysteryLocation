using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation
{
    public class Coordinate
    {
        private double longitude { get; set; }
        private double latitude { get; set; }

        public Coordinate(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public String toString()
        {
            return longitude + ", " + latitude;
        }
    }
}
