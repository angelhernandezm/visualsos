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

// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once
#pragma warning(disable:4192 4251 4834)

#include "ConfigReader.h"
#include "targetver.h"

#ifdef SosWrapper_EXPORT
#define SosWrapper_API __declspec(dllexport)
#else
#define SosWrapper_API __declspec(dllimport)
#endif



#define XmlRootNode L"config"
#define Max_Processes  0x0000400
#define Max_Loaded_Modules 0x0000100
#define LoadSosCommand ".loadby sos clr"
#define ConfigFileName  L"VisualSOS.xml"
#define TargetImageName L"VisualSOS.UI.exe"
#define TargetFrameworkVersion L"v4.0.30319"
#define Item_Count(x) ( x > 0 ? (x/sizeof(x)) : 0)
#define Array_Size(array) (sizeof(array) / sizeof(array[0]))
#define ExtPath ".extpath C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86\\WINXP;C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86\\winext;C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86\\winext\\arcade;C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86\\pri;C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86;C:\\Program Files (x86)\\Windows Kits\\8.1\\Debuggers\\x86\\winext\\arcade;"
