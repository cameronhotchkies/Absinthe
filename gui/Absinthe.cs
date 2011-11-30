//   Absinthe - Front End for the Absinthe SQL Injection Tool
//   This software is Copyright (C) 2005-2007 nummish, 0x90.org
//   $Id: Absinthe.cs,v 1.10 2006/08/14 23:00:46 nummish Exp $
//
//   This program is free software; you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation; either version 2 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class Absinthe : Form
    {
        private delegate void ThreadedSub();

        private DataStore _AbsintheState;
        private LocalSettings _AppSettings = new LocalSettings(Assembly.GetExecutingAssembly());
	 
        public Absinthe()
        {
            InitializeComponent();
            _AbsintheState = new DataStore();
            _AbsintheState.UserStatus += new UserEvents.UserStatusEventHandler(UserStatus);
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filOpenDialog.Title = "Open a saved Absinthe session ... ";
            filOpenDialog.AddExtension = true;
            filOpenDialog.FileName = string.Empty; 
            filOpenDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            filOpenDialog.DefaultExt = "xml";
            filOpenDialog.ShowDialog(this);
        }

        private void LoadTargetPage()
        {
            ctlConnection1.ConnectMethod = _AbsintheState.ConnectionMethod;
            ctlConnection1.TargetUrl = _AbsintheState.TargetURL;  

            optBlindInjection.Checked = false; optErrorBasedInjection.Checked = false;
            
            if (_AbsintheState.TargetAttackVector != null)
            {
                switch (_AbsintheState.TargetAttackVector.ExploitType)
                {
                    case GlobalDS.ExploitType.ErrorBasedTSQL:
                        optErrorBasedInjection.Checked = true;                     
                        break;
                    case GlobalDS.ExploitType.BlindSQLInjection:
                        optBlindInjection.Checked = true;
                        break;
                }
            }
            else // Solution for Bug #58
            {
                if (_AbsintheState.IsBlind)
                    optBlindInjection.Checked = true;
                else
                {
                    optErrorBasedInjection.Checked = true;       
                }
            }
            if (_AbsintheState.LoadedPluginName != null)
            {
                cboPluginSelection.SelectedIndex = cboPluginSelection.FindStringExact(_AbsintheState.LoadedPluginName);
            }

            LoadParamsFromStore();
            LoadCookiesFromStore();
           
            LoadAuthDataFromStore();
        }

        private void LoadAuthDataFromStore()
        {
            switch (_AbsintheState.AuthenticationMethod)
            {
                case GlobalDS.AuthType.None:
                    ctlUserAuth1.SetNoAuth();                    
                    break;
                case GlobalDS.AuthType.Basic:
                    ctlUserAuth1.SetBasicAuth(_AbsintheState.AuthUser, _AbsintheState.AuthPassword);
                    break;
                case GlobalDS.AuthType.Digest:
                    ctlUserAuth1.SetDigestAuth(_AbsintheState.AuthUser, _AbsintheState.AuthPassword);
                    break;
                case GlobalDS.AuthType.NTLM:
                    ctlUserAuth1.SetNTLMAuth(_AbsintheState.AuthUser, _AbsintheState.AuthPassword, _AbsintheState.AuthDomain);
                    break;
            }
        }

        private void LoadParamsFromStore()
        {
            FormParameters.ClearParameters();
            foreach (System.Collections.DictionaryEntry den in _AbsintheState.ParameterTable)
            {
                GlobalDS.FormParam fp = (GlobalDS.FormParam)den.Value;
                FormParameters.AddParameter(fp.Name, fp.DefaultValue, fp.Injectable, fp.AsString);
            }
        }

        private void LoadCookiesFromStore()
        {
            FormParameters.ClearCookies();

            if (_AbsintheState.Cookies == null) return;

            foreach (System.Collections.DictionaryEntry cookie in _AbsintheState.Cookies)
            {
                FormParameters.AddCookie((string) cookie.Key, (string) cookie.Value);
            }
        }

        private void LoadSchemaPage()
        {
            if (_AbsintheState.Username != null && _AbsintheState.Username.Length > 0)
            {
                SafelyUpdateUsername();
            }

            if (_AbsintheState.TableList != null)
            {
                SafelyUpdateTableInfo();                           
                butGetFieldInfo.Visible = true; butGetFieldInfo.Enabled = true;                               
            }
        }

        private void SafelyUpdateUsername()
        {
            //TODO: actually replace this with a threadsafe version
            tvwSchema.Nodes[0].Nodes[0].Nodes.Clear();
            tvwSchema.Nodes[0].Nodes[0].Nodes.Add(_AbsintheState.Username);
        }

        private delegate void UpdateTableInfoCallback();

        /// <summary>
        /// This is now thread safe.
        /// </summary>
        private void SafelyUpdateTableInfo()
        {

            if (tvwSchema.InvokeRequired)
            {
                this.Invoke(new UpdateTableInfoCallback(SafelyUpdateTableInfo), new object[] {  });
            }
            else
            {
                // Clear out the old cruft
                tvwSchema.Nodes[0].Nodes[1].Nodes.Clear();

                TreeNode TableNodeParent = tvwSchema.Nodes[0].Nodes[1];

                // ArrayList IdList = new ArrayList();

                for (int i = 0; i < _AbsintheState.TableList.Length; i++)
                {
                    TreeNode TableNode = TableNodeParent.Nodes.Add(_AbsintheState.TableList[i].Name);
                    TableNode.Nodes.Add("ID").Nodes.Add(_AbsintheState.TableList[i].ObjectID.ToString());
                    TableNode.Nodes.Add("Record Count").Nodes.Add(_AbsintheState.TableList[i].RecordCount.ToString());

                    TreeNode FieldNodeParent = TableNode.Nodes.Add("Fields");

                    if (_AbsintheState.TableList[i].FieldCount > 0)
                    {
                        LoadFieldDataFromTableName(_AbsintheState.TableList[i].Name, FieldNodeParent, true);
                    }
                    else
                        FieldNodeParent.Nodes.Add("??? UNKNOWN ???");

                }
            }
        } 

        private void LoadRecordsPage()
        {
            ReloadAvailableFields();
        }

        private void ReloadAvailableFields()
        {
            chklstRecordSelection.Items.Clear();

            if (_AbsintheState.TableList == null) return;
                        
            foreach (GlobalDS.Table table in _AbsintheState.TableList)
            {
                if (table.FieldCount > 0)
                {
                    foreach (GlobalDS.Field field in table.FieldList)
                    {
                        chklstRecordSelection.Items.Add(table.Name + "." + field.FieldName);
                    }
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filSaveDialog.FileOk += new CancelEventHandler(SaveCurrentSession);
            filSaveDialog.Title = "Save the current Absinthe session ... ";
            filSaveDialog.AddExtension = true;
            filSaveDialog.FileName = string.Empty;
            filSaveDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            filSaveDialog.DefaultExt = "xml";
            filSaveDialog.ShowDialog(this);            
            
        }

        private void SaveCurrentSession(Object sender, CancelEventArgs e)
        {
            filSaveDialog.FileOk -= new CancelEventHandler(SaveCurrentSession);
            _AbsintheState.OutputFile = filSaveDialog.FileName;

            saveToolStripMenuItem_Click(null, null);
        }

        private void injectionOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InjectionOptionsForm iof = new InjectionOptionsForm(ref _AbsintheState);

            iof.ShowDialog();
        }

        private void optBlindInjection_CheckedChanged(object sender, EventArgs e)
        {
            InjectionMethodChanged();
        }

        // This is the working array of plugin names. This is not the full list.
        private string[] _PluginEntries;

        private void InjectionMethodChanged()
        {
            lblPluginSelection.Enabled = true;
            cboPluginSelection.Enabled = true;

            cboPluginSelection.Items.Clear();
            cboPluginSelection.Text = string.Empty;
            LoadPluginList();

            foreach (string PluginName in _PluginEntries)
            {
                cboPluginSelection.Items.Add(PluginName);
            }

            chkVerifyVersion.Enabled = optErrorBasedInjection.Checked && !(cboPluginSelection.SelectedItem == null) && !cboPluginSelection.SelectedItem.ToString().Equals("Auto-Detect");
        }
 
        private void optErrorBasedInjection_CheckedChanged(object sender, EventArgs e)
        {
            InjectionMethodChanged();
        }

        private void LoadPluginList()
        {
            List<IPlugin> al = _AbsintheState.PluginList;
            List<string> NameList = new List<string>();

            if (optErrorBasedInjection.Checked)
            {
                NameList.Add("Auto-Detect");
            }

            foreach (IPlugin pt in al)
            {
                if ((optBlindInjection.Checked && pt.GetType().GetInterface("IBlindPlugin") != null) ||
                        (optErrorBasedInjection.Checked && pt.GetType().GetInterface("IErrorPlugin") != null))
                {
                    if (!NameList.Contains(pt.PluginDisplayTargetName))
                    {
                        NameList.Add(pt.PluginDisplayTargetName);
                    }
                    else
                    {
                        string EntryName = pt.PluginDisplayTargetName;
                        string Modifier = ""; int ModCounter = 0;
                        while (NameList.Contains(EntryName + Modifier))
                        {
                            ModCounter++;
                            Modifier = " {" + ModCounter + "}";
                        }
                        NameList.Add(EntryName + Modifier);
                    }
                }
            }

            _PluginEntries = NameList.ToArray();
        }

        private void cboPluginSelection_SelectedIndexChanged(object sender, EventArgs e)
        {            
            _AbsintheState.LoadedPluginName = cboPluginSelection.SelectedItem.ToString();
            chkVerifyVersion.Enabled = optErrorBasedInjection.Checked && !(cboPluginSelection.SelectedItem == null) && !cboPluginSelection.SelectedItem.ToString().Equals("Auto-Detect");
            chkVerifyVersion.Checked = chkVerifyVersion.Checked && (optErrorBasedInjection.Checked && !(cboPluginSelection.SelectedItem == null) && !cboPluginSelection.SelectedItem.ToString().Equals("Auto-Detect"));        
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout AboutForm = new frmAbout(_AbsintheState.PluginList.ToArray());
            AboutForm.ShowDialog();
        }

        private void proxyConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProxyDialog proxies = new ProxyDialog();
            proxies.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(1);
        }

        private void butInitializeInjection_Click(object sender, EventArgs e)
        {
            ThreadedSub a = new ThreadedSub(InitializeAttackVectors);
            a.BeginInvoke(null, new object());
        }
         

        private void ThreadUnsafeDisplayErrorReportDialog(string VersionString, string HavingError)
        {
            ErrorReportDialog erd = new ErrorReportDialog(cboPluginSelection.SelectedItem.ToString(), VersionString, HavingError, _AppSettings.RotatedProxy());
            erd.ShowDialog();
        }


        private delegate void ChangeSelectedPluginDelegate(string NewPluginText);

        private void ChangeSelectedPluginText(string NewPluginText)
        {
            if (cboPluginSelection.InvokeRequired)
            {
                this.Invoke(new ChangeSelectedPluginDelegate(ChangeSelectedPluginText), new object[] { NewPluginText });
            }
            else
            {
                cboPluginSelection.SelectedIndex = cboPluginSelection.FindStringExact(NewPluginText);
                _AbsintheState.LoadedPluginName = NewPluginText; // The event doesn't grab this properly
            }
                
        }
         
        private IErrorPlugin AutoDetectPlugin(AttackVectorFactory avf)
        {
            List<IErrorPlugin> PluginList = new List<IErrorPlugin>();

            foreach (IPlugin ep in _AbsintheState.PluginList)
            {
                if (ep.GetType().GetInterface("IErrorPlugin") != null)
                    PluginList.Add((IErrorPlugin) ep);
            }

            IErrorPlugin[] pl = SqlErrorAttackVector.AutoDetectPlugins(PluginList.ToArray(), avf, (_AppSettings.ProxyInUse) ? _AppSettings.RotatedProxy() : null);

            if (pl.Length == 1)
            {
                ChangeSelectedPluginText(pl[0].PluginDisplayTargetName);
                return pl[0];
            }
            else if (pl.Length == 0)
            {
                ChangeSelectedPluginText(PluginList[0].PluginDisplayTargetName);
                return PluginList[0];
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Multiple plugins support this version of SQL Server. Please select one of the following:");
                foreach (IErrorPlugin epl in pl)
                {
                    sb.Append(Environment.NewLine).Append(epl.PluginDisplayTargetName);
                }

                UserMessage(sb.ToString());
                // handle this
                return null;
            }
        }

        
        private void butGetUsername_Click(object sender, EventArgs e)
        {
            ThreadedSub a = new ThreadedSub(RetrieveUsernameFromDatabase);
            a.BeginInvoke(null, new object());
        }

       
        
        private void butGetTableInfo_Click(object sender, EventArgs e)
        {
	    	ThreadedSub a = new ThreadedSub(RetrieveTableInfoFromDatabase);
    		a.BeginInvoke(null, new object());
	    }

        /// <summary>
        /// This is basically a "Make Things Thread Safe Mux"
        /// </summary>
        private void UnSafelyUpdateUI()
        {
            //TODO: There's lots of garbage to insert here
            lock (_ThreadGuiMutex)
            {
                switch ((LocalGuiAction)_LocalGuiQueue.Dequeue())
                {
                    case LocalGuiAction.UsernameInfo:
                        tvwSchema.Nodes[0].Nodes[0].Nodes.Clear();
                        tvwSchema.Nodes[0].Nodes[0].Nodes.Add(_AbsintheState.Username);
                        break;
                    case LocalGuiAction.ControlEnable:           
                        ((Control)_LocalGuiQueue.Dequeue()).Enabled = (bool)_LocalGuiQueue.Dequeue();
                        break;
                    case LocalGuiAction.RefreshTableInfo:
                        SafelyUpdateTableInfo();                        
                        break;
                    case LocalGuiAction.PartialTableInfo:
                        GlobalDS.Table ModifiedTable = (GlobalDS.Table)_LocalGuiQueue.Dequeue();
                        ThreadUnsafeUpdatePartialTable(ModifiedTable);                     
                        break;
                    case LocalGuiAction.FieldInfo:
                        SafelyUpdateTableInfo();
                        break;
                }
            }
        }

        private void ThreadUnsafeUpdatePartialTable(GlobalDS.Table NewTable)
        {
            // Search for this node
            bool FoundNode = false;
            TreeNode tid = null;            
            TreeNode SearchID;
             
            for (int i=0; i < tvwSchema.Nodes[0].Nodes[1].Nodes.Count; i++)
            {
                SearchID = tvwSchema.Nodes[0].Nodes[1].Nodes[i];

                if (SearchID.Nodes[0].Nodes[0].Text.Equals(NewTable.ObjectID.ToString()))
                {
                    tid = SearchID;
                    FoundNode = true; 
                    break;
                }
            }

            if (!FoundNode)
            {
                tid = tvwSchema.Nodes[0].Nodes[1].Nodes.Add(NewTable.Name);
            }

            tid.Nodes.Clear();
            tid.Nodes.Add("ID").Nodes.Add(NewTable.ObjectID.ToString());
            tid.Nodes.Add("Record Count").Nodes.Add(NewTable.RecordCount.ToString());
            tid.Nodes.Add("Fields").Nodes.Add("??? UNKNOWN ???");

        }

        /// <summary>Can safely be called by any thread to update the cursor</summary>
        /// <param name="NewCursor">The cursor to change to</param>
        private void SafelyChangeCursor(Cursor NewCursor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CursorChangeDelegate(SafelyChangeCursor), new object[] { NewCursor });
            }
            else
                this.Cursor = NewCursor;
        }

        private delegate void CursorChangeDelegate(Cursor NewCursor);
        private delegate void SetStatusCallback(string Message);
        delegate void ErrorReportingDelegate(string VersionError, string HavingError);
        private delegate void GenericArgFunctionDelegate(object[] Args);

        private enum LocalGuiAction : byte
        {
            //
            //FieldInfo,
            UsernameInfo,
            FieldInfo,
            RefreshTableInfo,
            PartialTableInfo,
            ControlEnable
        }

       
        private void UserStatus(string Message)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new SetStatusCallback(UserStatus), new object[] { Message });
            }
            else
                statUserMessage.Text = Message;
        }

        private System.Collections.Queue _LocalGuiQueue = new System.Collections.Queue();
        private object _ThreadGuiMutex = new object();
 
        private void InitializeAttackVectors()
        {
            string URL;

            URL = ctlConnection1.UseSsl == true ? "https://" : "http://";
            URL += ctlConnection1.TargetUrl;

            string Method = ctlConnection1.ConnectMethod;

            if (Method.Equals("")) return;

            SafelyChangeCursor(Cursors.WaitCursor);

            // Generate StringDict
            string TargetName, TargetField;
            bool InjectAsString;
            TargetName = String.Empty; TargetField = String.Empty;

            NameValueCollection Others = new NameValueCollection();
            NameValueCollection Cookies = new NameValueCollection();

            Others = FormParameters.FormParameters(ref TargetName, ref TargetField, out InjectAsString);
            Cookies = FormParameters.Cookies;

            if (TargetName.Equals(String.Empty))
            {
                UserStatus("No Injection Point Found");
                SafelyChangeCursor(Cursors.Default);
                return;
            }

            UserStatus("Beginning Preliminary Scan");

            try
            {
                SafelyChangeEnableOfControl(butInitializeInjection, false);

                AttackVectorFactory avf;

                InjectionOptions opts;
                if (optBlindInjection.Checked == true)
                {
                    opts = new BlindInjectionOptions();

                    ((BlindInjectionOptions)opts).Tolerance = _AbsintheState.FilterTolerance;
                    ((BlindInjectionOptions)opts).Delimiter = _AbsintheState.FilterDelimiter;
                }
                else
                {
                    opts = new ErrorInjectionOptions();
                    ((ErrorInjectionOptions)opts).VerifyVersion = chkVerifyVersion.Checked;
                }


                opts.TerminateQuery = _AbsintheState.TerminateQuery;
                opts.Cookies = Cookies;
                opts.WebProxies = _AppSettings.ProxyQueue();
                opts.InjectAsString = InjectAsString;
                opts.UserAgent = _AbsintheState.UserAgent;


                opts.AuthCredentials = ctlUserAuth1.NetworkCredential;
                opts.AppendedQuery = _AbsintheState.AppendedText;

                avf = new AttackVectorFactory(URL, TargetName, TargetField, Others, Method, opts);
                avf.UserStatus += new UserEvents.UserStatusEventHandler(UserStatus);

                int PluginNumber = Array.IndexOf(_PluginEntries, _AbsintheState.LoadedPluginName);

                IPlugin pt = null;

                if (optBlindInjection.Checked)
                {
                    foreach (IPlugin bp in _AbsintheState.PluginList)
                    {
                        if (bp.GetType().GetInterface("IBlindPlugin") != null)
                        {
                            if (bp.PluginDisplayTargetName == _AbsintheState.LoadedPluginName)
                            {
                                pt = (IPlugin)bp;
                                break;
                            }
                        }
                    }

                    _AbsintheState.TargetAttackVector = avf.BuildBlindSqlAttackVector(_AbsintheState.FilterTolerance, (IBlindPlugin)pt);
                    UserStatus("Finished initial scan");
                }
                else if (optErrorBasedInjection.Checked)
                {
                    if (PluginNumber <= 0)
                    {
                        pt = AutoDetectPlugin(avf);
                    }
                    else
                    {
                        foreach (IPlugin ep in _AbsintheState.PluginList)
                        {
                            if (ep.PluginDisplayTargetName == _AbsintheState.LoadedPluginName)
                            {
                                pt = (IPlugin)ep;                                
                                break;                             
                            }
                        }
                    }
                    if (pt != null)
                    {
                        try
                        {
                            _AbsintheState.TargetAttackVector = avf.BuildSqlErrorAttackVector((IErrorPlugin)pt);
                            UserStatus("Finished initial scan");
                        }
                        catch (UnsupportedSQLErrorVersionException sqlex)
                        {
                            ErrorReportingDelegate ts = new ErrorReportingDelegate(ThreadUnsafeDisplayErrorReportDialog);
                            this.Invoke(ts, new object[] { sqlex.VersionErrorPageHtml, sqlex.HavingErrorPageHtml });
                        }
                    }
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                UserStatus(e.Message);
            }
            finally
            {
                SafelyChangeEnableOfControl(butInitializeInjection, true);
                SafelyChangeCursor(Cursors.Default);
            }

        }
       
        private void RetrieveUsernameFromDatabase()
        {
            SafelyChangeCursor(Cursors.WaitCursor);

            try
            {
                UserStatus("Retrieving username");

                if (_AbsintheState.TargetAttackVector != null)
                {
                    _AbsintheState.Username = _AbsintheState.TargetAttackVector.GetDatabaseUsername();

                    lock (_ThreadGuiMutex) { _LocalGuiQueue.Enqueue(LocalGuiAction.UsernameInfo); }
                    this.Invoke(new ThreadedSub(UnSafelyUpdateUI));
                }
                else
                {
                    UserMessage("Please initialize the system!");
                    SafelyChangeCursor(Cursors.Default);
                    return;
                }
            }
            catch (Exception e)
            {
                UserMessage(e.ToString());
                SafelyChangeCursor(Cursors.Default);
                throw e;
            }
            UserStatus("Username retrieved");
            SafelyChangeCursor(Cursors.Default);
        }

        private void UserMessage(string Message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetStatusCallback(UserMessage), new object[] { Message });
            }
            else
                MessageBox.Show(Message, "User Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private delegate void ChangeEnableCallback(Control ToChange, bool Condition);

        private void SafelyChangeEnableOfControl(Control ToChange, bool Condition)
        {
            if (ToChange.InvokeRequired)
                this.Invoke(new ChangeEnableCallback(SafelyChangeEnableOfControl), new object[] { ToChange, Condition });
            else
                ToChange.Enabled = Condition;
        }

        private void TargetAttackVector_TableChanged(GlobalDS.Table ChangedTable)
        {
            lock (_ThreadGuiMutex)
            {
                _LocalGuiQueue.Enqueue(LocalGuiAction.PartialTableInfo);
                _LocalGuiQueue.Enqueue(ChangedTable);
            }

            this.Invoke(new ThreadedSub(UnSafelyUpdateUI));
            _AbsintheState.PartialTable = ChangedTable;
        }

        private void RetrieveTableInfoFromDatabase()
        {
            try
            {
                _AbsintheState.TargetAttackVector.TableChanged += new TableChangedEventHandler(TargetAttackVector_TableChanged);
                SafelyChangeEnableOfControl(butGetTableInfo, false);

                UserStatus("Gathering Table Information");

                if (_AbsintheState.TargetAttackVector != null)
                {
                    if (_AbsintheState.AllTablesRetrieved)
                        _AbsintheState.TableList = _AbsintheState.TargetAttackVector.GetTableList();
                    else
                        _AbsintheState.TableList = _AbsintheState.TargetAttackVector.RecoverTableList(_AbsintheState.TableList);

                    lock (_ThreadGuiMutex) { _LocalGuiQueue.Enqueue(LocalGuiAction.RefreshTableInfo); }
                    this.Invoke(new ThreadedSub(UnSafelyUpdateUI));
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("This (Target Attack Vector) really shouldn't be null");
                }
                UserStatus("Finished Gathering Table Information");

                SafelyChangeEnableOfControl(butGetFieldInfo, true);
            }
            catch (Exception e)
            {
                UserMessage(e.ToString());
            }
            finally
            {
                SafelyChangeEnableOfControl(butGetTableInfo, true);
                _AbsintheState.TargetAttackVector.TableChanged -= new TableChangedEventHandler(TargetAttackVector_TableChanged);
            }
        }

        private void LoadFieldData()
        {
            LoadFieldData(false);
        }

        private void LoadFieldData(bool OnlyRepopulate)
        {
            if (_AbsintheState.TableList != null)
            {
                /* Give us the current selection */
                TreeNode item = tvwSchema.SelectedNode;

                while (item.Level > 2)
                {
                    item = item.Parent;
                }


                // else it was the parent
                System.Diagnostics.Debug.WriteLine(item.Text);
                System.Diagnostics.Debug.WriteLine("Should be in now");

                LoadFieldDataSub a = new LoadFieldDataSub(LoadFieldDataFromTableName);
                a.BeginInvoke(item.Text, item.LastNode, OnlyRepopulate, null, new object());
            }
            else
            {
                UserMessage("Unexpected error.  Please make sure Absinthe is initialized.");
            }
        }

        private delegate void LoadFieldDataSub(string TableName, TreeNode FieldParentNode, bool OnlyRepopulate);

        private void LoadFieldDataFromTableName(string TableName, TreeNode FieldParentNode, bool OnlyRepopulate)
        {
            for (int i = 0; i < _AbsintheState.TableList.Length; i++)
            {
                if (_AbsintheState.TableList[i].Name.Equals(TableName))
                {
                    if (OnlyRepopulate)
                    {
                        for (int j = 0; j < _AbsintheState.TableList[i].FieldCount; j++)
                        {
                            GlobalDS.Field[] FieldList = _AbsintheState.TableList[i].FieldList;
                            string FieldName = FieldList[j].FieldName;
                            string FieldType = FieldList[j].DataType.ToString();
                            string FieldInfo = FieldName + " (" + FieldType + ")";
                            TreeNode FNode = FieldParentNode.Nodes.Add(FieldInfo);
                            if (FieldList[j].IsPrimary) FNode.NodeFont = new Font(Control.DefaultFont, FontStyle.Bold);

                            //Add in to Download Field
                            if (!chklstRecordSelection.Items.Contains(TableName + "." + FieldName))
                            {
                                chklstRecordSelection.Items.Add(TableName + "." + FieldName);
                            }
                        }
                    }
                    else
                    {
                        _AbsintheState.TargetAttackVector.PopulateTableStructure(ref _AbsintheState.TableList[i]);

                        if (_AbsintheState.TableList[i].FieldCount > 0)
                        {
                            lock (_ThreadGuiMutex)
                            {
                                _LocalGuiQueue.Enqueue(LocalGuiAction.FieldInfo);
                            }
                            this.Invoke(new ThreadedSub(UnSafelyUpdateUI));
                        }
                    }
                }
            }
        }

        private void butGetFieldInfo_Click(object sender, EventArgs e)
        {
            LoadFieldData();
        }

        private void butRecordsBrowse_Click(object sender, EventArgs e)
        {
            filSaveDialog.FileOk += new CancelEventHandler(DownloadRecords_FileOk);
            filSaveDialog.Title = "Save the downloaded records to ... ";
            filSaveDialog.AddExtension = true;
            filSaveDialog.FileName = string.Empty;
            filSaveDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            filSaveDialog.DefaultExt = "xml";
            filSaveDialog.ShowDialog(this);
        
          
        }

        private void DownloadRecords_FileOk(Object sender, CancelEventArgs e)
        {
            filSaveDialog.FileOk -= new CancelEventHandler(DownloadRecords_FileOk);            
            txtRecordsFilename.Text = filSaveDialog.FileName;
        }

        private void filSaveDialog_FileOk(object sender, CancelEventArgs e)
        {
            string Filename = filSaveDialog.FileName;
            if (Filename.Length > 0)
            {
                _AbsintheState.OutputFile = Filename;

                saveToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_AbsintheState.OutputFile.Length == 0)
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            PrepDataForSave();
 
            try
            {
                _AbsintheState.OutputToFile(_AbsintheState.LoadedPluginName);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        private void PrepDataForSave()
        {
            _AbsintheState.TargetURL = ctlConnection1.TargetUrl;
            _AbsintheState.IsBlind = optBlindInjection.Checked;

            foreach (GlobalDS.FormParam fp in FormParameters.ExportParameters)
            {
                _AbsintheState.AddFormParameter(fp);
            }


            foreach (GlobalDS.FormParam fp in FormParameters.Cookies)
            {
                _AbsintheState.AddCookie(fp);
            }
       
            if (ctlUserAuth1.NetworkCredential == null)
            {
                _AbsintheState.Authdata(GlobalDS.AuthType.None);
            }
            else
            {
                _AbsintheState.Authdata(ctlUserAuth1.AuthType, ctlUserAuth1.NetworkCredential);                
            }
        }

        private void filOpenDialog_FileOk(object sender, CancelEventArgs e)
        {
            _AbsintheState.UserStatus += new UserEvents.UserStatusEventHandler(UserStatus);

            try
            {
                _AbsintheState.LoadXmlFile(filOpenDialog.FileName, _AppSettings.ProxyQueue());
            }
            catch (DataStore.InvalidDataFileException idfe)
            {
                MessageBox.Show(idfe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            try
            {
                LoadTargetPage();
                LoadSchemaPage();
                LoadRecordsPage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }			
        }

        private void tvwSchema_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void butDownloadRecords_Click(object sender, EventArgs e)
        {
            if (chklstRecordSelection.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select some records to download","Missing Information",MessageBoxButtons.OK);
                return;
            }

            if (txtRecordsFilename.Text.Length == 0)
            {
                MessageBox.Show("No file chosen for download target", "Missing Information", MessageBoxButtons.OK);
                return;
            }

            ThreadedSub a = new ThreadedSub(PullDataFromTables);
            a.BeginInvoke(null, new object());
        }

        private void PullDataFromTables()
        {
            Dictionary<string, List<string>> TableSet = new Dictionary<string, List<string>>();
            string TableName, FieldName;
            List<string> al;


            for (int cnt = 0; cnt < chklstRecordSelection.CheckedItems.Count; cnt++)
            {
                string FullName = chklstRecordSelection.CheckedItems[cnt].ToString();

                TableName = FullName.Split(new char[] { '.' })[0];

                FieldName = FullName.Split(new char[] { '.' })[1];

                //// Do we need this?
                //li.Column = 2;
                //li.Mask = ListCtrl.wxLIST_MASK_TEXT;
                //lstSelectedFields.GetItem(ref li);
                //FieldID = Int64.Parse(li.Text) + 1L;

                // add fieldid to stuff.
                if (!TableSet.ContainsKey(TableName))
                {
                    al = new List<string>();
                    TableSet.Add(TableName, al);                    
                }

                al = TableSet[TableName];
                al.Add(FieldName);
                TableSet[TableName] = al;

            }

            int TabCount = 0;
            List<long[]> ColAl = new List<long[]>();
            List<GlobalDS.Table> TblAl = new List<GlobalDS.Table>();              

            foreach (string tab in TableSet.Keys)
            {
                List<long> ColumnList = new List<long>();
                foreach (GlobalDS.Table tbl in _AbsintheState.TableList)
                {
                    if (tbl.Name == tab)
                    {
                        int counter = 0;
                        foreach (GlobalDS.Field fld in tbl.FieldList)
                        {
                            if (TableSet[tab].Contains(fld.FieldName))
                            {
                                ColumnList.Add(counter);
                            }
                            counter++;
                        }
                    }                
                }
                
                TblAl.Add(_AbsintheState.GetTableFromName(tab));
                ColAl.Add(ColumnList.ToArray());

                TabCount++;
            }

            _AbsintheState.TargetAttackVector.PullDataFromTable(TblAl.ToArray(), ColAl.ToArray(), txtRecordsFilename.Text);

            UserMessage("Data Retrieved");   
        }

        private void FormParameters_Load(object sender, EventArgs e)
        {

        }
          
    }
}