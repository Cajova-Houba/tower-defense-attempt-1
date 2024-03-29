﻿using System;
using System.Collections.Generic;
using System.Drawing;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;
using Timer = TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Tower
{
    public class DefaultTower : AbstractVisibleObject, ITower
    {

        public override string TextureName
        {
            get
            {
                if (shooting)
                {
                    return Selected ? Textures.DEFAULT_TOWER_SHOT_SELECTED : Textures.DEFAULT_TOWER_SHOT;
                } else
                {
                    return Selected ? Textures.DEFAULT_TOWER_SELECTED : Textures.DEFAULT_TOWER;
                }
            }
        }

        public override IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_TOWER_SHOT_SELECTED, Textures.DEFAULT_TOWER, Textures.DEFAULT_TOWER_SELECTED, Textures.DEFAULT_TOWER_SHOT };

        public uint Damage {get; private set;}

        public float AttackSpeed { get; private set; }

        public float AttackRange => 100;

        public Point[] Shot { get; private set; }
        public Point ShootingPoint => new Point(34, 9);

        public uint Price => 50;

        public bool Selected { get; set; }
        public uint UpgradePrice { get; private set; }

        public float UpgradePriceFactor => 2f;

        public float DamageUpgradeFactor => 1.5f;

        public float AttackSpeedUpgradeFactor => 1.5f;

        private Timer AttackTimer { get; set; }

        private bool shooting = false;
        private long stopShootingAnimation;

        public DefaultTower(float x, float y) : base(x,y)
        {
            Damage = 10;
            UpgradePrice = 100;
            AttackSpeed = 2;
            stopShootingAnimation = -1;
            Shot = null;
            Selected = false;
            AttackTimer = new Timer((long)(1000 / AttackSpeed));
        }

        public IShopItem Clone(float x, float y)
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

        public void Upgrade()
        {
            UpgradePrice = (uint)( UpgradePrice * UpgradePriceFactor);
            Damage = (uint)(Damage * DamageUpgradeFactor);
            AttackSpeed = AttackSpeed * AttackSpeedUpgradeFactor;
            AttackTimer.ResetPeriod((long)(1000 / AttackSpeed));
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
                shooting = true;
                stopShootingAnimation = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + 250;
                //Shot = new Point[]
                //    {
                //        Position + ShootingPoint,
                //        entity.Position.ToPoint() + entity.Center
                //    };
            }
        }
    }
}
