
//   Liquor Cabinet - A WebApp Penetration Testing Suite
//   This software is Copyright (C) 2005-2007 nummish, 0x90.org
//   $Id: LocalSettings.cs,v 1.17 2006/08/14 23:01:16 nummish Exp $
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
using System.IO;
using System.Xml;
using System.Collections;
using System.Net;
using System.Reflection;

namespace Absinthe.Gui
{
	public class LocalSettings
	{
		private string _CabinetAsmTitle;
		private string _CabinetSettingsFile;
		private string _CabinetSettingsFullPath;

		private string _ParentAsmTitle;
		private string _ParentSettingsFile;
		private string _ParentSettingsFullPath;

		private Hashtable _CabinetProxyTable = new Hashtable();
		private Hashtable _ParentProxyTable = new Hashtable();
		
		private Queue ProxyQ = new Queue();

		// {{{ Constructor
		public LocalSettings(Assembly ParentAssembly)
		{
			// Get both assembly titles - we use this to find out if we
			// are being instantiated from a sub-application
			_CabinetAsmTitle = ((AssemblyTitleAttribute) AssemblyTitleAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof (AssemblyTitleAttribute))).Title;
			_ParentAsmTitle = ((AssemblyTitleAttribute) AssemblyTitleAttribute.GetCustomAttribute(ParentAssembly, typeof (AssemblyTitleAttribute))).Title;

			// Always load the cabinet config file
			_CabinetSettingsFile = "/" + _CabinetAsmTitle + "-config.xml";
			_CabinetSettingsFullPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + _CabinetSettingsFile;
			LoadSettings(_CabinetSettingsFullPath);

			// Load the sub-application config file if we are being called
			// from a sub-application
			if (!_CabinetAsmTitle.Equals(_ParentAsmTitle))
			{
				_ParentSettingsFile = "/" + _ParentAsmTitle + "-config.xml";
				_ParentSettingsFullPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + _ParentSettingsFile;
				LoadSettings(_ParentSettingsFullPath);
			}

			BuildProxyQueue();
		}
		// }}}
	
		// {{{ RebuildProxyTable()
		/// <summary>
		/// 
		/// </summary>
		/// <remarks>This is thread safe.</remarks>
		public void RebuildProxyTables()
		{
			lock (ProxyQ)
			{
				ClearProxies();
				LoadSettings(_ParentSettingsFullPath);
				LoadSettings(_CabinetSettingsFullPath);
				BuildProxyQueue();
			}
		}

		// }}}

		// {{{ LoadSettings
		private void LoadSettings(string WhichFile)
		{
			if (!System.IO.File.Exists(WhichFile))
				return;		
		
			FileStream InputStream = null;
			
			XmlDocument xInput = new XmlDocument();
			try
			{
				InputStream = File.OpenRead(WhichFile);
				xInput.Load(new XmlTextReader(InputStream));
				XmlNode docNode = xInput.DocumentElement;

				foreach (XmlNode n in docNode.ChildNodes)
				{
					if (n.Name.Equals("proxy"))
						ReadProxyXml(n, WhichFile);
					else if (n.Name.Equals("proxies") && Boolean.Parse(n.Attributes["active"].InnerText))
					{
						foreach (XmlNode nd in n.ChildNodes)
							if (nd.Name.Equals("proxy"))
								ReadProxyXml(nd, WhichFile);
					}
					else if (n.Name.Equals("useragent"))
					{
						_UserAgent = n.Attributes["value"].InnerText;
					}
				}
			}
			catch (System.Exception)
			{
				// Ignore any exceptions here, the file will be overwritten if necessary
			}
			finally
			{
				InputStream.Close();
			}
		}
		// }}}

		// {{{ ReadProxyXml
		private void ReadProxyXml(XmlNode n, string WhichFile)
		{
			string ProxyAddress;
			int ProxyPort;
			
			if ((n.Attributes["address"] != null) && (n.Attributes["port"] != null))
			{
				ProxyAddress = n.Attributes["address"].InnerText;
				ProxyPort = Int32.Parse(n.Attributes["port"].InnerText);
				_ProxyInUse = true;

				// Store the proxy in the correct table.
				if (WhichFile.Equals(_ParentSettingsFullPath))
					_ParentProxyTable.Add(ProxyAddress, ProxyPort);
				else
					_CabinetProxyTable.Add(ProxyAddress, ProxyPort);
			}
		}
		// }}}

		// {{{ ProxyQueue
		public Queue ProxyQueue()
		{
			if (ProxyQ == null)
				ProxyQ = new Queue();

			return ProxyQ;
		}
		// }}}

		// {{{ BuildProxyQueue
		public void BuildProxyQueue()
		{
			ProxyQ.Clear();

			if (_CabinetProxyTable == null)
				_CabinetProxyTable = new Hashtable();
			else if ((!_CabinetAsmTitle.Equals(_ParentAsmTitle)) && _ParentProxyTable == null)
				_ParentProxyTable = new Hashtable();

			foreach (string HostKey in _CabinetProxyTable.Keys)
			{
				WebProxy wp = new WebProxy(HostKey, (int) _CabinetProxyTable[HostKey]);
				ProxyQ.Enqueue(wp);
			}

			// Only add the sub-application proxies if they exist
			if (!_CabinetAsmTitle.Equals(_ParentAsmTitle))
			{
				foreach (string HostKey in _ParentProxyTable.Keys)
				{
					WebProxy wp = new WebProxy(HostKey, (int) _ParentProxyTable[HostKey]);
					ProxyQ.Enqueue(wp);
				}
			}

			return;
		}
		// }}}
         
