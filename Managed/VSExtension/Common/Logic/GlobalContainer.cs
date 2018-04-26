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
using VisualSOS.Abstractions.Common;

namespace VisualSOS.Common.Logic {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="VisualSOS.Abstractions.Common.IGlobalContainer" />
    public class GlobalContainer : IGlobalContainer {
		/// <summary>
		/// The instance identifier
		/// </summary>
		private Guid _instanceId;

		/// <summary>
		/// The creation time
		/// </summary>
		private DateTime _creationTime;

		/// <summary>
		/// The instance
		/// </summary>
		private static IGlobalContainer _instance;

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		public static IGlobalContainer Current {
			get {
				lock (new object()) {
					if (_instance == null)
						_instance = new GlobalContainer(Guid.NewGuid());
				}
				return _instance;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalContainer"/> class.
		/// </summary>
		/// <param name="instanceId">The instance identifier.</param>
		private GlobalContainer(Guid instanceId) {
			_creationTime = DateTime.Now;
			_instanceId = instanceId;
		}

		/// <summary>
		/// Gets or sets the type container.
		/// </summary>
		/// <value>
		/// The type container.
		/// </value>
		public IContainer TypeContainer {
			get; set;
		}
	}
}