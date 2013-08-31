// -----------------------------------------------------------------------
// <copyright file="ImAIModels.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Isomites.IsomiteEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsoEngine.World.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class ImAIModels
    {
        public static void LoadAll(ContentManager content, Effect modelEffect)
        {
            Man = new ImRenderBasic(content.Load<Model>("Models/AI/man"), modelEffect);
        }
        public static ImRenderBasic Man;
    }
}
