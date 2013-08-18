using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace TestDnLib
{
    public class DnFile
    {
        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GenerateID", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GenerateID(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "OpenDnpFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool OpenDnpFile(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "CloseDnpFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseDnpFile(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GetFPtr", CallingConvention = CallingConvention.Cdecl)]
        public static extern SafeFileHandle GetFPtr(string pszStr, ref UInt32 usFileSize);
    }
}
