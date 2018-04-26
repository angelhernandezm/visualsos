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

using VisualSOS.Common.Entities;

namespace VisualSOS.Core.Model {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="VisualSOS.Common.Entities.BaseEntity" />
    public class ManagedApp : BaseEntity {
		/// <summary>
		/// The pid
		/// </summary>
		private int _pid;

		/// <summary>
		/// The image name
		/// </summary>
		private string _imageName;

		/// <summary>
		/// The image path
		/// </summary>
		private string _imagePath;

		/// <summary>
		/// Gets or sets the pid.
		/// </summary>
		/// <value>
		/// The pid.
		/// </value>
		public int Pid {
			get => _pid;
			set {
				_pid = value;
				RaisePropertyChanged("Pid");
			}
		}

		/// <summary>
		/// Gets or sets the name of the image.
		/// </summary>
		/// <value>
		/// The name of the image.
		/// </value>
		public string ImageName {
			get => _imageName;
			set {
				_imageName = value;
				RaisePropertyChanged("ImageName");
			}
		}

		/// <summary>
		/// Gets or sets the image path.
		/// </summary>
		/// <value>
		/// The image path.
		/// </value>
		public string ImagePath {
			get => _imagePath;
			set {
				_imagePath = value;
				RaisePropertyChanged("ImagePath");
			}
		}

	}

}
