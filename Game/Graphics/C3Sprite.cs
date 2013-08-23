using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Game.Graphics
{
    public class C3Sprite
    {
        struct SpriteVertex
        {
            public float x, y, z;     		// 屏幕坐标
            public float rhw;
            public UInt32 color;			// 颜色
            public float u, v;				// 贴图坐标
        };

        private readonly C3Texture _texture;
        SpriteVertex []_arrVertex;
        Rectangle source;

        private void CalcCoor()
        {
            float minU = (float)source.Left / _texture.width;
            float minV = (float)source.Top / _texture.height;
            float maxU = (float)source.Right / _texture.width;
            float maxV = (float)source.Bottom / _texture.height;

            _arrVertex[0].u = minU;
            _arrVertex[0].v = minV;

            _arrVertex[1].u = minU;
            _arrVertex[1].v = maxV;

            _arrVertex[2].u = maxU;
            _arrVertex[2].v = minV;

            _arrVertex[2].u = maxU;
            _arrVertex[2].v = maxV;

            _arrVertex[0].x = 
        }

        public C3Sprite( C3Texture texture )
        {
            _arrVertex = new SpriteVertex[4];
            _texture = texture;
            source = new Rectangle( 0, 0, _texture.width, _texture.height );

            _arrVertex[0].u = 0.0f;
            _arrVertex[0].v = 0.0f;

            _arrVertex[1].u = 0.0f;
            _arrVertex[1].v = ;

            _arrVertex[2].u = 1.0f;
            _arrVertex[2].v = 0.0f;

            _arrVertex[3].u = 1.0f;
            _arrVertex[3].v = 1.0f;

            _arrVertex[0].x = 0.0f;
            _arrVertex[0].y = 0.0f;
        }

        public void Draw( int x, int y )
        {

        }
    }
}
