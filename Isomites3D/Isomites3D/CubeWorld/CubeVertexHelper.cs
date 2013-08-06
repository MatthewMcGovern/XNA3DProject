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

    public static class Outlines
    {
        public static class X
        {
            public static CubeOutline UBLtoUBR = new CubeOutline(CubeVertices.UpBottomLeft, CubeVertices.UpBottomRight);
            public static CubeOutline DBLtoDBR = new CubeOutline(CubeVertices.DownBottomLeft, CubeVertices.DownBottomRight);
            public static CubeOutline UTLtoUTR = new CubeOutline(CubeVertices.UpTopLeft, CubeVertices.UpTopRight);
            public static CubeOutline DTLtoDTR = new CubeOutline(CubeVertices.DownTopLeft, CubeVertices.DownTopRight);
        }

        public static class Y
        {
            public static CubeOutline UTLtoDTL = new CubeOutline(CubeVertices.UpTopLeft, CubeVertices.DownTopLeft);
            public static CubeOutline UTRtoDTR = new CubeOutline(CubeVertices.UpTopRight, CubeVertices.DownTopRight);
            public static CubeOutline UBLtoDBL = new CubeOutline(CubeVertices.UpBottomLeft, CubeVertices.DownBottomLeft);
            public static CubeOutline UBRtoDBR = new CubeOutline(CubeVertices.UpBottomRight, CubeVertices.DownBottomRight);
        }

        public static class Z
        {
            public static CubeOutline UTLtoUBL = new CubeOutline(CubeVertices.UpTopLeft, CubeVertices.UpBottomLeft);
            public static CubeOutline UTRtoUBR = new CubeOutline(CubeVertices.UpTopRight, CubeVertices.UpBottomRight);
            public static CubeOutline DTLtoDBL = new CubeOutline(CubeVertices.DownTopLeft, CubeVertices.DownBottomLeft);
            public static CubeOutline DTRtoDBR = new CubeOutline(CubeVertices.DownTopRight, CubeVertices.DownBottomRight);
        }
    }
    public static class SmallCubeVertices
    {
        public static Vector3 CubeSize = new Vector3(0.025f, 0.025f, 0.025f);

        public static short[] Indices = new short[]
           {
                2,
                1,
                0,
                1,
                2,
                3,
                1,
                3,
                7,
                7,
                5,
                1,
                0,
                1,
                4,
                1,
                5,
                4,
                4,
                6,
                2,
                2,
                0,
                4,
                4,
                5,
                6,
                5,
                7,
                6,
                2,
                6,
                7,
                7,
                3,
                2
            };

        public static Vector3 VertexOffset = new Vector3(CubeSize.X / 2f, CubeSize.Y / 2f, CubeSize.Z / 2f);

        public static Vector3 UpTopLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpTopRight = new Vector3(VertexOffset.X, VertexOffset.Y, VertexOffset.Z);
        public static Vector3 UpBottomLeft = new Vector3(-VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 UpBottomRight = new Vector3(VertexOffset.X, VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 DownTopLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownTopRight = new Vector3(VertexOffset.X, -VertexOffset.Y, VertexOffset.Z);
        public static Vector3 DownBottomLeft = new Vector3(-VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);
        public static Vector3 DownBottomRight = new Vector3(VertexOffset.X, -VertexOffset.Y, -VertexOffset.Z);

        public static Vector3 TranslateToCorner(Vector3 vertPosition, Vector3 corner, Vector3 worldPosition)
        {
           return new Vector3(
                            vertPosition.X + ((CubeVertices.CubeSize.X)*worldPosition.X) +
                            corner.X,
                            vertPosition.Y + ((CubeVertices.CubeSize.Y)*worldPosition.Y) +
                            corner.Y,
                            vertPosition.Z + ((CubeVertices.CubeSize.Z) * worldPosition.Z) +
                            corner.Z);
        }

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
