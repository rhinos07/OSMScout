using System;
using System.Collections.Generic;
using System.Text;
using OpenSteetMapApi;
using System.Windows.Forms;
using System.IO;

namespace OpenStreetMapScout.SlippyMap
{
    class ThreadGetData
    {
        public OsmObjectFilter Filter 
        { 
            set { filter = value; } 
        }
        private OsmObjectFilter filter;

        private GetDataFinishedCallback callback;

        public enum METHOD
        {
            GetMapOnline,
            GetRelationOnline,
            GetGpxFile,
            LoadDump
        }

        public METHOD Method { get; set; }

        public bool Running { get { return running; } }
        private bool running = false;

        public ILayer Layer = null;

        public OsmTree OsmDumpData = null;

        public string ErrorMessage { get; set; }

        public string Message { get; set; }

        public string FileName { get; set; }

        public Coordinate LeftBottom
        {
            set;
            get;
        }

        public Coordinate RightTop
        {
            set;
            get;
        }

        public ThreadGetData(GetDataFinishedCallback callbackDelegate) 
        {
            callback = callbackDelegate;
        }
   
        public void ThreadProc()
        {
            running = true;
            ErrorMessage = string.Empty;
            Message = string.Empty;
            Layer = null;
            
            string layerDescription = "new layer";
            if (filter != null)
                layerDescription = filter.Description;

            try
            {
                switch (Method)
                {
                    case METHOD.GetMapOnline:
                        GetMapOnline(layerDescription);
                        break;
                    case METHOD.GetRelationOnline:
                        GetRelationOnline();
                        break;
                    case METHOD.GetGpxFile:
                        GetGpxFile();
                        break;
                    case METHOD.LoadDump:
                        LoadDump();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + "\n" + ex.ToString();
                //MessageBox.Show(ErrorMessage, "Scout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (callback != null)
                callback();

            running = false;
        }

        private void GetRelationOnline()
        {
            OsmTree osmObjects = Osm.GetMap(LeftBottom.Longitude, LeftBottom.Latitude,
                                            RightTop.Longitude, RightTop.Latitude);

            FormSelectRelation dlg = new FormSelectRelation();
            dlg.Relations = osmObjects.Relations;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                OsmTree osmTreeRelation = Osm.GetRelation(dlg.SelectedRelation.Id);
                Layer = (new OsmRelationLayer(osmTreeRelation, dlg.SelectedRelation));
            }
        }

        private void GetMapOnline(string layerDescription)
        {
            OsmTree osmObjects = Osm.GetMap(LeftBottom.Longitude, LeftBottom.Latitude,
                                                RightTop.Longitude, RightTop.Latitude);
            Layer = new OsmLayer(FilterHandler.ApplyFilter(filter, osmObjects), layerDescription);
        }

        private void GetGpxFile()
        {
            FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            GpxTree gt = Gpx.LoadByStream(file);
            file.Close();
            Layer = new GpxLayer(gt, FileName, true);
        }

        private void LoadDump()
        {
            if (OsmDumpData != null)
            {
                OsmDumpData = null;
            }
            FileStream file = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            OsmDumpData = Osm.LoadByStream(file);
            file.Close();
            OsmDumpData.Descripton = FileName;

            Message = "import of database succeeded";
        }
    }

    /// <summary>
    /// callback when thread is finished
    /// </summary>
    public delegate void GetDataFinishedCallback();
}
