// -----------------------------------------------------------------------
// <copyright file="TreeHolder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites3D.AI;
using Isomites3D.CubeWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeHolder
    {
        private List<Tree> _activeTrees;
        public ChunkManager Chunks;

        public TreeHolder()
        {
            _activeTrees = new List<Tree>();
        }

        public void AddTreeAt(Vector3 position)
        {
            _activeTrees.Add(new Tree(position));
            Chunks.AddCubeAt((int)position.X, (int)position.Y, (int)position.Z, 3);
        }

        public void Draw(GraphicsDevice device, Matrix viewMatrix, Matrix projectionMatrix)
        {
            foreach (Tree tree in _activeTrees)
            {
                tree.Draw(device, viewMatrix, projectionMatrix);
            }
        }
    }
}
