using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using OpenSteetMapApi;
using OpenStreetMapPictures.TilesDownload;
using OpenStreetMapScout.SlippyMap;


namespace OpenStreetMapPictures {
    public partial class MapControl : UserControl {
        /// <summary>
        /// 
        /// </summary>
        public delegate void MapChangedDelegate();

        /// <summary>
        /// 
        /// </summary>
        public MapChangedDelegate Changed;

        private IDownloadTiles mapLoader;

        /// <summary>
        /// coordinate in the center of the visible map (center of window)
        /// </summary>
        public Coordinate CenterCoordinate{
            get { return centerCoord;  }
            set { centerCoord = value;}
        }
        private Coordinate centerCoord = new Coordinate(0, 0);

        private Point mouseDown ;
        private bool isInMouseMove;
        // debug switch for faster mouse drag
        private bool mouseMoveBit = true;

        private Bitmap backBuffer;



        // imported osm database dump
        private OsmTree osmDumpObjects;
        private ThreadGetData getDataThread;

        private List<ILayer> layers = new List<ILayer>();

        /// <summary>
        /// used to show the progress bar
        /// </summary>
        public bool DownloadInProgress
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public MapControl() 
        {
            InitializeComponent();
        
            getDataThread = new ThreadGetData(new GetDataFinishedCallback(ThreadGetDataFinishedCallback));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void UpdateSettings(DownloadSettings settings) {
          this.mapLoader.SetDownloadSettings(settings);
        }           

        /// <summary>
        /// 
        /// </summary>
        public IDownloadTiles MapLoader {
            set { 
                this.mapLoader = value; 

                this.mapLoader.FinishedCache += new OpenStreetMapPictures.TilesDownload.FinishedCacheLoadingDelegate(tiles_FinishedCache);
                this.mapLoader.ExternalUpdateTile += new ExternalUpdateTileDelegate(tiles_ExternalUpdateTile);

                this.mapLoader.Initialize(9.7296, 52.5623, 6, true);
            }
        }

        private void tiles_ExternalUpdateTile(object sender, EventArgs e)
        {
            // try to detemine the area, that has to be painted
            // hm ... repaint complete window
            this.Invalidate();
        }      

        private void tiles_FinishedCache(object sender, EventArgs e) {
            this.Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);

            if (backBuffer != null)
            {
                backBuffer.Dispose();
                backBuffer = null;
            }

            Invalidate();

            CallMapChangedDelegate();
        }

        #region painting
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing!!
            //base.OnPaintBackground(e);
        }

        private Point startDrawingPoint = new Point();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            int neighbourTiles = 3;
            Point tileMatrixSize = new Point(neighbourTiles * 2 + 1, neighbourTiles * 2 + 1);

            if (backBuffer == null)
            {
                backBuffer = new Bitmap(tileMatrixSize.X * 256, tileMatrixSize.Y * 256);
            }

            if (!this.isInMouseMove || !this.mouseMoveBit)
            {
                double tileColumnPartly = 0;
                double tileRowPartly = 0;

                Calculator.GetTilesDoubleValue(centerCoord, ZoomValue, ref tileColumnPartly, ref tileRowPartly);

                int tileColumn = (int)Math.Floor(tileColumnPartly);
                int tileRow = (int)Math.Floor(tileRowPartly);

                tileColumnPartly = tileColumnPartly - tileColumn;
                tileRowPartly = tileRowPartly - tileRow;

                startDrawingPoint.X = (this.Width / 2) -
                    (int)(tileColumnPartly * 256) -
                    (neighbourTiles * 256);
                startDrawingPoint.Y = (this.Height / 2) -
                    (int)(tileRowPartly * 256) -
                    (neighbourTiles * 256);

                Graphics g = Graphics.FromImage(backBuffer);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                PaintMap(g);
                PaintLayers(g);

                g.Dispose();


                e.Graphics.DrawImageUnscaled(backBuffer, startDrawingPoint.X, startDrawingPoint.Y);
                PaintHud(e.Graphics);
            }
            else
            {
                Point mousePos = Control.MousePosition;
                int movedToX = startDrawingPoint.X + mousePos.X - mouseDown.X;
                int movedToY = startDrawingPoint.Y +mousePos.Y - mouseDown.Y;

                if (movedToX > 0)
                    e.Graphics.FillRectangle(Brushes.Gray, 0, 0, movedToX, this.Height);
                else if ( backBuffer.Width + movedToX < this.Width)
                    e.Graphics.FillRectangle(Brushes.Gray, backBuffer.Width + movedToX, 0, this.Width, this.Height);

                if (movedToY > 0)
                    e.Graphics.FillRectangle(Brushes.Gray, 0, 0, this.Width, movedToY);
                else if (backBuffer.Height + movedToY < this.Height)
                    e.Graphics.FillRectangle(Brushes.Gray, 0, backBuffer.Height + movedToY, this.Width, this.Height);

                e.Graphics.DrawImageUnscaled(backBuffer,  movedToX, movedToY );
            }


