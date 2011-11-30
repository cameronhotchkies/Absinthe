using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class InjectionOptionsForm : Form
    {
        DataStore _Storage;

        public InjectionOptionsForm(ref DataStore Storage)
        {
            InitializeComponent();

            _Storage = Storage;
            LoadValuesFromStorage();
            string[] UAChoices = { "Absinthe", "Firefox", "Galeon", "GoogleBot", "Internet Explorer", "Konqueror", "Links", "Mozilla", "Netscape", "Opera", "Safari", "Other" };

            cboUserAgent.Items.AddRange(UAChoices);
        }

        private void LoadValuesFromStorage()
        {
            txtTolerance.Text = (_Storage.FilterTolerance * 100).ToString();
            if (_Storage.FilterDelimiter.Equals(Convert.ToChar(0xa).ToString()))
            {
                txtFilterDelimiter.Text = String.Empty;
            }
            else
            {
                txtFilterDelimiter.Text = _Storage.FilterDelimiter;
            }
            txtThrottle.Text = _Storage.ThrottleValue.ToString();
            chkSpeedup.Checked = (_Storage.ThrottleValue < 0);
            txtAppended.Text = _Storage.AppendedText;
            chkAppendText.Checked = txtAppended.Text.Length > 0;
            CheckSavedUA();
        }

        private void CheckSavedUA()
        {
            txtUserAgent.Text = _Storage.UserAgent;

            switch (_Storage.UserAgent)
            {
                case CommonUserAgents.Absinthe:
                    cboUserAgent.Text = "Absinthe";
                    break;
                case CommonUserAgents.Firefox:
                    cboUserAgent.Text = "Firefox";
                    break;
                case CommonUserAgents.Galeon:
                    cboUserAgent.Text = "Galeon";
                    break;
                case CommonUserAgents.GoogleBot:
                    cboUserAgent.Text = "GoogleBot";
                    break;
                case CommonUserAgents.InternetExplorer:
                    cboUserAgent.Text = "Internet Explorer";
                    break;
                case CommonUserAgents.Konqueror:
                    cboUserAgent.Text = "Konqueror";
                    break;
                case CommonUserAgents.Links:
                    cboUserAgent.Text = "Links";
                    break;
                case CommonUserAgents.Mozilla:
                    cboUserAgent.Text = "Mozilla";
                    break;
                case CommonUserAgents.Netscape:
                    cboUserAgent.Text = "Netscape";
                    break;
                case CommonUserAgents.Opera:
                    cboUserAgent.Text = "Opera";
                    break;
                case CommonUserAgents.Safari:
                    cboUserAgent.Text = "Safari";
                    break;
                default:
                    cboUserAgent.Text = "Other";
                    cboUserAgent_SelectedIndexChanged(null, null);
                    break;
            }
        }

        private void cboUserAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserAgent.Enabled = false;
            switch (cboUserAgent.Text)
            {
                case "Absinthe":
                    txtUserAgent.Text = CommonUserAgents.Absinthe;
                    break;
                case "Firefox":
                    txtUserAgent.Text = CommonUserAgents.Firefox;
                    break;
                case "Galeon":
                    txtUserAgent.Text = CommonUserAgents.Galeon;
                    break;
                case "GoogleBot":
                    txtUserAgent.Text = CommonUserAgents.GoogleBot;
                    break;
                case "Internet Explorer":
                    txtUserAgent.Text = CommonUserAgents.InternetExplorer;
                    break;
                case "Konqueror":
                    txtUserAgent.Text = CommonUserAgents.Konqueror;
                    break;
                case "Links":
                    txtUserAgent.Text = CommonUserAgents.Links;
                    break;
                case "Mozilla":
                    txtUserAgent.Text = CommonUserAgents.Mozilla;
                    break;
                case "Netscape":
                    txtUserAgent.Text = CommonUserAgents.Netscape;
                    break;
                case "Opera":
                    txtUserAgent.Text = CommonUserAgents.Opera;
                    break;
                case "Safari":
                    txtUserAgent.Text = CommonUserAgents.Safari;
                    break;
                case "Other":
                    txtUserAgent.Enabled = true;
                    break;
            }
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            float FilterTolerance = 0;
            int Throttle = 0;

            try
            {
                FilterTolerance = Convert.ToSingle(txtTolerance.Text) / 100;
                // A filter tolerance of over 100% or negative is meaningless
                if (FilterTolerance < 0 || FilterTolerance >= 1)
                    throw new InvalidCastException("Just catch and ignore");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please enter a valid number for the tolerance.");
                return; // Exit, and prevent saving.
            }

            try
            {
                Throttle = Convert.ToInt32(txtThrottle.Text);
                // A filter tolerance of over 100% or negative is meaningless
                if (Throttle < 0)
                    throw new InvalidCastException("Just catch and ignore");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Please enter a valid number for the tolerance.");
                return; // Exit, and prevent saving.
            }

            _Storage.UserAgent = txtUserAgent.Text;
            _Storage.FilterTolerance = FilterTolerance;
            _Storage.FilterDelimiter = txtFilterDelimiter.Text;
            
            // A negative value for throttle will initiate multithreaded requests
            _Storage.ThrottleValue = chkSpeedup.Checked ? -100 : Throttle;

            _Storage.AppendedText = chkAppendText.Checked && txtAppended.Text.Length > 0 ? txtAppended.Text : string.Empty;
            this.Close();
        }
 
        private void chkAppendText_CheckedChanged(object sender, EventArgs e)
        {
            txtAppended.Enabled = chkAppendText.Checked;
        }

        private void chkCommentQuery_CheckedChanged(object sender, EventArgs e)
        {
            chkAppendText.Checked = true;
            txtAppended.Text = txtAppended.Text + "--";
        }

    }
}