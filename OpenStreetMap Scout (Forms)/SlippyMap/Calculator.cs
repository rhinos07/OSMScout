using System;
using System.Collections.Generic;
using System.Text;
using OpenSteetMapApi;

namespace OpenStreetMapPictures.TilesDownload {
    
    /// <summary>
    ///  http://wiki.openstreetmap.org/index.php/Slippy_map_tilenames
    /// </summary>
    public static class Calculator {

        /// <summary>
        /// calcutates the map tile number of tile that shows coordinate
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="zoom"></param>
        /// <param name="xtile"></param>
        /// <param name="ytile"></param>
        public static void GetTiles(Coordinate coord, int zoom, ref int xtile, ref int ytile)
        {
            double doublex = 0;
            double doubleY = 0;

            Calculator.GetTilesDoubleValue(coord, zoom, ref doublex, ref doubleY);

            xtile = (int)Math.Floor(doublex);
            ytile = (int)Math.Floor(doubleY);
        }

        /// <summary>
        /// calcutates the map tile number in double values of tile that shows coordinate
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="zoom"></param>
        /// <param name="xtile"></param>
        /// <param name="ytile"></param>
        public static void GetTilesDoubleValue(Coordinate coord, int zoom, ref double xtile, ref double ytile)
        {
            xtile = (coord.Longitude + 180) / 360 * (1 << zoom);
            ytile = (1 - Math.Log(Math.Tan(coord.Latitude * Math.PI / 180) + 1 / Math.Cos(coord.Latitude * Math.PI / 180)) / Math.PI) / 2 * (1 << zoom);
        }

        /// <summary>
        /// calcuates the corner coordinate of a map tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="zoom"></param>
        public static Coordinate GetLonLat(int x, int y, int zoom )
        {
            Coordinate coord = new Coordinate(0,0);
            int n = GetCountTiles(zoom);
            double relY = (double)y / (double)n;
            coord.Latitude = GetMercatorToLat(Math.PI * (1 - 2 * relY));
            coord.Longitude = -180.0 + 360.0 * x / n;
            return coord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="zoom"></param>
        public static Coordinate GetLonLat(double x, double y, int zoom)
        {
            Coordinate coord = new Coordinate(0,0);
            int n = GetCountTiles(zoom);
            double relY = (double)y / (double)n;
            coord.Latitude = GetMercatorToLat(Math.PI * (1 - 2 * relY));
            coord.Longitude = -180.0 + 360.0 * x / n;
            return coord;
        }

        /// <summary>
        /// get the count of the map tiles at this zoom level
        /// </summary>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static int GetCountTiles(int zoom)
        {
            return (int)Math.Pow(2.0, zoom);   
        }

        /// <summary>
        /// http://www.demo2s.com/Code/CSharp/Language-Basics/ConvertingRadianstoDegrees.htm 
        /// </summary>
        /// <param name="mercatory"></param>
        /// <returns></returns>
        private static double GetMercatorToLat(double mercatory) {
            return  (180.0 / Math.PI) * ( Math.Atan(Math.Sinh(mercatory)));
        }
    }
}
