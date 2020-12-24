using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Tower
{
    public class DefaultTower : ITower
    {
        public string TextureName => "assets/towers/default";

        public IEnumerable<string> AllTextures => new string[] { TextureName };

        public Vector2 Position { get; set; }

        public uint Damage => 10;

        public float AttackSpeed => 2;

        public DefaultTower(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        public void UpdateState(Map gameMap)
        {
            // pick the nearest enemy and shoot
            if (gameMap.Enemies.Count == 0)
            {
                return;
            }


            long minDist = long.MaxValue;

        }
    }
}
