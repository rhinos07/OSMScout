

using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// a gpx track 
    /// </summary>
    [XmlRoot("trk")]
    public class GpxTrack
    {

        /// <summary>
        /// a segment of a track
        /// </summary>
        [XmlElement("trkseg")]
        public GpxTrackSegment[] TrackSegments;

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// comment
        /// </summary>
        [XmlAttribute("cmt")]
        public string Comment
        {
            get;
            set;
        }
            

    }
}