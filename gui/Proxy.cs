
////   Liquor Cabinet - A WebApp Penetration Testing Suite
////   This software is Copyright (C) 2004 txs, 0x90.org
////   $Id: Proxy.cs,v 1.16 2005/12/07 05:16:20 nummish Exp $
////
////   This program is free software; you can redistribute it and/or modify
////   it under the terms of the GNU General Public License as published by
////   the Free Software Foundation; either version 2 of the License, or
////   (at your option) any later version.
////
////   This program is distributed in the hope that it will be useful,
////   but WITHOUT ANY WARRANTY; without even the implied warranty of
////   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
////   GNU General Public License for more details.
////
////   You should have received a copy of the GNU General Public License
////   along with this program; if not, write to the Free Software
////   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

//using System;
//using System.Collections;
//using System.Net;
//using System.Reflection;
 
//namespace LiquorCabinet.Shared
//{
//    public class ProxyConfigDialog : wx.Dialog
//    {
//        // {{{ Control Declarations
//        private CheckBox chkUseProxy;

//        private BoxSizer sizCabinetSizer;

//        private StaticText lblHostname;
//        private TextCtrl txtHostname;
//        private StaticText lblPort;
//        private TextCtrl txtPort;

//        private Button butAdd;
//        private Button butRemove;

//        private ListCtrl lstProxies;

//        private string ParentAsmTitle;
//        private string CurrentAsmTitle;

//        private BoxSizer sizAsmSizer;

//        private StaticText lblAsmHostname;
//        private TextCtrl txtAsmHostname;
//        private StaticText lblAsmPort;
//        private TextCtrl txtAsmPort;

//        private Button butAsmAdd;
//        private Button butAsmRemove;

//        private ListCtrl lstAsmProxies;

//        private Button butOk;
//        private Button butCancel;
//        // }}}

//        private LocalSettings _AppSettings;

//        // {{{ Constructor
//        public ProxyConfigDialog(Window Parent, ref LocalSettings AppSettings, Assembly ParentAssembly) : base(Parent, "Proxy Configuration", wxDefaultPosition, wxDefaultSize, wxSYSTEM_MENU | wxCAPTION | wxCLOSE_BOX | wxFRAME_FLOAT_ON_PARENT)
//        {
//            _AppSettings = AppSettings;
//            InitializeComponent(ParentAssembly);
//        }

//        private void SetupMainProxyList()
//        {
//            pnlMainProxy = new Panel(ntbProxyBook);

//            sizCabinetSizer = new BoxSizer(Orientation.wxVERTICAL);

//            StaticText lblInfo1 = new StaticText(pnlMainProxy, "Any proxies listed below are shared with all of the Liquor Cabinet applications.");
//            sizCabinetSizer.Add(lblInfo1, 0, Direction.wxALL, 5);

//            BoxSizer siz1 = new BoxSizer(Orientation.wxHORIZONTAL);

//            lblHostname = new StaticText(pnlMainProxy, "IP/Hostname:");
//            txtHostname = new TextCtrl(pnlMainProxy, string.Empty);
//            lblPort = new StaticText(pnlMainProxy, "Port:");
//            txtPort = new TextCtrl(pnlMainProxy, string.Empty);

//            siz1.Add(lblHostname, 0, Direction.wxALL | Alignment.wxALIGN_CENTRE_VERTICAL, 2);
//            siz1.Add(txtHostname, 0, Direction.wxALL, 2);
//            siz1.Add(lblPort, 0, Direction.wxALL | Alignment.wxALIGN_CENTRE_VERTICAL, 2);
//            siz1.Add(txtPort, 0, Direction.wxALL, 2);

//            BoxSizer siz2 = new BoxSizer(Orientation.wxHORIZONTAL);

//            butAdd = new Button(pnlMainProxy, "Add");
//            butRemove = new Button(pnlMainProxy, "Remove");
		
//            siz2.Add(butAdd, 0, Direction.wxALL, 5);
//            siz2.Add(butRemove, 0, Direction.wxALL, 5);

//            sizCabinetSizer.Add(siz1, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);
//            sizCabinetSizer.Add(siz2, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);

//            lstProxies = new ListCtrl(pnlMainProxy, new System.Drawing.Point(10, 10), new System.Drawing.Size(200, 200), ListCtrl.wxLC_REPORT);

//            sizCabinetSizer.Add(lstProxies, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);

//            pnlMainProxy.SetSizer(sizCabinetSizer);
//            ntbProxyBook.AddPage(pnlMainProxy, "Liquor Cabinet");
//        }
	 
