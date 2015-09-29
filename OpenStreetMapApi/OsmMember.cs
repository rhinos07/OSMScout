using System.Xml.Serialization;

namespace OpenSteetMapApi
{
    /// <summary>
    /// &lt;!ELEMENT member EMPTY&gt;
    /// &lt;!ATTLIST member type (way|node|relation) #REQUIRED&gt;
    /// &lt;!ATTLIST member ref  CDATA  #REQUIRED&gt;
    /// &lt;!ATTLIST member role CDATA  #IMPLIED&gt;
    /// </summary>
    [XmlRoot("member")]
    public class Member
    {
        /// <summary>
        /// type attribute
        /// </summary>
        [XmlAttribute("type")]
        public string type;

        /// <summary>
        /// reference attribute
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "ref")]
        public int reference;

        /// <summary>
        /// role attribute
        /// </summary>
        [XmlAttribute("role")]
        public string role;
    }
}