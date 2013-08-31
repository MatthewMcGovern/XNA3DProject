// -----------------------------------------------------------------------
// <copyright file="BlockHelper.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using Isomites.IsoEngine.Block;
using Isomites.IsoEngine.World;
using Isomites.IsomiteEngine.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine
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
        public static class BlockMasks
        {
            public static ImBlockMask Empty = ImBlockMask.Empty;
            public static ImBlockMask Null = ImBlockMask.IsObstacle;
            public static ImBlockMask Air = ImBlockMask.Type1;
            public static ImBlockMask AirBlocked = ImBlockMask.Type1 | ImBlockMask.IsObstacle;
            public static ImBlockMask Debug = ImBlockMask.IsObstacle | ImBlockMask.Type1 | ImBlockMask.Data1;
            public static ImBlockMask Soil = ImBlockMask.IsObstacle | ImBlockMask.Type1 | ImBlockMask.Data2;
            public static ImBlockMask Stone = ImBlockMask.IsObstacle | ImBlockMask.Type1 | ImBlockMask.Data1 | ImBlockMask.Data2;
        }

        public static class RampBlockMasks
        {
            public static class Top
            {
                public static ImBlockMask Air = ImBlockMask.Type2;
                public static ImBlockMask Debug = ImBlockMask.IsObstacle | ImBlockMask.Type2 | ImBlockMask.Data7;
                public static ImBlockMask Soil = ImBlockMask.IsObstacle | ImBlockMask.Type2 | ImBlockMask.Data8;
            }

            public static class Bottom
            {
                public static ImBlockMask Air = ImBlockMask.Type2;
                public static ImBlockMask Debug = ImBlockMask.IsObstacle | ImBlockMask.Type2 | ImBlockMask.Data3;
                public static ImBlockMask Soil = ImBlockMask.IsObstacle | ImBlockMask.Type2 | ImBlockMask.Data4;
            }
        }

        public static bool IsBlock(ImBlockMask blockMask)
        {
            return blockMask.HasFlag(ImBlockMask.Type1 & ~ImBlockMask.Type2);
        }

        public static bool IsRampBlock(ImBlockMask blockMask)
        {
            return blockMask.HasFlag(ImBlockMask.Type2 & ~ImBlockMask.Type1);
        }

        public static bool HasBottomRamp(ImBlockMask blockMask)
        {
            return (blockMask.HasFlag(ImBlockMask.Data3) || blockMask.HasFlag(ImBlockMask.Data4) ||
                    blockMask.HasFlag(ImBlockMask.Data5) || blockMask.HasFlag(ImBlockMask.Data6));
        }

        public static bool HasTopRamp(ImBlockMask blockMask)
        {
            return (blockMask.HasFlag(ImBlockMask.Data7) || blockMask.HasFlag(ImBlockMask.Data8) || blockMask.HasFlag(ImBlockMask.Data9) ||
                     blockMask.HasFlag(ImBlockMask.Data10)); 
        }

        public static int GetBlockID(ImBlockMask blockMask)
        {
            int id = (int)(blockMask & ~ImBlockMask.IsObstacle & ~ImBlockMask.Type1 & ~ImBlockMask.Type2) >> 3;

            return id;
        }

        public static int GetBottomRampID(ImBlockMask blockMask)
        {
            int id = (int)(blockMask & ~ImBlockMask.IsObstacle & ~ImBlockMask.Type1 & ~ImBlockMask.Type2 & ~ImBlockMask.Data1 & ~ImBlockMask.Data2 & ~ImBlockMask.Data7 & ~ImBlockMask.Data8 & ~ImBlockMask.Data9 & ~ImBlockMask.Data10) >> 5;

            return id; 
        }

        public static int GetTopRampID(ImBlockMask blockMask)
        {
            int id = (int)(blockMask & ~ImBlockMask.IsObstacle & ~ImBlockMask.Type1 & ~ImBlockMask.Type2 & ~ImBlockMask.Data1 & ~ImBlockMask.Data2 & ~ImBlockMask.Data3 & ~ImBlockMask.Data4 & ~ImBlockMask.Data5 & ~ImBlockMask.Data6) >> 9;

            return id;
        }

        public static ImBlockMask GetRampDirection(ImBlockMask blockMask)
        {
            if (blockMask.HasFlag(ImRampDirection.West))
                return ImRampDirection.West;
            if (blockMask.HasFlag(ImRampDirection.East))
                return ImRampDirection.East;
            if (blockMask.HasFlag(ImRampDirection.South))
                return ImRampDirection.South;

            return ImRampDirection.North;
        }

        public static ImBlockMask RotateRampTo(ImBlockMask blockMask , ImBlockMask direction)
        {
            // unset direction
            blockMask = blockMask | ~ImBlockMask.Data1 | ~ImBlockMask.Data2;
            // set new direciton
            blockMask = blockMask | direction;

            return blockMask;
        }

        public static bool DoesBlockMaskAObscureMaskBFace(ImBlockMask blockMaskA, ImBlockMask blockMaskB,
            Vector3 fromDirection)
        {

            // Air and null blocks are see through
            if (blockMaskA == BlockMasks.AirBlocked || blockMaskA == BlockMasks.Air || blockMaskA == BlockMasks.Null)
                return false;

            // Blocks always obscure.
            if (IsBlock(blockMaskA))
                return true;

            // A top+bottom ramp is the same as a block
            if (HasTopRamp(blockMaskA) && HasBottomRamp(blockMaskA))
                return true;

            // bottom ramps block the opposite direction
            if (HasBottomRamp(blockMaskA))
            {
                if (GetRampDirection(blockMaskA) == ImRampDirection.North && fromDirection == ImDirection.South)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.East && fromDirection == ImDirection.West)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.South && fromDirection == ImDirection.North)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.West && fromDirection == ImDirection.East)
                    return true;
            }

            if (HasTopRamp(blockMaskA))
            {

                // top ramps block the same direction
                if (GetRampDirection(blockMaskA) == ImRampDirection.North && fromDirection == ImDirection.North)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.East && fromDirection == ImDirection.East)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.South && fromDirection == ImDirection.South)
                    return true;
                if (GetRampDirection(blockMaskA) == ImRampDirection.West && fromDirection == ImDirection.West)
                    return true;
            }

            
            // top ramps block matching bottom ramps from Sides
            if (HasTopRamp(blockMaskA) && HasTopRamp(blockMaskB))
            {
                if (GetRampDirection(blockMaskA) == GetRampDirection(blockMaskB))
                {
                    if (GetRampDirection(blockMaskA) == ImRampDirection.North || GetRampDirection(blockMaskA) == ImRampDirection.South)
                    {
                        if (fromDirection == ImDirection.East || fromDirection == ImDirection.West)
                        {
                            return true;
                        }
                        return false;
                    }
                    if (GetRampDirection(blockMaskA) == ImRampDirection.East || GetRampDirection(blockMaskA) == ImRampDirection.West)
                    {
                        if (fromDirection == ImDirection.North || fromDirection == ImDirection.South)
                        {
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }

            // bottom ramps block matching bottom ramps from Sides
            if (HasBottomRamp(blockMaskA) && HasBottomRamp(blockMaskB))
            {
                if (GetRampDirection(blockMaskA) == GetRampDirection(blockMaskB))
                {
                    if (GetRampDirection(blockMaskA) == ImRampDirection.North || GetRampDirection(blockMaskA) == ImRampDirection.South)
                    {
                        if (fromDirection == ImDirection.East || fromDirection == ImDirection.West)
                        {
                            return true;
                        }
                        return false;
                    }
                    if (GetRampDirection(blockMaskA) == ImRampDirection.East || GetRampDirection(blockMaskA) == ImRampDirection.West)
                    {
                        if (fromDirection == ImDirection.North || fromDirection == ImDirection.South)
                        {
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }

            // top ramps block up
            if (HasBottomRamp(blockMaskA))
            {
                return (fromDirection == ImDirection.Up);
            }

            // bottom ramps block down
            if (HasTopRamp(blockMaskA))
            {
                return (fromDirection == ImDirection.Down);
            }

            return false;
        }
        public static bool DoesBlockMaskObscureFromDirection(ImBlockMask blockMask, Vector3 fromDirection)
        {
            if (blockMask == BlockMasks.Air || blockMask == BlockMasks.AirBlocked)
            {
                return false;
            }
            if (IsBlock(blockMask))
            {
                return true;
            }
            if (IsRampBlock(blockMask))
            {
                if (HasBottomRamp(blockMask) && HasTopRamp(blockMask))
                {
                    return true;
                }
                else if (HasBottomRamp(blockMask))
                {
                    if (fromDirection == ImDirection.Down)
                    {
                        return true;
                    }
                    switch (GetRampDirection(blockMask))
                    {
                        case ImRampDirection.North:
                            if (fromDirection == ImDirection.North)
                                return true;
                            return false;
                        case ImRampDirection.East:
                            if (fromDirection == ImDirection.East)
                                return true;
                            return false;
                        case ImRampDirection.South:
                            if (fromDirection == ImDirection.South)
                                return true;
                            return false;
                        case ImRampDirection.West:
                            if (fromDirection == ImDirection.West)
                                return true;
                            return false;
                    }
                }
                else if (HasTopRamp(blockMask))
                {
                    if (fromDirection == ImDirection.Up)
                    {
                        return true;
                    }
                    switch (GetRampDirection(blockMask))
                    {
                        case ImRampDirection.North:
                            if (fromDirection == ImDirection.South)
                                return true;
                            return false;
                        case ImRampDirection.East:
                            if (fromDirection == ImDirection.West)
                                return true;
                            return false;
                        case ImRampDirection.South:
                            if (fromDirection == ImDirection.North)
                                return true;
                            return false;
                        case ImRampDirection.West:
                            if (fromDirection == ImDirection.East)
                                return true;
                            return false;
                    }
                }
            }
            return false;
        }

        public static bool IsBlockAnObstacle(ImBlockMask blockMask)
        {
            return blockMask.HasFlag(ImBlockMask.IsObstacle);
        }
    }
}
