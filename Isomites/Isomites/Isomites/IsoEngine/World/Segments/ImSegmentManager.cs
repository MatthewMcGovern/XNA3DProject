// -----------------------------------------------------------------------
// <copyright file="ImSegmentManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using Isomites.IsoEngine.Block;
using Isomites.IsoEngine.Items;
using Isomites.IsomiteEngine;
using Isomites.IsomiteEngine.Block;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites.IsoEngine.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImSegmentManager
    {
        public ImSegment[,] Segments;
        private ImSegment _nullSegment;
        public Vector2 VectorSegment;
        public Vector3 ActiveSegment;
        public GraphicsDevice Device;

        private ImRenderBasic _BlockHighlight;
        private ImRenderBasic _BlockHighlightBlocked;
        private Vector3 _highlightPosition;
        private bool _highlightPositionIsObstacle;

        private List<ImWorldItem> _ItemsUnderCursor; 

        public ImSegmentManager(GraphicsDevice device)
        {
            Device = device;
            VectorSegment = Vector2.Zero;
            ActiveSegment = Vector3.Zero;
            _highlightPositionIsObstacle = false;
            _ItemsUnderCursor = new List<ImWorldItem>();

            Segments = new ImSegment[ImGlobal.WorldSegmentsSize.X, ImGlobal.WorldSegmentsSize.Z];

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x,z] = new ImSegment(Device, this, new Vector2(x,z)); 
                }
            }

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].AddItemAt(0,4,0);
                }
            }

            _highlightPosition = Vector3.Zero;

            _nullSegment = new ImSegment(device, this, new Vector2(-1,-1));
            _nullSegment.SetNull();

            /*SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 10)), ImBlockHelper.RampBlockMasks.Bottom.Soil | ImBlockHelper.RampBlockMasks.Top.Soil);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(12, 4, 12)), ImBlockHelper.RampBlockMasks.Top.Soil);*/

            
            
            // TEST BOTTOM RAMPS

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(11, 4, 11)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.North);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(12, 4, 11)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.North);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(13, 4, 11)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.North);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(14, 4, 11)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.North);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(15, 4, 11)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.North);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 12)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.East);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 13)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.East);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 14)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.East);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 15)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.East);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 4, 16)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.East);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(11, 4, 17)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.South);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(12, 4, 17)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.South);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(13, 4, 17)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.South);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(14, 4, 17)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.South);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(15, 4, 17)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.South);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 4, 12)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.West);
             SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 4, 13)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.West);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 4, 14)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.West);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 4, 15)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.West);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 4, 16)), ImBlockHelper.RampBlockMasks.Bottom.Debug | ImRampDirection.West);


            // TEST TOP RAMPS

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(11, 6, 11)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.North | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(12, 6, 11)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.North | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(13, 6, 11)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.North | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(14, 6, 11)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.North | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(15, 6, 11)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.North | ImBlockHelper.RampBlockMasks.Bottom.Soil);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 6, 12)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.East | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 6, 13)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.East | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 6, 14)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.East | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 6, 15)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.East | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(10, 6, 16)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.East | ImBlockHelper.RampBlockMasks.Bottom.Soil);

            SetBlockMaskAt(new ImSegmentLocation(new Vector3(11, 6, 17)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.South | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(12, 6, 17)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.South | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(13, 6, 17)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.South | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(14, 6, 17)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.South | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(15, 6, 17)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.South | ImBlockHelper.RampBlockMasks.Bottom.Soil);
        
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 6, 12)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.West | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 6, 13)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.West | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 6, 14)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.West | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 6, 15)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.West | ImBlockHelper.RampBlockMasks.Bottom.Soil);
            SetBlockMaskAt(new ImSegmentLocation(new Vector3(16, 6, 16)), ImBlockHelper.RampBlockMasks.Top.Debug | ImRampDirection.West | ImBlockHelper.RampBlockMasks.Bottom.Soil);

            //SetBlockMaskAt(new ImSegmentLocation(new Vector3(15, 6, 15)), ImBlockHelper.RampBlockMasks.Top.Debug | ImBlockHelper.RampBlockMasks.Bottom.Debug);

        }

        public void LoadContent(ContentManager content, Effect effect)
        {
            //_BlockHighlight = new ImRenderBasic(content.Load<Model>("Models/HighLight"), effect);
            //_BlockHighlightBlocked = new ImRenderBasic(content.Load<Model>("Models/HighLight_Blocked"), effect);
        }

        // GETS
        public ImSegment GetSegmentAt(ImSegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                return Segments[segmentLocation.SegmentX, segmentLocation.SegmentZ];

            return _nullSegment;
        }

        public ImRenderSegment GetRenderSegmentAt(ImSegmentLocation segmentLocation)
        {
            return GetSegmentAt(segmentLocation).RenderSegments[segmentLocation.RenderSegmentIndex];
        }

        public ImBlockMask GetBlockMaskAt(ImSegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                return GetRenderSegmentAt(segmentLocation).Blocks[segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ];

            return ImBlockHelper.BlockMasks.Null;
        }

        // SETS
        public void ClearItemsObstacleFlag(ImSegmentLocation segmentLocation)
        {
            GetRenderSegmentAt(segmentLocation).Blocks[
                segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ] =
                ImBlockHelper.BlockMasks.Air;

        }

        public void SetBlockMaskAt(ImSegmentLocation segmentLocation, ImBlockMask blockMask)
        {
            if (IsLocationInRange(segmentLocation))
            {
                // can't place block on obstructed air as its an item!
                if (GetBlockMaskAt(segmentLocation) == ImBlockHelper.BlockMasks.AirBlocked)
                    return;

                // can't delete block with item above it!
                if(blockMask == ImBlockHelper.BlockMasks.Air)
                    if (GetBlockMaskAt(segmentLocation.TranslateAndClone(new Vector3(0, 1, 0))) == ImBlockHelper.BlockMasks.AirBlocked)
                        return;

                GetRenderSegmentAt(segmentLocation).Blocks[
                    segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ] =
                    blockMask;

                // Mark all around it as dirty.
                SetLocationDirty(segmentLocation);
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.Up));
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.Down));
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.North));
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.East));
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.South));
                SetLocationDirty(segmentLocation.TranslateAndClone(ImDirection.West));
            }
        }

        public void SetFlagAt(ImSegmentLocation segmentLocation, ImBlockMask flag)
        {
            if (IsLocationInRange(segmentLocation))
                SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) | flag);
        }

        public void RemoveFlagAt(ImSegmentLocation segmentLocation, ImBlockMask flag)
        {
            if (IsLocationInRange(segmentLocation))
                SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) & ~flag);
        }

        public void SetLocationDirty(ImSegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                GetRenderSegmentAt(segmentLocation).Dirty = true;
        }

        public void SetLocationObstructed(ImSegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                SetFlagAt(segmentLocation, ImBlockMask.IsObstacle);
        }

        public void SetLocationPassable(ImSegmentLocation segmentLocation)
        {
            if (IsLocationInRange(segmentLocation))
                RemoveFlagAt(segmentLocation, ImBlockMask.IsObstacle);
        }

        // QUERIES
        public bool IsLocationObstructed(ImSegmentLocation segmentLocation)
        {
            if(IsLocationInRange(segmentLocation))
                return ImBlockHelper.IsBlockAnObstacle(GetBlockMaskAt(segmentLocation));

            return true;
        }

        public bool DoesLocationObscureDirection(ImSegmentLocation segmentLocation, Vector3 direction)
        {
            if(IsLocationInRange(segmentLocation))
                return ImBlockHelper.DoesBlockMaskObscureFromDirection(GetBlockMaskAt(segmentLocation), direction);

            return false;
        }

        public bool IsLocationInRange(ImSegmentLocation segmentLocation)
        {
            if (segmentLocation.WorldLocation.X < 0 || segmentLocation.WorldLocation.Y < 0 ||
                segmentLocation.WorldLocation.Z < 0)
                return false;
            if (segmentLocation.WorldLocation.X >= Segments.GetLength(0)*ImGlobal.RenderSegmentSize.X ||
                segmentLocation.WorldLocation.Z >= Segments.GetLength(1)*ImGlobal.RenderSegmentSize.Z ||
                segmentLocation.WorldLocation.Y >= ImGlobal.SegmentSize.Y)
                return false;
            
            return true;
        }

        // EXTRA
        public void AddItemAtWorldPosition(Vector3 position)
        {
            // Make sure its not floating

            foreach (Vector3 occupied in ImItemTypes.Tree.OccupiedSpace)
            {
                // make sure object fits!
                if (IsLocationObstructed(new ImSegmentLocation(occupied + position)))
                    return;
            }

            // ArBlocked means an object is currently present there! Can't block swap it.
            GetSegmentAt(new ImSegmentLocation(position)).AddItemAt(position);
        }

        // UPDATES
        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].Update(gameTime);
                }
            }
        }

        

           

        // DRAWING
        public void DrawBlocks()
        {
            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].DrawBlocks();
                }
            }
        }

        public void DrawItems(Camera3D camera)
        {
            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].DrawItems(Device, camera);
                }
            } 
        }
    }
}
