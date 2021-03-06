// SilentSeleniumCpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <windows.h>


bool ExecuteSilentProcess(wchar_t *lpszPath, wchar_t *lpszParam)
{
	wchar_t szCmd[4096];
	wcscpy_s(szCmd, lpszPath);
	wchar_t * lpszFile = wcsrchr(szCmd, L'\\');
	if (lpszFile == nullptr)
		return false;

	swprintf_s(lpszFile, 4096 - (int)(lpszFile - szCmd), L"\\SilentExecutor.exe -accepteula -d -x -s \"%s\" %s", lpszPath, lpszParam);

	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	ZeroMemory(&si, sizeof(si));
	si.cb = sizeof(si);
	ZeroMemory(&pi, sizeof(pi));

	// Start the child process. 
	if (!CreateProcessW(NULL,   // No module name (use command line)
		szCmd,        // Command line
		nullptr,           // Process handle not inheritable
		nullptr,           // Thread handle not inheritable
		FALSE,          // Set handle inheritance to FALSE
		CREATE_NO_WINDOW,              // No creation flags
		nullptr,           // Use parent's environment block
		nullptr,           // Use parent's starting directory 
		&si,            // Pointer to STARTUPINFO structure
		&pi)           // Pointer to PROCESS_INFORMATION structure
		)
	{
		printf("CreateProcess failed (%d).\n", GetLastError());
		return false;
	}

	// Wait until child process exits.
	WaitForSingleObject(pi.hProcess, INFINITE);

	// Close process and thread handles. 
	CloseHandle(pi.hProcess);
	CloseHandle(pi.hThread);
	return true;
}

int wmain(int argc, wchar_t *argv[])
{
	/* This code is to hide
	* */

	bool interactive = false;
	bool selenium = false;
	if (argc == 2)
	{
		if (wcscmp(argv[1], L"interactive") == 0)
			interactive = true;
		else if (wcscmp(argv[1], L"selenium") == 0)
			selenium = true;
	}
	if (!selenium && !interactive)
	{
		wchar_t szPath[4096];
		int len = 4096;
		if (GetModuleFileNameW(nullptr, szPath, len) <= 0)
			return false;

		ExecuteSilentProcess(szPath, L"selenium");
		return 0;
	}
	//////////end

	// Process for selenium
	Sleep(10000);
    return 0;
}
