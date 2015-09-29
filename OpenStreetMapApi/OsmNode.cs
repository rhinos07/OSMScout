using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// &lt;!ELEMENT node (tag*)&gt;
    /// &lt;!ATTLIST node id        CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST node lat       CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST node lon       CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST node visible   CDATA #IMPLIED&gt;
    /// &lt;!ATTLIST node user      CDATA #IMPLIED&gt;
    /// &lt;!ATTLIST node timestamp CDATA #IMPLIED&gt;
    /// 
    /// </summary>
    [XmlRoot("node")]
    public class Node : IComparable, ICloneable
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName ="id")]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "double", AttributeName = "lat")]
        public double Lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "double", AttributeName = "lon")]
        public double Lon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("visible")]
        public string Visible { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("user")]
        public string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("timestamp")]
        public string Timestamp { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [XmlElement("tag")]
        public Tag[] Tags;

        /// <summary>
        /// text to draw on the map
        /// </summary>
        public string DrawText { get; set; }

        /// <summary>
        /// image to draw on the map
        /// </summary>
        public string DrawIcon { get; set; }

        #region IComparable Member

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Node)
            {
                Node other = (Node)obj;
                return this.Id.CompareTo(other.Id);
            }
            else
            {
                throw new ArgumentException("Object is not a Node");
            }
        }

        #endregion

        #region ICloneable Member

        /// <summary>
        /// clone function, creates a new object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return ((Node)MemberwiseClone());
        }

        #endregion
    }
}