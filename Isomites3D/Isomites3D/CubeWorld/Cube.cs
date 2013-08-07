// -----------------------------------------------------------------------
// <copyright file="NewCube.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    
    // lightweight cube class as this is what gets stored in the giant world array.
    // all it needs is ID (to look up texture and vertices/idncies) and the bitmask.
    // Technically bitmask isn't required but it saves computational cost at the expense of like 6 bits ber cube?
    public class Cube
    {
        public ushort Type;
        public Connections Neighbours;

        public Cube(ushort type)
        {
            Type = type;
            Neighbours = Connections.None;
        }
    }
}
