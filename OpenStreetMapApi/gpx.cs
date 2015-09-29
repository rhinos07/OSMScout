using System.Xml.Serialization;
using System.IO;
using System;


namespace OpenSteetMapApi
{
    /// <summary>
    /// interface for loading and parsing of gpx xml files
    /// </summary>
    public static class Gpx
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static GpxTree LoadByStream(Stream stream)
        {
            string osmXmlResponse = string.Empty;
            StreamReader streamRd = new StreamReader(stream);
            string line;
            while ((line = streamRd.ReadLine()) != null)
            {
                osmXmlResponse += line;
            }
            // hack:
            osmXmlResponse = CutAttributeOut(osmXmlResponse, "xmlns=");
            osmXmlResponse = CutAttributeOut(osmXmlResponse, "xmlns:xsi=");
            osmXmlResponse = CutAttributeOut(osmXmlResponse, "xsi:schemaLocation=");
            osmXmlResponse = CutAttributeOut(osmXmlResponse, "xmlns:topografix=");

            GpxTree gpxTree = Deserialize(osmXmlResponse);

            return gpxTree;
        }

        private static string CutAttributeOut(string osmXmlResponse, string attribute)
        {
            int xmlnsPos = osmXmlResponse.IndexOf(attribute, StringComparison.Ordinal);
            if (xmlnsPos > -1)
            {
                int xmlnsEnd = osmXmlResponse.IndexOf("\"", xmlnsPos + attribute.Length +1, StringComparison.Ordinal);
                string temp1 = osmXmlResponse.Substring(0, xmlnsPos);
                string temp2 = osmXmlResponse.Substring(xmlnsEnd + 1);
                osmXmlResponse = temp1 + temp2;
            }
            return osmXmlResponse;
        }


        private static XmlSerializer _responseSerializer = new XmlSerializer(typeof(OpenSteetMapApi.GpxTree));

        internal static GpxTree Deserialize(string responseString)
        {
            XmlSerializer serializer = _responseSerializer;
            try
            {
                // Deserialise the web response into the gpx object
                StringReader responseReader = new StringReader(responseString);
                GpxTree response = (GpxTree)serializer.Deserialize(responseReader);
                responseReader.Close();

                return response;
            }
            catch (InvalidOperationException ex)
            {
                // Serialization error occurred!
                throw new OsmException("Invalid format of gpx", ex);
            }
        }
    }
}