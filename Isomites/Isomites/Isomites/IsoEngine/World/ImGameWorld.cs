// -----------------------------------------------------------------------
// <copyright file="GameWorld.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine.AI;
using Isomites.IsoEngine.Editor;
using Isomites.IsoEngine.World.AI;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImGameWorld
    {
        private ImSegmentManager _segmentManager;
        private ImEditor _editor;
        private ImAIManager _aiManager;

        private Effect _blockWorldEffect;
        private Effect _modelEffect;
        public GraphicsDevice Device;
        private Camera3D _camera;
        private Texture2D _blockAtlas;


        public ImGameWorld(GraphicsDevice device)
        {
            Device = device;
            _camera = new Camera3D(Device);
        }

        public void Load(ContentManager content)
        {
            _blockWorldEffect = content.Load<Effect>("blockWorld");
            _modelEffect = content.Load<Effect>("modelEffect");
            _blockAtlas = content.Load<Texture2D>("IsomitesAtlas");
            //Content.Load<Texture2D>("IsomitesAtlas")
            ImItemTypes.Init(content, _modelEffect);

            _segmentManager = new ImSegmentManager(Device);
            _editor = new ImEditor(_segmentManager);

            _editor.LoadContent(content, _modelEffect);

            ImAIModels.LoadAll(content, _modelEffect);
            _aiManager = new ImAIManager(_segmentManager);
          
            
        }


        public void Update(GameTime gameTime)
        {
            _camera.Update(gameTime);
            _segmentManager.Update(gameTime);
            _editor.Update();
            _aiManager.Update(gameTime);
        }

        public void Draw()
        {
            Matrix worldMatrix = Matrix.Identity;
            _blockWorldEffect.CurrentTechnique = _blockWorldEffect.Techniques["Textured"];
            _blockWorldEffect.Parameters["xWorld"].SetValue(worldMatrix);
            _blockWorldEffect.Parameters["xView"].SetValue(_camera.ViewMatrix);
            _blockWorldEffect.Parameters["xProjection"].SetValue(_camera.ProjectionMatrix);
            _blockWorldEffect.Parameters["xTexture"].SetValue(_blockAtlas);

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                // draw block world stuff
                _segmentManager.DrawBlocks();
                
            }

            foreach (EffectPass pass in _blockWorldEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _editor.DrawBlocks();
            }

            _segmentManager.DrawItems(_camera);
            _editor.DrawItems(_camera);
            _aiManager.Draw(_camera);
        }
    }
}