            CallMapChangedDelegate();
        }

        private void PaintHud(Graphics g)
        {
            bool showLayerInformation = true;

            if (showLayerInformation)
            {
                System.Drawing.Font drawFont = SystemFonts.StatusFont;

                int row = 5;
                int rowspace = 15;

                string mapName = "none";
                if (mapLoader != null)
                {
                    mapName = mapLoader.RendererName;
                }

                DrawStringWhiteShadow(g, "background: " + mapName, drawFont, row);
                row += rowspace;

                if (osmDumpObjects != null)
                {
                    DrawStringWhiteShadow(g, "osm dump: " + osmDumpObjects.Descripton , drawFont, row);
                    row += rowspace;
                }

                foreach (ILayer layer in layers)
                {
                    DrawStringWhiteShadow(g, "layer: " + layer.Descripton, drawFont, row );
                    row += rowspace;
                }
            }
        }

        private static void DrawStringWhiteShadow(Graphics g, string text, System.Drawing.Font drawFont, int row )
        {
            g.DrawString(text, drawFont, Brushes.White, 4, row - 1);
            g.DrawString(text, drawFont, Brushes.White, 4, row + 1);
            g.DrawString(text, drawFont, Brushes.White, 6, row - 1);
            g.DrawString(text, drawFont, Brushes.White, 6, row + 1);
            g.DrawString(text, drawFont, Brushes.Black, 5, row);
        }

        private void PaintMap(Graphics g)
        {
            if ((mapLoader != null) && (mapLoader.RendererName != "none"))
            {
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();

                System.Drawing.Image imageLoading = System.Drawing.Image.FromStream(a.GetManifestResourceStream("OpenStreetMapScout.LoadingTile.png"));

                // neighbourtile + centertile + neighbourtile
                int neighbourTiles = 3;
                Point tileMatrixSize = new Point(neighbourTiles * 2 + 1, neighbourTiles * 2 + 1);

                double tileColumnPartly = 0;
                double tileRowPartly = 0;

                Calculator.GetTilesDoubleValue(centerCoord, ZoomValue, ref tileColumnPartly, ref tileRowPartly);

                int tileColumn = (int)Math.Floor(tileColumnPartly);
                int tileRow = (int)Math.Floor(tileRowPartly);

                Point tileDrawingPoint = new Point(0,0);

                for (int i = 0; i < tileMatrixSize.X; i++)
                {
                    for (int j = 0; j < tileMatrixSize.Y; j++)
                    {
                        //if ((tileDrawingPoint.X > -tiles.getTileSize().Width) && (tileDrawingPoint.X < this.Width) &&
                        //    (tileDrawingPoint.Y > -tiles.getTileSize().Height) && (tileDrawingPoint.Y < this.Height))
                        {
                            try
                            {
                                string filePath = mapLoader.GetFailSafeTileFilePath(tileColumn + i - neighbourTiles, tileRow + j - neighbourTiles);
                                if (filePath.Length == 0)
                                    g.DrawImage(imageLoading, tileDrawingPoint);
                                else
                                    g.DrawImage(Image.FromFile(filePath), tileDrawingPoint);
                            }
                            catch (Exception)
                            {
                                //log.Debug("paint " + pex.Message);
                            }
                        }
                        tileDrawingPoint.Y += mapLoader.TileSize.Height;
                    }
                    tileDrawingPoint.Y = 0;
                    tileDrawingPoint.X += mapLoader.TileSize.Width; ;
                }
            }
            else
            {
                // only reached in designer mode ...
                g.FillRectangle(Brushes.Gray, 0, 0, 10000, 10000);
            }
        }

        /// <summary>
        /// export seen map into png file
        /// </summary>
        /// <param name="fileName"></param>
        public void ExportAsPng(String fileName)
        {
            int neighbourTiles = 3;
            Point tileMatrixSize = new Point(neighbourTiles * 2 + 1, neighbourTiles * 2 + 1);

            Image image = new Bitmap(tileMatrixSize.X * 256, tileMatrixSize.Y * 256);
            //(this.ClientSize.Width, this.ClientSize.Height);
            Graphics g = Graphics.FromImage(image);

            PaintMap(g);
            PaintLayers(g);

            g.Dispose();

            Image seenImage = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics l = Graphics.FromImage(seenImage);
            l.DrawImageUnscaled(image, startDrawingPoint);
            try
            {
                seenImage.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("File saved!", "OpenStreetMap Scout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void PaintLayers(Graphics g)
        {
            foreach(ILayer layer in layers)
            {
                PaintLayer(g, layer);
            }
        }

        private void PaintLayer(Graphics g, ILayer layer )
        {
            if (layer != null)
            {
                Coordinate leftBottom = new Coordinate(0, 0);
                Coordinate rightTop = new Coordinate(0, 0);

                this.getBoundingBox(ref leftBottom, ref rightTop);

                double halfWindowWidth = this.Width / 2;
                double halfWindowHeight = this.Height / 2;

                layer.Zoom = this.ZoomValue;
                layer.CenterCoord = centerCoord;
                layer.LeftBottom = leftBottom;
                layer.RightTop = rightTop;
                layer.HalfWindowHeight = halfWindowHeight;
                layer.HalfWindowWidth = halfWindowWidth;
                layer.StartDrawingPoint = startDrawingPoint;

                layer.Paint(g);
            }
        }

        #endregion
        
        #region mouse navigation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                if (this.mouseMoveBit)
                    this.mouseDown = Control.MousePosition;
                else
                    this.mouseDown = e.Location;

                isInMouseMove = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (Control.ModifierKeys == Keys.Control )
                {
                    object hit = HitTest(e.Location);

                    if (hit != null)
                    {
                        FormObjectDetails dlg = new FormObjectDetails();
                        dlg.Object = hit;
                        dlg.ShowDialog();
                    }
                }
                else
                {
                    contextMenuStrip.Show(this, e.Location);
                }
            }
        }

        private object HitTest(Point point)
        {
            Coordinate mouseCoord = CalculateCoordinate(point);
            object hit = null;

            for( int i = layers.Count -1; ((i >= 0) && (hit== null)); i-- )
            {
                hit = layers[i].HitTest(mouseCoord);
            }

            return hit;
        }

        private Coordinate CalculateCoordinate(Point pnt)
        {
            int divX = (pnt.X - this.Width / 2);
            int divY = (pnt.Y - this.Height / 2);

            double xTile = 0;
            double yTile = 0;
            Calculator.GetTilesDoubleValue(centerCoord, ZoomValue, ref xTile, ref yTile);

            xTile = xTile + ((double)divX / mapLoader.TileSize.Width);
            yTile = yTile + ((double)divY / mapLoader.TileSize.Height);

            return Calculator.GetLonLat(xTile, yTile, ZoomValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                isInMouseMove = false;

                if (this.mouseMoveBit)
                {
                    Point mousePos = Control.MousePosition;
                    int divX = (this.mouseDown.X - mousePos.X);
                    int divY = (this.mouseDown.Y - mousePos.Y);

                    double xTile = 0;
                    double yTile = 0;
                    Calculator.GetTilesDoubleValue(centerCoord, ZoomValue, ref xTile, ref yTile);

                    xTile = xTile + ((double)divX / mapLoader.TileSize.Width);
                    yTile = yTile + ((double)divY / mapLoader.TileSize.Height);

                    centerCoord = Calculator.GetLonLat(xTile, yTile, ZoomValue);
                }

                UpdateDLer(ZoomValue, true);
            }
        }

        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.mouseMoveBit)
                {
                    int divX = (e.X - this.mouseDown.X);
                    int divY = (e.Y - this.mouseDown.Y);

                    double xTile = 0;
                    double yTile = 0;
                    Calculator.GetTilesDoubleValue(centerCoord, ZoomValue, ref xTile, ref yTile);

                    xTile = xTile - ((double)divX / mapLoader.TileSize.Width);
                    yTile = yTile - ((double)divY / mapLoader.TileSize.Height);

                    centerCoord = Calculator.GetLonLat(xTile, yTile, ZoomValue);

                    this.mouseDown = e.Location;

                    UpdateDLer(ZoomValue, false);
                }

                this.Invalidate();
            }

            CallMapChangedDelegate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int dt = e.Delta;

            centerCoord.Longitude = centerCoord.Longitude +
                ((double)(e.Location.X - this.Size.Width / 2) / this.mapLoader.TileSize.Width) *
                GetTileWidthToLonStep();
            centerCoord.Latitude = centerCoord.Latitude +
                ((double)(e.Location.Y - this.Size.Height / 2) / this.mapLoader.TileSize.Height) *
                GetTileHeightToLatStep();

            if (dt > 0)
            {
                this.Zoom(1);
            }
            else
            {
                this.Zoom(-1);
            }
        }

        #endregion

        #region keyboard navigation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar.Equals('+'))
            {
                this.Zoom(1);
            }
            else if (e.KeyChar.Equals('-'))
            {
                this.Zoom(-1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Down)
            {
                centerCoord.Latitude = centerCoord.Latitude + GetTileHeightToLatStep();
                UpdateDLer(ZoomValue, true);
            }
            if (keyData == (Keys.Down | Keys.Control))
            {
                centerCoord.Latitude = centerCoord.Latitude + GetTileHeightToLatStep() * (this.Height / 256);
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == Keys.Up)
            {
                centerCoord.Latitude = centerCoord.Latitude - GetTileHeightToLatStep();
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == (Keys.Up| Keys.Control))
            {
                centerCoord.Latitude = centerCoord.Latitude - GetTileHeightToLatStep() * (this.Height / 256);
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == Keys.Left)
            {
                centerCoord.Longitude = centerCoord.Longitude - GetTileWidthToLonStep();
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == (Keys.Left| Keys.Control))
            {
                centerCoord.Longitude = centerCoord.Longitude - GetTileWidthToLonStep() * (this.Width/256);
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == Keys.Right)
            {
                centerCoord.Longitude = centerCoord.Longitude + GetTileWidthToLonStep();
                UpdateDLer(ZoomValue, true);
            }
            else if (keyData == (Keys.Right| Keys.Control))
            {
                centerCoord.Longitude = centerCoord.Longitude + GetTileWidthToLonStep() * (this.Width / 256);
                UpdateDLer(ZoomValue, true);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        private void CallMapChangedDelegate()
        {
            if (Changed != null)
                Changed();
        }

        internal void getBoundingBox(ref Coordinate leftBottom, ref Coordinate rightTop)
        {
            double lonStep = GetTileWidthToLonStep() * 
                (((double)this.Width / 2) / mapLoader.TileSize.Width);
            double latStep = GetTileHeightToLatStep() *
                (((double)this.Height / 2) / mapLoader.TileSize.Height);
            leftBottom.Longitude = -lonStep;
            leftBottom.Longitude += centerCoord.Longitude;
            rightTop.Longitude = lonStep + centerCoord.Longitude;
            leftBottom.Latitude = latStep + centerCoord.Latitude;
            rightTop.Latitude = centerCoord.Latitude - latStep;
        }

        internal int ZoomValue
        {
            get { 
                int n = 6;
                if (mapLoader != null)
                    n = mapLoader.ZoomValue;
                return n;
            }
        }

        #region map modification

        /// <summary>
        /// 
        /// </summary>
        /// <param name="step"></param>
        public void Zoom(int step)
        {
            int newZoom = mapLoader.ZoomValue + step;
            if (true)
            {
                UpdateDLer(newZoom, true);
            }
        }

        internal void Init(double lon, double lat, int zoom)
        {
            centerCoord.Longitude = lon;
            centerCoord.Latitude = lat;

            UpdateDLer(zoom, true);
        }

        private void UpdateDLer(int zoom, bool download)
        {
            if (mapLoader != null)
            {
                int xtile = 0;
                int ytile = 0;
                Calculator.GetTiles(centerCoord, zoom, ref xtile, ref ytile);
                mapLoader.Initialize(xtile - 2, ytile - 2, zoom, download);
            }

            CallMapChangedDelegate();
        }

        #endregion
               

        private double[] latSteps = 
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

        private double GetTileHeightToLatStep()
        {
            double latStep = latSteps[ZoomValue];
            //if (Math.Abs(latStep) < double.Epsilon)
            {
                Coordinate coord1 = Calculator.GetLonLat(mapLoader.XTileNumber, mapLoader.YTileNumber, ZoomValue);
                Coordinate coord2 = Calculator.GetLonLat(mapLoader.XTileNumber, mapLoader.YTileNumber + 1, ZoomValue);
                latStep = coord2.Latitude - coord1.Latitude;
                latSteps[ZoomValue]= latStep;
            }

            return latStep;
        }

        private double[] lonSteps = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        private double GetTileWidthToLonStep()
        {
            double lonStep = lonSteps[ZoomValue];
            //if (Math.Abs(lonStep) < double.Epsilon)
            {
                lonStep = (360.0 / Calculator.GetCountTiles(ZoomValue)); ;
                lonSteps[ZoomValue] = lonStep;
            }

            return lonStep;
        }

        #region context menu 
        
        private void ToolStripMenuItemAddOsmLayer_Click(object sender, EventArgs e)
        {
            FormSelectOsmDataSource dlg = new FormSelectOsmDataSource();
            dlg.DumpAvailable = (this.osmDumpObjects != null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OsmObjectFilter filter = null;

                if (dlg.FilterFile.Length != 0)
                {
                    FileStream file = new FileStream(dlg.FilterFile,FileMode.Open, FileAccess.Read);
                    filter = FilterHandler.CreateFilter(file);
                    file.Close();
                }

                if (dlg.Selection == DataSource.OsmOnlineApi)
                {
                    if (getDataThread.Running)
                    {
                        MessageBox.Show("please wait for finish of running api call", "Scout");
                    }
                    else
                    {
                        Coordinate leftBottom = new Coordinate(0, 0);
                        Coordinate rightTop = new Coordinate(0, 0);

                        this.getBoundingBox(ref leftBottom, ref rightTop);
                        getDataThread.Filter = filter;
                        getDataThread.LeftBottom = leftBottom;
                        getDataThread.RightTop = rightTop;
                        getDataThread.Method = ThreadGetData.METHOD.GetMapOnline;

                        Thread t = new Thread(new ThreadStart(getDataThread.ThreadProc));
                        t.Start();

                        DownloadInProgress = true;
                        CallMapChangedDelegate();
                    }

                }
                else if (dlg.Selection == DataSource.OsmDatabaseDump)
                {
                    string desc = string.Empty;
                    if (filter != null)
                    {
                        desc = filter.Description;
                    }

                    layers.Add(new OsmLayer(FilterHandler.ApplyFilter(filter, osmDumpObjects), desc));
                    this.Invalidate();
                }
            }
        }

        private void ToolStripMenuItemaddNewOsmRelationLayerDump_Click(object sender, EventArgs e)
        {
            FormSelectOsmDataSource dlg = new FormSelectOsmDataSource();
            dlg.DumpAvailable = (this.osmDumpObjects != null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Selection == DataSource.OsmOnlineApi)
                {
                    AddOnlineRelationLayer();
                }
                else if (dlg.Selection == DataSource.OsmDatabaseDump)
                {
                    FormSelectRelation dlgRel = new FormSelectRelation();
                    dlgRel.Relations = osmDumpObjects.Relations;
                    if (DialogResult.OK == dlgRel.ShowDialog())
                    {

                        layers.Add(new OsmRelationLayer(osmDumpObjects, dlgRel.SelectedRelation));
                    }

                    this.Invalidate();
                }
            }
        }

        private void AddOnlineRelationLayer()
        {
            if (getDataThread.Running)
            {
                MessageBox.Show("please wait for finish of running api call", "Scout");
            }
            else
            {
                Coordinate leftBottom = new Coordinate(0, 0);
                Coordinate rightTop = new Coordinate(0, 0);

                this.getBoundingBox(ref leftBottom, ref rightTop);
                getDataThread.Filter = null;
                getDataThread.LeftBottom = leftBottom;
                getDataThread.RightTop = rightTop;
                getDataThread.Method = ThreadGetData.METHOD.GetRelationOnline;

                Thread t = new Thread(new ThreadStart(getDataThread.ThreadProc));
                t.Start();

                DownloadInProgress = true;
                CallMapChangedDelegate();
            }
        }


        private void ToolStripMenuItemAddGpxLayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "gpx tracks (*.gpx)|*.gpx|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK )
            {
                if (getDataThread.Running)
                {
                    MessageBox.Show("please wait for finish of running api call", "Scout");
                }
                else
                {

                    getDataThread.Filter = null;
                    getDataThread.FileName = openFileDialog.FileName;
                    getDataThread.Method = ThreadGetData.METHOD.GetGpxFile;
                    Thread t = new Thread(new ThreadStart(getDataThread.ThreadProc));
                    t.Start();

                    DownloadInProgress = true;
                    CallMapChangedDelegate();
                }
            }
        }

        private void ToolStripMenuItemImportOsmDump_Click(object sender, EventArgs e)
        {
            if (getDataThread.Running)
            {
                MessageBox.Show("please wait for finish of running api call", "Scout");
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "osm dump (*.osm)|*.osm|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    getDataThread.Filter = null;
                    getDataThread.FileName = openFileDialog.FileName;
                    getDataThread.Method = ThreadGetData.METHOD.LoadDump;
                    Thread t = new Thread(new ThreadStart(getDataThread.ThreadProc));
                    t.Start();

                    DownloadInProgress = true;
                    CallMapChangedDelegate();
                }
            }
        }

        private void ToolStripMenuItemRemoveTopLayer_Click(object sender, EventArgs e)
        {
            if (layers.Count > 0)
            {
                layers.RemoveAt(layers.Count - 1);
                this.Invalidate();
            }
        }

        delegate void Invoker();


        /// <summary>
        /// 
        /// </summary>
        public void ThreadGetDataFinishedCallback()
        {
            if (this.InvokeRequired)
            {
                // Execute the same method, but this time on the GUI thread
                this.BeginInvoke(new Invoker(ThreadGetDataFinishedCallback) );

                // we return immediately
                return;
            }

            if ((getDataThread.ErrorMessage != null) && (getDataThread.ErrorMessage.Length > 0))
            {
                MessageBox.Show(getDataThread.ErrorMessage, "Scout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (( getDataThread.Message != null) && (getDataThread.Message.Length > 0))
                {
                    MessageBox.Show(getDataThread.Message, "Scout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (getDataThread.Method == ThreadGetData.METHOD.LoadDump)
                {
                    osmDumpObjects = getDataThread.OsmDumpData;
                }
                else
                {
                    if (getDataThread.Layer != null)
                    {
                        layers.Add(getDataThread.Layer);
                    }
                }
            }
            
            DownloadInProgress = false;
            Invalidate();
        }

        #endregion

    }
}
