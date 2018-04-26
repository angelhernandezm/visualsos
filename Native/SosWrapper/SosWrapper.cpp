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
#include "SosWrapper.h"

/// <summary>
/// The g singleton
/// </summary>
SosWrapper* g_Singleton;

#pragma region "Generic lambdas"

auto printException = [&](const std::exception& ex) -> void {
	size_t s;
	wchar_t szBuffer[1024];
	mbstowcs_s(&s, szBuffer, ex.what(), strlen(ex.what()));
	OutputDebugString(szBuffer);
};

#pragma endregion


#pragma region "SosWrapper class"

/// <summary>
/// Loads the sos.
/// </summary>
/// <returns></returns>
HRESULT SosWrapper::LoadSos() {
	HANDLE hProcess;
	auto isLoaded = FALSE;
	auto retval = S_FALSE;
	CComPtr<ICLRMetaHost> pMetaHost;
	CComPtr<IEnumUnknown> pEnumerator;
	CComPtr<ICLRRuntimeInfo> pRuntimeInfo;

	if ((hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, FALSE, GetCurrentProcessId())) != nullptr) {
		if (SUCCEEDED(CLRCreateInstance(CLSID_CLRMetaHost, IID_ICLRMetaHost, (LPVOID*)&pMetaHost))) {
			isLoaded = SUCCEEDED(pMetaHost->EnumerateLoadedRuntimes(hProcess, &pEnumerator)) && CheckIfClrIsLoaded(pEnumerator);
			// If CLR is not loaded we'll proceed to load and start it
			if (!isLoaded &&  SUCCEEDED(pMetaHost->GetRuntime(TargetFrameworkVersion, IID_ICLRRuntimeInfo, (LPVOID*)&pRuntimeInfo))) {
				if (SUCCEEDED(pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_ICLRRuntimeHost, (LPVOID*)&m_pRuntimeHost))) {
					retval = RunCommand(ExtPath, TRUE);
					retval = SUCCEEDED(m_pRuntimeHost->Start()) && SUCCEEDED(RunCommand(LoadSosCommand, TRUE)) ? TRUE : FALSE;
				}
			}
			else {
				retval = SUCCEEDED(RunCommand(ExtPath, TRUE)) && SUCCEEDED(RunCommand(LoadSosCommand, TRUE)) ? TRUE : FALSE;
			}
		}
	}

	CloseHandle(hProcess);

	return retval;
}


/// <summary>
/// Checks if color is loaded.
/// </summary>
/// <param name="pEnumerator">The p enumerator.</param>
/// <returns></returns>
BOOL SosWrapper::CheckIfClrIsLoaded(const CComPtr<IEnumUnknown>& pEnumerator) {
	ULONG fetched = 0;
	DWORD bufferSize;
	auto retval = FALSE;
	wchar_t szBuffer[MAX_PATH];
	CComPtr<ICLRRuntimeInfo> pRuntimeInfo;

	while (SUCCEEDED(pEnumerator->Next(1, (IUnknown **)&pRuntimeInfo, &fetched)) && fetched > 0) {
		if ((SUCCEEDED(pRuntimeInfo->GetVersionString(szBuffer, &bufferSize))))
			if (wcscmp(szBuffer, TargetFrameworkVersion) == 0) {
				retval = TRUE;
				break;
			}
	}

	return retval;
}

/// <summary>
/// Initializes a new instance of the <see cref="SosWrapper"/> class.
/// </summary>
SosWrapper::SosWrapper() {
	m_nProcessId = 0;
	m_bIsInitialized = m_bIsAttached = FALSE;
	Initialize();
}

/// <summary>
/// Finalizes an instance of the <see cref="SosWrapper"/> class.
/// </summary>
SosWrapper::~SosWrapper() {
	// Unload extensions
	if (!m_configReader.Extensions_get().empty())
		std::for_each(m_configReader.Extensions_get().begin(), m_configReader.Extensions_get().end(), [&, this](const ExtInformation& item) {
		m_pDbgControl->RemoveExtension(item.pHandle);
	});

	m_pDbgClient->EndSession(DEBUG_END_ACTIVE_DETACH);
}

/// <summary>
/// Initializes this instance.
/// </summary>
void SosWrapper::Initialize() {
	if (!m_bIsInitialized) {
		CoInitialize(nullptr);

		if (SUCCEEDED(DebugCreate(__uuidof(IDebugClient), (void**)&m_pDbgClient))) {
			if (SUCCEEDED(m_pDbgClient->QueryInterface(__uuidof(IDebugControl), (void**)&m_pDbgControl))) {
				// Load extensions
				std::for_each(m_configReader.Extensions_get().begin(), m_configReader.Extensions_get().end(), [&, this](ExtInformation& item) {
					m_pDbgControl->AddExtension(item.Path.data(), NULL, &item.pHandle);
				});

				m_pUnk.CoCreateInstance(L"VisualSOS.Core.Infrastructure.OutputMarshalling", nullptr);
				m_pDbgClient->SetOutputCallbacks(&m_pOutputCallback);
				m_pDbgClient->SetEventCallbacks(&m_pEventCallback);
				m_bIsInitialized = TRUE;
				OutputCallbacks::m_pUnk = m_pUnk;
			}
		}
		CoUninitialize();
	}

}

