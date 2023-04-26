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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using VisualSOS.Abstractions.Logic;
using VisualSOS.Common;
using VisualSOS.Core.Commands;
using VisualSOS.Core.Infrastructure;
using VisualSOS.Core.Model;
using VisualSOS.Core.ViewModels;

namespace VisualSOS.UI.ViewModel {
    public class CombinedViewModel : ParentViewModel {
        /// <summary>
        /// Gets or sets the refresh managed apps command.
        /// </summary>
        /// <value>
        /// The refresh managed apps command.
        /// </value>
        public CommandBase RefreshManagedAppsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the attach or detach command.
        /// </summary>
        /// <value>
        /// The attach or detach command.
        /// </value>
        public CommandBase AttachOrDetachCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the run sos command.
        /// </summary>
        /// <value>
        /// The run sos command.
        /// </value>
        public CommandBase RunSosCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the double click grid command.
        /// </summary>
        /// <value>
        /// The double click grid command.
        /// </value>
        public CommandBase DoubleClickGridCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the automatic scroll output command.
        /// </summary>
        /// <value>
        /// The automatic scroll output command.
        /// </value>
        public CommandBase AutoScrollOutputCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the managed apps vm.
        /// </summary>
        /// <value>
        /// The managed apps vm.
        /// </value>
        public ManagedAppViewModel ManagedAppsVm {
            get; set;
        }

        /// <summary>
        /// Gets or sets the debuggee.
        /// </summary>
        /// <value>
        /// The debuggee.
        /// </value>
        public ManagedApp Debuggee {
            get; set;
        }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>The status.</value>
		public DebuggerStatus Status {
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether this instance can attach.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can attach; otherwise, <c>false</c>.
		/// </value>
		public bool CanAttach => IsInitialized && !ManagedAppsVm.Repository.SosManager.IsCurrentlyAttached;

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized => ManagedAppsVm.Repository.SosManager.Initialized;

        /// <summary>
        /// Gets a value indicating whether this instance is debugging.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is debugging; otherwise, <c>false</c>.
        /// </value>
        public bool IsDebugging => IsInitialized && Debuggee != null && ManagedAppsVm.Repository.SosManager.IsCurrentlyAttached;

		/// <summary>
		/// Initializes a new instance of the <see cref="CombinedViewModel"/> class.
		/// </summary>
		public CombinedViewModel() {
			Status = new DebuggerStatus();
			ManagedAppsVm.Container = this;
			RunSosCommand = new CommandBase(RunSosCommand_Handler);
			OutputMarshalling.Current.PinFunctor(UpdateOutputWindow);
			AttachOrDetachCommand = new CommandBase(AttachOrDetachCommand_Handler);
			DoubleClickGridCommand = new CommandBase(DoubleClickGridCommand_Handler);
			AutoScrollOutputCommand = new CommandBase(AutoScrollOutputCommand_Handler);
			RefreshManagedAppsCommand = new CommandBase(RefreshManagedAppCommand_Handler);
		}

		/// <summary>
		/// Refreshes the managed application command handler.
		/// </summary>
		/// <param name="sender">The sender.</param>
		private async void RefreshManagedAppCommand_Handler(object sender) {
            VisualStateManager.GoToElementState(Instance, "_Loading", true);
            var r = await ManagedAppsVm.Repository.SosManager.GetManagedApps();
            ManagedAppsVm.Data = new ObservableCollection<ManagedApp>((List<ManagedApp>)r.Tag);
            VisualStateManager.GoToElementState(Instance, "_Loaded", true);
            Dispatcher.CurrentDispatcher.Invoke(() => Status.UpdateValues(ManagedAppsVm.Data.Count, Debuggee));
        }

        /// <summary>
        /// Runs the sos command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void RunSosCommand_Handler(object sender) {
            var cmd = sender is SosCommand ? SosCommands.Current[(SosCommand)sender] : sender?.ToString();

            if (IsDebugging) {
                ManagedAppsVm.Repository.SosManager.RunCommand(cmd, true);
            } else {
                MessageBox.Show("Unable to run command. Please, attach to running program and try again.",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// Automatics the scroll output command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void AutoScrollOutputCommand_Handler(object sender) {
            ((TextBox)sender).Focus();
            ((TextBox)sender).CaretIndex = ((TextBox)sender).Text.Length;
            ((TextBox)sender).ScrollToEnd();
        }

        /// <summary>
        /// Doubles the click grid command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void DoubleClickGridCommand_Handler(object sender) {
            AttachOrDetachDebuggingEngine(ManagedAppsVm.DataFields);
        }

        /// <summary>
        /// rs the attach or detach command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void AttachOrDetachCommand_Handler(object sender) {
            AttachOrDetachDebuggingEngine((ManagedApp)sender);
		}

		/// <summary>
		/// Attaches the or detach debugging engine.
		/// </summary>
		/// <param name="selected">The selected.</param>
		private void AttachOrDetachDebuggingEngine(ManagedApp selected) {
			if (IsInitialized && selected != null) {
				if (selected.Pid == Debuggee?.Pid) {
					if (MessageBox.Show($"{selected.ImageName} (Pid:{selected.Pid}) is already being debugged, do you want to detach it?",
						"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question).Equals(MessageBoxResult.Yes)) {
						Debuggee = null;
						ManagedAppsVm.Repository.SosManager.AttachOrDetach(DebuggerBehavior.Detach, selected.Pid);
						RefreshManagedAppCommand_Handler(null);
					}
				} else {
					if (MessageBox.Show($"Do you want to attach debugger to {selected.ImageName} (Pid:{selected.Pid})?",
							"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)
						.Equals(MessageBoxResult.Yes)) {
						if (Debuggee != null) // Let's detach before attaching newly selected process
							ManagedAppsVm.Repository.SosManager.AttachOrDetach(DebuggerBehavior.Detach, Debuggee.Pid);
						Debuggee = selected;
						ManagedAppsVm.Repository.SosManager.AttachOrDetach(DebuggerBehavior.Attach, selected.Pid);
					};
				}
				Dispatcher.CurrentDispatcher.Invoke(() => Status.UpdateValues(ManagedAppsVm.Data.Count, Debuggee));
			}
		}

		/// <summary>
		/// Updates the output window.
		/// </summary>
		/// <param name="message">The message.</param>
		private void UpdateOutputWindow(string message) {
			Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => ManagedAppsVm.OutPut += message));
		}
	}
}