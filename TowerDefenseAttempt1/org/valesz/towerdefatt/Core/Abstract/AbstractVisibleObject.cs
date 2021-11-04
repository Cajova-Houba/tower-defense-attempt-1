using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract
{
    /// <summary>
    /// Abstract representation of a game object that is visible - has some texture.
    /// </summary>
    public abstract class AbstractVisibleObject : AbstractGameObject, IHasTexture
    {
        public const uint TEXTURE_WIDTH = 64;

        public abstract string TextureName { get; }
        public abstract IEnumerable<string> AllTextures { get; }

        public AbstractVisibleObject(float x, float y) : base(x, y) { }

        /// <summary>
        /// Checks whether the given position collides with this object.
        /// </summary>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y.</param>
        /// <returns>True if the collision is detected.</returns>
        public bool CheckColision(float x, float y)
        {
            return (Position.X <= x && Position.X + TEXTURE_WIDTH >= x) &&
                    (Position.Y <= y && Position.Y + TEXTURE_WIDTH >= y);
        }
    }
}
