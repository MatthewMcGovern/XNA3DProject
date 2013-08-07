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
        private static List<CubeType> _cubeTypes;

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

        static CubeType()
        {
            // VERY MESSY incoming
            // Defining like 36 vertices and indices for each cube is the ONLY way to haev multiple cubes that have different
            // Textures per face... Minecraft cheats and only has like 1 texture per cube but that sucks and is ugly.

            // In short: every cube type has 6 faces of 4 vertices and 6 indices each.
            // This is where their shape and texture is set.
            // Textures are very lame to set as they use a 0-1 float value that represents their location in a texture file...
            // Yes its crazy. so e.g. 0.125f of a 512 file is 64px.

            _cubeTypes = new List<CubeType>();
            CubeType air = new CubeType();
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

            _cubeTypes.Add(soil);
            _cubeTypes.Add(stone);
        }

        public static CubeType GetById(ushort id)
        {
            return _cubeTypes[id];
        }

        

        public CubeDrawData GetDrawData(int offset, int outlineOffset, Vector3 worldPosition, Connections neighbours)
        {
            // Add all possible lines to list... Should probably store this somewhere else?
            List<CubeOutline> _cubeOutlines = new List<CubeOutline>();
            _cubeOutlines.Add(Outlines.X.UBLtoUBR);
            _cubeOutlines.Add(Outlines.X.DBLtoDBR);
            _cubeOutlines.Add(Outlines.X.UTLtoUTR);
            _cubeOutlines.Add(Outlines.X.DTLtoDTR);

            _cubeOutlines.Add(Outlines.Y.UTLtoDTL);
            _cubeOutlines.Add(Outlines.Y.UTRtoDTR);
            _cubeOutlines.Add(Outlines.Y.UBLtoDBL);
            _cubeOutlines.Add(Outlines.Y.UBRtoDBR);

            _cubeOutlines.Add(Outlines.Z.UTLtoUBL);
            _cubeOutlines.Add(Outlines.Z.UTRtoUBR);
            _cubeOutlines.Add(Outlines.Z.DTLtoDBL);
            _cubeOutlines.Add(Outlines.Z.DTRtoDBR);

            // Get a drawData object for this cube.
            CubeDrawData drawData = new CubeDrawData(offset, outlineOffset);

            // the following 6 neighour checks is to remove faces and add lines
            // e.g. if a neighbour is NOT above it, add the top face vertices/idncies
            // if a neighbough IS above it, remove the lines that the up face obscures.

            if (!neighbours.HasFlag(Connections.Up))
                drawData.AddWorldCubeVertices(_upVertices, _upIndices, worldPosition);
            else
            {
                _cubeOutlines.Remove(Outlines.Z.UTLtoUBL);
                _cubeOutlines.Remove(Outlines.Z.UTRtoUBR);
                _cubeOutlines.Remove(Outlines.X.UBLtoUBR);
                _cubeOutlines.Remove(Outlines.X.UTLtoUTR);

            }

            if (!neighbours.HasFlag(Connections.Down))
                drawData.AddWorldCubeVertices(_downVertices, _downIndices, worldPosition);
            else
            {

                _cubeOutlines.Remove(Outlines.Z.DTLtoDBL);
                _cubeOutlines.Remove(Outlines.Z.DTRtoDBR);
                _cubeOutlines.Remove(Outlines.X.DBLtoDBR);
                _cubeOutlines.Remove(Outlines.X.DTLtoDTR);
            }
            if (!neighbours.HasFlag(Connections.North))
                drawData.AddWorldCubeVertices(_northVertices, _northIndices, worldPosition);
            else
            {

                
                _cubeOutlines.Remove(Outlines.Y.UBLtoDBL);
                _cubeOutlines.Remove(Outlines.Y.UBRtoDBR);
                _cubeOutlines.Remove(Outlines.X.UBLtoUBR);
                _cubeOutlines.Remove(Outlines.X.DBLtoDBR);
            }
            if (!neighbours.HasFlag(Connections.East))
                drawData.AddWorldCubeVertices(_eastVertices, _eastIndices, worldPosition);
            else
            {

                _cubeOutlines.Remove(Outlines.Y.UTRtoDTR);
                _cubeOutlines.Remove(Outlines.Y.UBRtoDBR);
                _cubeOutlines.Remove(Outlines.Z.UTRtoUBR);
                _cubeOutlines.Remove(Outlines.Z.DTRtoDBR);
            }
            if (!neighbours.HasFlag(Connections.South))
                drawData.AddWorldCubeVertices(_southVertices, _southIndices, worldPosition);
            else
            {
                _cubeOutlines.Remove(Outlines.Y.UTLtoDTL);
                _cubeOutlines.Remove(Outlines.Y.UTRtoDTR);
                _cubeOutlines.Remove(Outlines.X.UTLtoUTR);
                _cubeOutlines.Remove(Outlines.X.DTLtoDTR);
            }
            if (!neighbours.HasFlag(Connections.West))
                drawData.AddWorldCubeVertices(_westVertices, _westIndices, worldPosition);
            else
            {

                _cubeOutlines.Remove(Outlines.Y.UTLtoDTL);
                _cubeOutlines.Remove(Outlines.Y.UBLtoDBL);
                _cubeOutlines.Remove(Outlines.Z.UTLtoUBL);
                _cubeOutlines.Remove(Outlines.Z.DTLtoDBL);
            }

            // Only lines that made it get added
            foreach (CubeOutline outline in _cubeOutlines)
            {
                drawData.AddLineAt(worldPosition, outline);
            }
            
            // Return the data so the main buffer can add all the vertices/indices.
            return drawData;
        }
    }
}
