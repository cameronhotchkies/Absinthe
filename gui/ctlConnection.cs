using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Absinthe.Gui
{
    public partial class ctlConnection : UserControl
    {
        public ctlConnection()
        {
            InitializeComponent();
        }

        /// <summary>Indicates if the connection is over SSL</summary>
        public bool UseSsl
        {
            get
            {
                return chkUseSsl.Checked;
            }
        }

        /// <summary>The URL of the target host</summary>
        public string TargetUrl
        {
            get
            {
                return txtTargetUrl.Text;
            }
            set
            {
                txtTargetUrl.Text = value;
            }
        }

        /// <summary>An uppercase string representing the action method of the form</summary>
        public string ConnectMethod
        {
            get
            {
                return optConnectPost.Checked ? "POST" : "GET";
            }
            set
            {
                if (value.ToUpper().Equals("POST"))
                    optConnectPost.Checked = true;
                else
                    optConnectGet.Checked = true;
            }

        }

        /// <summary>Change the label whenever the checkbox is changed</summary>
        private void chkUseSsl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseSsl.Checked)
                lblConnectionProtocol.Text = "https://";         
            else            
                lblConnectionProtocol.Text = "http://";            
        }
  
        private void ctlConnection_Load(object sender, EventArgs e)
        {

        }

      
    }
}
