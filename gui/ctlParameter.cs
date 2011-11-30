
//   Liquor Cabinet - A WebApp Penetration Testing Suite
//   This software is Copyright (C) 2005-2007 nummish, 0x90.org
//   $Id: ctlParameter.cs,v 1.6 2006/08/14 23:01:16 nummish Exp $
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class ctlParameter : UserControl
    {
        private List<GlobalDS.FormParam> _FormParameters;
        private List<GlobalDS.FormParam> _Cookies;

        public ctlParameter()
        {
            InitializeComponent();
            _FormParameters = new List<GlobalDS.FormParam>();
            _Cookies = new List<GlobalDS.FormParam>();
        }

        public void AddParameter(string ParameterName, string ParameterValue)
        {
            AddParameter(ParameterName, ParameterValue, false, false);
        }

        public void AddParameter(string ParameterName, string ParameterValue, bool Injectable, bool AsString)
        {           
            lstFormParameters.Items.Add(new ListViewItem(new string[] {ParameterName,ParameterValue,Injectable.ToString(), AsString.ToString()}));
            GlobalDS.FormParam fp;
            fp.Name = ParameterName;
            fp.DefaultValue = ParameterValue;
            fp.Injectable = Injectable;
            fp.AsString = AsString;
            _FormParameters.Add(fp);
        }

        public void AddCookie(string CookieName, string CookieValue)
        {
            AddCookie(CookieName, CookieValue, false, false);
        }

        public void AddCookie(string CookieName, string CookieValue, bool Injectable, bool AsString)
        {
            lstCookies.Items.Add(new ListViewItem(new string[] { CookieName, CookieValue }));
            GlobalDS.FormParam cookie;
            cookie.Name = CookieName;
            cookie.DefaultValue = CookieValue;
            cookie.Injectable = Injectable;
            cookie.AsString = AsString;
            _Cookies.Add(cookie);
        }

        private void EditParameter(string ParameterName, string ParameterValue, bool Injectable, bool AsString)
        {            
            if (lstFormParameters.SelectedIndices.Count > 0)
            {                
                _FormParameters.RemoveAt(lstFormParameters.SelectedIndices[0]);
            }
            lstFormParameters.SelectedItems.Clear();
            AddParameter(ParameterName, ParameterValue, Injectable, AsString);
        }

        private void EditCookie(string CookieName, string CookieValue, bool Injectable, bool AsString)
        {
            if (lstCookies.SelectedIndices.Count > 0)
            {
                _Cookies.RemoveAt(lstCookies.SelectedIndices[0]);
            }
            lstCookies.SelectedItems.Clear();
            AddCookie(CookieName, CookieValue);
        }
  
        private void butAddParameter_Click(object sender, EventArgs e)
        {
            frmAddParameter AddParam;
            if (tabParams.SelectedIndex == 0)
                AddParam = new frmAddParameter(AddParameter);
            else
                AddParam = new frmAddParameter(AddCookie);

            AddParam.ShowDialog();
        }

        

        private void butEditParameter_Click(object sender, EventArgs e)
        {            
            frmAddParameter EditParam;

            if (tabParams.SelectedIndex == 0)
            {
                if (lstFormParameters.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select a parameter to edit");
                    return;
                }
                EditParam = new frmAddParameter(EditParameter, lstFormParameters.SelectedItems[0].SubItems[0].Text,
                    lstFormParameters.SelectedItems[0].SubItems[1].Text, 
                    Convert.ToBoolean(lstFormParameters.SelectedItems[0].SubItems[2].Text), 
                    Convert.ToBoolean(lstFormParameters.SelectedItems[0].SubItems[3].Text));
                               
                lstFormParameters.Items.Remove(lstFormParameters.SelectedItems[0]);
            }
            else
            {
                if (lstCookies.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select a cookie to edit");
                    return;
                }
                EditParam = new frmAddParameter(EditCookie, lstCookies.SelectedItems[0].SubItems[0].Text,
                    lstCookies.SelectedItems[0].SubItems[1].Text,
                    Convert.ToBoolean(lstCookies.SelectedItems[0].SubItems[2].Text),
                    Convert.ToBoolean(lstCookies.SelectedItems[0].SubItems[3].Text));

                lstCookies.Items.Remove(lstCookies.SelectedItems[0]);
            }

            EditParam.ShowDialog();
        }

        public List<GlobalDS.FormParam> ExportParameters
        {
            get
            {
                return _FormParameters;
            }
        }

        public NameValueCollection FormParameters(ref string TargetName, ref string TargetField, out bool AsString)
        {
            bool FoundTarget = false;
            NameValueCollection retVal = new NameValueCollection();
            
            AsString = false;

            foreach (GlobalDS.FormParam fp in _FormParameters)
            {                
                if (!fp.Injectable || FoundTarget)
                {
                    retVal[fp.Name] = fp.DefaultValue;
                }
                else
                {
                    if (fp.AsString)
                    { AsString = true; }
                
                    TargetName = fp.Name;
                    TargetField = fp.DefaultValue;
                    FoundTarget = true;
                }
            }

            return retVal;
        }

        public NameValueCollection Cookies
        {
            get
            {
                NameValueCollection retVal = new NameValueCollection();

                foreach (GlobalDS.FormParam cookie in _Cookies)
                {                    
                    retVal[cookie.Name] = cookie.DefaultValue;
                }

                return retVal;
            }
        }

        public void ClearCookies()
        {
            lstCookies.Items.Clear();
        }

        public void ClearParameters()
        {
            lstFormParameters.Items.Clear();
        }

        private void butRemoveParameter_Click(object sender, EventArgs e)
        {
            if (tabParams.SelectedIndex == 0)
            {
                if (lstFormParameters.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select a parameter to remove");
                    return;
                }

                lstFormParameters.Items.Remove(lstFormParameters.SelectedItems[0]);
            }
            else
            {
                if (lstCookies.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select a cookie to remove");
                    return;
                }
                
                lstCookies.Items.Remove(lstCookies.SelectedItems[0]);
            }
        }
    }
}
