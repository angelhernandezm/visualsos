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

using System.Collections.Generic;
using System.Threading.Tasks;

namespace VisualSOS.Abstractions.Common {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV">The type of the v.</typeparam>
    /// <seealso cref="IBaseRepository" />
    public interface IMainRepository<T> : IBaseRepository
	{
		/// <summary>
		/// Gets the data scalar.
		/// </summary>
		/// <typeparam name="TV">The type of the v.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		Task<T> GetDataScalar<TV>(TV criteria);

		/// <summary>
		/// Gets the data collection.
		/// </summary>
		/// <typeparam name="TV">The type of the v.</typeparam>
		/// <param name="criteria">The criteria.</param>
		/// <returns></returns>
		Task<List<T>> GetDataCollection<TV>(TV criteria);
	}
}
