using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// &lt;!ELEMENT way (tag*,nd,tag*,nd,(tag|nd)*)&gt;
    /// &lt;!ATTLIST way id        CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST way visible   CDATA #IMPLIED&gt;
    /// &lt;!ATTLIST way user      CDATA #IMPLIED&gt;
    /// &lt;!ATTLIST way timestamp CDATA #IMPLIED&gt;
    /// </summary>
    [XmlRoot("way")]
    public class Way : IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType="int", AttributeName="id")]
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("visible")]
        public string Visible
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("user")]
        public string User
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("timestamp")]
        public string Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("tag")]
        public Tag[] Tags;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement("nd")]
        public WayNode[] Nodes;

        #region IComparable Member

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Way)
            {
                Way other = (Way)obj;
                return this.Id.CompareTo(other.Id);
            }
            else
            {
                throw new ArgumentException("Object is not a Way");
            }
        }

        #endregion
    }


    /// <summary>
    /// &lt;!ELEMENT nd EMPTY&gt;
    /// &lt;!ATTLIST nd ref         CDATA #REQUIRED&gt;
    /// </summary>
    [XmlRoot("nd")]
    public class WayNode
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "ref")]
        public int Reference
        {
            get;
            set;
        }

    }
}