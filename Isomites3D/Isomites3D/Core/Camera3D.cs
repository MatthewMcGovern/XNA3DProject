// -----------------------------------------------------------------------
// <copyright file="Camera3D.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Reflection;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites3D.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Camera3D
    {
        public GraphicsDevice Device;
        private Vector3 _position;
        private Vector3 _target;
        private float _yaw;
        private float _pitch;
        private float _roll;
        private float _speed;
        private Matrix _rotation;
        public Matrix ViewMatrix;
        public Matrix ProjectionMatrix;

        public Camera3D(GraphicsDevice device)
        {
            Device = device;
            ResetCamera();
        }

        public void Update()
        {
            HandleInput();
            UpdateViewMatrix();
        }

        private void MoveCamera(Vector3 addedVector)
        {
            _position += _speed * addedVector;
        }
        private void HandleInput()
        {
            if (InputHelper.IsKeyDown(Keys.J))
            {
                _yaw += .02f;
            }

            if (InputHelper.IsKeyDown(Keys.L))
            {
                _yaw -= .02f;
            }

            if (InputHelper.IsKeyDown(Keys.K))
            {
                _pitch -= .02f;
            }

            if (InputHelper.IsKeyDown(Keys.I))
            {
                _pitch += .02f;
            }

            if (InputHelper.IsKeyDown(Keys.U))
            {
                _roll -= .02f;
            }

            if (InputHelper.IsKeyDown(Keys.O))
            {
                _roll += .02f;
            }

            if (InputHelper.IsKeyDown(Keys.W))
            {
                MoveCamera(_rotation.Forward);
            }

            if (InputHelper.IsKeyDown(Keys.A))
            {
                MoveCamera(_rotation.Left);
            }

            if (InputHelper.IsKeyDown(Keys.S))
            {
                MoveCamera(_rotation.Backward);
            }

            if (InputHelper.IsKeyDown(Keys.D))
            {
                MoveCamera(_rotation.Right);
            }

            if (InputHelper.IsKeyDown(Keys.E))
            {
                MoveCamera(_rotation.Up);
            }

            if (InputHelper.IsKeyDown(Keys.Q))
            {
                MoveCamera(_rotation.Down);
            }
        }

        public void UpdateViewMatrix()
        {
            _rotation.Forward.Normalize();
            _rotation.Up.Normalize();
            _rotation.Right.Normalize();

            _rotation *= Matrix.CreateFromAxisAngle(_rotation.Right, _pitch);
            _rotation *= Matrix.CreateFromAxisAngle(_rotation.Up, _yaw);
            _rotation *= Matrix.CreateFromAxisAngle(_rotation.Forward, _roll);

            _yaw = 0.0f;
            _pitch = 0.0f;
            _roll = 0.0f;

            _target = _position + _rotation.Forward;

            Vector3 test = _rotation.Up;

            ViewMatrix = Matrix.CreateLookAt(_position, _target, new Vector3(0,1,0));

            float t = 1f;
        }

        public void ResetCamera()
        {
            _position = new Vector3(0, 2, 2);
            _target = new Vector3(0, 0, 0);
            _rotation = Matrix.Identity;

            _yaw = 0.0f;
            _pitch = 0.0f;
            _roll = 0.0f;

            _speed = 3f;

            ViewMatrix = Matrix.Identity;
            ProjectionMatrix = Matrix.CreateOrthographic(Device.Viewport.Width, Device.Viewport.Height, -5000f, 5000f);
            //ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
              //          Device.Viewport.AspectRatio, .2f, 5000f);
        }
    }
}
