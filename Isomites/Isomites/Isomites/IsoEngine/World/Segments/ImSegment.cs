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

        public void SetNull()
        {
            foreach (var imRenderSegment in RenderSegments)
            {
                imRenderSegment.Fill(ImBlockHelper.BlockMasks.Null);
            }
        }

        public void AddItemAt(Vector3 position)
        {
            AddItemAt((int) position.X, (int) position.Y, (int) position.Z);
        }


        // move this to segmentmanager?
        public void AddItemAt(int x2, int y2, int z2)
        {
            Vector3 position2 = new Vector3(Position.X * ImGlobal.SegmentSize.X + x2, y2, Position.Y * ImGlobal.SegmentSize.Z + z2);
            ImItemTree item = new ImItemTree(position2, this);

            if(item.ItemType.MustBePlacedOnFloor)
                if (!ParentSegmentManager.IsLocationObstructed(new ImSegmentLocation(position2 + new Vector3(0, -1, 0))))
                    return;

            Items.SubItems[item.ItemType.SubIndex].Add(item);

            foreach (Vector3 filledSpace in item.ItemType.OccupiedSpace)
            {
                ParentSegmentManager.SetLocationObstructed(new ImSegmentLocation(filledSpace + position2));
            }
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

        public void DrawBlocks()
        {
            for (int i = 0; i < RenderSegments.Length; i++)
            {
                RenderSegments[i].DrawBlocks();
            }
        }

        public void DrawItems(GraphicsDevice device, Camera3D camera)
        {
            Items.DrawAll(device, camera);
        }
    }
}
