// -----------------------------------------------------------------------
// <copyright file="CubeDrawData.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
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
        public List<VertexPositionNormalTexture> Vertices;
        public List<short> Indices;
        public List<VertexPositionColor> OutlineVertices;
        public List<short> OutlineIndices;
        public int OutlineOffset;
        public int Offset;

        public CubeDrawData(int offset = 0, int outlineOffset = 0)
        {
            OutlineOffset = outlineOffset;
            Offset = offset;
            Vertices = new List<VertexPositionNormalTexture>();
            OutlineVertices = new List<VertexPositionColor>();
            OutlineIndices = new List<short>();
            Indices = new List<short>();
        }

        public void AddSmallCubeAt(VertexPositionColor[] vertices, short[] indices, Vector3 worldPosition, Vector3 cubePosition)
        {
            for (int currentVertex = 0; currentVertex < vertices.Count(); currentVertex++)
            {
                OutlineVertices.Add(
                    new VertexPositionColor(
                        new Vector3(
                            vertices[currentVertex].Position.X + ((CubeVertices.CubeSize.X)*worldPosition.X) +
                            cubePosition.X,
                            vertices[currentVertex].Position.Y + ((CubeVertices.CubeSize.Y)*worldPosition.Y) +
                            cubePosition.Y,
                            vertices[currentVertex].Position.Z + ((CubeVertices.CubeSize.Z)*worldPosition.Z) +
                            cubePosition.Z), vertices[currentVertex].Color));
            }

            foreach (short index in indices)
            {
                OutlineIndices.Add((short)(index + OutlineOffset));
            }

            OutlineOffset += vertices.Count();
        }

        public void AddWorldCubeVertices(VertexPositionNormalTexture[] vertices, short[] indices, Vector3 worldPosition)
        {
            for (int i = 0; i < indices.Length / 3; i++)
            {
                Vector3 firstvec = vertices[indices[i * 3 + 1]].Position - vertices[indices[i * 3]].Position;
                Vector3 secondvec = vertices[indices[i * 3]].Position - vertices[indices[i * 3 + 2]].Position;
                Vector3 normal = Vector3.Cross(firstvec, secondvec);
                normal.Normalize();
                vertices[indices[i * 3]].Normal += normal;
                vertices[indices[i * 3 + 1]].Normal += normal;
                vertices[indices[i * 3 + 2]].Normal += normal;
            }

            foreach (short index in indices)
            {
                Indices.Add((short)(index + Offset));
            }

            
            for(int currentVertex = 0; currentVertex < vertices.Count(); currentVertex++)
            {
                
                Vertices.Add(new VertexPositionNormalTexture(
                        new Vector3( vertices[currentVertex].Position.X + ((CubeVertices.CubeSize.X) * worldPosition.X),  vertices[currentVertex].Position.Y + ((CubeVertices.CubeSize.Y) * worldPosition.Y),  vertices[currentVertex].Position.Z + ((CubeVertices.CubeSize.Z) * worldPosition.Z)),
                         vertices[currentVertex].Normal, vertices[currentVertex].TextureCoordinate));
            }

            Offset += vertices.Count();
        }
    }
}
