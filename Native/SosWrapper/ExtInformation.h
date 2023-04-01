// Copyright (C) 2023 Angel Hernandez Matos
// You can redistribute this software and/or modify it under the terms of the 
// GNU General Public License  (GPL).  This program is distributed in the hope 
// that it will be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See License.txt for more details. 

/* C++ compiler   : Microsoft (R) C/C++ Optimizing Compiler Version 19.35.32216.1 for x64
Creation date     : 31/03/2023
Developer         : Angel Hernandez Matos
e-m@il            : me@angelhernandezm.com
Website           : http://www.angelhernandezm.com
*/

#pragma once

struct ExtInformation {
	//public:
		/// <summary>
		/// The p handle
		/// </summary>
	ULONG64 pHandle;

	/// <summary>
	/// The name
	/// </summary>
	std::string Name;

	/// <summary>
	/// The path
	/// </summary>
	std::string Path;

	/// <summary>
	/// Initializes a new instance of the <see cref="_ExtInformation"/> struct.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="path">The path.</param>
	//_ExtInformation(std::string name, std::string path) {
	ExtInformation(std::string name, std::string path) {
		pHandle = 0;
		Name = name;
		Path = path;
	}
};