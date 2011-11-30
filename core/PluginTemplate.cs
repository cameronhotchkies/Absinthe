//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   All rights reserved.
//   $Id: PluginTemplate.cs,v 1.7 2005/08/29 17:16:59 nummish Exp $
//
//   Redistribution and use in source and binary forms, with or without 
//   modification, are permitted provided that the following conditions are met:
//
//   * Redistributions of source code must retain the above copyright notice, 
//     this list of conditions and the following disclaimer.
//   * Redistributions in binary form must reproduce the above copyright notice, 
//     this list of conditions and the following disclaimer in the documentation 
//     and/or other materials provided with the distribution.
//   * Neither the name of the 0x90.org nor the names of its contributors 
//     may be used to endorse or promote products derived from this software 
//     without specific prior written permission.
//
//   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
//   AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
//   IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
//   ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
//   LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
//   CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
//   GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
//   HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
//   LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
//   OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;

namespace Absinthe.Core
{
	
	// {{{ IPlugin Interface
	/// <summary>
	/// The base interface for all sql injection plugins
	/// </summary>
	public interface IPlugin
	{
		/// <summary>
		/// The name of the plugin to be displayed in the application
		/// </summary>
		string PluginDisplayTargetName{ get; }

		/// <summary>
		/// The name of the author of the plugin
		/// </summary>
		string AuthorName{ get; }
	}
	// }}}

	// {{{ IErrorPlugin Interface
	/// <summary>
	/// The interface for all Error Based SQL injections
	/// </summary>
	public interface IErrorPlugin : IPlugin
	{
		/// <summary>The initial part of the error string generated on a HAVING error</summary>
		string HavingErrorPre { get; }
		/// <summary>The final part of the error string generated on a HAVING error</summary>
		string HavingErrorPost { get; }
		/// <summary>The final part of the error string generated on a HAVING error with a GROUP BY</summary>
		string HavingErrorPostWithGroupBy { get; }
		
		/// <summary>The initial part of the error string generated on a UNION SUM error</summary>
		string UnionSumErrorPre { get; }
		/// <summary>The initial part of the error string generated on a UNION SUM error when an integer is found</summary>
		string UnionSumErrorOnIntPre { get; }
		/// <summary>The initial part of the error string generated on a UNION SUM error when text is found</summary>
		string UnionSumErrorOnText { get; }
		/// <summary>The final part of the error string generated on a UNION SUM error</summary>
		string UnionSumErrorPost { get; }
		
		/// <summary>The initial part of the error string generated on a UNION SELECT error with an NVarChar</summary>
		string UnionSelectErrorPreNvarchar { get; }
		/// <summary>The initial part of the error string generated on a UNION SELECT error with a VarChar</summary>
		string UnionSelectErrorPreVarchar { get; }
		/// <summary>The final part of the error string generated on a UNION SELECT error with an Integer</summary>
		string UnionSelectErrorPostInt { get; }
		/// <summary>The final part of the error string generated on a UNION SELECT error with an Float</summary>
		string UnionSelectErrorPostFloat { get; }
		/// <summary>The final part of the error string generated on a UNION SELECT error with an Smallint</summary>
		string UnionSelectErrorPostSmallint { get; }
		/// <summary>The full error string generated on a UNION SELECT error with an Integer</summary>
		string UnionSelectErrorStandaloneInt { get; }
		/// <summary>The full error string generated on a UNION SELECT error with a Float</summary>
		string UnionSelectErrorStandaloneFloat { get; }
		/// <summary>The full error string generated on a UNION SELECT error with a Numeric</summary>
		string UnionSelectErrorStandaloneNumeric { get; }
		/// <summary>The full error string generated on a UNION SELECT error with a DateTime</summary>
		string UnionSelectErrorStandaloneDateTime { get; }
		/// <summary>The full error string generated on a UNION SELECT error with a Money</summary>
		string UnionSelectErrorStandaloneMoney { get; }
//		/// <summary>The full error string generated on a UNION SELECT error with an NText</summary>
//		string UnionSelectErrorNTextOperandClash { get; }
		
		/// <summary>
		/// The list of version strings this plugin has been tested against
		/// </summary>
		string[] KnownSupportedVersions { get; }
	}
	// }}}

	// {{{ IBlindPlugin Interface
	/// <summary>
	/// The interface for all Blind SQL injection plugins
	/// </summary>
	public interface IBlindPlugin : IPlugin
	{
		/// <summary>Modifies text by wrapping it in an "AND xxx ="</summary>
		/// <param name="Value">The text to be wrapped</param>
		/// <returns>The given text wrapped by valid SQL</returns>
		string AndEqualWrapper(string Value);

