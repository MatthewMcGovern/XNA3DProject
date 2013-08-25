// -----------------------------------------------------------------------
// <copyright file="ImSegmentManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;
using Isomites.IsoEngine.Items;
using Isomites.IsomiteEngine;
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
        public Vector2 VectorSegment;
        public Vector3 ActiveSegment;
        public GraphicsDevice Device;
        private Effect _blockWorldEffect;
        private Texture2D _blockAtlas;

        private ImRenderBasic _BlockHighlight;
        private ImRenderBasic _BlockHighlightBlocked;
        private Vector3 _highlightPosition;
        private bool _highlightPositionIsObstacle;

        private List<ImWorldItem> _ItemsUnderCursor; 

        public ImSegmentManager(GraphicsDevice device, Effect desiredEffect, Texture2D textureAtlas)
        {
            Device = device;
            VectorSegment = Vector2.Zero;
            ActiveSegment = Vector3.Zero;
            _blockWorldEffect = desiredEffect;
            _blockAtlas = textureAtlas;
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
        }

        public void LoadContent(ContentManager content, Effect effect)
        {
            _BlockHighlight = new ImRenderBasic(content.Load<Model>("Models/HighLight"), effect);
            _BlockHighlightBlocked = new ImRenderBasic(content.Load<Model>("Models/HighLight_Blocked"), effect);
        }

        // GETS
        public ImSegment GetSegmentAt(ImSegmentLocation segmentLocation)
        {
            return Segments[segmentLocation.SegmentX, segmentLocation.SegmentZ];
        }

        public ImRenderSegment GetRenderSegmentAt(ImSegmentLocation segmentLocation)
        {
            return GetSegmentAt(segmentLocation).RenderSegments[segmentLocation.RenderSegmentIndex];
        }

        public ImBlockMask GetBlockMaskAt(ImSegmentLocation segmentLocation)
        {
            return GetRenderSegmentAt(segmentLocation).Blocks[segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ];
        }

        // SETS
        public void SetBlockMaskAt(ImSegmentLocation segmentLocation, ImBlockMask blockMask)
        {
            GetRenderSegmentAt(segmentLocation).Blocks[
                segmentLocation.BlockX, segmentLocation.RenderSegmentBlockMaskIndex, segmentLocation.BlockZ] = blockMask;
        }

        public void SetFlagAt(ImSegmentLocation segmentLocation, ImBlockMask flag)
        {
            SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) | flag);
        }

        public void RemoveFlagAt(ImSegmentLocation segmentLocation, ImBlockMask flag)
        {
            SetBlockMaskAt(segmentLocation, GetBlockMaskAt(segmentLocation) & ~flag);
        }

        public void SetLocationDirty(ImSegmentLocation segmentLocation)
        {
            GetRenderSegmentAt(segmentLocation).Dirty = true;
        }

        public void SetLocationObstructed(ImSegmentLocation segmentLocation)
        {
            SetFlagAt(segmentLocation, ImBlockMask.IsObstacle);
        }

        public void SetLocationPassable(ImSegmentLocation segmentLocation)
        {
            RemoveFlagAt(segmentLocation, ImBlockMask.IsObstacle);
        }

        // QUERIES
        public bool IsLocationObstructed(ImSegmentLocation segmentLocation)
        {
            return ImBlockHelper.IsBlockAnObstacle(GetBlockMaskAt(segmentLocation));
        }

        public bool DoesLocationBlockViewFrom(ImSegmentLocation segmentLocation, Vector3 direction)
        {
            return ImBlockHelper.DoesBlockMaskObscureFromDirection(GetBlockMaskAt(segmentLocation), direction);
        }

        // EXTRA
        public void AddItemAtWorldPosition(Vector3 position)
        {
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
            HandleHighlight();

            if (InputHelper.IsNewKeyPress(Keys.Delete))
            {
                foreach (ImWorldItem item in _ItemsUnderCursor)
                {
                    item.Remove();
                }

                SetBlockMaskAt(new ImSegmentLocation(_highlightPosition), ImBlockHelper.BlockMasks.Air);
            }

            if (InputHelper.IsNewKeyPress(Keys.Home))
            {
                AddItemAtWorldPosition(_highlightPosition);
            }

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].Update(gameTime);
                }
            }
        }

        public void HandleHighlight()
        {
            Vector3 newPosition = _highlightPosition;
            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {

                newPosition.X += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
                newPosition.Z += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad7))
            {
                newPosition.X -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad9))
            {
                newPosition.Z -= 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.Add))
            {

                newPosition.Y += 1;
            }
            if (InputHelper.IsNewKeyPress(Keys.Subtract))
            {
                newPosition.Y -= 1;
            }

            // Following is for valid block ranges only.
            if (GetBlockMaskAt(new ImSegmentLocation(newPosition)) != ImBlockHelper.BlockMasks.Null)
            {
                if (newPosition != _highlightPosition)
                {
                    // check for an object?
                    int segmentX = (int)Math.Floor((double)newPosition.X / ImGlobal.SegmentSize.X);
                    int segmentZ = (int)Math.Floor((double)newPosition.Z / ImGlobal.SegmentSize.Z);

                    _ItemsUnderCursor = Segments[segmentX, segmentZ].Items.FindItemsAt(newPosition);
                }

                _highlightPosition = newPosition;
                if (InputHelper.IsNewKeyPress(Keys.Insert))
                {
                    SetBlockMaskAt(new ImSegmentLocation(_highlightPosition), ImBlockHelper.BlockMasks.Soil);
                }

                if (IsLocationObstructed(new ImSegmentLocation(_highlightPosition)))
                {
                    _highlightPositionIsObstacle = true;
                }
                else
                {
                    _highlightPositionIsObstacle = false;
                }
            }

            
        }

        // DRAWING
        public void Draw(Camera3D camera)
        {
            Matrix worldMatrix = Matrix.Identity;

            _blockWorldEffect.CurrentTechnique = _blockWorldEffect.Techniques["Textured"];
            _blockWorldEffect.Parameters["xWorld"].SetValue(worldMatrix);
            _blockWorldEffect.Parameters["xView"].SetValue(camera.ViewMatrix);
            _blockWorldEffect.Parameters["xProjection"].SetValue(camera.ProjectionMatrix);
            _blockWorldEffect.Parameters["xTexture"].SetValue(_blockAtlas);

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                for (int x = 0; x < Segments.GetLength(0); x++)
                {
                    for (int z = 0; z < Segments.GetLength(1); z++)
                    {
                        Segments[x, z].Draw();
                    }
                }
            }

            for (int x = 0; x < Segments.GetLength(0); x++)
            {
                for (int z = 0; z < Segments.GetLength(1); z++)
                {
                    Segments[x, z].DrawItems(Device, camera);
                }
            }

            if (_highlightPositionIsObstacle)
                _BlockHighlightBlocked.Draw(Device, camera, _highlightPosition, Vector3.Zero, 0f, new Vector3(1, 1, 1));
            else
                _BlockHighlight.Draw(Device, camera, _highlightPosition, Vector3.Zero, 0f, new Vector3(1,1,1));
        }
    }
}
