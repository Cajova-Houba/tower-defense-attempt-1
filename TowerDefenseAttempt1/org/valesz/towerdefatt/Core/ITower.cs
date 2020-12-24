using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    public interface ITower : IHasTexture, IHasAI, ICanAttack
    {
        /// <summary>
        /// Returns the attack range of this tower.
        /// </summary>
        float AttackRange { get; }
    }
}
