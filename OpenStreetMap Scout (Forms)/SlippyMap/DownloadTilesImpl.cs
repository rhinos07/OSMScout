using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using OpenSteetMapApi;

namespace OpenStreetMapPictures.TilesDownload {
    
    /// <summary>
    /// handles the downloading of tiles
    /// </summary>
    public class DownloadTilesImpl : IDownloadTiles {

        /// <summary>
        /// 
        /// </summary>
        public event ExternalUpdateTileDelegate ExternalUpdateTile;

        /// <summary>
        /// 
        /// </summary>
        public event FinishedCacheLoadingDelegate FinishedCache;
  
        private const int MIN_ZOOM = 3;
        private const int MAX_ZOOM = 19;

        private int zoom = 5;
        private int xTileNumber;
        private int yTileNumber;

        // load 5x5 tiles 
        private Point tileMatrixSize = new Point(5, 5); 

        private int thread_id = -1;

        private Queue<Thread> downloadPool;
        private const int MAX_DOWNLOAD_THREADS = 5;

        private readonly object lockobject = new object();

        private String downloadurl;

        private Boolean online = true;

        private int cache_live = 10080; // seven days in minutes

        /// <summary>
        /// name of renderer from settings
        /// </summary>
        public string RendererName
        {
            get { return renderer; }
        }
        private String renderer;

