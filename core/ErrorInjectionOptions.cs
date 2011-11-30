//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: ErrorInjectionOptions.cs,v 1.4 2005/08/29 17:16:59 nummish Exp $
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

namespace Absinthe.Core
{
	/// <summary>Options specific to error based injections</summary>
	public class ErrorInjectionOptions : InjectionOptions
	{
		/// <summary>Instantiates a new InjectionOption class</summary>
		public ErrorInjectionOptions() : base()
		{
			 
		}

		/// <summary>Indicates whether the plugin should be used to identify if it is the correct one</summary>
		public bool VerifyVersion
		{
			get
			{
				return _VerifyVersion;
			}
			set
			{
				_VerifyVersion = value;
			}
		} private bool _VerifyVersion;
	}
}
