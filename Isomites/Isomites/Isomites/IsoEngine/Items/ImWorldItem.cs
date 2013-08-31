// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine;
using Isomites.IsoEngine.World;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImWorldItem
    {
        public Vector3 WorldGridLocation;
        public ImItemType ItemType;
        protected Vector3 _renderOffset;
        public Vector3 Scale;
        public float Rotation;
        public ImSegment ParentSegment;

        public ImWorldItem(Vector3 position, ImSegment segment)
        {
            ParentSegment = segment;
            WorldGridLocation = position;
            Scale = new Vector3(1,1,1);
        }

        public bool DoesObjectLieWithinPosition(int x, int y, int z)
        {
            Vector3 toTest = new Vector3(x, y, z);
            if (WorldGridLocation == toTest)
                return true;

            foreach (Vector3 filledSpace in ItemType.OccupiedSpace)
            {
                if (filledSpace + WorldGridLocation == toTest)
                    return true;
            }

            return false;
        }

        public void Remove()
        {
            ParentSegment.Items.SubItems[ItemType.SubIndex].Remove(this);

            foreach (Vector3 filledSpace in ItemType.OccupiedSpace)
            {
                ParentSegment.ParentSegmentManager.ClearItemsObstacleFlag(new ImSegmentLocation(filledSpace + WorldGridLocation));
            }
        }

        public void Draw(GraphicsDevice device, Camera3D camera)
        {
            ItemType.RenderBasic.Draw(device, camera, WorldGridLocation, _renderOffset, Rotation, Scale);
        }
    }
}
