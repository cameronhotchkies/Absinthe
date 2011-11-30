//   Absinthe Core - The Automated SQL Injection Library
//   This software is Copyright (C) 2004,2005  nummish, 0x90.org
//   $Id: BlindInjectionOptions.cs,v 1.4 2005/08/29 17:16:59 nummish Exp $
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
	/// <summary>
	/// This class represents all of the options required by a Blind SQL injection.
	/// </summary>
	public class BlindInjectionOptions : InjectionOptions
	{
		/// <summary>
		/// Just a simple constructor.
		/// </summary>
		public BlindInjectionOptions() : base()
		{
			_Tolerance = 0F; // Default tolerance should be zero, this is the source of most people's problems
			_Delimiter = Convert.ToChar(0x0a).ToString();
			_Throttle = 0;
		}

		private float _Tolerance;
		private string _Delimiter;
		private int _Throttle;
		
		// {{{ Delimiter Property
		/// <summary>
		/// The delimiter to use when generating signatures. By default, this is the carriage return 
		/// character (0x0a).
		/// </summary>
		public string Delimiter
		{
			get
			{
				return _Delimiter;
			}
			set
			{
				if (value.Length == 0)
				{
					_Delimiter = Environment.NewLine;
				}
				else
				{
					_Delimiter = value;
				}
			}
		}
		// }}}

		// {{{ Throttle Property
		/// <summary>
		/// The amount to either speed up or slow down the injection. A negative number will speed
		/// up the injection by threading requests. A positive number will add a delay of that many 
		/// seconds between requests.
		/// </summary>
		public int Throttle
		{
			get
			{
				return _Throttle;
			}
			set
			{
				_Throttle = value;
			}
		}
		// }}}

		// {{{ Tolerance Property
		/// <summary>
		/// The tolerance to use when comparing signatures. This is a decimal value, not a percentage.
		/// Anything higher than 1 will be treated as 100%. The default value is 0.01.
		/// </summary>
		public float Tolerance
		{
			get
			{
				return _Tolerance;
			}

			set
			{
				if (value > 0)
				{
					_Tolerance = value;
				}
				else
				{
					_Tolerance = 0;
				}
			}
		}
		// }}}
	}
}
