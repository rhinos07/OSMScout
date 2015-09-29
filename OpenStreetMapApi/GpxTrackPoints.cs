

using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [XmlRoot("trkpt")]
    public class GpxTrackPoint
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType="double", AttributeName = "lat")]
        public double Lat
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "double", AttributeName = "lon")]
        public double Lon
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("desc")]
        public string Description
        {
            get;
            set;
        }
    }
}