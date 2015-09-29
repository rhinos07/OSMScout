using System;
using System.Drawing;

namespace OpenStreetMapPictures.TilesDownload {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ExternalUpdateTileDelegate(object sender, EventArgs e);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void FinishedCacheLoadingDelegate(object sender, EventArgs e);


    /// <summary>
    /// 
    /// </summary>
    public interface IDownloadTiles {

        /// <summary>
        /// 
        /// </summary>
        event ExternalUpdateTileDelegate ExternalUpdateTile;

        /// <summary>
        /// 
        /// </summary>
        event FinishedCacheLoadingDelegate FinishedCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="zoom"></param>
        /// <param name="download"></param>
        void Initialize(double longitude, double latitude, int zoom, bool download);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="zoom"></param>
        /// <param name="download"></param>
        void Initialize(int x, int y, int zoom, bool download);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        void SetDownloadSettings(DownloadSettings settings);

        /// <summary>
        /// name of renderer (from settings)
        /// </summary>
        string RendererName
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Size TileSize{ get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int XTileNumber{ get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int YTileNumber{ get; }

        /// <summary>
        /// 
        /// </summary>
        int ZoomValue{get;}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        string GetFailSafeTileFilePath(int tileX, int tileY);
    }


    /// <summary>
    /// 
    /// </summary>
    public class DownloadImage
    {

        private String url;
        private int xtile;
        private int ytile;
        private int zoom;
        private String filepath;

        /// <summary>
        /// constructor
        /// </summary>
        public DownloadImage()
        {
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public DownloadImage(String u, int x, int y, int z )
        {
            this.url = u;
            this.xtile = x;
            this.ytile = y;
            this.zoom = z;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Url
        {
            get { return this.url; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int X
        {
            get { return this.xtile; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Y
        {
            get { return this.ytile; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Z
        {
            get { return this.zoom; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Filepath
        {
            get { return this.filepath; }
            set { this.filepath = value; }
        }
    }
}
