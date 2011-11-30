namespace Absinthe.Gui
{
    partial class ctlConnection
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
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblConnectionMethod = new System.Windows.Forms.Label();
            this.optConnectGet = new System.Windows.Forms.RadioButton();
            this.optConnectPost = new System.Windows.Forms.RadioButton();
            this.chkUseSsl = new System.Windows.Forms.CheckBox();
            this.lblTargetUrl = new System.Windows.Forms.Label();
            this.lblConnectionProtocol = new System.Windows.Forms.Label();
            this.txtTargetUrl = new System.Windows.Forms.TextBox();
            this.grpConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblConnectionMethod);
            this.grpConnection.Controls.Add(this.optConnectGet);
            this.grpConnection.Controls.Add(this.optConnectPost);
            this.grpConnection.Controls.Add(this.chkUseSsl);
            this.grpConnection.Controls.Add(this.lblTargetUrl);
            this.grpConnection.Controls.Add(this.txtTargetUrl);
            this.grpConnection.Controls.Add(this.lblConnectionProtocol);
            this.grpConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpConnection.Location = new System.Drawing.Point(0, 0);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(538, 91);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection:";
            // 
            // lblConnectionMethod
            // 
            this.lblConnectionMethod.AutoSize = true;
            this.lblConnectionMethod.Location = new System.Drawing.Point(6, 51);
            this.lblConnectionMethod.Name = "lblConnectionMethod";
            this.lblConnectionMethod.Size = new System.Drawing.Size(134, 17);
            this.lblConnectionMethod.TabIndex = 6;
            this.lblConnectionMethod.Text = "Connection Method:";
            // 
            // optConnectGet
            // 
            this.optConnectGet.AutoSize = true;
            this.optConnectGet.Location = new System.Drawing.Point(147, 49);
            this.optConnectGet.Name = "optConnectGet";
            this.optConnectGet.Size = new System.Drawing.Size(52, 21);
            this.optConnectGet.TabIndex = 5;
            this.optConnectGet.TabStop = true;
            this.optConnectGet.Text = "Get";
            this.optConnectGet.UseVisualStyleBackColor = true;
            // 
            // optConnectPost
            // 
            this.optConnectPost.AutoSize = true;
            this.optConnectPost.Location = new System.Drawing.Point(205, 49);
            this.optConnectPost.Name = "optConnectPost";
            this.optConnectPost.Size = new System.Drawing.Size(57, 21);
            this.optConnectPost.TabIndex = 4;
            this.optConnectPost.TabStop = true;
            this.optConnectPost.Text = "Post";
            this.optConnectPost.UseVisualStyleBackColor = true;
            // 
            // chkUseSsl
            // 
            this.chkUseSsl.AutoSize = true;
            this.chkUseSsl.Location = new System.Drawing.Point(268, 49);
            this.chkUseSsl.Name = "chkUseSsl";
            this.chkUseSsl.Size = new System.Drawing.Size(85, 21);
            this.chkUseSsl.TabIndex = 3;
            this.chkUseSsl.Text = "Use SSL";
            this.chkUseSsl.UseVisualStyleBackColor = true;
            this.chkUseSsl.CheckedChanged += new System.EventHandler(this.chkUseSsl_CheckedChanged);
            // 
            // lblTargetUrl
            // 
            this.lblTargetUrl.AutoSize = true;
            this.lblTargetUrl.Location = new System.Drawing.Point(6, 25);
            this.lblTargetUrl.Name = "lblTargetUrl";
            this.lblTargetUrl.Size = new System.Drawing.Size(86, 17);
            this.lblTargetUrl.TabIndex = 2;
            this.lblTargetUrl.Text = "Target URL:";
            // 
            // lblConnectionProtocol
            // 
            this.lblConnectionProtocol.AutoSize = true;
            this.lblConnectionProtocol.Location = new System.Drawing.Point(97, 25);
            this.lblConnectionProtocol.Name = "lblConnectionProtocol";
            this.lblConnectionProtocol.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblConnectionProtocol.Size = new System.Drawing.Size(44, 17);
            this.lblConnectionProtocol.TabIndex = 1;
            this.lblConnectionProtocol.Text = "http://";
            this.lblConnectionProtocol.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTargetUrl
            // 
            this.txtTargetUrl.Location = new System.Drawing.Point(147, 22);
            this.txtTargetUrl.Name = "txtTargetUrl";
            this.txtTargetUrl.Size = new System.Drawing.Size(385, 22);
            this.txtTargetUrl.TabIndex = 0;
            // 
            // ctlConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpConnection);
            this.Name = "ctlConnection";
            this.Size = new System.Drawing.Size(538, 91);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Label lblConnectionProtocol;
        private System.Windows.Forms.TextBox txtTargetUrl;
        private System.Windows.Forms.Label lblConnectionMethod;
        private System.Windows.Forms.RadioButton optConnectGet;
        private System.Windows.Forms.RadioButton optConnectPost;
        private System.Windows.Forms.CheckBox chkUseSsl;
        private System.Windows.Forms.Label lblTargetUrl;
    }
}
