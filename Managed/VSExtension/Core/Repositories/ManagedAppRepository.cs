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
using System.Threading.Tasks;
using VisualSOS.Abstractions.Logic;
using VisualSOS.Core.Model;


namespace VisualSOS.Core.Repositories {
    public class ManagedAppRepository : IManagedAppRepository<ManagedApp> {
		/// <summary>
		/// Gets the data collection.
		/// </summary>
		/// <typeparam name="TV">The type of the v.</typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		public Task<List<ManagedApp>> GetDataCollection<TV>(TV criteria) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the data scalar.
		/// </summary>
		/// <typeparam name="TV"></typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<ManagedApp> GetDataScalar<TV>(TV criteria) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets or sets the sos manager.
		/// </summary>
		/// <value>
		/// The sos manager.
		/// </value>
		public ISosManager SosManager {
			get; set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ManagedAppRepository"/> class.
		/// </summary>
		/// <param name="sosManager">The sos manager.</param>
		public ManagedAppRepository(ISosManager sosManager) {
			SosManager = sosManager;
		}
	}
}
