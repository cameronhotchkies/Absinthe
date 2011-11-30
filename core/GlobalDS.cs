//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: GlobalDS.cs,v 1.16 2006/05/25 22:40:23 nummish Exp $
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
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Absinthe.Core
{

	///<summary>All data structures universal to the Core Library</summary>
	public class GlobalDS
	{

        ///<summary>The details of a parameter sent along with an HTTP request</summary>
        public struct FormParam
        {
            ///<summary>The name of the parameter</summary>
            public string Name;
            ///<summary>The default value of the parameter</summary>
            public string DefaultValue;
            ///<summary>Indicates if the parameter is a valid injection point</summary>
            public bool Injectable;

            /// <summary>Indicates the parameter should be treated as a string</summary>
            public bool AsString;
        }

        /// <summary>The types of http authentication available</summary>
        public enum AuthType : byte
        {
            /// <summary>No authentication should be used</summary>
            None = 0,
            /// <summary>Basic plain text authentication should be used</summary>
            Basic = 1,
            /// <summary>HTTP Digest authentication should be used</summary>
            Digest = 2,
            /// <summary>NTLM authentication should be used</summary>
            NTLM = 3
        }

		///<summary>A primary key structure for enumerating table data</summary>
		public struct PrimaryKey
		{
			///<summary>The name of the column the primary key is located in</summary>
			public string Name;
			///<summary>The textual value of the primary key</summary>
			public string Value;
			///<summary>The value of the primary key escaped for use in queries</summary>
			public string OutputValue;
		}
		
		///<summary>A field in the database being exploited</summary>
		public struct Field
		{
			private string _FieldName;
			private System.Data.SqlDbType _DataType;
			private string _TableName; // Generally will be empty, but could be used for detached fields
			private bool _IsPrimary;

			///<summary>The human readable name of the field</summary>
			public string FieldName
			{
				get
				{
					return _FieldName;
				}

				set
				{
					if (value.LastIndexOf('.') >= 0)
					{
						_TableName = value.Substring(0, value.LastIndexOf('.'));
						_FieldName = value.Substring(value.LastIndexOf('.') + 1);
					}
					else
					{
						_FieldName = value;
					}
				}
			}
			
			///<summary>The human readable name of the table the field is located in</summary>
			public string TableName
			{
				get 
				{
					return _TableName;
				}
			}
			
            ///<summary>The name of the table and field appended together</summary>
			public string FullName
			{
				get
				{
					StringBuilder retVal = new StringBuilder();

					if (_TableName.Length > 0)
					{
						retVal.Append(_TableName).Append(".");
					}

					retVal.Append(_FieldName);

					return retVal.ToString();
				}
			}
			
			///<summary>The datatype of the field</summary>
			public System.Data.SqlDbType DataType
			{
				get
				{
					return _DataType;
				}

				set
				{
					_DataType = value;
				}
			}
			
			///<summary>Indicates if this field is a primary key for this table</summary>
			public bool IsPrimary
			{
				get
				{
					return _IsPrimary;
				}
				set
				{
					_IsPrimary = value;
				}
			}			
		}
		
		///<summary>A table in the database being exploited</summary>
		public struct Table
		{
			private string _TableName;
			private long _TableID, _RecordCount;
			private List<Field> _FieldList;

			///<summary>The human readable name of the table</summary>
			public string Name
			{
				get
				{
					return _TableName;
				}
				set
				{
					_TableName = value;
				}
			}

			///<summary>The number of fields (columns) in this table</summary>
			public int FieldCount
			{
				get
				{
					if (_FieldList == null) return 0;
					return _FieldList.Count;
				}
			}

			///<summary>The database recognizable numerical ID of this table</summary>
			public long ObjectID
			{
				get
				{
					return _TableID;
				}
				set
				{
					_TableID = value;
				}
			}
			
			///<summary>Add a field to this table</summary>
			///<param name="Value">The field to be added to this table</param>
			public void AddField(Field Value)
			{
                if (_FieldList == null) _FieldList = new List<GlobalDS.Field>();
				_FieldList.Add(Value);
			}
			
			///<summary>The list of fields stored in this table</summary>
			public GlobalDS.Field[] FieldList
			{
				get
				{	
					if (_FieldList == null) return null;

                    return _FieldList.ToArray();
				}
			}
			
			///<summary>The number of data records in this table</summary>
			public long RecordCount
			{
				get
				{
					return _RecordCount;
				}

				set
				{
					_RecordCount = value;
				}
			}			
		}

		/// <summary>The types of exploits available</summary>
		public enum ExploitType : byte
		{
			/// <summary>The type of exploit is undefined</summary>
			Undefined = 255,
			/// <summary>A blind sql injection</summary>
			BlindSQLInjection = 0,
			/// <summary>An error based sql injection</summary>
			ErrorBasedTSQL = 1
		}		
	}
}
