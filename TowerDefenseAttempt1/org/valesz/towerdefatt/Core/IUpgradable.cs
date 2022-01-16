using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Interface for upgradable objects. Player can buy upgrades from shop.
    /// </summary>
    public interface IUpgradable
    {
        /// <summary>
        /// How much does it cost to upgrade this entity to the next level.
        /// It is possible for this value to change as entity is upgraded.
        /// </summary>
        public uint UpgradePrice
        {
            get;
        }

        /// <summary>
        /// Upgrade this entity.
        /// </summary>
        public void Upgrade();
    }
}
