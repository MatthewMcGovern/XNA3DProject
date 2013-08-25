// -----------------------------------------------------------------------
// <copyright file="BlockHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsomiteEngine.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.GameWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ImBlockHelper
    {
        public static bool IsBlock(ImBlockMask blockMask)
        {
            return blockMask.HasFlag(ImBlockMask.Type1 & ~ImBlockMask.Type2);
        }

        public static int GetBlockID(ImBlockMask blockMask)
        {
            int id = (int)(blockMask & ~ImBlockMask.IsObstacle & ~ImBlockMask.Type1 & ~ImBlockMask.Type2);
            if (id > 0)
                id -= 7;

            return id;
        }

        public static bool DoesBlockMaskObscureFromDirection(ImBlockMask blockMask, Vector3 fromDirection)
        {
            if (blockMask == ImGlobal.BlockMasks.Air || blockMask == ImGlobal.BlockMasks.AirBlocked)
                return false;
            if (IsBlock(blockMask))
                return true;

            return false;
        }

        public static bool IsBlockAnObstacle(ImBlockMask blockMask)
        {
            return blockMask.HasFlag(ImBlockMask.IsObstacle);
        }
    }
}
