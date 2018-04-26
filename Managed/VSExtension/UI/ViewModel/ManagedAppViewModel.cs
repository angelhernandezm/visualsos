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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using VisualSOS.Abstractions.Logic;
using VisualSOS.Common;
using VisualSOS.Core.Commands;
using VisualSOS.Core.Model;
using VisualSOS.Core.ViewModels;

namespace VisualSOS.UI.ViewModel {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ManagedApp" />
    public class ManagedAppViewModel : ViewModelBase<ManagedApp, IManagedAppRepository<ManagedApp>> {
        /// <summary>
        /// The commands
        /// </summary>
        private Stack<string> _commands;

        /// <summary>
        /// The command
        /// </summary>
        private string _command;

        /// <summary>
        /// The output
        /// </summary>
        private string _output;

        /// <summary>
        /// Gets or sets the out put.
        /// </summary>
        /// <value>
        /// The out put.
        /// </value>
        public string OutPut {
            get => _output;
            set {
                _output = value;
                RaisePropertyChanged("Output");
            }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public string Command {
            get => _command;

            set {
                _command = value;
                RaisePropertyChanged("Command");
            }
        }

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        public Stack<string> Commands {
            get => _commands;

            set {
                _commands = value;
                RaisePropertyChanged("Commands");
            }
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public CombinedViewModel Container {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view managed threads command.
        /// </summary>
        /// <value>
        /// The view managed threads command.
        /// </value>
        public CommandBase ViewManagedThreadsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view managed call stack command.
        /// </summary>
        /// <value>
        /// The view managed call stack command.
        /// </value>
        public CommandBase ViewManagedCallStackCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view combined stack command.
        /// </summary>
        /// <value>
        /// The view combined stack command.
        /// </value>
        public CommandBase ViewCombinedStackCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show ee version command.
        /// </summary>
        /// <value>
        /// The show ee version command.
        /// </value>
        public CommandBase ShowEeVersionCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view function call arguments command.
        /// </summary>
        /// <value>
        /// The view function call arguments command.
        /// </value>
        public CommandBase ViewFunctionCallArgsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view local variables command.
        /// </summary>
        /// <value>
        /// The view local variables command.
        /// </value>
        public CommandBase ViewLocalVariablesCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view memory consumption by type command.
        /// </summary>
        /// <value>
        /// The view memory consumption by type command.
        /// </value>
        public CommandBase ViewMemoryConsumptionByTypeCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view most recent exception command.
        /// </summary>
        /// <value>
        /// The view most recent exception command.
        /// </value>
        public CommandBase ViewMostRecentExceptionCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view un managed stack command.
        /// </summary>
        /// <value>
        /// The view un managed stack command.
        /// </value>
        public CommandBase ViewUnManagedStackCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the view cpu consumption command.
        /// </summary>
        /// <value>
        /// The view cpu consumption command.
        /// </value>
        public CommandBase ViewCpuConsumptionCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the list unmanaged threads command.
        /// </summary>
        /// <value>
        /// The list unmanaged threads command.
        /// </value>
        public CommandBase ListUnmanagedThreadsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the list loaded modules command.
        /// </summary>
        /// <value>
        /// The list loaded modules command.
        /// </value>
        public CommandBase ListLoadedModulesCommand {
            get; set;
        }


        /// <summary>
        /// Gets or sets the call stack for all threads command.
        /// </summary>
        /// <value>
        /// The call stack for all threads command.
        /// </value>
        public CommandBase CallStackForAllThreadsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the dump domain command.
        /// </summary>
        /// <value>
        /// The dump domain command.
        /// </value>
        public CommandBase DumpDomainCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the call stack with method descriptor command.
        /// </summary>
        /// <value>
        /// The call stack with method descriptor command.
        /// </value>
        public CommandBase CallStackWithMethodDescriptorCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show gc heaps command.
        /// </summary>
        /// <value>
        /// The show gc heaps command.
        /// </value>
        public CommandBase ShowGcHeapsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show finalize queue command.
        /// </summary>
        /// <value>
        /// The show finalize queue command.
        /// </value>
        public CommandBase ShowFinalizeQueueCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the dump managed stack trace in all threads command.
        /// </summary>
        /// <value>
        /// The dump managed stack trace in all threads command.
        /// </value>
        public CommandBase DumpManagedStackTraceInAllThreadsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the analyze exception command.
        /// </summary>
        /// <value>
        /// The analyze exception command.
        /// </value>
        public CommandBase AnalyzeExceptionCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the gc handles leak command.
        /// </summary>
        /// <value>
        /// The gc handles leak command.
        /// </value>
        public CommandBase GcHandlesLeakCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the vm map command.
        /// </summary>
        /// <value>
        /// The vm map command.
        /// </value>
        public CommandBase VmMapCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the vm stat command.
        /// </summary>
        /// <value>
        /// The vm stat command.
        /// </value>
        public CommandBase VmStatCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show memory summary command.
        /// </summary>
        /// <value>
        /// The show memory summary command.
        /// </value>
        public CommandBase ShowMemorySummaryCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show all heaps command.
        /// </summary>
        /// <value>
        /// The show all heaps command.
        /// </value>
        public CommandBase ShowAllHeapsCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the show all locks command.
        /// </summary>
        /// <value>
        /// The show all locks command.
        /// </value>
        public CommandBase ShowAllLocksCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the clear output command.
        /// </summary>
        /// <value>
        /// The clear output command.
        /// </value>
        public CommandBase ClearOutputCommand {
            get; set;
        }

        /// <summary>
        /// Gets or sets the save output command.
        /// </summary>
        /// <value>
        /// The save output command.
        /// </value>
        public CommandBase SaveOutputCommand {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedAppViewModel"/> class.
        /// </summary>
        public ManagedAppViewModel() {
            _commands = new Stack<string>();
            ListLoadedModulesCommand = new CommandBase(ListLoadedModulesCommand_Handler);
            ViewCombinedStackCommand = new CommandBase(ViewCombinedStackCommand_Handler);
            ViewUnManagedStackCommand = new CommandBase(ViewUnManagedStackCommand_Handler);
            ViewCpuConsumptionCommand = new CommandBase(ViewCpuConsumptionCommand_Handler);
            ViewManagedThreadsCommand = new CommandBase(ViewManagedThreadsCommand_Handler);
            ViewLocalVariablesCommand = new CommandBase(ViewLocalVariablesCommand_Handler);
            ViewManagedCallStackCommand = new CommandBase(ViewManagedCallStackCommand_Handler);
            ViewFunctionCallArgsCommand = new CommandBase(ViewFunctionCallArgsCommand_Handler);
            ListUnmanagedThreadsCommand = new CommandBase(ListUnmanagedThreadsCommand_Handler);
            ViewMostRecentExceptionCommand = new CommandBase(ViewMostRecentExceptionCommand_Handler);
            ViewMemoryConsumptionByTypeCommand = new CommandBase(ViewMemoryConsumptionByTypeCommand_Handler);
            CallStackForAllThreadsCommand = new CommandBase(CallStackForAllThreadsCommand_Handler);
            DumpDomainCommand = new CommandBase(DumpDomainCommand_Handler);
            CallStackWithMethodDescriptorCommand = new CommandBase(CallStackWithMethodDescriptorCommand_Handler);
            ShowEeVersionCommand = new CommandBase(ShowEeVersionCommand_Handler);
            ShowGcHeapsCommand = new CommandBase(ShowGcHeapsCommand_Handler);
            ShowFinalizeQueueCommand = new CommandBase(ShowFinalizeQueueCommand_Handler);
            DumpManagedStackTraceInAllThreadsCommand = new CommandBase(DumpManagedStackTraceInAllThreadsCommand_Handler);
            AnalyzeExceptionCommand = new CommandBase(AnalyzeExceptionCommand_Handler);
            GcHandlesLeakCommand = new CommandBase(GcHandlesLeakCommand_Handler);
            VmMapCommand = new CommandBase(VmMapCommand_Handler);
            VmStatCommand = new CommandBase(VmStatCommand_Handler);
            ShowMemorySummaryCommand = new CommandBase(ShowMemorySummaryCommand_Handler);
            ShowAllHeapsCommand = new CommandBase(ShowAllHeapsCommand_Handler);
            ShowAllLocksCommand = new CommandBase(ShowAllLocksCommand_Handler);
            ClearOutputCommand = new CommandBase(ClearOutputCommand_Handler);
            SaveOutputCommand = new CommandBase(SaveOutputCommand_Handler);
        }

        /// <summary>
        /// Views the managed threads command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewManagedThreadsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ListManagedThreads);
        }

        /// <summary>
        /// Clears the output command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ClearOutputCommand_Handler(object sender) {
            if (Container.IsDebugging) {
                Repository.SosManager.RunCommand("!HistClear", true);
                Repository.SosManager.RunCommand("!SOSFlush", true);
            }
            OutPut = string.Empty;
            GC.Collect();
        }

        /// <summary>
        /// Saves the output command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void SaveOutputCommand_Handler(object sender) {
            SaveFileDialog saveDlg;

            if (Container.IsDebugging && !string.IsNullOrEmpty(OutPut)) {
                if ((saveDlg = new SaveFileDialog {
                    Title = "Select location to save VisualSOS output..."
                }).ShowDialog((Window)Instance).Value) {
                    if (!string.IsNullOrEmpty(saveDlg.FileName)) {
                        Task.Run(async () => {
                            var buffer = Encoding.ASCII.GetBytes(OutPut);
                            using (var fs = new FileStream(saveDlg.FileName, FileMode.OpenOrCreate,
                                FileAccess.Write, FileShare.None, buffer.Length, true)) {
                                await fs.WriteAsync(buffer, 0, buffer.Length);
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Shows all locks command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowAllLocksCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowLocks);
        }

        /// <summary>
        /// Shows all heaps command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowAllHeapsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowAllHeaps);
        }

        /// <summary>
        /// Shows the memory summary command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowMemorySummaryCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowMemorySummary);
        }

        /// <summary>
        /// Analyzes the exception command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void AnalyzeExceptionCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.Analyze);
        }

        /// <summary>
        /// Gcs the handles leak command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void GcHandlesLeakCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.GcHandlesLeak);
        }

        /// <summary>
        /// Vms the map command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void VmMapCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.VmMap);
        }

        /// <summary>
        /// Vms the stat command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void VmStatCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.VmStat);
        }

