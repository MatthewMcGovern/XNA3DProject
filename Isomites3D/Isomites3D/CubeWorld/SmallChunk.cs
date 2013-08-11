// -----------------------------------------------------------------------
// <copyright file="SmallChunkRenderer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;
using Isomites3D.Render;
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
    public class SmallChunk
    {
        private int _chunkX;
        private int _chunkZ;
        public GraphicsDevice Device;
        private RenderChunk[] _renderChunks;
        public bool Dirty;

        public SmallChunk(GraphicsDevice device, int chunkX, int chunkZ)
        {
            _renderChunks = new RenderChunk[IsomiteGlobals.ChunkSize.Y/4];
            _renderChunks[0] = new RenderChunk(device, 0);
            _renderChunks[1] = new RenderChunk(device, 1);
            _renderChunks[2] = new RenderChunk(device, 2);
            _renderChunks[3] = new RenderChunk(device, 3);
            _renderChunks[4] = new RenderChunk(device, 4);
            _renderChunks[5] = new RenderChunk(device, 5);
            _renderChunks[6] = new RenderChunk(device, 6);
            _renderChunks[7] = new RenderChunk(device, 7);
            _renderChunks[8] = new RenderChunk(device, 8);
            _renderChunks[9] = new RenderChunk(device, 9);
            _renderChunks[10] = new RenderChunk(device, 10);
            _renderChunks[11] = new RenderChunk(device, 11);
            _renderChunks[12] = new RenderChunk(device, 12);
            _renderChunks[13] = new RenderChunk(device, 13);
            _renderChunks[14] = new RenderChunk(device, 14);
            _renderChunks[15] = new RenderChunk(device, 15);


            Device = device;
            Dirty = true;
            _chunkX = chunkX;
            _chunkZ = chunkZ;
        }

        public void Update()
        {
            if (Dirty)
            {
                foreach (var renderChunk in _renderChunks)
                {
                    if(renderChunk.Dirty)
                        renderChunk.UpdateDrawModules(_chunkX, _chunkZ);
                }
                Dirty = false;
            }
        }

        public void AddCubeAt(int x, int y, int z, Cube cube)
        {
            if (IsPosInRange(x, y, z))
            {
                Dirty = true;
                int cubeY = y % (IsomiteGlobals.ChunkSize.Y / 16);
                _renderChunks[(int)Math.Floor((double)(y / 4))].AddCubeAt(x, cubeY, z, cube);
            }
        }

        public Cube GetCubeAt(int x, int y, int z, bool isDirtyTouch)
        {
            if(IsPosInRange(x,y,z))
            {
                if (isDirtyTouch)
                {
                    Dirty = true;
                }
                int cubeY = y % (IsomiteGlobals.ChunkSize.Y/16);
                return _renderChunks[(int)Math.Floor((double)(y / 4))].GetCube(x, cubeY, z, isDirtyTouch);
            }
            return null;
        }

        public bool IsPosInRange(int x, int y, int z)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (z < 0)
                return false;
            if (x >= IsomiteGlobals.ChunkSize.X)
                return false;
            if (y >= IsomiteGlobals.ChunkSize.Y)
                return false;
            if (z >= IsomiteGlobals.ChunkSize.Z)
                return false;

            return true;
        }

        public void Draw()
        {
            foreach (var renderChunk in _renderChunks)
            {
                renderChunk.Draw();
            }
        }

        public void DrawOutline()
        {
            foreach (var renderChunk in _renderChunks)
            {
                renderChunk.DrawOutline();
            }
        }
    }
}
