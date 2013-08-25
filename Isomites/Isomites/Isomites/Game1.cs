using System;
using System.Collections.Generic;
using System.Linq;
using Isomites.GameWorld;
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
        private ImSegmentManager _world;
        private Camera3D _camera3D;

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

            // TODO: use this.Content to load your game content here
            // GAME STUFF
            Effect modelEffect = Content.Load<Effect>("modelEffect");
            ImItemTypes.Init(Content, modelEffect);
            _camera3D = new Camera3D(GraphicsDevice);
            _world = new ImSegmentManager(GraphicsDevice, Content.Load<Effect>("blockWorld"), Content.Load<Texture2D>("IsomitesAtlas"));
            _world.LoadContent(Content, modelEffect);
            
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
            _camera3D.Update(gameTime);

            _world.Update(gameTime);
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

            _world.Draw(_camera3D);
            

            base.Draw(gameTime);
        }
    }
}
