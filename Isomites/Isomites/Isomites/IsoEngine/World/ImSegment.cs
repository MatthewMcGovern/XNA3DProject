// -----------------------------------------------------------------------
// <copyright file="ImSegment.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine.Items;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
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
    public class ImSegment
    {
        public ImItemContainer Items;
        public ImSegmentManager ParentSegmentManager;
        public ImRenderSegment[] RenderSegments;
        public GraphicsDevice Device;
        public bool IsLoaded;
        public Vector2 Position;

        public ImSegment(GraphicsDevice device, ImSegmentManager segmentManager, Vector2 position)
        {
            ParentSegmentManager = segmentManager;
            IsLoaded = false;
            Device = device;
            Position = position;
            LoadSegment();
            Items = new ImItemContainer();
        }

        public void AddItemAt(Vector3 position)
        {
            AddItemAt((int) position.X, (int) position.Y, (int) position.Z);
        }

        public void AddItemAt(int x2, int y2, int z2)
        {
            Vector3 position2 = new Vector3(Position.X * ImGlobal.SegmentSize.X + x2, y2, Position.Y * ImGlobal.SegmentSize.Z + z2);
            ImItemTree item = new ImItemTree(position2, this);
            Items.SubItems[item.ItemType.SubIndex].Add(item);

            foreach (Vector3 filledSpace in item.ItemType.OccupiedSpace)
            {
                ParentSegmentManager.MarkWorldPositionAsObstacle((int)(position2.X + filledSpace.X), (int)(position2.Y + filledSpace.Y), (int)(position2.Z + filledSpace.Z));
            }
            /*for (int x = 0; x < 32; x ++)
            {
                for (int z = 0; z < 32; z ++)
                {
                    Vector3 position = new Vector3(Position.X * ImGlobal.SegmentSize.X + x, 4, Position.Y * ImGlobal.SegmentSize.Z + z);
                    ImItemTree test = new ImItemTree(position, this);
                    Items.SubItems[test.ItemType.SubIndex].Add(test);

                    foreach (Vector3 filledSpace in test.ItemType.OccupiedSpace)
                    {
                        ParentSegmentManager.MarkBlockMaskAsObstacle((int)(position.X + filledSpace.X), (int)(position.Y + filledSpace.Y), (int)(position.Z + filledSpace.Z));
                    }
                }
            }*/
        }

        public void LoadSegment()
        {
            RenderSegments = new ImRenderSegment[ImGlobal.NoOfRenderSegments];
            for (int i = 0; i < ImGlobal.NoOfRenderSegments; i++)
            {
                RenderSegments[i] = new ImRenderSegment(Device, this, i);
            }

            RenderSegments[0].Fill(ImBlockHelper.BlockMasks.Soil);
            IsLoaded = true;
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < ImGlobal.NoOfRenderSegments; i++)
            {
                RenderSegments[i].Update(gameTime);
            }
        }
        public void UpdateDrawModules()
        {
            // Get all the vertices required and stick it in a draw module.
            for (int i = 0; i < RenderSegments.Length; i++)
            {
                RenderSegments[i].UpdateDrawModule();
            }
        }

        public bool IsPositionInRange(int x, int y, int z)
        {
            return !(x < 0 || y < 0 || z < 0 || x >= ImGlobal.SegmentSize.X || y >= ImGlobal.SegmentSize.Y ||
                     z >= ImGlobal.SegmentSize.Z);
        }


        public void AddBlockMaskAt(ImBlockMask blockMask, int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                RenderSegments[ImGlobal.RenderSegmentIndices[y]].AddBlockMaskAt(blockMask, x,
                ImGlobal.RenderBlockMaskIndices[y], z);
            }
        }

        public void SetBlockMaskAsObstacle(int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                RenderSegments[ImGlobal.RenderSegmentIndices[y]].SetBlockMaskAsObstacle(x,ImGlobal.RenderBlockMaskIndices[y],z);
            } 
        }

        public void SetBlockMaskAsPassable(int x, int y, int z)
        {
            if (IsPositionInRange(x, y, z))
            {
                RenderSegments[ImGlobal.RenderSegmentIndices[y]].SetBlockMaskAsVisible(x, ImGlobal.RenderBlockMaskIndices[y], z);
            }
        }
        public ImBlockMask GetInternalBlockMaskAt(int x, int y, int z)
        {

            if (!IsPositionInRange(x, y, z))
            {
                // if x or y is too small, get the prev chunk
                // if x or z is too big, get the next chunk
                return ImBlockHelper.BlockMasks.Null;
            }

            return RenderSegments[ImGlobal.RenderSegmentIndices[y]].GetInternalBlockMaskAt(x,
                ImGlobal.RenderBlockMaskIndices[y], z);
        }

        public bool IsInternalBlockSolid(int x, int y, int z)
        {
            return ImBlockHelper.IsBlock(GetInternalBlockMaskAt(x, y, z));
        }

        public bool IsInternalBlockAnObstacle(int x, int y, int z)
        {
            return GetInternalBlockMaskAt(x, y, z).HasFlag(ImBlockMask.IsObstacle);
        }

        public void Draw()
        {
            for (int i = 0; i < RenderSegments.Length; i++)
            {
                RenderSegments[i].Draw();
            }
        }

        public void DrawItems(GraphicsDevice device, Camera3D camera)
        {
            Items.DrawAll(device, camera);
        }
    }
}
