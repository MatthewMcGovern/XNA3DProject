// -----------------------------------------------------------------------
// <copyright file="Connections.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Policy;

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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

        public static Connections All = Connections.Down | Connections.East | Connections.North | Connections.South |
                                        Connections.Up | Connections.West;
    }
}
