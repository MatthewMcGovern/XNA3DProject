#region Using Statements
using System;
using System.Collections.Generic;
using CoreUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using OpenTK.Graphics.ES20;
using MathHelper = OpenTK.MathHelper;

#endregion

namespace _3DPlayground
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        private GraphicsDevice _device;
        SpriteBatch _spriteBatch;
        private Effect _effect;
        private VertexPositionColor[] _vertices;
        private int[] _indices;

        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;
        private Matrix _worldMatrix;

        private int _terrainWidth = 4;
        private int _terrainHeight = 3;
        private float[,] _heightData;

        private float _triAngle = 0f;
        private float _worldAngle = 0f;

        private Vector3 _cameraPosition = new Vector3(60, 80, -80);
        private Vector3 _lookAt = new Vector3(0, 0, 0);

        private Texture2D _heightMap;

        private SpriteFont _debugFont;
        private FrameRateCounter _frameRateCounter;

        private void SetUpVertices()
        {
            _vertices = new VertexPositionColor[_terrainWidth * _terrainHeight];
            for (int x = 0; x < _terrainWidth; x++)
            {
                for (int y = 0; y < _terrainHeight; y++)
                {
                    _vertices[x + y * _terrainWidth].Position = new Vector3(x, _heightData[x, y], -y);
                    _vertices[x + y * _terrainWidth].Color = Color.White;
                }
            }
        }

        private void SetUpIndices()
        {
            _indices = new int[(_terrainWidth - 1) * (_terrainHeight - 1) * 6];
            int counter = 0;
            for (int y = 0; y < _terrainHeight - 1; y++)
            {
                for (int x = 0; x < _terrainWidth - 1; x++)
                {
                    int lowerLeft = x + y * _terrainWidth;
                    int lowerRight = (x + 1) + y * _terrainWidth;
                    int topLeft = x + (y + 1) * _terrainWidth;
                    int topRight = (x + 1) + (y + 1) * _terrainWidth;

                   _indices[counter++] = topLeft;
                   _indices[counter++] = lowerRight;
                   _indices[counter++] = lowerLeft;

                   _indices[counter++] = topLeft;
                   _indices[counter++] = topRight;
                   _indices[counter++] = lowerRight;
                }
            }
        }

        private void LoadHeightData()
        {
            _terrainWidth = _heightMap.Width;
            _terrainHeight = _heightMap.Height;

            Color[] heightMapColors = new Color[_terrainWidth * _terrainHeight];
            _heightMap.GetData(heightMapColors);

            _heightData = new float[_terrainWidth, _terrainHeight];
            for (int x = 0; x < _terrainWidth; x++)
                for (int y = 0; y < _terrainHeight; y++)
                    _heightData[x, y] = heightMapColors[x + y * _terrainWidth].R / 5f;
        }

        private void SetUpCamera()
        {
            _viewMatrix = Matrix.CreateLookAt(_cameraPosition, _lookAt, new Vector3(0, 1, 0));
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio,
                1.0f, 300.0f);
            _worldMatrix = Matrix.Identity;
        }

        public Game1()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            _graphics.PreferredBackBufferWidth = 1980;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            _frameRateCounter = new FrameRateCounter();
            InputHelper.Init();

            Window.Title = "3DPlayground Test";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _device = _graphics.GraphicsDevice;

            _effect = Content.Load<Effect>("effects");

            _heightMap = Content.Load<Texture2D>("heightmap");

            _debugFont = Content.Load<SpriteFont>("debugFont");

            LoadHeightData();
            SetUpVertices();
            SetUpIndices();
            SetUpCamera();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHelper.Update(gameTime);
            // TODO: Add your update logic here

            if (InputHelper.IsKeyDown(Keys.Delete))
            {
                _worldAngle += 0.05f;
            }

            if (InputHelper.IsKeyDown(Keys.PageUp))
            {
                _cameraPosition.Y += 0.2f;
            }


            if (InputHelper.IsKeyDown(Keys.PageDown))
            {
                _cameraPosition.Y -= 0.2f;
            }

            if (InputHelper.IsKeyDown(Keys.Left))
            {
                _cameraPosition.X += 0.2f;
                _cameraPosition.Z += 0.2f;
                _lookAt.X += 0.2f;
                _lookAt.Z += 0.2f;
            }


            if (InputHelper.IsKeyDown(Keys.Up))
            {
                //_cameraPosition.Y += 0.2f;
                _cameraPosition.Z += 0.2f;
                _cameraPosition.X -= 0.2f;
                //_lookAt.Y += 0.2f;
                _lookAt.Z += 0.2f;
                _lookAt.X -= 0.2f;
            }


            if (InputHelper.IsKeyDown(Keys.Down))
            {
                //_cameraPosition.Y += 0.2f;
                _cameraPosition.Z -= 0.2f;
                _cameraPosition.X += 0.2f;
                //_lookAt.Y += 0.2f;
                _lookAt.Z -= 0.2f;
                _lookAt.X += 0.2f;
            }

            if (InputHelper.IsKeyDown(Keys.Right))
            {
                _cameraPosition.X -= 0.2f;
                _cameraPosition.Z -= 0.2f;
                _lookAt.X -= 0.2f;
                _lookAt.Z -= 0.2f;
            }

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
            rs.FillMode = FillMode.WireFrame;
            _device.RasterizerState = rs;

            _effect.CurrentTechnique = _effect.Techniques["ColoredNoShading"];
            _viewMatrix = Matrix.CreateLookAt(_cameraPosition, _lookAt, new Vector3(0, 1, 0));
            _effect.Parameters["xView"].SetValue(_viewMatrix);
            _effect.Parameters["xProjection"].SetValue(_projectionMatrix);

            //Vector3 rotAxis = new Vector3(3*_triAngle, _triAngle, 2*_triAngle);
            //rotAxis.Normalize();
            
            _worldMatrix = Matrix.CreateTranslation(-_terrainWidth/2.0f, 0, _terrainHeight/2.0f) * Matrix.CreateRotationY(_worldAngle);
            _effect.Parameters["xWorld"].SetValue(_worldMatrix);


            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
        
                _device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length, _indices, 0, _indices.Length/3, VertexPositionColor.VertexDeclaration);
                // draw HERE
            }

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _frameRateCounter.Draw(_spriteBatch, _debugFont);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
