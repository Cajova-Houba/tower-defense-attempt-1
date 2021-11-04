using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Enemy
{
    public class DefaultEnemy : AbstractLivingObject, IEnemy
    {
        const uint MAX_HP = 100;

        /// <summary>
        /// Destination is reached when it's closer to the current position than this treshold.
        /// 
        /// For the convenience, this constant holds squared treshold.
        /// </summary>
        const float DEST_REACHED_TRESHOLD_2 = 64*64;
         
        public override string TextureName => (Hp < MAX_HP / 2) ? Textures.DEFAULT_ENEMY_HURT : Textures.DEFAULT_ENEMY;

        public override IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_ENEMY_HURT, Textures.DEFAULT_ENEMY };

        public float Speed { get; private set; }

        public uint Damage { get; private set; }

        public float AttackSpeed { get; private set; }

        public uint Value => 25;

        /// <summary>
        /// Destination this enemy is trying to reach.
        /// </summary>
        private Vector2 Destination { get; set; }

        private Timer AttackTimer { get; set; }

        public DefaultEnemy(Vector2 position) : this(position.X, position.Y) {}

        public DefaultEnemy(float x, float y) : base(MAX_HP, x, y)
        {
            Speed = 0.5f;
            Damage = 10;
            AttackSpeed = 1;
            AttackTimer = new Timer((long)(1000 / AttackSpeed));
        }

        public void UpdateState(Map gameMap)
        {
            Destination = gameMap.Base.Position;

            if ((Position - Destination).LengthSquared() < DEST_REACHED_TRESHOLD_2) {
                Attack(gameMap.Base);
            } else
            {
                // vector from the current position to destination
                Vector2 distanceVector = Destination - Position;

                // we can now calculate x,y to add to the current 
                // position from the movement speed and distance vector
                float alpha = (float)Math.Asin(distanceVector.Y / distanceVector.Length());
                float y1 = (float)(Math.Sin(alpha) * Speed);
                float x1 = (float)(Math.Cos(alpha) * Speed);
                Vector2 newPosition = new Vector2(Position.X - x1, Position.Y + y1);

                // check if there's an obstacle in the new position and if there is, attack it
                IObstacle obstacle = gameMap.CheckObstacle(newPosition);
                if (obstacle != null)
                {
                    Attack(obstacle);
                } else
                {
                    Position = newPosition;
                }
            }
        }

        /// <summary>
        /// Checks if it's time for attack and attacks the given entity.
        /// </summary>
        /// <param name="entity">Entity to attack.</param>
        private void Attack(IHasHp entity)
        {
            if (AttackTimer.HasPassed())
            {
                entity.TakeHit(Damage);
            }
        }
    }
}
