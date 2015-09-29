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
    /// show details of object
    /// </summary>
    public partial class FormObjectDetails : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public FormObjectDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// object to show
        /// </summary>
        public object Object
        {
            get;
            set;
        }

        private void FormObjectDetails_Load(object sender, EventArgs e)
        {
            ParseObject();

            this.listBoxProperties.Select();
            this.listBoxProperties.Focus();
        }

        private void ParseObject()
        {
            if ( Object.GetType() == typeof(OpenSteetMapApi.Node))
            {
                OpenSteetMapApi.Node node = (OpenSteetMapApi.Node)Object;
                this.textBoxType.Text  = "OSM.Node";
                this.textBoxId.Text = node.Id.ToString();

                this.listBoxProperties.Items.Add("longitude:\t" + node.Lon.ToString());
                this.listBoxProperties.Items.Add("latitude:\t\t" + node.Lat.ToString());
                this.listBoxProperties.Items.Add("user:    \t\t" + node.User );
                this.listBoxProperties.Items.Add("timestamp: \t" + node.Timestamp );

                if (node.Tags != null)
                {
                    foreach (OpenSteetMapApi.Tag tag in node.Tags)
                    {
                        this.listBoxProperties.Items.Add(tag.Key + ":  \t" + tag.Value);
                    }
                }
            }
        }

        private void listBoxProperties_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
