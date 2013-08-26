using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Data;
using SlimDX.Direct3D9;
using SlimDX;
using System.Drawing;

namespace Game.Graphics
{
    public class C3Texture
    {
        public string filePath { get; private set; }
        private ImageInformation imageInfo; 
        public Texture texture { get; private set;}

        public int width { get { return imageInfo.Width;} }
        
        public int height { get { return imageInfo.Height; } }

        public C3Texture(string path, int mipLevel = 3, int colorKey = 0)
        {
            filePath = path;
            int size  = 0;
            byte []fileBytes = DnFile.GetFileBytes( path, out size );//从内存中查找
            if (fileBytes != null)
            {
                texture = Texture.FromMemory(Core.Device, fileBytes, 0, 0, mipLevel, Usage.None, Format.Unknown
                    , Pool.Managed, Filter.Linear, Filter.Linear, colorKey, out imageInfo);

            }
            else
            {
                texture = Texture.FromFile(Core.Device, path, 0, 0, mipLevel, Usage.None, Format.Unknown
    , Pool.Managed, Filter.Linear, Filter.Linear, colorKey, out imageInfo);
            }
          
        }

    }
}
