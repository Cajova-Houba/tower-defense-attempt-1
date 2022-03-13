using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Base interface for objects with hit points.
    /// </summary>
    public interface IHasHp : IHasPosition
    {

        /// <summary>
        /// Returns HP of this enemy.
        /// </summary>
        /// <returns>HP</returns>
        uint Hp
        {
            get;
        }

        /// <summary>
        /// Returns true if this entity is dead (has 0 HP).
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        /// Returns the max HP of this entity.
        /// </summary>
        uint MaxHp { get; }

        /// <summary>
        /// Take hit to HP.
        /// </summary>
        /// <param name="damage">Damage. Max of current HP can be applied.</param>
        void TakeHit(uint damage);
    }
}
