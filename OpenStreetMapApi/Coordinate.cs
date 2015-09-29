using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// coordinate object manages the min max degrees
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public Coordinate( double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        /// <summary>
        /// -180 > x > 180
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set
            {
                double temp = value;
                if (temp > 180)
                {
                    temp = temp - 360;
                    Longitude = temp;
                }
                else if (temp < -180)
                {
                    temp = temp + 360;
                    Longitude = temp;
                }
                else
                {
                    longitude = temp;
                }
            }
        }
        private double longitude; // = 0;


        /// <summary>
        ///  -90 > y > 90
        /// </summary>
        public double Latitude
        {
            get { return latitude; }
            set
            {
                double temp = value;

                if (temp > 90)
                {
                    temp = temp - 180;
                    Latitude = temp;
                }
                else if (temp < -90)
                {
                    temp = temp + 180;
                    Latitude = temp;
                }
                else
                {
                    latitude = temp;
                }
            }
        }
        private double latitude; // = 0.0;
    }
}