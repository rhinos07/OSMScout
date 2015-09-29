using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenStreetMapScout.SlippyMap
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSelectOsmDataSource : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormSelectOsmDataSource()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DumpAvailable
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FilterFile 
        {
            get
            {
                if (this.checkBoxNoFilter.Checked)
                {
                    return string.Empty;
                }
                else
                {
                    return this.textBoxFilterFile.Text;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public DataSource Selection
        {
            set;
            get;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (this.radioButtonOnline.Checked)
            {
                Selection = DataSource.OsmOnlineApi;
            }
            else
            {
                Selection = DataSource.OsmDatabaseDump;
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormSelectOsmDataSource_Load(object sender, EventArgs e)
        {
            if (DumpAvailable)
            {
                this.radioButtonDump.Select();
            }
            else
            {
                this.radioButtonOnline.Select();
                this.radioButtonDump.Enabled = false;
            }

            this.buttonOK.Select();
        }

        private void ButtonFilterFile_Click(object sender, EventArgs e)
        {
            openFilterFileDialog.Filter = "filter definitions (*.filter)|*.filter|All files (*.*)|*.*";
            openFilterFileDialog.RestoreDirectory = true;
            if ( openFilterFileDialog.ShowDialog() == DialogResult.OK )
            {
                this.textBoxFilterFile.Text = openFilterFileDialog.FileName;
            }
        }

        private void checkBoxNoFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNoFilter.Checked)
            {
                this.textBoxFilterFile.Enabled = false;
                this.buttonFilterFile.Enabled = false;
            }
            else
            {
                this.textBoxFilterFile.Enabled = true;
                this.buttonFilterFile.Enabled = true;
            }
        }


        private void FormSelectOsmDataSource_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum DataSource
    {
        /// <summary>
        /// 
        /// </summary>
        OsmOnlineApi,
        /// <summary>
        /// 
        /// </summary>
        OsmDatabaseDump
    }

}
