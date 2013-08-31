// -----------------------------------------------------------------------
// <copyright file="ImRampBlockVertexData.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
using Isomites.IsoEngine.World;
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
    public class ImBlockRampVertexData
    {
        public ImBlockVertexData TopNorthRampVertexData;
        public ImBlockVertexData TopEastRampVertexData;
        public ImBlockVertexData TopSouthRampVertexData;
        public ImBlockVertexData TopWestRampVertexData;
        public ImBlockVertexData BottomNorthRampVertexData;
        public ImBlockVertexData BottomEastRampVertexData;
        public ImBlockVertexData BottomSouthRampVertexData;
        public ImBlockVertexData BottomWestRampVertexData;

        private static float textureOffset = 0;
        private static float bleedingFix = 0;

        private static List<ImBlockRampVertexData> _blockRampData;


        public ImBlockRampVertexData()
        {
            TopNorthRampVertexData = new ImBlockVertexData();
            TopNorthRampVertexData = new ImBlockVertexData();
            TopEastRampVertexData = new ImBlockVertexData();
            TopSouthRampVertexData = new ImBlockVertexData();
            TopWestRampVertexData = new ImBlockVertexData();
            BottomNorthRampVertexData = new ImBlockVertexData();
            BottomEastRampVertexData = new ImBlockVertexData();
            BottomSouthRampVertexData = new ImBlockVertexData();
            BottomWestRampVertexData = new ImBlockVertexData();
        }

        static ImBlockRampVertexData()
        {
            textureOffset = 32f/2048f;
            bleedingFix = 1f/2048f;

            _blockRampData = new List<ImBlockRampVertexData>();
            _blockRampData.Add(new ImBlockRampVertexData());

            ImBlockRampVertexData debugRamp = new ImBlockRampVertexData();
            debugRamp.BottomNorthRampVertexData = GetBottomNorthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomEastRampVertexData = GetBottomEastRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomSouthRampVertexData = GetBottomSouthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.BottomWestRampVertexData = GetBottomWestRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopNorthRampVertexData = GetTopNorthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopSouthRampVertexData = GetTopSouthRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopEastRampVertexData = GetTopEastRampBlockVertexData(0, 4, 5, 1, 2, 3);
            debugRamp.TopWestRampVertexData = GetTopWestRampBlockVertexData(0, 4, 5, 1, 2, 3);
            //debugRamp.down
            _blockRampData.Add(debugRamp);

            ImBlockRampVertexData soilRamp = new ImBlockRampVertexData();
            soilRamp.BottomNorthRampVertexData = GetBottomNorthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomEastRampVertexData = GetBottomEastRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomSouthRampVertexData = GetBottomSouthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.BottomWestRampVertexData = GetBottomWestRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopNorthRampVertexData = GetTopNorthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopSouthRampVertexData = GetTopSouthRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopEastRampVertexData = GetTopEastRampBlockVertexData(6, 8, 7, 7, 7, 7);
            soilRamp.TopWestRampVertexData = GetTopWestRampBlockVertexData(6, 8, 7, 7, 7, 7);
            _blockRampData.Add(soilRamp);
        }

        static public ImBlockVertexData GetBlockMaskVertexData(ImBlockMask blockMask)
        {
            ImBlockMask direction = ImBlockHelper.GetRampDirection(blockMask);
            if (ImBlockHelper.IsRampBlock(blockMask))
            {
                if (ImBlockHelper.HasBottomRamp(blockMask) && ImBlockHelper.HasTopRamp(blockMask))
                {
                    if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.North)
                    {
                        ImBlockVertexData toReturnDown = _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomNorthRampVertexData.Copy();
                        ImBlockVertexData toReturnTop = _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopNorthRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.East)
                    {
                        ImBlockVertexData toReturnDown = _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomEastRampVertexData.Copy();
                        ImBlockVertexData toReturnTop = _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopEastRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.South)
                    {
                        ImBlockVertexData toReturnDown = _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomSouthRampVertexData.Copy();
                        ImBlockVertexData toReturnTop = _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopSouthRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                    if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.West)
                    {
                        ImBlockVertexData toReturnDown = _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomWestRampVertexData.Copy();
                        ImBlockVertexData toReturnTop = _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopWestRampVertexData.Copy();
                        toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.UpIndices = new ushort[0];
                        toReturnTop.DownIndices = new ushort[0];
                        toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                        toReturnDown.Merge(toReturnTop.Copy());
                        return toReturnDown;
                    }
                }
                if (ImBlockHelper.HasTopRamp(blockMask))
                {
                    if (direction == ImRampDirection.North)
                    {
                        return _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopNorthRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.East)
                    {
                        return _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopEastRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.South)
                    {
                        return _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopSouthRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.West)
                    {
                        return _blockRampData[ImBlockHelper.GetTopRampID(blockMask)].TopWestRampVertexData.Copy();
                    }
                }
                if(ImBlockHelper.HasBottomRamp(blockMask))
                {
                    if (direction == ImRampDirection.North)
                    {
                        return _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomNorthRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.East)
                    {
                        return _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomEastRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.South)
                    {
                        return _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomSouthRampVertexData.Copy();
                    }
                    if (direction == ImRampDirection.West)
                    {
                        return _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)].BottomWestRampVertexData.Copy();
                    }
                }
            }

            return new ImBlockVertexData();
        }

        static public ImBlockRampVertexData GetBlockMaskRampVertexData(ImBlockMask blockMask)
        {
            if (ImBlockHelper.IsRampBlock(blockMask))
            {
                // top + bottom needs doing
                if(ImBlockHelper.HasTopRamp(blockMask))
                    return _blockRampData[ImBlockHelper.GetTopRampID(blockMask)];
                if (ImBlockHelper.HasBottomRamp(blockMask))
                    return _blockRampData[ImBlockHelper.GetBottomRampID(blockMask)];
            }

            return new ImBlockRampVertexData();
        }

        public static ImBlockVertexData GetCopyOfBlockMasBlockVertexData(ImBlockMask blockMask)
        {
            ImBlockRampVertexData toCopy = GetBlockMaskRampVertexData(blockMask);

            if (ImBlockHelper.HasTopRamp(blockMask) && ImBlockHelper.HasBottomRamp(blockMask))
            {
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.North)
                {
                    ImBlockVertexData toReturnDown = toCopy.BottomNorthRampVertexData.Copy();
                    toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.UpIndices = new ushort[0];
                    ImBlockVertexData toReturnTop = toCopy.TopNorthRampVertexData.Copy();
                    toReturnTop.DownIndices = new ushort[0];
                    toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.Merge(toReturnTop.Copy());
                    return toReturnDown;
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.East)
                {
                    ImBlockVertexData toReturnDown = toCopy.BottomNorthRampVertexData.Copy();
                    toReturnDown.UpVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.UpIndices = new ushort[0];
                    ImBlockVertexData toReturnTop = toCopy.TopNorthRampVertexData.Copy();
                    toReturnTop.DownIndices = new ushort[0];
                    toReturnTop.DownVertices = new VertexPositionNormalTexture[0];
                    toReturnDown.Merge(toReturnTop.Copy());
                    return toReturnDown;
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.South)
                {
                    ImBlockVertexData toReturn = toCopy.BottomSouthRampVertexData.Copy();
                    toReturn.Merge(toCopy.TopSouthRampVertexData.Copy());
                    return toReturn;
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.West)
                {
                    ImBlockVertexData toReturn = toCopy.BottomWestRampVertexData.Copy();
                    toReturn.Merge(toCopy.TopWestRampVertexData.Copy());
                    return toReturn;
                }
            }

            if (ImBlockHelper.HasBottomRamp(blockMask))
            {
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.North)
                {
                    return toCopy.BottomNorthRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.East)
                {
                    return toCopy.BottomEastRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.South)
                {
                    return toCopy.BottomSouthRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.West)
                {
                    return toCopy.BottomWestRampVertexData.Copy();
                }
            }
            if (ImBlockHelper.HasTopRamp(blockMask))
            {
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.North)
                {
                    return toCopy.TopNorthRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.East)
                {
                    return toCopy.TopEastRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.South)
                {
                    return toCopy.TopSouthRampVertexData.Copy();
                }
                if (ImBlockHelper.GetRampDirection(blockMask) == ImRampDirection.West)
                {
                    return toCopy.TopWestRampVertexData.Copy();
                }
            }


            return new ImBlockVertexData();
        }

        static ImBlockVertexData GetBottomNorthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateBottomNorthUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.DownNorth.UpIndices;
            toReturn.DownVertices = CalculateBottomNorthDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.DownNorth.DownIndices;
            toReturn.NorthVertices = CalculateBottomNorthNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.DownNorth.NorthIndices;
            toReturn.EastVertices = CalculateBottomNorthEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.DownNorth.EastIndices;
            toReturn.SouthVertices = CalculateBottomNorthSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.DownNorth.SouthIndices;
            toReturn.WestVertices = CalculateBottomNorthWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.DownNorth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomNorthUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomNorthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[0];
            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomNorthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static ImBlockVertexData GetBottomEastRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateBottomEastUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.DownEast.UpIndices;
            toReturn.DownVertices = CalculateBottomEastDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.DownEast.DownIndices;
            toReturn.NorthVertices = CalculateBottomEastNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.DownEast.NorthIndices;
            toReturn.EastVertices = CalculateBottomEastEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.DownEast.EastIndices;
            toReturn.SouthVertices = CalculateBottomEastSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.DownEast.SouthIndices;
            toReturn.WestVertices = CalculateBottomEastWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.DownEast.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomEastUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomEastNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomEastWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[0];
            return westVertices;
        }

        static ImBlockVertexData GetBottomSouthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateBottomSouthUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.DownSouth.UpIndices;
            toReturn.DownVertices = CalculateBottomSouthDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.DownSouth.DownIndices;
            toReturn.NorthVertices = CalculateBottomSouthNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.DownSouth.NorthIndices;
            toReturn.EastVertices = CalculateBottomSouthEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.DownSouth.EastIndices;
            toReturn.SouthVertices = CalculateBottomSouthSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.DownSouth.SouthIndices;
            toReturn.WestVertices = CalculateBottomSouthWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.DownSouth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateBottomSouthUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomSouthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[0];
           

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1)*textureOffset) - bleedingFix;
            
            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomSouthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            
            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static VertexPositionNormalTexture[] CalculateBottomWestUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestDownVertices(int bottomFaceIndex)
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

        private static VertexPositionNormalTexture[] CalculateBottomWestNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[0];

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));     
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateBottomWestWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static ImBlockVertexData GetBottomWestRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateBottomWestUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.DownWest.UpIndices;
            toReturn.DownVertices = CalculateBottomWestDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.DownWest.DownIndices;
            toReturn.NorthVertices = CalculateBottomWestNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.DownWest.NorthIndices;
            toReturn.EastVertices = CalculateBottomWestEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.DownWest.EastIndices;
            toReturn.SouthVertices = CalculateBottomWestSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.DownWest.SouthIndices;
            toReturn.WestVertices = CalculateBottomWestWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.DownWest.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }
        static ImBlockVertexData GetTopNorthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateTopNorthUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.TopNorth.UpIndices;
            toReturn.DownVertices = CalculateTopNorthDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.TopNorth.DownIndices;
            toReturn.NorthVertices = CalculateTopNorthNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.TopNorth.SouthIndices;
            toReturn.EastVertices = CalculateTopNorthEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.TopNorth.EastIndices;
            toReturn.SouthVertices = CalculateTopNorthSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.TopNorth.NorthIndices;
            toReturn.WestVertices = CalculateTopNorthWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.TopNorth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopNorthUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[0];
            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));
            southVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopNorthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));

            return westVertices;
        }

        static ImBlockVertexData GetTopEastRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateTopEastUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.TopEast.UpIndices;
            toReturn.DownVertices = CalculateTopEastDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.TopEast.DownIndices;
            toReturn.NorthVertices = CalculateTopEastNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.TopEast.NorthIndices;
            toReturn.EastVertices = CalculateTopEastEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.TopEast.EastIndices;
            toReturn.SouthVertices = CalculateTopEastSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.TopEast.SouthIndices;
            toReturn.WestVertices = CalculateTopEastWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.TopEast.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopEastUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastDownVertices(int bottomFaceIndex)
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
            bottomVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[0];
            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopEastWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowEndY));
            westVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            return westVertices;
        }

        static ImBlockVertexData GetTopSouthRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateTopSouthUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.TopSouth.UpIndices;
            toReturn.DownVertices = CalculateTopSouthDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.TopSouth.DownIndices;
            toReturn.NorthVertices = CalculateTopSouthNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.TopSouth.NorthIndices;
            toReturn.EastVertices = CalculateTopSouthEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.TopSouth.EastIndices;
            toReturn.SouthVertices = CalculateTopSouthSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.TopSouth.SouthIndices;
            toReturn.WestVertices = CalculateTopSouthWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.TopSouth.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

        static VertexPositionNormalTexture[] CalculateTopSouthUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthDownVertices(int bottomFaceIndex)
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
            bottomVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowEndY));
            northVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[0];
            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopSouthWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int westRow = (int)Math.Floor((double)westFaceIndex / 64);
            int westColumn = westFaceIndex % 64;

            float westRowStartX = (westColumn * textureOffset) + bleedingFix;
            float westRowEndX = ((westColumn + 1) * textureOffset) - bleedingFix;
            float westRowStartY = (westRow * textureOffset) + bleedingFix;
            float westRowEndY = ((westRow + 1) * textureOffset) - bleedingFix;

            westVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowStartY));
            westVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(westRowStartX, westRowStartY));
            
            westVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopLeft, Vector3.Zero, new Vector2(westRowEndX, westRowEndY));

            

            return westVertices;
        }

        static VertexPositionNormalTexture[] CalculateTopWestUpVertices(int topFaceIndex)
        {
            VertexPositionNormalTexture[] topVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int topRow = (int)Math.Floor((double)topFaceIndex / 64);
            int topColumn = topFaceIndex % 64;

            float topRowStartX = (topColumn * textureOffset) + bleedingFix;
            float topRowEndX = ((topColumn + 1) * textureOffset) - bleedingFix;
            float topRowStartY = (topRow * textureOffset) + bleedingFix;
            float topRowEndY = ((topRow + 1) * textureOffset) - bleedingFix;

            topVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(topRowEndX, topRowStartY));
            topVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(topRowStartX, topRowStartY));
            topVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(topRowEndX, topRowEndY));
            topVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(topRowStartX, topRowEndY));

            return topVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestDownVertices(int bottomFaceIndex)
        {
            VertexPositionNormalTexture[] bottomVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int bottomRow = (int)Math.Floor((double)bottomFaceIndex / 64);
            int bottomColumn = bottomFaceIndex % 64;

            float bottomRowStartX = (bottomColumn * textureOffset) + bleedingFix;
            float bottomRowEndX = ((bottomColumn + 1) * textureOffset) - bleedingFix;
            float bottomRowStartY = (bottomRow * textureOffset) + bleedingFix;
            float bottomRowEndY = ((bottomRow + 1) * textureOffset) - bleedingFix;

            bottomVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowStartY));
            bottomVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowStartY));
            bottomVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(bottomRowStartX, bottomRowEndY));
            bottomVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(bottomRowEndX, bottomRowEndY));

            return bottomVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestNorthVertices(int northFaceIndex)
        {
            VertexPositionNormalTexture[] northVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int northRow = (int)Math.Floor((double)northFaceIndex / 64);
            int northColumn = northFaceIndex % 64;

            float northRowStartX = (northColumn * textureOffset) + bleedingFix;
            float northRowEndX = ((northColumn + 1) * textureOffset) - bleedingFix;
            float northRowStartY = (northRow * textureOffset) + bleedingFix;
            float northRowEndY = ((northRow + 1) * textureOffset) - bleedingFix;

            northVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopLeft, Vector3.Zero, new Vector2(northRowStartX, northRowStartY));
            northVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowStartY));
            northVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(northRowEndX, northRowEndY));

            return northVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestEastVertices(int eastFaceIndex)
        {
            VertexPositionNormalTexture[] eastVertices = new VertexPositionNormalTexture[4];
            //atlast is 2048 in size, textures are 32 in size.
            int eastRow = (int)Math.Floor((double)eastFaceIndex / 64);
            int eastColumn = eastFaceIndex % 64;

            float eastRowStartX = (eastColumn * textureOffset) + bleedingFix;
            float eastRowEndX = ((eastColumn + 1) * textureOffset) - bleedingFix;
            float eastRowStartY = (eastRow * textureOffset) + bleedingFix;
            float eastRowEndY = ((eastRow + 1) * textureOffset) - bleedingFix;

            eastVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowStartY));
            eastVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowStartY));
            eastVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(eastRowEndX, eastRowEndY));
            eastVertices[3] = new VertexPositionNormalTexture(ImBlockVertices.DownTopRight, Vector3.Zero, new Vector2(eastRowStartX, eastRowEndY));

            return eastVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestSouthVertices(int southFaceIndex)
        {
            VertexPositionNormalTexture[] southVertices = new VertexPositionNormalTexture[3];
            //atlast is 2048 in size, textures are 32 in size.
            int southRow = (int)Math.Floor((double)southFaceIndex / 64);
            int southColumn = southFaceIndex % 64;

            float southRowStartX = (southColumn * textureOffset) + bleedingFix;
            float southRowEndX = ((southColumn + 1) * textureOffset) - bleedingFix;
            float southRowStartY = (southRow * textureOffset) + bleedingFix;
            float southRowEndY = ((southRow + 1) * textureOffset) - bleedingFix;

            southVertices[0] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomLeft, Vector3.Zero, new Vector2(southRowEndX, southRowStartY));
            southVertices[1] = new VertexPositionNormalTexture(ImBlockVertices.UpBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowStartY));
            
            southVertices[2] = new VertexPositionNormalTexture(ImBlockVertices.DownBottomRight, Vector3.Zero, new Vector2(southRowStartX, southRowEndY));

            return southVertices;
        }

        private static VertexPositionNormalTexture[] CalculateTopWestWestVertices(int westFaceIndex)
        {
            VertexPositionNormalTexture[] westVertices = new VertexPositionNormalTexture[0];

            return westVertices;
        }

        static ImBlockVertexData GetTopWestRampBlockVertexData(int topIndex, int downIndex, int northIndex, int eastIndex,
            int southIndex, int westIndex)
        {
            ImBlockVertexData toReturn = new ImBlockVertexData();
            toReturn.UpVertices = CalculateTopWestUpVertices(topIndex);
            toReturn.UpIndices = ImBlockRampIndices.TopWest.UpIndices;
            toReturn.DownVertices = CalculateTopWestDownVertices(downIndex);
            toReturn.DownIndices = ImBlockRampIndices.TopWest.DownIndices;
            toReturn.NorthVertices = CalculateTopWestNorthVertices(northIndex);
            toReturn.NorthIndices = ImBlockRampIndices.TopWest.NorthIndices;
            toReturn.EastVertices = CalculateTopWestEastVertices(eastIndex);
            toReturn.EastIndices = ImBlockRampIndices.TopWest.EastIndices;
            toReturn.SouthVertices = CalculateTopWestSouthVertices(southIndex);
            toReturn.SouthIndices = ImBlockRampIndices.TopWest.SouthIndices;
            toReturn.WestVertices = CalculateTopWestWestVertices(westIndex);
            toReturn.WestIndices = ImBlockRampIndices.TopWest.WestIndices;
            toReturn.CalculateNormals();
            return toReturn;
        }

    }
}
