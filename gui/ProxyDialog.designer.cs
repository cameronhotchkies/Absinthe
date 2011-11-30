namespace Absinthe.Gui
{
    partial class ProxyDialog
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
            this.optCabinetProxies = new System.Windows.Forms.RadioButton();
            this.optCurrentAppProxies = new System.Windows.Forms.RadioButton();
            this.lstProxies = new System.Windows.Forms.ListBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butAdd = new System.Windows.Forms.Button();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.lblProxyPort = new System.Windows.Forms.Label();
            this.txtProxyHost = new System.Windows.Forms.TextBox();
            this.lblProxyHost = new System.Windows.Forms.Label();
            this.butRemove = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // optCabinetProxies
            // 
            this.optCabinetProxies.AutoSize = true;
            this.optCabinetProxies.Location = new System.Drawing.Point(12, 93);
            this.optCabinetProxies.Name = "optCabinetProxies";
            this.optCabinetProxies.Size = new System.Drawing.Size(220, 21);
            this.optCabinetProxies.TabIndex = 0;
            this.optCabinetProxies.TabStop = true;
            this.optCabinetProxies.Text = "All Liquor Cabinet Applications";
            this.optCabinetProxies.UseVisualStyleBackColor = true;
            // 
            // optCurrentAppProxies
            // 
            this.optCurrentAppProxies.AutoSize = true;
            this.optCurrentAppProxies.Location = new System.Drawing.Point(238, 93);
            this.optCurrentAppProxies.Name = "optCurrentAppProxies";
            this.optCurrentAppProxies.Size = new System.Drawing.Size(101, 21);
            this.optCurrentAppProxies.TabIndex = 1;
            this.optCurrentAppProxies.TabStop = true;
            this.optCurrentAppProxies.Text = "CurrentApp";
            this.optCurrentAppProxies.UseVisualStyleBackColor = true;
            // 
            // lstProxies
            // 
            this.lstProxies.FormattingEnabled = true;
            this.lstProxies.ItemHeight = 16;
            this.lstProxies.Location = new System.Drawing.Point(12, 117);
            this.lstProxies.MultiColumn = true;
            this.lstProxies.Name = "lstProxies";
            this.lstProxies.Size = new System.Drawing.Size(428, 292);
            this.lstProxies.TabIndex = 2;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(333, 426);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(98, 30);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(229, 426);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(98, 30);
            this.butOk.TabIndex = 4;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.txtProxyPort);
            this.groupBox1.Controls.Add(this.lblProxyPort);
            this.groupBox1.Controls.Add(this.txtProxyHost);
            this.groupBox1.Controls.Add(this.lblProxyHost);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 61);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New:";
            // 
            // butAdd
            // 
            this.butAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butAdd.Location = new System.Drawing.Point(355, 21);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(63, 23);
            this.butAdd.TabIndex = 4;
            this.butAdd.Text = "Add";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.Location = new System.Drawing.Point(281, 22);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.Size = new System.Drawing.Size(67, 22);
            this.txtProxyPort.TabIndex = 3;
            // 
            // lblProxyPort
            // 
            this.lblProxyPort.AutoSize = true;
            this.lblProxyPort.Location = new System.Drawing.Point(236, 23);
            this.lblProxyPort.Name = "lblProxyPort";
            this.lblProxyPort.Size = new System.Drawing.Size(38, 17);
            this.lblProxyPort.TabIndex = 2;
            this.lblProxyPort.Text = "Port:";
            // 
            // txtProxyHost
            // 
            this.txtProxyHost.Location = new System.Drawing.Point(54, 23);
            this.txtProxyHost.Name = "txtProxyHost";
            this.txtProxyHost.Size = new System.Drawing.Size(175, 22);
            this.txtProxyHost.TabIndex = 1;
            // 
            // lblProxyHost
            // 
            this.lblProxyHost.AutoSize = true;
            this.lblProxyHost.Location = new System.Drawing.Point(6, 23);
            this.lblProxyHost.Name = "lblProxyHost";
            this.lblProxyHost.Size = new System.Drawing.Size(41, 17);
            this.lblProxyHost.TabIndex = 0;
            this.lblProxyHost.Text = "Host:";
            // 
            // butRemove
            // 
            this.butRemove.Enabled = false;
            this.butRemove.Location = new System.Drawing.Point(345, 88);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(85, 23);
            this.butRemove.TabIndex = 6;
            this.butRemove.Text = "Remove";
            this.butRemove.UseVisualStyleBackColor = true;
            // 
            // ProxyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(455, 468);
            this.Controls.Add(this.butRemove);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.lstProxies);
            this.Controls.Add(this.optCurrentAppProxies);
            this.Controls.Add(this.optCabinetProxies);
            this.Name = "ProxyDialog";
            this.Text = "Proxies";
            this.Load += new System.EventHandler(this.ProxyDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optCabinetProxies;
        private System.Windows.Forms.RadioButton optCurrentAppProxies;
        private System.Windows.Forms.ListBox lstProxies;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProxyHost;
        private System.Windows.Forms.Label lblProxyHost;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.TextBox txtProxyPort;
        private System.Windows.Forms.Label lblProxyPort;
        private System.Windows.Forms.Button butRemove;
    }
}
