//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: ParsePage.cs,v 1.14 2006/02/17 17:19:20 nummish Exp $
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
//  You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Data;

namespace Absinthe.Core
{

	///<summary>This class handles anything related to page comparisons</summary>
	public class ParsePage
	{
		/// <summary>The event to send a status message to a parent class</summary>
		public static event UserEvents.UserStatusEventHandler UserStatus;

		#region Error Based Stuff
		/// <summary>Parses a result page for a UNION SUM error to determine the data type</summary>
		/// <param name="HTMLCode">The HTML text from the resulting error page</param>
		/// <param name="Plugin">The Plugin to use to identify the data type</param>
		/// <returns>The data type of the field generating the error</returns>
		public static System.Data.SqlDbType ParseUnionSumError(string HTMLCode, IErrorPlugin Plugin)
		{
			SqlDbType retVal = new System.Data.SqlDbType();
			retVal = SqlDbType.Variant;

			int StartError, StartData, EndError, StartSize;
			string ErrorData;

			// Check for the first half of the error
			StartError = HTMLCode.IndexOf(Plugin.UnionSumErrorPre);
			StartSize = Plugin.UnionSumErrorPre.Length;

			if (StartError >= 0)
			{
				// Now check for the second half of the error
				EndError = HTMLCode.IndexOf(Plugin.UnionSumErrorPost);

				if (EndError > StartError)
				{
					StartData = StartError + StartSize;
					ErrorData = HTMLCode.Substring(StartData, EndError - StartData);

					switch (ErrorData)
					{
						case "datetime":
							retVal = SqlDbType.DateTime;
							break;
						case "varchar":
							retVal = SqlDbType.VarChar;
							break;
						case "nvarchar":
							retVal = SqlDbType.NVarChar;
							break;
						case "bit":
							retVal = SqlDbType.Bit;
							break;
						case "char":
							retVal = SqlDbType.Char;
							break;
						case "smallint":
							retVal = SqlDbType.SmallInt;
							break;
						default:
							UserStatus(String.Format("Unknown Data Type of type: {0}", ErrorData));
							break;
					}
				}

			}
			else if (HTMLCode.IndexOf(Plugin.UnionSumErrorOnIntPre) >= 0)
			{
				retVal = SqlDbType.Int;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSumErrorOnText) >= 0)
			{
				retVal = SqlDbType.Text;
			}				
			else
			{
				UserStatus(String.Format("Error: {0}", HTMLCode));
			}
		

			return retVal;
		}
	 
