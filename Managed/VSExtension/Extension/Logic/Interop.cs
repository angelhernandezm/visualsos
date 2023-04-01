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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace VisualSOS.Extension.Logic {
    /// <summary>
    /// 
    /// </summary>
    internal class Interop {
        #region "Delegates, enums and constants"

        /// <summary>
        /// The wm gettext
        /// </summary>
        public const uint WM_GETTEXT = 0x000D;

        /// <summary>
        /// The GWL style
        /// </summary>
        public const int GWL_STYLE = -16;

        /// <summary>
        /// The wm keydown
        /// </summary>
        public const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// The wm keyup
        /// </summary>
        public const int WM_KEYUP = 0x101;

        /// <summary>
        /// The wm command
        /// </summary>
        public const int WM_COMMAND = 0x111;

        /// <summary>
        /// The wm lbuttondown
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;

        /// <summary>
        /// The wm lbuttonup
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;

        /// <summary>
        /// The wm lbuttondblclk
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;

        /// <summary>
        /// The wm rbuttondown
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;

        /// <summary>
        /// The wm rbuttonup
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;

        /// <summary>
        /// The wm rbuttondbdlclk
        /// </summary>
        public const int WM_RBUTTONDBDLCLK = 0x206;

        /// <summary>
        /// The visual sos window style
        /// </summary>
        public const uint VisualSosWindowStyle = 0x96CF0000; //0x96CD0000;

		/// <summary>
		/// 
		/// </summary>
		public enum CmdShow : int {
            SW_HIDE = 0,
            SW_MAXIMIZE = 3,
            SW_RESTORE = 9
        }

        /// <summary>
        /// 
        /// </summary>
        public enum SystemMetric {
            SM_CXSCREEN = 0,  // 0x00
            SM_CYSCREEN = 1,  // 0x01
            SM_CXVSCROLL = 2,  // 0x02
            SM_CYHSCROLL = 3,  // 0x03
            SM_CYCAPTION = 4,  // 0x04
            SM_CXBORDER = 5,  // 0x05
            SM_CYBORDER = 6,  // 0x06
            SM_CXDLGFRAME = 7,  // 0x07
            SM_CXFIXEDFRAME = 7,  // 0x07
            SM_CYDLGFRAME = 8,  // 0x08
            SM_CYFIXEDFRAME = 8,  // 0x08
            SM_CYVTHUMB = 9,  // 0x09
            SM_CXHTHUMB = 10, // 0x0A
            SM_CXICON = 11, // 0x0B
            SM_CYICON = 12, // 0x0C
            SM_CXCURSOR = 13, // 0x0D
            SM_CYCURSOR = 14, // 0x0E
            SM_CYMENU = 15, // 0x0F
            SM_CXFULLSCREEN = 16, // 0x10
            SM_CYFULLSCREEN = 17, // 0x11
            SM_CYKANJIWINDOW = 18, // 0x12
            SM_MOUSEPRESENT = 19, // 0x13
            SM_CYVSCROLL = 20, // 0x14
            SM_CXHSCROLL = 21, // 0x15
            SM_DEBUG = 22, // 0x16
            SM_SWAPBUTTON = 23, // 0x17
            SM_CXMIN = 28, // 0x1C
            SM_CYMIN = 29, // 0x1D
            SM_CXSIZE = 30, // 0x1E
            SM_CYSIZE = 31, // 0x1F
            SM_CXSIZEFRAME = 32, // 0x20
            SM_CXFRAME = 32, // 0x20
            SM_CYSIZEFRAME = 33, // 0x21
            SM_CYFRAME = 33, // 0x21
            SM_CXMINTRACK = 34, // 0x22
            SM_CYMINTRACK = 35, // 0x23
            SM_CXDOUBLECLK = 36, // 0x24
            SM_CYDOUBLECLK = 37, // 0x25
            SM_CXICONSPACING = 38, // 0x26
            SM_CYICONSPACING = 39, // 0x27
            SM_MENUDROPALIGNMENT = 40, // 0x28
            SM_PENWINDOWS = 41, // 0x29
            SM_DBCSENABLED = 42, // 0x2A
            SM_CMOUSEBUTTONS = 43, // 0x2B
            SM_SECURE = 44, // 0x2C
            SM_CXEDGE = 45, // 0x2D
            SM_CYEDGE = 46, // 0x2E
            SM_CXMINSPACING = 47, // 0x2F
            SM_CYMINSPACING = 48, // 0x30
            SM_CXSMICON = 49, // 0x31
            SM_CYSMICON = 50, // 0x32
            SM_CYSMCAPTION = 51, // 0x33
            SM_CXSMSIZE = 52, // 0x34
            SM_CYSMSIZE = 53, // 0x35
            SM_CXMENUSIZE = 54, // 0x36
            SM_CYMENUSIZE = 55, // 0x37
            SM_ARRANGE = 56, // 0x38
            SM_CXMINIMIZED = 57, // 0x39
            SM_CYMINIMIZED = 58, // 0x3A
            SM_CXMAXTRACK = 59, // 0x3B
            SM_CYMAXTRACK = 60, // 0x3C
            SM_CXMAXIMIZED = 61, // 0x3D
            SM_CYMAXIMIZED = 62, // 0x3E
            SM_NETWORK = 63, // 0x3F
            SM_CLEANBOOT = 67, // 0x43
            SM_CXDRAG = 68, // 0x44
            SM_CYDRAG = 69, // 0x45
            SM_SHOWSOUNDS = 70, // 0x46
            SM_CXMENUCHECK = 71, // 0x47
            SM_CYMENUCHECK = 72, // 0x48
            SM_SLOWMACHINE = 73, // 0x49
            SM_MIDEASTENABLED = 74, // 0x4A
            SM_MOUSEWHEELPRESENT = 75, // 0x4B
            SM_XVIRTUALSCREEN = 76, // 0x4C
            SM_YVIRTUALSCREEN = 77, // 0x4D
            SM_CXVIRTUALSCREEN = 78, // 0x4E
            SM_CYVIRTUALSCREEN = 79, // 0x4F
            SM_CMONITORS = 80, // 0x50
            SM_SAMEDISPLAYFORMAT = 81, // 0x51
            SM_IMMENABLED = 82, // 0x52
            SM_CXFOCUSBORDER = 83, // 0x53
            SM_CYFOCUSBORDER = 84, // 0x54
            SM_TABLETPC = 86, // 0x56
            SM_MEDIACENTER = 87, // 0x57
            SM_STARTER = 88, // 0x58
            SM_SERVERR2 = 89, // 0x59
            SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
            SM_CXPADDEDBORDER = 92, // 0x5C
            SM_DIGITIZER = 94, // 0x5E
            SM_MAXIMUMTOUCHES = 95, // 0x5F
            SM_REMOTESESSION = 0x1000, // 0x1000
            SM_SHUTTINGDOWN = 0x2000, // 0x2000
            SM_REMOTECONTROL = 0x2001, // 0x2001
            SM_CONVERTABLESLATEMODE = 0x2003,
            SM_SYSTEMDOCKED = 0x2004,
        }


        /// <summary>
        /// 
        /// </summary>
        [Flags()]
        public enum SetWindowPosFlags : uint {
            AsynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        #endregion

        #region "Extern Win32 functions"

        /// <summary>
        /// Determines whether the specified h WND is window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>
        /// 	<c>true</c> if the specified h WND is window; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nCmdShow">The n CMD show.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="hWndChild">The h WND child.</param>
        /// <param name="hWndNewParent">The h WND new parent.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// Enums the thread windows.
        /// </summary>
        /// <param name="dwThreadId">The dw thread identifier.</param>
        /// <param name="lpfn">The LPFN.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        /// <summary>
        /// Gets the window information.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="pwi">The pwi.</param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);


        /// <summary>
        /// Enums the child windows.
        /// </summary>
        /// <param name="hwndParent">The HWND parent.</param>
        /// <param name="lpEnumFunc">The lp enum function.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpString">The lp string.</param>
        /// <param name="nMaxCount">The n maximum count.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Sets the window long32.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Sets the window long PTR64.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// Gets the system metrics.
        /// </summary>
        /// <param name="smIndex">Index of the sm.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);


        /// <summary>
        /// Sets the window position.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hWndInsertAfter">The h WND insert after.</param>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        /// <summary>
        /// Moves the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="bRepaint">if set to <c>true</c> [b repaint].</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int cx, int cy, bool bRepaint);

        /// <summary>
        /// Updates the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UpdateWindow(IntPtr hWnd);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="strClassName">Name of the string class.</param>
        /// <param name="strWindowName">Name of the string window.</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        /// <summary>
        /// Finds the window ex.
        /// </summary>
        /// <param name="hwndParent">The HWND parent.</param>
        /// <param name="hwndChildAfter">The HWND child after.</param>
        /// <param name="strClassName">Name of the string class.</param>
        /// <param name="strWindowName">Name of the string window.</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string strClassName, string strWindowName);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);


        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        /// <summary>
        /// Gets the window rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        #endregion

        #region "Static helper functions"

        /// <summary>
        /// Sets the window long PTR.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong) {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        /// <summary>
        /// Enumerates the process window handles.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <returns></returns>
        public static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId) {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
                EnumThreadWindows(thread.Id, (hWnd, lParam) => {
                    handles.Add(hWnd);
                    return true;
                }, IntPtr.Zero);

            return handles;
        }

        /// <summary>
        /// Gets the visual studio windows.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <returns></returns>
        public static Dictionary<IntPtr, string> GetVisualStudioWindows(int processId) {
            var retval = new Dictionary<IntPtr, string>();

            foreach (var handle in EnumerateProcessWindowHandles(processId)) {
                var message = new StringBuilder(1000);
                SendMessage(handle, WM_GETTEXT, message.Capacity, message);
                retval.Add(handle, message.ToString());
            }
            return retval;
        }


        /// <summary>
        /// Gets the child windows.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public static Dictionary<IntPtr, StringBuilder> GetChildWindows(IntPtr parent) {
            var retval = new Dictionary<IntPtr, StringBuilder>();
            var listHandle = GCHandle.Alloc(retval);

            try {
                EnumChildWindows(parent, EnumWindow, GCHandle.ToIntPtr(listHandle));
            } finally {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            return retval;
        }

        /// <summary>
        /// Enums the window.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="pointer">The pointer.</param>
        /// <returns></returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer) {
            var gch = GCHandle.FromIntPtr(pointer);
            Dictionary<IntPtr, StringBuilder> windows;

            if ((windows = gch.Target as Dictionary<IntPtr, StringBuilder>) != null) {
                var buffer = new StringBuilder(256);
                GetWindowText(handle, buffer, buffer.Length);
                windows.Add(handle, buffer);
            }

            return true;
        }

        #endregion

        #region "WINDOWINFO structure"

        /// <summary>
        /// WINDOWINFO structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO {
            /// <summary>
            /// The cb size
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// The rc window
            /// </summary>
            public RECT rcWindow;
            /// <summary>
            /// The rc client
            /// </summary>
            public RECT rcClient;
            /// <summary>
            /// The dw style
            /// </summary>
            public uint dwStyle;
            /// <summary>
            /// The dw ex style
            /// </summary>
            public uint dwExStyle;
            /// <summary>
            /// The dw window status
            /// </summary>
            public uint dwWindowStatus;
            /// <summary>
            /// The cx window borders
            /// </summary>
            public uint cxWindowBorders;
            /// <summary>
            /// The cy window borders
            /// </summary>
            public uint cyWindowBorders;
            /// <summary>
            /// The atom window type
            /// </summary>
            public ushort atomWindowType;
            /// <summary>
            /// The w creator version
            /// </summary>
            public ushort wCreatorVersion;

            /// <summary>
            /// Initializes a new instance of the <see cref="WINDOWINFO"/> struct.
            /// </summary>
            /// <param name="filler">The filler.</param>
            public WINDOWINFO(Boolean? filler) : this() {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }

        }

        #endregion

        #region "RECT structure"

        /// <summary>
        /// RECT structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left, Top, Right, Bottom;

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT"/> struct.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="top">The top.</param>
            /// <param name="right">The right.</param>
            /// <param name="bottom">The bottom.</param>
            public RECT(int left, int top, int right, int bottom) {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT"/> struct.
            /// </summary>
            /// <param name="r">The r.</param>
            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            /// <summary>
            /// Gets or sets the x.
            /// </summary>
            /// <value>
            /// The x.
            /// </value>
            public int X {
                get {
                    return Left;
                }
                set {
                    Right -= (Left - value);
                    Left = value;
                }
            }

            /// <summary>
            /// Gets or sets the y.
            /// </summary>
            /// <value>
            /// The y.
            /// </value>
            public int Y {
                get {
                    return Top;
                }
                set {
                    Bottom -= (Top - value);
                    Top = value;
                }
            }

            /// <summary>
            /// Gets or sets the height.
            /// </summary>
            /// <value>
            /// The height.
            /// </value>
            public int Height {
                get {
                    return Bottom - Top;
                }
                set {
                    Bottom = value + Top;
                }
            }

            /// <summary>
            /// Gets or sets the width.
            /// </summary>
            /// <value>
            /// The width.
            /// </value>
            public int Width {
                get {
                    return Right - Left;
                }
                set {
                    Right = value + Left;
                }
            }

            /// <summary>
            /// Gets or sets the location.
            /// </summary>
            /// <value>
            /// The location.
            /// </value>
            public System.Drawing.Point Location {
                get {
                    return new System.Drawing.Point(Left, Top);
                }
                set {
                    X = value.X;
                    Y = value.Y;
                }
            }

            /// <summary>
            /// Gets or sets the size.
            /// </summary>
            /// <value>
            /// The size.
            /// </value>
            public System.Drawing.Size Size {
                get {
                    return new System.Drawing.Size(Width, Height);
                }
                set {
                    Width = value.Width;
                    Height = value.Height;
                }
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="RECT"/> to <see cref="System.Drawing.Rectangle"/>.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <returns>
            /// The result of the conversion.
            /// </returns>
            public static implicit operator System.Drawing.Rectangle(RECT r) {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="System.Drawing.Rectangle"/> to <see cref="RECT"/>.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <returns>
            /// The result of the conversion.
            /// </returns>
            public static implicit operator RECT(System.Drawing.Rectangle r) {
                return new RECT(r);
            }

            /// <summary>
            /// Implements the operator ==.
            /// </summary>
            /// <param name="r1">The r1.</param>
            /// <param name="r2">The r2.</param>
            /// <returns>
            /// The result of the operator.
            /// </returns>
            public static bool operator ==(RECT r1, RECT r2) {
                return r1.Equals(r2);
            }

            /// <summary>
            /// Implements the operator !=.
            /// </summary>
            /// <param name="r1">The r1.</param>
            /// <param name="r2">The r2.</param>
            /// <returns>
            /// The result of the operator.
            /// </returns>
            public static bool operator !=(RECT r1, RECT r2) {
                return !r1.Equals(r2);
            }

            /// <summary>
            /// Equalses the specified r.
            /// </summary>
            /// <param name="r">The r.</param>
            /// <returns></returns>
            public bool Equals(RECT r) {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
            /// <returns>
            ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
            /// </returns>
            public override bool Equals(object obj) {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            public override int GetHashCode() {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            /// <summary>
            /// Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString() {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                                    "{{Left={0},Top={1},Right={2},Bottom={3}}}",
                                    Left, Top, Right, Bottom);
            }
        }

        #endregion
    }
}