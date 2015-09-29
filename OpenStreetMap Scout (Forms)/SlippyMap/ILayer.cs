using System;
using System.Collections.Generic;
using System.Text;
using OpenSteetMapApi;
using System.Drawing;

namespace OpenStreetMapScout.SlippyMap
{
    interface ILayer
    {
        Coordinate LeftBottom
        {
            set;
        }

        Coordinate RightTop
        {
            set;
        }

        Coordinate CenterCoord
        {
            set;
        }

        double HalfWindowWidth
        {
            set;
        }

        double HalfWindowHeight
        {
            set;
        }

        string Descripton
        {
            set;
            get;
        }

        Point StartDrawingPoint
        {
            set;
        }

        int Zoom
        {
            set;
        }

        void Paint(Graphics g);

        object HitTest(Coordinate mouseCoord);
    }
}