		/// <summary>
		/// Parses a result page for a UNION SELECT error to refine the data type
		/// </summary>
		/// <param name="HTMLCode">The HTML text from the resulting error page</param>
		/// <param name="Plugin">The Plugin to use to identify the data type</param>
		/// <returns>The refined data type of the field that caused the error</returns>
		public static System.Data.SqlDbType ParseUnionSelectForIntegerRefinement(string HTMLCode, IErrorPlugin Plugin)
		{
			System.Data.SqlDbType retVal = new System.Data.SqlDbType();
			//retVal = TSQL.DataType.Undefined;

			int StartError, EndError;

			// Check for the first half of the error
			StartError = HTMLCode.IndexOf(Plugin.UnionSelectErrorPreVarchar); 
			UserStatus(HTMLCode);

			// Now check for the second half of the error
			EndError = HTMLCode.IndexOf(Plugin.UnionSelectErrorPostInt); 
			if (EndError > StartError || HTMLCode.IndexOf(Plugin.UnionSelectErrorStandaloneInt) > StartError)
			{
				retVal = SqlDbType.Int;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorPostFloat) > StartError || HTMLCode.IndexOf(Plugin.UnionSelectErrorStandaloneFloat) > StartError)
			{
				retVal = SqlDbType.Float;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorPostSmallint) > StartError)
			{
				retVal = SqlDbType.SmallInt;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorStandaloneNumeric) > StartError)
			{
				retVal = SqlDbType.Int;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorStandaloneDateTime) > StartError)
			{
				retVal = SqlDbType.DateTime;
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorStandaloneMoney) > StartError)
			{
				retVal = SqlDbType.Money;
			}
  			else 
			{
				UserStatus(HTMLCode);
				//throw new ApplicationException("Unknown Int");
				retVal = SqlDbType.Variant;
			}


			return retVal;
		}
	 
		/// <summary>
		/// Parses out Varchar text from a UNION SELECT error
		/// </summary>
		/// <param name="HTMLCode">The HTML text from the resulting error page</param>
		/// <param name="Plugin">The Plugin to use to identify the data type</param>
		/// <returns>The string encapsulated in the error message</returns>
		public static string ParseUnionSelectForVarchar(string HTMLCode, IErrorPlugin Plugin)
		{
			return ParseUnionSelectForStrings(HTMLCode, Plugin, StringTypes.Varchar);
		}

		/// <summary>
		///  Parses out NVarchar text from a UNION SELECT error
		/// </summary>
		/// <param name="HTMLCode">The HTML text from the resulting error page</param>
		/// <param name="Plugin">The Plugin to use to identify the data type</param>
		/// <returns>The string encapsulated in the error message</returns>
		public static string ParseUnionSelectForNvarchar(string HTMLCode, IErrorPlugin Plugin)
		{
			return ParseUnionSelectForStrings(HTMLCode, Plugin, StringTypes.Nvarchar);
		}

		private enum StringTypes
		{
			Nvarchar = 1,
			Varchar = 2,
		}
		
		private static string ParseUnionSelectForStrings(string HTMLCode, IErrorPlugin Plugin, StringTypes StringType)
		{
			string retVal = "";

			int StartError, EndError;

			// Check for the first half of the error
			StartError = -1;

			switch (StringType)
			{
				case StringTypes.Nvarchar:
					StartError = HTMLCode.IndexOf(Plugin.UnionSelectErrorPreNvarchar); 
					StartError += Plugin.UnionSelectErrorPreNvarchar.Length;
					break;
				case StringTypes.Varchar:
					StartError = HTMLCode.IndexOf(Plugin.UnionSelectErrorPreVarchar); 
					StartError += Plugin.UnionSelectErrorPreVarchar.Length;
					break;
			}
		 
			// Now check for the second half of the error
			EndError = HTMLCode.IndexOf(Plugin.UnionSelectErrorPostInt); 
			if (EndError > StartError)
			{
				retVal = HTMLCode.Substring(StartError, EndError - StartError);
			}
			else if(HTMLCode.IndexOf(Plugin.UnionSelectErrorPostFloat) > StartError)
			{
				retVal = HTMLCode.Substring(StartError, HTMLCode.IndexOf(Plugin.UnionSelectErrorPostFloat) - StartError);
			}
			else 
			{
				retVal = String.Empty;
			}

            if (UserStatus != null)
			    UserStatus(string.Format("Parsed: [{0}]", retVal));

			return retVal;
		}
		// }}}

		// {{{ ParseGroupedHaving
		/// <summary>
		/// Parse the error generated by a HAVING error that has a GROUP BY clause
		/// </summary>
		/// <param name="HTMLCode">The HTML text from the resulting error page</param>
		/// <param name="Plugin">The Plugin to use to identify the data type</param>
		/// <returns>The name of the field that caused the error</returns>
		public static GlobalDS.Field ParseGroupedHaving(string HTMLCode, IErrorPlugin Plugin)
		{
			GlobalDS.Field retVal = new GlobalDS.Field();
			int StartError, StartData, EndError, StartSize;
			string ErrorData;

			// Initialize retVal
			retVal.DataType = SqlDbType.Variant;
			retVal.FieldName = String.Empty;

			// Check for first half of the error
			StartError = HTMLCode.IndexOf(Plugin.HavingErrorPre);
			StartSize = Plugin.HavingErrorPre.Length; 

			if (StartError >= 0)
			{

				// Now check for the second half of the error
				EndError = HTMLCode.IndexOf(Plugin.HavingErrorPost); 
				if (EndError < StartError)
					EndError = HTMLCode.IndexOf(Plugin.HavingErrorPostWithGroupBy);

				if (EndError > StartError)
				{
					StartData = StartError + StartSize;
					ErrorData = HTMLCode.Substring(StartData, EndError - StartData);

					retVal.FieldName = ErrorData;
				}

			}

			// Return the information
			return retVal;
		}

		// }}}
