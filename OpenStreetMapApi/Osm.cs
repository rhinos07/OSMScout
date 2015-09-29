
using System;
using System.Globalization;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace OpenSteetMapApi
{
    /// <summary>
    /// interface for the interaction with the OpenStreetMap.org database
    /// </summary>
    public static class Osm
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static OsmTree GetMap(double left, double bottom, double right, double top)
        {
            string osmXmlResponse = GetMapXmlString(left, bottom, right, top);
            StringReader responseReader = new StringReader(osmXmlResponse);
            OsmTree osmTree = Deserialize(responseReader);
            osmTree.Sort();

            return osmTree;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        private static string GetMapXmlString( double left, double bottom,double right,double top )
        {
            string bBoxCoods = BuildBoundingBoxString(left, bottom, right, top);
            string variables = "bbox={bbox}";

            variables = variables.Replace("{bbox}", bBoxCoods);

            return DoGetResponse(osmUri + "map", variables );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public static OsmTree GetRelation(int relationId)
        {
            string osmXmlResponse = DoGetResponse(osmUri + "relation/"+relationId.ToString()+"/full", string.Empty); ;
            StringReader responseReader = new StringReader(osmXmlResponse);
            OsmTree osmTree = Deserialize(responseReader);
            osmTree.Sort();

            return osmTree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wayId"></param>
        /// <returns></returns>
        public static OsmTree GetWay(int wayId)
        {
            string osmXmlResponse = DoGetResponse(osmUri + "way/" + wayId.ToString() + "/full", string.Empty); ;
            StringReader responseReader = new StringReader(osmXmlResponse);
            OsmTree osmTree = Deserialize(responseReader);
            osmTree.Sort();

            return osmTree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static OsmTree LoadByStream(Stream stream)
        {
            OsmTree osmTree = Deserialize(new StreamReader(stream));
            osmTree.Sort();
            return osmTree;
        }

        private static Uri osmUri = new Uri("http://api.openstreetmap.org/api/0.5/");

        private static XmlSerializer _responseSerializer = new XmlSerializer(typeof(OpenSteetMapApi.OsmTree));

        private static string DoGetResponse(string url, string variables)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;

            if (variables.Length == 0)
            {
                // nothing
            }
            else if (variables.Length < 2000)
            {
                url += "?" + variables;
                variables = string.Empty;
            } 

            // Initialise the web request
            req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method =  "POST";

            if (req.Method == "POST") req.ContentLength = variables.Length;

            req.UserAgent = "Mozilla/4.0 OpenStreetMap API (compatible; MSIE 6.0; Windows NT 5.1)"; ;
            //if (Proxy != null) 
            //    req.Proxy = Proxy;
            req.Timeout = 30000;
            req.KeepAlive = false;
            if (variables.Length > 0)
            {
                req.ContentType = "application/x-www-form-urlencoded";
                StreamWriter sw = new StreamWriter(req.GetRequestStream());
                sw.Write(variables);
                sw.Close();
            }
            else
            {
                req.GetRequestStream().Close();
            }

            try
            {
                // Get response from the internet
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
     
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse res2 = (HttpWebResponse)ex.Response;
                    if (res2 != null)
                    {
                        throw new OsmException(String.Format("HTTP Error {0}, {1}", (int)res2.StatusCode, res2.StatusDescription), ex);
                    }
                }
                throw new OsmException(ex.Message, ex);

            }

            string responseString = string.Empty;

            if (res != null)
            {
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
            }

            return responseString;
        }

        private static string BuildBoundingBoxString(double left, double bottom, double right, double top)
        {
            CultureInfo us = new CultureInfo("en-US");
            string bbox = left.ToString(us) + "," + bottom.ToString(us) + ","
                        + right.ToString(us) + "," + top.ToString(us);
            return bbox;
        }


        private static OsmTree Deserialize(TextReader responseReader)
        {
            XmlSerializer serializer = _responseSerializer;
            try
            {
                // Deserialise the web response into the osm tree object
                OsmTree response = (OsmTree)serializer.Deserialize(responseReader);
                responseReader.Close();

                return response;
            }
            catch (InvalidOperationException ex)
            {
                // Serialization error occurred!
                throw new OsmException("Invalid format of osm response", ex);
            }
        }
    }
}