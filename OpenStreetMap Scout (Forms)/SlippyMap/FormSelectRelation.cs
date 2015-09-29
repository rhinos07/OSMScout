using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenSteetMapApi;

namespace OpenStreetMapScout.SlippyMap
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSelectRelation : Form
    {
        /// <summary>
        /// constructor
        /// </summary>
        public FormSelectRelation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Relation[] Relations
        {
            set { relations = value; }
        }
        private Relation[] relations;

        /// <summary>
        /// 
        /// </summary>
        public Relation SelectedRelation
        {
            get {
                int id = GetId((string)this.comboBox1.SelectedItem);
                Relation myId = new Relation();
                myId.Id = id;
                int index = Array.BinarySearch(relations, myId);
                return relations[index]; 
            }
        }

        private static int GetId(string selectedText)
        {
            int idnumber = 0;
            string idstring = selectedText.Substring(selectedText.LastIndexOf("(")+1);
            idstring = idstring.Substring(0, idstring.IndexOf(")"));
            try
            {
                idnumber = Convert.ToInt32(idstring);
            }
            catch(FormatException)
            {
            }
            catch (OverflowException)
            {
            }
            return idnumber;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectRelation_Load(object sender, EventArgs e)
        {
            if (relations != null)
            {
                List<string> relationNames = new List<string>();

                try
                {
                    foreach (Relation rel in relations)
                    {
                        string id = rel.Id.ToString();
                        string name = string.Empty;
                        string network = string.Empty;

                        if (rel.Tags != null)
                        {
                            foreach (Tag tag in rel.Tags)
                            {
                                if (tag.Key == "name")
                                {
                                    name = tag.Value;
                                }
                                else if (tag.Key == "network")
                                {
                                    network = tag.Value;
                                }
                            }
                        }

                        // only named relations should be shown 
                        if ((network.Length > 0) || (name.Length > 0))
                            relationNames.Add(network + " - " + name + " (" + id + ") ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + " " + ex.StackTrace);
                }

                relationNames.Sort();

                this.comboBox1.Items.AddRange(relationNames.ToArray());

                this.comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("no relations found", "osm objects", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                buttonCancel_Click(this, null);
            }
        }


    }
}
