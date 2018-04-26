using System.Windows;
using VisualSOS.Abstractions.UI;


namespace VisualSOS.UI.View {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IViewBase {
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			((IViewModel)DataContext).Instance = Instance = this;
		}

		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public FrameworkElement Instance {
			get;
			set;
		}
	}
}