#endregion

		#region Blind Stuff
		// {{{ CompareSignatures
		///<summary>Compares a known value signature set to an unknown value signature set</summary>
		///<returns>A value indicating if the two signatures match</returns>
		///<param name="KnownCase">The signature of the known value</param>
		///<param name="UnknownCase">The signature of the unknown value</param>
		///<param name="Tolerance">The tolerance band to use during comparison</param>
		public static bool CompareSignatures(double[] KnownCase, double[] UnknownCase, float Tolerance)
		{
			if (KnownCase.Length != UnknownCase.Length)
			{
				// Should it be extended? I say no for now.. 
				//ParentOutput("Page Lengths don't match.. I'm outta here {0} vs Unknown: {1}", KnownCase.Length, UnknownCase.Length);
				//return false;
			}

			int MaxIter = KnownCase.Length > UnknownCase.Length ? UnknownCase.Length : KnownCase.Length;

			for (int i=0; i < MaxIter; i++)
			{
				double Known, Unknown;
				Known = KnownCase[i];
				Unknown = UnknownCase[i];

				UserStatus(String.Format("Known Value: [{0}] Unknown Value [{1}]", Known, Unknown));

				// Compare the difference to the tolerance for this value
				if ((Math.Abs(Known - Unknown)/Known) > (Tolerance * Known))
				{
					return false;
				}
			}

			// No breaks encountered
			return true;
		}
		
		/// <summary>Compares the signature to a recieved page utilizing the filters</summary>
		/// <param name="KnownCase">The signature with a known meaning</param>
		/// <param name="UnknownCase">The signature with an unknown meaning</param>
		/// <param name="Filter">The filter to apply against the comparison</param>
		/// <param name="Tolerance">The tolerance for error on the signatures</param>
		/// <returns>An indication if they match</returns>
		public static bool CompareSignatures(double[] KnownCase, double[] UnknownCase, int[] Filter, float Tolerance)
		{
			bool retVal = true;
			for (int i=0; i < Filter.Length; i++)
			{
				int CompareIndex = Filter[i];

				if (CompareIndex < UnknownCase.Length)
				{
					double Known, Unknown;
					Known = KnownCase[CompareIndex];
					Unknown = UnknownCase[CompareIndex];

					// Compare the difference to the tolerance for this value
					if ((Math.Abs(Known - Unknown)/Known) > (Tolerance * Known))
					{
						retVal = false;
					}
				}
			}

			// No breaks encountered
			return retVal;
		}
 
		///<summary>Generate the Page Signature for use in blind SQL Injections</summary>
		///<param name="HtmlPage">The source HTML to generate a signature from</param>
		///<param name="Delimiter">The delimiter to use when generating signatures</param>
		///<returns>The ASCII-Sum signature for the given page</returns>
		public static double[] GetHtmlPageSignature(string HtmlPage, string Delimiter)
		{
			string[] PageStringArray;
			List<double> SumArray = new List<double>();
			int Sum;

			PageStringArray = SplitByString(HtmlPage, Delimiter);

			for (int i = 0; i < PageStringArray.Length; i++)
			{
				int j = 0;
				char[] LineArray = PageStringArray[i].ToCharArray();

				Sum = 0;
				while (j < PageStringArray[i].Length)
				{
					Sum += LineArray[j];
					j++;
				}
				SumArray.Add(double.Parse(Sum.ToString()));
			}

			return SumArray.ToArray();
		}
 
		private static string[] SplitByString(string Value, string Delimiter)
		{
			List<string> retVal = new List<string>();
			string Shrinker = Value;

			while (Shrinker.IndexOf(Delimiter) >= 0)
			{
				int pos = Shrinker.IndexOf(Delimiter);
				if (pos > 0)
				{
					string s = Shrinker.Substring(0, pos);
					retVal.Add(s);
				}
				Shrinker = Shrinker.Substring(pos + Delimiter.Length);
			}

			if (Shrinker.Length > 0) retVal.Add(Shrinker); 

			return retVal.ToArray();
		}
		// }}}

		// {{{ GenerateAdaptiveFilter
		///<summary>Generate the adaptive filter from a set of signatures that embody the same boolean value</summary>
		///<param name="Signatures">An array of signatures that represent the same result value</param>
		///<param name="Tolerance">The tolerance to be used to generate the filter</param>
		///<returns>An array of indices that do not change indepedently of the desired page results</returns>		
		public static int[] GenerateAdaptiveFilter(double[][] Signatures, float Tolerance)
		{
			int SignatureCount = Signatures.Length;
			int MinSignatureCount = 0;

			for (int i=0; i < SignatureCount; i++)
			{
				if (MinSignatureCount == 0 || MinSignatureCount > Signatures[i].Length)
				{
					MinSignatureCount = Signatures[i].Length;
				}
			}

			List<int> RetVal = new List<int>();
			
			/* Previous incarnations took an average, but this was not mathematically sound */
			for (int i=0; i < MinSignatureCount; i++)
			{
				bool NoAdd = false;
			
				for(int j=0; j < SignatureCount; j++)
				{
					if (j > 0 && Math.Abs(Signatures[j][i] - Signatures[j-1][i]) > Tolerance) NoAdd = true;
				}

				if (NoAdd == false) RetVal.Add(i);
			}

			return RetVal.ToArray();
		}
		// }}}

		// {{{ GenerateSubtractiveFilter
		///<summary>Generate the subtractive filter from a set of opposing signatures</summary>
		///<param name="Signature1">The first signature to generate the filter against</param>
		///<param name="Signature2">The second signature to generate the filter against. This should represent a 
		///different value than the first signature</param>
		///<param name="Tolerance">The tolerance to be used to generate the filter</param>
		///<returns>An array of indices that contain values unique to each signature</returns>
		public static int[] GenerateSubtractiveFilter(double[] Signature1, double[] Signature2, float Tolerance)
		{
			int MinLength = (Signature1.Length > Signature2.Length) ? Signature2.Length : Signature1.Length;
			List<int> RetVal = new List<int>();

			for (int i = 0; i < MinLength; i++)
			{
				if (Math.Abs(Signature1[i] - Signature2[i]) > Tolerance)
				{
					RetVal.Add(i);
				}
			}

			return RetVal.ToArray();
		}
		// }}}
		#endregion
	}
}
