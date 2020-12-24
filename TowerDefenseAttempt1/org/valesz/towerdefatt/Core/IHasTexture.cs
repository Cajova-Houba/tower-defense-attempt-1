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
        public string TextureName
        {
            get;
        }

        /// <summary>
        /// Collection of all textures used by this object.
        /// </summary>
        public IEnumerable<string> AllTextures
        {
            get;
        }
    }
}
