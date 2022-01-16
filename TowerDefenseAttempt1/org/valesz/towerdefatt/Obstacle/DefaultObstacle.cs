using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Obstacle
{
    public class DefaultObstacle : AbstractLivingObject, IObstacle
    {
        private const float UPGRADE_PRICE_FACTOR = 2f;
        private const float UPGRADE_HP_FACTOR = 1.5f;

        public override string TextureName => Selected ? Textures.DEFAULT_OBSTACLE_SELECTED : Textures.DEFAULT_OBSTACLE;

        public override IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_OBSTACLE, Textures.DEFAULT_OBSTACLE_SELECTED };

        public uint Price => 100;

        public bool Selected { get; set; }

        public uint UpgradePrice { get; private set; }


        public DefaultObstacle(float x, float y) : base(125, x, y)
        {
            Selected = false;
            UpgradePrice = 200;
        }

        public IShopItem Clone(float x, float y)
        {
            return new DefaultObstacle(x, y);
        }

        public void Upgrade()
        {
            UpgradePrice = (uint)(UpgradePrice * UPGRADE_PRICE_FACTOR);
            MaxHp = (uint)(MaxHp * UPGRADE_HP_FACTOR);
            Hp = MaxHp;
        }
    }
}
