namespace Absinthe.Gui
{
    partial class ctlUserAuth
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
            this.grpAuthentication = new System.Windows.Forms.GroupBox();
            this.lblAuthDomain = new System.Windows.Forms.Label();
            this.lblAuthPassword = new System.Windows.Forms.Label();
            this.lblAuthUsername = new System.Windows.Forms.Label();
            this.txtAuthPassword = new System.Windows.Forms.TextBox();
            this.txtAuthDomain = new System.Windows.Forms.TextBox();
            this.txtAuthUsername = new System.Windows.Forms.TextBox();
            this.cboAuthType = new System.Windows.Forms.ComboBox();
            this.lblAuthType = new System.Windows.Forms.Label();
            this.chkUseAuth = new System.Windows.Forms.CheckBox();
            this.grpAuthentication.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAuthentication
            // 
            this.grpAuthentication.AutoSize = true;
            this.grpAuthentication.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpAuthentication.Controls.Add(this.lblAuthDomain);
            this.grpAuthentication.Controls.Add(this.lblAuthPassword);
            this.grpAuthentication.Controls.Add(this.lblAuthUsername);
            this.grpAuthentication.Controls.Add(this.txtAuthPassword);
            this.grpAuthentication.Controls.Add(this.txtAuthDomain);
            this.grpAuthentication.Controls.Add(this.txtAuthUsername);
            this.grpAuthentication.Controls.Add(this.cboAuthType);
            this.grpAuthentication.Controls.Add(this.lblAuthType);
            this.grpAuthentication.Controls.Add(this.chkUseAuth);
            this.grpAuthentication.Location = new System.Drawing.Point(3, 3);
            this.grpAuthentication.Name = "grpAuthentication";
            this.grpAuthentication.Size = new System.Drawing.Size(260, 179);
            this.grpAuthentication.TabIndex = 0;
            this.grpAuthentication.TabStop = false;
            this.grpAuthentication.Text = "User Authentication:";
            // 
            // lblAuthDomain
            // 
            this.lblAuthDomain.AutoSize = true;
            this.lblAuthDomain.Enabled = false;
            this.lblAuthDomain.Location = new System.Drawing.Point(6, 139);
            this.lblAuthDomain.Name = "lblAuthDomain";
            this.lblAuthDomain.Size = new System.Drawing.Size(60, 17);
            this.lblAuthDomain.TabIndex = 8;
            this.lblAuthDomain.Text = "Domain:";
            // 
            // lblAuthPassword
            // 
            this.lblAuthPassword.AutoSize = true;
            this.lblAuthPassword.Enabled = false;
            this.lblAuthPassword.Location = new System.Drawing.Point(6, 111);
            this.lblAuthPassword.Name = "lblAuthPassword";
            this.lblAuthPassword.Size = new System.Drawing.Size(73, 17);
            this.lblAuthPassword.TabIndex = 7;
            this.lblAuthPassword.Text = "Password:";
            // 
            // lblAuthUsername
            // 
            this.lblAuthUsername.AutoSize = true;
            this.lblAuthUsername.Enabled = false;
            this.lblAuthUsername.Location = new System.Drawing.Point(6, 83);
            this.lblAuthUsername.Name = "lblAuthUsername";
            this.lblAuthUsername.Size = new System.Drawing.Size(77, 17);
            this.lblAuthUsername.TabIndex = 6;
            this.lblAuthUsername.Text = "Username:";
            // 
            // txtAuthPassword
            // 
            this.txtAuthPassword.Enabled = false;
            this.txtAuthPassword.Location = new System.Drawing.Point(89, 108);
            this.txtAuthPassword.Name = "txtAuthPassword";
            this.txtAuthPassword.Size = new System.Drawing.Size(165, 22);
            this.txtAuthPassword.TabIndex = 5;
            // 
            // txtAuthDomain
            // 
            this.txtAuthDomain.Enabled = false;
            this.txtAuthDomain.Location = new System.Drawing.Point(89, 136);
            this.txtAuthDomain.Name = "txtAuthDomain";
            this.txtAuthDomain.Size = new System.Drawing.Size(165, 22);
            this.txtAuthDomain.TabIndex = 4;
            // 
            // txtAuthUsername
            // 
            this.txtAuthUsername.Enabled = false;
            this.txtAuthUsername.Location = new System.Drawing.Point(89, 80);
            this.txtAuthUsername.Name = "txtAuthUsername";
            this.txtAuthUsername.Size = new System.Drawing.Size(165, 22);
            this.txtAuthUsername.TabIndex = 3;
            // 
            // cboAuthType
            // 
            this.cboAuthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAuthType.Enabled = false;
            this.cboAuthType.FormattingEnabled = true;
            this.cboAuthType.Items.AddRange(new object[] {
            "Basic",
            "Digest",
            "NTLM"});
            this.cboAuthType.Location = new System.Drawing.Point(89, 49);
            this.cboAuthType.Name = "cboAuthType";
            this.cboAuthType.Size = new System.Drawing.Size(165, 24);
            this.cboAuthType.TabIndex = 2;
            this.cboAuthType.SelectedIndexChanged += new System.EventHandler(this.cboAuthType_SelectedIndexChanged);
            // 
            // lblAuthType
            // 
            this.lblAuthType.AutoSize = true;
            this.lblAuthType.Enabled = false;
            this.lblAuthType.Location = new System.Drawing.Point(6, 52);
            this.lblAuthType.Name = "lblAuthType";
            this.lblAuthType.Size = new System.Drawing.Size(77, 17);
            this.lblAuthType.TabIndex = 1;
            this.lblAuthType.Text = "Auth Type:";
            // 
            // chkUseAuth
            // 
            this.chkUseAuth.AutoSize = true;
            this.chkUseAuth.Location = new System.Drawing.Point(7, 22);
            this.chkUseAuth.Name = "chkUseAuth";
            this.chkUseAuth.Size = new System.Drawing.Size(149, 21);
            this.chkUseAuth.TabIndex = 0;
            this.chkUseAuth.Text = "Use Authentication";
            this.chkUseAuth.UseVisualStyleBackColor = true;
            this.chkUseAuth.CheckedChanged += new System.EventHandler(this.chkUseAuth_CheckedChanged);
            // 
            // ctlUserAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAuthentication);
            this.Name = "ctlUserAuth";
            this.Size = new System.Drawing.Size(266, 186);
            this.grpAuthentication.ResumeLayout(false);
            this.grpAuthentication.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAuthentication;
        private System.Windows.Forms.Label lblAuthDomain;
        private System.Windows.Forms.Label lblAuthPassword;
        private System.Windows.Forms.Label lblAuthUsername;
        private System.Windows.Forms.TextBox txtAuthPassword;
        private System.Windows.Forms.TextBox txtAuthDomain;
        private System.Windows.Forms.TextBox txtAuthUsername;
        private System.Windows.Forms.ComboBox cboAuthType;
        private System.Windows.Forms.Label lblAuthType;
        private System.Windows.Forms.CheckBox chkUseAuth;
    }
}