        /// <summary>
        /// constructor
        /// </summary>
        public DownloadTilesImpl() {
            this.downloadPool = new Queue<Thread>(MAX_DOWNLOAD_THREADS);

            DownloadSettings temp = new DownloadSettings();
            this.downloadurl = temp.PossibleRenderers[0].Url;
            this.renderer = temp.PossibleRenderers[0].Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="zoom"></param>
        /// <param name="download"></param>
        public void Initialize(double longitude, double latitude, int zoom, bool download) {
            if ((zoom > MIN_ZOOM) && (zoom < MAX_ZOOM))
            {
                this.zoom = zoom;
                Calculator.GetTiles(new Coordinate(latitude, longitude), zoom, ref this.xTileNumber, ref this.yTileNumber);

                if (download)
                {
                    this.KillAllThreads();
                    this.StartDownload();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="zoom"></param>
        /// <param name="download"></param>
        public void Initialize(int x, int y, int zoom, bool download)
        {

            if (( zoom > MIN_ZOOM ) && (zoom < MAX_ZOOM))
            {
                this.xTileNumber = x;
                this.yTileNumber = y; 
                this.zoom = zoom;
                if (download)
                {
                    //this.killAllThreads();
                    this.StartDownload();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void SetDownloadSettings(DownloadSettings settings) {
            //this.downloadSetting = settings;

            this.renderer = settings.Renderer.Name;
            this.downloadurl = settings.Renderer.Url;

            this.KillAllThreads();
            this.StartDownload();
        }

  
        /// <summary>
        /// returns the actural zoom level
        /// </summary>
        public int ZoomValue{
            get { return this.zoom; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int XTileNumber {
            get { return this.xTileNumber; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int YTileNumber{
            get { return this.yTileNumber; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        protected void StartDownload() {
            lock (lockobject) {
                Thread downloadThread = new Thread(new ThreadStart(DownloadImages));
                downloadThread.IsBackground = true;
                this.thread_id = downloadThread.ManagedThreadId;

                if (this.downloadPool.Count == MAX_DOWNLOAD_THREADS) {
                    try {
                        Thread t = this.downloadPool.Dequeue();
                        if (t.IsAlive) {
                            t.Abort();
                        }                        
                    } catch (Exception ) {
                        //log.Warn("removing thread " + ex.Message);
                    }
                } 
                
                this.downloadPool.Enqueue(downloadThread);
                downloadThread.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void KillAllThreads() {
            lock (lockobject) {
                try {
                    int count = this.downloadPool.Count;
                    for (int i = 0; i < count; i++) {
                        Thread t = this.downloadPool.Dequeue();
                        if (t.IsAlive) {
                            t.Abort();
                        }
                    }

                } catch (Exception ) {
                    //log.Warn("kill all threads " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Point TileMatrixSize{
            set { tileMatrixSize = value; }
            get { return tileMatrixSize;  }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void DownloadImages() {

            DownloadImage[,] mapimages = new DownloadImage[tileMatrixSize.X, tileMatrixSize.Y];
            int nCountTiles = Calculator.GetCountTiles(zoom);

            for (int i = xTileNumber; i <= xTileNumber + tileMatrixSize.X - 1; i++)
            {
                // correct tile number when Longitude around -180deg / 180deg 
                int xTile = i;

                if (xTile < 0)
                {
                    xTile = xTile + nCountTiles;
                }
                else if (xTile >= nCountTiles)
                {
                    xTile = xTile - nCountTiles;
                }

                for (int j = yTileNumber; j <= yTileNumber + tileMatrixSize.Y - 1; j++)
                {
                    DownloadImage di = new DownloadImage(this.GetTilesUrl(xTile, j, zoom), xTile, j, zoom );
                    try
                    {
                        mapimages[i - xTileNumber, j - yTileNumber] = di;
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                    }

                }
            }


            DateTime dt = DateTime.Now;

            String zoompath = Utils.GetZoompath(this.zoom, renderer);

            if (!Directory.Exists(zoompath)) {
                Directory.CreateDirectory(zoompath);
            }

            List<DownloadImage> downloadlist = new List<DownloadImage>();

            foreach (DownloadImage di in mapimages) {
                try {
                    String savepath = this.GetFilePathAndCreateDir(di);

                    Boolean needDownload = true;

                    if (this.online) {
                        if (File.Exists(savepath)) {
                            needDownload = false;

                            TimeSpan test = dt - File.GetLastWriteTime(savepath);

                            if (test.TotalMinutes > this.cache_live) {
                                needDownload = true;
                            }
                        }
                    }

                    di.Filepath = savepath;

                    if (!needDownload) 
                    {
                        this.DownloadedTile( Thread.CurrentThread.ManagedThreadId );
                    } 
                    else 
                    {
                        if ((di.X == xTileNumber + 2) && (di.Y == yTileNumber + 2))
                            downloadlist.Insert(0, di);
                        else
                            downloadlist.Add(di);
                    }

                } catch (Exception ) {
                }
            }

            try {
                this.FinishedCache(this,null);
            } catch (Exception ) {
            }
                       
            foreach (DownloadImage di in downloadlist) {
                this.DownloadImage(di);
            }

        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadid"></param>
        protected void DownloadedTile(int threadid) {
            lock (lockobject) {
                if (this.thread_id == threadid) {
                    ExternalUpdateTile(this, null);
                } 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Size TileSize{
            get
            {
                return tileSize;
            }
        }
        private static Size tileSize = new Size(256, 256);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        public string GetFailSafeTileFilePath(int tileX, int tileY)
        {
            int newTileX = tileX;
            int nCountTiles = Calculator.GetCountTiles(zoom);
            if (tileX < 0)
            {
                newTileX = tileX + nCountTiles;
            }
            else if (tileX >= nCountTiles)
            {
                newTileX = tileX - nCountTiles;
            }
            string filePath = Utils.GetFilePath(newTileX, tileY, ZoomValue, renderer);

            if (!File.Exists(filePath))
            {
                filePath = string.Empty;
            }
            return filePath;
        }

        private void DownloadImage(DownloadImage di)
        {
            try
            {
                if (!File.Exists(di.Filepath))
                {
                    HttpWebRequest req = (HttpWebRequest) WebRequest.Create(di.Url);
                    req.UserAgent = "Mozilla";
              
                    req.Timeout = 10000;
                    WebResponse resp = req.GetResponse();
                    Stream stream = resp.GetResponseStream();

                    Image image = Image.FromStream(stream);
                    stream.Close();

                    try
                    {
                        image.Save(di.Filepath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    catch (Exception)
                    {
                        //log.Warn("SAVE: " + svx.Message);
                    }
                    resp.Close();
                }
                else
                {
                    //log.Debug("someone already downloaded the tile ...");
                }
            }
            catch (Exception )
            {
                //log.Warn("http connection " + ex.Message);
            }

            try
            {
                this.DownloadedTile( Thread.CurrentThread.ManagedThreadId );
            }
            catch (Exception)
            {
                //log.Warn("update tile event " + ex.Message);
            }
        }

        private String GetFilePathAndCreateDir(DownloadImage di)
        {
            String xpath = Utils.GetXPath(di.X, di.Z, renderer);

            if (!Directory.Exists(xpath))
            {
                Directory.CreateDirectory(xpath);
            }

            String savepath = Utils.GetYPath(xpath, di.Y);

            return savepath;
        }

        private String GetTilesUrl(int xtile, int ytile, int zoom)
        {
            String myurl = this.downloadurl;

            myurl = myurl.Replace("{ZOOM}", zoom.ToString());
            myurl = myurl.Replace("{XTILE}", xtile.ToString());
            myurl = myurl.Replace("{YTILE}", ytile.ToString());


            return myurl;
        }
    }
}
