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


using Autofac;
using System;
using System.Linq;
using System.Windows;
using VisualSOS.Abstractions.UI;

namespace VisualSOS.Core.ViewModels {
    /// <summary>
    /// 
    /// </summary>
    public abstract class ParentViewModel : IViewModel {
		/// <summary>
		/// Initializes a new instance of the <see cref="ParentViewModel"/> class.
		/// </summary>
		protected ParentViewModel() {
			InitializeChildrenViewModels();
		}

		/// <summary>
		/// Gets or sets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		/// <exception cref="NotImplementedException">
		/// </exception>
		public FrameworkElement Instance {
			get;
			set;
		}

		/// <summary>
		/// Refreshes the model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="treatAsReference">if set to <c>true</c> [treat as reference].</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void RefreshModel(object model, bool treatAsReference = false) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Resets the model.
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void ResetModel() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Initializes the children view models.
		/// </summary>
		private void InitializeChildrenViewModels() {
			var type = GetType();
			var props = type.GetProperties().Where(r => !string.IsNullOrEmpty(r.PropertyType.GetInterface(typeof(IViewModel).Name)?.Name))?.ToList();

			// Let's instantiate every child viewmodel found
			props?.ForEach(x => x.SetValue(this, x.PropertyType?.GetConstructor(Type.EmptyTypes)?.Invoke(null)));
		}
	}
}