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
#include  "../../dbghelp/inc/dbgeng.h"

/// <summary>
/// 
/// </summary>
/// <seealso cref="IDebugEventCallbacks" />
class EventCallback : public IDebugEventCallbacks {		 
private:
	/// <summary>
	/// The m reference
	/// </summary>
	long m_ref;

public:
	/// <summary>
	/// Initializes a new instance of the <see cref="EventCallback"/> class.
	/// </summary>
	EventCallback();

	/// <summary>
	/// Finalizes an instance of the <see cref="EventCallback"/> class.
	/// </summary>
	~EventCallback();

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

	// IDebugEventCallbacks
/// <summary>
/// Exits the thread.
/// </summary>
/// <param name="ExitCode">The exit code.</param>
/// <returns></returns>
	STDMETHOD(ExitThread)(__in ULONG ExitCode);

	/// <summary>
	/// Sessions the status.
	/// </summary>
	/// <param name="Status">The status.</param>
	/// <returns></returns>
	STDMETHOD(SessionStatus)(__in ULONG Status);

	/// <summary>
	/// Exits the process.
	/// </summary>
	/// <param name="ExitCode">The exit code.</param>
	/// <returns></returns>
	STDMETHOD(ExitProcess)(__in ULONG ExitCode);

	/// <summary>
	/// Gets the interest mask.
	/// </summary>
	/// <param name="Mask">The mask.</param>
	/// <returns></returns>
	STDMETHOD(GetInterestMask)(__out PULONG Mask);

	/// <summary>
	/// Breakpoints the specified bp.
	/// </summary>
	/// <param name="Bp">The bp.</param>
	/// <returns></returns>
	STDMETHOD(Breakpoint)(__in PDEBUG_BREAKPOINT Bp);

	/// <summary>
	/// Systems the error.
	/// </summary>
	/// <param name="Error">The error.</param>
	/// <param name="Level">The level.</param>
	/// <returns></returns>
	STDMETHOD(SystemError)(__in ULONG Error, __in ULONG Level);

	/// <summary>
	/// Changes the state of the engine.
	/// </summary>
	/// <param name="Flags">The flags.</param>
	/// <param name="Argument">The argument.</param>
	/// <returns></returns>
	STDMETHOD(ChangeEngineState)(__in ULONG Flags, __in ULONG64 Argument);

	/// <summary>
	/// Changes the state of the symbol.
	/// </summary>
	/// <param name="Flags">The flags.</param>
	/// <param name="Argument">The argument.</param>
	/// <returns></returns>
	STDMETHOD(ChangeSymbolState)(__in ULONG Flags, __in ULONG64 Argument);

	/// <summary>
	/// Changes the state of the debuggee.
	/// </summary>
	/// <param name="Flags">The flags.</param>
	/// <param name="Argument">The argument.</param>
	/// <returns></returns>
	STDMETHOD(ChangeDebuggeeState)(__in ULONG Flags, __in ULONG64 Argument);

	/// <summary>
	/// Unloads the module.
	/// </summary>
	/// <param name="ImageBaseName">Name of the image base.</param>
	/// <param name="BaseOffset">The base offset.</param>
	/// <returns></returns>
	STDMETHOD(UnloadModule)(__in_opt PCSTR ImageBaseName, __in ULONG64 BaseOffset);

	/// <summary>
	/// Exceptions the specified exception.
	/// </summary>
	/// <param name="Exception">The exception.</param>
	/// <param name="FirstChance">The first chance.</param>
	/// <returns></returns>
	STDMETHOD(Exception)(__in PEXCEPTION_RECORD64 Exception, __in ULONG FirstChance);

	/// <summary>
	/// Creates the thread.
	/// </summary>
	/// <param name="Handle">The handle.</param>
	/// <param name="DataOffset">The data offset.</param>
	/// <param name="StartOffset">The start offset.</param>
	/// <returns></returns>
	STDMETHOD(CreateThread)(__in ULONG64 Handle, __in ULONG64 DataOffset, __in ULONG64 StartOffset);

	/// <summary>
	/// Loads the module.
	/// </summary>
	/// <param name="ImageFileHandle">The image file handle.</param>
	/// <param name="BaseOffset">The base offset.</param>
	/// <param name="ModuleSize">Size of the module.</param>
	/// <param name="ModuleName">Name of the module.</param>
	/// <param name="ImageName">Name of the image.</param>
	/// <param name="CheckSum">The check sum.</param>
	/// <param name="TimeDateStamp">The time date stamp.</param>
	/// <returns></returns>
	STDMETHOD(LoadModule)(__in ULONG64 ImageFileHandle, __in ULONG64 BaseOffset, __in ULONG ModuleSize,
		__in_opt PCSTR ModuleName, __in_opt PCSTR ImageName, __in ULONG CheckSum, __in ULONG TimeDateStamp);

	/// <summary>
	/// Creates the process.
	/// </summary>
	/// <param name="ImageFileHandle">The image file handle.</param>
	/// <param name="Handle">The handle.</param>
	/// <param name="BaseOffset">The base offset.</param>
	/// <param name="ModuleSize">Size of the module.</param>
	/// <param name="ModuleName">Name of the module.</param>
	/// <param name="ImageName">Name of the image.</param>
	/// <param name="CheckSum">The check sum.</param>
	/// <param name="TimeDateStamp">The time date stamp.</param>
	/// <param name="InitialThreadHandle">The initial thread handle.</param>
	/// <param name="ThreadDataOffset">The thread data offset.</param>
	/// <param name="StartOffset">The start offset.</param>
	/// <returns></returns>
	STDMETHOD(CreateProcess)(__in ULONG64 ImageFileHandle, __in ULONG64 Handle, __in ULONG64 BaseOffset,
		__in ULONG  ModuleSize, __in_opt  PCSTR ModuleName, __in_opt  PCSTR ImageName, __in ULONG CheckSum,
		__in ULONG TimeDateStamp, __in ULONG64 InitialThreadHandle, __in ULONG64 ThreadDataOffset,
		__in ULONG64 StartOffset);
};
