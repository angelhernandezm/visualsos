// Copyright (C) 2018 Angel Hernandez Matos
// You can redistribute this software and/or modify it under the terms of the 
// GNU General Public License  (GPL).  This program is distributed in the hope 
// that it will be useful, but WITHOUT ANY WARRANTY; without even the implied 
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See License.txt for more details. 

/* C++ compiler   : Microsoft (R) C/C++ Optimizing Compiler Version 19.13.26129 for x64
Creation date     : 26/04/2018
Developer         : Angel Hernandez Matos
e-m@il            : me@angelhernandezm.com
Website           : http://www.angelhernandezm.com
*/

#pragma once

#include <fstream>
//#include <memory.h>
//#include <typeinfo.h>
#include <msxml6.h>
#include <vector>
#include "ExtInformation.h"
#include <map>
#import "c:\windows\system32\msxml6.dll"

/// <summary>
/// 
/// </summary>
class ConfigReader {
	/// <summary>
	/// Gets the thread token.
	/// </summary>
	/// <returns></returns>
	HANDLE GetThreadToken();

	/// <summary>
	/// Locates the configuration file.
	/// </summary>
	/// <returns></returns>
	BOOL LocateConfigFile();

	/// <summary>
	/// The m extensions
	/// </summary>
	std::vector<ExtInformation> m_extensions;

	/// <summary>
	/// Processes the element recursively.
	/// </summary>
	/// <param name="node">The node.</param>
	void ProcessElementRecursively(IXMLDOMNodePtr& node);

	/// <summary>
	/// Parses the configuration file.
	/// </summary>
	/// <param name="configFile">The configuration file.</param>
	/// <returns></returns>
	BOOL ParseConfigFile(const std::wstring& configFile);

	/// <summary>
	/// Extracts the information from element.
	/// </summary>
	/// <param name="node">The node.</param>
	void ExtractInformationFromElement(IXMLDOMNodePtr& node);

	/// <summary>
	/// The properties
	/// </summary>
	std::map<const std::wstring, const std::wstring> Properties;

	/// <summary>
	/// Sets the privilege.
	/// </summary>
	/// <param name="hToken">The h token.</param>
	/// <param name="Privilege">The privilege.</param>
	/// <param name="bEnablePrivilege">The b enable privilege.</param>
	/// <returns></returns>
	BOOL SetPrivilege(HANDLE& hToken, LPCTSTR Privilege, BOOL bEnablePrivilege);

	/// <summary>
	/// Checks whether the configuration file exist.
	/// </summary>
	/// <param name="hProcess">The h process.</param>
	/// <param name="hModules">The h modules.</param>
	/// <param name="targetImage">The target image.</param>
	/// <returns></returns>
	std::wstring DoesConfigFileExist(const HANDLE& hProcess, std::vector<HMODULE>& hModules, const wchar_t* targetImage);

protected:
	/// <summary>
	/// Reads the configuration.
	/// </summary>
	void ReadConfig();

public:
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigReader"/> class.
	/// </summary>
	ConfigReader();

	/// <summary>
	/// Finalizes an instance of the <see cref="ConfigReader"/> class.
	/// </summary>
	~ConfigReader();

	/// <summary>
	/// Gets the setting.
	/// </summary>
	/// <param name="key">The key.</param>
	/// <returns></returns>
	const std::wstring GetSetting(const wchar_t* key);

	/// <summary>
	/// Gets loaded extensions
	/// </summary>
	/// <returns></returns>
	std::vector<ExtInformation>& Extensions_get();
};