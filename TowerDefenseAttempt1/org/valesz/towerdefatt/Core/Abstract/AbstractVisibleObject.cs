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
        public abstract string TextureName { get; }
        public abstract IEnumerable<string> AllTextures { get; }
    }
}
