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
using System.Runtime.InteropServices;
using System.Threading;
using VisualSOS.Abstractions.Common;

namespace VisualSOS.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="VisualSOS.Abstractions.Common.IOutputMarshalling" />
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [ComSourceInterfaces(typeof(IOutputMarshalling))]
    [Guid("23BCFADD-2904-47D3-BAC1-3423C3524F31")]
    public class OutputMarshalling : IOutputMarshalling
    {
        /// <summary>
        /// The m instance
        /// </summary>
        private static OutputMarshalling m_instance;

        /// <summary>
        /// The output message functor
        /// </summary>
        private static Action<string> m_outputMessageFunctor;

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public static OutputMarshalling Current {
            get {
                if (m_instance == null) {
                    lock (new object()) {
                        m_instance = new OutputMarshalling();
                    }
                }
                return m_instance;
            }
        }

        /// <summary>
        /// Redirects the output.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void RedirectOutput(string message) {
            if (!string.IsNullOrEmpty(message))
                (new Thread(() => m_outputMessageFunctor?.Invoke(message))).Start();
        }

        /// <summary>
        /// Pins the functor.
        /// </summary>
        /// <param name="action">The action.</param>
        public void PinFunctor(Action<string> action) {
            if (action != null) {
                m_outputMessageFunctor = action;
            } else
                throw new NullReferenceException(nameof(action));
        }
    }
}