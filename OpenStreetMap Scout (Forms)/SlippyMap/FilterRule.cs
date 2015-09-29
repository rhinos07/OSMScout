using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OpenStreetMapScout.SlippyMap
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("rule")]
    public class FilterRule
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("target")]
        public string Target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("selector")]
        public string Selector { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("template")]
        public FilterTemplate[] Templates;
    }
}
