// -----------------------------------------------------------------------
// <copyright file="Cube.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Cube
    {
        // Top face vertices
        private VertexPositionNormalTexture[] _vertices = new VertexPositionNormalTexture[24];
      
        public Cube()
        {
            // UpFace
            _vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0));
            _vertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0f, 0));
            _vertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 1f));
            _vertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0f, 1f));

            // DownFace
            _vertices[12] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.25f, 0));
            _vertices[13] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0));
            _vertices[14] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 1f));
            _vertices[15] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 1f));

            // SouthFace
            _vertices[4] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0));
            _vertices[5] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0));
            _vertices[6] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 1f));
            _vertices[7] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 1f));

            // NorthFace
            _vertices[8] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.375f, 0));
            _vertices[9] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0));
            _vertices[10] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.375f, 1f));
            _vertices[11] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 1f));

            // EastFace
            _vertices[16] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0));
            _vertices[17] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.5f, 0));
            _vertices[18] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.5f, 1f));
            _vertices[19] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 1f));

            // WestFace
            _vertices[20] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0));
            _vertices[21] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.75f, 0));
            _vertices[22] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.75f, 1f));
            _vertices[23] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 1f));

        }
        // Top face Indices
        private short[] _topIndices =
        {
                0,
                2,
                1,
                1,
                2,
                3,
                4,
                5,
                6,
                6,
                5,
                7,
                8,
                10,
                9,
                10,
                11,
                9,
                12,
                13,
                14,
                14,
                13,
                15,
                16,
                17,
                18,
                18,
                19,
                16,
                21,
                20,
                22,
                22,
                20,
                23
        };

        public short[] GetIndices()
        {
            return _topIndices;
        }

        public VertexPositionNormalTexture[] GetVertices()
        {
            return _vertices;
        }
    }
}
