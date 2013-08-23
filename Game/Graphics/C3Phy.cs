using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;

namespace Game.Graphics
{
    using SlimDX.Direct3D9;
    using DWORD = UInt32;
    using WORD = UInt16;
    using Microsoft.Win32.SafeHandles;
    using System.IO;
    using System.Drawing;

    public class C3Phy
    {
        public const int _BONE_MAX_ = 2;
        public const int _MORPH_MAX_ = 4;

        public const int VERTERX_SIZE = 24;
        public struct PhyOutVertex
        {
            public float x, y, z;
            //float							nx, ny, nz;
            public DWORD color;
            public float u, v;
        };

        public class PhyVertex
        {
            public Vector3						    []pos = new Vector3[_MORPH_MAX_];
	        public float							u, v;
	       //float							nx, ny, nz; //没有法线
	        public DWORD							color;
	        public DWORD							[]index = new DWORD[_BONE_MAX_];
	        public float							[]weight = new float[_BONE_MAX_]; //
            public PhyVertex( BinaryReader br )
            {
                for (int i = 0; i < _MORPH_MAX_; i++)
                {
                    pos[i].X = br.ReadSingle();
                    pos[i].Y = br.ReadSingle();
                    pos[i].Z = br.ReadSingle();
                }
                u = br.ReadSingle();
                v = br.ReadSingle();
                color = br.ReadUInt32();
                for (int i = 0; i < _BONE_MAX_; i++)
                {
                    index[i] = br.ReadUInt32();
                }
                for (int i = 0; i < _BONE_MAX_; i++)
                {
                    weight[i] = br.ReadSingle();
                }
            }
        }

        string name;            // 物件名称
        DWORD dwBlendCount;     // 每个顶点受到多少根骨骼影响
        DWORD dwNVecCount;		// 顶点数(普通顶点)
        DWORD dwAVecCount;		// 顶点数(透明顶点)
        PhyVertex []lpVB;	    // 顶点池(普通顶点/透明顶点)
        VertexBuffer vb;

        DWORD dwNTriCount;		// 多边形数(普通多边形)
        DWORD dwATriCount;		// 多边形数(透明多边形)
        WORD  []lpIB;			// 索引池(普通多边形/透明多边形)
        IndexBuffer ib;

        public void Load( BinaryReader br )
        {
            DWORD temp = br.ReadUInt32();
            char []lpName = new char[temp + 1];
            char []strName = br.ReadChars((int)temp);
            Array.Copy(strName, lpName, temp);
            lpName[temp] = '\0';
            this.name = lpName.ToString();

            this.dwBlendCount = br.ReadUInt32();
            this.dwNVecCount = br.ReadUInt32();
            this.dwAVecCount = br.ReadUInt32();

            int vertexCount = (int)dwNVecCount + (int)dwAVecCount;
            this.vb = new VertexBuffer(Core.Device, vertexCount * VERTERX_SIZE,
                 Usage.WriteOnly, VertexFormat.None, Pool.Managed);
            this.lpVB = new PhyVertex[ vertexCount ];
            for ( int i = 0; i < vertexCount; i++ )
            {
                this.lpVB[i] = new PhyVertex(br);
            }
            DataStream vbs = this.vb.Lock( 0, 0, LockFlags.None );
            for (int i = 0; i < vertexCount; i++)
            {
                PhyOutVertex fvf;
                fvf.u = this.lpVB[i].u;
                fvf.v = this.lpVB[i].v;
                fvf.color = this.lpVB[i].color;
            }
            vbs.Close();
            this.vb.Unlock();

            this.dwNTriCount = br.ReadUInt32();
            this.dwATriCount = br.ReadUInt32();

            //三角形个数乘以3即是索引个数
            int triCount = (int)this.dwNTriCount + (int)this.dwATriCount;
            this.ib = new IndexBuffer( Core.Device, triCount * 3 * 16 , Usage.WriteOnly, Pool.Managed, true );

            DataStream ibs = this.ib.Lock(0, 0, LockFlags.None);
            for (int i = 0; i < triCount * 3; i++)
            {
                WORD idx = br.ReadUInt16();
                ibs.Write<WORD>(idx);
            }
            ibs.Close();

            this.ib.Unlock();
            
        }

    }
}
