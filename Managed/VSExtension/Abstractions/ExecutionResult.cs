// Copyright (C) 2018 Angel Hernandez Matos
// You can redistribute this software and/or modify it under the terms of the 
// GNU General Public License  (GPL).  This program is distributed in the hope 
// that it will be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See License.txt for more details. 

/* Visual C# compiler   : Microsoft (R) Visual C# Compiler version 2.7.0.62707 (75dfc9b3)
Creation date           : 26/04/2018
Developer               : Angel Hernandez Matos
e-m@il                  : me@angelhernandezm.com
Website                 : http://www.angelhernandezm.com
*/

using System;

namespace VisualSOS.Abstractions {
    /// <summary>
    /// 
    /// </summary>
    public class ExecutionResult {
		/// <summary>
		/// Gets the empty.
		/// </summary>
		/// <value>
		/// The empty.
		/// </value>
		public static ExecutionResult Empty => new ExecutionResult();

		/// <summary>
		/// Gets or sets a value indicating whether this instance is success.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the last exception if any.
		/// </summary>
		/// <value>
		/// The last exception if any.
		/// </value>
		public Exception LastExceptionIfAny {
			get; set;
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>
		/// The context.
		/// </value>
		public string Context {
			get; set;
		}

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		public object Tag {
			get; set;
		}

	}
}
