using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Isomites3D.CubeWorld
{
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
