using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Isomites3D.CubeWorld
{
    /// <summary>
    /// TODO: summary
    /// </summary>
    
    // Very simple class just to hold the start and end point of various lines
    // this is so I can be lazy and static define a bunch of Vector3 pairs.
    public class CubeOutline
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;

        public CubeOutline(Vector3 start, Vector3 end)
        {
            this.StartPosition = start;
            this.EndPosition = end;
        }
    }
}
