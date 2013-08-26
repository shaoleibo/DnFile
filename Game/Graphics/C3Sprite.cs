using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SlimDX.Direct3D9;
using SlimDX;


namespace Game.Graphics
{
    public class C3Sprite
    {
        struct SpriteVertex
        {
            public float x, y, z, rhw;     	// 屏幕坐标
            public int color;			// 颜色
            public float u, v;				// 贴图坐标
        };
        private VertexDeclaration _vertexDecl;
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

            _arrVertex[3].u = maxU;
            _arrVertex[3].v = maxV;

            _arrVertex[0].x = 0;
            _arrVertex[0].y = 0;

            _arrVertex[1].x = 0;
            _arrVertex[1].y = _texture.height;

            _arrVertex[2].x = _texture.width;
            _arrVertex[2].y = 0;

            _arrVertex[3].x = _texture.width;
            _arrVertex[3].y = _texture.height;

        }

        public C3Sprite(C3Texture texture)
        {
            _arrVertex = new SpriteVertex[4];
            for (int i = 0; i < 4; i++)
            {
                _arrVertex[i].z = 0.5f;
                _arrVertex[i].rhw = 1.0f;
                _arrVertex[i].color = new Color4(255, 255, 255, 255).ToArgb();
            }
            _texture = texture;
            source = new Rectangle( 0, 0, _texture.width, _texture.height );
            CalcCoor();

            VertexElement[] vertexElems = new VertexElement[]{
                new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0 ),
                new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0 ),
                new VertexElement(0, 20, DeclarationType.Float2, DeclarationMethod.Default, DeclarationUsage.TextureCoordinate, 0 ),
                VertexElement.VertexDeclarationEnd
            };
            _vertexDecl = new VertexDeclaration(Core.Device, vertexElems);

         }

        public void Draw( int x, int y )
        {
            
            //顶点之间的顺序默认按照顺时针方向进行绘制 忽略裁剪
            Core.Device.SetRenderState( RenderState.CullMode, Cull.None );
            //Core.Device.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Linear);
            //Core.Device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.Linear); 
            Core.Device.VertexDeclaration = _vertexDecl;  
            Core.Device.SetTexture( 0, _texture.texture );                     
            Result hr = Core.Device.DrawUserPrimitives<SpriteVertex>(PrimitiveType.TriangleStrip, 2, _arrVertex );
        }
    }
}
