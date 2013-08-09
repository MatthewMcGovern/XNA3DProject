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
    
    // Custom data holder... Was much easier to store it all in an object.
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
            // Init everything
            OutlineOffset = outlineOffset;
            Offset = offset;
            Vertices = new List<VertexPositionNormalTexture>();
            OutlineVertices = new List<VertexPositionColor>();
            OutlineIndices = new List<short>();
            Indices = new List<short>();
        }

        public void AddHighlightAt(Vector3 worldPosition, CubeOutline outline)
        {
            Vector3 direction = outline.StartPosition - outline.EndPosition;
            if (direction.X == CubeVertices.CubeSize.X)
            {
                // right to left
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomRight, outline.StartPosition, worldPosition), Color.Yellow));
            }
            else if (direction.X == -CubeVertices.CubeSize.X)
            {
                // left to right
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopRight, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomRight, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopRight, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Yellow));
            }
            else if (direction.Y == CubeVertices.CubeSize.Y)
            {
                // top to bottom
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopRight, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Yellow));
            }
            else if (direction.Z == CubeVertices.CubeSize.Z)
            {
                // North to south
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.UpBottomRight, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopLeft, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownTopRight, outline.StartPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Yellow));
                OutlineVertices.Add(new VertexPositionColor(HighlightVertices.TranslateToCorner(HighlightVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Yellow));
            }


            foreach (short index in HighlightVertices.Indices)
            {
                OutlineIndices.Add((short)(index + OutlineOffset));
            }

            // Offset always goes up by 8, should probably change this so it isn't a magic number.
            OutlineOffset += 8;
        }

        public void AddLineAt(Vector3 worldPosition, CubeOutline outline)
        {
            // this add the outlines to the Outline buffers
            // World position is used to translate the vertices to the correct location
            // Outline just stores point A to point B

            // First need to figure out which way the line is going
            // TODO: Bottom to Top, South to North
            Vector3 direction = outline.StartPosition - outline.EndPosition;
            if (direction.X == CubeVertices.CubeSize.X)
            {
                // right to left
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomRight, outline.StartPosition, worldPosition), Color.Black));
            }
            else if(direction.X == -CubeVertices.CubeSize.X)
            {
                // left to right
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopRight, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomRight, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopRight, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Black));
            }
            else if (direction.Y == CubeVertices.CubeSize.Y)
            {
                // top to bottom
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopRight, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Black));
            }
            else if (direction.Z == CubeVertices.CubeSize.Z)
            {
                // North to south
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpTopRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.UpBottomRight, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopLeft, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownTopRight, outline.StartPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomLeft, outline.EndPosition, worldPosition), Color.Black));
                OutlineVertices.Add(new VertexPositionColor(SmallCubeVertices.TranslateToCorner(SmallCubeVertices.DownBottomRight, outline.EndPosition, worldPosition), Color.Black));
            }


            foreach (short index in SmallCubeVertices.Indices)
            {
                OutlineIndices.Add((short)(index + OutlineOffset));
            }
            
            // Offset always goes up by 8, should probably change this so it isn't a magic number.
            OutlineOffset += 8;
            ;
        }

        public void AddWorldCubeVertices(VertexPositionNormalTexture[] vertices, short[] indices, Vector3 worldPosition)
        {
            // Takes the vertices of a cubes face and adds them to the buffer
            // Worldposition used to translate vertices to right location

            // This loop generates the normal of the vertices (i.e. the perpendicular angle)
            // This will be used for lighting if I ever add it.
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
                // didn't want to recreate the object for no reason here but accessing the original vertex didn't give the correct results for some reason.
                Vertices.Add(new VertexPositionNormalTexture(
                        new Vector3( vertices[currentVertex].Position.X + ((CubeVertices.CubeSize.X) * worldPosition.X),  vertices[currentVertex].Position.Y + ((CubeVertices.CubeSize.Y) * worldPosition.Y),  vertices[currentVertex].Position.Z + ((CubeVertices.CubeSize.Z) * worldPosition.Z)),
                         vertices[currentVertex].Normal, vertices[currentVertex].TextureCoordinate));
            }

            Offset += vertices.Count();
        }
    }
}
