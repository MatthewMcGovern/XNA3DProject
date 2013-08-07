// -----------------------------------------------------------------------
// <copyright file="Connections.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Policy;

namespace Isomites3D.CubeWorld
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    
    // Bit mask for directions a cube can touch another cube.
    [Flags]
    public enum Connections
    {
        None = 0,
        Up = 1,
        Down = 2,
        North = 4,
        East = 8,
        South = 16,
        West = 32
    }

    public static class ConnectionUtils
    {
        // Lazy function to get the opposite side, ie opposite of north is south.
        public static Connections GetOppositeBlockMask(Connections direction)
        {
            switch (direction)
            {
                case Connections.Up:
                    return Connections.Down;
                case Connections.Down:
                    return Connections.Up;
                case Connections.North:
                    return Connections.South;
                case Connections.East:
                    return Connections.West;
                case Connections.South:
                    return Connections.North;
                case Connections.West:
                    return Connections.East;
                default:
                    return Connections.North;
            }
        }

        // Lazy static that defines all of the bitmasks at once.
        public static Connections All = Connections.Down | Connections.East | Connections.North | Connections.South |
                                        Connections.Up | Connections.West;
    }
}
