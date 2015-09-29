using System;
using System.Xml.Serialization;
using System.Globalization;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("gpx")]
    public class GpxTree
    {
        /// <summary>
        /// version attribute
        /// </summary>
        [XmlAttribute("version")]
        public string version;

        /// <summary>
        /// generator attribute
        /// </summary>
        [XmlAttribute("creator")]
        public string creator;


        /// <summary>
        /// node list
        /// </summary>
        [XmlElement("trk")]
        public GpxTrack[] tracks  { get; set; }


    }
}