        /// <summary>
        /// Dumps the managed stack trace in all threads command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void DumpManagedStackTraceInAllThreadsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.DumpManagedStackTraceInAllThreads);
        }

        /// <summary>
        /// Shows the finalize queue command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowFinalizeQueueCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.FinalizeQueue);
        }

        /// <summary>
        /// Calls the stack with method descriptor command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void CallStackWithMethodDescriptorCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.CallStackWithMethodDescriptor);
        }

        /// <summary>
        /// Shows the ee version command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowEeVersionCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowEeVersion);
        }


        /// <summary>
        /// Shows the gc heaps command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ShowGcHeapsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowGcHeaps);
        }

        /// <summary>
        /// Dumps the domain command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void DumpDomainCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.DumpDomain);
        }

        /// <summary>
        /// Calls the stack for all threads command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void CallStackForAllThreadsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.CallStackForAllThreads);
        }

        /// <summary>
        /// Views the managed call stack command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewManagedCallStackCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ViewManagedStack);
        }

        /// <summary>
        /// Views the combined stack command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewCombinedStackCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ViewCombinedStack);
        }

        /// <summary>
        /// Views the function call arguments command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewFunctionCallArgsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ViewFunctionCallArgs);
        }


        /// <summary>
        /// Views the memory consumption by type command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewMemoryConsumptionByTypeCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowMemoryConsumptionByType);
        }

        /// <summary>
        /// Views the most recent exception command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewMostRecentExceptionCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ShowMostRecentException);
        }

        /// <summary>
        /// Views the un managed stack command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewUnManagedStackCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ViewNativeStack);
        }


        /// <summary>
        /// Views the cpu consumption command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewCpuConsumptionCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ListCpuConsumption);
        }

        /// <summary>
        /// Lists the unmanaged threads command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ListUnmanagedThreadsCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ListNativeThreads);
        }

        /// <summary>
        /// Lists the loaded modules command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ListLoadedModulesCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ListLoadedModules);
        }

        /// <summary>
        /// Views the local variables command handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void ViewLocalVariablesCommand_Handler(object sender) {
            SendCommandToSos(SosCommand.ViewLocalVariables);
        }


        /// <summary>
        /// Sends the command to sos.
        /// </summary>
        /// <param name="command">The command.</param>
        private void SendCommandToSos(object command) {
            Container.RunSosCommand_Handler(command);
        }
    }
}