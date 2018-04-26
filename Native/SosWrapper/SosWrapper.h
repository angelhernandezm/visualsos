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

#include "stdafx.h"
#include "OutputCallbacks.h"
#include "EventCallback.h"
#include "ConfigReader.h"

class OutputCallbacks;

#pragma region "SosWrapper class"

/// <summary>
/// 
/// </summary>

class SosWrapper_API SosWrapper {
private:
	/// <summary>
	/// The m b is attached
	/// </summary>
	BOOL m_bIsAttached;

	/// <summary>
	/// The m n process identifier
	/// </summary>
	ULONG m_nProcessId;

	/// <summary>
	/// The m b is initialized
	/// </summary>
	BOOL m_bIsInitialized;

	/// <summary>
	/// The m target process
	/// </summary>
	HANDLE m_targetProcess;

	/// <summary>
	/// The m configuration reader
	/// </summary>
	ConfigReader m_configReader;

	/// <summary>
	/// The m p event callback
	/// </summary>
	EventCallback m_pEventCallback;

	/// <summary>
	/// The m p output callback
	/// </summary>
	OutputCallbacks m_pOutputCallback;

	/// <summary>
	/// The m p debug client
	/// </summary>
	CComPtr<IDebugClient> m_pDbgClient;

	/// <summary>
	/// The m p debug control
	/// </summary>
	CComPtr<IDebugControl> m_pDbgControl;

	/// <summary>
	/// The m p runtime host
	/// </summary>
	CComPtr<ICLRRuntimeHost> m_pRuntimeHost;

	/// <summary>
	/// The m extensions
	/// </summary>
	std::vector<ExtInformation> m_extensions;

	/// <summary>
	/// Loads the sos.
	/// </summary>
	/// <returns></returns>
	HRESULT LoadSos();

	/// <summary>
	/// Checks if color is loaded.
	/// </summary>
	/// <param name="pEnumerator">The p enumerator.</param>
	/// <returns></returns>
	BOOL CheckIfClrIsLoaded(const CComPtr<IEnumUnknown>& pEnumerator);

public:

	/// <summary>
	/// Initializes a new instance of the <see cref="SosWrapper" /> class.
	/// </summary>
	SosWrapper();

	/// <summary>
	/// Finalizes an instance of the <see cref="SosWrapper" /> class.
	/// </summary>
	~SosWrapper();

	/// <summary>
	/// Initializes this instance.
	/// </summary>
	void Initialize();

	/// <summary>
	/// Processes the identifier get.
	/// </summary>
	/// <returns></returns>
	const ULONG ProcessId_get();

	/// <summary>
	/// Determines whether [is attached get].
	/// </summary>
	/// <returns></returns>
	bool Is_Attached_get() const;


	/// <summary>
	/// Determines whether [is initialized get].
	/// </summary>
	/// <returns></returns>
	bool IsInitialized_get() const;

	/// <summary>
	/// Runs the command.
	/// </summary>
	/// <param name="szCommand">The sz command.</param>
	/// <param name="bPrivate">The b private.</param>
	/// <returns></returns>
	HRESULT RunCommand(const char* szCommand, BOOL bPrivate);


	/// <summary>
	/// Attaches the or detach.
	/// </summary>
	/// <param name="behavior">The behavior.</param>
	/// <param name="nProcessId">The n process identifier.</param>
	/// <returns></returns>
	HRESULT AttachOrDetach(AttachBehavior behavior, ULONG nProcessId);

	/// <summary>
	/// The m p unk
	/// </summary>
	CComPtr<IDispatch> m_pUnk;
};


#pragma endregion 

#pragma region "Exported functions"

extern "C" {
	SosWrapper_API HRESULT IsInitialized();
	SosWrapper_API HRESULT IsDebuggingEngineAttached();
	SosWrapper_API HRESULT ManageDebuggingEngine(UINT option);
	SosWrapper_API HRESULT AttachOrDetach(int attachBehavior, ULONG pid);
	SosWrapper_API HRESULT RunCommand(const char* szCommand, BOOL bPrivate);
}

#pragma endregion 