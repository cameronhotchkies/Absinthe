using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Absinthe.Gui
{
    public partial class ctlAboutContents : UserControl
    {
        public ctlAboutContents()
        {
            InitializeComponent();
        }

        public string[] Authors
        {
            set
            {
                _Authors = value;
            }
        } string[] _Authors;

        public Assembly CallingAssembly
        {
            set
            {
                _CallingAssembly = value;
            }
        } private Assembly _CallingAssembly; 

        private void ctlAboutContents_Load(object sender, EventArgs e)
        {
            if (_CallingAssembly == null) _CallingAssembly = Assembly.GetCallingAssembly();
            SetupControl();
        }

        private void SetupControl()
        {
            Assembly asm = _CallingAssembly;
            string AppTitle = ((AssemblyProductAttribute)AssemblyProductAttribute.GetCustomAttribute(asm, typeof (AssemblyProductAttribute))).Product;
           
            AppTitle += " v" + asm.GetName().Version.ToString(2);
            
            lblAppName.Text = AppTitle;
            
            lblAppTitle.Text = ((AssemblyDescriptionAttribute)AssemblyDescriptionAttribute.GetCustomAttribute(asm, typeof (AssemblyDescriptionAttribute))).Description;
           
            lblCopyright.Text = ((AssemblyCopyrightAttribute)AssemblyCopyrightAttribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute))).Copyright;

            StringBuilder AuthorBuilder = new StringBuilder();

            if (_Authors == null)
            {
                AuthorBuilder.Append("Authors: Anonymous");
            }
            else
            {
                if (_Authors.Length == 1)
                    AuthorBuilder.Append("Author: ");
                else
                    AuthorBuilder.Append("Authors: ");

                bool First = true;
                foreach (string Auth in _Authors)
                {
                    if (!First) AuthorBuilder.Append(", ");
                    AuthorBuilder.Append(Auth);
                    First = false;
                }
            }

            lblAuthors.Text = AuthorBuilder.ToString();

            //Visual Studio requires the namespace to be applied to the resource name, otherwise it can't find it.
            Stream LicenseStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LICENSE");
            if (LicenseStream == null) LicenseStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Absinthe.Gui.LICENSE");
            StreamReader str = new StreamReader(LicenseStream);
            txtLicense.Text = str.ReadToEnd();
            txtLicense.Select(0, 0);
        }
    }
}
