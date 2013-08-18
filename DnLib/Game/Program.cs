using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Windows;
using SlimDX.Direct3D9;
using System.Drawing;
using Game.Graphics;

namespace Game
{
    static class Program
    {
        struct Vertex
        {
            public Vector4 Position;
            public int Color;
        }

        [STAThread]
        static void Main()
        {

            
            var form = new RenderForm("SlimDX - MiniTri Direct3D9 Sample");
            //var device = new Device(new Direct3D(), 0, DeviceType.Hardware, form.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters()
            //{
            //    BackBufferWidth = form.ClientSize.Width,
            //    BackBufferHeight = form.ClientSize.Height
            //});
            Core.CreateDevice(form);
            Camera cam = new Camera();
            cam.BuildView();
            cam.BuildProjection(form.ClientSize.Width, form.ClientSize.Height);
            var vertices = new VertexBuffer(Core.Device, 5 * 20, Usage.WriteOnly, VertexFormat.None, Pool.Managed);
            vertices.Lock(0, 0, LockFlags.None).WriteRange(new[] {
                new Vertex() { Color = Color.Red.ToArgb(), Position = new Vector4(100.0f, 100.0f, 0.5f, 1.0f) },
                new Vertex() { Color = Color.Blue.ToArgb(), Position = new Vector4(550.0f, 100.0f, 0.5f, 1.0f) },
                new Vertex() { Color = Color.Green.ToArgb(), Position = new Vector4(350.0f, 500.0f, 0.5f, 1.0f) },
                //new Vertex() { Color = Color.Blue.ToArgb(), Position = new Vector4(550.0f, 100.0f, 0.5f, 1.0f) },
                new Vertex() { Color = Color.Green.ToArgb(), Position = new Vector4(450.0f, 500.0f, 0.5f, 1.0f) },
                new Vertex() { Color = Color.Green.ToArgb(), Position = new Vector4(350.0f, 300.0f, 0.5f, 1.0f) }

            });
            vertices.Unlock();

            IndexBuffer indexs = new IndexBuffer(Core.Device, 6 * sizeof(ushort), Usage.WriteOnly, Pool.Managed, true);
            indexs.Lock(0, 0, LockFlags.None).WriteRange<ushort>(new ushort[] { 0, 1, 2, 1, 3, 4 });
            indexs.Unlock();

                var vertexElems = new[] {
                        new VertexElement(0, 0, DeclarationType.Float4, DeclarationMethod.Default, DeclarationUsage.PositionTransformed, 0),
                        new VertexElement(0, 16, DeclarationType.Color, DeclarationMethod.Default, DeclarationUsage.Color, 0),
                                VertexElement.VertexDeclarationEnd
                };

                var vertexDecl = new VertexDeclaration(Core.Device, vertexElems);
                Mesh teapot = Mesh.CreateTeapot(Core.Device);


            MessagePump.Run(form, () =>
            {
                //device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                Core.ClearBuffer(true, true, new Color4(1.0f, 0, 0));
                Core.Begin3D();

                Core.Device.SetStreamSource(0, vertices, 0, 20);
                Core.Device.VertexDeclaration = vertexDecl;
                Core.Device.Indices = indexs;
                //device.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
                Core.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 5, 0, 2);

                teapot.DrawSubset(0);
                Core.End3D();
                Core.Filp();
            });

            foreach (var item in ObjectTable.Objects)
                item.Dispose();

        }
    }
}
