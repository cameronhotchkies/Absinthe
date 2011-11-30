//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: DataStore.cs,v 1.36 2006/08/14 23:00:46 nummish Exp $
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
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Absinthe.Core
{
	/// <summary>The class that handles storage of internal state variables. This may be converted to a singleton later.</summary>
	public class DataStore 
	{
		private bool _UseSSL = false;
		  
#region Public Properties
			 
		/// <summary>The list of all available plugins</summary>
		public List<IPlugin> PluginList
		{
			get
			{
				return _Plugins.PluginList;
			}
		}private PluginManager _Plugins;
			 
		/// <summary>The name of the plugin being used</summary>
		public string LoadedPluginName
		{
			get
			{
				return _LoadedPluginName;
			}
            set
            {
                _LoadedPluginName = value;
            }
		} private string _LoadedPluginName;

		/// <summary>Indicates if a blind injection is being used</summary>
		public bool IsBlind
		{
			get
			{
				return _IsBlind;
			}
			set
			{
				_IsBlind = value;
			}
		}private bool _IsBlind = true;

		/// <summary>Indicates if the injection parameter should be treated as a string</summary>
		public bool InjectAsString
		{
			get
			{
				return _InjectAsString;
			}
			set
			{
				_InjectAsString = value;
			}
		} private bool _InjectAsString = false;

		/// <summary>The comparison tolerance used in blind filters</summary>
		public float FilterTolerance
		{
			get
			{
				return _Tolerance;
			}
			set
			{
				_Tolerance = 0;
				if (value >= 0) _Tolerance = value;
			}	
		} private float _Tolerance = 0.0F; // A default tolerance was just causing problems

		///<summary>Indicates if a SQL statement is to be terminated with a comment</summary>
		public bool TerminateQuery
		{
			get
			{
				return _TerminateQuery;
			}

			set
			{
				_TerminateQuery = value;
			}
		} private bool _TerminateQuery = false;

		///<summary>Used as the delimiter to create the linear signature</summary>
		///<remarks>Defaults to Environment.NewLine</remarks>
		public string FilterDelimiter
		{
			get
			{
				return _FilterDelimiter;
			}
			set
			{
				if (value.Length == 0)
				{
					_FilterDelimiter = Convert.ToChar(0xa).ToString();
				}
				else
				{	
					_FilterDelimiter = value;
				}
			}
		} private string _FilterDelimiter = Environment.NewLine;

        /// <summary>
        /// This is the text appended to the end of the query.
        /// </summary>
        public string AppendedText
        {
            get
            {
                return _AppendedText;
            }
            set
            {
                _AppendedText = value;
            }
        } private string _AppendedText;

		///<summary>The throttle value used to sleep (in msec) between requests</summary>
		///<remarks>If the value is negative, it is rounded to the nearest hundred 
		///which is used for multithreading (eg. -289 -> -300 -> 3 Threads)</remarks>
		public int ThrottleValue
		{
			get
			{
				return _ThrottleValue;
			}
			set
			{
				if(value < 0)
				{
					int Remainder;
					Math.DivRem(value, 100, out Remainder);
					
					if (Remainder < -50) Remainder = 100 + Remainder;

					_ThrottleValue = value - Remainder;
				}
				else
				{
					_ThrottleValue = value;
				}
			}
		} private int _ThrottleValue;	

		///<summary>The filename in which to store the attack configuration</summary>
		public string OutputFile
		{
			get
			{
				return _Filename;
			}

			set
			{
				_Filename = value;
			}
		} private string _Filename = "Absinthe.xml";

		///<summary>The URL of the attack target</summary>
		public string TargetURL
		{
			get
			{
				return _TargetURL;
			}

			set
			{
				_TargetURL = value;
			}
		} private string _TargetURL = "";

		///<summary>The connection method to be used. May be "GET" or "POST"</summary>
		public string ConnectionMethod
		{
			get
			{
				return _ConnectionMethod;
			}

			set
			{
				if (value.ToUpper().Equals("POST") || value.ToUpper().Equals("GET"))
				{
					_ConnectionMethod = value.ToUpper();
				}
				else
				{
					_ConnectionMethod = "";
				}
			}
		} private string _ConnectionMethod = "";

		///<summary>The username the database connection operates as</summary>
		public string Username
		{
			get
			{
				return _Username;
			}

			set
			{
				_Username = value;
			}
		} private string _Username = "";

		/// <summary>The authentication method to be used</summary>
		public GlobalDS.AuthType AuthenticationMethod
		{
			get
			{
				return _AuthenticationMethod;
			}
        } private GlobalDS.AuthType _AuthenticationMethod = GlobalDS.AuthType.None;
		 
		/// <summary>The authentication username to be used</summary>
		public string AuthUser
		{
			get
			{
				return _AuthUser;
			}
		} private string _AuthUser;
		 
		/// <summary>The authentication password to be used</summary>
		public string AuthPassword
		{
			get
			{
				return _AuthPassword;
			}
		} private string _AuthPassword;
		 
		/// <summary>The authentication domain used by NTLM</summary>
		public string AuthDomain
		{
			get
			{
				return _AuthDomain;
			}
		} private string _AuthDomain;
		
		/// <summary>The user agent to send during connections</summary>
		public string UserAgent
		{
			get
			{
				return _UserAgent;
			}
			set
			{
				_UserAgent = value;
			}
		} private string _UserAgent = CommonUserAgents.Absinthe;
		
		///<summary>The list of all the key/value pairs to be treated as cookies</summary>
		public NameValueCollection Cookies
		{
			get 
			{
				return _Cookies;
			}
			set
			{
				_Cookies = value;
			}
		} private NameValueCollection _Cookies = null;

		///<summary>A hashtable of the form parameters, keyed by the parameter name</summary>
		public Hashtable ParameterTable
		{
			get
			{
				return _ParamList;
			}
		} private Hashtable _ParamList = new Hashtable();

		///<summary>The AttackVector used against the target</summary>
		public AttackVector TargetAttackVector
		{
			get
			{
				return _TargetAttackVector;
			}
			set
			{
				value.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
				value.TableChanged += new TableChangedEventHandler(TableChanged);
				_TargetAttackVector = value;
			}
		} private AttackVector _TargetAttackVector = null;
	  
		///<summary>A list of all the tables recovered from the database</summary>
		public Absinthe.Core.GlobalDS.Table[] TableList
		{
			get
			{
				return _DBTables;
			}
			set
			{
				_DBTables = value;
				_AllTablesRetrieved = true;
			}
		} private GlobalDS.Table[] _DBTables = null;
	 	
		/// <summary>Indicates if table retrieval is finished</summary>
		/// <remarks>This is used primarily for restoring a session that failed.</remarks>
		public bool AllTablesRetrieved
		{
			get
			{
				return _AllTablesRetrieved;
			}
		} private bool _AllTablesRetrieved = true;
#endregion
  
#region Public Methods
		/// <summary>Sets the Auth data to empty</summary>
		/// <param name="AuthType">The Auth type, although it doesn't matter what it's set to this overload will set it to "None"</param>
        public void Authdata(GlobalDS.AuthType AuthType)
		{
			_AuthUser = string.Empty;
			_AuthPassword = string.Empty;
			_AuthDomain = string.Empty;
            _AuthenticationMethod = GlobalDS.AuthType.None;
            if (AuthType != GlobalDS.AuthType.None)
			{ throw new Exception("Missing Information, AuthType set to 'None'"); }
			return;
		}

		/// <summary>Sets the authentication data</summary>
		/// <param name="AuthType">The authentication type</param>
		/// <param name="Username">The authentication username</param>
		/// <param name="Password">The authentication password</param>
        public void Authdata(GlobalDS.AuthType AuthType, string Username, string Password)
		{
			_AuthUser = Username;
			_AuthPassword = Password;
			_AuthDomain = string.Empty;

            if (AuthType == GlobalDS.AuthType.None)
			{
				Authdata(AuthType); return;
			}

			_AuthenticationMethod = AuthType;

            if (AuthType == GlobalDS.AuthType.NTLM)
			{
                _AuthenticationMethod = GlobalDS.AuthType.Basic;
				throw new Exception("Missing Domain information, AuthType set to 'Basic'");
			}

		}

		/// <summary>Sets the authentication data</summary>
		/// <param name="AuthType">The authentication type</param>
		/// <param name="Credentials">The authentication credentials</param>
        public void Authdata(GlobalDS.AuthType AuthType, System.Net.NetworkCredential Credentials)
		{
			_AuthUser = Credentials.UserName;
			_AuthPassword = Credentials.Password;
			_AuthDomain = Credentials.Domain;

            if (AuthType == GlobalDS.AuthType.None)
			{
				Authdata(AuthType); return;
			}

			_AuthenticationMethod = AuthType;

            if (AuthType != GlobalDS.AuthType.NTLM)
				_AuthDomain = string.Empty;
		
		}
		 
		///<summary>Adds a form parameter for use during the attack</summary>
		///<param name="value">The FormParam object containing the relevant parameter information</param>
        public void AddFormParameter(GlobalDS.FormParam value)
		{
			//_ParentOutput("There are {0} keys in the Parameter list!", _ParamList.Count);
			_ParamList[value.Name] =  value;
		}

        public void AddCookie(GlobalDS.FormParam value)
        {
            _Cookies.Add(value.Name, value.DefaultValue);
        }
#endregion

#region Events
		/// <summary>The Event to send a status message to the user</summary>
		public event UserEvents.UserStatusEventHandler UserStatus;
		#endregion

		///<summary>Instantiates a new DataStore class</summary>
		public DataStore()
		{			
			_Plugins = new PluginManager();
		}

		private void BubbleUserStatus(string StatusMessage)
		{
            if (UserStatus != null)
			    UserStatus(StatusMessage);
		}

		private void TableChanged(GlobalDS.Table ChangedTable)
		{
			PartialTable = ChangedTable;
		}

        /// <summary>
        /// The unfinished table as it is being downloaded
        /// </summary>
		public GlobalDS.Table PartialTable
		{
			set
			{
				_AllTablesRetrieved = false;
				bool Found = false;
				for (int i = 0; _DBTables != null && i < _DBTables.Length; i++)
				{
					if (_DBTables[i].ObjectID == value.ObjectID)
					{
						_DBTables[i] = value;
						Found = true;
					}	
				}

				if (!Found)
				{
					List<GlobalDS.Table> temp;
					if (_DBTables != null)
						temp = new List<GlobalDS.Table>(_DBTables);
					else
						temp = new List<GlobalDS.Table>();

					temp.Add(value);
					_DBTables = temp.ToArray();
				}
			}
		}
  
		///<summary>Used as a name based lookup for a table</summary>
		///<param name="TableName">The human readable name of the table</param>
		///<returns>The table structure associated with the given name</returns>
		public GlobalDS.Table GetTableFromName(string TableName)
		{
			foreach(GlobalDS.Table tbl in _DBTables)
			{
				if (tbl.Name.Equals(TableName)) return tbl;
			}

			return new GlobalDS.Table();
		}

#region XML File Routines
 
		///<summary>Save all known data to a file</summary>
		public void OutputToFile(string PluginName)
		{

			// TODO: Define this exception better
			if (_Filename.Length == 0) throw new System.Exception("No save file defined.");

			XmlTextWriter xOutput = new XmlTextWriter(_Filename, System.Text.Encoding.UTF8);
			xOutput.Formatting = Formatting.Indented;
			xOutput.Indentation = 4;
			xOutput.WriteStartDocument();

			xOutput.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='http://www.0x90.org/Absinthe.xsl'");

			xOutput.WriteStartElement("absinthedata");
			xOutput.WriteStartAttribute("version", null);
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
			xOutput.WriteString(asm.GetName().Version.ToString(2));
			xOutput.WriteEndAttribute();

			xOutput.WriteStartAttribute("xmlns:xsi", null);
			xOutput.WriteString("http://www.w3.org/2001/XMLSchema-instance");
			xOutput.WriteEndAttribute();

			xOutput.WriteStartAttribute("xsi:noNamespaceSchemaLocation", null);
			xOutput.WriteString("http://www.0x90.org/Absinthe.xsd");
			xOutput.WriteEndAttribute();
   
			WriteTargetInfo(ref xOutput, PluginName);

			WriteDatabaseSchema(ref xOutput);

			xOutput.WriteEndElement();
			xOutput.WriteEndDocument();
			xOutput.Close();
		}
	 
		private void WriteDatabaseSchema(ref XmlTextWriter xOutput)
		{
			if (_Username.Length > 0 || (_DBTables != null && _DBTables.Length > 0))
			{
				xOutput.WriteStartElement("databaseschema");

				if (_Username.Length > 0)
				{
					xOutput.WriteStartAttribute("username", null);
					xOutput.WriteString(_Username);
					xOutput.WriteEndAttribute();
				}

				xOutput.WriteStartAttribute("tablesfinished", null);
				xOutput.WriteString(_AllTablesRetrieved.ToString().ToLower());
				xOutput.WriteEndAttribute();

				if (_DBTables != null && _DBTables.Length > 0) WriteTablesToXml(ref xOutput);

				xOutput.WriteEndElement();
			}
		}
	 
		private void WriteTablesToXml(ref XmlTextWriter xOutput)
		{

			for (int i = 0; i < _DBTables.Length; i++)
			{
				xOutput.WriteStartElement("table");	

				xOutput.WriteStartAttribute("id", null);
				xOutput.WriteString(_DBTables[i].ObjectID.ToString());
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("name", null);
				xOutput.WriteString(_DBTables[i].Name);
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("recordcount", null);
				xOutput.WriteString(_DBTables[i].RecordCount.ToString());
				xOutput.WriteEndAttribute();

				if (_DBTables[i].FieldCount > 0) WriteFieldToXml(ref xOutput, _DBTables[i]);	

				xOutput.WriteEndElement();
			}
		}
	 
		private void WriteFieldToXml(ref XmlTextWriter xOutput, GlobalDS.Table Tbl)
		{
			for (int i = 0; i < Tbl.FieldCount; i++)
			{
				xOutput.WriteStartElement("field");

				xOutput.WriteStartAttribute("id", null);
				xOutput.WriteString((i+1).ToString());
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("name", null);
				xOutput.WriteString(Tbl.FieldList[i].FieldName);
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("datatype", null);
				xOutput.WriteString(Tbl.FieldList[i].DataType.ToString());
				xOutput.WriteEndAttribute();

				if (Tbl.FieldList[i].IsPrimary)
				{
					xOutput.WriteStartAttribute("primary", null);
					xOutput.WriteString(true.ToString().ToLower());
					xOutput.WriteEndAttribute();
				}
				
				xOutput.WriteEndElement();
			}
		}
		 
		private void WriteAttackVector(ref XmlTextWriter xOutput)
		{
			if (_TargetAttackVector != null)
			{
				_TargetAttackVector.ToXml(ref xOutput);
			}
		}
	 
		private void WriteTargetInfo(ref XmlTextWriter xOutput, string PluginName)
		{
			if (_TargetURL.Length > 0 )
			{
				xOutput.WriteStartElement("target");

				xOutput.WriteStartAttribute("address", null);
				xOutput.WriteString(_TargetURL);
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("method", null);
				if (_ConnectionMethod.ToUpper().Equals("POST") || _ConnectionMethod.ToUpper().Equals("GET"))
				{
					xOutput.WriteString(_ConnectionMethod.ToUpper());					
				}
				else
				{
					xOutput.WriteString("GET");					
				}
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("ssl", null);
				xOutput.WriteString(_UseSSL.ToString().ToLower());
				xOutput.WriteEndAttribute();

				if (_UserAgent.Length > 0)
				{
					xOutput.WriteStartAttribute("useragent", null);
					xOutput.WriteString(_UserAgent);
					xOutput.WriteEndAttribute();
				}

				xOutput.WriteStartAttribute("terminatequery", null);
				xOutput.WriteString(_TerminateQuery.ToString().ToLower());
				xOutput.WriteEndAttribute();

				xOutput.WriteStartAttribute("throttle", null);
				xOutput.WriteString(_ThrottleValue.ToString());
				xOutput.WriteEndAttribute();
				
				xOutput.WriteStartAttribute("delimiter", null);
				xOutput.WriteString(_FilterDelimiter.ToString());				
				xOutput.WriteEndAttribute();                   

				xOutput.WriteStartAttribute("tolerance", null);
				xOutput.WriteString(_Tolerance.ToString());
				xOutput.WriteEndAttribute();                   

				xOutput.WriteStartAttribute("blind", null);
				xOutput.WriteString(_IsBlind.ToString().ToLower());
				xOutput.WriteEndAttribute();                   
				
				xOutput.WriteStartAttribute("plugin", null);
				xOutput.WriteString(PluginName);
				xOutput.WriteEndAttribute();

                xOutput.WriteStartAttribute("appendedtext", null);
                xOutput.WriteString(_AppendedText);
                xOutput.WriteEndAttribute();

				WriteAuthenticationData(ref xOutput);
				WriteTargetParameters(ref xOutput);
				WriteCookieData(ref xOutput);

				WriteAttackVector(ref xOutput);

				xOutput.WriteEndElement();
			}
		}
		 
		private void WriteAuthenticationData(ref XmlTextWriter xOutput)
		{
			xOutput.WriteStartElement("authentication");
			
			xOutput.WriteStartAttribute("authtype", null);
			xOutput.WriteString(_AuthenticationMethod.ToString());
			xOutput.WriteEndAttribute();

            if (_AuthenticationMethod != GlobalDS.AuthType.None)
			{
				xOutput.WriteStartElement("username");
				xOutput.WriteStartAttribute("value", null);
				xOutput.WriteString(_AuthUser);
				xOutput.WriteEndAttribute();
				xOutput.WriteEndElement();

				xOutput.WriteStartElement("password");
				xOutput.WriteStartAttribute("value", null);
				xOutput.WriteString(_AuthPassword);
				xOutput.WriteEndAttribute();
				xOutput.WriteEndElement();

                if (_AuthenticationMethod == GlobalDS.AuthType.NTLM)
				{
					xOutput.WriteStartElement("domain");
					xOutput.WriteStartAttribute("value", null);
					xOutput.WriteString(_AuthDomain);
					xOutput.WriteEndAttribute();
					xOutput.WriteEndElement();
				}
			}
			xOutput.WriteEndElement();
		}
	 
		private void WriteTargetParameters(ref XmlTextWriter xOutput)
		{
			foreach (object xp in _ParamList)
			{
                if (((DictionaryEntry)xp).Value.GetType() == typeof(GlobalDS.FormParam))
				{
                    GlobalDS.FormParam fp = (GlobalDS.FormParam)((DictionaryEntry)xp).Value;
					xOutput.WriteStartElement("parameter");

					xOutput.WriteStartAttribute("name", null);
					xOutput.WriteString(fp.Name);
					xOutput.WriteEndAttribute();

					xOutput.WriteStartAttribute("value", null);
					xOutput.WriteString(fp.DefaultValue);
					xOutput.WriteEndAttribute();

					xOutput.WriteStartAttribute("injectable", null);
					xOutput.WriteString(fp.Injectable.ToString().ToLower());
					xOutput.WriteEndAttribute();

					if (fp.AsString)
					{
						xOutput.WriteStartAttribute("string", null);
						xOutput.WriteString(fp.AsString.ToString());
						xOutput.WriteEndAttribute();
					}

					xOutput.WriteEndElement();
				}				 
			}
		}
	 
		private void WriteCookieData(ref XmlTextWriter xOutput)
		{
			if (_Cookies != null)
			{
				foreach (string CookieName in _Cookies.Keys)
				{
					xOutput.WriteStartElement("cookie");

					xOutput.WriteStartAttribute("name", null);
					xOutput.WriteString(CookieName);
					xOutput.WriteEndAttribute();

					xOutput.WriteStartAttribute("value", null);
					xOutput.WriteString(_Cookies[CookieName]);
					xOutput.WriteEndAttribute();

					xOutput.WriteEndElement();
				}
			}
		}

		bool _bValid;

		//ValidationEventHandler Call-back Method
		private void ValidationError(object sender, ValidationEventArgs arguments)
		{
 			if (((System.Xml.Schema.XmlSchemaException)arguments.Exception).Message.IndexOf("tolerance") < 0)
				_bValid = false; //validation failed
			else
				UserStatus("Ignoring I18N xml issues");
		}

		private bool ValidateSavedFile(string Filename)
		{
			FileStream InputStream = null;
			bool retVal = false;

			try
			{
				InputStream = File.OpenRead(Filename);
				retVal = ValidateSavedFile(InputStream);
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				InputStream.Close();
			}
			
			return retVal;
		}
		
		private bool ValidateSavedFile(Stream InputStream)
		{
			_bValid = true;

			Assembly asm = Assembly.GetExecutingAssembly();
			Stream SchemaStream = asm.GetManifestResourceStream("Absinthe.xsd");
			if (SchemaStream == null) SchemaStream = asm.GetManifestResourceStream("Absinthe.Core.Absinthe.xsd");
			XmlTextReader xReader = new XmlTextReader(SchemaStream);

			XmlSchema xmlSchema = XmlSchema.Read(xReader, new ValidationEventHandler(ValidationError));

			//XmlSchemaCollection m_schSchemas = new XmlSchemaCollection();
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.ValidationEventHandler += new ValidationEventHandler(ValidationError);
            xrs.Schemas.Add(xmlSchema);
			//m_schSchemas.Add(xmlSchema);
 
			XmlTextReader xReader2 = new XmlTextReader(InputStream);
            XmlReader xrdr = XmlReader.Create(xReader2, xrs);
			//XmlValidatingReader xValidator = new XmlValidatingReader(xReader2);

			//Assign Schemas to the XmlValidatingReader object
			//xValidator.Schemas.Add(m_schSchemas);
           
			//xValidator.ValidationType = ValidationType.Auto; //set the ValidationType

			//Set the Event Handler for ValidationEventHandler
			//These events occur during Read and only if a ValidationType of DTD, XDR, Schema,  
			//or Auto is specified.
			//If no event handler is provided an XmlException is thrown on the first validation error  
	//		xValidator.ValidationEventHandler += new ValidationEventHandler(ValidationError);
       


			//Validate Document Node By Node
          
            try
            {
                while (xrdr.Read())  //empty body
                {
                    //System.Console.WriteLine(xValidator.NodeType.ToString());
                }
            }
            catch (XmlException)
            {
                //ValidationError();
                _bValid = false;
            }

			return _bValid;
		}

		private MemoryStream UpdateFromOldSavefile(string Filename)
		{
			MemoryStream retVal = null;

			try
			{
				//load the Xml doc
				XPathDocument myXPathDoc = new XPathDocument(Filename) ;
          
				XslCompiledTransform myXslTrans = new XslCompiledTransform() ;

				//load the Xsl from the resources 
				Assembly asm = Assembly.GetExecutingAssembly();
				Stream TransformStream = asm.GetManifestResourceStream("To1_4.xslt");
				if (TransformStream == null) TransformStream = asm.GetManifestResourceStream("Absinthe.Core.To1_4.xslt");
				XmlTextReader xReader = new XmlTextReader(TransformStream);
				myXslTrans.Load(xReader, null, null) ;

				//create the output stream
				retVal = new MemoryStream();
				
				//do the actual transform of Xml
				myXslTrans.Transform(myXPathDoc, null, retVal);
			}
			catch(Exception e)
			{
				Console.WriteLine("Exception: {0}", e.ToString());
			}

			return retVal;
		}

		///<summary>Loads saved target information from an XML file</summary>
		///<param name="Filename">The name of the file to read information from</param>
		///<param name="AnonProxies">The proxies to be using when the data is initialized</param>
		public void LoadXmlFile(string Filename, Queue AnonProxies)
		{
			FileStream InputStream = null;
			XmlDocument xInput = new XmlDocument();
			try
			{
				if (ValidateSavedFile(Filename))
				{
                    InputStream = File.OpenRead(Filename);
					InputStream.Seek(0,	System.IO.SeekOrigin.Begin);
					XmlTextReader xReader3 = new XmlTextReader(InputStream);
					xInput.Load(xReader3);
				}
				else
				{
					//Convert
					MemoryStream mInputStream;
					mInputStream = UpdateFromOldSavefile(Filename);
 
					mInputStream.Seek(0,0);
					
					//Revalidate
					if (ValidateSavedFile(mInputStream))
					{
						mInputStream.Seek(0,0);
						XmlTextReader xReader3 = new XmlTextReader(mInputStream);
						xInput.Load(xReader3);
					}
					else
					{
						throw new InvalidDataFileException(Filename);
					}
				}

				XmlNode docNode = xInput.DocumentElement;

				foreach (XmlNode n in docNode.ChildNodes)
				{
					switch (n.Name)
					{
						case "target": // Load General Target information
							DeserializeTargetXml(n, AnonProxies);
						break;
						case "databaseschema":
							DeserializeSchemaXml(n);
						break;	
						default:
						break;
					}
				}
				OutputFile = Filename;
			}
			catch (System.Xml.XmlException xe)
			{
				System.Console.WriteLine(xe.ToString());
				throw new InvalidDataFileException(Filename);
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.ToString());
			}
			finally
			{
				if (InputStream != null)
					InputStream.Close();
			}
		}

		///<summary>An exception generated when the data file is malformed</summary>
		public class InvalidDataFileException : System.Xml.XmlException
		{
			private string _Filename;

			/// <summary>Instantiates a new InvalidDataFileException</summary>
			/// <param name="Filename">The name of the file causing the exception</param>
			public InvalidDataFileException(string Filename)
			{
				_Filename = Filename;
			}

			/// <summary>The display message used for this exception</summary>
			/// <returns>Indicates the filename that is not valid for Absinthe</returns>
			public new string Message
			{
                get
                {
                    return "The file: \"" + _Filename + "\" is not a valid Absinthe data file.";
                }
			}

		}

		private void DeserializeTargetXml(XmlNode TargetNode, Queue AnonProxies)
		{
			// Init member vars
			_TargetURL = ""; _ConnectionMethod = ""; _UseSSL = false;

			if (TargetNode.Attributes["address"] != null)
			{
				_TargetURL = TargetNode.Attributes["address"].InnerText;
			}

			if (TargetNode.Attributes["useragent"] != null)
			{
				_UserAgent = TargetNode.Attributes["useragent"].InnerText;
			}

			if (TargetNode.Attributes["method"] != null)
			{
				_ConnectionMethod = TargetNode.Attributes["method"].InnerText.ToUpper();
				if (!_ConnectionMethod.ToUpper().Equals("POST") && !_ConnectionMethod.ToUpper().Equals("GET"))
				{
					_ConnectionMethod = "";
				}
			}

			if (TargetNode.Attributes["ssl"] != null)
			{
				_UseSSL = System.Boolean.Parse(TargetNode.Attributes["ssl"].InnerText);
			}

			if (TargetNode.Attributes["blind"] != null)
			{
				_IsBlind = System.Boolean.Parse(TargetNode.Attributes["blind"].InnerText);
			}

			if (TargetNode.Attributes["terminatequery"] != null)
			{
				_TerminateQuery = System.Boolean.Parse(TargetNode.Attributes["terminatequery"].InnerText);
			}

			if (TargetNode.Attributes["plugin"] != null)
			{
				_LoadedPluginName = TargetNode.Attributes["plugin"].InnerText;
			}

			if (TargetNode.Attributes["throttle"] != null)
			{
				_ThrottleValue = Int32.Parse(TargetNode.Attributes["throttle"].InnerText);
			}

            if (TargetNode.Attributes["appendedtext"] != null)
            {
                _AppendedText = TargetNode.Attributes["appendedtext"].InnerText;
            }

			if (_IsBlind)
			{
				if (TargetNode.Attributes["tolerance"] != null)
				{
					FilterTolerance = (float) Convert.ToDouble(TargetNode.Attributes["tolerance"].InnerText);
				}

				if (TargetNode.Attributes["delimiter"] != null)
				{
					FilterDelimiter = TargetNode.Attributes["delimiter"].InnerText;
				}
			}

			DeserializeAttackVector(ref TargetNode, AnonProxies);
			ExtractParametersFromXml(ref TargetNode);
			ExtractCookiesFromXml(ref TargetNode);
			ExtractAuthenticationDataFromXml(ref TargetNode);

		}
	 
        /// <summary>
        /// Converts the Attack Vector from the native type to XML
        /// </summary>
        /// <param name="xInput">The XML node to start deserialization</param>
        /// <param name="AnonProxies">Any anonymous proxies being used</param>
		public void DeserializeAttackVector(ref XmlNode xInput, Queue AnonProxies)
		{
			string FullUrl;
			if (!_UseSSL) FullUrl = "http://" + _TargetURL;
			else FullUrl = "https://" + _TargetURL;

			XmlNode n = xInput.SelectSingleNode("attackvector");	
			if (n == null) return;

			InjectionOptions opts;
			if (_IsBlind)
			{
				opts = new BlindInjectionOptions();
				((BlindInjectionOptions) opts).Delimiter = _FilterDelimiter;
				((BlindInjectionOptions) opts).Tolerance = _Tolerance;
				((BlindInjectionOptions) opts).Throttle = _ThrottleValue;
			}
			else
				opts = new ErrorInjectionOptions();		

			opts.TerminateQuery = _TerminateQuery;
			opts.WebProxies = AnonProxies;

			AttackVectorFactory avf = new AttackVectorFactory(FullUrl, "", "", _ParamList, _ConnectionMethod, opts);
			_TargetAttackVector = avf.BuildFromXml(n, opts, _Plugins.GetPluginByName(_LoadedPluginName));

			_TargetAttackVector.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
		}

		private void ExtractAuthenticationDataFromXml(ref XmlNode TargetNode)
		{
			XmlNode AuthDataNode = TargetNode.SelectSingleNode("authentication");

            if (AuthDataNode == null || AuthDataNode.Attributes["authtype"] == null || !System.Enum.IsDefined(typeof(GlobalDS.AuthType), AuthDataNode.Attributes["authtype"].InnerText)) // Older Data file
			{
                _AuthenticationMethod = GlobalDS.AuthType.None;
				_AuthUser = string.Empty;
				_AuthPassword = string.Empty;
				_AuthDomain = string.Empty;
				return;
			}

            _AuthenticationMethod = (GlobalDS.AuthType)System.Enum.Parse(typeof(GlobalDS.AuthType), AuthDataNode.Attributes["authtype"].InnerText);

			XmlNode tmpnode = AuthDataNode.SelectSingleNode("username");
			if (tmpnode == null || tmpnode.Attributes["value"] == null)
				_AuthUser = string.Empty;
			else
				_AuthUser = tmpnode.Attributes["value"].InnerText;

			tmpnode = AuthDataNode.SelectSingleNode("password");
			if (tmpnode == null || tmpnode.Attributes["value"] == null)
				_AuthPassword = string.Empty;
			else
				_AuthPassword = tmpnode.Attributes["value"].InnerText;

			tmpnode = AuthDataNode.SelectSingleNode("domain");
			if (tmpnode == null || tmpnode.Attributes["value"] == null)
				_AuthDomain = string.Empty;
			else
				_AuthDomain = tmpnode.Attributes["value"].InnerText;

			return;
		}

		private void ExtractParametersFromXml(ref XmlNode TargetNode)
		{
			XmlNodeList Parameters = TargetNode.SelectNodes("parameter");

			if (Parameters.Count > 0)
			{
				_ParamList.Clear();

				foreach (XmlNode param in Parameters)
				{
                    GlobalDS.FormParam NewParam = new GlobalDS.FormParam();

					if (param.Attributes["name"] != null)
					{
						NewParam.Name = param.Attributes["name"].InnerText;
					}

					if (param.Attributes["value"] != null)
					{
						NewParam.DefaultValue = param.Attributes["value"].InnerText;
					}

					if (param.Attributes["injectable"] != null)
					{
						NewParam.Injectable = System.Boolean.Parse(param.Attributes["injectable"].InnerText);

						if (param.Attributes["string"] != null)
						{
							NewParam.AsString = System.Boolean.Parse(param.Attributes["string"].InnerText);
						}
					}

					_ParamList.Add(NewParam.Name, NewParam);
				}
			}

		}

		private void ExtractCookiesFromXml(ref XmlNode TargetNode)
		{
			XmlNodeList CookieElements = TargetNode.SelectNodes("cookie");

			_Cookies = new NameValueCollection();

			if (CookieElements.Count > 0)
			{
				foreach (XmlNode cookie in CookieElements)
				{
					string CookieName, CookieValue;

					if (cookie.Attributes["name"] != null)
					{
						CookieName = cookie.Attributes["name"].InnerText;

						if (cookie.Attributes["value"] != null)
						{
							CookieValue = cookie.Attributes["value"].InnerText;
							_Cookies[CookieName] = CookieValue;
						}
					}
				}
			}
		}

		private void DeserializeSchemaXml(XmlNode TargetNode)
		{
			// Init member vars
			_Username = "";_AllTablesRetrieved = true;

			if (TargetNode.Attributes["username"] != null)
			{
				_Username = TargetNode.Attributes["username"].InnerText;
			}

			if (TargetNode.Attributes["tablesfinished"] != null)
			{
				_AllTablesRetrieved = bool.Parse(TargetNode.Attributes["tablesfinished"].InnerText);
			}

			XmlNodeList Tables = TargetNode.SelectNodes("table");

			if (Tables.Count > 0)
			{
				List<GlobalDS.Table> TableList = new List<GlobalDS.Table>();
				
				foreach (XmlNode ExtractedTable in Tables)
				{
					GlobalDS.Table ThisTable = new GlobalDS.Table();

					if (ExtractedTable.Attributes["name"] != null && ExtractedTable.Attributes["id"] != null)
					{
						ThisTable.Name = ExtractedTable.Attributes["name"].InnerText;
						ThisTable.ObjectID = System.Int32.Parse(ExtractedTable.Attributes["id"].InnerText);

						if (ExtractedTable.Attributes["recordcount"] != null)
						{
							ThisTable.RecordCount = System.Int64.Parse(ExtractedTable.Attributes["recordcount"].InnerText);
						}

						XmlNodeList Fields = ExtractedTable.SelectNodes("field");
						foreach (XmlNode ExtractedField in Fields)
						{
							GlobalDS.Field ThisField = new GlobalDS.Field();

							if (ExtractedField.Attributes["name"] != null)
							{
								ThisField.FieldName = ExtractedField.Attributes["name"].InnerText;
							}

							if (ExtractedField.Attributes["datatype"] != null)
							{
								ThisField.DataType = (System.Data.SqlDbType) System.Enum.Parse(typeof(System.Data.SqlDbType),ExtractedField.Attributes["datatype"].InnerText);
							}

							if (ExtractedField.Attributes["primary"] != null)
							{
								try
								{
									ThisField.IsPrimary = bool.Parse(ExtractedField.Attributes["primary"].InnerText);
								}
								catch (System.FormatException)
								{
									ThisField.IsPrimary = false;
								}
							}

							ThisTable.AddField(ThisField);
						}

						TableList.Add(ThisTable);
					}
				}
				_DBTables = TableList.ToArray();                    
			}
		}
#endregion

	}
}
