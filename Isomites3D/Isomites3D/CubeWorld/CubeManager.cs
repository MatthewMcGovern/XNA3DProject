// -----------------------------------------------------------------------
// <copyright file="CubeWorld.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Configuration;
using Core;
using Isomites3D.Render;
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
    public class CubeManager
    {
        private Cube[,,] _cubes;

        private Vector3 _selectedCube;

        private BatchedDrawModule<VertexPositionNormalTexture> _batchedDrawModule;
        private BatchedDrawModule<VertexPositionColor> _batchedOutlineDrawModule;
        private GraphicsDevice _device;
        private VertexBuffer _vertexBuffer;
        private VertexBuffer _outlineVertexBuffer;
        private VertexBuffer _highlightVertexBuffer;
        private IndexBuffer _indexBuffer;
        private IndexBuffer _outlineIndexBuffer;
        private IndexBuffer _highlightIndexBuffer;
        private List<VertexPositionNormalTexture> _cachedCubeVerts;
        private List<VertexPositionColor> _cachedOutlineVerts;
        private List<VertexPositionColor> _cachedHighlightVerts;


        private bool _recalcDrawModule = true;

        private List<short> _cachedCubeIndices;
        private List<ushort> _cachedOutlineIndices;
        private List<short> _cachedHighlightIndices; 

        public CubeManager(int width, int column, int depth, GraphicsDevice device)
        {
            _device = device;
            _selectedCube = Vector3.Zero;
            _cubes = new Cube[width, column, depth];

            _batchedDrawModule = new BatchedDrawModule<VertexPositionNormalTexture>(_device, VertexPositionNormalTexture.VertexDeclaration);
            _batchedOutlineDrawModule = new BatchedDrawModule<VertexPositionColor>(_device, VertexPositionColor.VertexDeclaration);

            _cachedCubeVerts = new List<VertexPositionNormalTexture>();
            _cachedOutlineVerts = new List<VertexPositionColor>();
            _cachedHighlightVerts = new List<VertexPositionColor>();
            _cachedCubeIndices = new List<short>();
            _cachedOutlineIndices = new List<ushort>();
            _cachedHighlightIndices = new List<short>();


            _cachedCubeIndices = new List<short>();

            for (int x = 0; x < _cubes.GetLength(0); x++)
            {
                for (int y = 0; y < _cubes.GetLength(1); y++)
                {
                    for (int z = 0; z < _cubes.GetLength(2); z++)
                    {
                        // Loop through full world and create 'air' cubes.
                        _cubes[x, y, z] = new Cube(0);
                    }
                }
            }
            Random rand = new Random();
          
            // Loop through world but only up to half the height and add soil.
            for (int x = 0; x < _cubes.GetLength(0); x++)
            {
                    for (int z = 0; z < _cubes.GetLength(2); z++)
                    {
                        for (int y = 0; y < _cubes.GetLength(1); y++)
                        {
                             AddCubeAt(x,y,z, 1);
                        }
                    }
            }


            // A load of testing cubes for fun.
           /*AddCubeAt(12, 12, 10, 1);
           AddCubeAt(12, 11, 10, 1);
           AddCubeAt(12, 10, 10, 1);
            
          AddCubeAt(10, 10, 12, 1);
           AddCubeAt(10, 11, 12, 1);
           AddCubeAt(10, 12, 12, 1);

           AddCubeAt(12, 12, 12, 1);
           AddCubeAt(12, 11, 12, 1);
           AddCubeAt(12, 10, 12, 1);

           AddCubeAt(10, 12, 11, 1);
           AddCubeAt(12, 12, 11, 1);

           AddCubeAt(11, 12, 12, 1);

           AddCubeAt(5, 10, 5, 1);
           AddCubeAt(5, 11, 5, 1);
           AddCubeAt(5, 12, 5, 1);
           AddCubeAt(6, 12, 5, 1);
           AddCubeAt(7, 12, 5, 1);
           AddCubeAt(7, 11, 5, 1);

           AddCubeAt(17, 10, 17, 1);
           AddCubeAt(18, 10, 18, 1);
           AddCubeAt(0, 10, 0, 2);
           AddCubeAt(1, 10, 0, 2);*/
           

        }

        public void AddCubeAt(int x, int y, int z, ushort cubeType)
        {
            // when adding a cube, check if it has any neighbours and resolve the connection if it does
            // Oh and checks the boundaries too so it doesn't try to access a cube outside the range of world array.
            Cube newBlock = new Cube(cubeType);

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

        public void ResolveCubeConnection(Cube cubeA, Cube cubeB, Connections direction)
        {
            Connections opposite = ConnectionUtils.GetOppositeBlockMask(direction);
            // Literally just removes the connecting edges on the two cubes.
            if (cubeB.Type != 0 && cubeA.Type != 0)
            {
                cubeA.Neighbours = cubeA.Neighbours | direction;
                cubeB.Neighbours = cubeB.Neighbours | opposite;
            }
            if (cubeA.Type == 0)
            {
                cubeB.Neighbours = cubeB.Neighbours & ~opposite;
            }
        }

        public void ClearCaches()
        {
            _recalcDrawModule = true;
            _cachedCubeVerts.Clear();

            _cachedCubeIndices.Clear();
            _cachedOutlineVerts.Clear();
            _cachedOutlineIndices.Clear();
        }

        public void Update()
        {
            if (InputHelper.IsNewKeyPress(Keys.Delete))
            {
                AddCubeAt((int)_selectedCube.X, (int)_selectedCube.Y,(int) _selectedCube.Z, 0);
                ClearCaches();
            }

            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {
                if(_selectedCube.X + 1 < _cubes.GetLength(0))
                    _selectedCube.X += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
                if (_selectedCube.Z + 1 < _cubes.GetLength(2))
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
                if (_selectedCube.Y + 1 < _cubes.GetLength(1))
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
                ClearCaches();
            }

            if (InputHelper.IsNewKeyPress(Keys.Home))
            {
                AddCubeAt((int)_selectedCube.X, (int)_selectedCube.Y, (int)_selectedCube.Z, 1);
                ClearCaches();
            }
            // this is where the magic happens...
            // If we don't have any verts, let's get some.
            if (_recalcDrawModule)
            {
                _batchedDrawModule.Reset();
                _batchedOutlineDrawModule.Reset();
                int offset = 0;
                int outlineOffset = 0;
                for (int x = 0; x < _cubes.GetLength(0); x++)
                {
                    for (int y = 0; y < _cubes.GetLength(1); y++)
                    {
                        for (int z = 0; z < _cubes.GetLength(2); z++)
                        {
                            // Loops though every single cube and gets the cube.
                            Cube cube = _cubes[x, y, z];
                            // No point getting vertices if its air.
                            if (cube.Type != 0)
                            {
                                // No point getting vertices if its obscured by blocks all around it.
                                if (!cube.Neighbours.HasFlag(ConnectionUtils.All))
                                {
                                    // Time to get the cubes draw data.
                                    CubeDrawData data = CubeType.GetById(cube.Type).GetDrawData(new Vector3(x, y, z),  cube.Neighbours);
                                    _batchedDrawModule.AddData(data.Vertices, data.Indices);
                                    _batchedOutlineDrawModule.AddData(data.OutlineVertices, data.OutlineIndices);
                                    // got the data, add it to the buffers
                                    //_cachedCubeVerts.AddRange(data.Vertices);
                                    //_cachedCubeIndices.AddRange(data.Indices);
                                    //_cachedOutlineVerts.AddRange(data.OutlineVertices);
                                    //_cachedOutlineIndices.AddRange(data.OutlineIndices);
                                    //
                                    // Increase offset so indices don't just use the first few vertices -_-
                                    //offset = data.Offset;
                                    //outlineOffset = data.OutlineOffset;
                                }
                            }
                        }
                    }
                }

                

                // VertexBuffer/IndexBuffer are XNA classes for feeding the graphics card cached data.
                // they get created to the exact size required based on the kind of vertex and how many vertices there are
                // Similar for indices except it only accepts 16bit size objects... so shorts... I should probably try ushorts for 2x the indices.
                // TODO: Split my list buffers into smaller lists of no more than 196600 as vertex buffers can only draw 65535 triangles at once!!!
                // TODO: in other words this will error if I have too many cubes :(
                //_vertexBuffer = new VertexBuffer(_device, VertexPositionNormalTexture.VertexDeclaration, _cachedCubeVerts.Count, BufferUsage.None);
                //_indexBuffer = new IndexBuffer(_device, typeof(short), _cachedCubeIndices.Count, BufferUsage.WriteOnly);
                //_outlineIndexBuffer = new IndexBuffer(_device, typeof(short), _cachedOutlineIndices.Count, BufferUsage.WriteOnly);
                //_outlineVertexBuffer = new VertexBuffer(_device, VertexPositionColor.VertexDeclaration, _cachedOutlineVerts.Count, BufferUsage.None);
                _batchedDrawModule.PrepareToDraw();
                _batchedOutlineDrawModule.PrepareToDraw();
                // Set the actual data to the GPU.
                //_vertexBuffer.SetData(_cachedCubeVerts.ToArray());
                //_indexBuffer.SetData(_cachedCubeIndices.ToArray());
                //_outlineIndexBuffer.SetData(_cachedOutlineIndices.ToArray());
                //_outlineVertexBuffer.SetData(_cachedOutlineVerts.ToArray());

                _recalcDrawModule = false;
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

            _highlightVertexBuffer = new VertexBuffer(_device, VertexPositionColor.VertexDeclaration, highlightData.OutlineVertices.Count, BufferUsage.WriteOnly);
            _highlightIndexBuffer = new IndexBuffer(_device, typeof(short), highlightData.OutlineIndices.Count, BufferUsage.WriteOnly);
            _highlightVertexBuffer.SetData(highlightData.OutlineVertices.ToArray());
            _highlightIndexBuffer.SetData(highlightData.OutlineIndices.ToArray());
        }

        public void Draw()
        {
            // Need to tell the graphics card which buffer we are using
            // This is NOT to be confused with SetData
            // This literally is just a pointer to the buffer already in GPU memory.
            // Set Data has a much larger overhead than this.
            //_device.SetVertexBuffer(_vertexBuffer);
            //_device.Indices = _indexBuffer;
            _batchedDrawModule.Draw();
            // Draws all the cube Tris
            //_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _vertexBuffer.VertexCount, 0, _indexBuffer.IndexCount/3);
        }

        public void DrawOutline()
        {
            // Made it optional to show the effect off *shrug*
            // Hold P to disable.
            if (!InputHelper.IsKeyDown(Keys.P))
            {
                // Similar to above, just a different buffer.
               // _batchedOutlineDrawModule.Draw();
            }
            
            /*_device.SetVertexBuffer(_highlightVertexBuffer);
            _device.Indices = _highlightIndexBuffer;
            _device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _highlightVertexBuffer.VertexCount, 0,
                    _highlightIndexBuffer.IndexCount / 3);*/

        }
    }
}
