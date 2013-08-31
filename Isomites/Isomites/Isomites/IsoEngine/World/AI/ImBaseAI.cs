// -----------------------------------------------------------------------
// <copyright file="BaseAI.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine.World;
using Isomites.IsoEngine.World.AI;
using Isomites.IsomiteEngine;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImBaseAI
    {
        public ImAIManager Parent;
        public Vector3 WorldPosition;
        private ImRenderBasic _baseRenderBasic;
        private Vector3 _offset;
        private float _rotation;
        private Vector3 _scale;

        private float _actionTime;
        private float _actionTimer;
        public bool Ready;

        public ImBaseAI(ImAIManager parent, float startTimerAt = 0f)
        {
            Parent = parent;
            _baseRenderBasic = ImAIModels.Man;
            _rotation = 0f;
            _offset = new Vector3(0, 0.175f, 0);
            _scale = Vector3.One;
            _rotation = ImRenderDirection.North;
            _actionTimer = startTimerAt;
            _actionTime = 500f;
            Ready = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!Ready)
            {
                _actionTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (_actionTimer >= _actionTime)
                {
                    Ready = true;
                    _actionTimer = _actionTimer - _actionTime;
                }
            }
        }

        public void Processed()
        {
            Ready = false;
        }

        public bool MoveNorth()
        {
            if (!Parent.SM.IsLocationObstructed(new ImSegmentLocation(WorldPosition + ImDirection.North)))
            {
                WorldPosition += ImDirection.North;
                _rotation = ImRenderDirection.North;
                Processed();
                return true;
            }

            return false;
        }

        public bool MoveEast()
        {
            if (!Parent.SM.IsLocationObstructed(new ImSegmentLocation(WorldPosition + ImDirection.East)))
            {
                WorldPosition += ImDirection.East;
                _rotation = ImRenderDirection.East;
                Processed();
                return true;
            }

            return false;
        }

        public bool MoveSouth()
        {
            if (!Parent.SM.IsLocationObstructed(new ImSegmentLocation(WorldPosition + ImDirection.South)))
            {
                WorldPosition += ImDirection.South;
                _rotation = ImRenderDirection.South;
                Processed();
                return true;
            }

            return false;
        }

        public bool MoveWest()
        {
            if (!Parent.SM.IsLocationObstructed(new ImSegmentLocation(WorldPosition + ImDirection.West)))
            {
                WorldPosition += ImDirection.West;
                _rotation = ImRenderDirection.West;
                Processed();
                return true;
            }

            return false;
        }


        public void Draw(GraphicsDevice device, Camera3D camera)
        {
            _baseRenderBasic.Draw(device, camera, WorldPosition, _offset, _rotation, _scale);
        }
    }
}
