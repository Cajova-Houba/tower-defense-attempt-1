﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract
{
    /// <summary>
    /// Abstract reprezentation of the most basic game object.
    /// </summary>
    public abstract class AbstractGameObject : IHasPosition
    {
        public virtual Vector2 Position { get; set; }

        public Point Center => new Point(32, 32);

        public AbstractGameObject(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}
