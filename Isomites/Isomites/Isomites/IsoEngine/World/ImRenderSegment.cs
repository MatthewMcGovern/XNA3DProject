// -----------------------------------------------------------------------
// <copyright file="RenderSegment.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsomiteEngine.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImRenderSegment
    {
        public ImSegment ParentRenderSegment;
        public ImBlockMask[,,] Blocks;
        private DrawModule<VertexPositionNormalTexture> _blockDrawModule;
        private GraphicsDevice Device;
        public bool Dirty;
        private byte _heightOffset;
        private Vector3 _locationOffset;
        private bool _IsAllAir;

        public ImRenderSegment(GraphicsDevice device, ImSegment container, int heightOffset)
        {
            ParentRenderSegment = container;
            _heightOffset = (byte) heightOffset;
            Dirty = true;
            _IsAllAir = false;
            Device = device;
            Blocks =
                new ImBlockMask[ImGlobal.RenderSegmentSize.X, ImGlobal.RenderSegmentSize.Y, ImGlobal.RenderSegmentSize.Z
                    ];

            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        Blocks[x, y, z] = ImBlockHelper.BlockMasks.Air;
                    }
                }
            }


            _blockDrawModule = new DrawModule<VertexPositionNormalTexture>(Device,
                VertexPositionNormalTexture.VertexDeclaration);

            _locationOffset = new Vector3(ParentRenderSegment.Position.X*ImGlobal.SegmentSize.X,
                _heightOffset*ImGlobal.RenderSegmentSize.Y, ParentRenderSegment.Position.Y
                                                            *ImGlobal.SegmentSize.Z);
        }

        public void Update(GameTime gameTime)
        {
            if(Dirty)
                UpdateDrawModule();
        }

        public void Fill(ImBlockMask mask)
        {
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        Blocks[x, y, z] = mask;
                    }
                }
            }

            Dirty = true;
        }

        public void UpdateDrawModule()
        {
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        if (ImBlockHelper.IsBlock(Blocks[x, y, z]) && Blocks[x,y,z] != ImBlockHelper.BlockMasks.Air &&  Blocks[x,y,z] != ImBlockHelper.BlockMasks.AirBlocked)
                        {
                            ImBlockVertexData currentVertextData =
                                ImBlockVertexData.GetBlockMaskVertexData(Blocks[x, y, z]);
                            //check up
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int) _locationOffset.X + x,
                                    (int) _locationOffset.Y + y + 1, (int) _locationOffset.Z + z), ImDirection.Up))
                            {
                                // add up vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.UpVertices, new Vector3(_locationOffset.X + x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.UpIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int)_locationOffset.X + x,
                                    (int)_locationOffset.Y + y - 1, (int)_locationOffset.Z + z), ImDirection.Down))
                            {
                                // add down vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.DownVertices, new Vector3(_locationOffset.X +x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.DownIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int)_locationOffset.X + x + 1,
                                    (int)_locationOffset.Y + y, (int)_locationOffset.Z + z), ImDirection.East))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.EastVertices, new Vector3(_locationOffset.X +x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.EastIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int)_locationOffset.X + x - 1,
                                    (int)_locationOffset.Y + y, (int)_locationOffset.Z + z), ImDirection.West))
                            {
                                // add west vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.WestVertices, new Vector3(_locationOffset.X +x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.WestIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int)_locationOffset.X + x,
                                    (int)_locationOffset.Y + y, (int)_locationOffset.Z + z - 1), ImDirection.North))
                            {
                                // add north vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.NorthVertices, new Vector3(_locationOffset.X +x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.NorthIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskObscureFromDirection(
                                ParentRenderSegment.ParentSegmentManager.GetBlockMaskAtWorldPosition((int)_locationOffset.X + x,
                                    (int)_locationOffset.Y + y, (int)_locationOffset.Z + z + 1), ImDirection.South))
                            {
                                // add south vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(ImBlockVertexData.TranslateVerticesToWorldLocation(currentVertextData.SouthVertices, new Vector3(_locationOffset.X +x, _locationOffset.Y + y, _locationOffset.Z + z)).ToList(), currentVertextData.SouthIndices.ToList());
                            }

                            //check down
                            //check north
                            //check east
                            //check south
                            // check west
                        }
                    }
                }
            }
            _blockDrawModule.PrepareToDraw();

            Dirty = false;
        }


        public bool IsPositionInRange(int x, int y, int z)
        {
            return !(x < 0 || y < 0 || z < 0 || x >= Blocks.GetLength(0) || y >= Blocks.GetLength(1) ||
                    z > Blocks.GetLength(2));
        }
        public void AddBlockMaskAt(ImBlockMask blockMask, int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                Blocks[x, y, z] = blockMask;
                Dirty = true;
            }
        }

        public void SetBlockMaskAsObstacle(int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                Blocks[x, y, z] = Blocks[x, y, z] | ImBlockMask.IsObstacle;
            }
        }

        public void SetBlockMaskAsVisible(int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                Blocks[x, y, z] = Blocks[x, y, z] & ~ImBlockMask.IsObstacle;
            }
        }

        public ImBlockMask GetInternalBlockMaskAt(int x, int y, int z)
        {
            // if y < 0, go to render chunk and get chunk below
            // if y > length, go to render chunk and get chunk above, using (offet * renderheight) + y
            // if x < 0, go to render chunk and tell it to go to chunk manager
            // if x > length, go to render chunk and tell it to go to chunk manager, using its (offset * chunkWidth) + x
            // same for z.
            if (!IsPositionInRange(x, y, z))
            {
                return ImBlockHelper.BlockMasks.Null;
            }
            

            // get it from
            return Blocks[x, y, z];
        }

        public bool IsInternalBlockMaskSolid(int x, int y, int z)
        {
            return ImBlockHelper.IsBlock(GetInternalBlockMaskAt(x, y, z));
        }

        public bool IsInternalBlockMaskPassable(int x, int y, int z)
        {
            return GetInternalBlockMaskAt(x, y, z).HasFlag(ImBlockMask.IsObstacle);
        }

        public void Draw()
        {
            _blockDrawModule.Draw();
        }
    }
}
