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

#include  "../../dbghelp/inc/dbgeng.h"

#define MAX_OUTPUTCALLBACKS_BUFFER 0x1000000  // 1Mb
#define MAX_OUTPUTCALLBACKS_LENGTH 0x0FFFFFF  // 1Mb - 1

class SosWrapper;

/// <summary>
/// 
/// </summary>
/// <seealso cref="IDebugOutputCallbacks" />
class OutputCallbacks : public IDebugOutputCallbacks {
	/// <summary>
	/// The m reference
	/// </summary>
	long m_ref;

	/// <summary>
	/// The m p buffer normal
	/// </summary>
	PCHAR m_pBufferNormal;

	/// <summary>
	/// The m n buffer normal
	/// </summary>
	size_t m_nBufferNormal;

	/// <summary>
	/// The m p buffer error
	/// </summary>
	PCHAR m_pBufferError;

	/// <summary>
	/// The m n buffer error
	/// </summary>
	size_t m_nBufferError;

public:
	/// <summary>
	/// Initializes a new instance of the <see cref="OutputCallbacks"/> class.
	/// </summary>
	OutputCallbacks();

	/// <summary>
	/// Finalizes an instance of the <see cref="OutputCallbacks"/> class.
	/// </summary>
	~OutputCallbacks();

	// IUnknown
	/// <summary>
	/// Adds the reference.
	/// </summary>
	/// <returns></returns>
	STDMETHOD_(ULONG, AddRef)();

	/// <summary>
	/// Releases this instance.
	/// </summary>
	/// <returns></returns>
	STDMETHOD_(ULONG, Release)();

	/// <summary>
	/// Queries the interface.
	/// </summary>
	/// <param name="InterfaceId">The interface identifier.</param>
	/// <param name="Interface">The interface.</param>
	/// <returns></returns>
	STDMETHOD(QueryInterface)(__in REFIID InterfaceId, __out PVOID* Interface);

	// IDebugOutputCallbacks
	/// <summary>
	/// Clears this instance.
	/// </summary>
	void Clear();

	/// <summary>
	/// Buffers the error.
	/// </summary>
	/// <returns></returns>
	PCHAR BufferError();

	/// <summary>
	/// Buffers the normal.
	/// </summary>
	/// <returns></returns>
	PCHAR BufferNormal();

	/// <summary>
	/// Supporteds the mask.
	/// </summary>
	/// <returns></returns>
	ULONG SupportedMask();

	/// <summary>
	/// Outputs the specified mask.
	/// </summary>
	/// <param name="Mask">The mask.</param>
	/// <param name="Text">The text.</param>
	/// <returns></returns>
	STDMETHOD(Output)(__in ULONG Mask, __in PCSTR Text);
	
	/// <summary>
	/// The m p unk
	/// </summary>
	static CComPtr<IDispatch> m_pUnk;
};