// -----------------------------------------------------------------------
// <copyright file="ImRampDirection.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Isomites.IsoEngine.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ImRampDirection
    {
        public const ImBlockMask North = ImBlockMask.Empty;
        public const ImBlockMask East = ImBlockMask.Data1;
        public const ImBlockMask South = ImBlockMask.Data2;
        public const ImBlockMask West = ImBlockMask.Data2 | ImBlockMask.Data1;
    }
}
