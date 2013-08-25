// -----------------------------------------------------------------------
// <copyright file="ChunkManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
        public Vector3 SelectedCubePosition;
        private VertexBuffer _highlightVertexBuffer;
        private IndexBuffer _highlightIndexBuffer;
       
       
        private List<List<SmallChunk>> _chunks;
        public GraphicsDevice Device;

        public ChunkManager(GraphicsDevice device)
        {
            _chunks = new List<List<SmallChunk>>();
            Device = device;
            SelectedCubePosition = Vector3.Zero;
            AddChunk(0, 0);
            AddChunk(0, 1);
            AddChunk(0, 2);
            AddChunk(1, 0);
            AddChunk(1, 1);
            AddChunk(1, 2);
            AddChunk(2, 0);
            AddChunk(2, 1);
            AddChunk(2, 2);
        }

        public bool IsBlockPassable(Vector3 position)
        {
            return IsBlockPassable((int)position.X, (int)position.Y, (int)position.Z);
        }
        public bool IsBlockPassable(int x, int y, int z)
        {
            if (x < 0 || z < 0 || y < 0)
                return false;
            Cube cube = GetCubeAt(x, y, z, false);
            if (cube == null)
                return false;
            return (cube.Type == 0);
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

                  //int upperY = (int) (IsomiteGlobals.ChunkSize.Y*test.NextDouble());
                  int upperY = IsomiteGlobals.ChunkSize.Y / 4; 
                  for (int cubeY = 0; cubeY < upperY; cubeY++)
                        {
                         AddCubeAt(cubeX, cubeY, cubeZ, 1);
                    }
                }
            }
        }

        public void Update()
        {
            if (InputHelper.IsNewKeyPress(Keys.Delete))
            {
                AddCubeAt((int)SelectedCubePosition.X, (int)SelectedCubePosition.Y, (int)SelectedCubePosition.Z, 0);
            }

            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {

                    SelectedCubePosition.X += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
               
                    SelectedCubePosition.Z += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad7))
            {
                if (SelectedCubePosition.X - 1 >= 0)
                    SelectedCubePosition.X -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad9))
            {
                if (SelectedCubePosition.Z - 1 >= 0)
                    SelectedCubePosition.Z -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.Add))
            {
               
                    SelectedCubePosition.Y += 1;
            }

            if (InputHelper.IsNewKeyPress(Keys.Subtract))
            {
                if (SelectedCubePosition.Y - 1 >= 0)
                    SelectedCubePosition.Y -= 1;
            }

            if (InputHelper.IsNewKeyPress(Keys.Insert))
            {
                AddCubeAt((int)SelectedCubePosition.X, (int)SelectedCubePosition.Y, (int)SelectedCubePosition.Z, 2);
            }

            if (InputHelper.IsNewKeyPress(Keys.Home))
            {
                AddCubeAt((int)SelectedCubePosition.X, (int)SelectedCubePosition.Y, (int)SelectedCubePosition.Z, 1);

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
                highlightData.AddHighlightAt(SelectedCubePosition, outline);
            }

            _highlightVertexBuffer = new VertexBuffer(Device, VertexPositionColor.VertexDeclaration, highlightData.OutlineVertices.Count, BufferUsage.WriteOnly);
            _highlightIndexBuffer = new IndexBuffer(Device, typeof(short), highlightData.OutlineIndices.Count, BufferUsage.WriteOnly);
            _highlightVertexBuffer.SetData(highlightData.OutlineVertices.ToArray());
            _highlightIndexBuffer.SetData(highlightData.OutlineIndices.ToArray());
        }

        public Cube GetCubeAt(Vector3 position, bool isDirtyTouch)
        {
            return GetCubeAt((int)position.X, (int)position.Y, (int)position.Z, isDirtyTouch);
        }

        public Cube GetCubeAt(int x, int y, int z, bool isDirtyTouch)
        {
            
            int cubeX = x % IsomiteGlobals.ChunkSize.X;
            int cubeZ = z % IsomiteGlobals.ChunkSize.Z;

            SmallChunk chunk = GetChunk(x, z);
            if (chunk == null)
                return new Cube(10);

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
            else if (cubeB.Type != 0 && cubeA.Type != 0 && cubeB.Type != 3 && cubeA.Type != 3)
            {
                cubeA.Neighbours = cubeA.Neighbours | direction;
                cubeB.Neighbours = cubeB.Neighbours | opposite;
            }
            else if (cubeA.Type == 0 || cubeA.Type == 3)
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
