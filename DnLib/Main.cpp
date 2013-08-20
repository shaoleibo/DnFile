#include "DnFile.h"
#include <io.h>
#include <windows.h>
#define LIBEXPORT extern "C" __declspec(dllexport)

LIBEXPORT void SetLogFun( DebugPointer ptrLogFun )
{
	LogFun = ptrLogFun;
}

LIBEXPORT unsigned long GenerateID( const char* pszStr )
{
	return g_objDnFile.GenerateID( pszStr );
}

LIBEXPORT bool OpenDnpFile(const char* pszFile )
{
	return g_objDnFile.OpenFile( pszFile );
}

LIBEXPORT void CloseDnpFile(const char* pszFile)
{
	return g_objDnFile.CloseFile( pszFile );
}

LIBEXPORT HANDLE GetFPtr(const char* pszFile, unsigned long& usFileSize)
{
	FILE * pFile = g_objDnFile.GetFPtr( pszFile, usFileSize );
	//C语言文件句柄向操作系统句柄转换
	return (HANDLE)_get_osfhandle(_fileno( pFile ) );
	//return g_objDnFile.GetFPtr( pszFile, usFileSize );
}

