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

#include "stdafx.h"
#include "ConfigReader.h"


/// <summary>
/// Initializes a new instance of the <see cref="ConfigReader"/> class.
/// </summary>
ConfigReader::ConfigReader() {
	ReadConfig();
}

/// <summary>
/// Finalizes an instance of the <see cref="ConfigReader"/> class.
/// </summary>
ConfigReader::~ConfigReader() = default;

/// <summary>
/// Reads the configuration.
/// </summary>
void ConfigReader::ReadConfig() {
	if (!LocateConfigFile())
		throw std::exception("Config file not found. Unable to proceed.");
}


/// <summary>
/// Extracts the information from element.
/// </summary>
/// <param name="node">The node.</param>
void ConfigReader::ExtractInformationFromElement(IXMLDOMNodePtr& node) {
	size_t nSize;
	VARIANT value;
	std::wstring key;
	BSTR nodeContent;
	DOMNodeType nodeType;
	WCHAR szNodeText[512] = {0};
	char szBuffer[MAX_PATH] = {0};
	

	CoInitialize(nullptr);

	if (SUCCEEDED(node->get_nodeType(&nodeType)) && nodeType == DOMNodeType::NODE_ELEMENT) {
		nodeContent = SysAllocString(szNodeText);
		auto pElement = (IXMLDOMElementPtr)node;
		pElement->get_tagName(&nodeContent);

		if (wcscmp(nodeContent, L"sendOutputToVSWindow") == 0) {
			pElement->getAttribute(_bstr_t(L"enabled"), &value);

			if (value.vt != VT_NULL) 
				Properties.insert(std::make_pair(nodeContent, value.bstrVal));

		} else if (wcscmp(nodeContent, L"extension") == 0) {
			pElement->getAttribute(_bstr_t(L"name"), &value);

			if (value.vt != VT_NULL)
				key.assign(value.bstrVal);

			pElement->getAttribute(_bstr_t(L"path"), &value);

			if (value.vt != VT_NULL && !key.empty()) {
				Properties.insert(std::make_pair(key.c_str(), value.bstrVal));
				wcstombs_s(&nSize, szBuffer, key.c_str(), key.size());
				std::string name(szBuffer);
				wcstombs_s(&nSize, szBuffer, value.bstrVal, wcslen(value.bstrVal));
				std::string path(szBuffer);
				m_extensions.push_back(ExtInformation(name, path));
			}
				
		}

		SysFreeString(nodeContent);
	}

	CoUninitialize();
}


/// <summary>
/// Gets the setting.
/// </summary>
/// <param name="key">The key.</param>
/// <returns></returns>
const std::wstring ConfigReader::GetSetting(const wchar_t* key) {
	std::wstring retval;

	if (!Properties.empty() && key != nullptr && wcslen(key) > 0) {
		typedef std::pair<const std::wstring, const std::wstring> item;

		std::find_if(Properties.begin(), Properties.end(), [&](item i) {
			auto ret = FALSE;

			if (retval.empty()) {
				if (wcscmp(i.first.data(), key) == 0) {
					retval.assign(i.second);
					ret = TRUE;
				}
			}
			return ret;
		});
	}


	return retval;
}

/// <summary>
/// Processes the element recursively.
/// </summary>
/// <param name="node">The node.</param>
void ConfigReader::ProcessElementRecursively(IXMLDOMNodePtr& node) {
	long childrenCount = 0;
	IXMLDOMNodePtr childNode;
	IXMLDOMNodeListPtr children;

	CoInitialize(nullptr);

	if (SUCCEEDED(node->get_childNodes(&children)) && SUCCEEDED(children->get_length(&childrenCount)) && childrenCount > 0) {
		for (auto nCount = 0; nCount < childrenCount; nCount++) {
			if (SUCCEEDED(children->get_item(nCount, &childNode))) {
				ExtractInformationFromElement(childNode);
				ProcessElementRecursively(childNode);
			}
		}
	}
	
	CoUninitialize();
}


/// <summary>
/// Parses the configuration file.
/// </summary>
/// <param name="configFile">The configuration file.</param>
/// <returns></returns>
BOOL ConfigReader::ParseConfigFile(const std::wstring& configFile) {
	auto retval = FALSE;
	VARIANT_BOOL success;
	IXMLDOMDocumentPtr pDocPtr;
	IXMLDOMNodePtr selectedNode; 

	CoInitialize(nullptr);

	pDocPtr.CreateInstance("Msxml2.DOMDocument.6.0");
	
	if (SUCCEEDED(pDocPtr->load(_variant_t(configFile.c_str()), &success))) {
		if (SUCCEEDED(pDocPtr->selectSingleNode(_bstr_t(XmlRootNode), &selectedNode))) {
			ProcessElementRecursively(selectedNode);
			retval = TRUE;
		}
	}

	CoUninitialize();

	return retval;
}

