// -----------------------------------------------------------------------
// <copyright file="ImVertexHelpers.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsomiteEngine.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImVertexHelper
    {
        public static VertexPositionNormalTexture[] TranslateVerticesToWorldLocation(
            VertexPositionNormalTexture[] verticesIn, Vector3 worldGridLocation)
        {
            VertexPositionNormalTexture[] verticesOut = new VertexPositionNormalTexture[verticesIn.Length];
            for (int currentVertex = 0; currentVertex < verticesIn.Length; currentVertex++)
            {
                verticesOut[currentVertex] = new VertexPositionNormalTexture(new Vector3(verticesIn[currentVertex].Position.X + ((ImBlockVertices.CubeSize.X) * worldGridLocation.X), verticesIn[currentVertex].Position.Y + ((ImBlockVertices.CubeSize.Y) * worldGridLocation.Y), verticesIn[currentVertex].Position.Z + ((ImBlockVertices.CubeSize.Z) * worldGridLocation.Z)),
                         verticesIn[currentVertex].Normal, verticesIn[currentVertex].TextureCoordinate);
            }

            return verticesOut;
        }
    }
}
