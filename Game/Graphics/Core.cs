using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;

namespace Game.Graphics
{
    public class Core
    {
        
        private static Device _device;
        public static Device Device
        {
            get { return _device; }
        }

        public static void CreateDevice( RenderForm form )
        {
            int width = form.ClientSize.Width;
            int height = form.ClientSize.Height;
            _device = new Device(new Direct3D(), 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters()
            {
                BackBufferWidth = width,
                BackBufferHeight = height
            });

            _device.Viewport = new Viewport(0, 0, width, height, 0.0f, 1.0f);
        }

        public static bool ClearBuffer(bool bZbuffer, bool bTarget, Color4 color)
        {
            UInt32 dwFlags = 0;
            if ( bZbuffer )
            {
                dwFlags |= (UInt32)ClearFlags.ZBuffer;
            }
            if ( bTarget )
            {
                dwFlags |= (UInt32)ClearFlags.Target;
            }
            
            Result hr = _device.Clear((ClearFlags)dwFlags, color, 1.0f, 0);
            return Failed(hr);
        }

        public static bool Failed(Result hr)
        {
            if (hr.IsSuccess)
                return true;
            else if (hr.IsFailure)
                return false;
            return false;
        }

        public static bool Begin3D()
        {
            return _device.BeginScene().IsSuccess;
        }

        public static bool End3D()
        {
            return _device.EndScene().IsSuccess;
        }

        public static bool Filp()
        {
            return _device.Present().IsSuccess;
        }
         




    }
}
