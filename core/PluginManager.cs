//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: PluginManager.cs,v 1.6 2005/12/26 07:56:18 nummish Exp $
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
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Absinthe.Core
{
	/// <summary>
	/// The class that manages all the SQL Injection plugins
	/// </summary>
	public class PluginManager
	{	
		/// <summary>
		/// Instantiates a new PluginManager
		/// </summary>
		public PluginManager()
		{
			LoadAllPlugins();
		}

		/// <summary>A list of all the Plugins</summary>
		public List<IPlugin> PluginList
		{
			get
			{
				return _Plugins;
			}
		} private List<IPlugin> _Plugins = new List<IPlugin>();

		
		/// <summary>
		/// Retrieves the plugin by its display name
		/// </summary>
		/// <param name="PluginName">The display name used by the plugin</param>
		/// <returns>An instance of the appropriate injection plugin</returns>
		public IPlugin GetPluginByName(string PluginName)
		{
			foreach (IPlugin pt in _Plugins)
			{
				if (pt.PluginDisplayTargetName.Equals(PluginName)) return pt;
			}

			throw new UnsupportedPluginException("No plugin matching \""+ PluginName +"\" was found.");
		}

		private void LoadAllPlugins()
		{
			Assembly asm = Assembly.GetAssembly(this.GetType());

			string PluginPath = asm.Location.Substring(0, 1+asm.Location.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
			PluginPath += "plugins" + System.IO.Path.DirectorySeparatorChar;

			if (!System.IO.Directory.Exists(PluginPath)) 
			{
				Console.WriteLine(PluginPath + " not found.");
			}

            System.IO.FileInfo[] f411;

            try
            {
                System.IO.DirectoryInfo d411 = new System.IO.DirectoryInfo(PluginPath);
                f411 = d411.GetFiles("*.dll");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                // bubble message to parent
                System.Diagnostics.Debug.Write("Plugin folder not found.. attempting to use Devel paths...");
                PluginPath = asm.Location.Substring(0, 1 + asm.Location.LastIndexOf(System.IO.Path.DirectorySeparatorChar));
                PluginPath += ".." + System.IO.Path.DirectorySeparatorChar + ".." + System.IO.Path.DirectorySeparatorChar;
                PluginPath += ".." + System.IO.Path.DirectorySeparatorChar;
                PluginPath += "plugins" + System.IO.Path.DirectorySeparatorChar + "Bin" + System.IO.Path.DirectorySeparatorChar;
                PluginPath += "Debug" + System.IO.Path.DirectorySeparatorChar;
                System.IO.DirectoryInfo d411 = new System.IO.DirectoryInfo(PluginPath);
                f411 = d411.GetFiles("Absinthe.Plugins.dll");
            }

			foreach (System.IO.FileInfo fi in f411)
			{
				Assembly a = Assembly.LoadFile(PluginPath + fi.Name);
				Type[] AllClasses = a.GetTypes();

				foreach (Type t in AllClasses)
				{
					Type iface = t.GetInterface("IPlugin");
					
					if (iface != null)
					{
						IPlugin pt = (IPlugin) Activator.CreateInstance(t);
						_Plugins.Add(pt);
					}
				}
			}
		}
	}
}
