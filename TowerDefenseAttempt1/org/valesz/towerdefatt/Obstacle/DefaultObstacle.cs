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

        public override string TextureName => Selected ? Textures.DEFAULT_OBSTACLE_SELECTED : Textures.DEFAULT_OBSTACLE;

        public override IEnumerable<string> AllTextures => new string[] { Textures.DEFAULT_OBSTACLE, Textures.DEFAULT_OBSTACLE_SELECTED };

        public uint Price => 100;

        public bool Selected { get; set; }

        public DefaultObstacle(float x, float y) : base(50, x, y)
        {
            Selected = false;
        }

        public IShopItem Clone(float x, float y)
        {
            return new DefaultObstacle(x, y);
        }
    }
}