//        private void InitializeComponent(Assembly asm)
//        {
//            BoxSizer sizMain = new BoxSizer(Orientation.wxVERTICAL);
//            BoxSizer sizLists = new BoxSizer(Orientation.wxHORIZONTAL);
			
//            chkUseProxy = new CheckBox(this, "Use HTTP Proxies");

//            sizMain.Add(chkUseProxy, 0, Direction.wxALL, 5);

//            ntbProxyBook = new Notebook(this, wxDefaultPosition, wxDefaultSize);			

//            SetupMainProxyList(); 
   
//            //sizLists.Add(sizCabinetSizer, 0, Direction.wxALL, 5);
//            sizMain.Add(ntbProxyBook, 1, wx.Stretch.wxEXPAND | Direction.wxALL, 5);

//            ParentAsmTitle = ((AssemblyTitleAttribute) AssemblyTitleAttribute.GetCustomAttribute(asm, typeof (AssemblyTitleAttribute))).Title;
//            Assembly CurrentAsm = Assembly.GetExecutingAssembly();
//            CurrentAsmTitle = ((AssemblyTitleAttribute) AssemblyTitleAttribute.GetCustomAttribute(CurrentAsm, typeof (AssemblyTitleAttribute))).Title;

//            if (!ParentAsmTitle.Equals(CurrentAsmTitle))
//            {
//                SetupParentProxyList();
//            }
			
//            //sizMain.Add(sizLists, 0, Alignment.wxALIGN_CENTER | Direction.wxALL, 5);

//            BoxSizer siz3 = new BoxSizer(Orientation.wxHORIZONTAL);

//            butOk = new Button(this, "OK");
//            butCancel = new Button(this, "Cancel");

//            siz3.Add(butOk, 0, Direction.wxALL, 5);
//            siz3.Add(butCancel, 0, Direction.wxALL, 5);

//            sizMain.Add(siz3, 0, Alignment.wxALIGN_CENTER | Direction.wxALL, 5);

//            SetSizerAndFit(sizMain, true);

//            SetupProxyList(lstProxies, false);

//            if (lstProxies.ItemCount == 0)
//                butRemove.Enabled = false;

//            BindEvents();
//        }

//        Panel pnlParentProxy, pnlMainProxy;
//        Notebook ntbProxyBook;

//        private void SetupParentProxyList()
//        {
//            pnlParentProxy = new Panel(ntbProxyBook);

//            sizAsmSizer = new BoxSizer(Orientation.wxVERTICAL);

//            StaticText lblInfo = new StaticText(pnlParentProxy, "Any proxies listed below will be appended to the Liquor Cabinet proxies.");
//            sizAsmSizer.Add(lblInfo, 0, Direction.wxALL, 5);

//            BoxSizer siz1Asm = new BoxSizer(Orientation.wxHORIZONTAL);

//            lblAsmHostname = new StaticText(pnlParentProxy, "IP/Hostname:");
//            txtAsmHostname = new TextCtrl(pnlParentProxy, string.Empty);
//            lblAsmPort = new StaticText(pnlParentProxy, "Port:");
//            txtAsmPort = new TextCtrl(pnlParentProxy, string.Empty);

//            siz1Asm.Add(lblAsmHostname, 0, Direction.wxALL | Alignment.wxALIGN_CENTRE_VERTICAL, 2);
//            siz1Asm.Add(txtAsmHostname, 0, Direction.wxALL, 2);
//            siz1Asm.Add(lblAsmPort, 0, Direction.wxALL | Alignment.wxALIGN_CENTRE_VERTICAL, 2);
//            siz1Asm.Add(txtAsmPort, 0, Direction.wxALL, 2);

//            BoxSizer siz2Asm = new BoxSizer(Orientation.wxHORIZONTAL);

//            butAsmAdd = new Button(pnlParentProxy, "Add");
//            butAsmRemove = new Button(pnlParentProxy, "Remove");
		
//            siz2Asm.Add(butAsmAdd, 0, Direction.wxALL, 5);
//            siz2Asm.Add(butAsmRemove, 0, Direction.wxALL, 5);

//            sizAsmSizer.Add(siz1Asm, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);
//            sizAsmSizer.Add(siz2Asm, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);

//            lstAsmProxies = new ListCtrl(pnlParentProxy, wxDefaultPosition, new System.Drawing.Size(200, 200), ListCtrl.wxLC_REPORT);

//            sizAsmSizer.Add(lstAsmProxies, 0, Direction.wxALL | Alignment.wxALIGN_CENTER, 5);

////			sizLists.Add(sizAsmSizer, 0, Direction.wxALL, 5);
//            pnlParentProxy.SetSizer(sizAsmSizer);

//            SetupProxyList(lstAsmProxies, true);

