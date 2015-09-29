using System;
using System.Xml.Serialization;
using System.Globalization;

namespace OpenSteetMapApi
{
    /// <summary>
    ///  &lt;!ELEMENT osm ((node|relation|way)*)&gt;
    ///  &lt;!ATTLIST osm version   (0.5) #REQUIRED&gt;
    ///  &lt;ATTLIST osm generator CDATA #REQUIRED&gt;
    /// </summary>
    [XmlRoot("osm")]
    public class OsmTree
    {
        /// <summary>
        /// version attribute
        /// </summary>
        [XmlAttribute("version")]
        public string Version   { get; set; }

        /// <summary>
        /// generator attribute
        /// </summary>
        [XmlAttribute("generator")]
        public string Generator { get; set; }

        /// <summary>
        /// node list
        /// </summary>
        [XmlElement("node")]
        public Node[] Nodes { get; set; }

        /// <summary>
        /// ways list
        /// </summary>
        [XmlElement("way")]
        public Way[] Ways { get; set; }

        /// <summary>
        /// relation list
        /// </summary>
        [XmlElement("relation")]
        public Relation[] Relations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Descripton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void Sort()
        {
            Array.Sort(Nodes);
            Array.Sort(Ways);
        }
    }
}