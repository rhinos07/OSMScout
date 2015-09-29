namespace OpenStreetMapScout.SlippyMap
{
    partial class FormSelectOsmDataSource
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectOsmDataSource));
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDump = new System.Windows.Forms.RadioButton();
            this.radioButtonOnline = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonFilterFile = new System.Windows.Forms.Button();
            this.textBoxFilterFile = new System.Windows.Forms.TextBox();
            this.checkBoxNoFilter = new System.Windows.Forms.CheckBox();
            this.openFilterFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(230, 180);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 25);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(136, 180);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 25);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonDump);
            this.groupBox1.Controls.Add(this.radioButtonOnline);
            this.groupBox1.Location = new System.Drawing.Point(16, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 80);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "data souce";
            // 
            // radioButtonDump
            // 
            this.radioButtonDump.AutoSize = true;
            this.radioButtonDump.Location = new System.Drawing.Point(15, 47);
            this.radioButtonDump.Name = "radioButtonDump";
            this.radioButtonDump.Size = new System.Drawing.Size(208, 17);
            this.radioButtonDump.TabIndex = 1;
            this.radioButtonDump.TabStop = true;
            this.radioButtonDump.Text = "complete OSM database dump (offline)";
            this.radioButtonDump.UseVisualStyleBackColor = true;
            // 
            // radioButtonOnline
            // 
            this.radioButtonOnline.AutoSize = true;
            this.radioButtonOnline.Location = new System.Drawing.Point(15, 24);
            this.radioButtonOnline.Name = "radioButtonOnline";
            this.radioButtonOnline.Size = new System.Drawing.Size(257, 17);
            this.radioButtonOnline.TabIndex = 0;
            this.radioButtonOnline.TabStop = true;
            this.radioButtonOnline.Text = "OpenStreetMap.org Api (online) (only visible area)";
            this.radioButtonOnline.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonFilterFile);
            this.groupBox2.Controls.Add(this.textBoxFilterFile);
            this.groupBox2.Controls.Add(this.checkBoxNoFilter);
            this.groupBox2.Location = new System.Drawing.Point(19, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 71);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "filter";
            // 
            // buttonFilterFile
            // 
            this.buttonFilterFile.Enabled = false;
            this.buttonFilterFile.Location = new System.Drawing.Point(242, 17);
            this.buttonFilterFile.Name = "buttonFilterFile";
            this.buttonFilterFile.Size = new System.Drawing.Size(27, 23);
            this.buttonFilterFile.TabIndex = 2;
            this.buttonFilterFile.Text = "...";
            this.buttonFilterFile.UseVisualStyleBackColor = true;
            this.buttonFilterFile.Click += new System.EventHandler(this.ButtonFilterFile_Click);
            // 
            // textBoxFilterFile
            // 
            this.textBoxFilterFile.Enabled = false;
            this.textBoxFilterFile.Location = new System.Drawing.Point(14, 19);
            this.textBoxFilterFile.Name = "textBoxFilterFile";
            this.textBoxFilterFile.Size = new System.Drawing.Size(222, 20);
            this.textBoxFilterFile.TabIndex = 1;
            // 
            // checkBoxNoFilter
            // 
            this.checkBoxNoFilter.AutoSize = true;
            this.checkBoxNoFilter.Checked = true;
            this.checkBoxNoFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNoFilter.Location = new System.Drawing.Point(14, 45);
            this.checkBoxNoFilter.Name = "checkBoxNoFilter";
            this.checkBoxNoFilter.Size = new System.Drawing.Size(60, 17);
            this.checkBoxNoFilter.TabIndex = 0;
            this.checkBoxNoFilter.Text = "no filter";
            this.checkBoxNoFilter.UseVisualStyleBackColor = true;
            this.checkBoxNoFilter.CheckedChanged += new System.EventHandler(this.checkBoxNoFilter_CheckedChanged);
            // 
            // openFilterFileDialog
            // 
            this.openFilterFileDialog.FileName = "openFileDialog1";
            // 
            // FormSelectOsmDataSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 217);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormSelectOsmDataSource";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "select data source";
            this.Load += new System.EventHandler(this.FormSelectOsmDataSource_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSelectOsmDataSource_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonDump;
        private System.Windows.Forms.RadioButton radioButtonOnline;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFilterFile;
        private System.Windows.Forms.TextBox textBoxFilterFile;
        private System.Windows.Forms.CheckBox checkBoxNoFilter;
        private System.Windows.Forms.OpenFileDialog openFilterFileDialog;
    }
}