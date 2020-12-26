using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.SpawnPoint
{
    public class DefaultSpawnPoint : ISpawnPoint
    {
        public Vector2 Position { get; set; }

        public Point Center => Position.ToPoint();

        public DefaultSpawnPoint(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}
