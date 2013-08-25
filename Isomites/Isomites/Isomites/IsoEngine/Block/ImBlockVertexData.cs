// -----------------------------------------------------------------------
// <copyright file="ImBlockVertexData.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsomiteEngine.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImBlockVertexData
    {
        private static List<ImBlockVertexData> _blockData;
        private static float textureOffset = 0;
        private static float bleedingFix = 0;

        public VertexPositionNormalTexture[] UpVertices;
        public ushort[] UpIndices;
        public VertexPositionNormalTexture[] DownVertices;
        public ushort[] DownIndices;
        public VertexPositionNormalTexture[] NorthVertices;
        public ushort[] NorthIndices;
        public VertexPositionNormalTexture[] EastVertices;
        public ushort[] EastIndices;
        public VertexPositionNormalTexture[] SouthVertices;
        public ushort[] SouthIndices;
        public VertexPositionNormalTexture[] WestVertices;
        public ushort[] WestIndices;

        static ImBlockVertexData()
        {
            textureOffset = 32f/2048f;
            bleedingFix = 1f/2048f;
            _blockData = new List<ImBlockVertexData>();

            ImBlockVertexData soil = new ImBlockVertexData();
            soil.UpVertices = CalculateUpVertices(0);
            soil.UpIndices = ImBlockIndices.UpIndices;
            soil.DownVertices = CalculateDownVertices(2);
            soil.DownIndices = ImBlockIndices.DownIndices;
            soil.NorthVertices = CalculateNorthVertices(1);
            soil.NorthIndices = ImBlockIndices.NorthIndices;
            soil.EastVertices = CalculateEastVertices(1);
            soil.EastIndices = ImBlockIndices.EastIndices;
            soil.SouthVertices = CalculateSouthVertices(1);
            soil.SouthIndices = ImBlockIndices.SouthIndices;
            soil.WestVertices = CalculateWestVertices(1);
            soil.WestIndices = ImBlockIndices.WestIndices;

            _blockData.Add(new ImBlockVertexData());
            _blockData.Add(soil);
        }

        public static ImBlockVertexData GetBlockMaskVertexData(ImBlockMask blockMask)
        {
            if (ImBlockHelper.IsBlock(blockMask))
            {
                return _blockData[ImBlockHelper.GetBlockID(blockMask)];
            }

            return new ImBlockVertexData();
        }

        static VertexPositionNormalTexture[] CalculateUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int) Math.Floor((double) topFaceIndex/64);
            int topColumn = topFaceIndex%64;

            float topRowStartX =(topColumn*textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1)*textureOffset) - bleedingFix;
            float topRowStartY = (topRow*textureOffset) + bleedingFix;
            float topRowEndY = ((topRow+1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));
            eastVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));
            southVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));
            westVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));

            return westVertices;
        }

        public static VertexPositionNormalTexture[] TranslateVerticesToWorldLocation(
            VertexPositionNormalTexture[] verticesIn, Vector3 worldGridLocation)
        {
            VertexPositionNormalTexture[] verticesOut = new VertexPositionNormalTexture[verticesIn.Count()];
            for (int currentVertex = 0; currentVertex < verticesIn.Count(); currentVertex++)
            {
                verticesOut[currentVertex] = new VertexPositionNormalTexture(new Vector3(verticesIn[currentVertex].Position.X + ((ImBlockVertices.CubeSize.X) * worldGridLocation.X), verticesIn[currentVertex].Position.Y + ((ImBlockVertices.CubeSize.Y) * worldGridLocation.Y), verticesIn[currentVertex].Position.Z + ((ImBlockVertices.CubeSize.Z) * worldGridLocation.Z)),
                         verticesIn[currentVertex].Normal, verticesIn[currentVertex].TextureCoordinate);
            }

            return verticesOut;
        }

    }
}
