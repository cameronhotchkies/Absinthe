using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class ctlUserAuth : UserControl
    {
        public ctlUserAuth()
        {
            InitializeComponent();
        }

        public GlobalDS.AuthType AuthType
        {
            get
            {
                switch (cboAuthType.SelectedItem.ToString())
                {
                    case "Basic":
                        return GlobalDS.AuthType.Basic;
                    case "Digest":
                        return GlobalDS.AuthType.Digest;
                    case "NTLM":
                        return GlobalDS.AuthType.NTLM;
                    default:
                        return GlobalDS.AuthType.None;
                }
            }
        }

        private void chkUseAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseAuth.Checked)
                EnableAuthFields();
            else
            {
                lblAuthDomain.Enabled = false;
                lblAuthPassword.Enabled = false;
                lblAuthType.Enabled = false;
                lblAuthUsername.Enabled = false;
                cboAuthType.Enabled = false;
                txtAuthDomain.Enabled = false;
                txtAuthPassword.Enabled = false;
                txtAuthUsername.Enabled = false;
            }
        }

        public void SetNoAuth()
        {
            txtAuthDomain.Text = string.Empty;
            txtAuthPassword.Text = string.Empty;
            txtAuthUsername.Text = string.Empty;
            chkUseAuth.Checked = false;
            cboAuthType.SelectedIndex = -1;
            EnableAuthFields();
        }

        public void SetBasicAuth(string Username, string Password)
        {
            txtAuthUsername.Text = Username;
            txtAuthPassword.Text = Password;
            txtAuthDomain.Text = string.Empty;
            cboAuthType.SelectedIndex = cboAuthType.FindStringExact("Basic");
            chkUseAuth.Checked = true;
            EnableAuthFields();
        }

        public void SetDigestAuth(string Username, string Password)
        {
            txtAuthUsername.Text = Username;
            txtAuthPassword.Text = Password;
            txtAuthDomain.Text = string.Empty;
            cboAuthType.SelectedIndex = cboAuthType.FindStringExact("Digest");
            chkUseAuth.Checked = true;
            EnableAuthFields();
        }

        public System.Net.NetworkCredential NetworkCredential
        {
            get
            {
                if (chkUseAuth.Checked)
                {
                    if (cboAuthType.SelectedItem.ToString().Equals("NTLM"))
                        return new System.Net.NetworkCredential(txtAuthUsername.Text, txtAuthPassword.Text, txtAuthDomain.Text);
                    else
                        return new System.Net.NetworkCredential(txtAuthUsername.Text, txtAuthPassword.Text);            
                }
                else
                {
                    return null;
                }
            }
        }

        public void SetNTLMAuth(string Username, string Password, string Domain)
        {
            txtAuthUsername.Text = Username;
            txtAuthPassword.Text = Password;
            txtAuthDomain.Text = Domain;
            cboAuthType.SelectedIndex = cboAuthType.FindStringExact("NTLM");
            chkUseAuth.Checked = true;
            EnableAuthFields();
        }

        private void EnableAuthFields()
        {
            lblAuthType.Enabled = true;
            cboAuthType.Enabled = true;

            if (cboAuthType.Text.Length > 0)
            {                
                lblAuthPassword.Enabled = true;
                lblAuthUsername.Enabled = true;                
                txtAuthPassword.Enabled = true;
                txtAuthUsername.Enabled = true;

                if (cboAuthType.Text.Equals("NTLM"))
                {
                    txtAuthDomain.Enabled = true;
                    lblAuthDomain.Enabled = true;
                }
                else
                {
                    txtAuthDomain.Enabled = false;
                    lblAuthDomain.Enabled = false;
                }
            }
            else
            {
                lblAuthDomain.Enabled = false;
                lblAuthPassword.Enabled = false;
                lblAuthUsername.Enabled = false;
                txtAuthDomain.Enabled = false;
                txtAuthPassword.Enabled = false;
                txtAuthUsername.Enabled = false;
            }
        }

        private void cboAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableAuthFields();
        }
    }
}
