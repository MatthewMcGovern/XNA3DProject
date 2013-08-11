// -----------------------------------------------------------------------
// <copyright file="ChunkManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ChunkManager
    {
        private Vector3 _selectedCube;
        private VertexBuffer _highlightVertexBuffer;
        private IndexBuffer _highlightIndexBuffer;
       
       
        private List<List<SmallChunk>> _chunks;
        public GraphicsDevice Device;

        public ChunkManager(GraphicsDevice device)
        {
            _chunks = new List<List<SmallChunk>>();
            Device = device;
            _selectedCube = Vector3.Zero;
            AddChunk(0, 0);
            AddChunk(0, 1);
            AddChunk(0, 2);
            AddChunk(0, 3);
            AddChunk(0, 4);
            AddChunk(0, 5);
            AddChunk(0, 6);
            AddChunk(0, 7);
            AddChunk(1, 0);
            AddChunk(1, 1);
            AddChunk(1, 2);
            AddChunk(1, 3);
            AddChunk(1, 4);
            AddChunk(1, 5);
            AddChunk(1, 6);
            AddChunk(1, 7);
            AddChunk(2, 0);
            AddChunk(2, 1);
            AddChunk(2, 2);
            AddChunk(2, 3);
            AddChunk(2, 4);
            AddChunk(2, 5);
            AddChunk(2, 6);
            AddChunk(2, 7);
            AddChunk(3, 0);
            AddChunk(3, 1);
            AddChunk(3, 2);
            AddChunk(3, 3);
            AddChunk(3, 4);
            AddChunk(3, 5);
            AddChunk(3, 6);
            AddChunk(3, 7);
            AddChunk(4, 0);
            AddChunk(4, 1);
            AddChunk(4, 2);
            AddChunk(4, 3);
            AddChunk(4, 4);
            AddChunk(4, 5);
            AddChunk(4, 6);
            AddChunk(4, 7);
            AddChunk(5, 0);
            AddChunk(5, 1);
            AddChunk(5, 2);
            AddChunk(5, 3);
            AddChunk(5, 4);
            AddChunk(5, 5);
            AddChunk(5, 6);
            AddChunk(5, 7);
            AddChunk(6, 0);
            AddChunk(6, 1);
            AddChunk(6, 2);
            AddChunk(6, 3);
            AddChunk(6, 4);
            AddChunk(6, 5);
            AddChunk(6, 6);
            AddChunk(6, 7);
            AddChunk(7, 0);
            AddChunk(7, 1);
            AddChunk(7, 2);
            AddChunk(7, 3);
            AddChunk(7, 4);
            AddChunk(7, 5);
            AddChunk(7, 6);
            AddChunk(7, 7);
        }

        public void AddChunk(int x, int z)
        {
            _chunks.Add(new List<SmallChunk>());
            _chunks[x].Add(new SmallChunk(Device, x, z));

            Random test = new Random();

            for (int cubeX = x * IsomiteGlobals.ChunkSize.X; cubeX < (x + 1) * IsomiteGlobals.ChunkSize.X; cubeX++)
            {
              for (int cubeZ = z * IsomiteGlobals.ChunkSize.Z; cubeZ < (z + 1) * IsomiteGlobals.ChunkSize.Z; cubeZ++)
              {

                        
                        for (int cubeY = 0; cubeY < (int)(IsomiteGlobals.ChunkSize.Y*test.NextDouble()); cubeY++)
                        {
                         AddCubeAt(cubeX, cubeY, cubeZ, 2);
                    }
                }
            }
        }

        public void Update()
        {
            if (InputHelper.IsNewKeyPress(Keys.Delete))
            {
                AddCubeAt((int)_selectedCube.X, (int)_selectedCube.Y, (int)_selectedCube.Z, 0);
            }

            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {

                    _selectedCube.X += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
               
                    _selectedCube.Z += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad7))
            {
                if (_selectedCube.X - 1 >= 0)
                    _selectedCube.X -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad9))
            {
                if (_selectedCube.Z - 1 >= 0)
                    _selectedCube.Z -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.Add))
            {
               
                    _selectedCube.Y += 1;
            }

            if (InputHelper.IsNewKeyPress(Keys.Subtract))
            {
                if (_selectedCube.Y - 1 >= 0)
                    _selectedCube.Y -= 1;
            }

            if (InputHelper.IsNewKeyPress(Keys.Insert))
            {
                AddCubeAt((int)_selectedCube.X, (int)_selectedCube.Y, (int)_selectedCube.Z, 2);
            }

            if (InputHelper.IsNewKeyPress(Keys.Home))
            {
                AddCubeAt((int)_selectedCube.X, (int)_selectedCube.Y, (int)_selectedCube.Z, 1);

            }

            foreach (List<SmallChunk> chunks in _chunks)
            {
                foreach (SmallChunk chunk in chunks)
                {
                    if (chunk.Dirty)
                    {
                        chunk.Update();
                    }
                }
            }

            CubeDrawData highlightData = new CubeDrawData();
            List<CubeOutline> _cubeOutlines = new List<CubeOutline>();
            _cubeOutlines.Add(Outlines.X.UBLtoUBR);
            _cubeOutlines.Add(Outlines.X.DBLtoDBR);
            _cubeOutlines.Add(Outlines.X.UTLtoUTR);
            _cubeOutlines.Add(Outlines.X.DTLtoDTR);

            _cubeOutlines.Add(Outlines.Y.UTLtoDTL);
            _cubeOutlines.Add(Outlines.Y.UTRtoDTR);
            _cubeOutlines.Add(Outlines.Y.UBLtoDBL);
            _cubeOutlines.Add(Outlines.Y.UBRtoDBR);

            _cubeOutlines.Add(Outlines.Z.UTLtoUBL);
            _cubeOutlines.Add(Outlines.Z.UTRtoUBR);
            _cubeOutlines.Add(Outlines.Z.DTLtoDBL);
            _cubeOutlines.Add(Outlines.Z.DTRtoDBR);

            foreach (CubeOutline outline in _cubeOutlines)
            {
                highlightData.AddHighlightAt(_selectedCube, outline);
            }

            _highlightVertexBuffer = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, highlightData.OutlineVertices.Count, BufferUsage.WriteOnly);
            _highlightIndexBuffer = new IndexBuffer(Device, typeof(short), highlightData.OutlineIndices.Count, BufferUsage.WriteOnly);
            _highlightVertexBuffer.SetData(highlightData.OutlineVertices.ToArray());
            _highlightIndexBuffer.SetData(highlightData.OutlineIndices.ToArray());
        }
        public Cube GetCubeAt(int x, int y, int z, bool isDirtyTouch)
        {
            int cubeX = x % IsomiteGlobals.ChunkSize.X;
            int cubeZ = z % IsomiteGlobals.ChunkSize.Z;

            SmallChunk chunk = GetChunk(x, z);
            if (chunk == null)
                return null;

            return chunk.GetCubeAt(cubeX, y, cubeZ, isDirtyTouch);
        }

        public void AddCubeAt(int x, int y, int z, ushort type)
        {
            Cube newBlock = new Cube(type);

            ResolveCubeConnection(newBlock, GetCubeAt(x, y, z + 1, true), Connections.South);
            ResolveCubeConnection(newBlock, GetCubeAt(x, y, z - 1, true), Connections.North);
            ResolveCubeConnection(newBlock, GetCubeAt(x, y - 1, z, true), Connections.Down);
            ResolveCubeConnection(newBlock, GetCubeAt(x + 1, y, z, true), Connections.East);
            ResolveCubeConnection(newBlock, GetCubeAt(x, y + 1, z, true), Connections.Up);
            ResolveCubeConnection(newBlock, GetCubeAt(x - 1, y, z, true), Connections.West);
            
            int cubeX = x % IsomiteGlobals.ChunkSize.X;
            int cubeZ = z % IsomiteGlobals.ChunkSize.Z;

            GetChunk(x, z).AddCubeAt(cubeX, y, cubeZ, newBlock);
        }

        public void ResolveCubeConnection(Cube cubeA, Cube cubeB, Connections direction)
        {
            Connections opposite = ConnectionUtils.GetOppositeBlockMask(direction);
            // Literally just removes the connecting edges on the two cubes.
            if (cubeB == null)
            {
                return;
            }
            else if (cubeB.Type != 0 && cubeA.Type != 0)
            {
                cubeA.Neighbours = cubeA.Neighbours | direction;
                cubeB.Neighbours = cubeB.Neighbours | opposite;
            }
            else if (cubeA.Type == 0)
            {
                cubeB.Neighbours = cubeB.Neighbours & ~opposite;
            }
        }

        public SmallChunk GetChunk(int x, int z)
        {
            if (x < 0 || z < 0)
                return null;

            int chunkX = (int)Math.Floor((double)(x / IsomiteGlobals.ChunkSize.X));
            int chunkZ = (int)Math.Floor((double)(z / IsomiteGlobals.ChunkSize.Z));
            if (chunkX >= _chunks.Count)
                return null;
            if (chunkZ >= _chunks[chunkX].Count)
                return null;
            return _chunks[chunkX][chunkZ];
        }

        public void Draw()
        {
            foreach (var chunks in _chunks)
            {
                foreach (var smallChunk in chunks)
                {
                    smallChunk.Draw();
                }
            }
        }

        public void DrawOutline()
        {
            if (!InputHelper.IsKeyDown(Keys.P))
            {
                foreach (var chunks in _chunks)
                {
                    foreach (var smallChunk in chunks)
                    {
                        smallChunk.DrawOutline();
                    }
                }
            }
            Device.SetVertexBuffer(_highlightVertexBuffer);
            Device.Indices = _highlightIndexBuffer;
            Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _highlightVertexBuffer.VertexCount, 0,
                    _highlightIndexBuffer.IndexCount / 3);

        }
    }
}
