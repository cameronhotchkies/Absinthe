namespace Absinthe.Gui
{
    partial class ctlAboutContents
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblAuthors = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(0, 154);
            this.txtLicense.Multiline = true;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicense.Size = new System.Drawing.Size(568, 242);
            this.txtLicense.TabIndex = 14;
            // 
            // lblLicense
            // 
            this.lblLicense.AutoSize = true;
            this.lblLicense.Location = new System.Drawing.Point(-3, 134);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(61, 17);
            this.lblLicense.TabIndex = 13;
            this.lblLicense.Text = "License:";
            // 
            // lblAuthors
            // 
            this.lblAuthors.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAuthors.AutoSize = true;
            this.lblAuthors.Location = new System.Drawing.Point(3, 103);
            this.lblAuthors.Name = "lblAuthors";
            this.lblAuthors.Size = new System.Drawing.Size(139, 17);
            this.lblAuthors.TabIndex = 12;
            this.lblAuthors.Text = "Authors: Anonymous";
            this.lblAuthors.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(3, 86);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(148, 17);
            this.lblCopyright.TabIndex = 11;
            this.lblCopyright.Text = "Copyright (c) 0x90.org";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAppName
            // 
            this.lblAppName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAppName.AutoSize = true;
            this.lblAppName.Location = new System.Drawing.Point(3, 18);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(118, 17);
            this.lblAppName.TabIndex = 9;
            this.lblAppName.Text = "Application Name";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Location = new System.Drawing.Point(3, 35);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(127, 17);
            this.lblAppTitle.TabIndex = 10;
            this.lblAppTitle.Text = "Title Of Application";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctlAboutContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtLicense);
            this.Controls.Add(this.lblLicense);
            this.Controls.Add(this.lblAuthors);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblAppTitle);
            this.Name = "ctlAboutContents";
            this.Size = new System.Drawing.Size(568, 401);
            this.Load += new System.EventHandler(this.ctlAboutContents_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.Label lblAuthors;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblAppTitle;

    }
}
