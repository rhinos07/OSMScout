using System.Xml.Serialization;

namespace OpenSteetMapApi
{
    /// <summary>
    /// &lt;!ELEMENT tag EMPTY&gt;
    /// &lt;!ATTLIST tag  k         CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST tag  v         CDATA #REQUIRED&gt;
    /// </summary>
    [XmlRoot("tag")]
    public class Tag
    {
        /// <summary>
        /// key attribute
        /// </summary>
        [XmlAttribute("k")]
        public string Key
        {
            get;
            set;
        }


        /// <summary>
        /// value attribute
        /// </summary>
        [XmlAttribute("v")]
        public string Value
        {
            get;
            set;
        }

    }
}