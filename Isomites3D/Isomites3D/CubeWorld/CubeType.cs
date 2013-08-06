﻿// -----------------------------------------------------------------------
// <copyright file="CubeType.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Isomites3D.CubeWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CubeType
    {
        private static List<CubeType> _cubeTypes;
        private static List<Vector3> _outlinePoints;
        private static VertexPositionNormalTexture[] _outlineVertices;
        private static VertexPositionColor[] _smallCubeVertices;
        private static short[] _smallCubeIndices;
        private static short[] _outlineIndices;

        private VertexPositionNormalTexture[]  _vertices;
        private VertexPositionNormalTexture[] _upVertices;
        private short[] _upIndices;
        private VertexPositionNormalTexture[] _downVertices;
        private short[] _downIndices;
        private VertexPositionNormalTexture[] _northVertices;
        private short[] _northIndices;
        private VertexPositionNormalTexture[] _eastVertices;
        private short[] _eastIndices;
        private VertexPositionNormalTexture[] _southVertices;
        private short[] _southIndices;
        private VertexPositionNormalTexture[] _westVertices;
        private short[] _westIndices;
        private short[] _indices;

        static CubeType()
        {

            _outlinePoints = new List<Vector3>();
            _outlinePoints.Add(LinePoints.UpTopLeft);
            _outlinePoints.Add(LinePoints.UpTopRight);
            _outlinePoints.Add(LinePoints.UpBottomLeft);
            _outlinePoints.Add(LinePoints.UpBottomRight);
            _outlinePoints.Add(LinePoints.DownTopLeft);
            _outlinePoints.Add(LinePoints.DownTopRight);
            _outlinePoints.Add(LinePoints.DownBottomLeft);
            _outlinePoints.Add(LinePoints.DownBottomRight);

            _smallCubeVertices = new VertexPositionColor[8];
            _smallCubeVertices[0] = new VertexPositionColor(SmallCubeVertices.UpTopLeft, Color.Black);
            _smallCubeVertices[1] = new VertexPositionColor(SmallCubeVertices.UpTopRight, Color.Black);
            _smallCubeVertices[2] = new VertexPositionColor(SmallCubeVertices.UpBottomLeft, Color.Black);
            _smallCubeVertices[3] = new VertexPositionColor(SmallCubeVertices.UpBottomRight, Color.Black);
            _smallCubeVertices[4] = new VertexPositionColor(SmallCubeVertices.DownTopLeft, Color.Black);
            _smallCubeVertices[5] = new VertexPositionColor(SmallCubeVertices.DownTopRight, Color.Black);
            _smallCubeVertices[6] = new VertexPositionColor(SmallCubeVertices.DownBottomLeft, Color.Black);
            _smallCubeVertices[7] = new VertexPositionColor(SmallCubeVertices.DownBottomRight, Color.Black);

            _smallCubeIndices = new short[]
            {
                0,
                1,
                2,
                1,
                2,
                3,
                1,
                3,
                7,
                7,
                3,
                5,
                0,
                1,
                4,
                4,
                5,
                1,
                2,
                6,
                4,
                4,
                0,
                2,
                4,
                5,
                6,
                6,
                7,
                5,
                2,
                6,
                7,
                2,
                3,
                7
            };

            _outlineVertices = new VertexPositionNormalTexture[8];
            _outlineVertices[0] = new VertexPositionNormalTexture(LinePointVertices.UpTopLeft, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[1] = new VertexPositionNormalTexture(LinePointVertices.UpTopRight, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[2] = new VertexPositionNormalTexture(LinePointVertices.UpBottomLeft, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[3] = new VertexPositionNormalTexture(LinePointVertices.UpBottomRight, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[4] = new VertexPositionNormalTexture(LinePointVertices.DownTopLeft, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[5] = new VertexPositionNormalTexture(LinePointVertices.DownTopRight, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[6] = new VertexPositionNormalTexture(LinePointVertices.DownBottomLeft, Vector3.Zero, new Vector2(1f, 1f));
            _outlineVertices[7] = new VertexPositionNormalTexture(LinePointVertices.DownBottomRight, Vector3.Zero, new Vector2(1f, 1f));

            _outlineIndices = new short[]
            {
                0,
                1,
                2,
                2,
                1,
                3,
                4,
                5,
                6,
                6,
                4,
                7,
                1,
                7,
                3,
                7,
                5,
                1,
                0,
                6,
                4,
                6,
                4,
                2,
                1,
                0,
                4,
                4,
                5,
                1,
                2,
                7,
                6,
                3,
                7,
                6
            };

        _cubeTypes = new List<CubeType>();
            CubeType air = new CubeType();
            air._indices = new short[0];
            air._vertices = new VertexPositionNormalTexture[0];
            _cubeTypes.Add(air);
            
            CubeType soil = new CubeType();
           
            // UpFace
            soil._upVertices = new VertexPositionNormalTexture[4];
            soil._upVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.124f, 0.01f));
            soil._upVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0.01f, 0.01f));
            soil._upVertices[2] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.124f, 0.49f));
            soil._upVertices[3] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0.01f, 0.49f));

            soil._upIndices = new short[]
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            // DownFace
            soil._downVertices = new VertexPositionNormalTexture[4];
            soil._downVertices[0] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.24f, 0));
            soil._downVertices[1] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.125f, 0));
            soil._downVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.24f, 0.49f));
            soil._downVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.125f, 0.49f));

            soil._downIndices = new short[]
            {
                0,
                1,
                2,
                2,
                1,
                3
            };


            // SouthFace
            soil._southVertices = new VertexPositionNormalTexture[4];
            soil._southVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.5f, 0));
            soil._southVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0.624f, 0));
            soil._southVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.5f, 0.49f));
            soil._southVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.624f, 0.49f));

            soil._southIndices = new short[]
            {
                0,
                1,
                2,
                2,
                1,
                3,
            };

            // NorthFace
            soil._northVertices = new VertexPositionNormalTexture[4];
            soil._northVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.374f, 0));
            soil._northVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0.25f, 0));
            soil._northVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.374f, 0.49f));
            soil._northVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.25f, 0.49f));

            soil._northIndices= new short[]
            {
                0,
                2,
                1,
                2,
                3,
                1
            };
                
            // EastFace
            soil._eastVertices = new VertexPositionNormalTexture[4];
            soil._eastVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0.375f, 0));
            soil._eastVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0.49f, 0));
            soil._eastVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.49f, 0.49f));
            soil._eastVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.375f, 0.49f));

            soil._eastIndices = new short[]
            {
                0,
                1,
                2,
                2,
                3,
                0  
            };
           
            // WestFace
            soil._westVertices = new VertexPositionNormalTexture[4];
            soil._westVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.625f, 0));
            soil._westVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.74f, 0));
            soil._westVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.74f, 0.49f));
            soil._westVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.625f, 0.49f));

            soil._westIndices = new short[]
            {
                1,
                0,
                2,
                2,
                0,
                3
            };

            CubeType stone = new CubeType();
          
            stone._upVertices = new VertexPositionNormalTexture[4];
            stone._upVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.124f, 0.5f));
            stone._upVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0f, 0.5f));
            stone._upVertices[2] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.124f, 0.99f));
            stone._upVertices[3] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0f, 0.99f));

            stone._upIndices = new short[]
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

            stone._downVertices = new VertexPositionNormalTexture[4];
            stone._downVertices[0] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.24f, 0.5f));
            stone._downVertices[1] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.125f, 0.5f));
            stone._downVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.24f, 0.99f));
            stone._downVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.125f, 0.99f));

            stone._downIndices = new short[]
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

            stone._southVertices = new VertexPositionNormalTexture[4];
            stone._southVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.5f, 0.5f));
            stone._southVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0.624f, 0.5f));
            stone._southVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.5f, 0.99f));
            stone._southVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.624f, 0.99f));

            stone._southIndices = new short[]
            {
                0,
                1,
                2,
                2,
                1,
                3,
            };

            stone._northVertices = new VertexPositionNormalTexture[4];
            stone._northVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.374f, 0.5f));
            stone._northVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0.25f, 0.5f));
            stone._northVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.374f, 0.99f));
            stone._northVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.25f, 0.99f));

            stone._northIndices = new short[]
            {
                0,
                2,
                1,
                2,
                3,
                1
            };



            stone._eastVertices = new VertexPositionNormalTexture[4];
            stone._eastVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopRight, Vector3.Zero, new Vector2(0.375f, 0.5f));
            stone._eastVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomRight, Vector3.Zero, new Vector2(0.49f, 0.5f));
            stone._eastVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomRight, Vector3.Zero, new Vector2(0.49f, 0.99f));
            stone._eastVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopRight, Vector3.Zero, new Vector2(0.375f, 0.99f));

            stone._eastIndices = new short[]
            {
                0,
                1,
                2,
                2,
                3,
                0  
            };
       

            stone._westVertices = new VertexPositionNormalTexture[4];
            stone._westVertices[0] = new VertexPositionNormalTexture(CubeVertices.UpTopLeft, Vector3.Zero, new Vector2(0.625f, 0.5f));
            stone._westVertices[1] = new VertexPositionNormalTexture(CubeVertices.UpBottomLeft, Vector3.Zero, new Vector2(0.74f, 0.5f));
            stone._westVertices[2] = new VertexPositionNormalTexture(CubeVertices.DownBottomLeft, Vector3.Zero, new Vector2(0.74f, 0.99f));
            stone._westVertices[3] = new VertexPositionNormalTexture(CubeVertices.DownTopLeft, Vector3.Zero, new Vector2(0.625f, 0.99f));

            stone._westIndices = new short[]
            {
                1,
                0,
                2,
                2,
                0,
                3
            };
                

            soil._indices = new short[]
            {
                0,
                2,
                1,
                1,
                2,
                3,
                4,
                5,
                6,
                6,
                5,
                7,
                8,
                10,
                9,
                10,
                11,
                9,
                12,
                13,
                14,
                14,
                13,
                15,
                16,
                17,
                18,
                18,
                19,
                16,
                21,
                20,
                22,
                22,
                20,
                23
            };

            _cubeTypes.Add(soil);
            _cubeTypes.Add(stone);
        }

        public static CubeType GetById(ushort id)
        {
            return _cubeTypes[id];
        }

        

        public CubeDrawData GetDrawData(int offset, int outlineOffset, Vector3 worldPosition, Connections neighbours)
        {
            List<Vector3> cubeOutlinePoints = _outlinePoints.ToList();
            
            List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
            List<short> indices = new List<short>();

            CubeDrawData drawData = new CubeDrawData(offset, outlineOffset);

            if (!neighbours.HasFlag(Connections.Up))
                drawData.AddWorldCubeVertices(_upVertices, _upIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.UpTopLeft);
                cubeOutlinePoints.Remove(LinePoints.UpTopRight);
                cubeOutlinePoints.Remove(LinePoints.UpBottomLeft);
                cubeOutlinePoints.Remove(LinePoints.UpBottomRight);
            }

            if (!neighbours.HasFlag(Connections.Down))
                drawData.AddWorldCubeVertices(_downVertices, _downIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.DownTopLeft);
                cubeOutlinePoints.Remove(LinePoints.DownTopRight);
                cubeOutlinePoints.Remove(LinePoints.DownBottomLeft);
                cubeOutlinePoints.Remove(LinePoints.DownBottomRight);
            }
            if (!neighbours.HasFlag(Connections.North))
                drawData.AddWorldCubeVertices(_northVertices, _northIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.UpTopLeft);
                cubeOutlinePoints.Remove(LinePoints.UpTopRight);
                cubeOutlinePoints.Remove(LinePoints.DownTopLeft);
                cubeOutlinePoints.Remove(LinePoints.DownTopRight);
            }
            if (!neighbours.HasFlag(Connections.East))
                drawData.AddWorldCubeVertices(_eastVertices, _eastIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.UpTopRight);
                cubeOutlinePoints.Remove(LinePoints.UpBottomRight);
                cubeOutlinePoints.Remove(LinePoints.DownTopRight);
                cubeOutlinePoints.Remove(LinePoints.DownBottomRight);
            }
            if (!neighbours.HasFlag(Connections.South))
                drawData.AddWorldCubeVertices(_southVertices, _southIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.UpBottomLeft);
                cubeOutlinePoints.Remove(LinePoints.UpBottomRight);
                cubeOutlinePoints.Remove(LinePoints.DownBottomLeft);
                cubeOutlinePoints.Remove(LinePoints.DownBottomRight);
            }
            if (!neighbours.HasFlag(Connections.West))
                drawData.AddWorldCubeVertices(_westVertices, _westIndices, worldPosition);
            else
            {
                cubeOutlinePoints.Remove(LinePoints.UpTopLeft);
                cubeOutlinePoints.Remove(LinePoints.UpBottomLeft);
                cubeOutlinePoints.Remove(LinePoints.DownTopLeft);
                cubeOutlinePoints.Remove(LinePoints.DownBottomLeft);
            }


            drawData.AddSmallCubeAt(_smallCubeVertices, _smallCubeIndices, worldPosition, CubeVertices.UpBottomLeft);
            return drawData;
        }

        public short[] GetIndices()
        {
            return _indices;
        }

        public short[] GetIndices(Connections neighbours)
        {
            List<short> visibleIndices = new List<short>();
            byte facesProcessed = 0;
            if (!neighbours.HasFlag(Connections.Up))
            {
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                facesProcessed++;
            }
            if (!neighbours.HasFlag(Connections.South))
            {
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                facesProcessed++;
            }
            if (!neighbours.HasFlag(Connections.North))
            {
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                facesProcessed++;
            }
            if (!neighbours.HasFlag(Connections.Down))
            {
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                facesProcessed++;
            }

            if (!neighbours.HasFlag(Connections.East))
            {
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                facesProcessed++;
            }

            if (!neighbours.HasFlag(Connections.West))
            {
                visibleIndices.Add((short)(1 + (facesProcessed * 4)));
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(2 + (facesProcessed * 4)));
                visibleIndices.Add((short)(0 + (facesProcessed * 4)));
                visibleIndices.Add((short)(3 + (facesProcessed * 4)));
                facesProcessed++;
            }

            return visibleIndices.ToArray();
        }
    }
}
