// -----------------------------------------------------------------------
// <copyright file="ImItemRender.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Isomites.IsomiteEngine.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImCachedRenderBasics
    {
        public static ImRenderBasic Tree;

        public static void Init(ContentManager content, Effect modelEffect)
        {
            Tree = new ImRenderBasic(content.Load<Model>("tree"), modelEffect);
        }
    }
}
