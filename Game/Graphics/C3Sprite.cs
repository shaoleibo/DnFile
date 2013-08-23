using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Graphics
{
    public class C3Sprite
    {
        struct SpriteVertex
        {
            float x, y;     		// 屏幕坐标
            float z;
            float rhw;
            UInt32 color;			// 颜色
            float u, v;				// 贴图坐标
        };

        SpriteVertex []_arrVertex;

        public C3Sprite()
        {

        }
    }
}
