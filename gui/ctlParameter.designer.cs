namespace Absinthe.Gui
{
    partial class ctlParameter
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
            this.tabParams = new System.Windows.Forms.TabControl();
            this.tabFormParams = new System.Windows.Forms.TabPage();
            this.lstFormParameters = new System.Windows.Forms.ListView();
            this.colParamName = new System.Windows.Forms.ColumnHeader();
            this.colParamValue = new System.Windows.Forms.ColumnHeader();
            this.colParamInject = new System.Windows.Forms.ColumnHeader();
            this.colParamAsString = new System.Windows.Forms.ColumnHeader();
            this.tabCookies = new System.Windows.Forms.TabPage();
            this.lstCookies = new System.Windows.Forms.ListView();
            this.colCookieName = new System.Windows.Forms.ColumnHeader();
            this.colCookieValue = new System.Windows.Forms.ColumnHeader();
            this.colCookieInjectable = new System.Windows.Forms.ColumnHeader();
            this.colCookieAsString = new System.Windows.Forms.ColumnHeader();
            this.butAddParameter = new System.Windows.Forms.Button();
            this.butEditParameter = new System.Windows.Forms.Button();
            this.butRemoveParameter = new System.Windows.Forms.Button();
            this.grpParameters = new System.Windows.Forms.GroupBox();
            this.tabParams.SuspendLayout();
            this.tabFormParams.SuspendLayout();
            this.tabCookies.SuspendLayout();
            this.grpParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.tabFormParams);
            this.tabParams.Controls.Add(this.tabCookies);
            this.tabParams.Location = new System.Drawing.Point(87, 21);
            this.tabParams.Name = "tabParams";
            this.tabParams.SelectedIndex = 0;
            this.tabParams.Size = new System.Drawing.Size(200, 277);
            this.tabParams.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabParams.TabIndex = 1;
            // 
            // tabFormParams
            // 
            this.tabFormParams.Controls.Add(this.lstFormParameters);
            this.tabFormParams.Location = new System.Drawing.Point(4, 25);
            this.tabFormParams.Name = "tabFormParams";
            this.tabFormParams.Padding = new System.Windows.Forms.Padding(3);
            this.tabFormParams.Size = new System.Drawing.Size(192, 248);
            this.tabFormParams.TabIndex = 0;
            this.tabFormParams.Text = "Parameters";
            this.tabFormParams.UseVisualStyleBackColor = true;
            // 
            // lstFormParameters
            // 
            this.lstFormParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colParamName,
            this.colParamValue,
            this.colParamInject,
            this.colParamAsString});
            this.lstFormParameters.Location = new System.Drawing.Point(3, 4);
            this.lstFormParameters.Name = "lstFormParameters";
            this.lstFormParameters.Size = new System.Drawing.Size(186, 241);
            this.lstFormParameters.TabIndex = 0;
            this.lstFormParameters.UseCompatibleStateImageBehavior = false;
            this.lstFormParameters.View = System.Windows.Forms.View.Details;
            // 
            // colParamName
            // 
            this.colParamName.Text = "Name";
            // 
            // colParamValue
            // 
            this.colParamValue.Text = "Value";
            // 
            // colParamInject
            // 
            this.colParamInject.Text = "Injectable";
            // 
            // colParamAsString
            // 
            this.colParamAsString.Text = "Str";
            // 
            // tabCookies
            // 
            this.tabCookies.Controls.Add(this.lstCookies);
            this.tabCookies.Location = new System.Drawing.Point(4, 25);
            this.tabCookies.Name = "tabCookies";
            this.tabCookies.Padding = new System.Windows.Forms.Padding(3);
            this.tabCookies.Size = new System.Drawing.Size(192, 248);
            this.tabCookies.TabIndex = 1;
            this.tabCookies.Text = "Cookies";
            this.tabCookies.UseVisualStyleBackColor = true;
            // 
            // lstCookies
            // 
            this.lstCookies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCookieName,
            this.colCookieValue,
            this.colCookieInjectable,
            this.colCookieAsString});
            this.lstCookies.Location = new System.Drawing.Point(3, 4);
            this.lstCookies.Name = "lstCookies";
            this.lstCookies.Size = new System.Drawing.Size(186, 241);
            this.lstCookies.TabIndex = 1;
            this.lstCookies.UseCompatibleStateImageBehavior = false;
            this.lstCookies.View = System.Windows.Forms.View.Details;
            // 
            // colCookieName
            // 
            this.colCookieName.Text = "Name";
            // 
            // colCookieValue
            // 
            this.colCookieValue.Text = "Value";
            // 
            // colCookieInjectable
            // 
            this.colCookieInjectable.Text = "Injectable";
            // 
            // colCookieAsString
            // 
            this.colCookieAsString.Text = "String";
            // 
            // butAddParameter
            // 
            this.butAddParameter.Location = new System.Drawing.Point(6, 118);
            this.butAddParameter.Name = "butAddParameter";
            this.butAddParameter.Size = new System.Drawing.Size(75, 23);
            this.butAddParameter.TabIndex = 2;
            this.butAddParameter.Text = "Add ...";
            this.butAddParameter.UseVisualStyleBackColor = true;
            this.butAddParameter.Click += new System.EventHandler(this.butAddParameter_Click);
            // 
            // butEditParameter
            // 
            this.butEditParameter.Location = new System.Drawing.Point(6, 147);
            this.butEditParameter.Name = "butEditParameter";
            this.butEditParameter.Size = new System.Drawing.Size(75, 23);
            this.butEditParameter.TabIndex = 3;
            this.butEditParameter.Text = "Edit ...";
            this.butEditParameter.UseVisualStyleBackColor = true;
            this.butEditParameter.Click += new System.EventHandler(this.butEditParameter_Click);
            // 
            // butRemoveParameter
            // 
            this.butRemoveParameter.Location = new System.Drawing.Point(6, 176);
            this.butRemoveParameter.Name = "butRemoveParameter";
            this.butRemoveParameter.Size = new System.Drawing.Size(75, 23);
            this.butRemoveParameter.TabIndex = 4;
            this.butRemoveParameter.Text = "Remove";
            this.butRemoveParameter.UseVisualStyleBackColor = true;
            this.butRemoveParameter.Click += new System.EventHandler(this.butRemoveParameter_Click);
            // 
            // grpParameters
            // 
            this.grpParameters.AutoSize = true;
            this.grpParameters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpParameters.Controls.Add(this.tabParams);
            this.grpParameters.Controls.Add(this.butRemoveParameter);
            this.grpParameters.Controls.Add(this.butAddParameter);
            this.grpParameters.Controls.Add(this.butEditParameter);
            this.grpParameters.Location = new System.Drawing.Point(3, 3);
            this.grpParameters.Name = "grpParameters";
            this.grpParameters.Size = new System.Drawing.Size(293, 319);
            this.grpParameters.TabIndex = 5;
            this.grpParameters.TabStop = false;
            this.grpParameters.Text = "Parameters:";
            // 
            // ctlParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.grpParameters);
            this.Name = "ctlParameter";
            this.Size = new System.Drawing.Size(299, 325);
            this.tabParams.ResumeLayout(false);
            this.tabFormParams.ResumeLayout(false);
            this.tabCookies.ResumeLayout(false);
            this.grpParameters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabParams;
        private System.Windows.Forms.TabPage tabFormParams;
        private System.Windows.Forms.ListView lstFormParameters;
        private System.Windows.Forms.ColumnHeader colParamName;
        private System.Windows.Forms.ColumnHeader colParamValue;
        private System.Windows.Forms.ColumnHeader colParamInject;
        private System.Windows.Forms.TabPage tabCookies;
        private System.Windows.Forms.ListView lstCookies;
        private System.Windows.Forms.ColumnHeader colCookieName;
        private System.Windows.Forms.ColumnHeader colCookieValue;
        private System.Windows.Forms.ColumnHeader colCookieInjectable;
        private System.Windows.Forms.Button butAddParameter;
        private System.Windows.Forms.Button butEditParameter;
        private System.Windows.Forms.Button butRemoveParameter;
        private System.Windows.Forms.GroupBox grpParameters;
        private System.Windows.Forms.ColumnHeader colParamAsString;
        private System.Windows.Forms.ColumnHeader colCookieAsString;
    }
}