/// <summary>
/// Doeses the configuration file exist.
/// </summary>
/// <param name="hProcess">The h process.</param>
/// <param name="hModules">The h modules.</param>
/// <param name="targetImage">The target image.</param>
/// <returns></returns>
std::wstring ConfigReader::DoesConfigFileExist(const HANDLE& hProcess, std::vector<HMODULE>& hModules, const wchar_t* targetImage) {
	BOOL found = FALSE;
	std::wstring retval;
	wchar_t szDir[_MAX_DIR];
	wchar_t szExt[_MAX_EXT];
	wchar_t szBuffer[MAX_PATH];
	wchar_t szFName[_MAX_FNAME];
	wchar_t szDrive[_MAX_DRIVE];

	std::find_if(hModules.begin(), hModules.end(), [&, this](HMODULE hModule) {
		auto ret = FALSE;

		if (!found && hModule != nullptr  && (GetModuleFileNameEx(hProcess, hModule, szBuffer, Array_Size(szBuffer))) != NULL) {
			size_t cntConverted;
			char szAnsiPath[MAX_PATH];
			_wsplitpath_s(szBuffer, szDrive, Array_Size(szDrive), szDir, Array_Size(szDir), szFName, Array_Size(szFName), szExt, Array_Size(szExt));
			auto imageName = std::wstring(szFName).append(szExt);
			auto configPath = std::wstring(szDrive).append(szDir).append(ConfigFileName);
			wcstombs_s(&cntConverted, szAnsiPath, configPath.data(), configPath.size() );
			std::ifstream configFile(szAnsiPath);

			if (wcscmp(targetImage, imageName.data()) == 0  && configFile.good()) {
				configFile.close();
				retval.assign(configPath);
				found = TRUE;
			} 
		}

		return ret;
	});

	return retval;
}

/// <summary>
/// Gets the thread token.
/// </summary>
/// <returns></returns>
HANDLE ConfigReader::GetThreadToken() {
	HANDLE retval;
	auto flags = TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY;

	if (!OpenThreadToken(GetCurrentThread(), flags, FALSE, &retval)) {
		if (GetLastError() == ERROR_NO_TOKEN) {
			if (ImpersonateSelf(SecurityImpersonation) &&
				!OpenThreadToken(GetCurrentThread(), flags, FALSE, &retval))
				retval = nullptr;
		}
	}
	return retval;
}

/// <summary>
/// Sets the privilege.
/// </summary>
/// <param name="hToken">The h token.</param>
/// <param name="Privilege">The privilege.</param>
/// <param name="bEnablePrivilege">The b enable privilege.</param>
/// <returns></returns>
BOOL ConfigReader::SetPrivilege(HANDLE& hToken, LPCTSTR Privilege, BOOL bEnablePrivilege) {
	LUID luid;
	auto retval = FALSE;
	TOKEN_PRIVILEGES tp = {0};
	DWORD cb = sizeof(TOKEN_PRIVILEGES);

	if (LookupPrivilegeValue(nullptr, Privilege, &luid)) {
		tp.PrivilegeCount = 1;
		tp.Privileges[0].Luid = luid;
		tp.Privileges[0].Attributes = bEnablePrivilege ? SE_PRIVILEGE_ENABLED : 0;
		AdjustTokenPrivileges(hToken, FALSE, &tp, cb, nullptr, nullptr);

		if (GetLastError() == ERROR_SUCCESS)
			retval = TRUE;
	}
	return retval;
}

/// <summary>
/// Locates the configuration file.
/// </summary>
/// <returns></returns>
BOOL ConfigReader::LocateConfigFile() {
	auto retval = FALSE;
	DWORD nModuleCount = 0;
	IXMLDOMDocumentPtr pDocPtr;
	MODULEINFO moduleDetails = {0};
	HANDLE hToken = nullptr, hProcess = nullptr;
	HMODULE hLoadedModules[Max_Loaded_Modules];

	CoInitialize(nullptr);

	if ((hToken = GetThreadToken()) != nullptr && SetPrivilege(hToken, SE_DEBUG_NAME, TRUE)) {
		if ((hProcess = OpenProcess(PROCESS_ALL_ACCESS, TRUE, GetCurrentProcessId())) != nullptr) {
			if ((EnumProcessModulesEx(hProcess,  hLoadedModules, sizeof(hLoadedModules), &nModuleCount, LIST_MODULES_ALL)) != NULL) {
				auto modules = std::vector<HMODULE>(std::begin(hLoadedModules), std::end(hLoadedModules));

			     #ifdef _WIN64
				     nModuleCount = Item_Count(nModuleCount) / 2;
			     #else
				     nModuleCount = Item_Count(nModuleCount);
			     #endif
                 
				 auto config = DoesConfigFileExist(hProcess, modules, TargetImageName);

				 if (!config.empty())
					 retval = ParseConfigFile(config);
			}
			CloseHandle(hProcess);
		}
		SetPrivilege(hToken, SE_DEBUG_NAME, FALSE);
		CloseHandle(hToken);

		CoUninitialize();
	}

	return retval;
}

/// <summary>
/// Extensionses the get.
/// </summary>
/// <returns></returns>
std::vector<ExtInformation>& ConfigReader::Extensions_get() {
	return m_extensions;
}