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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VisualSOS.UI.Behavior {
    public class MouseDoubleClick {
		/// <summary>
		/// The command property
		/// </summary>
		public static DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseDoubleClick), 
												new UIPropertyMetadata(CommandChanged));

		/// <summary>
		/// The command parameter property
		/// </summary>
		public static DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(MouseDoubleClick),
												 new UIPropertyMetadata(null));

		/// <summary>
		/// Sets the command.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="value">The value.</param>
		public static void SetCommand(DependencyObject target, ICommand value) {
			target.SetValue(CommandProperty, value);
		}

		/// <summary>
		/// Sets the command parameter.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="value">The value.</param>
		public static void SetCommandParameter(DependencyObject target, object value) {
			target.SetValue(CommandParameterProperty, value);
		}
		/// <summary>
		/// Gets the command parameter.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <returns></returns>
		public static object GetCommandParameter(DependencyObject target) {
			return target.GetValue(CommandParameterProperty);
		}

		/// <summary>
		/// Commands the changed.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
		private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) {
			Control control;

			if ((control = target as Control) != null) {
				if ((e.NewValue != null) && (e.OldValue == null))
					control.MouseDoubleClick += OnMouseDoubleClick;
				else if ((e.NewValue == null) && (e.OldValue != null))
					control.MouseDoubleClick -= OnMouseDoubleClick;
			}
		}

		/// <summary>
		/// Called when [mouse double click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private static void OnMouseDoubleClick(object sender, RoutedEventArgs e) {
			var control = sender as Control;
			var command = (ICommand)control?.GetValue(CommandProperty);
			var commandParameter = control?.GetValue(CommandParameterProperty);
			command?.Execute(commandParameter);
		}
	}
}