namespace OpenStreetMapScout.SlippyMap
{
    partial class FormObjectDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormObjectDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.listBoxProperties = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "object type:";
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(88, 5);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(120, 20);
            this.textBoxType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "id:";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(256, 5);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.ReadOnly = true;
            this.textBoxId.Size = new System.Drawing.Size(106, 20);
            this.textBoxId.TabIndex = 3;
            // 
            // listBoxProperties
            // 
            this.listBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxProperties.FormattingEnabled = true;
            this.listBoxProperties.Location = new System.Drawing.Point(15, 58);
            this.listBoxProperties.Name = "listBoxProperties";
            this.listBoxProperties.Size = new System.Drawing.Size(347, 173);
            this.listBoxProperties.TabIndex = 4;
            this.listBoxProperties.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.listBoxProperties_PreviewKeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "properties:";
            // 
            // FormObjectDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 246);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxProperties);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormObjectDetails";
            this.Text = "object details";
            this.Load += new System.EventHandler(this.FormObjectDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.ListBox listBoxProperties;
        private System.Windows.Forms.Label label3;

    }
}