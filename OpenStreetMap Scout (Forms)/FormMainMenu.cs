using System.Windows.Forms;
using OpenStreetMapPictures;

namespace OpenStreetMapScout
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMainMenu : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            FormSlippyMap dlgSelect = new FormSlippyMap();

            dlgSelect.Show();
        }

        private void buttonExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void buttonAbout_Click(object sender, System.EventArgs e)
        {
            FormAboutBox aBox = new FormAboutBox();

            aBox.ShowDialog();
        }
    }
}