//            if (lstAsmProxies.ItemCount == 0)
//                butAsmRemove.Enabled = false;
//            ntbProxyBook.AddPage(pnlParentProxy, ParentAsmTitle);

//        }
		 
//        private void BindEvents()
//        {
//            EVT_BUTTON(butAdd.ID, new wx.EventListener(this.AddProxy_Click));
//            EVT_BUTTON(butRemove.ID, new wx.EventListener(this.RemoveProxy_Click));
//            if (!ParentAsmTitle.Equals(CurrentAsmTitle))
//            {
//                EVT_BUTTON(butAsmAdd.ID, new wx.EventListener(this.AsmAddProxy_Click));
//                EVT_BUTTON(butAsmRemove.ID, new wx.EventListener(this.AsmRemoveProxy_Click));
//            }
//            EVT_BUTTON(butOk.ID, new EventListener(this.butOk_Click));
//            EVT_BUTTON(butCancel.ID, new EventListener(this.butCancel_Click));
//        }
		 
//        // If WhichTable is true we want to get the proxies from the sub-application
//        // hashtable.
//        private void SetupProxyList(ListCtrl lstWhich, bool WhichTable)
//        {
//            lstWhich.InsertColumn(0, "Hostname");
//            lstWhich.InsertColumn(1, "Port");

//            Hashtable tmpTable = _AppSettings.GetProxyTable(WhichTable);

//            foreach (string HostKey in tmpTable.Keys)
//            {
//                WebProxy wp = new WebProxy(HostKey, (int) tmpTable[HostKey]);
//                lstWhich.InsertItem(lstWhich.ItemCount, wp.Address.Host);
//                lstWhich.SetItem(lstWhich.ItemCount - 1, 1, wp.Address.Port.ToString());
//            }

//            chkUseProxy.Value = _AppSettings.ProxyInUse;
//        }
//        // }}}

//        // {{{ VerifyValidProxy
//        // Return true if valid, false if not.
//        // We don't care if the machine is actually a proxy,
//        // only if it isn't already in one of the two lists.
//        // 
//        // asmProxy is true if adding to a sub-application's proxy list.
//        bool VerifyValidProxy(bool asmProxy)
//        {
//            // TODO: Verify valid hostname
//            if((asmProxy && txtAsmHostname.Value.Equals("")) || (!asmProxy && txtHostname.Value.Equals("")))
//            {
//                wx.MessageDialog.MessageBox("A hostname or IP address is required!");
//                return false;
//            }

//            // TODO: Verify valid portname
//            if((asmProxy && txtAsmPort.Value.Equals("")) || (!asmProxy && txtPort.Value.Equals("")))
//            {
//                wx.MessageDialog.MessageBox("A proxy port is required!");
//                return false;
//            }

//            // We still need two seperate loops to go through each proxy list control.
//            // We could do it in one loop and catch any exceptions thrown by going past the
//            // end of one list, but I *think* this will be a faster way of doing it.
//            // Throwing a bunch of exceptions until we get to the end of the longer list
//            // can't be a faster way to do it.
//            for (int cnt = 0; cnt < lstProxies.ItemCount; cnt++)
//            {
//                ListItem li = new ListItem();
//                li.Id = cnt;
//                li.Column = 0;
//                li.Mask = ListCtrl.wxLIST_MASK_TEXT;
//                lstProxies.GetItem(ref li);
//                if ((asmProxy && li.Text.Equals(txtAsmHostname.Value)) || (!asmProxy && li.Text.Equals(txtHostname.Value)))
//                {
//                    wx.MessageDialog.MessageBox("A cabinet proxy with that hostname already exists.");
//                    return false;
//                }
//            }

//            // We need this asmProxy check because we can't loop through the list control if
//            // it has not been created yet.
//            if (!ParentAsmTitle.Equals(CurrentAsmTitle))
//            {
//                for (int cnt = 0; cnt < lstAsmProxies.ItemCount; cnt++)
//                {
//                    ListItem li = new ListItem();
//                    li.Id = cnt;
//                    li.Column = 0;
//                    li.Mask = ListCtrl.wxLIST_MASK_TEXT;
//                    lstAsmProxies.GetItem(ref li);
//                    if ((asmProxy && li.Text.Equals(txtAsmHostname.Value)) || (!asmProxy && li.Text.Equals(txtHostname.Value)))
//                    {
//                        wx.MessageDialog.MessageBox("A custom proxy with that hostname already exists.");
//                        return false;
//                    }
//                }
//            }
			
