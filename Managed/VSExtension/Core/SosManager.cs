using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VisualSOS.Abstractions;
using VisualSOS.Abstractions.Logic;
using VisualSOS.Core.Model;

namespace VisualSOS.Core {
    public class SosManager : ISosManager, IDisposable {
        /// <summary>
        /// The sos wrapper image
        /// </summary>
        private const string SosWrapperImage = "SosWrapper.dll";

        #region "Imports - Win32 specific"
        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the lp file.</param>
        /// <returns></returns>
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        /// <summary>
        /// Closes the handle.
        /// </summary>
        /// <param name="hHandle">The h handle.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        /// <summary>
        /// Frees the library.
        /// </summary>
        /// <param name="hModule">The h module.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// Gets the proc address.
        /// </summary>
        /// <param name="hModule">The h module.</param>
        /// <param name="procName">Name of the proc.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public delegate int CheckStatusWrapperDelegate();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public delegate int GetManagedAppsDelegate(StringBuilder result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attachBehavior">The attach behavior.</param>
        /// <param name="pid">The pid.</param>
        /// <returns></returns>
        public delegate int AttachOrDetachDelegate(int attachBehavior, int pid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="isPrivate">The is private.</param>
        /// <returns></returns>
        public delegate int ExecuteCommandDelegate(string command, int isPrivate);

        /// <summary>
        /// The _is disposed
        /// </summary>
        private bool _isDisposed = false;

        /// <summary>
        /// Gets or sets the sos wrapper handle.
        /// </summary>
        /// <value>
        /// The sos wrapper handle.
        /// </value>
        protected IntPtr SosWrapperHandle {
            get; set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is currently attached.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is currently attached; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrentlyAttached {
            get {
                var retval = false;
                var hProcAddress = IntPtr.Zero;

                if (SosWrapperHandle != IntPtr.Zero) {
                    if ((hProcAddress = GetProcAddress(SosWrapperHandle, "IsDebuggingEngineAttached")) != IntPtr.Zero) {
                        var functor = Marshal.GetDelegateForFunctionPointer(hProcAddress, typeof(CheckStatusWrapperDelegate));
                        retval = (int)functor.DynamicInvoke(null) == 0;
                    }
                }
                return retval;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:VisualSOS.Abstractions.Logic.ISosManager" /> is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized {
            get {
                var retval = false;
                var hProcAddress = IntPtr.Zero;

                if (SosWrapperHandle != IntPtr.Zero) {
                    if ((hProcAddress = GetProcAddress(SosWrapperHandle, "IsInitialized")) != IntPtr.Zero) {
                        var functor = Marshal.GetDelegateForFunctionPointer(hProcAddress, typeof(CheckStatusWrapperDelegate));
                        retval = (int)functor.DynamicInvoke(null) == 0;
                    }
                }
                return retval;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SosManager"/> class.
        /// </summary>
        public SosManager() {
            var sosWrapperPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{SosWrapperImage}";

            if (File.Exists(sosWrapperPath)) {
                if ((SosWrapperHandle = LoadLibrary(sosWrapperPath)) == IntPtr.Zero)
                    throw new FileLoadException(nameof(sosWrapperPath));
            } else
                throw new FileNotFoundException(nameof(sosWrapperPath));
        }

        /// <summary>
        /// Attaches the or detach.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        /// <param name="pid">The pid.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ExecutionResult AttachOrDetach(DebuggerBehavior behavior, int pid) {
            var hProcAddress = IntPtr.Zero;
            var retval = ExecutionResult.Empty;

            if (SosWrapperHandle != IntPtr.Zero) {
                if ((hProcAddress = GetProcAddress(SosWrapperHandle, "AttachOrDetach")) != IntPtr.Zero) {
                    var functor = Marshal.GetDelegateForFunctionPointer(hProcAddress, typeof(AttachOrDetachDelegate));
                    retval.IsSuccess = (int)functor.DynamicInvoke((int)behavior, pid) == 0;
                    var debugeePath = Path.GetDirectoryName(Process.GetProcessById(pid)?.MainModule?.FileName);
                    LoadSymbols(debugeePath);
                }
            }
            return retval;
        }

        /// <summary>
        /// Loads the symbols.
        /// </summary>
        /// <param name="debugeePath">The debugee path.</param>
        /// <returns></returns>
        public ExecutionResult LoadSymbols(string debugeePath) {
            var retval = ExecutionResult.Empty;
            var command = $".sympath srv*https://msdl.microsoft.com/download/symbols;{debugeePath}";
            retval.IsSuccess = RunCommand(command, true).IsSuccess && RunCommand(".reload", true).IsSuccess;

            return retval;
        }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="privateExecution">if set to <c>true</c> [private execution].</param>
        /// <returns></returns>
        public ExecutionResult RunCommand(string command, bool privateExecution) {
            var hProcAddress = IntPtr.Zero;
            var retval = ExecutionResult.Empty;

            if (SosWrapperHandle != IntPtr.Zero) {
                if ((hProcAddress = GetProcAddress(SosWrapperHandle, "RunCommand")) != IntPtr.Zero) {
                    var functor = Marshal.GetDelegateForFunctionPointer(hProcAddress, typeof(ExecuteCommandDelegate));
                    retval.IsSuccess = (int)functor.DynamicInvoke(command, privateExecution ? 1 : 0) == 0;
                }
            }
            return retval;

        }

        /// <summary>
        /// Runs the task list.
        /// </summary>
        /// <returns></returns>
        private ExecutionResult RunTaskList() {
            var retval = ExecutionResult.Empty;
            try {
                retval.Tag = new StringBuilder();
                using (var newProc = new Process() {
                    StartInfo = new ProcessStartInfo("tasklist.exe") {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Arguments = $"/m mscorlib*",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden

                    }, EnableRaisingEvents = true
                }) {
                    retval.IsSuccess = true;
                    newProc.OutputDataReceived += (s, e) => ((StringBuilder)retval.Tag).AppendLine(e.Data);
                    newProc.Start();
                    newProc.BeginOutputReadLine();
                    newProc.WaitForExit();

                }
            } catch (Exception e) {
                retval.Tag = null;
                retval.IsSuccess = false;
                retval.LastExceptionIfAny = e;
            }
            return retval;
        }

        /// <summary>
        /// Gets the managed apps.
        /// </summary>
        /// <returns></returns>
        public async Task<ExecutionResult> GetManagedApps() {
            return await Task.Run(() => {
                Match m;
                var retval = RunTaskList();
                var regex = new Regex(@"\b(\d+)\b");
                var managedApps = new List<ManagedApp>();

                if (retval.IsSuccess) {
                    var processes = ((StringBuilder)retval.Tag).ToString().Split("\r\n".ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    if (processes.Length > 1) {
                        // Let's ignore header returned by TaskList
                        for (var n = 2; n < processes.Length; n++) {
                            if ((m = regex.Match(processes[n])).Success) {
                                var p = Process.GetProcessById(int.Parse(m.Value));

                                if (!p.ProcessName.ToLower().Contains("visualsos.ui")) // We'll ignore "VisualSOS"
                                    managedApps.Add(new ManagedApp { Pid = p.Id, ImageName = p.ProcessName, ImagePath = p.MainModule.FileName });
                            }
                        }
                        retval.Tag = managedApps;
                    }
                }
                return retval;
            });
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool isDisposing) {
            if (_isDisposed)
                return;

            if (isDisposing) {
                FreeLibrary(SosWrapperHandle);
                CloseHandle(SosWrapperHandle);
            }
            _isDisposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}