		/// <summary>Modifies text by wrapping it in an "AND xxx >"</summary>
		/// <param name="Value">The text to be wrapped</param>
		/// <returns>The given text wrapped by valid SQL</returns>
		string AndGreaterThanWrapper(string Value);
		
		/// <summary>Modifies text by wrapping it in an "AND xxx >="</summary>
		/// <param name="Value">The text to be wrapped</param>
		/// <returns>The given text wrapped by valid SQL</returns>
		string AndGreaterThanEqualWrapper(string Value);

		/// <summary>Modifies text by wrapping it in an "AND xxx IS NULL"</summary>
		/// <param name="Value">The text to be wrapped</param>
		/// <returns>The given text wrapped by valid SQL</returns>
		string AndIsNullWrapper(string Value);

		/// <summary>The SQL required to get the next table ID higher than the given table id</summary>
		/// <param name="PrevTableID">The last table id, set to 0 for the first table</param>
		/// <returns>The required SQL code</returns>
		string NextLowestTableID(long PrevTableID);
		
		/// <summary>The SQL required to get the next field ID higher than the given field id</summary>
		/// <param name="TableID">The table id this field is in</param>
		/// <param name="PrevFieldID">The field if of the previous field. Set to 0 for the first field.</param>
		/// <returns>The required SQL code</returns>
		string NextLowestFieldID(long TableID, long PrevFieldID);

		/// <summary>The SQL required to get the length of a given table's name</summary>
		/// <param name="TableID">The table id this name is for</param>
		/// <returns>The required SQL code</returns>
		string TableNameLength(long TableID);
		
		/// <summary>The SQL required to get the value of an index to given table's name</summary>
		/// <param name="Index">The index of the character being retrieved</param>
		/// <param name="TableID">The table id this name is for</param>
		/// <returns>The required SQL code</returns>
		string TableNameCharacterValue(long Index, long TableID);

		/// <summary>The SQL required to get the number of records in a table</summary>
		/// <param name="TableName">The name of the table this is for</param>
		/// <returns>The desired SQL code</returns>
		string NumberOfRecords(string TableName);

		/// <summary>The SQL required to get the column number of the primary key in a table</summary>
		/// <param name="TableID">The ID of the table this is for</param>
		/// <returns>The desired SQL code</returns>		
		string PrimaryKeyColumn(long TableID);

		/// <summary>The SQL required to get the data type of a field</summary>
		/// <param name="FieldID">The ID of the field in question</param>
		/// <param name="TableID">The ID of the table in question</param>
		/// <returns>The desired SQL code</returns>		
		string FieldDataType(long FieldID, long TableID);

		/// <summary>The SQL required to get the length of the name of a field</summary>
		/// <param name="FieldID">The ID of the field in question</param>
		/// <param name="TableID">The ID of the table in question</param>
		/// <returns>The desired SQL code</returns>		
		string FieldNameLength(long FieldID, long TableID);

		/// <summary>The SQL required to get a single character of a name of a field</summary>
		/// <param name="Index">The position of the character in the name</param>
		/// <param name="FieldID">The ID of the field in question</param>
		/// <param name="TableID">The ID of the table in question</param>
		/// <returns>The desired SQL code</returns>		
		string FieldNameCharacterValue(long Index, long FieldID, long TableID);

		/// <summary>Select the number of tables in the database</summary>
		/// <returns>The desired SQL code</returns>
		string NumberOfTables();

		/// <summary>Select the number of fields in a given table in the database</summary>
		/// <param name="TableID">The Table ID to retrieve information on</param>
		/// <returns>The desired SQL code</returns>		
		string NumberOfFieldsInTable(long TableID);

		/// <summary>Select the length of a primary key value converted to text</summary>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>
		/// <returns>The desired SQL code</returns>
		string LengthOfConvertedPrimaryKeyValue(string KeyName, string TableName);

		/// <summary>Select the length of a primary key value converted to text</summary>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="PrevKeyValue">The value of the last primary key checked</param>
		/// <returns>The desired SQL code</returns>
		string LengthOfConvertedPrimaryKeyValue(string KeyName, string TableName, string PrevKeyValue);

		/// <summary>Select a single character of a primary key value converted to text</summary>
		/// <param name="Index">The position of the character</param>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>		
		/// <returns>The desired SQL code</returns>
		string ConvertedPrimaryKeyValueCharacter(long Index, string KeyName, string TableName);

