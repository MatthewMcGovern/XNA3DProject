// -----------------------------------------------------------------------
// <copyright file="ImItemType.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsomiteEngine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImItemType
    {
        public ImRenderBasic RenderBasic;
        public int SubIndex;
        public List<Vector3> OccupiedSpace;
        public bool MustBePlacedOnFloor;

    }

    public static class ImItemTypes
    {
        public static ImItemType Tree;

        public static void Init(ContentManager content, Effect modelEffect)
        {
            Tree = new ImItemType();
            Tree.RenderBasic = new ImRenderBasic(content.Load<Model>("Models/Item/Tree"), modelEffect);
            Tree.SubIndex = 0;
            Tree.OccupiedSpace = new List<Vector3>();
            Tree.OccupiedSpace.Add(Vector3.Zero);
            Tree.OccupiedSpace.Add(new Vector3(0, 1, 0));
            Tree.OccupiedSpace.Add(new Vector3(0, 2, 0));
            Tree.OccupiedSpace.Add(new Vector3(0, 3, 0));
            Tree.MustBePlacedOnFloor = true;
        }

    }
}