//            // Parse the port
//            try
//            {
//                if (asmProxy)
//                    Int32.Parse(txtAsmPort.Value);
//                else
//                    Int32.Parse(txtPort.Value);
//            }
//            catch (System.FormatException)
//            {
//                wx.MessageDialog.MessageBox("Invalid proxy port.", "Error!");
//                return false;
//            }
//            catch (Exception ex)
//            {
//                wx.MessageDialog.MessageBox(ex.ToString(), "Error!");
//                return false;
//            }

//            return true;
//        }
//        // }}}

//        // {{{ AddProxy_Click
//        void AddProxy_Click(object sender, Event e)
//        {
//            if (VerifyValidProxy(false))
//            {
//                lstProxies.InsertItem(lstProxies.ItemCount, txtHostname.Value);
//                lstProxies.SetItem(lstProxies.ItemCount - 1, 1, txtPort.Value);

//                txtHostname.Value = "";
//                txtPort.Value = "";

//                butRemove.Enabled = true;
//            }
//        }
//        // }}}

//        // {{{ AsmAddProxy_Click
//        void AsmAddProxy_Click(object sender, Event e)
//        {
//            if (VerifyValidProxy(true))
//            {
//                lstAsmProxies.InsertItem(lstAsmProxies.ItemCount, txtAsmHostname.Value);
//                lstAsmProxies.SetItem(lstAsmProxies.ItemCount - 1, 1, txtAsmPort.Value);

//                txtAsmHostname.Value = "";
//                txtAsmPort.Value = "";

//                butAsmRemove.Enabled = true;
//            }
//        }
//        // }}}

//        // {{{ RemoveProxyFromList
//        void RemoveProxyFromList(ListCtrl lstWhich)
//        {
//            int j = 0;
//            int state;
		
//            while (j != lstWhich.ItemCount) 
//            {
//                state = lstWhich.GetItemState(j, ListCtrl.wxLIST_STATE_SELECTED);
//                if ((state &= ListCtrl.wxLIST_STATE_SELECTED) == ListCtrl.wxLIST_STATE_SELECTED) 
//                    lstWhich.DeleteItem(j); // Incrementing would cause us to skip the next one due to the indexes changing
//                else
//                    j++;
//            }

//            if (lstWhich.ItemCount.Equals(0))
//            {
//                if (lstWhich.Equals(lstAsmProxies))
//                    butAsmRemove.Disable();
//                else
//                    butRemove.Disable();
//            }
//        }
//        // }}}
		
//        // {{{ RemoveProxy_Click
//        void RemoveProxy_Click(object sender, Event e)
//        {
//            RemoveProxyFromList(lstProxies);
//        }
//        // }}}
		
//        // {{{ AsmRemoveProxy_Click
//        void AsmRemoveProxy_Click(object sender, Event e)
//        {
//            RemoveProxyFromList(lstAsmProxies);
//        }
//        // }}}

//        // {{{ butOk_Click
//        void butOk_Click(object sender, Event e)
//        {
//            _AppSettings.ClearProxies();

//            // The list control must actually exist first
//            if (!ParentAsmTitle.Equals(CurrentAsmTitle))
//            {
//                AddProxyWhich(lstAsmProxies);
//                _AppSettings.SaveSettings(true);
//            }

//            // Always add the proxies in the cabinet list control
//            AddProxyWhich(lstProxies);

//            _AppSettings.SaveSettings(false);

//            _AppSettings.BuildProxyQueue();
//            this.Close();
//        }
//        // }}}

//        // {{{ AddProxyWhich
//        // lstWhich is the list control to loop through
//        void AddProxyWhich(ListCtrl lstWhich)
//        {
//            string ProxyHostName;
//            int ProxyPort;

//            for (int cnt = 0; cnt < lstWhich.ItemCount; cnt++)
//            {
//                ListItem li = new ListItem();
//                li.Id = cnt;

//                li.Column = 0;
//                li.Mask = ListCtrl.wxLIST_MASK_TEXT;
//                lstWhich.GetItem(ref li);
//                ProxyHostName = li.Text;

//                li.Column = 1;
//                li.Mask = ListCtrl.wxLIST_MASK_TEXT;
//                lstWhich.GetItem(ref li);
//                ProxyPort = Convert.ToInt32(li.Text);

//                // Add the proxy to the appropriate hash table
//                if (lstWhich.Equals(lstAsmProxies))
//                    _AppSettings.AddProxyToTable(ProxyHostName, ProxyPort, true);
//                else
//                    _AppSettings.AddProxyToTable(ProxyHostName, ProxyPort, false);
//            }
//        }
//        // }}}

//        // {{{ butCancel_Click
//        void butCancel_Click(object sender, Event e)
//        {
//            this.Close();	
//        }
//        // }}}
//    }
//}
