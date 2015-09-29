using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using OpenSteetMapApi;

namespace OpenStreetMapScout.SlippyMap
{
    class FilterHandler
    {
        private FilterHandler()
        {}

        private static XmlSerializer filterSerializer = new XmlSerializer(typeof(OsmObjectFilter));

        public static OsmObjectFilter CreateFilter(Stream stream)
        {
            StreamReader streamRd = new StreamReader(stream);

            OsmObjectFilter osmFilter;

            XmlSerializer serializer = filterSerializer;
            try
            {
                // Deserialise the web response into the osm tree object
                osmFilter = (OsmObjectFilter)serializer.Deserialize(streamRd);
                streamRd.Close();

            }
            catch (InvalidOperationException)
            {
                throw;
            }

            return osmFilter;
        }

        public static OsmTree ApplyFilter(OsmObjectFilter ft, OsmTree origObjects)
        {
            if (ft != null)
            {
                OsmTree newOsmTree = new OsmTree();
                List<Node> nodeList = new List<Node>();

                foreach (FilterRule rule in ft.Rules)
                {
                    if (rule.Target == "node")
                    {
                        string key = rule.Selector.Substring(0, rule.Selector.IndexOf("="));
                        string value = rule.Selector.Substring(rule.Selector.IndexOf("=") + 1);

                        foreach (Node node in origObjects.Nodes)
                        {
                            bool showNode = false;
                            if (node.Tags != null)
                            {
                                foreach (Tag tag in node.Tags)
                                {
                                    if (tag.Key == key)
                                    {
                                        if (value == "*")
                                        {
                                            showNode = true;
                                        }
                                        else if (value == tag.Value)
                                        {
                                            showNode = true;
                                        }
                                    }
                                }
                            }

                            if (showNode)
                            {
                                Node newNode = (Node) node.Clone();
                                foreach (FilterTemplate template in rule.Templates)
                                {
                                    if (template.FilterType == "dot")
                                    {
                                        // nothing to do
                                    }
                                    else if (template.FilterType == "text")
                                    {
                                        string text = string.Empty;
                                        foreach (Tag tag in node.Tags)
                                        {
                                            if (tag.Key == template.Tag)
                                            {
                                                text = tag.Value;
                                            }
                                        }
                                        newNode.DrawText += text + "\n";
                                    }
                                    else if (template.FilterType == "icon")
                                    {
                                        newNode.DrawIcon = template.Name;
                                    }
                                }
                                nodeList.Add(newNode);
                            }
                        }

                    }


                }
                newOsmTree.Nodes = nodeList.ToArray();


                return newOsmTree;
            }
            else
            {
                return origObjects;
            }
        }
    }
}
