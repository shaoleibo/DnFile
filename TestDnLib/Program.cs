using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace TestDnLib
{
    class Program
    {
        [DllImport("DnLib.dll", CharSet=CharSet.Ansi, EntryPoint="GenerateID", CallingConvention=CallingConvention.Cdecl)]
        public static extern UInt32 GenerateID( string pszStr );

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "OpenDnpFile", CallingConvention = CallingConvention.Cdecl )]
        public static extern bool OpenDnpFile( string pszStr );

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "CloseDnpFile", CallingConvention = CallingConvention.Cdecl )]
        public static extern void CloseDnpFile( string pszStr );

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GetFPtr", CallingConvention = CallingConvention.Cdecl )]
        public static extern SafeFileHandle GetFPtr( string pszStr, ref UInt32 usFileSize );

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "GetMPtr", CallingConvention = CallingConvention.Cdecl )]
        public static extern IntPtr GetMPtr( string pszStr, ref UInt32 usFileSize );

        [DllImport( "DnLib.dll", CharSet = CharSet.Ansi, EntryPoint = "SetLogFun", CallingConvention = CallingConvention.Cdecl )]
        public static extern void SetLogFun( IntPtr logFun );

        public delegate void LogMsg( string msg );
        static void ShowMsg( string msg )
        {
            Console.WriteLine( msg );
        }
        static void Main( string[] args )
        {
            LogMsg dLog = new LogMsg( ShowMsg );
            //Console.WriteLine( OpenDnpFile( "d:/c3/res/c3.dnp" ) );
            SetLogFun( Marshal.GetFunctionPointerForDelegate( dLog  )  );
            bool bOpen = OpenDnpFile( "c3.dnp" );
            if( bOpen )
            {
                Console.WriteLine( "open" );
                UInt32 size = 0;
                string fileName = 
                SafeFileHandle sf = GetFPtr( "c3/mesh/001131040.c3", ref size );
                FileStream fs = new FileStream( sf, FileAccess.ReadWrite );
                BinaryReader br = new BinaryReader( fs );
                //File.WriteAllBytes(br.r)
                
                //Console.WriteLine( "SIZE:" + size );
                //if( sf != null )
                //{
                //    //if( sf.IsClosed )
                //    //{
                //    //    Console.WriteLine( "close" );
                //    //}
                //    if (sf.IsInvalid)
                //    {
                //        Console.WriteLine( "invalid" );
                //    }
                //    Console.WriteLine( "sf!=null" );
                //    FileStream fs = new FileStream( sf, FileAccess.ReadWrite);
                //    //byte[] bytes = new byte[size];
                //    //fs.Read( bytes, 0, (int)size );
                //    //File.WriteAllBytes("test.dds", fs.r)
                //    BinaryReader br = new BinaryReader( fs );
                //    byte[] bytes = br.ReadBytes( (int)size );
                //    br.Close();
                //    File.WriteAllBytes( "test.dds", bytes );
                    
                //}

            }
            CloseDnpFile( "d:/c3/res/c3.dnp" );

        }
    }
}
