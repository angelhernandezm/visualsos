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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autofac;
using VisualSOS.Abstractions.Common;
using VisualSOS.Abstractions.UI;
using VisualSOS.Common.Entities;
using VisualSOS.Common.Logic;

namespace VisualSOS.Core.ViewModels {
	public class ViewModelBase<TViewModelData, TRepository> : FrameworkElement, INotifyPropertyChanged,
															  IViewModel where TViewModelData : BaseEntity, new()
																		 where TRepository : IBaseRepository {

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// The data
		/// </summary>
		private ObservableCollection<TViewModelData> _data;

		/// <summary>
		/// Gets or sets the data fields.
		/// </summary>
		/// <value>
		/// The data fields.
		/// </value>
		public TViewModelData DataFields {
			get => (TViewModelData)GetValue(m_dataFields);
			set {
				SetValue(m_dataFields, value);
				RaisePropertyChanged("DataFields");
			}
		}

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		/// <value>
		/// The repository.
		/// </value>
		public TRepository Repository {
			get; set;
		}

		/// <summary>
		/// Gets or sets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public ObservableCollection<TViewModelData> Data {
			get => _data;
			set {
				_data = value;
				RaisePropertyChanged("Data");
			}
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
		/// Resets the model.
		/// </summary>
		public void ResetModel() {
			Data.Clear();
			DataFields = new TViewModelData();
		}

		/// <summary>
		/// Refreshes the model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="treatAsReference">if set to <c>true</c> [treat as reference].</param>
		public void RefreshModel(object model, bool treatAsReference = false) {
			ResetModel();

			if (model != null) {
				var collection = model as IEnumerable<TViewModelData>;
				var observable = model as ObservableCollection<TViewModelData>;

				if (collection != null) {
					if (!treatAsReference)
						collection.ToList().ForEach(x => Data.Add(x));
					else if (observable != null)
						Data = observable;
				} else if ((model as TViewModelData) != null)
					DataFields = (TViewModelData)model;
			}
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelBase{TViewModelData, TRepository}"/> class.
		/// </summary>
		public ViewModelBase() {
			DataFields = new TViewModelData();
			Data = new ObservableCollection<TViewModelData>();
			FindSuitableRepository();
		}

		/// <summary>
		/// Finds the suitable repository.
		/// </summary>
		private void FindSuitableRepository() {
			if (GlobalContainer.Current?.TypeContainer != null) {
				Repository = GlobalContainer.Current.TypeContainer.Resolve<TRepository>();
			} else
				throw new ArgumentNullException();
		}

		/// <summary>
		/// Raises the property changed.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		public void RaisePropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// The m_data fields
		/// </summary>
		private static readonly DependencyProperty m_dataFields = DependencyProperty.Register("DataFields", typeof(TViewModelData),
																							  typeof(ViewModelBase<TViewModelData, TRepository>),
																							  new UIPropertyMetadata(null));
	}
}
