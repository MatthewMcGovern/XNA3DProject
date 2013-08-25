// -----------------------------------------------------------------------
// <copyright file="BlockIndices.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Isomites.IsomiteEngine.Block
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ImBlockIndices
    {
        public static ushort[] UpIndices =
            {
                0,
                2,
                1,
                1,
                2,
                3
            };

        public static ushort[] DownIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3
            };

        public static ushort[] SouthIndices =
            {
                0,
                1,
                2,
                2,
                1,
                3,
            };

 public static ushort[] NorthIndices = 
            {
                0,
                2,
                1,
                2,
                3,
                1
            };

 public static ushort[] EastIndices =
            {
                0,
                1,
                2,
                2,
                3,
                0  
            };

  public static ushort[] WestIndices =
            {
                1,
                0,
                2,
                2,
                0,
                3
            };
    }
}
