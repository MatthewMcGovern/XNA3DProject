// -----------------------------------------------------------------------
// <copyright file="CubeVertexHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 
    
    public static class CubeVertices
    {
        public static Vector3 CubeSize = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 VertexOffset = new Vector3(CubeSize.X/ 2f, CubeSize.Y / 2f, CubeSize.Z / 2f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
    }

    public static class SmallCubeVertices
    {
        public static Vector3 CubeSize = new Vector3(0.05f, 0.05f, 0.05f);

        public static Vector3 VertexOffset = new Vector3(CubeSize.X / 2f, CubeSize.Y / 2f, CubeSize.Z / 2f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
    }

    public static class LinePoints
    {
        public static Vector3 VertexOffset = new Vector3((CubeVertices.CubeSize.X / 2f) + 0.1f, (CubeVertices.CubeSize.Y / 2f) + 0.1f, (CubeVertices.CubeSize.Z / 2f) + 0.1f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
    }

    public static class LinePointVertices
    {
        public static Vector3 CubeSize = new Vector3(0.1f, 0.1f, 0.1f);
        public static Vector3 VertexOffset = new Vector3(CubeSize.X / 2f, CubeSize.Y / 2f, CubeSize.Z / 2f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
    }
}
