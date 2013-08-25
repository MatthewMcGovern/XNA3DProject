// -----------------------------------------------------------------------
// <copyright file="ImGlobal.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Isomites.GameWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ImGlobal
    {
        public static class BlockMasks
        {
            public static ImBlockMask Empty = ImBlockMask.Empty;
            public static ImBlockMask Null = ImBlockMask.IsObstacle;
            public static ImBlockMask Air =  ImBlockMask.Type1;
            public static ImBlockMask AirBlocked = ImBlockMask.Type1 | ImBlockMask.IsObstacle;
            public static ImBlockMask Soil = ImBlockMask.IsObstacle | ImBlockMask.Type1 | ImBlockMask.Data1;
            public static ImBlockMask Stone = ImBlockMask.IsObstacle | ImBlockMask.Type1 | ImBlockMask.Data2;
        }

        public static class ItemIDs
        {
            public static int Tree = 0;
        }

        public static class WorldSegmentsSize
        {
            public static int X = 1;
            public static int Z = 1;
        }

        public static class SegmentSize
        {
            public static int X = 32;
            public static int Y = 64;
            public static int Z = 32;
        }

        public static class RenderSegmentSize
        {
            public static int X = SegmentSize.X;
            public static int Y = 4;
            public static int Z = SegmentSize.Z;
        }

        public static int[] RenderSegmentIndices;
        public static int[] RenderBlockMaskIndices;

        public static void Init()
        {
            RenderSegmentIndices = new int[SegmentSize.Y];
            RenderBlockMaskIndices = new int[SegmentSize.Y];

            for (int i = 0; i < SegmentSize.Y; i++)
            {
                RenderSegmentIndices[i] = (int)Math.Floor((double)(i/RenderSegmentSize.Y));
            }

            for (int y = 0; y < SegmentSize.Y; y++)
            {
                RenderBlockMaskIndices[y] = y%RenderSegmentSize.Y;
            }
        }

        public static int NoOfRenderSegments = (int)Math.Ceiling((double)SegmentSize.Y/RenderSegmentSize.Y);
    }
}
