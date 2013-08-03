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
        private VertexPositionTexture[] _vertices = new VertexPositionTexture[24];
      
        public Cube()
        {
            // UpFace
            _vertices[0] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, 0.25f), new Vector2(0.125f, 0));
            _vertices[1] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, 0.25f), new Vector2(0f, 0));
            _vertices[2] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, -0.25f), new Vector2(0.125f, 1f));
            _vertices[3] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, -0.25f), new Vector2(0f, 1f));

            // DownFace
            _vertices[12] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, 0.25f), new Vector2(0.25f, 0));
            _vertices[13] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, 0.25f), new Vector2(0.125f, 0));
            _vertices[14] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, -0.25f), new Vector2(0.25f, 1f));
            _vertices[15] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, -0.25f), new Vector2(0.125f, 1f));

            // SouthFace
            _vertices[4] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, 0.25f), new Vector2(0.5f, 0));
            _vertices[5] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, 0.25f), new Vector2(0.625f, 0));
            _vertices[6] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, 0.25f), new Vector2(0.5f, 1f));
            _vertices[7] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, 0.25f), new Vector2(0.625f, 1f));

            // NorthFace
            _vertices[8] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, -0.25f), new Vector2(0.375f, 0));
            _vertices[9] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, -0.25f), new Vector2(0.25f, 0));
            _vertices[10] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, -0.25f), new Vector2(0.375f, 1f));
            _vertices[11] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, -0.25f), new Vector2(0.25f, 1f));

            // EastFace
            _vertices[16] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, 0.25f), new Vector2(0.375f, 0));
            _vertices[17] = new VertexPositionTexture(new Vector3(0.25f, 0.25f, -0.25f), new Vector2(0.5f, 0));
            _vertices[18] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, -0.25f), new Vector2(0.5f, 1f));
            _vertices[19] = new VertexPositionTexture(new Vector3(0.25f, -0.25f, 0.25f), new Vector2(0.375f, 1f));

            // WestFace
            _vertices[20] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, 0.25f), new Vector2(0.625f, 0));
            _vertices[21] = new VertexPositionTexture(new Vector3(-0.25f, 0.25f, -0.25f), new Vector2(0.75f, 0));
            _vertices[22] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, -0.25f), new Vector2(0.75f, 1f));
            _vertices[23] = new VertexPositionTexture(new Vector3(-0.25f, -0.25f, 0.25f), new Vector2(0.625f, 1f));

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

        public VertexPositionTexture[] GetVertices()
        {
            return _vertices;
        }
    }
}
