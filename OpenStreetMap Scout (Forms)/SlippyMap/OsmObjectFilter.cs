using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenStreetMapScout.SlippyMap
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("osm_object_filter")]
    public class OsmObjectFilter
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("desc")]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("rule")]
        public FilterRule[] Rules { get; set; }
    }
}
