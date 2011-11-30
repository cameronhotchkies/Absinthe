//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004-2007  nummish, 0x90.org
//   $Id: AttackVector.cs,v 1.15 2005/12/26 10:01:44 nummish Exp $
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
using System.Data;
using System.Net;
using System.Xml;
using System.Collections;

namespace Absinthe.Core
{
	/// <summary>Definition of the TableChanged event handler delegate</summary>
	public delegate void TableChangedEventHandler(GlobalDS.Table ModifiedTable);
	 
	///<summary>The general interface denoting the style of injection taking place</summary>
	public interface AttackVector
	{
		/// <summary>The type of exploit that the attack vector represents.</summary>
		GlobalDS.ExploitType ExploitType 
		{
			get;
		}
		
		/// <summary>Any proxies specific to the attack vector</summary>
		Queue Proxies 
		{
			set;
		}

		/// <summary>Event triggered when a table's data has been changed</summary>
		event TableChangedEventHandler TableChanged;

        /// <summary>
        /// The event used to bubble status messages to the user
        /// </summary>
		event UserEvents.UserStatusEventHandler UserStatus;

		/// <summary>Create an xml representation of the AttackVector instance.</summary>
		/// <param name="xOutput">An XmlTextWriter instance that is already created for exporting
		/// the AttackVector information to.</param>
		void ToXml(ref XmlTextWriter xOutput);

		/// <summary>Retrieve the the username the database connection is running as.</summary>
		/// <returns>The current database username.</returns>
		string GetDatabaseUsername();

		/// <summary>Retrieve the tables from the database.</summary>
		/// <returns>An array of Tables including the name, id and record count.</returns>
		GlobalDS.Table[] GetTableList();

		/// <summary>Takes a partially built schema and continues to download what is left</summary>
		/// <param name="RecoveredList">The preexisting schema</param>
		/// <returns>The fully downloaded schema</returns>
		GlobalDS.Table[] RecoverTableList(GlobalDS.Table[] RecoveredList);

		/// <summary>Retrieve the columns of a given table and load it into the given Table information.</summary>
		/// <param name="TableData">The table to retrieve the field information for.</param>
		void PopulateTableStructure(ref Absinthe.Core.GlobalDS.Table TableData);

		/// <summary>Download records from the database.</summary>
		/// <param name="SrcTable">An array of tables from which to retrieve data.</param>
		/// <param name="ColumnIDLists">All the fields for the corresponding tables for which data is desired.</param>
		/// <param name="xmlFilename">The filename to save the retrieved data to.</param>
		void PullDataFromTable(GlobalDS.Table[] SrcTable, long[][] ColumnIDLists, string xmlFilename);

		//void Initialize();
	}
}
