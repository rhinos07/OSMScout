using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenStreetMapPictures {
    
    /// <summary>
    /// 
    /// </summary>
    public static class Utils
    {

        private static string osmMapPath = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static String MapCachePath 
        {
            get
            {
                if (osmMapPath.Length == 0)
                {
                    osmMapPath = Path.GetTempPath() + Path.DirectorySeparatorChar + "OsmScoutTileCache";
                }
                return osmMapPath;
            }
        }

        private static String GetWorkPath(String file, String renderer) {
        
            String workpath = MapCachePath;
            workpath += Path.DirectorySeparatorChar;
            workpath += renderer;
            workpath += Path.DirectorySeparatorChar;
            workpath += file;

            return workpath;
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoom"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static String GetZoompath(int zoom, String renderer) {
            return GetWorkPath("cache", renderer) + Path.DirectorySeparatorChar + zoom.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="zoom"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static String GetXPath(int x, int zoom, String renderer) {
            return GetZoompath(zoom, renderer) + Path.DirectorySeparatorChar + x.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static String GetYPath(String xpath, int y) {
            return xpath + Path.DirectorySeparatorChar + y.ToString() + ".png";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <param name="zoom"></param>
        /// <param name="renderer"></param>
        /// <returns></returns>
        public static string GetFilePath(int tileX, int tileY, int zoom, string renderer)
        {
            return GetYPath(GetXPath(tileX, zoom, renderer), tileY);
        }
    }
}
