// -----------------------------------------------------------------------
// <copyright file="ImWorldHelpers.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

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
    public static class ImWorldHelpers
    {
        public static ImSegmentLocation GetImSegmentLocation(Vector3 worldPosition)
        {
            return new ImSegmentLocation(worldPosition);
        }

        public static ImSegmentLocation GetImSegmentLocation(int x, int y, int z)
        {
            Vector3 worldPosition = new Vector3(x, y, z);
            return new ImSegmentLocation(worldPosition);
        }
    }
}
