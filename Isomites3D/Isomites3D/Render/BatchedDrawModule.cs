// -----------------------------------------------------------------------
// <copyright file="BatchedDrawData.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;

namespace Isomites3D.Render
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BatchedDrawModule <T> where T : struct 
    {
        private List<DrawModule<T>> _drawModules;
        private DrawModule<T> _currentModule; 
        public GraphicsDevice Device;
        public VertexDeclaration VertexDeclaration;

        public BatchedDrawModule(GraphicsDevice device, VertexDeclaration vertexDeclaration)
        {
            Device = device;
            VertexDeclaration = vertexDeclaration;

            Reset();
        }

        public void Reset()
        {
            _drawModules = new List<DrawModule<T>>();
            CreateNewModule();
        }

        public DrawModule<T> GetCurrentModule()
        {
            return _currentModule;
        }

        public void CreateNewModule()
        {
             DrawModule<T> newModule = new DrawModule<T>(Device, VertexDeclaration);
            _drawModules.Add(newModule);
            _currentModule = newModule;
        }

        public void AddData(List<T> vertices, List<ushort> indices)
        {
            if (!_currentModule.AddData(vertices, indices))
            {
                CreateNewModule();
                AddData(vertices, indices);
            }
        }

        public void PrepareToDraw()
        {
            foreach (DrawModule<T> module in _drawModules)
            {
                module.PrepareToDraw();
            }
        }

        public void Draw()
        {
            foreach (DrawModule<T> module in _drawModules)
            {
                module.Draw();
            }
        }
    }
}
