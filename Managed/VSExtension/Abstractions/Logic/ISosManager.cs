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

using System.Threading.Tasks;

namespace VisualSOS.Abstractions.Logic {
    /// <summary>
    /// 
    /// </summary>
    public enum DebuggerBehavior {
		/// <summary>
		/// The attach
		/// </summary>
		Attach,
		/// <summary>
		/// The detach
		/// </summary>
		Detach
	}

	/// <summary>
	/// 
	/// </summary>
	public enum DebuggingEngineOption {
		Start,
		Stop
	}

	/// <summary>
	/// 
	/// </summary>
	public interface ISosManager {
		/// <summary>
		/// Attaches the or detach.
		/// </summary>
		/// <param name="behavior">The behavior.</param>
		/// <param name="pid">The pid.</param>
		/// <returns></returns>
		ExecutionResult AttachOrDetach(DebuggerBehavior behavior, int pid);

        /// <summary>
        /// Loads the symbols.
        /// </summary>
        /// <param name="debugeePath">The debugee path.</param>
        /// <returns></returns>
        ExecutionResult LoadSymbols(string debugeePath);

		/// <summary>
		/// Runs the command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="privateExecution">if set to <c>true</c> [private execution].</param>
		/// <returns></returns>
		ExecutionResult RunCommand(string command, bool privateExecution);

		/// <summary>
		/// Gets the managed apps.
		/// </summary>
		/// <returns></returns>
		Task<ExecutionResult> GetManagedApps();

		/// <summary>
		/// Gets a value indicating whether this instance is currently attached.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is currently attached; otherwise, <c>false</c>.
		/// </value>
		bool IsCurrentlyAttached {
			get;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ISosManager"/> is initialized.
		/// </summary>
		/// <value>
		///   <c>true</c> if initialized; otherwise, <c>false</c>.
		/// </value>
		bool Initialized {
			get;
		}
	}
}
