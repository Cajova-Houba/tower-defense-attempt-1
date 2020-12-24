using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    public interface IHasPosition
    {
        public Vector2 Position
        {
            get;
            set;
        }
    }
}
