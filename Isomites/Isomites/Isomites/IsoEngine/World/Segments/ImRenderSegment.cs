// -----------------------------------------------------------------------
// <copyright file="RenderSegment.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
using Isomites.IsoEngine.Block;
using Isomites.IsoEngine.Debug;
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
            DebugLog.Log("Updating RenderChunk: (" + ParentRenderSegment.Position.X + ", " + _heightOffset + ", " + ParentRenderSegment.Position.Y+ ");");
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        if (Blocks[x,y,z] != ImBlockHelper.BlockMasks.Air &&  Blocks[x,y,z] != ImBlockHelper.BlockMasks.AirBlocked)
                        {
                            if (y == 5)
                            {
                                bool test = true;
                            }
                            ImBlockVertexData currentVertextData = new ImBlockVertexData();
                            Vector3 blockLocation = new Vector3(x, y, z) + _locationOffset;
                            if (ImBlockHelper.IsBlock(Blocks[x, y, z]))
                            {
                                currentVertextData = ImBlockVertexData.GetCopyOfBlockMasBlockVertexData(Blocks[x, y, z]);
                            }
                            else if (ImBlockHelper.IsRampBlock(Blocks[x, y, z]))
                            {
                                currentVertextData = ImBlockRampVertexData.GetBlockMaskVertexData(Blocks[x, y, z]);
                            }
                            
                            currentVertextData.TranslateToWorldLocation(blockLocation);

                            // Following checks all the face blocking cubes around the current location to see if we'll add the vertices to the buffer or not.
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.Up)), Blocks[x,y,z], ImDirection.Up))
                            {
                                _blockDrawModule.AddData(currentVertextData.UpVertices.ToList(),
                                    currentVertextData.UpIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.Down)), Blocks[x, y, z], ImDirection.Down))
                            {
                                _blockDrawModule.AddData(currentVertextData.DownVertices.ToList(),
                                    currentVertextData.DownIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.North)), Blocks[x, y, z], ImDirection.North))
                            {
                                _blockDrawModule.AddData(currentVertextData.NorthVertices.ToList(),
                                    currentVertextData.NorthIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.East)), Blocks[x, y, z], ImDirection.East))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.EastVertices.ToList(),
                                    currentVertextData.EastIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.South)), Blocks[x, y, z], ImDirection.South))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.SouthVertices.ToList(),
                                    currentVertextData.SouthIndices.ToList());
                            }
                            if (!ImBlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new ImSegmentLocation(blockLocation + ImDirection.West)), Blocks[x, y, z], ImDirection.West))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.WestVertices.ToList(),
                                    currentVertextData.WestIndices.ToList());
                            }
                        }
                    }
                }
            }
            _blockDrawModule.PrepareToDraw();

            Dirty = false;
        }

        public void DrawBlocks()
        {
            _blockDrawModule.Draw();
        }
    }
}
