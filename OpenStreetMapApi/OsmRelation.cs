using System.Xml.Serialization;
using System;

namespace OpenSteetMapApi
{
    /// <summary>
    /// 
    /// &lt;!ELEMENT relation ((tag|member)*)>
    /// &lt;!ATTLIST relation id        CDATA #REQUIRED&gt;
    /// &lt;!ATTLIST relation visible   CDATA #IMPLIED&gt;
    /// &lt;!ATTLIST relation user      CDATA #IMPLIED&gt;
    /// &lt;ATTLIST relation timestamp CDATA #IMPLIED&gt;
    /// </summary>
    [XmlRoot("relation")]
    public class Relation : IComparable 
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "id")]
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
        [XmlElement("member")]
        public Member[] Members;

        #region IComparable Member
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Relation)
            {
                Relation other = (Relation)obj;
                return this.Id.CompareTo(other.Id);
            }
            else
            {
                throw new ArgumentException("Object is not a Relation");
            }
        }

        #endregion
    }
}