namespace Absinthe.Gui
{
    partial class frmAddParameter
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
            this.butCancel = new System.Windows.Forms.Button();
            this.chkParameterAsString = new System.Windows.Forms.CheckBox();
            this.chkInjectableParameter = new System.Windows.Forms.CheckBox();
            this.txtParameterValue = new System.Windows.Forms.TextBox();
            this.lblParameterValue = new System.Windows.Forms.Label();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.lblParameterOptions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(126, 129);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 7;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // chkParameterAsString
            // 
            this.chkParameterAsString.AutoSize = true;
            this.chkParameterAsString.Location = new System.Drawing.Point(79, 89);
            this.chkParameterAsString.Name = "chkParameterAsString";
            this.chkParameterAsString.Size = new System.Drawing.Size(122, 21);
            this.chkParameterAsString.TabIndex = 5;
            this.chkParameterAsString.Text = "Treat as string";
            this.chkParameterAsString.UseVisualStyleBackColor = true;
            // 
            // chkInjectableParameter
            // 
            this.chkInjectableParameter.AutoSize = true;
            this.chkInjectableParameter.Location = new System.Drawing.Point(79, 62);
            this.chkInjectableParameter.Name = "chkInjectableParameter";
            this.chkInjectableParameter.Size = new System.Drawing.Size(90, 21);
            this.chkInjectableParameter.TabIndex = 4;
            this.chkInjectableParameter.Text = "Injectable";
            this.chkInjectableParameter.UseVisualStyleBackColor = true;
            // 
            // txtParameterValue
            // 
            this.txtParameterValue.Location = new System.Drawing.Point(79, 34);
            this.txtParameterValue.Name = "txtParameterValue";
            this.txtParameterValue.Size = new System.Drawing.Size(148, 22);
            this.txtParameterValue.TabIndex = 3;
            // 
            // lblParameterValue
            // 
            this.lblParameterValue.AutoSize = true;
            this.lblParameterValue.Location = new System.Drawing.Point(24, 37);
            this.lblParameterValue.Name = "lblParameterValue";
            this.lblParameterValue.Size = new System.Drawing.Size(48, 17);
            this.lblParameterValue.TabIndex = 2;
            this.lblParameterValue.Text = "Value:";
            // 
            // txtParameterName
            // 
            this.txtParameterName.Location = new System.Drawing.Point(79, 6);
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.Size = new System.Drawing.Size(148, 22);
            this.txtParameterName.TabIndex = 1;
            // 
            // lblParameterName
            // 
            this.lblParameterName.AutoSize = true;
            this.lblParameterName.Location = new System.Drawing.Point(24, 9);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(49, 17);
            this.lblParameterName.TabIndex = 0;
            this.lblParameterName.Text = "Name:";
            // 
            // butOK
            // 
            this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butOK.Location = new System.Drawing.Point(45, 129);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 6;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // lblParameterOptions
            // 
            this.lblParameterOptions.AutoSize = true;
            this.lblParameterOptions.Location = new System.Drawing.Point(12, 63);
            this.lblParameterOptions.Name = "lblParameterOptions";
            this.lblParameterOptions.Size = new System.Drawing.Size(61, 17);
            this.lblParameterOptions.TabIndex = 8;
            this.lblParameterOptions.Text = "Options:";
            // 
            // frmAddParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(249, 164);
            this.Controls.Add(this.lblParameterOptions);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.chkParameterAsString);
            this.Controls.Add(this.txtParameterName);
            this.Controls.Add(this.chkInjectableParameter);
            this.Controls.Add(this.lblParameterName);
            this.Controls.Add(this.txtParameterValue);
            this.Controls.Add(this.lblParameterValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.Name = "frmAddParameter";
            this.Text = "Add...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.CheckBox chkParameterAsString;
        private System.Windows.Forms.CheckBox chkInjectableParameter;
        private System.Windows.Forms.TextBox txtParameterValue;
        private System.Windows.Forms.Label lblParameterValue;
        private System.Windows.Forms.TextBox txtParameterName;
        private System.Windows.Forms.Label lblParameterName;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label lblParameterOptions;

        
    }
}
