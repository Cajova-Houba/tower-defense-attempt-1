using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Spawn
{
    public class DefaultSpawnPoint : ISpawnPoint
    {
        public Vector2 Position { get; set; }

        public Point Center => new Point((int)Position.x, (int)Position.y);

        public DefaultSpawnPoint(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}
