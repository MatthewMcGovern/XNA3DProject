// -----------------------------------------------------------------------
// <copyright file="SegmentLocation.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine;
using Microsoft.Xna.Framework;

namespace Isomites.IsoEngine.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImSegmentLocation
    {
        public Vector3 WorldLocation;
        public int SegmentX;
        public int SegmentZ;
        public int BlockX;
        public int BlockY;
        public int BlockZ;
        public int RenderSegmentIndex;
        public int RenderSegmentBlockMaskIndex;

        public ImSegmentLocation(Vector3 worldLocation)
        {
            WorldLocation = worldLocation;
            SegmentX = (int) Math.Floor((double) worldLocation.X / ImGlobal.SegmentSize.X);
            SegmentZ = (int)Math.Floor((double) worldLocation.Z / ImGlobal.SegmentSize.Z);
            BlockX = (int) worldLocation.X % ImGlobal.SegmentSize.X;
            BlockY = (int)worldLocation.Y % ImGlobal.SegmentSize.Y;
            BlockZ = (int)worldLocation.Z % ImGlobal.SegmentSize.Z;
            RenderSegmentIndex = ImGlobal.RenderSegmentIndices[(int) worldLocation.Y];
            RenderSegmentBlockMaskIndex = ImGlobal.RenderBlockMaskIndices[(int)worldLocation.Y];
        }

        public ImSegmentLocation TranslateAndClone(Vector3 distance)
        {
            return new ImSegmentLocation(WorldLocation + distance);
        }
    }
}
