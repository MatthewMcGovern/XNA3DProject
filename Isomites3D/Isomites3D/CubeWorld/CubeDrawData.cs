// -----------------------------------------------------------------------
// <copyright file="CubeDrawData.cs" company="Microsoft">
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
    public class CubeDrawData
    {
        public List<VertexPositionTexture> Vertices;
        public List<short> Indices;
        public int Offset;

        public CubeDrawData(int offset = 0)
        {
            Offset = offset;
            Vertices = new List<VertexPositionTexture>();
            Indices = new List<short>();
        }

        public void AddData(VertexPositionTexture[] vertices, short[] indices, Vector3 worldPosition)
        {
            foreach (short index in indices)
            {
                Indices.Add((short)(index + Offset));
            }

            
            for(int currentVertex = 0; currentVertex < vertices.Count(); currentVertex++)
            {
                
                Vertices.Add(new VertexPositionTexture(
                        new Vector3( vertices[currentVertex].Position.X + (0.5f * worldPosition.X),  vertices[currentVertex].Position.Y + (0.5f * worldPosition.Y),  vertices[currentVertex].Position.Z + (0.5f * worldPosition.Z)),
                         vertices[currentVertex].TextureCoordinate));
            }

            Offset += vertices.Count();
        }
    }
}
