using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Absinthe.Gui
{
    public partial class ProxyDialog : Form
    {
        public ProxyDialog()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetCallingAssembly();
            optCurrentAppProxies.Text = ((AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(asm, typeof (AssemblyProductAttribute))).Product + " Proxies";
          
        }

        private void ProxyDialog_Load(object sender, EventArgs e)
        {

        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            if (VerifyValidProxy(false))
            {
                lstProxies.Items.Add(new ListViewItem(new string[] {txtProxyHost.Text, txtProxyPort.Text}));
                
                txtProxyHost.Text = "";
                txtProxyPort.Text = "";

                butRemove.Enabled = true;
            }
        }

        /// <summary>
        /// We don't care if the machine is actually a proxy,
        /// only if it isn't already in one of the two lists.
        /// </summary>
        /// <param name="asmProxy">True if adding to a sub-application's proxy list.</param>        
        /// <returns>True if valid, false if not.</returns>
        bool VerifyValidProxy(bool asmProxy)
        {
            // TODO: Verify valid hostname
            if (txtProxyHost.Text.Trim().Length == 0)
            {
                MessageBox.Show("A hostname or IP address is required!");
                return false;
            }

            // TODO: Verify valid portname
            if (txtProxyPort.Text.Trim().Length == 0)
            {
                MessageBox.Show("A proxy port is required!");
                return false;
            }

            // We still need two separate loops to go through each proxy list control.
            // We could do it in one loop and catch any exceptions thrown by going past the
            // end of one list, but I *think* this will be a faster way of doing it.
            // Throwing a bunch of exceptions until we get to the end of the longer list
            // can't be a faster way to do it.
            for (int cnt = 0; cnt < lstProxies.Items.Count; cnt++)
            {                
                ListViewItem li = (ListViewItem) lstProxies.Items[cnt];

                if ((li.Text[0].Equals(txtProxyHost.Text)))
                {
                    MessageBox.Show("A cabinet proxy with that hostname already exists.");
                    return false;
                }
            }


            //TODO: this whole section needs to be fixed when I'm sober
            // We need this asmProxy check because we can't loop through the list control if
            // it has not been created yet.
            //if (!ParentAsmTitle.Equals(CurrentAsmTitle))
            //{
            //    for (int cnt = 0; cnt < lstAsmProxies.ItemCount; cnt++)
            //    {
            //        ListItem li = new ListItem();
            //        li.Id = cnt;
            //        li.Column = 0;
            //        li.Mask = ListCtrl.wxLIST_MASK_TEXT;
            //        lstAsmProxies.GetItem(ref li);
            //        if ((asmProxy && li.Text.Equals(txtAsmHostname.Value)) || (!asmProxy && li.Text.Equals(txtHostname.Value)))
            //        {
            //            MessageBox.Show("A custom proxy with that hostname already exists.");
            //            return false;
            //        }
            //    }
            //}

            // Parse the port
            try
            {
                Int32.Parse(txtProxyPort.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Invalid proxy port.", "Error!");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!");
                return false;
            }

            return true;
        }
        // }}}

        private void butOk_Click(object sender, EventArgs e)
        {

        }
    }
}
