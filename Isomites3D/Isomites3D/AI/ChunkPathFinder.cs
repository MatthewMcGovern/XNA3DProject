// -----------------------------------------------------------------------
// <copyright file="PathFinder.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Security.Cryptography;
using Isomites3D.CubeWorld;
using Microsoft.Xna.Framework;

namespace Isomites3D.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ChunkPathFinder
    {
        public static ChunkManager Chunks;

        private Vector3 _path;
        private Random _rng;

        private Vector3 _startNode;
        private Vector3 _goalNode;
        private ushort _goalBlockID;
        private List<Node> _nodesToExplore;
        private List<Vector3> _positionsExpanded; 

        public ChunkPathFinder()
        {
            _goalBlockID = 0;
            _nodesToExplore = new List<Node>();
            _positionsExpanded = new List<Vector3>();
            _rng = new Random();
        }

        public bool ExpandNodeToFindBlockID(Node node)
        {
            if (_positionsExpanded.Contains(node.Position))
            {
                _nodesToExplore.Remove(node);
                return false;
            }

            List<int> functionOrder = new List<int>();
            functionOrder.Add(0);
            functionOrder.Add(1);
            functionOrder.Add(2);
            functionOrder.Add(3);


            int n = functionOrder.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                int value = functionOrder[k];
                functionOrder[k] = functionOrder[n];
                functionOrder[n] = value;
            }

            _positionsExpanded.Add(node.Position);

            foreach (int function in functionOrder)
            {
                if (function == 0)
                {
                    Cube TempCube = Chunks.GetCubeAt((int)node.Position.X, (int)node.Position.Y, (int)node.Position.Z - 1, false);


                    if (TempCube.IsPassable())
                    {
                        Node southNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z - 1),
                            node);
                        _nodesToExplore.Add(southNode);
                    }
                    if (TempCube.Type == _goalBlockID)
                    {
                        Node southNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z - 1),
                            node);
                        _nodesToExplore.Add(southNode);
                        return true;
                    }
                }
                if (function == 1)
                {
                    Cube TempCube = Chunks.GetCubeAt((int)node.Position.X, (int)node.Position.Y, (int)node.Position.Z + 1, false);

                    if (TempCube.IsPassable())
                    {
                        Node northNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z + 1),
                            node);
                        _nodesToExplore.Add(northNode);
                    }
                    if (TempCube.Type == _goalBlockID)
                    {
                        Node northNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z + 1),
                           node);
                        _nodesToExplore.Add(northNode);
                        return true;
                    }
                }
                if (function == 2)
                {
                    Cube TempCube = Chunks.GetCubeAt((int)node.Position.X - 1, (int)node.Position.Y, (int)node.Position.Z, false);

                    if (TempCube.IsPassable())
                    {
                        Node westNode = new Node(new Vector3(node.Position.X - 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(westNode);
                    }
                    if (TempCube.Type == _goalBlockID)
                    {
                        Node westNode = new Node(new Vector3(node.Position.X - 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(westNode);
                        return true;
                    }
                }
                if (function == 3)
                {
                    Cube TempCube = Chunks.GetCubeAt((int)node.Position.X + 1, (int)node.Position.Y, (int)node.Position.Z, false);

                    if (TempCube.IsPassable())
                    {
                        Node eastNode = new Node(new Vector3(node.Position.X + 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(eastNode);
                    }
                    if (TempCube.Type == _goalBlockID)
                    {
                        Node eastNode = new Node(new Vector3(node.Position.X + 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(eastNode);
                        return true;   
                    }
                }
            }


            _nodesToExplore.Remove(node);
            return false;
        }

        public List<Vector3> GeneratePathToClosestBlockID(Vector3 startPosition, ushort blockID)
        {
            _goalBlockID = blockID;

            _nodesToExplore.Clear();
            _positionsExpanded.Clear();
            _startNode = startPosition;

            _nodesToExplore.Add(new Node(startPosition, null));

            while (_nodesToExplore.Count > 0)
            {
                if (ExpandNodeToFindBlockID(_nodesToExplore[0]))
                {
                    break;
                }
            }

            List<Vector3> path = new List<Vector3>();

            if (_nodesToExplore.Count == 0)
            {
                return path;
            }
            Node currentNode = _nodesToExplore[_nodesToExplore.Count - 1];

            
                currentNode = _nodesToExplore[_nodesToExplore.Count - 2];


            while (currentNode.PreviousNode != null)
            {
                path.Add(currentNode.Position);
                currentNode = currentNode.PreviousNode;
            }
            path.Add(currentNode.Position);
            path.Reverse();
            return path;
        }

        public bool ExpandNode(Node node)
        {
            if (_positionsExpanded.Contains(node.Position))
            {
                _nodesToExplore.Remove(node);
                return false;
            }

            List<int> functionOrder = new List<int>();
            functionOrder.Add(0);
            functionOrder.Add(1);
            functionOrder.Add(2);
            functionOrder.Add(3);


            int n = functionOrder.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                int value = functionOrder[k];
                functionOrder[k] = functionOrder[n];
                functionOrder[n] = value;
            }  
            
            _positionsExpanded.Add(node.Position);

            foreach (int function in functionOrder)
            {
                if (function == 0)
                {
                    if (Chunks.IsBlockPassable((int) node.Position.X, (int) node.Position.Y, (int) node.Position.Z - 1))
                    {
                        Node southNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z - 1),
                            node);
                        _nodesToExplore.Add(southNode);
                        if (IsGoalNode(southNode))
                            return true;
                    }
                }
                if (function == 1)
                {
                    if (Chunks.IsBlockPassable((int) node.Position.X, (int) node.Position.Y, (int) node.Position.Z + 1))
                    {
                        Node northNode = new Node(new Vector3(node.Position.X, node.Position.Y, node.Position.Z + 1),
                            node);
                        _nodesToExplore.Add(northNode);
                        if (IsGoalNode(northNode))
                            return true;
                    }
                }
                if (function == 2)
                {
                    if (Chunks.IsBlockPassable((int) node.Position.X - 1, (int) node.Position.Y, (int) node.Position.Z))
                    {
                        Node westNode = new Node(new Vector3(node.Position.X - 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(westNode);
                        if (IsGoalNode(westNode))
                            return true;
                    }
                }
                if (function == 3)
                {
                    if (Chunks.IsBlockPassable((int) node.Position.X + 1, (int) node.Position.Y, (int) node.Position.Z))
                    {
                        Node eastNode = new Node(new Vector3(node.Position.X + 1, node.Position.Y, node.Position.Z),
                            node);
                        _nodesToExplore.Add(eastNode);
                        if (IsGoalNode(eastNode))
                            return true;
                    }
                }
            }
            

            _nodesToExplore.Remove(node);
            return false;
        }

        public bool IsGoalNode(Node node)
        {
            return (_goalNode == node.Position);
        }

        public List<Vector3> GeneratePathVector3(Vector3 startPosition, Vector3 goalPosition)
        {
            if (!Chunks.IsBlockPassable(goalPosition))
            {
                return new List<Vector3>();
            }
            _nodesToExplore.Clear();
            _positionsExpanded.Clear();
            _startNode = startPosition;
            _goalNode = goalPosition;

            _nodesToExplore.Add(new Node(startPosition, null));

            while (_nodesToExplore.Count> 0)
            {
                if (ExpandNode(_nodesToExplore[0]))
                {
                    break;
                }
            }

            List<Vector3> path = new List<Vector3>();

            if (_nodesToExplore.Count == 0)
            {
                return path;
            }
            Node currentNode = _nodesToExplore[_nodesToExplore.Count - 1];
            

            while (currentNode.PreviousNode != null)
            {
                path.Add(currentNode.Position);
                currentNode = currentNode.PreviousNode;
            }

            path.Reverse();
            return path;
        }
    }
}
