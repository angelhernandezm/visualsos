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

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using VisualSOS.Extension.UI;
using Threading = System.Threading.Tasks;


namespace VisualSOS.Extension.Logic {

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("d7d01b4e-1c5c-437a-8663-332ffaf67456")]
    public class VisualSosToolWindow : ToolWindowPane, IVsWindowFrameNotify3 {
        /// <summary>
        /// The visual sos HWND
        /// </summary>
        private static IntPtr _visualSosHwnd;

        /// <summary>
        /// The self HWND
        /// </summary>
        private static IntPtr _selfHwnd;

        /// <summary>
        /// The title bar height
        /// </summary>
        private static int _titleBarHeight;

        /// <summary>
        /// Gets or sets the visual sos window handle.
        /// </summary>
        /// <value>
        /// The visual sos window handle.
        /// </value>
        public static IntPtr VisualSosWindowHandle {
            get => _visualSosHwnd;
            set {
                _visualSosHwnd = value;

                var x = Interop.GetVisualStudioWindows(Process.GetCurrentProcess().Id);

                foreach (var e in x.Keys) {
                    var wi = new Interop.WINDOWINFO();
                    Interop.GetWindowInfo(e, ref wi);

                    if (wi.dwStyle == Interop.VisualSosWindowStyle) {  // Visual SOS' Window style
                        _selfHwnd = e;
                        var rect = new Interop.RECT();
                        Interop.SetParent(_visualSosHwnd, e);
                        Interop.SetWindowLongPtr(_visualSosHwnd, Interop.GWL_STYLE, IntPtr.Zero);  //GWL_STYLE (-16)

                        _titleBarHeight = Interop.GetSystemMetrics(Interop.SystemMetric.SM_CYFRAME) +
                                             Interop.GetSystemMetrics(Interop.SystemMetric.SM_CYCAPTION) +
                                             Interop.GetSystemMetrics(Interop.SystemMetric.SM_CXPADDEDBORDER);

                        Interop.GetWindowRect(e, ref rect);
                        var height = Math.Ceiling((double)_titleBarHeight) - 10;
                        Interop.MoveWindow(_visualSosHwnd, 0, (int)height, rect.Width, rect.Height - 21, true);
                        Interop.ShowWindow(_visualSosHwnd, (int)Interop.CmdShow.SW_RESTORE);
                        Interop.UpdateWindow(e);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is visual sos window present.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visual sos window present; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisualSosWindowPresent => VisualSosWindowHandle != IntPtr.Zero || Interop.IsWindow(VisualSosWindowHandle);
        /// <summary>
        /// Gets the get current DTE.
        /// </summary>
        /// <value>
        /// The get current DTE.
        /// </value>
        public EnvDTE.DTE GetCurrentDte => (EnvDTE.DTE)VisualSosToolWindowCommand.ServiceProvider.GetService(typeof(EnvDTE.DTE));


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualSosToolWindow"/> class.
        /// </summary>
        public VisualSosToolWindow() : base(null) {
            Caption = "Visual SOS";
            Content = new Container();
        }

        /// <summary>
        /// Starts the visual sos.
        /// </summary>
        public static void StartVisualSos() {
            Threading.Task.Run(() => {
                Process existing;
                // If VisualSOS is running we'll return
                if ((existing = Process.GetProcessesByName("VisualSOS.UI").FirstOrDefault()) != null) {
                    VisualSosWindowHandle = existing.MainWindowHandle;
                } else {
                    var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var exePath = $@"{path}\VisualSOS.UI.exe";

                    using (var visualSos = new Process() {
                        StartInfo = new ProcessStartInfo(exePath) {
                            WindowStyle = ProcessWindowStyle.Minimized
                        },
                    }) {
                        visualSos.Start();

                        for (var nRetry = 0; nRetry < 5 || (VisualSosWindowHandle = visualSos.MainWindowHandle) == IntPtr.Zero; nRetry++)
                            Thread.Sleep(100);
                    }
                }
            });
        }


        #region "IVsWindowFrameNotify3 Implementation" 

        /// <summary>
        /// Notifies the VSPackage of a change in the window's display state.
        /// </summary>
        /// <param name="fShow">[in] Specifies the reason for the display state change. Value taken from the <see cref="T:Microsoft.VisualStudio.Shell.Interop.__FRAMESHOW" /> enumeration.</param>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        public int OnShow(int fShow) {
            if (fShow == 1 && !IsVisualSosWindowPresent)
                StartVisualSos();

            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        /// <summary>
        /// Notifies the VSPackage that a window is being moved.
        /// </summary>
        /// <param name="x">[in] New horizontal position.</param>
        /// <param name="y">[in] New vertical position.</param>
        /// <param name="w">[in] New window width.</param>
        /// <param name="h">[in] New window height.</param>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        public int OnMove(int x, int y, int w, int h) {
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        /// <summary>
        /// Notifies the VSPackage that a window is being resized.
        /// </summary>
        /// <param name="x">[in] New horizontal position.</param>
        /// <param name="y">[in] New vertical position.</param>
        /// <param name="w">[in] New window width.</param>
        /// <param name="h">[in] New window height.</param>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        public int OnSize(int x, int y, int w, int h) {
            var height = Math.Ceiling((double)_titleBarHeight) - 10;
            Interop.MoveWindow(_visualSosHwnd, 0, (int)height, w, h, true);
            Interop.ShowWindow(_visualSosHwnd, (int)Interop.CmdShow.SW_RESTORE);
            Interop.UpdateWindow(_selfHwnd);

            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        /// <summary>
        /// Notifies the VSPackage that a window's docked state is being altered.
        /// </summary>
        /// <param name="fDockable">[in] true if the window frame is being docked.</param>
        /// <param name="x">[in] Horizontal position of undocked window.</param>
        /// <param name="y">[in] Vertical position of undocked window.</param>
        /// <param name="w">[in] Width of undocked window.</param>
        /// <param name="h">[in] Height of undocked window.</param>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        public int OnDockableChange(int fDockable, int x, int y, int w, int h) {
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        /// <summary>
        /// Raises the Close event.
        /// </summary>
        /// <param name="pgrfSaveOptions">[in, out] Specifies options for saving window content. Values are taken from the <see cref="T:Microsoft.VisualStudio.Shell.Interop.__FRAMECLOSE" /> enumeration.</param>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        public int OnClose(ref uint pgrfSaveOptions) {
            _visualSosHwnd = IntPtr.Zero;
            return Microsoft.VisualStudio.VSConstants.S_OK;
        }

        #endregion
    }
}