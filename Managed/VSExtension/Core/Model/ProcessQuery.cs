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

using VisualSOS.Abstractions.Common;

namespace VisualSOS.Core.Model {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="int" />
    public class ProcessQuery : IRequest<int> {
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public int Value {
			get;
			set;
		}
	}
}