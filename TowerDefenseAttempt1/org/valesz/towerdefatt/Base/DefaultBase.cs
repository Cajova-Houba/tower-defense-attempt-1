﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Base
{
    public class DefaultBase : AbstractLivingObject, IBase
    {
        private const uint MAX_HP = 500;

        public override string TextureName => "assets/bases/default";
        public override IEnumerable<string> AllTextures => new string[] { TextureName };

        public DefaultBase(float x, float y) : base(MAX_HP, x, y)
        {
            Position = new Vector2(x, y);
        }
    }
}
