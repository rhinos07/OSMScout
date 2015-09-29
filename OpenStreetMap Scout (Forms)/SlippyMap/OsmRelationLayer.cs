using OpenSteetMapApi;
using System.Drawing;
using System;
namespace OpenStreetMapScout.SlippyMap
{
    class OsmRelationLayer : OsmLayer
    {
        private Relation relation;
        private Pen wayPen = new Pen(Color.Red, 3);

        public OsmRelationLayer(OsmTree gt, Relation selectedRelation)
            :base(gt, string.Empty )
        {
            relation = selectedRelation;
            description = GetDesc(relation);
        }

        private static string GetDesc(Relation relation)
        {
            string id = relation.Id.ToString();
            string name = string.Empty;
            string network = string.Empty;

            foreach (Tag tag in relation.Tags)
            {
                if (tag.Key == "name")
                {
                    name = tag.Value;
                }
                else if (tag.Key == "network")
                {
                    network = tag.Value;
                }
            }

            return  network + " - " + name + " (" + id + ") ";
        }

        public override void Paint(System.Drawing.Graphics g)
        {
            if (osmTree != null)
            {
                foreach (Member mb in relation.Members)
                {
                    if (mb.type == "way")
                    {
                        Way w = GetWay(mb.reference);
                        if (w != null)
                            PaintWay(g, w);
                    }
                    else if (mb.type == "node")
                    {
                        Node n = GetNode(mb.reference);
                        if (n != null)
                            PaintNode(g, n);
                    }
                }
            }
        }

        private Node GetNode(int id)
        {
            Node node = new Node();
            node.Id = id;

            int index = Array.BinarySearch(osmTree.Nodes, node);

            if (index > 0)
            {
                node = osmTree.Nodes[index];
            }
            else
            {
                node = null;
            }

            return node;
        }

        private Way GetWay(int id)
        {
            Way way = new Way();
            way.Id = id;

            int index = Array.BinarySearch(osmTree.Ways,way);

            if (index >= 0)
            {
                way = osmTree.Ways[index];
            }
            else
            {
                way = null;
            }

            return way;
        }


        private void PaintNode(System.Drawing.Graphics g, Node node)
        {
            Coordinate objectCoord = new Coordinate(node.Lat, node.Lon);

            if ((leftBottom.Longitude < objectCoord.Longitude) && (objectCoord.Longitude < rightTop.Longitude)
                && ((leftBottom.Latitude < objectCoord.Latitude) && (objectCoord.Latitude < rightTop.Latitude)))
            {
                Point drawAt = CalculateScreenPos(objectCoord );

                g.FillRectangle(Brushes.Gray, drawAt.X - 2, drawAt.Y - 2, 5, 5);
            }
        }

        private void PaintWay(System.Drawing.Graphics g, Way w)
        {
            Node firstNode = null;
            foreach (WayNode wnd in w.Nodes)
            {
                Node secondNode = GetNode(wnd.Reference);

                if (secondNode != null)
                {
                    if (firstNode != null)
                    {
                        PaintLine(g, firstNode, secondNode);
                    }
                    firstNode = secondNode;
                }
                   
            }
        }

        private void PaintLine(Graphics g, Node firstNode, Node secondNode)
        {
            Coordinate firstObjectCoord  = new Coordinate(firstNode.Lat, firstNode.Lon);
            Coordinate secondObjectCoord = new Coordinate(secondNode.Lat, secondNode.Lon);

            if ((leftBottom.Longitude < firstObjectCoord.Longitude) && (firstObjectCoord.Longitude < rightTop.Longitude)
                && ((leftBottom.Latitude < firstObjectCoord.Latitude) && (firstObjectCoord.Latitude < rightTop.Latitude)))
            {
                Point firstPoint = CalculateScreenPos(firstObjectCoord);
                Point secondPoint = CalculateScreenPos(secondObjectCoord);

                g.DrawLine(wayPen, firstPoint, secondPoint);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseCoord"></param>
        /// <returns></returns>
        public override object HitTest(Coordinate mouseCoord)
        {
            object hit = null;

            foreach ( Node node in osmTree.Nodes)
            {
                if (Math.Abs(mouseCoord.Latitude - node.Lat) < 0.0001)
                {
                    if (Math.Abs(mouseCoord.Longitude - node.Lon) < 0.0001)
                    {
                        hit = node;
                    }
                }
            }

            return hit;
        }
    }
}