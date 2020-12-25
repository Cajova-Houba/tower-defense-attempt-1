using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Base interface for all enemy objects.
    /// </summary>
    public interface IEnemy : IHasTexture, IHasHp, IHasAI, ICanAttack
    {
        /// <summary>
        /// Movement speed of this enemy.
        /// </summary>
        public float Speed { get; }

        /// <summary>
        /// Score player gains for killing this enemy.
        /// </summary>
        public uint Value { get; }
    }
}
