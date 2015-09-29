namespace OpenStreetMapPictures {
    partial class FormSlippyMap {
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
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSlippyMap));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToCoordinateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListOnline = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.timerProgressStepper = new System.Windows.Forms.Timer(this.components);
            this.mapControl = new OpenStreetMapPictures.MapControl();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.navigateToolStripMenuItem,
            this.settingsToolStripMenuItem1,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(592, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsImageToolStripMenuItem,
            this.closeMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsImageToolStripMenuItem
            // 
            this.saveAsImageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsImageToolStripMenuItem.Image")));
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveAsImageToolStripMenuItem.Text = "Save as Image";
            this.saveAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageToolStripMenuItem_Click);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeMenuItem.Image")));
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(199, 22);
            this.closeMenuItem.Text = "Save Position and Close";
            this.closeMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // navigateToolStripMenuItem
            // 
            this.navigateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToCoordinateToolStripMenuItem});
            this.navigateToolStripMenuItem.Name = "navigateToolStripMenuItem";
            this.navigateToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.navigateToolStripMenuItem.Text = "Navigate";
            // 
            // moveToCoordinateToolStripMenuItem
            // 
            this.moveToCoordinateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("moveToCoordinateToolStripMenuItem.Image")));
            this.moveToCoordinateToolStripMenuItem.Name = "moveToCoordinateToolStripMenuItem";
            this.moveToCoordinateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.moveToCoordinateToolStripMenuItem.Text = "&Move to Coordinate";
            this.moveToCoordinateToolStripMenuItem.Click += new System.EventHandler(this.MoveToCoordinateToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundMapToolStripMenuItem,
            this.deleteCacheToolStripMenuItem});
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // backgroundMapToolStripMenuItem
            // 
            this.backgroundMapToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("backgroundMapToolStripMenuItem.Image")));
            this.backgroundMapToolStripMenuItem.Name = "backgroundMapToolStripMenuItem";
            this.backgroundMapToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.backgroundMapToolStripMenuItem.Text = "Background Map Options";
            this.backgroundMapToolStripMenuItem.Click += new System.EventHandler(this.backgroundMapToolStripMenuItem_Click);
            // 
            // deleteCacheToolStripMenuItem
            // 
            this.deleteCacheToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteCacheToolStripMenuItem.Image")));
            this.deleteCacheToolStripMenuItem.Name = "deleteCacheToolStripMenuItem";
            this.deleteCacheToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.deleteCacheToolStripMenuItem.Text = "Delete Map Cache";
            this.deleteCacheToolStripMenuItem.Click += new System.EventHandler(this.DeleteCacheToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemHelp.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.aboutToolStripMenuItem.Text = "About OSM Scout";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // imageListOnline
            // 
            this.imageListOnline.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOnline.ImageStream")));
            this.imageListOnline.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOnline.Images.SetKeyName(0, "online.png");
            this.imageListOnline.Images.SetKeyName(1, "offline.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 344);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(592, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "status";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Maximum = 1000;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // timerProgressStepper
            // 
            this.timerProgressStepper.Tick += new System.EventHandler(this.TimerProgressStepper_Tick);
            // 
            // mapControl
            // 
            this.mapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapControl.AutoSize = true;
            this.mapControl.BackColor = System.Drawing.Color.White;
            this.mapControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mapControl.DownloadInProgress = false;
            this.mapControl.Location = new System.Drawing.Point(0, 22);
            this.mapControl.MinimumSize = new System.Drawing.Size(256, 256);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(592, 344);
            this.mapControl.TabIndex = 4;
            // 
            // FormSlippyMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 366);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormSlippyMap";
            this.Text = "OpenStreetMap Scout";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private MapControl mapControl;
        private System.Windows.Forms.ToolStripMenuItem deleteCacheToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListOnline;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem navigateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToCoordinateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Timer timerProgressStepper;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundMapToolStripMenuItem;
    }
}

