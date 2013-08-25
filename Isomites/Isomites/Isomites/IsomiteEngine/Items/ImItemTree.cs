// -----------------------------------------------------------------------
// <copyright file="ImItemTree.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.GameWorld;
using Microsoft.Xna.Framework;

namespace Isomites.IsomiteEngine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImItemTree : ImWorldItem
    {
        public ImItemTree(Vector3 position, ImSegment segment) : base(position, segment)
        {
            ItemType = ImItemTypes.Tree;
            Scale = new Vector3(0.8f, 0.8f, 0.75f);
            _renderOffset = new Vector3(0, 0.85f, 0);
        }
    }
}
