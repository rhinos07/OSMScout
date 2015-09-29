using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenStreetMapPictures {

    /// <summary>
    /// change settings dialog
    /// </summary>
    public partial class FormSettings : Form {

        private DownloadSettings mysettings = new DownloadSettings();
        
        /// <summary>
        /// constructor
        /// </summary>
        public FormSettings() {
            InitializeComponent();

            this.comboBoxRenderer.Items.Clear();
            this.comboBoxRenderer.Items.AddRange(DownloadSettings.PossibleRenderers.ToArray());

            this.comboBoxRenderer.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public DownloadSettings DownloadSettings {
            get { return this.mysettings; }
            set { this.mysettings = value;
            this.comboBoxRenderer.SelectedItem = this.mysettings.Renderer;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e) {

            this.mysettings.Renderer = (Renderer) this.comboBoxRenderer.SelectedItem;
        }
    }
}