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
    public class NewCube
    {
        public ushort Type;
        public Connections Neighbours;

        public NewCube(ushort type)
        {
            Type = type;
            Neighbours = Connections.None;
        }
    }
}
