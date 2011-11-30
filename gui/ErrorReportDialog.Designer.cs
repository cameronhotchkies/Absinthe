namespace Absinthe.Gui
{
    partial class ErrorReportDialog
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblWhatToSend = new System.Windows.Forms.Label();
            this.txtVersionData = new System.Windows.Forms.TextBox();
            this.txtHavingData = new System.Windows.Forms.TextBox();
            this.lblClarify = new System.Windows.Forms.Label();
            this.lblCredit = new System.Windows.Forms.Label();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(670, 61);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "The version of SQL Server was not recognized. To help development of Absinthe, a " +
                "copy of the generated version page will be sent to 0x90.org unless you decide ot" +
                "herwise.";
            // 
            // lblWhatToSend
            // 
            this.lblWhatToSend.AutoSize = true;
            this.lblWhatToSend.Location = new System.Drawing.Point(9, 53);
            this.lblWhatToSend.Name = "lblWhatToSend";
            this.lblWhatToSend.Size = new System.Drawing.Size(193, 17);
            this.lblWhatToSend.TabIndex = 1;
            this.lblWhatToSend.Text = "What will be sent to 0x90.org:";
            // 
            // txtVersionData
            // 
            this.txtVersionData.Location = new System.Drawing.Point(12, 73);
            this.txtVersionData.Multiline = true;
            this.txtVersionData.Name = "txtVersionData";
            this.txtVersionData.Size = new System.Drawing.Size(670, 209);
            this.txtVersionData.TabIndex = 2;
            // 
            // txtHavingData
            // 
            this.txtHavingData.Location = new System.Drawing.Point(12, 288);
            this.txtHavingData.Multiline = true;
            this.txtHavingData.Name = "txtHavingData";
            this.txtHavingData.Size = new System.Drawing.Size(670, 209);
            this.txtHavingData.TabIndex = 3;
            // 
            // lblClarify
            // 
            this.lblClarify.AutoSize = true;
            this.lblClarify.Location = new System.Drawing.Point(12, 500);
            this.lblClarify.Name = "lblClarify";
            this.lblClarify.Size = new System.Drawing.Size(289, 17);
            this.lblClarify.TabIndex = 4;
            this.lblClarify.Text = "(feel free to remove site-specific information)";
            // 
            // lblCredit
            // 
            this.lblCredit.AutoSize = true;
            this.lblCredit.Location = new System.Drawing.Point(15, 530);
            this.lblCredit.Name = "lblCredit";
            this.lblCredit.Size = new System.Drawing.Size(215, 17);
            this.lblCredit.TabIndex = 5;
            this.lblCredit.Text = "Name / Email Address (optional):";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new System.Drawing.Point(236, 527);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Size = new System.Drawing.Size(446, 22);
            this.txtCredit.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtCredit, "This is only asked for to possibly verify bug fixes.");
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(526, 602);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 7;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(607, 602);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 8;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // ErrorReportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(694, 637);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.txtCredit);
            this.Controls.Add(this.lblCredit);
            this.Controls.Add(this.lblClarify);
            this.Controls.Add(this.txtHavingData);
            this.Controls.Add(this.txtVersionData);
            this.Controls.Add(this.lblWhatToSend);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ErrorReportDialog";
            this.Text = "Error Reporting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblWhatToSend;
        private System.Windows.Forms.TextBox txtVersionData;
        private System.Windows.Forms.TextBox txtHavingData;
        private System.Windows.Forms.Label lblClarify;
        private System.Windows.Forms.Label lblCredit;
        private System.Windows.Forms.TextBox txtCredit;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
    }
}