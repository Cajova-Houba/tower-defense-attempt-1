using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Obstacle
{
    public class DefaultObstacle : AbstractLivingObject, IObstacle
    {

        public override string TextureName => Textures.DEFAULT_OBSTACLE;

        public override IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_OBSTACLE };

        private Vector2 position;
        public override Vector2 Position { get => position; 
            set
            {
                if (position != null)
                {
                    throw new InvalidOperationException("Obstacle cannot be moved.");
                }
                else
                {
                    position = value;
                }
            }
        }

        public DefaultObstacle(float x, float y) : base(50, x, y)
        {
        }
    }
}
