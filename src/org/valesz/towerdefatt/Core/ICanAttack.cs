using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Interface for all entities capable of attacking.
    /// </summary>
    public interface ICanAttack
    {
        /// <summary>
        /// Damage this enemy does with one attack.
        /// </summary>
        uint Damage { get; }

        /// <summary>
        /// How many attacks per second this enemy can do.
        /// </summary>
        float AttackSpeed { get; }
    }
}
