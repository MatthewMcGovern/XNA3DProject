// -----------------------------------------------------------------------
// <copyright file="Node.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace Isomites3D.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Node
    {
        public Vector3 Position;
        public Node PreviousNode;

        public Node(Vector3 position, Node previousNode)
        {
            Position = position;
            PreviousNode = previousNode;
        }
    }
}
