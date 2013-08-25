// -----------------------------------------------------------------------
// <copyright file="CubeManManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;
using Core;
using Isomites3D.CubeWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Isomites3D.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CubeManManager
    {
        private ChunkPathFinder _pathFinder;
        private List<CubeMan> _activeCubeMen;
        public ChunkManager Chunks;

        public CubeManManager(ChunkManager world)
        {
            _activeCubeMen = new List<CubeMan>();
            _pathFinder = new ChunkPathFinder();
            Chunks = world;
            ChunkPathFinder.Chunks = world;
        }

        public void GetPathForCubeMan(Vector3 startPos, Vector3 endPos, CubeMan cubeman)
        {
            cubeman.Path = _pathFinder.GeneratePathVector3(startPos, endPos);
        }

        public void AddCubeMan(CubeMan man)
        {
            _activeCubeMen.Add(man);
        }

        public void AddManAt(Vector3 position)
        {
            _activeCubeMen.Add(new CubeMan(position));
        }

        public void Update(GameTime gameTime)
        {
            if (InputHelper.IsKeyDown(Keys.M))
            {
                _activeCubeMen[0].Path = _pathFinder.GeneratePathVector3(_activeCubeMen[0].Position, Chunks.SelectedCubePosition);
            }

            if (InputHelper.IsKeyDown(Keys.T))
            {
                _activeCubeMen[0].Path = _pathFinder.GeneratePathToClosestBlockID(_activeCubeMen[0].Position, 3);
            }

            Random rand = new Random();
            foreach (CubeMan man in _activeCubeMen)
            {
                man.Update(gameTime);

             if (man.CanAct)
             {
                 man.CanAct = false;
                 man.MoveOnPath();
                 /*
                    int choice = rand.Next(0, 4);
                    if (choice == 0)
                    {
                        MoveNorth(man);
                    }
                    else if (choice == 1)
                    {
                        MoveEast(man);
                    }
                    else if (choice == 2)
                    {
                        //MoveSouth(man);
                    }
                    else if (choice == 3)
                    {
                    //    MoveWest(man);
                    }

                    man.CanAct = false;*/
             }
            }
        }

        public void MoveNorth(CubeMan man)
        {
            if (Chunks.IsBlockPassable((int)man.Position.X, (int)man.Position.Y, (int)man.Position.Z + 1))
            {
                man.MoveNorth();
            }
        }

        public void MoveSouth(CubeMan man)
        {
            if (Chunks.IsBlockPassable((int) man.Position.X, (int) man.Position.Y, (int) man.Position.Z - 1))
            {
                man.MoveSouth();
            }
        }

        public void MoveEast(CubeMan man)
        {
            if (Chunks.IsBlockPassable((int) man.Position.X + 1, (int) man.Position.Y, (int) man.Position.Z))
            {
                man.MoveEast();
            }
        }

        public void MoveWest(CubeMan man)
        {
            if (Chunks.IsBlockPassable((int) man.Position.X - 1, (int) man.Position.Y, (int) man.Position.Z))
            {
                man.MoveWest();
            }
        }

        public void Draw(GraphicsDevice device, Matrix viewMatrix, Matrix projectionMatrix)
        {
            foreach (CubeMan man in _activeCubeMen)
            {
                man.Draw(device, viewMatrix, projectionMatrix);
            }
        }
    }
}
