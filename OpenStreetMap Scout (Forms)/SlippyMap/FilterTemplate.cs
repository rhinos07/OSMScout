using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenStreetMapScout.SlippyMap
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("template")]
    public class FilterTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("type")]
        public string FilterType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
