using System;
using System.Collections.Generic;
using System.Linq;
using Isomites.IsoEngine;
using Isomites.IsoEngine.Debug;
using Isomites.IsoEngine.Editor;
using Isomites.IsoEngine.World;
using Isomites.IsomiteEngine.Items;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Isomites
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // GAME STUFF
        private FrameRateCounter _frameRateCounter;
        private ImSegmentManager _world;
        private ImEditor _editor;
        private Camera3D _camera3D;
        private SpriteFont _debugFont;

        private ImGameWorld _gameWorld;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 1000,
                PreferredBackBufferWidth = 1000,
                IsFullScreen = false

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
            ImGlobal.Init();
            // TODO: Add your initialization logic here

            _frameRateCounter = new FrameRateCounter();

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

            _gameWorld = new ImGameWorld(GraphicsDevice);
            _gameWorld.Load(Content);

            // TODO: use this.Content to load your game content here
            // GAME STUFF
            Effect modelEffect = Content.Load<Effect>("modelEffect");
            
            _camera3D = new Camera3D(GraphicsDevice);
            // Content.Load<Effect>("blockWorld"), Content.Load<Texture2D>("IsomitesAtlas")
            _world = new ImSegmentManager(GraphicsDevice);
            _world.LoadContent(Content, modelEffect);

            //_editor = new ImEditor(_world);
            //_editor.LoadContent(Content, modelEffect);

            _debugFont = Content.Load<SpriteFont>("Fonts/debugFont");
            DebugLog.Init(_debugFont);

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            InputHelper.Update(gameTime);
           // _camera3D.Update(gameTime);

           // _world.Update(gameTime);
            //_editor.Update();
            _gameWorld.Update(gameTime);


            if (InputHelper.IsNewKeyPress(Keys.F5))
            {
                IsFixedTimeStep = !IsFixedTimeStep;
                graphics.SynchronizeWithVerticalRetrace = !graphics.SynchronizeWithVerticalRetrace;
                graphics.ApplyChanges();
            }

            
            _frameRateCounter.Update(gameTime);
            DebugLog.Update();

            // HACK: camera kept resetting movement as mouse is being forced to centre of screen by Camera3D
            // Update again because it needs to set CurrentState to LastState again
            InputHelper.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            RasterizerState rs = new RasterizerState();

            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.Solid;
            GraphicsDevice.RasterizerState = rs;

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            _gameWorld.Draw();

           // _world.Draw(_camera3D);
           // _editor.Draw(GraphicsDevice, _camera3D);
            
            _frameRateCounter.Draw(spriteBatch, _debugFont);
            DebugLog.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
