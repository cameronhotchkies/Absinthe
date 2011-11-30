
//   Absinthe Plugins - The Automated SQL Injection Library
//   This software is Copyright (C) 2005  nummish, 0x90.org
//   $Id: Sql2kErrorPlugin_B.cs,v 1.6 2006/07/15 01:48:58 nummish Exp $
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

namespace Absinthe.Plugins
{
	/// <summary>Summary description for Sql2kErrorPlugin_B.</summary>
	public class Sql2kErrorPlugin_B : IErrorPlugin
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
                        @"Microsoft SQL Server  2000 - 8\.00\.... \(Intel X86\) \n\t(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) (\d| )\d 20\d\d \d\d:\d\d:\d\d \n\tCopyright \(c\) 1988-20\d\d Microsoft Corporation\n\t(Standard|Developer|Enterprise) Edition on Windows NT \d.\d \(Build \d\d\d\d: (Service Pack \d)?\)"
			        };
			}
		}

		public string HavingErrorPre
		{
			get
			{
				return "Column '";
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
				return "The sum or average aggregate operation cannot take a ";
			}
		}

		public string UnionSumErrorOnIntPre
		{
			get
			{	
				return "All queries in an SQL statement containing a UNION operator must have an equal number of expressions in their target lists.";
			}
		}

		public string UnionSumErrorOnText
		{
			get
			{
				return "The text, ntext, and image data types cannot be compared or sorted, except when using IS NULL or LIKE operator.";
			}
		}

		public string UnionSumErrorPost
		{
			get
			{
				return " data type as an argument.";
			}
		}

		/// <summary>The first half of an error when converting an nvarchar to a numeric data type.</summary>
		public string UnionSelectErrorPreNvarchar
		{
			get
			{
				return "Syntax error converting the nvarchar value '";
			}
		}

		/// <summary>The first half of an error when converting a varchar to a numeric data type.</summary>
		public string UnionSelectErrorPreVarchar
		{
			get
			{
				return "Syntax error converting the varchar value '";
			}
		}

		/// <summary>The second half of an error when converting a varchar to an integer.</summary>
		public string UnionSelectErrorPostInt
		{
			get
			{
				return "' to a column of data type int.";
			}
		}

		/// <summary>The second half of an error when converting a varchar to a float.</summary>
		public string UnionSelectErrorPostFloat
		{
			get
			{
				return "' to a column of data type float.";
			}
		}

		/// <summary>The second half of an error when converting a varchar to a smallint.</summary>
		public string UnionSelectErrorPostSmallint
		{
			get
			{
				return "' to a column of data type smallint.";
			}
		}

		/// <summary>The full error string generated when attempting to convert a varchar to a float.</summary>
		public string UnionSelectErrorStandaloneFloat
		{
			get
			{
				//return "' to a column of data type float.";
				return "Error converting data type varchar to float.";
			}
		}

		/// <summary>The full error string generated when attempting to convert a varchar to an integer.</summary>
		public string UnionSelectErrorStandaloneInt
		{
			get
			{
				//return "' to a column of data type int.";
				return "Error converting data type varchar to int.";
			}
		}

		/// <summary>The full error string generated when attempting to convert a varchar to a numeric.</summary>
		public string UnionSelectErrorStandaloneNumeric
		{
			get
			{
				return "Error converting data type varchar to numeric.";
			}
		}

		/// <summary>The full error string generated when attempting to convert a varchar to a money.</summary>
		public string UnionSelectErrorStandaloneMoney
		{
			get
			{
				return "Implicit conversion from data type char to money is not allowed. Use the CONVERT function to run this query.";
			}
		}

		/// <summary>The full error string generated when attempting to convert a varchar to a datetime.</summary>
		public string UnionSelectErrorStandaloneDateTime
		{
			get
			{
				return "Syntax error converting datetime from character string.";
			}
		}
	 
	}
}
