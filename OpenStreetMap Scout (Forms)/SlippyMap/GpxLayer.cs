using OpenSteetMapApi;
using System.Drawing;

namespace OpenStreetMapScout.SlippyMap
{
    class GpxLayer : BaseLayer
    {
        private GpxTree gpxTree;

        private bool connectNodes = true;

        private Pen wayPen = new Pen(Color.Yellow, 2);
        private Font descriptionFont = SystemFonts.StatusFont;
        private Brush descriptionBrush = Brushes.Red;
        private Brush normaleBrush = Brushes.Orange;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="gt"></param>
        /// <param name="desc"></param>
        /// <param name="connect"></param>
        public GpxLayer(GpxTree gt, string desc, bool connect)
        {
            gpxTree = gt;
            description = desc;
            connectNodes = connect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void Paint(System.Drawing.Graphics g)
        {
            if ((gpxTree != null) && (gpxTree.tracks != null ))
            {
                foreach (GpxTrack track in gpxTree.tracks)
                {
                    if (track.TrackSegments != null)
                    {
                        foreach (GpxTrackSegment trackseg in track.TrackSegments)
                        {
                            Point lastTrackPntLoc = new Point(0, 0);
                            if (trackseg.TrackPoints != null)
                            {
                                foreach (GpxTrackPoint trackpnt in trackseg.TrackPoints)
                                {
                                    Coordinate objectCoord = new Coordinate(trackpnt.Lat, trackpnt.Lon);

                                    // is track point in visible area ?
                                    if ((leftBottom.Longitude < objectCoord.Longitude) && (objectCoord.Longitude < rightTop.Longitude)
                                        && ((leftBottom.Latitude < objectCoord.Latitude) && (objectCoord.Latitude < rightTop.Latitude)))
                                    {
                                        Point trackPntLoc = CalculateScreenPos(objectCoord);

                                        // draw point with description
                                        if ((trackpnt.Description != null) && (trackpnt.Description.Length > 0))
                                        {
                                            g.FillRectangle(descriptionBrush, trackPntLoc.X - 1, trackPntLoc.Y - 1, 3, 3);
                                            g.DrawString(trackpnt.Description, descriptionFont, descriptionBrush, trackPntLoc.X, trackPntLoc.Y);
                                        }
                                        else // draw normal point
                                        {
                                            g.FillRectangle(normaleBrush, trackPntLoc.X - 1, trackPntLoc.Y - 1, 3, 3);
                                        }

                                        // connect points to ways
                                        if (connectNodes && (lastTrackPntLoc.X > 0))
                                        {
                                            g.DrawLine(wayPen, lastTrackPntLoc, trackPntLoc);
                                        }
                                        lastTrackPntLoc = trackPntLoc;
                                    }
                                }
                            }
                        }
 
                    }
                }
            }
        }
    }
}