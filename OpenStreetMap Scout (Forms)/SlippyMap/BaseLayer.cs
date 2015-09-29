using System.Drawing;
using OpenStreetMapPictures.TilesDownload;
using OpenSteetMapApi;


namespace OpenStreetMapScout.SlippyMap
{
    class BaseLayer : ILayer
    {
        #region ILayer Member

        public Coordinate LeftBottom
        {
            set { leftBottom = value; }
        }
        protected Coordinate leftBottom = new Coordinate(0, 0);

        public Coordinate RightTop
        {
            set { rightTop = value; }
        }
        protected Coordinate rightTop = new Coordinate(0,0);


        double centerCoordTileColumnPartly;
        double centerCoordTileRowPartly;

        public Coordinate CenterCoord
        {
            set { centerCoord = value;
            Calculator.GetTilesDoubleValue(centerCoord, zoom, ref centerCoordTileColumnPartly, ref centerCoordTileRowPartly);
            }
        }
        protected Coordinate centerCoord = new Coordinate(0, 0);

        public double HalfWindowWidth
        {
            set { halfWindowWidth = value; }
        }
        protected double halfWindowWidth;

        public double HalfWindowHeight
        {
            set { halfWindowHeight = value; }
        }
        protected double halfWindowHeight;

        public string Descripton
        {
            set { description = value; }
            get { return description; }
        }
        protected string description = string.Empty;

        public int Zoom
        {
            set { zoom = value; }
        }
        private int zoom;

        public Point StartDrawingPoint
        {
            set { startDrawingPoint = value; }
        }
        protected Point startDrawingPoint = new Point();

        public virtual void Paint(System.Drawing.Graphics g)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>e
        /// <param name="mouseCoord"></param>
        /// <returns></returns>
        public virtual object HitTest(Coordinate mouseCoord)
        {
            return null;
        }

        #endregion

        protected Point CalculateScreenPos(Coordinate objectCoord)
        {
            Point drawAt = new Point();

            double tileColumnPartly = 0;
            double tileRowPartly = 0;

            Calculator.GetTilesDoubleValue(objectCoord, zoom, ref tileColumnPartly, ref tileRowPartly);

            drawAt.X = -startDrawingPoint.X +  (int)(((tileColumnPartly - centerCoordTileColumnPartly) * 256) + halfWindowWidth);
            drawAt.Y = -startDrawingPoint.Y +  (int)(((tileRowPartly - centerCoordTileRowPartly) * 256) + halfWindowHeight);

            return drawAt;
        }

        public int NodeCount
        {
            get;
            set;
        }

        protected int maxNodeCount = 50000;

    }
}