/// <summary>
/// Processes the identifier get.
/// </summary>
/// <returns></returns>
const ULONG SosWrapper::ProcessId_get() {
	return m_nProcessId;
}

/// <summary>
/// Determines whether [is attached get].
/// </summary>
/// <returns></returns>
bool SosWrapper::Is_Attached_get() const {
	return m_bIsAttached;
}

/// <summary>
/// Determines whether [is initialized get].
/// </summary>
/// <returns></returns>
bool SosWrapper::IsInitialized_get() const {
	return m_bIsInitialized;
}

/// <summary>
/// Runs the command.
/// </summary>
/// <param name="szCommand">The sz command.</param>
/// <param name="bPrivate">The b private.</param>
/// <returns></returns>
HRESULT SosWrapper::RunCommand(const char* szCommand, BOOL bPrivate) {
	auto retval = S_FALSE;

	if (szCommand != nullptr && strlen(szCommand) > 0) {
		try {
			if (bPrivate)
				retval = m_pDbgControl->Execute(DEBUG_OUTCTL_THIS_CLIENT | DEBUG_OUTCTL_NOT_LOGGED,
					szCommand, DEBUG_EXECUTE_NOT_LOGGED | DEBUG_EXECUTE_NO_REPEAT);
			else retval = m_pDbgControl->Execute(DEBUG_OUTCTL_ALL_CLIENTS, szCommand, DEBUG_EXECUTE_NO_REPEAT);
		}
		catch (std::exception &ex) {
			printException(ex);
		}
	}

	return retval;
}

/// <summary>
/// Attaches the or detach.
/// </summary>
/// <param name="behavior">The behavior.</param>
/// <param name="nProcessId">The n process identifier.</param>
/// <returns></returns>
HRESULT SosWrapper::AttachOrDetach(AttachBehavior behavior, ULONG nProcessId) {
	HRESULT hr;
	auto retval = S_FALSE;

	// If debugging engine couldn't be initialized it's pointless to try to attach to it so we return
	if (!m_bIsInitialized)
		return retval;

	switch (behavior) {
	case AttachBehavior::Attach:
		if (!m_bIsAttached && nProcessId > 0) {
			if (SUCCEEDED(retval = m_pDbgClient->AttachProcess(NULL, nProcessId, DEBUG_ATTACH_INVASIVE_NO_INITIAL_BREAK))) {
				if ((m_targetProcess = OpenProcess(PROCESS_ALL_ACCESS, TRUE, nProcessId)) != nullptr && DebugBreakProcess(m_targetProcess)) {
					m_pDbgControl->WaitForEvent(DEBUG_WAIT_DEFAULT, 3000);
					m_pDbgControl->SetInterrupt(DEBUG_INTERRUPT_ACTIVE);
					m_bIsAttached = SUCCEEDED(LoadSos()); 
				}
			}
		}
		break;
	case AttachBehavior::Detach:
		if (m_bIsAttached) {
			retval = m_pDbgClient->DetachProcesses();
			CloseHandle(m_targetProcess);
			m_bIsAttached = false;
		}
		break;
	}
	return retval;
}

#pragma  endregion 

#pragma region "Exported functions"
/// <summary>
/// Initializes the sos wrapper.
/// </summary>
void InitializeSosWrapper() {
	if (g_Singleton == nullptr)
		g_Singleton = new SosWrapper();
}

/// <summary>
/// Determines whether this instance is initialized.
/// </summary>
/// <returns></returns>
SosWrapper_API HRESULT IsInitialized() {
	InitializeSosWrapper();
	return (g_Singleton->IsInitialized_get() ? S_OK : S_FALSE);
}

/// <summary>
/// Determines whether [is debugging engine attached].
/// </summary>
/// <returns></returns>
SosWrapper_API HRESULT IsDebuggingEngineAttached() {
	InitializeSosWrapper();
	return (g_Singleton->Is_Attached_get() ? S_OK : S_FALSE);
}



/// <summary>
/// Attaches the or detach.
/// </summary>
/// <param name="attachBehavior">The attach behavior.</param>
/// <param name="pid">The pid.</param>
/// <returns></returns>
SosWrapper_API HRESULT AttachOrDetach(int attachBehavior, ULONG pid) {
	HRESULT retval = S_FALSE;

	if (g_Singleton != nullptr) {
		retval = g_Singleton->AttachOrDetach((AttachBehavior)attachBehavior, pid);
	}

	return retval;
}

/// <summary>
/// Runs the command.
/// </summary>
/// <param name="szCommand">The sz command.</param>
/// <param name="bPrivate">The b private.</param>
/// <returns></returns>
SosWrapper_API HRESULT RunCommand(const char* szCommand, BOOL bPrivate) {
	HRESULT retval = S_FALSE;

	if (g_Singleton != nullptr) {
		retval = g_Singleton->RunCommand(szCommand, bPrivate);
	}

	return retval;
}

/// <summary>
/// Manages the debugging engine.
/// </summary>
/// <param name="option">The option.</param>
/// <returns></returns>
SosWrapper_API HRESULT ManageDebuggingEngine(UINT option) {
	if (option == DebuggingEngineOption::Start) {
		if (g_Singleton == nullptr)
			g_Singleton = new SosWrapper();
	}
	else if (option == DebuggingEngineOption::Stop) {
		delete g_Singleton;
	}

	return  S_OK;;
}

#pragma endregion 