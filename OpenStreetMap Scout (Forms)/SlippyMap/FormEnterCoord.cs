using System;
using System.Windows.Forms;
using OpenSteetMapApi;

namespace OpenStreetMapPictures
{
    public partial class FormEnterCoordinate : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public FormEnterCoordinate()
        {
            InitializeComponent();
        }

        private Coordinate myPos = new Coordinate(0, 0);

        /// <summary>
        /// entred longitude
        /// </summary>
        public double Longitude
        {
            get { return myPos.Longitude; }
            set { myPos.Longitude = value; }
        }

        /// <summary>
        /// entered latitude
        /// </summary>
        public double Latitude
        {
            get { return myPos.Latitude; }
            set { myPos.Latitude = value;}
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
