using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace Game.Data
{
    public static class DnFile
    {
        public const int C3_HEADER_SIZE = 16;

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GenerateID", CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 GenerateID(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "OpenDnpFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool OpenDnpFile(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "CloseDnpFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CloseDnpFile(string pszStr);

        [DllImport("DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GetFPtr", CallingConvention = CallingConvention.Cdecl)]
        public static extern SafeFileHandle GetFPtr(string pszStr, ref UInt32 usFileSize);

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GetMPtr", CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr GetMPtr( string pszStr, ref UInt32 usFileSize );

        /// <summary>
        /// 打开一个C3文件，并获取去掉开头标识的文件流
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static FileStream GetFileStream(string fileName, out int fileSize)
        {
            UInt32 size = 0;
            SafeFileHandle handle = GetFPtr(fileName, ref size);
            FileStream fs = new FileStream(handle, FileAccess.Read);
            fs.Seek( C3_HEADER_SIZE, SeekOrigin.Current );
            fileSize = (int)size;
            return fs;
        }

        /// <summary>
        /// 打开一个C3文件，并获取去掉开头标识的字节数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static byte[] GetFileBytes( string fileName, out int fileSize )
        {
            UInt32 size = 0;
            IntPtr intPtr = GetMPtr( fileName, ref size );
            byte[] fileBytes;
            if( intPtr.ToInt32()==0 )
            {
                fileSize = 0;
                return null;
            }
            fileSize = (int)size;
            fileBytes = new byte[fileSize];
            Marshal.Copy( intPtr, fileBytes, 0, fileSize );
            return fileBytes;
        }


    }
}
