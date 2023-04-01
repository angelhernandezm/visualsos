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
#include "OutputCallbacks.h"
#include <atlcomcli.h>
#include <winnt.h>
#include <cstdlib>
#include <debugapi.h>
#include <process.h>
#include <winerror.h>

CComPtr<IDispatch> OutputCallbacks::m_pUnk;

/// <summary>
/// Initializes a new instance of the <see cref="OutputCallbacks"/> class.
/// </summary>
OutputCallbacks::OutputCallbacks() {
	m_ref = 1;
	m_pBufferNormal = nullptr;
	m_nBufferNormal = 0;
	m_pBufferError = nullptr;
	m_nBufferError = 0;
}

/// <summary>
/// Finalizes an instance of the <see cref="OutputCallbacks"/> class.
/// </summary>
OutputCallbacks::~OutputCallbacks() {
	Clear();
}

/// <summary>
/// Queries the interface.
/// </summary>
/// <param name="InterfaceId">The interface identifier.</param>
/// <param name="Interface">The interface.</param>
/// <returns></returns>
STDMETHODIMP OutputCallbacks::QueryInterface(__in REFIID InterfaceId, __out PVOID* Interface) {
	*Interface = nullptr;
	if (IsEqualIID(InterfaceId, __uuidof(IUnknown)) || IsEqualIID(InterfaceId, __uuidof(IDebugOutputCallbacks))) {
		*Interface =  reinterpret_cast<IDebugOutputCallbacks*>(this);
		InterlockedIncrement(&m_ref);
		return S_OK;
	}
	else {
		return E_NOINTERFACE;
	}
}

/// <summary>
/// Adds the reference.
/// </summary>
/// <returns></returns>
STDMETHODIMP_(ULONG) OutputCallbacks::AddRef() {
	return InterlockedIncrement(&m_ref);
}

/// <summary>
/// Releases this instance.
/// </summary>
/// <returns></returns>
STDMETHODIMP_(ULONG) OutputCallbacks::Release() {
	if (InterlockedDecrement(&m_ref) == 0) {
		delete this;
		return 0;
	}
	return m_ref;
}

/// <summary>
/// Outputs the specified mask.
/// </summary>
/// <param name="Mask">The mask.</param>
/// <param name="Text">The text.</param>
/// <returns></returns>
STDMETHODIMP OutputCallbacks::Output(__in ULONG Mask, __in PCSTR Text) {
	// We intercept messages from dbgengine and pass it across to Visual.SOS (.NET application) via COM
	// To reduce impact to UI, we do this from a newly created thread
	unsigned int id;

	CloseHandle(reinterpret_cast<HANDLE>(_beginthreadex(nullptr, NULL, [](void* pData) -> unsigned int {
		auto text = static_cast<std::string*>(pData);
		m_pUnk.Invoke1(_bstr_t("RedirectOutput"), &_variant_t(text->c_str()));
		delete pData;
		return 0;
	}, new std::string(Text), NULL, &id)));

#ifdef _DEBUG
	size_t s;
	wchar_t szBuffer[8192];
	mbstowcs_s(&s, szBuffer, Text, strlen(Text));
	OutputDebugString(szBuffer);
#endif

	if ((Mask & DEBUG_OUTPUT_NORMAL) == DEBUG_OUTPUT_NORMAL) {
		if (m_pBufferNormal == nullptr) {
			m_nBufferNormal = 0;
			m_pBufferNormal = static_cast<PCHAR>(malloc(sizeof(CHAR)*(MAX_OUTPUTCALLBACKS_BUFFER)));
			if (m_pBufferNormal == nullptr) return E_OUTOFMEMORY;
			m_pBufferNormal[0] = '\0';
			m_pBufferNormal[MAX_OUTPUTCALLBACKS_LENGTH] = '\0';
		}

		size_t len = strlen(Text);
		if (len > (MAX_OUTPUTCALLBACKS_LENGTH - m_nBufferNormal)) {
			len = MAX_OUTPUTCALLBACKS_LENGTH - m_nBufferNormal;
		}

		if (len > 0) {
			memcpy(&m_pBufferNormal[m_nBufferNormal], Text, len);
			m_nBufferNormal += len;
			m_pBufferNormal[m_nBufferNormal] = '\0';
		}
	}

	if ((Mask & DEBUG_OUTPUT_ERROR) == DEBUG_OUTPUT_ERROR) {
		if (m_pBufferError == nullptr) {
			m_nBufferError = 0;
			m_pBufferError = static_cast<PCHAR>(malloc(sizeof(CHAR) * (MAX_OUTPUTCALLBACKS_BUFFER)));
			if (m_pBufferError == nullptr) return E_OUTOFMEMORY;
			m_pBufferError[0] = '\0';
			m_pBufferError[MAX_OUTPUTCALLBACKS_LENGTH] = '\0';
		}

		size_t len = strlen(Text);

		if (len >= (MAX_OUTPUTCALLBACKS_LENGTH - m_nBufferError)) {
			len = MAX_OUTPUTCALLBACKS_LENGTH - m_nBufferError;
		}

		if (len > 0) {
			memcpy(&m_pBufferError[m_nBufferError], Text, len);
			m_nBufferError += len;
			m_pBufferError[m_nBufferError] = '\0';
		}
	}

	return S_OK;
}


/// <summary>
/// Clears this instance.
/// </summary>
void OutputCallbacks::Clear() {
	if (m_pBufferNormal) {
		free(m_pBufferNormal);
		m_pBufferNormal = nullptr;
		m_nBufferNormal = 0;
	}
	if (m_pBufferError) {
		free(m_pBufferError);
		m_pBufferError = nullptr;
		m_nBufferError = 0;
	}
}

/// <summary>
/// Supporteds the mask.
/// </summary>
/// <returns></returns>
ULONG OutputCallbacks::SupportedMask() {
	return DEBUG_OUTPUT_NORMAL | DEBUG_OUTPUT_ERROR;
}

/// <summary>
/// Buffers the normal.
/// </summary>
/// <returns></returns>
PCHAR OutputCallbacks::BufferNormal() {
	return m_pBufferNormal;
}

/// <summary>
/// Buffers the error.
/// </summary>
/// <returns></returns>
PCHAR  OutputCallbacks::BufferError() {
	return m_pBufferError;
}