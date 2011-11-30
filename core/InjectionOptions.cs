//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004-2007  nummish, 0x90.org
//   $Id: InjectionOptions.cs,v 1.9 2006/05/25 22:40:23 nummish Exp $
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
using System.Collections;
using System.Collections.Specialized;
using System.Net;


namespace Absinthe.Core
{
	/// <summary>A collection of commonly used user agents.</summary>
	public class CommonUserAgents  
	{
		/// <summary>The default user agent for 0x90.org's Absinthe</summary>
		public const string Absinthe = "Absinthe/2.0";
		/// <summary>The user agent for Mozilla Firefox</summary>
		public const string Firefox = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-GB; rv:1.7.8) Gecko/20050418 Firefox/1.0.4";
		/// <summary>The user agent for Galeon</summary>
		public const string Galeon = "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.7.3) Gecko/20040913 Galeon/1.3.18";
		/// <summary>The user agent for GoogleBot</summary>
		public const string GoogleBot = "Googlebot/2.1 (+http://www.googlebot.com/bot.html)";
		/// <summary>The user agent for MS Internet Explorer</summary>
		public const string InternetExplorer = "Mozilla/4.0 (compatible; MSIE 6.0; WINDOWS; .NET CLR 1.1.4322)";
		/// <summary>The user agent for Konqueror</summary>
		public const string Konqueror = "Mozilla/5.0 (compatible; Konqueror/3.4; Linux) KHTML/3.4.1 (like Gecko)";
		/// <summary>The user agent for Links</summary>
		public const string Links = "Links (2.1pre15; Linux 2.4.26-vc4 i586; x)";
		/// <summary>The user agent for Mozilla</summary>
		public const string Mozilla = "Mozilla/5.0 (Windows; U; Win98; en-US; rv:1.8b3) Gecko/20050713 SeaMonkey/1.0a";
		/// <summary>The user agent for Netscape</summary>
		public const string Netscape = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:1.7.5) Gecko/20050519 Netscape/8.0.1";
		/// <summary>The user agent for Opera</summary>
		public const string Opera = "Opera/8.01 (Windows NT 5.1)";
		/// <summary>The user agent for Safari</summary>
		public const string Safari = "Mozilla/5.0 (Macintosh; U; PPC Mac OS X; en) AppleWebKit/412 (KHTML, like Gecko) Safari/412";
	}

	/// <summary>
	/// The base class of options available to injections
	/// </summary>
	public abstract class InjectionOptions
	{
		private bool _TerminateQuery;
		private bool _InjectAsString;
		private Queue _WebProxies;
		private NameValueCollection _Cookies;
	
		private string _AppendedQuery;
		private NetworkCredential _AuthCredentials;

		/// <summary>The user agent to display at all connections</summary>
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
		} private string _UserAgent = "Absinthe/1.4";

		/// <summary>
		/// Instantiates a new Injection Option class
		/// </summary>
		public InjectionOptions()
		{
			
			_TerminateQuery = false;
			_WebProxies = null;
			_Cookies = null;			
			_InjectAsString = false;
			_AppendedQuery = String.Empty;
			_AuthCredentials = null;
		}

		/// <summary>The text to be appended to the end of the query</summary>
		public string AppendedQuery
		{
			get
			{
				return _AppendedQuery;
			}
			set
			{
				_AppendedQuery = value;
				if (value != null && value.Length > 0) TerminateQuery = false;
			}
		}
		
		/// <summary>Identifies if the injected parameter should be treated as a string</summary>
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
		}
		
		/// <summary>Indicates if the SQL should be terminated by comments</summary>
		public bool TerminateQuery
		{
			get
			{
				return _TerminateQuery;
			}
			set
			{
				_TerminateQuery = value;
				if (value) _AppendedQuery = String.Empty;
			}
		}
		
		// {{{ WebProxies Property
		/// <summary>
		/// The list of all the Web proxies to be used during the injection
		/// </summary>
		public Queue WebProxies
		{
			get
			{
				return _WebProxies;
			}
			set
			{
				if (value != null && value.Count > 0)
				{
					_WebProxies = value;
				}
				else
				{
					_WebProxies = null;
				}
			}
		}
		// }}}

		// {{{ Cookies Property
		/// <summary>
		/// All of the Cookies to be used during the injection
		/// </summary>
		public NameValueCollection Cookies
		{
			get
			{
				return _Cookies;
			}
			set
			{
				if (value != null && value.Count > 0)
				{ 
					_Cookies = value;
				}
				else
				{
					_Cookies = null;
				}
			}
		}
		// }}}

		// {{{ AuthCredentials Property
		/// <summary>
		/// The authentication information for this connection
		/// </summary>
		public NetworkCredential AuthCredentials
		{
			get
			{
				return _AuthCredentials;
			}
			set
			{
				_AuthCredentials = value;
			}
		}
		// }}}
	}
}
