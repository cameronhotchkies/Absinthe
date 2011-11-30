namespace Absinthe.Gui
{
    partial class Absinthe
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Username:");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Tables:");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Database", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Absinthe));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkVerifyVersion = new System.Windows.Forms.CheckBox();
            this.cboPluginSelection = new System.Windows.Forms.ComboBox();
            this.optErrorBasedInjection = new System.Windows.Forms.RadioButton();
            this.optBlindInjection = new System.Windows.Forms.RadioButton();
            this.lblPluginSelection = new System.Windows.Forms.Label();
            this.lblInjectionType = new System.Windows.Forms.Label();
            this.AbsintheTabs = new System.Windows.Forms.TabControl();
            this.tabHostInfo = new System.Windows.Forms.TabPage();
            this.butInitializeInjection = new System.Windows.Forms.Button();
            this.ctlUserAuth1 = new ctlUserAuth();
            this.FormParameters = new ctlParameter();
            this.ctlConnection1 = new ctlConnection();
            this.tabSchema = new System.Windows.Forms.TabPage();
            this.grpSchema = new System.Windows.Forms.GroupBox();
            this.tvwSchema = new System.Windows.Forms.TreeView();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.butGetFieldInfo = new System.Windows.Forms.Button();
            this.butGetTableInfo = new System.Windows.Forms.Button();
            this.butGetUsername = new System.Windows.Forms.Button();
            this.tabRecordDownload = new System.Windows.Forms.TabPage();
            this.butDownloadRecords = new System.Windows.Forms.Button();
            this.grpRecordSelection = new System.Windows.Forms.GroupBox();
            this.chklstRecordSelection = new System.Windows.Forms.CheckedListBox();
            this.grpOutputFile = new System.Windows.Forms.GroupBox();
            this.butRecordsBrowse = new System.Windows.Forms.Button();
            this.lblRecordsFilename = new System.Windows.Forms.Label();
            this.txtRecordsFilename = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectionOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proxyConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statUserMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.filOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.filSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.AbsintheTabs.SuspendLayout();
            this.tabHostInfo.SuspendLayout();
            this.tabSchema.SuspendLayout();
            this.grpSchema.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.tabRecordDownload.SuspendLayout();
            this.grpRecordSelection.SuspendLayout();
            this.grpOutputFile.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.chkVerifyVersion);
            this.groupBox1.Controls.Add(this.cboPluginSelection);
            this.groupBox1.Controls.Add(this.optErrorBasedInjection);
            this.groupBox1.Controls.Add(this.optBlindInjection);
            this.groupBox1.Controls.Add(this.lblPluginSelection);
            this.groupBox1.Controls.Add(this.lblInjectionType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exploit Type:";
            // 
            // chkVerifyVersion
            // 
            this.chkVerifyVersion.AutoSize = true;
            this.chkVerifyVersion.Enabled = false;
            this.chkVerifyVersion.Location = new System.Drawing.Point(483, 22);
            this.chkVerifyVersion.Name = "chkVerifyVersion";
            this.chkVerifyVersion.Size = new System.Drawing.Size(118, 21);
            this.chkVerifyVersion.TabIndex = 5;
            this.chkVerifyVersion.Text = "Verify Version";
            this.chkVerifyVersion.UseVisualStyleBackColor = true;
            // 
            // cboPluginSelection
            // 
            this.cboPluginSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPluginSelection.Enabled = false;
            this.cboPluginSelection.FormattingEnabled = true;
            this.cboPluginSelection.Location = new System.Drawing.Point(192, 43);
            this.cboPluginSelection.Name = "cboPluginSelection";
            this.cboPluginSelection.Size = new System.Drawing.Size(409, 24);
            this.cboPluginSelection.TabIndex = 4;
            this.cboPluginSelection.SelectedIndexChanged += new System.EventHandler(this.cboPluginSelection_SelectedIndexChanged);
            // 
            // optErrorBasedInjection
            // 
            this.optErrorBasedInjection.AutoSize = true;
            this.optErrorBasedInjection.Location = new System.Drawing.Point(315, 21);
            this.optErrorBasedInjection.Name = "optErrorBasedInjection";
            this.optErrorBasedInjection.Size = new System.Drawing.Size(161, 21);
            this.optErrorBasedInjection.TabIndex = 3;
            this.optErrorBasedInjection.TabStop = true;
            this.optErrorBasedInjection.Text = "Error Based Injection";
            this.optErrorBasedInjection.UseVisualStyleBackColor = true;
            this.optErrorBasedInjection.CheckedChanged += new System.EventHandler(this.optErrorBasedInjection_CheckedChanged);
            // 
            // optBlindInjection
            // 
            this.optBlindInjection.AutoSize = true;
            this.optBlindInjection.Location = new System.Drawing.Point(192, 22);
            this.optBlindInjection.Name = "optBlindInjection";
            this.optBlindInjection.Size = new System.Drawing.Size(116, 21);
            this.optBlindInjection.TabIndex = 2;
            this.optBlindInjection.TabStop = true;
            this.optBlindInjection.Text = "Blind Injection";
            this.optBlindInjection.UseVisualStyleBackColor = true;
            this.optBlindInjection.CheckedChanged += new System.EventHandler(this.optBlindInjection_CheckedChanged);
            // 
            // lblPluginSelection
            // 
            this.lblPluginSelection.AutoSize = true;
            this.lblPluginSelection.Enabled = false;
            this.lblPluginSelection.Location = new System.Drawing.Point(7, 46);
            this.lblPluginSelection.Name = "lblPluginSelection";
            this.lblPluginSelection.Size = new System.Drawing.Size(179, 17);
            this.lblPluginSelection.TabIndex = 1;
            this.lblPluginSelection.Text = "Select the target database:";
            // 
            // lblInjectionType
            // 
            this.lblInjectionType.AutoSize = true;
            this.lblInjectionType.Location = new System.Drawing.Point(7, 22);
            this.lblInjectionType.Name = "lblInjectionType";
            this.lblInjectionType.Size = new System.Drawing.Size(178, 17);
            this.lblInjectionType.TabIndex = 0;
            this.lblInjectionType.Text = "Select the type of injection:";
            // 
            // AbsintheTabs
            // 
            this.AbsintheTabs.Controls.Add(this.tabHostInfo);
            this.AbsintheTabs.Controls.Add(this.tabSchema);
            this.AbsintheTabs.Controls.Add(this.tabRecordDownload);
            this.AbsintheTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AbsintheTabs.Location = new System.Drawing.Point(0, 26);
            this.AbsintheTabs.Name = "AbsintheTabs";
            this.AbsintheTabs.SelectedIndex = 0;
            this.AbsintheTabs.Size = new System.Drawing.Size(627, 621);
            this.AbsintheTabs.TabIndex = 1;
            // 
            // tabHostInfo
            // 
            this.tabHostInfo.Controls.Add(this.butInitializeInjection);
            this.tabHostInfo.Controls.Add(this.ctlUserAuth1);
            this.tabHostInfo.Controls.Add(this.FormParameters);
            this.tabHostInfo.Controls.Add(this.ctlConnection1);
            this.tabHostInfo.Controls.Add(this.groupBox1);
            this.tabHostInfo.Location = new System.Drawing.Point(4, 25);
            this.tabHostInfo.Name = "tabHostInfo";
            this.tabHostInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabHostInfo.Size = new System.Drawing.Size(619, 592);
            this.tabHostInfo.TabIndex = 0;
            this.tabHostInfo.Text = "Host Information";
            this.tabHostInfo.UseVisualStyleBackColor = true;
            // 
            // butInitializeInjection
            // 
            this.butInitializeInjection.Location = new System.Drawing.Point(234, 527);
            this.butInitializeInjection.Name = "butInitializeInjection";
            this.butInitializeInjection.Size = new System.Drawing.Size(150, 23);
            this.butInitializeInjection.TabIndex = 4;
            this.butInitializeInjection.Text = "Initialize Injection";
            this.butInitializeInjection.UseVisualStyleBackColor = true;
            this.butInitializeInjection.Click += new System.EventHandler(this.butInitializeInjection_Click);
            // 
            // ctlUserAuth1
            // 
            this.ctlUserAuth1.Location = new System.Drawing.Point(350, 196);
            this.ctlUserAuth1.Name = "ctlUserAuth1";
            this.ctlUserAuth1.Size = new System.Drawing.Size(266, 186);
            this.ctlUserAuth1.TabIndex = 3;
            // 
            // FormParameters
            // 
            this.FormParameters.AutoSize = true;
            this.FormParameters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormParameters.Location = new System.Drawing.Point(9, 196);
            this.FormParameters.Name = "FormParameters";
            this.FormParameters.Size = new System.Drawing.Size(299, 325);
            this.FormParameters.TabIndex = 2;
            this.FormParameters.Load += new System.EventHandler(this.FormParameters_Load);
            // 
            // ctlConnection1
            // 
            this.ctlConnection1.ConnectMethod = "GET";
            this.ctlConnection1.Location = new System.Drawing.Point(7, 98);
            this.ctlConnection1.Name = "ctlConnection1";
            this.ctlConnection1.Size = new System.Drawing.Size(609, 91);
            this.ctlConnection1.TabIndex = 1;
            this.ctlConnection1.TargetUrl = "";
            // 
            // tabSchema
            // 
            this.tabSchema.Controls.Add(this.grpSchema);
            this.tabSchema.Controls.Add(this.grpActions);
            this.tabSchema.Location = new System.Drawing.Point(4, 25);
            this.tabSchema.Name = "tabSchema";
            this.tabSchema.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchema.Size = new System.Drawing.Size(619, 592);
            this.tabSchema.TabIndex = 1;
            this.tabSchema.Text = "Database Schema";
            this.tabSchema.UseVisualStyleBackColor = true;
            // 
            // grpSchema
            // 
            this.grpSchema.Controls.Add(this.tvwSchema);
            this.grpSchema.Location = new System.Drawing.Point(9, 77);
            this.grpSchema.Name = "grpSchema";
            this.grpSchema.Size = new System.Drawing.Size(552, 484);
            this.grpSchema.TabIndex = 1;
            this.grpSchema.TabStop = false;
            // 
            // tvwSchema
            // 
            this.tvwSchema.Location = new System.Drawing.Point(7, 22);
            this.tvwSchema.Name = "tvwSchema";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Username:";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Tables:";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Database";
            this.tvwSchema.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.tvwSchema.Size = new System.Drawing.Size(539, 456);
            this.tvwSchema.TabIndex = 0;
            this.tvwSchema.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSchema_AfterSelect);
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.butGetFieldInfo);
            this.grpActions.Controls.Add(this.butGetTableInfo);
            this.grpActions.Controls.Add(this.butGetUsername);
            this.grpActions.Location = new System.Drawing.Point(9, 7);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(552, 64);
            this.grpActions.TabIndex = 0;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Actions:";
            // 
            // butGetFieldInfo
            // 
            this.butGetFieldInfo.Location = new System.Drawing.Point(296, 22);
            this.butGetFieldInfo.Name = "butGetFieldInfo";
            this.butGetFieldInfo.Size = new System.Drawing.Size(122, 23);
            this.butGetFieldInfo.TabIndex = 2;
            this.butGetFieldInfo.Text = "Load Field Info";
            this.butGetFieldInfo.UseVisualStyleBackColor = true;
            this.butGetFieldInfo.Click += new System.EventHandler(this.butGetFieldInfo_Click);
            // 
            // butGetTableInfo
            // 
            this.butGetTableInfo.Location = new System.Drawing.Point(163, 22);
            this.butGetTableInfo.Name = "butGetTableInfo";
            this.butGetTableInfo.Size = new System.Drawing.Size(127, 23);
            this.butGetTableInfo.TabIndex = 1;
            this.butGetTableInfo.Text = "Load Table Info";
            this.butGetTableInfo.UseVisualStyleBackColor = true;
            this.butGetTableInfo.Click += new System.EventHandler(this.butGetTableInfo_Click);
            // 
            // butGetUsername
            // 
            this.butGetUsername.Location = new System.Drawing.Point(7, 22);
            this.butGetUsername.Name = "butGetUsername";
            this.butGetUsername.Size = new System.Drawing.Size(150, 23);
            this.butGetUsername.TabIndex = 0;
            this.butGetUsername.Text = "Retrieve Username";
            this.butGetUsername.UseVisualStyleBackColor = true;
            this.butGetUsername.Click += new System.EventHandler(this.butGetUsername_Click);
            // 
            // tabRecordDownload
            // 
            this.tabRecordDownload.Controls.Add(this.butDownloadRecords);
            this.tabRecordDownload.Controls.Add(this.grpRecordSelection);
            this.tabRecordDownload.Controls.Add(this.grpOutputFile);
            this.tabRecordDownload.Location = new System.Drawing.Point(4, 25);
            this.tabRecordDownload.Name = "tabRecordDownload";
            this.tabRecordDownload.Size = new System.Drawing.Size(619, 592);
            this.tabRecordDownload.TabIndex = 2;
            this.tabRecordDownload.Text = "Download Records";
            this.tabRecordDownload.UseVisualStyleBackColor = true;
            // 
            // butDownloadRecords
            // 
            this.butDownloadRecords.Location = new System.Drawing.Point(210, 509);
            this.butDownloadRecords.Name = "butDownloadRecords";
            this.butDownloadRecords.Size = new System.Drawing.Size(174, 23);
            this.butDownloadRecords.TabIndex = 2;
            this.butDownloadRecords.Text = "Download Fields to XML";
            this.butDownloadRecords.UseVisualStyleBackColor = true;
            this.butDownloadRecords.Click += new System.EventHandler(this.butDownloadRecords_Click);
            // 
            // grpRecordSelection
            // 
            this.grpRecordSelection.Controls.Add(this.chklstRecordSelection);
            this.grpRecordSelection.Location = new System.Drawing.Point(9, 69);
            this.grpRecordSelection.Name = "grpRecordSelection";
            this.grpRecordSelection.Size = new System.Drawing.Size(552, 438);
            this.grpRecordSelection.TabIndex = 1;
            this.grpRecordSelection.TabStop = false;
            this.grpRecordSelection.Text = "Fields to Download:";
            // 
            // chklstRecordSelection
            // 
            this.chklstRecordSelection.FormattingEnabled = true;
            this.chklstRecordSelection.Location = new System.Drawing.Point(7, 22);
            this.chklstRecordSelection.Name = "chklstRecordSelection";
            this.chklstRecordSelection.Size = new System.Drawing.Size(539, 412);
            this.chklstRecordSelection.TabIndex = 0;
            // 
            // grpOutputFile
            // 
            this.grpOutputFile.Controls.Add(this.butRecordsBrowse);
            this.grpOutputFile.Controls.Add(this.lblRecordsFilename);
            this.grpOutputFile.Controls.Add(this.txtRecordsFilename);
            this.grpOutputFile.Location = new System.Drawing.Point(9, 4);
            this.grpOutputFile.Name = "grpOutputFile";
            this.grpOutputFile.Size = new System.Drawing.Size(552, 58);
            this.grpOutputFile.TabIndex = 0;
            this.grpOutputFile.TabStop = false;
            this.grpOutputFile.Text = "Output:";
            // 
            // butRecordsBrowse
            // 
            this.butRecordsBrowse.Location = new System.Drawing.Point(461, 22);
            this.butRecordsBrowse.Name = "butRecordsBrowse";
            this.butRecordsBrowse.Size = new System.Drawing.Size(85, 22);
            this.butRecordsBrowse.TabIndex = 2;
            this.butRecordsBrowse.Text = "Browse ...";
            this.butRecordsBrowse.UseVisualStyleBackColor = true;
            this.butRecordsBrowse.Click += new System.EventHandler(this.butRecordsBrowse_Click);
            // 
            // lblRecordsFilename
            // 
            this.lblRecordsFilename.AutoSize = true;
            this.lblRecordsFilename.Location = new System.Drawing.Point(6, 25);
            this.lblRecordsFilename.Name = "lblRecordsFilename";
            this.lblRecordsFilename.Size = new System.Drawing.Size(69, 17);
            this.lblRecordsFilename.TabIndex = 1;
            this.lblRecordsFilename.Text = "Filename:";
            // 
            // txtRecordsFilename
            // 
            this.txtRecordsFilename.Location = new System.Drawing.Point(81, 22);
            this.txtRecordsFilename.Name = "txtRecordsFilename";
            this.txtRecordsFilename.Size = new System.Drawing.Size(374, 22);
            this.txtRecordsFilename.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(627, 26);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(40, 22);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.quitToolStripMenuItem.Text = "E&xit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.injectionOptionsToolStripMenuItem,
            this.proxyConfigurationToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // injectionOptionsToolStripMenuItem
            // 
            this.injectionOptionsToolStripMenuItem.Name = "injectionOptionsToolStripMenuItem";
            this.injectionOptionsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.injectionOptionsToolStripMenuItem.Text = "Injection Options";
            this.injectionOptionsToolStripMenuItem.Click += new System.EventHandler(this.injectionOptionsToolStripMenuItem_Click);
            // 
            // proxyConfigurationToolStripMenuItem
            // 
            this.proxyConfigurationToolStripMenuItem.Name = "proxyConfigurationToolStripMenuItem";
            this.proxyConfigurationToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.proxyConfigurationToolStripMenuItem.Text = "Proxy Configuration";
            this.proxyConfigurationToolStripMenuItem.Click += new System.EventHandler(this.proxyConfigurationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statUserMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 625);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(627, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statUserMessage
            // 
            this.statUserMessage.Name = "statUserMessage";
            this.statUserMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // filOpenDialog
            // 
            this.filOpenDialog.FileName = "openFileDialog1";
            this.filOpenDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.filOpenDialog_FileOk);
            // 
            // Absinthe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 647);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.AbsintheTabs);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Absinthe";
            this.Text = "Absinthe";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.AbsintheTabs.ResumeLayout(false);
            this.tabHostInfo.ResumeLayout(false);
            this.tabHostInfo.PerformLayout();
            this.tabSchema.ResumeLayout(false);
            this.grpSchema.ResumeLayout(false);
            this.grpActions.ResumeLayout(false);
            this.tabRecordDownload.ResumeLayout(false);
            this.grpRecordSelection.ResumeLayout(false);
            this.grpOutputFile.ResumeLayout(false);
            this.grpOutputFile.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboPluginSelection;
        private System.Windows.Forms.RadioButton optErrorBasedInjection;
        private System.Windows.Forms.RadioButton optBlindInjection;
        private System.Windows.Forms.Label lblPluginSelection;
        private System.Windows.Forms.Label lblInjectionType;
        private System.Windows.Forms.TabControl AbsintheTabs;
        private System.Windows.Forms.TabPage tabHostInfo;
        private System.Windows.Forms.TabPage tabSchema;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabRecordDownload;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem injectionOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proxyConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private ctlConnection ctlConnection1;
        private ctlUserAuth ctlUserAuth1;
        private ctlParameter FormParameters;
        private System.Windows.Forms.Button butInitializeInjection;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox grpSchema;
        private System.Windows.Forms.TreeView tvwSchema;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button butGetFieldInfo;
        private System.Windows.Forms.Button butGetTableInfo;
        private System.Windows.Forms.Button butGetUsername;
        private System.Windows.Forms.GroupBox grpRecordSelection;
        private System.Windows.Forms.CheckedListBox chklstRecordSelection;
        private System.Windows.Forms.GroupBox grpOutputFile;
        private System.Windows.Forms.Button butRecordsBrowse;
        private System.Windows.Forms.Label lblRecordsFilename;
        private System.Windows.Forms.TextBox txtRecordsFilename;
        private System.Windows.Forms.Button butDownloadRecords;
        private System.Windows.Forms.OpenFileDialog filOpenDialog;
        private System.Windows.Forms.SaveFileDialog filSaveDialog;
        private System.Windows.Forms.CheckBox chkVerifyVersion;
        private System.Windows.Forms.ToolStripStatusLabel statUserMessage;
    }
}