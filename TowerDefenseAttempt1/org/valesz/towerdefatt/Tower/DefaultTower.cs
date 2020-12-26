using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Tower
{
    public class DefaultTower : ITower
    {
        /// <summary>
        /// Constant used to initialize NextAttack.
        /// </summary>
        const long NO_ATTACK = -1;

        public string TextureName
        {
            get
            {
                if (shooting)
                {
                    return Selected ? "assets/towers/default_shot_selected" : "assets/towers/default_shot";
                } else
                {
                    return Selected ? "assets/towers/default_selected" : "assets/towers/default";
                }
            }
        }

        public IEnumerable<string> AllTextures => new string[] { "assets/towers/default", "assets/towers/default_shot", "assets/towers/default_selected", "assets/towers/default_shot_selected" };

        public Vector2 Position { get; set; }

        public uint Damage => 10;

        public float AttackSpeed => 2;

        public float AttackRange => 100;

        public Point[] Shot { get; private set; }
        public Point ShootingPoint => new Point(34, 9);
        public Point Center => new Point(32,32);

        public uint Price => 50;

        public bool Selected { get; set; }

        /// <summary>
        /// Time when the next attack is allowed in millis. Initialized to -1.
        /// </summary>
        private long NextAttack { get; set; }


        private bool shooting = false;
        private long stopShootingAnimation;

        public DefaultTower(float x, float y)
        {
            Position = new Vector2(x, y);
            stopShootingAnimation = -1;
            Shot = null;
            Selected = false;
        }

        public ITower Clone(float x, float y)
        {
            return new DefaultTower(x, y);
        }

        public void UpdateState(Map gameMap)
        {
            // pick the nearest enemy and shoot
            if (gameMap.Enemies.Count == 0)
            {
                return;
            }


            float minDist = float.MaxValue;
            IHasHp nearestEnemy = null;
            foreach(IEnemy enemy in gameMap.Enemies)
            {
                float dist = (Position - enemy.Position).LengthSquared();

                if (minDist == float.MaxValue || dist < minDist)
                {
                    nearestEnemy = enemy;
                    minDist = dist;
                }
            }

            Attack(nearestEnemy);

            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (shooting && now > stopShootingAnimation)
            {
                shooting = false;
                Shot = null;
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
                shooting = true;
                stopShootingAnimation = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + 250;
                Shot = new Point[]
                    {
                        Position.ToPoint() + ShootingPoint,
                        entity.Position.ToPoint() + entity.Center
                    };
            }
            else
            {
                long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (now >= NextAttack)
                {
                    entity.TakeHit(Damage);
                    NextAttack = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + (long)(1000 / AttackSpeed);
                    shooting = true;
                    stopShootingAnimation = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + 250;
                    Shot = new Point[]
                    {
                        Position.ToPoint() + ShootingPoint,
                        entity.Position.ToPoint() + entity.Center
                    };
                }
            }
        }
    }
}