		/// <summary>Select a single character of a primary key value converted to text</summary>
		/// <param name="Index">The position of the character</param>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>		
		/// <param name="PrevKeyValue">The value of the last primary key checked</param>
		/// <returns>The desired SQL code</returns>
		string ConvertedPrimaryKeyValueCharacter(long Index, string KeyName, string TableName, string PrevKeyValue);

		/// <summary>Select the value of a numeric primary key</summary>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>
		/// <returns>The desired SQL code</returns>
		string IntegerPrimaryKeyValue(string KeyName, string TableName);

		/// <summary>Select the value of a numeric primary key</summary>
		/// <param name="KeyName">The field name of the key</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="PrevKeyValue">The value of the last primary key checked</param>
		/// <returns>The desired SQL code</returns>
		string IntegerPrimaryKeyValue(string KeyName, string TableName, string PrevKeyValue);
  
		/// <summary>The SQL to select a value of a field for a given Primary Key</summary>
		/// <param name="FieldName">The name of the field to select for</param>
		/// <param name="TableName">The table to select from</param>
		/// <param name="pk">The primary key of the target</param>
		/// <returns>The required SQL code</returns>
		string SelectValueForGivenPrimaryKey( string FieldName, string TableName, GlobalDS.PrimaryKey pk);
		
		/// <summary>Select the length of a value for a given primary key</summary>
		/// <param name="FieldName">The name of the field</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="pk">The primary key value associated with this record</param>
		/// <returns>The desired SQL code</returns>
		string SelectLengthOfValueForGivenPrimaryKey(string FieldName, string TableName, GlobalDS.PrimaryKey pk);
		
		/// <summary>Select the value of a character of a field value for a given primary key</summary>
		/// <param name="Index">The position of the character in the value</param>
		/// <param name="FieldName">The name of the field</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="pk">The primary key value associated with this record</param>
		/// <returns>The desired SQL code</returns>
		string SelectCharacterValueForGivenPrimaryKey(long Index, string FieldName, string TableName, GlobalDS.PrimaryKey pk);

		/// <summary>Select the length of a value converted to text for a given primary key</summary>
		/// <param name="FieldName">The name of the field</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="pk">The primary key value associated with this record</param>
		/// <returns>The desired SQL code</returns>
		string SelectLengthOfConvertedRecordValue(string FieldName, string TableName, GlobalDS.PrimaryKey pk);

		/// <summary>Select the value of a character of a field value converted to text for a given primary key</summary>
		/// <param name="Index">The position of the character in the value</param>
		/// <param name="FieldName">The name of the field</param>
		/// <param name="TableName">The name of the table</param>
		/// <param name="pk">The primary key value associated with this record</param>
		/// <returns>The desired SQL code</returns>
		string SelectCharacterValueForConvertedRecordValue(long Index, string FieldName, string TableName, GlobalDS.PrimaryKey pk);

		/// <summary>The concatenation character for this database</summary>
		string ConcatenationCharacter
		{
			get;
		}

		/// <summary>
		/// The database function to convert a decimal to a character
		/// </summary>
		/// <param name="DecimalValue">The ascii value of the character</param>
		/// <returns>The desired SQL code</returns>
		string CharConversionFunction(long DecimalValue);

		/// <summary>
		/// Converts the native data type to the .NET recognizable SQL data types
		/// </summary>
		/// <param name="DataType">The database native data type</param>
		/// <returns>The .NET recognizable version of the data type</returns>
		System.Data.SqlDbType ConvertNativeDataType(long DataType);

		/// <summary>
		/// Select the length  of the username of the database connection.
		/// </summary>
		/// <returns>The desired SQL code</returns>
		string SelectDatabaseUsernameLength();

		/// <summary>
		/// Selects a character of the database username
		/// </summary>
		/// <param name="Index">The position of the character in the username to select</param>
		/// <returns>The desired SQL code</returns>
		string SelectCharacterFromDatabaseUsername(long Index);
	}
	// }}}

	// {{{ UnsupportedPluginException
	/// <summary>
	/// An exception thrown when an unsupported plugin is loaded
	/// </summary>
	public class UnsupportedPluginException : Exception
	{
		private string _Message;

		/// <summary>
		/// Instantiates a new UnsupportedPluginException
		/// </summary>
		/// <param name="Message">The message to display to the user</param>
		public UnsupportedPluginException(string Message)
		{
			_Message = Message;
		}

		/// <summary>
		/// The message for the user about this exception
		/// </summary>
		public override string Message
		{
			get
			{
				return _Message;
			}
		}
	}
	// }}}
}
