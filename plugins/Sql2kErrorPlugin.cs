
//   Absinthe Plugins - The Automated SQL Injection Library
//   This software is Copyright (C) 2005  nummish, 0x90.org
//   $Id: Sql2kErrorPlugin.cs,v 1.3 2005/08/17 17:40:00 nummish Exp $
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

using Absinthe.Core;

public class Sql2kErrorPlugin : IErrorPlugin
{
	public string PluginDisplayTargetName
	{
		get
		{
			return "SQL Server 2000";
		}
	}
	
	public string AuthorName
	{
		get
		{
			return "nummish";
		}
	}

	public string[] KnownSupportedVersions
	{
		get
		{
			return new string[]  
			{				
				"Microsoft SQL Server  2000 - 8.00.760 (Intel X86) \n\tDec 17 2002 14:22:05 \n\tCopyright (c) 1988-2003 Microsoft Corporation\n\tStandard Edition on Windows NT 5.2 (Build 3790: )",
				"Microsoft SQL Server  2000 - 8.00.760 (Intel X86) \n\tDec 17 2002 14:22:05 \n\tCopyright (c) 1988-2003 Microsoft Corporation\n\tStandard Edition on Windows NT 5.0 (Build 2195: Service Pack 4)",
				"Microsoft SQL Server  2000 - 8.00.2039 (Intel X86) \n\tMay  3 2005 23:18:38 \n\tCopyright (c) 1988-2003 Microsoft Corporation\n\tStandard Edition on Windows NT 5.0 (Build 2195: Service Pack 4)",
				"Microsoft SQL Server  2000 - 8.00.194 (Intel X86) \n\tAug  6 2000 00:57:48 \n\tCopyright (c) 1988-2000 Microsoft Corporation\n\tStandard Edition on Windows NT 5.0 (Build 2195: Service Pack 4)"
				
			};
		}
	}

	public string HavingErrorPre
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Column '";
		}
	}

	public string HavingErrorPost
	{
		get
		{
			return "' is invalid in the select list because it is not contained in an aggregate function and there is no GROUP BY clause.";
		}
	}

	public string HavingErrorPostWithGroupBy
	{
		get
		{
			return "' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause.";
		}
	}

	public string UnionSumErrorPre
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]The sum or average aggregate operation cannot take a ";
		}
	}

	public string UnionSumErrorOnIntPre
	{
		get
		{	
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]All queries in an SQL statement containing a UNION operator must have an equal number of expressions in their target lists.";
		}
	}

	public string UnionSumErrorOnText
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]The text, ntext, and image data types are invalid in this subquery or aggregate expression.";
		}
	}

	public string UnionSumErrorPost
	{
		get
		{
			return " data type as an argument.";
		}
	}

	/// <summary>
	/// The first half of an error when converting an nvarchar to a numeric data type.
	/// </summary>
	public string UnionSelectErrorPreNvarchar
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Syntax error converting the nvarchar value '";
		}
	}

	/// <summary>
	/// The first half of an error when converting a varchar to a numeric data type.
	/// </summary>
	public string UnionSelectErrorPreVarchar
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Syntax error converting the varchar value '";
		}
	}

	/// <summary>
	/// The second half of an error when converting a varchar to an integer.
	/// </summary>
	public string UnionSelectErrorPostInt
	{
		get
		{
			return "' to a column of data type int.";
		}
	}

	/// <summary>
	/// The second half of an error when converting a varchar to a float.
	/// </summary>
	public string UnionSelectErrorPostFloat
	{
		get
		{
			return "' to a column of data type float.";
		}
	}

	/// <summary>
	/// The full error string generated when attempting to convert a varchar to a float.
	/// </summary>
	public string UnionSelectErrorStandaloneFloat
	{
		get
		{
			//return "' to a column of data type float.";
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Error converting data type varchar to float.";
		}
	}

	/// <summary>
	/// The full error string generated when attempting to convert a varchar to an integer.
	/// </summary>
	public string UnionSelectErrorStandaloneInt
	{
		get
		{
			//return "' to a column of data type int.";
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Error converting data type varchar to int.";
		}
	}

	/// <summary>
	/// The full error string generated when attempting to convert a varchar to a numeric.
	/// </summary>
	public string UnionSelectErrorStandaloneNumeric
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Error converting data type varchar to numeric.";
		}
	}

	/// <summary>
	/// The full error string generated when attempting to convert a varchar to a money.
	/// </summary>
	public string UnionSelectErrorStandaloneMoney
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]THIS IS A PLACEHOLDER FOR ABSINTHE [0x90.org]";
		}
	}

	/// <summary>
	/// The full error string generated when attempting to convert a varchar to a datetime.
	/// </summary>
	public string UnionSelectErrorStandaloneDateTime
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]Syntax error converting datetime from character string.";
		}
	}
 
	/// <summary>
	/// The second half of an error when converting a varchar to a smallint.
	/// </summary>
	public string UnionSelectErrorPostSmallint
	{
		get
		{
			return "[Microsoft][ODBC SQL Server Driver][SQL Server]THIS IS A PLACEHOLDER FOR ABSINTHE [0x90.org]";
		}
	}
}
