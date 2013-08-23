using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Game.Data
{
    public class ChunkHeader
    {
        public const int SIZE_BYTE = 8;
        byte[] chunkID = new byte[4];

        public UInt32 chunkSize;
        public ChunkHeader( BinaryReader br )
        {
            chunkID = br.ReadBytes(4);
            chunkSize = br.ReadUInt32();
        }
        public bool IsPhyFile()
        {
            if (chunkID[0] == 'P' && chunkID[1] == 'H' && chunkID[2] == 'Y')
                return true;
            return false;
        }

    }
}
