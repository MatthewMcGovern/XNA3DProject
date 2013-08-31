// -----------------------------------------------------------------------
// <copyright file="ImAiManager.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsoEngine.AI;
using Isomites.IsomiteEngine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Isomites.IsoEngine.World.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImAIManager
    {
        private Random _rand;
        private List<ImBaseAI> _agents;
        public ImSegmentManager SM;

        public ImAIManager(ImSegmentManager parent)
        {
            SM = parent;
            _agents = new List<ImBaseAI>();
            _rand = new Random();


            for (int i = 0; i < 2000; i ++)
            {
                int x = _rand.Next(1, 64);
                int z = _rand.Next(1, 64);
                float startTimerAt = _rand.Next(0, 499);

                ImBaseAI newMan = new ImBaseAI(this, startTimerAt);
                newMan.WorldPosition = new Vector3(x, 4, z);
                _agents.Add(newMan);
            }
        }

        // to do: pathfinding, just start with basic NESW stuff.

        public void Update(GameTime gameTime)
        {
            List<ImBaseAI> agentsReady = new List<ImBaseAI>();
            for (int i = 0; i < _agents.Count; i++)
            {
                _agents[i].Update(gameTime);
                if (_agents[i].Ready)
                {
                   agentsReady.Add(_agents[i]);
                }
            }

            for (int i = 0; i < agentsReady.Count; i++)
            {
                int choice = _rand.Next(0, 5);

                if (choice == 0)
                {
                    agentsReady[i].Processed();
                }
                if (choice == 1)
                {
                    agentsReady[i].MoveNorth();
                }
                if (choice == 2)
                {
                    agentsReady[i].MoveEast();
                }
                if (choice == 3)
                {
                    agentsReady[i].MoveSouth();
                }
                if (choice == 4)
                {
                    agentsReady[i].MoveWest();
                }
            }
        }

        public void Draw(Camera3D camera)
        {
            foreach (ImBaseAI agent in _agents)
            {
                agent.Draw(SM.Device, camera);
            }
        }
    }
}
