using System;
using System.Collections.Generic;
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

        private Camera3D _camera3D;
        private Vector3 _cameraPosition = new Vector3(0, 80, 0);
        private Vector3 _lookAt = new Vector3(0, 0, 0);

        private GraphicsDevice _device;
        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;
        private Matrix _worldMatrix;
        private Effect _effect;
        private Texture2D _texture;
        private Cube _cube;
        private CubeManager _world;

        private FrameRateCounter _frameRateCounter;

        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;

        private SpriteFont _debugFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 1000,
                PreferredBackBufferWidth = 1000,
                SynchronizeWithVerticalRetrace = false
            };
            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
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
            _cube = new Cube();
            _device = graphics.GraphicsDevice;
            _effect = Content.Load<Effect>("effects");
            _texture = Content.Load<Texture2D>("cubemap");
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio,
    1.0f, 300.0f);
            InputHelper.Init();

            _vertexBuffer = new VertexBuffer(_device, VertexPositionTexture.VertexDeclaration, _cube.GetVertices().Count(), BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_cube.GetVertices());
            _indexBuffer = new IndexBuffer(_device, typeof(short), _cube.GetIndices().Count(), BufferUsage.WriteOnly);
            _indexBuffer.SetData(_cube.GetIndices());

            _device.Indices = _indexBuffer;
            _device.SetVertexBuffer(_vertexBuffer);

            _camera3D = new Camera3D(_device);
            _world = new CubeManager(20, 20, 20, _device);

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
            InputHelper.Update(gameTime);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _camera3D.Update();
            _world.Update();
            base.Update(gameTime);

            _frameRateCounter.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.CullCounterClockwiseFace;
            rs.FillMode = FillMode.Solid;
            _device.RasterizerState = rs;

            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.Default;
            _device.SamplerStates[0] = SamplerState.LinearWrap;

            Matrix worldMatrix = Matrix.CreateScale(50f);
            _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 2, 2), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio, 0.2f, 500.0f);
            _effect.CurrentTechnique = _effect.Techniques["TexturedNoShading"];
            _effect.Parameters["xWorld"].SetValue(worldMatrix);
            //_viewMatrix = Matrix.CreateLookAt(_cameraPosition, _lookAt, new Vector3(0, 1, 0));

            _effect.Parameters["xView"].SetValue(_camera3D.ViewMatrix);
            _effect.Parameters["xProjection"].SetValue(_camera3D.ProjectionMatrix);
            _effect.Parameters["xTexture"].SetValue(_texture);


            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                //_device.Indices = _indexBuffer;
               //_device.SetVertexBuffer(_vertexBuffer);
                //_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 4);

                //_device.DrawUserPrimitives(PrimitiveType.TriangleList, _cube.GetVertices(), 0, 2, VertexPositionTexture.VertexDeclaration);
               // _device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _cube.GetVertices(), 0, _cube.GetVertices().Count(), _cube.GetIndices(), 0, _cube.GetIndices().Count()/3, VertexPositionTexture.VertexDeclaration);
                
               _world.Draw(_device);
            }
            _frameRateCounter.Draw(spriteBatch, _debugFont);
            _world.DrawDebugInfo(spriteBatch, _debugFont);
            base.Draw(gameTime);
        }
    }
}
