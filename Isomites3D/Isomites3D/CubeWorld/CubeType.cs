// -----------------------------------------------------------------------
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
        public static List<CubeType> _cubeTypes;

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
            _cubeTypes = new List<CubeType>();
            CubeType air = new CubeType();
            air._indices = new short[0];
            air._vertices = new VertexPositionNormalTexture[0];
            _cubeTypes.Add(air);
            
            CubeType soil = new CubeType();
            soil._vertices = new VertexPositionNormalTexture[24];
            // UpFace
            soil._vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0));
            soil._vertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0f, 0));
            soil._vertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 0.5f));
            soil._vertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0f, 0.5f));

            soil._upVertices = new VertexPositionNormalTexture[4];
            soil._upVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.12f, 0));
            soil._upVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0f, 0));
            soil._upVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.12f, 0.4f));
            soil._upVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0f, 0.4f));

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
            soil._vertices[12] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.24f, 0));
            soil._vertices[13] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0));
            soil._vertices[14] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.24f, 0.49f));
            soil._vertices[15] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 0.49f));

            soil._downVertices = new VertexPositionNormalTexture[4];
            soil._downVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.24f, 0));
            soil._downVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0));
            soil._downVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.24f, 0.49f));
            soil._downVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 0.49f));

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
            soil._vertices[4] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0));
            soil._vertices[5] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0));
            soil._vertices[6] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0.49f));
            soil._vertices[7] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0.49f));

            soil._southVertices = new VertexPositionNormalTexture[4];
            soil._southVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0));
            soil._southVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0));
            soil._southVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0.49f));
            soil._southVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0.49f));

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
            soil._northVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0));
            soil._northVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0));
            soil._northVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0.49f));
            soil._northVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0.49f));

            soil._northIndices= new short[]
            {
                0,
                2,
                1,
                2,
                3,
                1
            };
                 

            soil._vertices[8] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0));
            soil._vertices[9] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0));
            soil._vertices[10] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0.49f));
            soil._vertices[11] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0.49f));

            // EastFace
            soil._eastVertices = new VertexPositionNormalTexture[4];
            soil._eastVertices[0] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0));
            soil._eastVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.49f, 0));
            soil._eastVertices[2] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.49f, 0.49f));
            soil._eastVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0.49f));

            soil._eastIndices = new short[]
            {
                0,
                1,
                2,
                2,
                3,
                0  
            };
           
            soil._vertices[16] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0));
            soil._vertices[17] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.49f, 0));
            soil._vertices[18] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.5f, 0.49f));
            soil._vertices[19] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0.49f));

            // WestFace
            soil._vertices[20] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0));
            soil._vertices[21] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0));
            soil._vertices[22] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0.49f));
            soil._vertices[23] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0.49f));

            soil._westVertices = new VertexPositionNormalTexture[4];
            soil._westVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0));
            soil._westVertices[1] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0));
            soil._westVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0.49f));
            soil._westVertices[3] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0.49f));

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
            stone._vertices = new VertexPositionNormalTexture[24];
          
            stone._upVertices = new VertexPositionNormalTexture[4];
            stone._upVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.124f, 0.5f));
            stone._upVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0f, 0.5f));
            stone._upVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.124f, 0.99f));
            stone._upVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0f, 0.99f));

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
            stone._downVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.24f, 0.5f));
            stone._downVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.125f, 0.5f));
            stone._downVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.24f, 0.99f));
            stone._downVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.125f, 0.99f));

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
            stone._southVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0.5f));
            stone._southVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0.5f));
            stone._southVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.5f, 0.99f));
            stone._southVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.624f, 0.99f));

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
            stone._northVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0.5f));
            stone._northVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0.5f));
            stone._northVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.374f, 0.99f));
            stone._northVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.25f, 0.99f));

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
            stone._eastVertices[0] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0.5f));
            stone._eastVertices[1] = new VertexPositionNormalTexture(new Vector3(0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.49f, 0.5f));
            stone._eastVertices[2] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.49f, 0.99f));
            stone._eastVertices[3] = new VertexPositionNormalTexture(new Vector3(0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.375f, 0.99f));

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
            stone._westVertices[0] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0.5f));
            stone._westVertices[1] = new VertexPositionNormalTexture(new Vector3(-0.25f, 0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0.5f));
            stone._westVertices[2] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, -0.25f), Vector3.Zero, new Vector2(0.74f, 0.99f));
            stone._westVertices[3] = new VertexPositionNormalTexture(new Vector3(-0.25f, -0.25f, 0.25f), Vector3.Zero, new Vector2(0.625f, 0.99f));

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

   

        public CubeDrawData GetDrawData(int offset, Vector3 worldPosition, Connections neighbours)
        {
            CubeDrawData drawData = new CubeDrawData(offset);
            //neighbours = Connections.Up;
            if (!neighbours.HasFlag(Connections.Up))
                drawData.AddData(_upVertices, _upIndices, worldPosition);
            if (!neighbours.HasFlag(Connections.Down))
                drawData.AddData(_downVertices, _downIndices, worldPosition);
            if (!neighbours.HasFlag(Connections.North))
                drawData.AddData(_northVertices, _northIndices, worldPosition);
            if (!neighbours.HasFlag(Connections.East))
                drawData.AddData(_eastVertices, _eastIndices, worldPosition);
            if (!neighbours.HasFlag(Connections.South))
                drawData.AddData(_southVertices, _southIndices, worldPosition);
            if (!neighbours.HasFlag(Connections.West))
                drawData.AddData(_westVertices, _westIndices, worldPosition);

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
