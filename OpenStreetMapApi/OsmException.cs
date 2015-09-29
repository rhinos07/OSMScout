using System;
using System.Runtime.Serialization;

namespace OpenSteetMapApi
{
    /// <summary>
    /// Generic Osm Exception.
    /// </summary>
    [Serializable] 
    public class OsmException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public OsmException()
        {
        }

        /// <summary>
        /// serialization constructor
        /// </summary>
        /// <param name="si"></param>
        /// <param name="sc"></param>
        protected OsmException(SerializationInfo si, StreamingContext sc)
            : base(si, sc)
        {
        }

        /// <summary>
        /// special constructor
        /// </summary>
        /// <param name="message"></param>
        public OsmException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// special constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public OsmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}