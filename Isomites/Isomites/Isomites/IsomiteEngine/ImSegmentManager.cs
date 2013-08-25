// -----------------------------------------------------------------------
// <copyright file="ImSegmentManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Remoting.Messaging;
using Isomites.IsomiteEngine;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites.GameWorld
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

        public void Update(GameTime gameTime)
        {
            HandleHighlight();

            if (InputHelper.IsNewKeyPress(Keys.Delete))
            {
                foreach (ImWorldItem item in _ItemsUnderCursor)
                {
                    item.Remove();
                }

                AddBlockMaskAtWorldPosition(ImGlobal.BlockMasks.Air, _highlightPosition);
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

        public void AddItemAtWorldPosition(Vector3 position)
        {
            foreach (Vector3 occupied in ImItemTypes.Tree.OccupiedSpace)
            {
                // make sure object fits!
                if(IsWorldPositionAnObstacle(occupied + position))
                    return;
            }

            int segmentX = (int)Math.Floor((double)position.X / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)position.Z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return;
            }

            int blockX = (int)position.X % ImGlobal.SegmentSize.X;
            int blockZ = (int)position.Z % ImGlobal.SegmentSize.Z;
            int blockY = (int)position.Y;

            // ArBlocked means an object is currently present there! Can't block swap it.
            Segments[segmentX, segmentZ].AddItemAt(position);
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
            if (GetBlockMaskAtWorldPosition(newPosition) != ImGlobal.BlockMasks.Null)
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
                    AddBlockMaskAtWorldPosition(ImGlobal.BlockMasks.Soil, _highlightPosition);
                }

                if (ImBlockHelper.IsBlockAnObstacle(GetBlockMaskAtWorldPosition(_highlightPosition)))
                {
                    _highlightPositionIsObstacle = true;
                }
                else
                {
                    _highlightPositionIsObstacle = false;
                }
            }

            
        }


        public ImBlockMask GetBlockMaskAtWorldPosition(Vector3 position)
        {
            return GetBlockMaskAtWorldPosition((int) position.X, (int) position.Y, (int) position.Z);
        }

        public bool IsWorldPositionAnObstacle(Vector3 position)
        {
            return IsWorldPositionAnObstacle((int) position.X, (int) position.Y, (int) position.Z);
        }


        public bool IsWorldPositionAnObstacle(int x, int y, int z)
        {
            int segmentX = (int)Math.Floor((double)x / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return true;
            }

            int blockX = x % ImGlobal.SegmentSize.X;
            int blockZ = z % ImGlobal.SegmentSize.Z;
            int blockY = y;

            return Segments[segmentX, segmentZ].IsInternalBlockAnObstacle(blockX, blockY, blockZ);
        }

        public void AddBlockMaskAtWorldPosition(ImBlockMask blockMask, Vector3 position)
        {
            int segmentX = (int)Math.Floor((double)position.X / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)position.Z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return;
            }

            int blockX = (int)position.X % ImGlobal.SegmentSize.X;
            int blockZ = (int)position.Z % ImGlobal.SegmentSize.Z;
            int blockY = (int)position.Y;

            // ArBlocked means an object is currently present there! Can't block swap it.
            if (Segments[segmentX, segmentZ].GetInternalBlockMaskAt(blockX, blockY, blockZ) ==
                ImGlobal.BlockMasks.AirBlocked)
                return;
            Segments[segmentX, segmentZ].AddBlockMaskAt(blockMask, blockX, blockY, blockZ);

            // Now have to touch all the surrounding areas as dirty too. Probably a better way to do this.
            MarkWorldPositionAsDirty(position + ImDirection.Up);
            MarkWorldPositionAsDirty(position + ImDirection.Down);
            MarkWorldPositionAsDirty(position + ImDirection.East);
            MarkWorldPositionAsDirty(position + ImDirection.West);
            MarkWorldPositionAsDirty(position + ImDirection.North);
            MarkWorldPositionAsDirty(position + ImDirection.South);
        }

        public void MarkWorldPositionAsDirty(Vector3 position)
        {
            int segmentX = (int)Math.Floor((double)position.X / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)position.Z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return;
            }

            Segments[segmentX, segmentZ].RenderSegments[ImGlobal.RenderSegmentIndices[(int)position.Y]].Dirty = true;

            // Now have to touch all the surrounding areas as dirty too. Probably a better way to do this.
        }

        public void MarkWorldPositionAsObstacle(int x, int y, int z)
        {
            int segmentX = (int)Math.Floor((double)x / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return;
            }

            int blockX = x % ImGlobal.SegmentSize.X;
            int blockZ = z % ImGlobal.SegmentSize.Z;
            int blockY = y;

            Segments[segmentX, segmentZ].SetBlockMaskAsObstacle(blockX, blockY, blockZ);
        }

        public void MarkWorldPositionAsPassable(int x, int y, int z)
        {
            int segmentX = (int)Math.Floor((double)x / ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return;
            }

            int blockX = x % ImGlobal.SegmentSize.X;
            int blockZ = z % ImGlobal.SegmentSize.Z;
            int blockY = y;

            Segments[segmentX, segmentZ].SetBlockMaskAsPassable(blockX, blockY, blockZ);
        }

        public ImBlockMask GetBlockMaskAtWorldPosition(int x, int y, int z)
        {
            int segmentX = (int)Math.Floor((double)x/ImGlobal.SegmentSize.X);
            int segmentZ = (int)Math.Floor((double)z / ImGlobal.SegmentSize.Z);

            if (segmentX < 0 || segmentZ < 0 || segmentX >= Segments.GetLength(0) || segmentZ >= Segments.GetLength(1))
            {
                return ImGlobal.BlockMasks.Null;
            }

            int blockX = x%ImGlobal.SegmentSize.X;
            int blockZ = z%ImGlobal.SegmentSize.Z;
            int blockY = y;

            return Segments[segmentX, segmentZ].GetInternalBlockMaskAt(blockX, blockY, blockZ);
        }

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
