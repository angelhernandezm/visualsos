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


using System.Collections.Generic;

namespace VisualSOS.Common {
    /// <summary>
    /// 
    /// </summary>
    public enum SosCommand {
        ListLoadedModules,
        ViewCombinedStack,
        ViewNativeStack,
        ListCpuConsumption,
        ListManagedThreads,
        ViewLocalVariables,
        ViewManagedStack,
        ViewFunctionCallArgs,
        ListNativeThreads,
        ShowMostRecentException,
        ShowMemoryConsumptionByType,
        CallStackForAllThreads,
        ShowEeVersion,
        DumpDomain,
        CallStackWithMethodDescriptor,
        ShowGcHeaps,
        FinalizeQueue,
        DumpManagedStackTraceInAllThreads,
        Analyze,
        GcHandlesLeak,
        VmMap,
        VmStat,
        ShowMemorySummary,
        ShowAllHeaps,
        ShowLocks
    }
    /// <summary>
    /// 
    /// </summary>
    public class SosCommands {
        /// <summary>
        /// The m commands
        /// </summary>
        private readonly Dictionary<SosCommand, string> m_Commands = new Dictionary<SosCommand, string>();

        /// <summary>
        /// The m instance
        /// </summary>
        private static SosCommands m_instance;

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static SosCommands Current {
            get {
                lock (new object()) {
                    if (m_instance == null)
                        m_instance = new SosCommands();
                }

                return m_instance;
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="SosCommands"/> class.
        /// </summary>
        protected SosCommands() {
            m_Commands.Add(SosCommand.ListLoadedModules, "lmf");
            m_Commands.Add(SosCommand.ViewCombinedStack, "!dumpstack");
            m_Commands.Add(SosCommand.ViewNativeStack, "k");
            m_Commands.Add(SosCommand.ListCpuConsumption, "!runaway");
            m_Commands.Add(SosCommand.ListManagedThreads, "!threads");
            m_Commands.Add(SosCommand.ViewLocalVariables, "!clrstack -l");
            m_Commands.Add(SosCommand.ViewManagedStack, "!clrstack");
            m_Commands.Add(SosCommand.ViewFunctionCallArgs, "!clrstack -p");
            m_Commands.Add(SosCommand.ListNativeThreads, "~");
            m_Commands.Add(SosCommand.ShowMostRecentException, "!PrintException");
            m_Commands.Add(SosCommand.ShowMemoryConsumptionByType, "!dumpheap -stat");
            m_Commands.Add(SosCommand.CallStackForAllThreads, "~* k");
            m_Commands.Add(SosCommand.ShowEeVersion, "!EEVersion");
            m_Commands.Add(SosCommand.DumpDomain, "!DumpDomain");
            m_Commands.Add(SosCommand.CallStackWithMethodDescriptor, "!DumpStack -EE");
            m_Commands.Add(SosCommand.ShowGcHeaps, "!EEHeap - gc");
            m_Commands.Add(SosCommand.FinalizeQueue, "!FinalizeQueue");
            m_Commands.Add(SosCommand.DumpManagedStackTraceInAllThreads, "!EEStack -short");
            m_Commands.Add(SosCommand.Analyze, "!analyze -v");
            m_Commands.Add(SosCommand.GcHandlesLeak, "!GCHandleLeaks");
            m_Commands.Add(SosCommand.VmMap, "!VMMap");
            m_Commands.Add(SosCommand.VmStat, "!VMStat");
            m_Commands.Add(SosCommand.ShowMemorySummary, "!Address -summary");
            m_Commands.Add(SosCommand.ShowAllHeaps, "!heap -s");
            m_Commands.Add(SosCommand.ShowLocks, "!locks");
        }

        /// <summary>
        /// Gets the <see cref="System.String"/> with the specified command.
        /// </summary>
        /// <value>
        /// The <see cref="System.String"/>.
        /// </value>
        /// <param name="cmd">The command.</param>
        /// <returns></returns>
        public string this[SosCommand cmd] => m_Commands.ContainsKey(cmd) ? m_Commands[cmd] : string.Empty;
    }
}
