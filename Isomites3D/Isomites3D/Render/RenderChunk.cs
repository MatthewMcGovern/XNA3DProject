// -----------------------------------------------------------------------
// <copyright file="RenderChunk.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Permissions;
using Isomites3D.CubeWorld;
using Microsoft.Xna.Framework;
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
    public class RenderChunk
    {
        private DrawModule<VertexPositionNormalTexture> _blockDrawModule;
        private DrawModule<VertexPositionColor> _outlineDrawModule;
        private Cube[, ,] _cubes;
        public GraphicsDevice Device;
        private int _chunkY = 0;
        public bool Dirty;

        public RenderChunk(GraphicsDevice device, int chunkY)
        {
            _chunkY = chunkY;
            _cubes = new Cube[IsomiteGlobals.ChunkSize.X,IsomiteGlobals.ChunkSize.Y/16,IsomiteGlobals.ChunkSize.Z];
            Dirty = true;
            Device = device;

            for (int x = 0; x < _cubes.GetLength(0); x++)
            {
                for (int y = 0; y < _cubes.GetLength(1); y++)
                {
                    for (int z = 0; z < _cubes.GetLength(2); z++)
                    {
                        _cubes[x,y,z] = new Cube(0);
                    }
                }
            }

            _blockDrawModule = new DrawModule<VertexPositionNormalTexture>(Device, VertexPositionNormalTexture.VertexDeclaration);
            _outlineDrawModule = new DrawModule<VertexPositionColor>(Device, VertexPositionColor.VertexDeclaration);
        }

        public void UpdateDrawModules(int chunkX, int chunkZ)
        {
            int xOffset = (chunkX*IsomiteGlobals.ChunkSize.X);
            int zOffset = (chunkZ*IsomiteGlobals.ChunkSize.Z);
            int yOffset = (_chunkY * 4);
            for (int x = 0; x < _cubes.GetLength(0); x++)
            {
                for (int y = 0; y < _cubes.GetLength(1); y++)
                {
                    for (int z = 0; z < _cubes.GetLength(2); z++)
                    {
                        Cube cube = _cubes[x, y, z];
                        // No point getting vertices if its air.
                        if (cube.Type != 0 && cube.Type != 3)
                        {
                            // No point getting vertices if its obscured by blocks all around it.
                            if (!cube.Neighbours.HasFlag(ConnectionUtils.All))
                            {
                                // Time to get the cubes draw data.
                                CubeDrawData data = CubeType.GetById(cube.Type)
                                    .GetDrawData(new Vector3(xOffset + x, yOffset + y, zOffset + z), cube.Neighbours);
                                _blockDrawModule.AddData(data.Vertices, data.Indices);
                                _outlineDrawModule.AddData(data.OutlineVertices, data.OutlineIndices);
                            }
                        }
                    }
                }
            }
            _blockDrawModule.PrepareToDraw();
            _outlineDrawModule.PrepareToDraw();
            Dirty = false;
        }

        public Cube GetCube(int x, int y, int z, bool isDirtyTouch)
        {
            if(isDirtyTouch)
                Dirty = true;
            return _cubes[x, y, z];
        }

        public void AddCubeAt(int x, int y, int z, Cube cube)
        {
            Dirty = true;
            _cubes[x, y, z] = cube;
        }

        public void Draw()
        {
            _blockDrawModule.Draw();
            
        }

        public void DrawOutline()
        {
           _outlineDrawModule.Draw();  
        }
    }
}
