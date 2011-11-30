namespace Absinthe.Gui
{
    partial class frmAbout
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
            this.lblPlugins = new System.Windows.Forms.Label();
            this.txtPluginList = new System.Windows.Forms.TextBox();
            this.butOk = new System.Windows.Forms.Button();
            this.ctlAboutContents1 = new ctlAboutContents();
            this.SuspendLayout();
            // 
            // lblPlugins
            // 
            this.lblPlugins.AutoSize = true;
            this.lblPlugins.Location = new System.Drawing.Point(13, 420);
            this.lblPlugins.Name = "lblPlugins";
            this.lblPlugins.Size = new System.Drawing.Size(58, 17);
            this.lblPlugins.TabIndex = 1;
            this.lblPlugins.Text = "Plugins:";
            // 
            // txtPluginList
            // 
            this.txtPluginList.Location = new System.Drawing.Point(16, 441);
            this.txtPluginList.Multiline = true;
            this.txtPluginList.Name = "txtPluginList";
            this.txtPluginList.ReadOnly = true;
            this.txtPluginList.Size = new System.Drawing.Size(564, 205);
            this.txtPluginList.TabIndex = 2;
            // 
            // butOk
            // 
            this.butOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butOk.Location = new System.Drawing.Point(263, 652);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 3;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            // 
            // ctlAboutContents1
            // 
            this.ctlAboutContents1.Location = new System.Drawing.Point(12, 12);
            this.ctlAboutContents1.Name = "ctlAboutContents1";
            this.ctlAboutContents1.Size = new System.Drawing.Size(568, 401);
            this.ctlAboutContents1.TabIndex = 0;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 685);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.txtPluginList);
            this.Controls.Add(this.lblPlugins);
            this.Controls.Add(this.ctlAboutContents1);
            this.Name = "frmAbout";
            this.Text = "About Absinthe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctlAboutContents ctlAboutContents1;
        private System.Windows.Forms.Label lblPlugins;
        private System.Windows.Forms.TextBox txtPluginList;
        private System.Windows.Forms.Button butOk;
    }
}