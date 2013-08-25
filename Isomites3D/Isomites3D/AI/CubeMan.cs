// -----------------------------------------------------------------------
// <copyright file="CubeMan.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Security.Cryptography.X509Certificates;
using Isomites3D.CubeWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CubeMan
    {
        public static Model Model;
        public static Texture2D[] Texture2Ds;

        public static void LoadModel(Model model, Effect effect)
        {
            List<Texture2D> textures = new List<Texture2D>();
            foreach (ModelMesh mesh in model.Meshes)
                foreach (BasicEffect currentEffect in mesh.Effects)
                    textures.Add(currentEffect.Texture);

            Texture2Ds = textures.ToArray();
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = effect.Clone();
                }
            }

            Model = model;
        }
        public Vector3 Position;
        public Vector3 _offset;
        private float _rotation;
        private float _actionTime;
        private float _actionTimer;
        public List<Vector3> Path; 
        public bool CanAct;

        public CubeMan(Vector3 startPos)
        {
            Random rand = new Random((int)(startPos.X + startPos.Y + startPos.Z));
            CanAct = false;
            Position = startPos;
            _rotation = 0f;
            _actionTime = (float)rand.Next(250, 250);
            _actionTimer = 0f;
            _offset = Vector3.Zero;
            Path = new List<Vector3>();
        }

        public void Update(GameTime gameTime)
        {
            if (!CanAct)
            {
                _actionTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (_actionTimer >= _actionTime)
                {
                    CanAct = true;
                    _actionTimer -= _actionTime;
                }
            }
        }

        public void MoveOnPath()
        {
            if (Path.Count > 0)
            {
                Vector3 nextPos = Path[0];
                Path.Remove(nextPos);

                if (nextPos.X - Position.X == -1)
                    MoveWest();
                if (nextPos.X - Position.X == 1)
                    MoveEast();
                if (nextPos.Z - Position.Z == -1)
                    MoveSouth();
                if (nextPos.Z - Position.Z == 1)
                    MoveNorth();   
            }
        }
        public void MoveNorth()
        {
            _rotation = 0f;
            Position.Z++;
            _offset = new Vector3(0, 0, 0.05f);
        }

        public void MoveSouth()
        {
            _rotation = 3.14159265f;
            Position.Z--;
            _offset = new Vector3(0,0, -0.05f);
        }

        public void MoveEast()
        {
            _rotation = 1.57079633f;
            Position.X++;
            _offset = new Vector3(0.05f, 0, 0);
        }

        public void MoveWest()
        {
            _rotation = 4.71238898f;
            Position.X--;
            _offset = new Vector3(-0.05f, 0, 0);
        }
        public void Draw(GraphicsDevice device, Matrix viewMatrix, Matrix projectionMatrix)
        {
            int i = 0;
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix worldMatrix =  Matrix.CreateRotationX(-1.57079633f) * Matrix.CreateRotationY(_rotation) * Matrix.CreateTranslation((Position.X * 0.5f) + _offset.X, (Position.Y * 0.5f) + 0.2f, (Position.Z * 0.5f) + _offset.Z);
                    currentEffect.Parameters["xTexture"].SetValue(Texture2Ds[i++]);
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(viewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(projectionMatrix);
                }
                mesh.Draw();
            }
        }
    }
}
