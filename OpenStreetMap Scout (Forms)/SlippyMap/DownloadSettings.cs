using System;
using System.Collections.Generic;
using System.Text;

namespace OpenStreetMapPictures {

    /// <summary>
    /// takes the settings 
    /// </summary>
    public class DownloadSettings {

        /// <summary>
        /// 
        /// </summary>
        public List<Renderer> PossibleRenderers = new List<Renderer>();
        
        private Renderer selectedRenderer;

        /// <summary>
        /// constructor
        /// </summary>
        public DownloadSettings() {
            PossibleRenderers.Add(new Renderer("OpenStreetMap MAPNIK", "http://tile.openstreetmap.org/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap OSMARENDER","http://tah.openstreetmap.org/Tiles/tile/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap CYCLE_MAP","http://andy.sandbox.cloudmade.com/tiles/cycle/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap CLOUDMADE_WEB_STYLE","http://tile.cloudmade.com/8bafab36916b5ce6b4395ede3cb9ddea/1/256/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap CLOUDMADE_MOBILE_STYLE","http://tile.cloudmade.com/8bafab36916b5ce6b4395ede3cb9ddea/2/256/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap CLOUDMADE_NONAMES_STYLE","http://tile.cloudmade.com/8bafab36916b5ce6b4395ede3cb9ddea/3/256/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("OpenStreetMap MAPLINT","http://tah.openstreetmap.org/Tiles/maplint/{ZOOM}/{XTILE}/{YTILE}.png"));
            PossibleRenderers.Add(new Renderer("Google Maps","http://mt0.google.com/mt?v=w2.83&x={XTILE}&y={YTILE}&z={ZOOM}"));
            PossibleRenderers.Add(new Renderer("Google Satelite","http://khm.google.de/kh?v=31&x={XTILE}&y={YTILE}&z={ZOOM}"));
            PossibleRenderers.Add(new Renderer("Google Terrain", "http://mt.google.com/mt?v=app.81&x={XTILE}&s=&y={YTILE}&z={ZOOM}"));
            //PossibleRenderers.Add(new Renderer("Yahoo Hybrid", "http://us.maps3.yimg.com/aerial.maps.yimg.com/tile?v=1.7&t=a&x={XTILE}&y={YTILE}&z={ZOOM}"));
            PossibleRenderers.Add(new Renderer("none", string.Empty));

            this.selectedRenderer = PossibleRenderers[0];
        }

        /// <summary>
        /// access to the render type
        /// </summary>
        public Renderer Renderer
        {
            get { return this.selectedRenderer; }
            set { this.selectedRenderer = value; }
        }
       
    }

    /// <summary>
    /// 
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public Renderer(string name, string url)
        {
            Url = url;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
