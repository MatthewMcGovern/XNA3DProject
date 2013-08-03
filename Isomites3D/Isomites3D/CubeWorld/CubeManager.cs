﻿// -----------------------------------------------------------------------
// <copyright file="CubeWorld.cs" company="Microsoft">
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
    public class CubeManager
    {
        private byte[,,] _cubeIDs;
        private NewCube[,,] _cubes;

        private GraphicsDevice _device;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private List<VertexPositionTexture[]> _cachedVerts;
        private List<VertexPositionTexture> _cachedVerts2;
        private List<short> _cachedIndices2;
        private List<short[]> _cachedIndices;

        public CubeManager(int width, int column, int depth, GraphicsDevice device)
        {
            _device = device;
            _cubeIDs = new byte[width, column, depth];
            _cubes = new NewCube[width, column, depth];
            _cachedVerts = new List<VertexPositionTexture[]>();
            _cachedVerts2 = new List<VertexPositionTexture>();
            _cachedIndices = new List<short[]>();
            _cachedIndices2 = new List<short>();

            for (int x = 0; x < _cubeIDs.GetLength(0); x++)
            {
                for (int y = 0; y < _cubeIDs.GetLength(1); y++)
                {
                    for (int z = 0; z < _cubeIDs.GetLength(2); z++)
                    {
                        _cubeIDs[x, y, z] = 1;
                        _cubes[x, y, z] = new NewCube(0);
                    }
                }
            }

            for (int x = 0; x < _cubeIDs.GetLength(0); x++)
            {
                for (int y = 0; y < _cubeIDs.GetLength(1)/2; y++)
                {
                    for (int z = 0; z < _cubeIDs.GetLength(2); z++)
                    {
                        AddCubeAt(x,y,z, 1);
                    }
                }
            }

           AddCubeAt(10, 10, 10, 2);
           AddCubeAt(10, 11, 10, 2);
           AddCubeAt(10, 12, 10, 2);

           AddCubeAt(11, 12, 10, 2);

           AddCubeAt(12, 12, 10, 2);
           AddCubeAt(12, 11, 10, 2);
           AddCubeAt(12, 10, 10, 2);

           AddCubeAt(10, 10, 12, 2);
           AddCubeAt(10, 11, 12, 2);
           AddCubeAt(10, 12, 12, 2);

           AddCubeAt(12, 12, 12, 2);
           AddCubeAt(12, 11, 12, 2);
           AddCubeAt(12, 10, 12, 2);

           AddCubeAt(10, 12, 11, 2);
           AddCubeAt(12, 12, 11, 2);

           AddCubeAt(11, 12, 12, 2);
        }

        public void AddCubeAt(int x, int y, int z, ushort cubeType)
        {
            NewCube newBlock = new NewCube(cubeType);

            if (z + 1 < _cubes.GetLength(2))
            {
                ResolveCubeConnection(newBlock, _cubes[x, y, z + 1], Connections.South);
                
            }

            if (z - 1 >= 0)
            {
                ResolveCubeConnection(newBlock, _cubes[x, y, z - 1], Connections.North);
                
            }

            if (y - 1 >= 0)
            {
                ResolveCubeConnection(newBlock, _cubes[x, y - 1, z], Connections.Down);
            }

            if (x + 1 < _cubes.GetLength(0))
            {
                ResolveCubeConnection(newBlock, _cubes[x + 1, y, z], Connections.East);
            }

            if (y + 1 < _cubes.GetLength(1))
            {
                ResolveCubeConnection(newBlock, _cubes[x, y + 1, z], Connections.Up);
            }

            if (x - 1 >= 0)
            {
                ResolveCubeConnection(newBlock, _cubes[x - 1, y, z], Connections.West);
            }

            _cubes[x, y, z] = newBlock;
        }

        public void ResolveCubeConnection(NewCube cubeA, NewCube cubeB, Connections direction)
        {
            if (cubeB.Type != 0 && cubeA.Type != 0)
            {
                Connections opposite = ConnectionUtils.GetOppositeBlockMask(direction);
                cubeA.Neighbours = cubeA.Neighbours | direction;
                cubeB.Neighbours = cubeB.Neighbours | opposite;
            }
        }

        public void Update()
        {
            if (_cachedVerts2.Count == 0)
            {
                int offset = 0;
                for (int x = 0; x < _cubeIDs.GetLength(0); x++)
                {
                    for (int y = 0; y < _cubeIDs.GetLength(1); y++)
                    {
                        for (int z = 0; z < _cubeIDs.GetLength(2); z++)
                        {
                            NewCube cube = _cubes[x, y, z];
                            if (cube.Type != 0)
                            {
                                if (!cube.Neighbours.HasFlag(ConnectionUtils.All))
                                {
                                    CubeDrawData data = CubeType.GetById(cube.Type).GetDrawData(offset, new Vector3(x, y, z),  cube.Neighbours);
                                    _cachedVerts2.AddRange(data.Vertices);
                                    _cachedIndices2.AddRange(data.Indices);
                                    offset = data.Offset;
                                }
                            }
                        }
                    }
                }
                _vertexBuffer = new VertexBuffer(_device, VertexPositionTexture.VertexDeclaration, _cachedVerts2.Count, BufferUsage.None);
                _indexBuffer = new IndexBuffer(_device, typeof(short), _cachedIndices2.Count, BufferUsage.WriteOnly);

                _vertexBuffer.SetData(_cachedVerts2.ToArray());
                _indexBuffer.SetData(_cachedIndices2.ToArray());
            }
        }
        public void Updatenew()
        {
            if (_cachedVerts.Count == 0 && _cachedIndices.Count == 0)
            {
                for (int x = 0; x < _cubeIDs.GetLength(0); x++)
                {
                    for (int y = 0; y < _cubeIDs.GetLength(1); y++)
                    {
                        for (int z = 0; z < _cubeIDs.GetLength(2); z++)
                        {
                            NewCube cube = _cubes[x, y, z];
                            if (cube.Type != 0)
                            {
                                if (!cube.Neighbours.HasFlag(ConnectionUtils.All))
                                {
                                    VertexPositionTexture[] verts = CubeType.GetById(cube.Type)
                                        .GetVertices(x, y, z, cube.Neighbours);
                                    short[] indices = CubeType.GetById(_cubeIDs[x, y, z]).GetIndices(cube.Neighbours);

                                    _cachedVerts.Add(verts);
                                    _cachedIndices.Add(indices);
                                }
                            }
                        }
                    }
                }

                _vertexBuffer = new VertexBuffer(_device, VertexPositionTexture.VertexDeclaration, _cachedVerts.Count * _cachedVerts[0].Count(), BufferUsage.None);
                _indexBuffer = new IndexBuffer(_device, typeof(short), _cachedIndices.Count * _cachedIndices[0].Count(), BufferUsage.WriteOnly);
                short[] calcIndices = new short[_cachedIndices.Count * _cachedIndices[0].Count()];
                VertexPositionTexture[] calcVerts = new VertexPositionTexture[_cachedVerts.Count * _cachedVerts[0].Count()];
                int count = 0;
                foreach (VertexPositionTexture[] verts in _cachedVerts)
                {
                    foreach (VertexPositionTexture vert in verts)
                    {
                        calcVerts[count] = vert;
                        count++;
                    }
                }

                short count2 = 0;
                // this isn't 32 anymore! shit
                short indexoffset = 32;
                List<short> ind = new List<short>();
                foreach (short[] indices in _cachedIndices)
                {
                    short offset = (short) ind.Count;
                    foreach (short index in indices)
                    {
                        ind.Add((short)(index + offset));
                        count2++;
                        if (count2 < 0)
                        {
                            break;
                        }
                    }
                    if (count2 < 0)
                    {
                        break;
                    }
                }

                calcIndices = ind.ToArray();
                _vertexBuffer.SetData(calcVerts);
                _indexBuffer.SetData(calcIndices);
            }
        }
        public void UpdateOld()
        {
            if (_cachedVerts.Count == 0 && _cachedIndices.Count == 0)
            {
                
                for (int x = 0; x < _cubeIDs.GetLength(0); x++)
                {
                    for (int y = 0; y < _cubeIDs.GetLength(1); y++)
                    {
                        for (int z = 0; z < _cubeIDs.GetLength(2); z++)
                        {
                            VertexPositionTexture[] verts = CubeType.GetById(_cubeIDs[x, y, z]).GetVertices(x, y, z);
                            short[] indices = CubeType.GetById(_cubeIDs[x, y, z]).GetIndices();

                            _cachedVerts.Add(verts);
                            _cachedIndices.Add(indices);

                        }
                    }
                }

                _vertexBuffer = new VertexBuffer(_device, VertexPositionTexture.VertexDeclaration, _cachedVerts.Count * _cachedVerts[0].Count(), BufferUsage.None);
                _indexBuffer = new IndexBuffer(_device, typeof(short), _cachedIndices.Count * _cachedIndices[0].Count(), BufferUsage.WriteOnly);
                short[] calcIndices = new short[_cachedIndices.Count * _cachedIndices[0].Count()];
                VertexPositionTexture[] calcVerts = new VertexPositionTexture[_cachedVerts.Count * _cachedVerts[0].Count()];
                int count = 0;
                foreach (VertexPositionTexture[] verts in _cachedVerts)
                {
                    foreach (VertexPositionTexture vert in verts)
                    {
                        calcVerts[count] = vert;
                        count++;
                    }
                }

                short count2 = 0;
                short indexoffset = 32;
                foreach (short[] indices in _cachedIndices)
                {
                    int offset = count2 * indexoffset;
                    foreach (short index in indices)
                    {
                        calcIndices[count2] = (short)(index + offset);
                        count2++;
                        if (count2 < 0)
                        {
                            break;
                        }
                    }
                    if (count2 < 2)
                    {
                        break;
                    }
                }
                _vertexBuffer.SetData(calcVerts);
                _indexBuffer.SetData(calcIndices);
            }
        }

        public void Draw(GraphicsDevice device)
        {
            _device.SetVertexBuffer(_vertexBuffer);
            _device.Indices = _indexBuffer;

            _device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertexBuffer.VertexCount, 0, _indexBuffer.IndexCount/3);
           // for (int i = 0; i < _cachedVerts.Count; i++)
            //{
//
  //              device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _cachedVerts2.ToArray(), 0,
    //                _cachedVerts2.ToArray().Count(), _cachedIndices2.ToArray(), 0, _cachedIndices2.Count() / 3,
      //              VertexPositionTexture.VertexDeclaration);
        //    }
        }

        public void DrawDebugInfo(SpriteBatch spriteBatch, SpriteFont font)
        {
            /*spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            int cubeCount = _cubes.GetLength(0)*_cubes.GetLength(1)*_cubes.GetLength(2);
            int vertTotal = cubeCount*24;
            int vertDrawn = _vertexBuffer.VertexCount;
            float vertPercentage = vertDrawn / (float)vertTotal;
            int indicesTotal = cubeCount*36;
            int indicesDrawn = _indexBuffer.IndexCount;
            float indicesPercentage = (indicesDrawn / (float)indicesTotal);
            int trisTotal = cubeCount*12;
            float trisDrawn = indicesDrawn / (float)3;
            float trisPercentage = (trisDrawn/trisTotal);

            string message = "Cubes: " + cubeCount
                             + "\n" + "Verts Drawn: " + vertDrawn + " of " + vertTotal + " (" + vertPercentage + "%)"
                             + "\n" + "Indices Used: " + indicesDrawn + " of " + indicesTotal + " (" + indicesPercentage +
                             "%)"
                             + "\n" + "Tris Drawn: " + trisDrawn + " of " + trisTotal + " (" + trisPercentage + "%)";
            // Verts Total:
            // Indices Used:
            // Indices Total:
            // Tris Used:
            // Tris Total:


            spriteBatch.DrawString(font, message, new Vector2(32, 100), Color.Yellow);
            spriteBatch.End();
             */
        }
    }
}
