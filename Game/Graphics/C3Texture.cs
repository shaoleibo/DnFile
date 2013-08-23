using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Graphics
{
    public class C3Texture
    {
        private string filePath { get; private set; }
        public void Load(string path)
        {
            filePath = path;
            
        }

    }
}
