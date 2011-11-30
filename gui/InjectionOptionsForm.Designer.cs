namespace Absinthe.Gui
{
    partial class InjectionOptionsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InjectionOptionsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilterDelimiter = new System.Windows.Forms.TextBox();
            this.tipDelimiter = new System.Windows.Forms.ToolTip(this.components);
            this.txtThrottle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSpeedup = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboUserAgent = new System.Windows.Forms.ComboBox();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.chkCommentQuery = new System.Windows.Forms.CheckBox();
            this.chkAppendText = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.tipThrottle = new System.Windows.Forms.ToolTip(this.components);
            this.txtAppended = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Compared Tolerance:";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(163, 6);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(33, 22);
            this.txtTolerance.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Filter Delimiter:";
            // 
            // txtFilterDelimiter
            // 
            this.txtFilterDelimiter.Location = new System.Drawing.Point(337, 6);
            this.txtFilterDelimiter.Name = "txtFilterDelimiter";
            this.txtFilterDelimiter.Size = new System.Drawing.Size(100, 22);
            this.txtFilterDelimiter.TabIndex = 4;
            this.tipDelimiter.SetToolTip(this.txtFilterDelimiter, "Leave blank for newline");
            // 
            // tipDelimiter
            // 
            this.tipDelimiter.OwnerDraw = true;
            this.tipDelimiter.ShowAlways = true;
            // 
            // txtThrottle
            // 
            this.txtThrottle.Location = new System.Drawing.Point(163, 35);
            this.txtThrottle.Name = "txtThrottle";
            this.txtThrottle.Size = new System.Drawing.Size(33, 22);
            this.txtThrottle.TabIndex = 5;
            this.tipThrottle.SetToolTip(this.txtThrottle, "The delay in seconds between requests.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Attack Throttle:";
            this.tipThrottle.SetToolTip(this.label4, "The delay in seconds between requests.");
            // 
            // chkSpeedup
            // 
            this.chkSpeedup.AutoSize = true;
            this.chkSpeedup.Location = new System.Drawing.Point(232, 38);
            this.chkSpeedup.Name = "chkSpeedup";
            this.chkSpeedup.Size = new System.Drawing.Size(130, 21);
            this.chkSpeedup.TabIndex = 7;
            this.chkSpeedup.Text = "Attack Speedup";
            this.tipThrottle.SetToolTip(this.chkSpeedup, "Selecting this will set Throttle to zero.");
            this.chkSpeedup.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "User Agent:";
            // 
            // cboUserAgent
            // 
            this.cboUserAgent.FormattingEnabled = true;
            this.cboUserAgent.Location = new System.Drawing.Point(163, 147);
            this.cboUserAgent.Name = "cboUserAgent";
            this.cboUserAgent.Size = new System.Drawing.Size(168, 24);
            this.cboUserAgent.TabIndex = 9;
            this.cboUserAgent.SelectedIndexChanged += new System.EventHandler(this.cboUserAgent_SelectedIndexChanged);
            // 
            // txtUserAgent
            // 
            this.txtUserAgent.Location = new System.Drawing.Point(163, 177);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(274, 22);
            this.txtUserAgent.TabIndex = 10;
            // 
            // chkCommentQuery
            // 
            this.chkCommentQuery.AutoSize = true;
            this.chkCommentQuery.Location = new System.Drawing.Point(163, 65);
            this.chkCommentQuery.Name = "chkCommentQuery";
            this.chkCommentQuery.Size = new System.Drawing.Size(173, 21);
            this.chkCommentQuery.TabIndex = 11;
            this.chkCommentQuery.Text = "Comment end of query";
            this.chkCommentQuery.UseVisualStyleBackColor = true;
            this.chkCommentQuery.CheckedChanged += new System.EventHandler(this.chkCommentQuery_CheckedChanged);
            // 
            // chkAppendText
            // 
            this.chkAppendText.AutoSize = true;
            this.chkAppendText.Location = new System.Drawing.Point(163, 92);
            this.chkAppendText.Name = "chkAppendText";
            this.chkAppendText.Size = new System.Drawing.Size(205, 21);
            this.chkAppendText.TabIndex = 12;
            this.chkAppendText.Text = "Append text to end of query";
            this.chkAppendText.UseVisualStyleBackColor = true;
            this.chkAppendText.CheckedChanged += new System.EventHandler(this.chkAppendText_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Query Options:";
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(151, 215);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 14;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(232, 215);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 15;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // txtAppended
            // 
            this.txtAppended.Enabled = false;
            this.txtAppended.Location = new System.Drawing.Point(163, 119);
            this.txtAppended.Name = "txtAppended";
            this.txtAppended.Size = new System.Drawing.Size(274, 22);
            this.txtAppended.TabIndex = 16;
            // 
            // InjectionOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(450, 256);
            this.Controls.Add(this.txtAppended);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkAppendText);
            this.Controls.Add(this.chkCommentQuery);
            this.Controls.Add(this.txtUserAgent);
            this.Controls.Add(this.cboUserAgent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkSpeedup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtThrottle);
            this.Controls.Add(this.txtFilterDelimiter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTolerance);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InjectionOptionsForm";
            this.Text = "Injection Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilterDelimiter;
        private System.Windows.Forms.ToolTip tipDelimiter;
        private System.Windows.Forms.TextBox txtThrottle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSpeedup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboUserAgent;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.CheckBox chkCommentQuery;
        private System.Windows.Forms.CheckBox chkAppendText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.ToolTip tipThrottle;
        private System.Windows.Forms.TextBox txtAppended;
    }
}