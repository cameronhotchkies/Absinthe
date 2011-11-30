using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class ErrorReportDialog : Form
    {
        string _PluginUsed;
        System.Net.WebProxy _ProxyToUse;

        public ErrorReportDialog(string PluginUsed, string VersionString, string HavingError, System.Net.WebProxy ProxyToUse)
        {
            _PluginUsed = PluginUsed;
            txtVersionData.Text = VersionString;
            txtHavingData.Text = HavingError;
            _ProxyToUse = ProxyToUse;

            InitializeComponent();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            NameValueCollection Data = new NameValueCollection();
            Assembly asm = Assembly.GetExecutingAssembly();

            Data.Add("submittedby", txtCredit.Text);
            Data.Add("versiontag", asm.GetName().Version.ToString(4) + " " + _PluginUsed);
            Data.Add("initialversionresult", txtVersionData.Text);
            Data.Add("initialhavingresult", txtHavingData.Text);


            string x = httpConnect.PageRequest("http://www.0x90.org/releases/absinthe/signatures/", Data, _ProxyToUse, true, null, null, "Your Mother");
            this.Close();
        }
    }
}