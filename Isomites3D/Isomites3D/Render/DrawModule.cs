// -----------------------------------------------------------------------
// <copyright file="DrawData.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.Render
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DrawModule <T> where T : struct 
    {
        public GraphicsDevice Device;
        public VertexDeclaration VertexDeclaration;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private List<T> _vertices;
        private List<ushort> _indices;
        private const ushort Max = 65535;

        private bool _readyToDraw;

        public DrawModule(GraphicsDevice device, VertexDeclaration vertexDeclaration)
        {
            VertexDeclaration = vertexDeclaration;
            Device = device;

            _readyToDraw = false;

           _vertices = new List<T>();
           _indices = new List<ushort>();
        }

        public bool HasSpace(List<ushort> indices)
        {
            if (_indices.Count > 0)
            {
                // 24 is hard coded as pragmatically was too fucking slow.
                // 24 is the maximum number of unique vertices that will be added at once, 6 faces * 4 vertices
                if (_vertices.Count + 24 >= Max)
                {
                    return false;
                }
            }
            return ((indices.Count + _indices.Count)/3 < Max);
        }

        public bool AddData(List<T> vertices, List<ushort> indices)
        {
            if (HasSpace(indices))
            {
                ushort offset = (ushort)_vertices.Count();

                _vertices.AddRange(vertices);

                foreach (ushort index in indices)
                {
                    _indices.Add((ushort)(index + offset));
                }

                return true;
            }

            return false;
        }

        public void PrepareToDraw()
        {
            if (_vertices.Count == 0 || _indices.Count == 0)
            {
                _readyToDraw = false;
                return;
            }
            _vertexBuffer = new VertexBuffer(Device, VertexDeclaration, _vertices.Count, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_vertices.ToArray());

            _indexBuffer = new IndexBuffer(Device, typeof(ushort), _indices.Count, BufferUsage.WriteOnly);
            _indexBuffer.SetData(_indices.ToArray());

            _indices.Clear();
            _vertices.Clear();

            _readyToDraw = true;
        }

        public void Draw()
        {
            if (_readyToDraw)
            {
                Device.SetVertexBuffer(_vertexBuffer);
                Device.Indices = _indexBuffer;
                Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertexBuffer.VertexCount, 0, _indexBuffer.IndexCount / 3);
            }
        }
    }
}
