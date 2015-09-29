namespace OpenStreetMapPictures {

    /// <summary>
    /// control manages and shows map tiles
    /// </summary>
    partial class MapControl {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();

                if (backBuffer != null)
                    backBuffer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapControl));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemImportOsmDump = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadOsmLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemaddNewOsmRelationLayerDump = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadGpxLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRemoveTopLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemImportOsmDump,
            this.toolStripMenuItemLoadOsmLayer,
            this.toolStripMenuItemaddNewOsmRelationLayerDump,
            this.toolStripMenuItemLoadGpxLayer,
            this.toolStripMenuItemRemoveTopLayer});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(217, 136);
            // 
            // toolStripMenuItemImportOsmDump
            // 
            this.toolStripMenuItemImportOsmDump.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemImportOsmDump.Image")));
            this.toolStripMenuItemImportOsmDump.Name = "toolStripMenuItemImportOsmDump";
            this.toolStripMenuItemImportOsmDump.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemImportOsmDump.Text = "import osm dump";
            this.toolStripMenuItemImportOsmDump.Click += new System.EventHandler(this.ToolStripMenuItemImportOsmDump_Click);
            // 
            // toolStripMenuItemLoadOsmLayer
            // 
            this.toolStripMenuItemLoadOsmLayer.Image = global::OpenStreetMapScout.Properties.Resources.list_add;
            this.toolStripMenuItemLoadOsmLayer.Name = "toolStripMenuItemLoadOsmLayer";
            this.toolStripMenuItemLoadOsmLayer.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemLoadOsmLayer.Text = "add new osm object layer";
            this.toolStripMenuItemLoadOsmLayer.Click += new System.EventHandler(this.ToolStripMenuItemAddOsmLayer_Click);
            // 
            // toolStripMenuItemaddNewOsmRelationLayerDump
            // 
            this.toolStripMenuItemaddNewOsmRelationLayerDump.Image = global::OpenStreetMapScout.Properties.Resources.list_add;
            this.toolStripMenuItemaddNewOsmRelationLayerDump.Name = "toolStripMenuItemaddNewOsmRelationLayerDump";
            this.toolStripMenuItemaddNewOsmRelationLayerDump.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemaddNewOsmRelationLayerDump.Text = "add new osm relation layer";
            this.toolStripMenuItemaddNewOsmRelationLayerDump.Click += new System.EventHandler(this.ToolStripMenuItemaddNewOsmRelationLayerDump_Click);
            // 
            // toolStripMenuItemLoadGpxLayer
            // 
            this.toolStripMenuItemLoadGpxLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemLoadGpxLayer.Image")));
            this.toolStripMenuItemLoadGpxLayer.Name = "toolStripMenuItemLoadGpxLayer";
            this.toolStripMenuItemLoadGpxLayer.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemLoadGpxLayer.Text = "add new gpx objects layer";
            this.toolStripMenuItemLoadGpxLayer.Click += new System.EventHandler(this.ToolStripMenuItemAddGpxLayer_Click);
            // 
            // toolStripMenuItemRemoveTopLayer
            // 
            this.toolStripMenuItemRemoveTopLayer.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemRemoveTopLayer.Image")));
            this.toolStripMenuItemRemoveTopLayer.Name = "toolStripMenuItemRemoveTopLayer";
            this.toolStripMenuItemRemoveTopLayer.Size = new System.Drawing.Size(216, 22);
            this.toolStripMenuItemRemoveTopLayer.Text = "remove top layer";
            this.toolStripMenuItemRemoveTopLayer.Click += new System.EventHandler(this.ToolStripMenuItemRemoveTopLayer_Click);
            // 
            // MapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(256, 256);
            this.Name = "MapControl";
            this.Size = new System.Drawing.Size(256, 256);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseMove);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadOsmLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadGpxLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemoveTopLayer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemaddNewOsmRelationLayerDump;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImportOsmDump;
    }
}
