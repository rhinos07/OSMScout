using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace OpenStreetMapScout
{
    public partial class FormAboutBox : Form
    {
        /// <summary>
        /// construcotr
        /// </summary>
        public FormAboutBox()
        {
            InitializeComponent();

            this.labelVersionNumber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();


            Stream stream = this.GetType().Assembly.GetManifestResourceStream("OpenStreetMapScout.Copyrights.rtf");
            if (stream != null)
            {
                StreamReader sr = new StreamReader(stream);
                richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
                sr.Close();
            }
        }
    }
}
