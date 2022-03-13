using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Base interface for all classes whch have a texture.
    /// </summary>
    public interface IHasTexture : IHasPosition
    {
        /// <summary>
        /// Path + name of the texture to use for when rendering this object (can change in time).
        /// </summary>
        /// <returns>E.g. assets/enemies/default</returns>
        string TextureName
        {
            get;
        }

        /// <summary>
        /// Collection of all textures used by this object.
        /// </summary>
        IEnumerable<string> AllTextures
        {
            get;
        }

        /// <summary>
        /// Checks whether the given position collides with this object.
        /// </summary>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y.</param>
        /// <returns>True if the collision is detected.</returns>
        bool CheckColision(float x, float y);
    }
}