        /// <summary>The user agent to be displayed during requests to the target server</summary>
		public string CustomUserAgent
		{
			get
			{
				return _UserAgent;
			}
			set
			{
				_UserAgent = value;
			}
        } private string _UserAgent;
		 
        /// <summary>Indicates if the web proxies should be turned on.</summary>
		public bool ProxyInUse
		{
			get
			{
				return _ProxyInUse;
			}
			set
			{
				_ProxyInUse = value;
			}
        } private bool _ProxyInUse = false;
		 
		/// <summary>
        /// 
        /// </summary>
        /// <param name="ProxyAddress"></param>
        /// <param name="ProxyPort"></param>
        /// <param name="SubAppTable">true when adding to the sub-application proxy table</param>
		public void AddProxyToTable(string ProxyAddress, int ProxyPort, bool SubAppTable)
		{
			if (_CabinetProxyTable == null)
				_CabinetProxyTable = new Hashtable();
			else if ((!_CabinetAsmTitle.Equals(_ParentAsmTitle)) && _ParentProxyTable == null)
				_ParentProxyTable = new Hashtable();

			if (SubAppTable)
				_ParentProxyTable.Add(ProxyAddress, ProxyPort);
			else
				_CabinetProxyTable.Add(ProxyAddress, ProxyPort);

			_ProxyInUse = true;
		}
	
		// {{{ GetProxyTable
		// If WhichTable is true we return the sub-application proxy table.
		public Hashtable GetProxyTable(bool WhichTable)
		{
			if (WhichTable)
				return _ParentProxyTable;
			else
				return _CabinetProxyTable;				
		}
		// }}}

		// {{{ ClearProxies
		public void ClearProxies()
		{
			if (_CabinetProxyTable != null)
				_CabinetProxyTable.Clear();

			if (_ParentProxyTable != null)
				_ParentProxyTable.Clear();
		}
		// }}}

		// {{{ RotatedProxy
		/// <summary>
		/// Extract proxy and reinsert it
		/// </summary>
		/// <remarks>This is thread safe.</remarks>
		/// <returns>A proxy from the round robin list</returns>
		public WebProxy RotatedProxy()
		{
			WebProxy retVal = null;
			
			if (ProxyQ != null)
			{ 
				lock (ProxyQ)
				{
					retVal = (WebProxy) ProxyQ.Dequeue(); 
					ProxyQ.Enqueue(retVal); 
				}
			}
			return retVal;
		}
		// }}}

		// {{{ GeneratePath
		private void GeneratePath()
		{
			if (!System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)))
			{
				System.IO.Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));
			}
		}
		// }}}

		// {{{ SaveSettings
		public void SaveSettings(bool CalledFromSubApp)
		{
			Hashtable tmpTable;
			string WhichFile;
			string tmpProxyInUse = "True";

			if (CalledFromSubApp)
			{
				WhichFile = _ParentSettingsFullPath;
				tmpTable = GetProxyTable(true);
			}
			else
			{
				WhichFile = _CabinetSettingsFullPath;
				tmpTable = GetProxyTable(false);
			}

			if (tmpTable.Count.Equals(0))
				tmpProxyInUse = "False";

			GeneratePath();

			XmlTextWriter xOutput = new XmlTextWriter(WhichFile, System.Text.Encoding.UTF8);
			xOutput.Formatting = Formatting.Indented;
			xOutput.Indentation = 4;
			xOutput.WriteStartDocument();

			xOutput.WriteStartElement("settings");

			if (_UserAgent != null && _UserAgent.Length > 0)
			{
				xOutput.WriteStartElement("useragent");
				xOutput.WriteStartAttribute("value", null);
				xOutput.WriteString(_UserAgent);
				xOutput.WriteEndAttribute();
				xOutput.WriteEndElement();
			}

			xOutput.WriteStartElement("proxies");
			xOutput.WriteStartAttribute("active", null);
			xOutput.WriteString(tmpProxyInUse.ToString());
			xOutput.WriteEndAttribute();

			if (tmpTable != null && tmpTable.Count > 0)
			{
				foreach (string HostKey in tmpTable.Keys)
				{
					string ProxyAddress = HostKey;
					int ProxyPort = (int) tmpTable[HostKey];

					xOutput.WriteStartElement("proxy");

					xOutput.WriteStartAttribute("address", null);
					xOutput.WriteString(ProxyAddress);
					xOutput.WriteEndAttribute();

					xOutput.WriteStartAttribute("port", null);
					xOutput.WriteString(ProxyPort.ToString());
					xOutput.WriteEndAttribute();

					xOutput.WriteEndElement();
				}
			}
			xOutput.WriteEndElement();

			xOutput.WriteEndElement();
			xOutput.WriteEndDocument();
			xOutput.Close();

			if (_CabinetProxyTable.Count.Equals(0) && _ParentProxyTable.Count.Equals(0))
				_ProxyInUse = false;
		}
		// }}}
	}
}
