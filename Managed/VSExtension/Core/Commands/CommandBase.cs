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
using System.Windows.Input;

namespace VisualSOS.Core.Commands {
    public class CommandBase : ICommand {
		#region "Events"

		/// <summary>
		/// Occurs when changes occur that affect whether or not the command should execute.
		/// </summary>
		public event EventHandler CanExecuteChanged;
		/// <summary>
		/// Occurs when [can execute requested].
		/// </summary>
		public event EventHandler<CanExecuteRequestedEventArgs> CanExecuteRequested;
		/// <summary>
		/// Occurs when [execute requested].
		/// </summary>
		public event EventHandler<ExecuteRequestedEventArgs> ExecuteRequested;

		#endregion

		#region "Members"

		private bool _canExecuteStatus = true;

		#endregion

		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseCommand"/> class.
		/// </summary>
		/// <param name="targetAction">The _target action.</param>
		public CommandBase(Action<object> targetAction)
			: this(targetAction, DefaultCanExecute) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandBase" /> class.
		/// </summary>
		/// <param name="targetAction">The target action.</param>
		/// <param name="canExecute">The can execute.</param>
		public CommandBase(Action<object> targetAction, Predicate<object> canExecute) {
			TargetAction = targetAction ?? throw new ArgumentNullException(nameof(targetAction));
			IsExecutable = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
		}

		#endregion

		#region "Properties"

		/// <summary>
		/// Gets or sets the is executable.
		/// </summary>
		/// <value>The is executable.</value>
		private Predicate<object> IsExecutable {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the target action.
		/// </summary>
		/// <value>The target action.</value>
		private Action<object> TargetAction {
			get;
			set;
		}


		#endregion

		#region "Public Methods"

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
		public bool CanExecute(object parameter) {
			if (CanExecuteRequested == null) {
				return _canExecuteStatus;
			}

			var args = new CanExecuteRequestedEventArgs {
				Parameter = parameter,
				CommandParent = TargetAction
			};

			CanExecuteRequested(parameter, args);

			return args.CanExecute;
		}

		/// <summary>
		/// Updates the can execute status.
		/// </summary>
		/// <param name="newCanExecuteStatus">if set to <c>true</c> [new can execute status].</param>
		public void UpdateCanExecuteStatus(bool newCanExecuteStatus) {
			_canExecuteStatus = newCanExecuteStatus;
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public void Execute(object parameter) {
			var args = new ExecuteRequestedEventArgs {
				Parameter = parameter,
				CommandParent = TargetAction
			};

			TargetAction(parameter);

			ExecuteRequested?.Invoke(parameter, args);
		}

		#endregion

		/// <summary>
		/// Defaults the can execute.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>System.Boolean.</returns>
		private static bool DefaultCanExecute(object parameter) {
			return true;
		}
	}


	/// <summary>
	/// Class CanExecuteRequestedEventArgs.
	/// </summary>
	public class CanExecuteRequestedEventArgs : EventArgs {
		/// <summary>
		/// Gets or sets the parameter.
		/// </summary>
		/// <value>
		/// The parameter.
		/// </value>
		public object Parameter {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the command parent.
		/// </summary>
		/// <value>
		/// The command parent.
		/// </value>
		public object CommandParent {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance can execute.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
		/// </value>
		public bool CanExecute {
			get;
			set;
		}
	}

	/// <summary>
	/// Class ExecuteRequestedEventArgs.
	/// </summary>
	/// <seealso cref="System.EventArgs" />
	public class ExecuteRequestedEventArgs : EventArgs {
		/// <summary>
		/// Gets or sets the parameter.
		/// </summary>
		/// <value>
		/// The parameter.
		/// </value>
		public object Parameter {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the command parent.
		/// </summary>
		/// <value>
		/// The command parent.
		/// </value>
		public object CommandParent {
			get;
			set;
		}
	}
}
