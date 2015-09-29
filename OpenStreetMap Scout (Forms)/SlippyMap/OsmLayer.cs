using OpenSteetMapApi;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace OpenStreetMapScout.SlippyMap
{
    class OsmLayer : BaseLayer
    {
        protected OsmTree osmTree;

        private List<Image> imageCache = new List<Image>();
        private List<string> imagePathes = new List<string>();

        public OsmLayer(OsmTree gt, string desc )
        {
            osmTree = gt;
            description = "osm objects: " + desc;
        }

        public override void Paint(System.Drawing.Graphics g)
        {
            NodeCount = 0;

            if (osmTree != null)
            {
                foreach (Node node in osmTree.Nodes)
                {
                    if (NodeCount < maxNodeCount)
                    {

                        Coordinate objectCoord = new Coordinate(node.Lat, node.Lon);

                        if ((leftBottom.Longitude < objectCoord.Longitude) && (objectCoord.Longitude < rightTop.Longitude)
                            && ((leftBottom.Latitude < objectCoord.Latitude) && (objectCoord.Latitude < rightTop.Latitude)))
                        {
                            Point drawAt = CalculateScreenPos(objectCoord);

                            if ((node.DrawIcon != null) && (node.DrawIcon.Length > 0))
                            {
                                DrawIcon(g, node.DrawIcon, drawAt);
                            }
                            else
                            {
                                g.FillRectangle(Brushes.Gray, drawAt.X - 2, drawAt.Y - 2, 5, 5);
                            }

                            if ((node.DrawText!= null )&&(node.DrawText.Length > 0))
                            {
                                DrawStringWhiteShadow(g, node.DrawText, Brushes.Black, drawAt);
                            }

                            NodeCount++;
                        }
                    }
                    else
                    {
                        Point drawAt = CalculateScreenPos(centerCoord);
                        g.DrawString(Descripton + " max node count reached!", SystemFonts.StatusFont, Brushes.Red, drawAt);
                                
                        break;
                    }

 
                }
            }
        }

        private static void DrawStringWhiteShadow(System.Drawing.Graphics g, string text, Brush b, Point drawAt)
        {
            g.DrawString(text, SystemFonts.StatusFont, Brushes.White, drawAt.X - 1, drawAt.Y - 1);
            g.DrawString(text, SystemFonts.StatusFont, Brushes.White, drawAt.X - 1, drawAt.Y + 1);
            g.DrawString(text, SystemFonts.StatusFont, Brushes.White, drawAt.X + 1, drawAt.Y - 1);
            g.DrawString(text, SystemFonts.StatusFont, Brushes.White, drawAt.X + 1, drawAt.Y + 1);
            g.DrawString(text, SystemFonts.StatusFont, b, drawAt);
        }

        private void DrawIcon(System.Drawing.Graphics g, string imagePath, Point drawAt)
        {
            try
            {
                Image newImage = GetImageFromCache(imagePath);
                g.DrawImage(newImage,drawAt.X-8,drawAt.Y-8,16,16);
            }
            catch (System.IO.FileNotFoundException)
            {
                DrawStringWhiteShadow(g,"image not found", Brushes.Red, drawAt);
            }
            catch (Exception)
            {
                DrawStringWhiteShadow(g, "error", Brushes.Red, drawAt);
            }

        }

        private Image GetImageFromCache(string imagePath)
        {
            Image foundImage = null;
            for (int i = 0; i< imagePathes.Count; i++)
            {
                if (imagePathes[i] == imagePath)
                {
                    foundImage = imageCache[i];
                }
            }
            if (foundImage == null)
            {
                foundImage = Image.FromFile(imagePath);
            }

            return foundImage;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseCoord"></param>
        /// <returns></returns>
        public override object HitTest(Coordinate mouseCoord)
        {
            object hit = null;

            foreach (Node node in osmTree.Nodes)
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