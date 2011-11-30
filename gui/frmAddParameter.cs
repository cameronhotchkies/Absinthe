
//   Liquor Cabinet - A WebApp Penetration Testing Suite
//   This software is Copyright (C) 2005-2007 nummish, 0x90.org
//   $Id: frmAddParameter.cs,v 1.2 2006/02/01 05:54:28 nummish Exp $
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Absinthe.Gui
{
    public partial class frmAddParameter : Form
    {
        private SendParameterData _DataSaveCallback;

        public frmAddParameter(SendParameterData CallBack)
        {
            InitializeComponent();
            _DataSaveCallback = CallBack;
        }

        public frmAddParameter(SendParameterData CallBack, string ParameterName, string ParameterValue, bool Injectable, bool AsString)
        {
            InitializeComponent();
            _DataSaveCallback = CallBack;
            txtParameterName.Text = ParameterName;
            txtParameterValue.Text = ParameterValue;
            chkInjectableParameter.Checked = Injectable;
            chkParameterAsString.Checked = AsString;

            _BackupAsString = AsString;
            _BackupInjectable = Injectable;
            _BackupName = ParameterName;
            _BackupValue = ParameterValue;
            _IsEdit = true;
        }

        public delegate void SendParameterData(string ParameterName, string ParameterValue, bool Injectable, bool AsString);
         
        private void butOK_Click(object sender, EventArgs e)
        {
            _DataSaveCallback(txtParameterName.Text, txtParameterValue.Text, chkInjectableParameter.Checked, chkParameterAsString.Checked);
            this.Close();
        }

        private string _BackupName, _BackupValue;
        private bool _BackupInjectable, _BackupAsString, _IsEdit = false;

        private void butCancel_Click(object sender, EventArgs e)
        {
            if (_IsEdit)
            {
                _DataSaveCallback(_BackupName, _BackupValue, _BackupInjectable, _BackupAsString);
            }

            this.Close();
      
        }
    }
}
