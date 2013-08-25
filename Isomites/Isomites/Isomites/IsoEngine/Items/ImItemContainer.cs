// -----------------------------------------------------------------------
// <copyright file="ItemContainer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImItemContainer
    {
        public List<List<ImWorldItem>> SubItems;

        public ImItemContainer()
        {
            SubItems = new List<List<ImWorldItem>>();
            // Index 0 = trees
            SubItems.Add(new List<ImWorldItem>());
        }

        public List<ImWorldItem> FindItemsAt(Vector3 position)
        {
            return FindItemsAt((int) position.X, (int) position.Y, (int) position.Z);
        }

        public List<ImWorldItem> FindItemsAt(int x, int y, int z)
        {
            List<ImWorldItem> foundItems = new List<ImWorldItem>();
            foreach (var subItems in SubItems)
            {
                foreach (var imWorldItem in subItems)
                {
                    if (imWorldItem.DoesObjectLieWithinPosition(x, y, z))
                    {
                        foundItems.Add(imWorldItem);
                    }
                }
            }

            return foundItems;
        }

        public void DrawAll(GraphicsDevice device, Camera3D camera)
        {
            foreach (List<ImWorldItem> subItems in SubItems)
            {
                foreach (ImWorldItem item in subItems)
                {
                    item.Draw(device, camera);
                }
            }
        }
    }
}
