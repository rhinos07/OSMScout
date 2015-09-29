using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OpenStreetMapPictures.TilesDownload;
using OpenSteetMapApi;
using System.Globalization;
using OpenStreetMapScout;


namespace OpenStreetMapPictures {
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSlippyMap : Form {

        private const String imagefilefilter = "PNG (*.png)|*.png";

        private FormSettings setDlg = new FormSettings();
        
        private IDownloadTiles tiles;

        private System.Windows.Forms.Timer ClearOldCacheTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// constructor
        /// </summary>
        public FormSlippyMap() {
            InitializeComponent();

            ClearOldCacheTimer.Enabled = true;
            ClearOldCacheTimer.Interval = 1000 * 30;
            ClearOldCacheTimer.Tick += new EventHandler(ClearOldCacheTimer_Tick);

            tiles = new DownloadTilesImpl();

            double lon = Convert.ToDouble(Application.UserAppDataRegistry.GetValue("SelectBBox_lon", "9,4859").ToString(), CultureInfo.CurrentCulture);
            double lat = Convert.ToDouble(Application.UserAppDataRegistry.GetValue("SelectBBox_lat", "49,2791").ToString(), CultureInfo.CurrentCulture);
            int zoom = Convert.ToInt32(Application.UserAppDataRegistry.GetValue("SelectBBox_zoom", "6").ToString());


            this.mapControl.MapLoader = tiles;
            this.mapControl.Init(lon, lat, zoom );

            this.mapControl.Changed = new MapControl.MapChangedDelegate( UpdateStatusBar );

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            string status = this.mapControl.CenterCoordinate.Longitude.ToString("F4", CultureInfo.CurrentCulture) + " - " + this.mapControl.CenterCoordinate.Latitude.ToString("F4", CultureInfo.CurrentCulture);
            this.toolStripStatusLabel1.Text = status + " Zoom:" + this.mapControl.ZoomValue.ToString();

            if (this.mapControl.DownloadInProgress)
            {
                this.toolStripProgressBar.Visible = true;
                this.timerProgressStepper.Enabled = true;
            }
            else
            {
                this.toolStripProgressBar.Visible = false;
                this.timerProgressStepper.Enabled = false;
                this.toolStripProgressBar.Value = 0;
            }

            this.toolStripStatusLabel1.Invalidate();

            this.mapControl.Focus();
        }

        private void ClearOldCacheTimer_Tick(object sender, EventArgs e) {
            this.ClearOldCacheTimer.Enabled = false;
            Thread clearThread = new Thread(new ThreadStart(clearOldCachedFiles));
            clearThread.IsBackground = true;
            clearThread.Priority = ThreadPriority.Lowest;
            clearThread.Start();  
        }


        private void DeleteCacheToolStripMenuItem_Click(object sender, EventArgs e) {
            Thread tr = new Thread(new ThreadStart(deleteCacheComplete));
            tr.IsBackground = true;
            tr.Start();
        }

        private void deleteCacheComplete() {
            if (Directory.Exists(Utils.MapCachePath))
            {
                try {
                    Directory.Delete(Utils.MapCachePath, true);
                } catch (IOException) { }
            }
        }

        private void clearOldCachedFiles() {
            try {

                String[] allfiles = Directory.GetFiles(Utils.MapCachePath, "*.png", SearchOption.AllDirectories);

                for (int i = 0; i < allfiles.Length; i++) {
                    DateTime dt = File.GetLastWriteTime(allfiles[i]);
                    TimeSpan t = DateTime.Now - dt;

                    // delete pictures older than a week
                    if (t.TotalDays > 7) {
                        try 
                        {
                            File.Delete(allfiles[i]);
                        } 
                        catch (Exception ) { }
                    }
                }

            } catch (Exception ) {
                //log.Warn("clear old cache " + ex.Message);
            }
        }

        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e) {

            System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
            save.Filter = imagefilefilter;

            if (save.ShowDialog() == DialogResult.OK) 
            {
                this.mapControl.ExportAsPng(save.FileName);
            }

            save.Dispose();
        }


        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Application.UserAppDataRegistry.SetValue("SelectBBox_lon", mapControl.CenterCoordinate.Longitude.ToString(CultureInfo.CurrentCulture));
            Application.UserAppDataRegistry.SetValue("SelectBBox_lat", mapControl.CenterCoordinate.Latitude.ToString(CultureInfo.CurrentCulture));
            Application.UserAppDataRegistry.SetValue("SelectBBox_zoom", mapControl.ZoomValue.ToString());

            Close();
        }

        private void MoveToCoordinateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEnterCoordinate dlg = new FormEnterCoordinate();

            dlg.Longitude = mapControl.CenterCoordinate.Longitude;
            dlg.Latitude = mapControl.CenterCoordinate.Latitude;

            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                this.mapControl.Init(dlg.Longitude, dlg.Latitude, this.mapControl.ZoomValue);
            }
        }

        private void TimerProgressStepper_Tick(object sender, EventArgs e)
        {
            if (this.toolStripProgressBar.Value > this.toolStripProgressBar.Maximum - this.toolStripProgressBar.Step)
            {
                this.toolStripProgressBar.Value = 0;
            }

            this.toolStripProgressBar.PerformStep();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutBox aBox = new FormAboutBox();

            aBox.ShowDialog();
        }

        private void backgroundMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setDlg.ShowDialog();

            if (setDlg.DialogResult == DialogResult.OK)
            {
                DownloadSettings ds = setDlg.DownloadSettings;
                this.mapControl.UpdateSettings(ds);
            }

            setDlg.Hide();
        }
    }
}