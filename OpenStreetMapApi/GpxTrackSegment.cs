

using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [XmlRoot("trkseg")]
    public class GpxTrackSegment
    {

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("trkpt")]
        public GpxTrackPoint[] TrackPoints;

    }
}