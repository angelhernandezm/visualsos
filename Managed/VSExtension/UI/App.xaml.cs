using System;
using System.Threading;
using System.Windows;
using Autofac;
using VisualSOS.Common.Logic;
using VisualSOS.Core.Infrastructure;

namespace VisualSOS.UI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Application" />
    public class App : Application {
        [STAThread]
        public static void Main() {
            using (var mutex = new Mutex(false, "Visual-Sos-Mutex")) {
                if (mutex.WaitOne(0, true)) {
                    var app = new App();
                    app.InitializeComponent();
                    app.Run();
                }
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent() {
            Bootstrapper.Run(InitializeTypeContainer);
            StartupUri = new Uri(@"..\View\MainWindow.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Initializes the type container.
        /// </summary>
        /// <param name="container">The container.</param>
        private void InitializeTypeContainer(IContainer container) {
            GlobalContainer.Current.TypeContainer = container ?? throw new ArgumentNullException();
        }
    }
}
