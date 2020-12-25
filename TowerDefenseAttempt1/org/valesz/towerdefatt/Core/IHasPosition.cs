using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    public interface IHasPosition
    {
        /// <summary>
        /// Top left corner of this object.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the center point of this object.
        /// </summary>
        public Point Center { get; }
    }
}
