// Copyright (C) 2023 Angel Hernandez Matos
// You can redistribute this software and/or modify it under the terms of the 
// GNU General Public License  (GPL).  This program is distributed in the hope 
// that it will be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See License.txt for more details. 

/* Visual C# compiler   : Microsoft (R) Visual C# Compiler version 4.5.2-3.23171.7 (d17f7415)
Creation date           : 24/04/2023
Developer               : Angel Hernandez Matos
e-m@il                  : me@angelhernandezm.com
Website                 : http://www.angelhernandezm.com
*/

using VisualSOS.Common.Entities;

namespace VisualSOS.Core.Model {
	public class DebuggerStatus : BaseEntity {
		/// <summary>
		/// The apps using color
		/// </summary>
		private string _appsUsingClr;

		/// <summary>
		/// The debugger state
		/// </summary>
		private string _debuggerState;

		/// <summary>
		/// Gets or sets the color of the apps using.
		/// </summary>
		/// <value>The color of the apps using.</value>
		public string AppsUsingClr {
			get => _appsUsingClr;
			set {
				_appsUsingClr = value;
				RaisePropertyChanged("AppsUsingClr");
			}
		}

		/// <summary>
		/// Gets or sets the state of the debugger.
		/// </summary>
		/// <value>The state of the debugger.</value>
		public string DebuggerState {
			get => _debuggerState;
			set {
				_debuggerState = value;
				RaisePropertyChanged("DebuggerState");
			}
		}

		/// <summary>
		/// Updates the values.
		/// </summary>
		/// <param name="clrEnabledApps">The color enabled apps.</param>
		/// <param name="debuggee">The debuggee.</param>
		public void UpdateValues(int clrEnabledApps, ManagedApp debuggee) {
			DebuggerState = debuggee != null ? $"Attached to '{debuggee?.ImageName}'" : "No attached process";
			AppsUsingClr = clrEnabledApps == 0 ? "There aren't any apps using the CLR now." : $"There are {clrEnabledApps} apps using the CLR now.";
		}
	}
}
