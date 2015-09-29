namespace OpenStreetMapPictures
{
    /// <summary>
    /// dlg to enter a coordiante
    /// </summary>
    partial class FormEnterCoordinate
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnterCoordinate));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxLat = new System.Windows.Forms.TextBox();
            this.textBoxLong = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.enterCoordinateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enterCoordinateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Longitude:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Latitude:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLat);
            this.groupBox1.Controls.Add(this.textBoxLong);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 86);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // textBoxLat
            // 
            this.textBoxLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this, "Latitude", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N5"));
            this.textBoxLat.Location = new System.Drawing.Point(69, 53);
            this.textBoxLat.Name = "textBoxLat";
            this.textBoxLat.Size = new System.Drawing.Size(161, 20);
            this.textBoxLat.TabIndex = 2;
            this.textBoxLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxLong
            // 
            this.textBoxLong.DataBindings.Add(new System.Windows.Forms.Binding("Text", this, "Longitude", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N5"));
            this.textBoxLong.Location = new System.Drawing.Point(69, 18);
            this.textBoxLong.Name = "textBoxLong";
            this.textBoxLong.Size = new System.Drawing.Size(162, 20);
            this.textBoxLong.TabIndex = 1;
            this.textBoxLong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(179, 104);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 25);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // enterCoordinateBindingSource
            // 
            this.enterCoordinateBindingSource.DataSource = typeof(OpenStreetMapPictures.FormEnterCoordinate);
            // 
            // FormEnterCoordinate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 141);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.enterCoordinateBindingSource, "Longitude", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N4"));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEnterCoordinate";
            this.Text = "enter coordinate";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enterCoordinateBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxLat;
        private System.Windows.Forms.TextBox textBoxLong;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.BindingSource enterCoordinateBindingSource;
    }
}