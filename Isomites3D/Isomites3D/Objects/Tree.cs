// -----------------------------------------------------------------------
// <copyright file="Tree.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Tree
    {
        public static Model Model;
        public static Texture2D[] Texture2Ds;
        public Vector3 Position;
        public Vector3 _offset;
        public Vector3 _scale;
        public float _rotation;

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

        public Tree(Vector3 worldPosition)
        {
            Random rand = new Random((int)(worldPosition.X * worldPosition.Y * worldPosition.Z));
            Position = worldPosition;
            _offset =   
            _rotation = (float)(rand.NextDouble()*(6.2 - 0));
            _scale = new Vector3(0.8f,0.8f,0.75f);
        }
        public void Draw(GraphicsDevice device, Matrix viewMatrix, Matrix projectionMatrix)
        {
            int i = 0;
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix worldMatrix = Matrix.CreateScale(_scale) *  Matrix.CreateRotationX(-1.57079633f) * Matrix.CreateRotationY(_rotation) * Matrix.CreateTranslation((Position.X * 0.5f) + _offset.X, (Position.Y * 0.5f) + 0.2f + _offset.Y, (Position.Z * 0.5f) + _offset.Z);
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
