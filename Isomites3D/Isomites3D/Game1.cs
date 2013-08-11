using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Isomites3D.Core;
using Isomites3D.CubeWorld;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Isomites3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Custom rendertargets.
        RenderTarget2D sceneRenderTarget;
        RenderTarget2D normalDepthRenderTarget;
            

        private Camera3D _camera3D;

        private GraphicsDevice _device;
        private Matrix _viewMatrix;
        private Matrix _worldMatrix;
        private Effect _effect;
        private Texture2D _texture;
        private CubeManager _world;
        private ChunkManager _chunkManager;

        private FillMode _fillMode;
        private CullMode _cullMode;

        private FrameRateCounter _frameRateCounter;

        private SpriteFont _debugFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 1000,
                PreferredBackBufferWidth = 1000
               // SynchronizeWithVerticalRetrace = false
            };
            Content.RootDirectory = "Content";
            //graphics.PreferMultiSampling = true;
            
           //IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _device = graphics.GraphicsDevice;
            _effect = Content.Load<Effect>("effects");
            //_effect = Content.Load<Effect>("effects");
            _texture = Content.Load<Texture2D>("cubemap");
            
            InputHelper.Init();

            _fillMode = FillMode.Solid;
            _cullMode = CullMode.CullCounterClockwiseFace;


            _camera3D = new Camera3D(_device);
            _world = new CubeManager(32, 64, 32, _device);
            _chunkManager = new ChunkManager(_device);

            _debugFont = Content.Load<SpriteFont>("debugFont");

            _frameRateCounter = new FrameRateCounter();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get any user input.
            InputHelper.Update(gameTime);

            _camera3D.Update(gameTime);
            _world.Update();
            _chunkManager.Update();
            

            _frameRateCounter.Update(gameTime);
            

            base.Update(gameTime);


            if (InputHelper.IsNewKeyPress(Keys.F5))
            {
                IsFixedTimeStep = !IsFixedTimeStep;
                graphics.SynchronizeWithVerticalRetrace = !graphics.SynchronizeWithVerticalRetrace;
                graphics.ApplyChanges();
            }
            if (InputHelper.IsNewKeyPress(Keys.F6))
            {
                _fillMode = FillMode.Solid;
            }
            if (InputHelper.IsNewKeyPress(Keys.F7))
            {
                _fillMode = FillMode.WireFrame;
            }
            if (InputHelper.IsNewKeyPress(Keys.F10))
            {
                _cullMode = CullMode.CullCounterClockwiseFace;
            }
            if (InputHelper.IsNewKeyPress(Keys.F11))
            {
                _cullMode = CullMode.CullClockwiseFace;
            }
            if (InputHelper.IsNewKeyPress(Keys.F12))
            {
                _cullMode = CullMode.None;
            }
            // HACK: camera kept resetting movement as mouse is being forced to centre of screen by Camera3D
            // Update again because it needs to set CurrentState to LastState again
            InputHelper.Update(gameTime);
        }

        public void DrawWorld(string techniqueName)
        {
            _effect.CurrentTechnique = _effect.Techniques[techniqueName];
             // Set suitable renderstates for drawing a 3D model.
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            _worldMatrix = Matrix.Identity;

            _effect.Parameters["xWorld"].SetValue(_worldMatrix);
            _effect.Parameters["xView"].SetValue(_camera3D.ViewMatrix);
            _effect.Parameters["xProjection"].SetValue(_camera3D.ProjectionMatrix);
            _effect.Parameters["xTexture"].SetValue(_texture);

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
              // _world.Draw();
               _chunkManager.Draw();
            }

            _effect.CurrentTechnique = _effect.Techniques["ColoredNoShading"];
            _effect.Parameters["xWorld"].SetValue(_worldMatrix);
            _effect.Parameters["xView"].SetValue(_camera3D.ViewMatrix);
            _effect.Parameters["xProjection"].SetValue(_camera3D.ProjectionMatrix);
            _effect.Parameters["xTexture"].SetValue(_texture);

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _world.DrawOutline();
                _chunkManager.DrawOutline();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
                RasterizerState rs = new RasterizerState();
           
                rs.CullMode = _cullMode;
                rs.FillMode = _fillMode;
                _device.RasterizerState = rs;


                _device.Clear(Color.Black);



               _device.Clear(Color.CornflowerBlue);

                DrawWorld("Textured");



            _frameRateCounter.Draw(spriteBatch, _debugFont);

            string controls = "Controls:\n" +
                               "F1: Ghost Cam\n" +
                               "F2: Orthographic Cam\n" +
                               "P Hold: Hides outlines\n" +
                               "WASD: Ghost Cam FPS movement\n" +
                               "Q/Z: Up/Down Cam movement\n"+
                               "F5: Toggle FPS Cap (fucks with camera controls)\n" +
                               "F6: Draw Solid\n" +
                               "F7: Draw WireFrame\n" +
                               "F10: CullCounterClockwise (default)\n" +
                               "F11: Cull Clockwise\n" +
                               "F12: Cull None\n" +
                               "Apparently drawing the controls to the screen reduces my FPS from 7000 to 5000?";

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);      
            //spriteBatch.DrawString(_debugFont, controls, new Vector2(32, 64), Color.Yellow);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
