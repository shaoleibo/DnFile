using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Graphics;
using Game.Data;
using System.IO;

namespace Game.Framework
{
    public class C3DObj
    {
        public const int PHY_MAX = 16; //
        private int _phyNum = 0;
        private C3Phy[] _arrPhy = new C3Phy[PHY_MAX];

        public void Create(string filePath)
        {
            for (int i = 0; i < PHY_MAX; i++)
            {
                _arrPhy[i] = null;
            }

            int fileSize = 0;
            FileStream fs = DnFile.GetFileStream(filePath, out fileSize);
            BinaryReader br = new BinaryReader( fs );

            int nOffset = DnFile.C3_HEADER_SIZE;

            while (nOffset < fileSize)
            {
                ChunkHeader ch = new ChunkHeader( br );
                nOffset += ChunkHeader.SIZE_BYTE;
                if (ch.IsPhyFile())
                {
                    C3Phy phy = new C3Phy();
                    phy.Load(br);
                    _arrPhy[_phyNum++] = new C3Phy();
                }
                else
                {
                    br.BaseStream.Seek(ch.chunkSize, SeekOrigin.Current);
                }
                nOffset += (int)ch.chunkSize;
            }
        }
    }
}
