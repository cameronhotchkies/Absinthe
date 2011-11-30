using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Absinthe.Core;

namespace Absinthe.Gui
{
    public partial class frmAbout : Form
    {
        public frmAbout(IPlugin[] PluginList)
        {
            InitializeComponent();
            ctlAboutContents1.Authors = new string[] { "nummish" };
            ctlAboutContents1.CallingAssembly = Assembly.GetExecutingAssembly();

            txtPluginList.Text = PluginInfo(PluginList);
        }

        private string PluginInfo(IPlugin[] PluginList)
        {
            StringBuilder retVal = new StringBuilder();

            foreach (IPlugin pt in PluginList)
            {
                retVal.Append(pt.PluginDisplayTargetName);
                retVal.Append(" - Author: ").Append(pt.AuthorName);
                retVal.Append(Environment.NewLine);
            }

            return retVal.ToString();
        }
    }
}