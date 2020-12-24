using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Enemy
{
    public class DefaultEnemy : IEnemy
    {
        const uint MAX_HP = 100;

        /// <summary>
        /// Destination is reached when it's closer to the current position than this treshold.
        /// 
        /// For the convenience, this constant holds squared treshold.
        /// </summary>
        const float DEST_REACHED_TRESHOLD_2 = 64*64;

        /// <summary>
        /// Constant used to initialize NextAttack.
        /// </summary>
        const long NO_ATTACK = -1;

        public string TextureName => (Hp < MAX_HP / 2) ? "assets/enemies/default_hurt" : "assets/enemies/default";

        public IEnumerable<string> AllTextures => new string[] { "assets/enemies/default_hurt", "assets/enemies/default" };

        public Vector2 Position { get; set; }

        public uint Hp {
            get;
            private set;
        }

        public float Speed { get; private set; }

        public uint Damage { get; private set; }

        public float AttackSpeed { get; private set; }

        /// <summary>
        /// Destination this enemy is trying to reach.
        /// </summary>
        private Vector2 Destination { get; set; }

        /// <summary>
        /// Time when the next attack is allowed in millis. Initialized to -1.
        /// </summary>
        private long NextAttack { get; set; }

        public DefaultEnemy(float x, float y)
        {
            Hp = MAX_HP;
            Position = new Vector2(x, y);
            Speed = 0.5f;
            Damage = 10;
            AttackSpeed = 1;
            NextAttack = NO_ATTACK;
        }

        public void TakeHit(uint damage)
        {
            if (Hp < damage)
            {
                Hp = 0;
            }
            else
            {
                Hp -= damage;
            }
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
                Position = new Vector2(Position.X - x1, Position.Y + y1);
            }
        }

        /// <summary>
        /// Checks if it's time for attack and attacks the given entity.
        /// </summary>
        /// <param name="entity">Entity to attack.</param>
        private void Attack(IHasHp entity)
        {
            if (NextAttack == NO_ATTACK)
            {
                entity.TakeHit(Damage);
                NextAttack = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + (long)(1000 / AttackSpeed);
            } else
            {
                long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (now >= NextAttack)
                {
                    entity.TakeHit(Damage);
                    NextAttack = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + (long)(1000 / AttackSpeed);
                }
            }
        }
    }
}
