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

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Threading.Tasks;

namespace VisualSOS.Extension.Logic {
    public static class Extensions {
        /// <summary>
        /// Shows the specified check for visual sos instance.
        /// </summary>
        /// <param name="windowFrame">The window frame.</param>
        public static void Show(this IVsWindowFrame windowFrame) {
            if (windowFrame != null) {
                Task.Run(() => ErrorHandler.ThrowOnFailure(windowFrame.Show()));
            }
        }
    }
}
