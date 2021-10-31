using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Obstacle
{
    public class DefaultObstacle : IObstacle
    {
        private readonly Point center = new Point(32, 32);

        public uint Hp { get; private set; }

        public uint MaxHp => 50;

        public string TextureName => Textures.DEFAULT_OBSTACLE;

        public IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_OBSTACLE };

        private Vector2 position;
        public Vector2 Position { get => position; set => throw new InvalidOperationException("Obstacle cannot be moved."); }

        public Point Center => center;

        public DefaultObstacle(float x, float y)
        {
            position = new Vector2(x, y);
            Hp = MaxHp;
        }

        public void TakeHit(uint damage)
        {
            if (Hp < damage)
            {
                Hp = 0;
            } else
            {
                Hp -= damage;
            }
        }
    }
}
