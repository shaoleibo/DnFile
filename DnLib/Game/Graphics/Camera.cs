using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlimDX;
using SlimDX.Direct3D9;

namespace Game.Graphics
{
    public class Camera
    {
        Vector3 lpFrom;
        Vector3 lpTo;

        float fNear;		// 近平面
        float fFar;			// 远平面
        float fFov;			// 视域
        public const int _SCR_WIDTH = 1024;
        public const int _SCR_HEIGHT = 768;
        public Camera()
        {
            lpFrom.X = _SCR_WIDTH / 2;
            lpFrom.Y = -1000;
            lpFrom.Z = _SCR_HEIGHT / 2;

            lpTo.X = _SCR_WIDTH / 2;
            lpTo.Y = 0;
            lpTo.Z = _SCR_HEIGHT / 2;

            fNear = 1.0f;
            fFar = 10000.0f;

            fFov = Mathf.AngleToRadian(60.0f);
        }

        /// <summary>
        /// 相机变换
        /// </summary>
        public void BuildView()
        {
            Vector3 up = new Vector3( 0.0f, 0.0f, -1.0f );
            Matrix viewMat = Matrix.LookAtLH( lpFrom, lpTo, up );
            Core.Device.SetTransform(TransformState.View, viewMat);
        } 

        /// <summary>
        /// 正交投影
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void BuildOrtho( float width, float height )
        {
            Matrix orthoMat = Matrix.OrthoLH(width, height, fNear, fFar);
            Core.Device.SetTransform(TransformState.Projection, orthoMat);
        }

        public void BuildProjection( float width, float height )
        {
            Matrix projectionMat = Matrix.PerspectiveFovLH(fFov, width / height, fNear, fFar);
            Core.Device.SetTransform(TransformState.Projection, projectionMat);
        }


    }
}
