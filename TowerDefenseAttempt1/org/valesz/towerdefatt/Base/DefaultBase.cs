using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Base
{
    public class DefaultBase : IBase
    {
        public string TextureName => "assets/bases/default";
        public IEnumerable<string> AllTextures => new string[] { TextureName };

        public Point Center => new Point(32, 32);

        public uint Hp
        {
            get;
            private set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public DefaultBase(float x, float y)
        {
            Hp = 500;
            Position = new Vector2(x, y);
        }

        public void TakeHit(uint damage)
        {
            if (Hp < damage) {
                Hp = 0;
            } else
            {
                Hp -= damage;
            }
        }
    }
}
