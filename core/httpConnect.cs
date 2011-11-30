
//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004-2007  nummish, 0x90.org
//   $Id: httpConnect.cs,v 1.21 2006/05/25 22:40:23 nummish Exp $
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

#define DISPLAY_CONNECTS
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Absinthe.Core
{

	///<summary>
	///All HTTP connections are done through this class.
	///</summary>
	public class httpConnect
	{
        /// <summary>
        /// The event for bubbling up status messages
        /// </summary>
		public static event UserEvents.UserStatusEventHandler UserStatus;
		
		///<summary>
		///Request the HTML page
		///</summary>
		///<returns>The whole HTML page as a single string</returns>
		///<param name="ConnectURL">The URL to request the page from</param>
		///<param name="Data">The Key/Value pairs to send along with the request</param>
		///<param name="Proxy">The web proxy to use. This is null if it is a direct connection</param>
		///<param name="UsePost">Indicates if the request is a POST request. Otherwise it is a GET.</param>
		///<param name="Cookies">The Key/Value pairs to send as cookies.</param>
		///<param name="AuthCredentials">The authentication data for this request</param>
		///<param name="UserAgent">The user agent to be displayed for this request</param>
		public static string PageRequest(string ConnectURL, NameValueCollection Data, WebProxy Proxy, bool UsePost, NameValueCollection Cookies, 
				NetworkCredential AuthCredentials, string UserAgent)
		{
			//ParentOutput(System.String.Format("Making a Page request {0}", UsePost));
			if (UsePost)
			{
				return PostPage(ConnectURL, Data, Proxy, Cookies, AuthCredentials, UserAgent);
			}
			else
			{
				return GetPage(ConnectURL, Data, Proxy, Cookies, AuthCredentials, UserAgent);
			}
		}
 
		private static string GetPage(string ConnectURL, NameValueCollection GetData, WebProxy Proxy, NameValueCollection Cookies, NetworkCredential AuthCredentials, string UserAgent)
		{
			return GetPage(ConnectURL, GetData, Proxy, Cookies, AuthCredentials, UserAgent, false);
		}

        private static bool DummySslCheck(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

		private static string GetPage(string ConnectURL, NameValueCollection GetData, WebProxy Proxy, NameValueCollection Cookies, NetworkCredential AuthCredentials, string UserAgent, bool SlowProtocol)
		{
            RemoteCertificateValidationCallback instance = new RemoteCertificateValidationCallback(DummySslCheck);
            ServicePointManager.ServerCertificateValidationCallback = instance;
			StringBuilder QueryURL = new StringBuilder();
			QueryURL.Append(ConnectURL);

			if (GetData.Count > 0)
			{
				// Toss onto qstring
				QueryURL.Append("?");
				QueryURL.Append(GenerateQueryString(GetData, true));
			}

			HttpWebRequest TestGet = (HttpWebRequest) WebRequest.Create(QueryURL.ToString());
			TestGet.Method = "GET";
			if (SlowProtocol) TestGet.ProtocolVersion = HttpVersion.Version10; 
			if (Proxy != null) TestGet.Proxy = Proxy;

			TestGet.UserAgent = UserAgent;

#if DISPLAY_CONNECTS
			UserStatus(QueryURL.ToString()); //HACK
#endif

			HttpWebResponse resp;
			resp = null;	

			if (Cookies != null)
			{
				foreach (string CookieName in Cookies.Keys)
				{
					try
					{
						Cookie ck = new Cookie(CookieName, Cookies[CookieName], "/", ConnectURL.Split('/')[2].Split(':')[0]);
						if (TestGet.CookieContainer == null)
						{
							TestGet.CookieContainer = new CookieContainer();
						}
						TestGet.CookieContainer.Add(ck);
					}
					catch (NullReferenceException nre)
					{
						UserStatus(nre.Message);	
						throw new Exception("Cookies could not be attached");
					}					 
				}
			}

			if (AuthCredentials != null) TestGet.Credentials = AuthCredentials;

			try
			{ 
				resp = (HttpWebResponse)TestGet.GetResponse(); 
			}
			catch (WebException wex)
			{
				if(wex.Status == WebExceptionStatus.ReceiveFailure)
				{
					// Mono seems to choke when used over wifi, then idled.. if this error takes place,
					// drop down to HTTP/1.0 for the single renegotiate connect.. (standard 1.0 is generally too slow though)
					return GetPage(ConnectURL, GetData, Proxy, Cookies, AuthCredentials, UserAgent, true);
				}
				else if (wex.Status == WebExceptionStatus.Timeout)
				{
					// Try again I guess.. 
					return GetPage(ConnectURL, GetData, Proxy, Cookies, AuthCredentials, UserAgent, false);
				}
				else if(wex.Status == WebExceptionStatus.ProtocolError)
				{
					// Assume this is for the error based injection
					resp = (HttpWebResponse)wex.Response;
				}
				else 
				{
					//ParentOutput(wex.ToString());
					throw(wex);
				}
			}

			// Get the stream associated with the response.
			Stream receiveStream = resp.GetResponseStream();

			// Pipes the stream to a higher level stream reader with the required encoding format. 
			StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);

			string retVal = readStream.ReadToEnd();
			resp.Close ();
			readStream.Close ();
 
			return retVal;
		}
		// }}}

		// {{{ GenerateQueryString
		private static string GenerateQueryString(NameValueCollection Data, bool Encode)
		{
			StringBuilder QueryParams = new StringBuilder();

			foreach(string Key in Data.Keys)
			{
				if (Encode)
				{QueryParams.Append(HttpUtility.UrlEncode(Key));}
				else
				{QueryParams.Append((Key));}

				QueryParams.Append("=");

				if (Encode) 
				{QueryParams.Append(HttpUtility.UrlEncode(Data[Key]));}
				else
				{QueryParams.Append((Data[Key]));}

				QueryParams.Append("&");
			}

			// Trim trailing ampersand
			QueryParams.Remove(QueryParams.Length - 1, 1);

			return QueryParams.ToString();
		}
		// }}}

		private static void SetPostVars(ref HttpWebRequest myHttpWebRequest, NameValueCollection PostData)
		{
			string postData = GenerateQueryString(PostData, true);

			ASCIIEncoding encoding=new ASCIIEncoding();
			byte[]  byte1=encoding.GetBytes(postData);
			// Set the content type of the data being posted.
			myHttpWebRequest.ContentType="application/x-www-form-urlencoded";

			// Set the content length of the string being posted.
			myHttpWebRequest.ContentLength = postData.Length;
			Stream newStream = myHttpWebRequest.GetRequestStream();
			newStream.Write(byte1, 0, byte1.Length);

			// Close the Stream object.
			newStream.Close();
		}
		private static string PostPage(string ConnectURL, NameValueCollection PostData, WebProxy Proxy, NameValueCollection Cookies, NetworkCredential AuthCredentials, string UserAgent)
		{
			return PostPage(ConnectURL, PostData, Proxy, Cookies, AuthCredentials, UserAgent, false);
		}
		private static string PostPage(string ConnectURL, NameValueCollection PostData, WebProxy Proxy, NameValueCollection Cookies, NetworkCredential AuthCredentials, string UserAgent, bool SlowProtocol)
		{
            RemoteCertificateValidationCallback instance = new RemoteCertificateValidationCallback(DummySslCheck);
            ServicePointManager.ServerCertificateValidationCallback = instance;
            HttpWebRequest TestPost = (HttpWebRequest)WebRequest.Create(ConnectURL);
			if (Proxy != null) TestPost.Proxy = Proxy;

			TestPost.Method = "POST";

			if (SlowProtocol) TestPost.ProtocolVersion = HttpVersion.Version10; 
			if (Proxy != null) TestPost.Proxy = Proxy;

			TestPost.UserAgent = UserAgent;

			if (Cookies != null)
			{
				foreach (string CookieName in Cookies.Keys)
				{
					if (TestPost.CookieContainer == null)
					{
						TestPost.CookieContainer = new CookieContainer();
					}
					TestPost.CookieContainer.Add(new Cookie(CookieName, Cookies[CookieName], "/", ConnectURL.Split('/')[2].Split(':')[0]));
				}
			}

			if (AuthCredentials != null) TestPost.Credentials = AuthCredentials;

#if DISPLAY_CONNECTS		
				UserStatus(ConnectURL); //HACK			
#endif

			HttpWebResponse resp;
			resp = null;	

			SetPostVars(ref TestPost, PostData);

			try
			{	resp = (HttpWebResponse)TestPost.GetResponse(); }
			catch (WebException wex)
			{
				if(wex.Status == WebExceptionStatus.ReceiveFailure)
				{
					// Mono seems to choke when used over wifi, then idled.. if this error takes place,
					// drop down to HTTP/1.0 for the single renegotiate connect.. (standard 1.0 is generally too slow though)
					return PostPage(ConnectURL, PostData, Proxy, Cookies, AuthCredentials, UserAgent, true);
				}
				else 
				{
					//resp = (HttpWebResponse)wex.Response;
					//ParentOutput(wex.ToString());
					throw(wex);
				}
			}

			// Get the stream associated with the response.
			Stream receiveStream = resp.GetResponseStream();

			// Pipes the stream to a higher level stream reader with the required encoding format. 
			StreamReader readStream = new StreamReader (receiveStream, Encoding.UTF8);

			string retVal = readStream.ReadToEnd();
			resp.Close ();
			readStream.Close ();

			return retVal;
		}
	}
}
 
