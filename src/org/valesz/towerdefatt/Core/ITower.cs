using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    public interface ITower : IHasTexture, IHasAI, ICanAttack, IShopItem, IUpgradable
    {
        /// <summary>
        /// Returns the attack range of this tower.
        /// </summary>
        float AttackRange { get; }

        /// <summary>
        /// Returns the point on the texture from where the shot is fired.
        /// </summary>
        Point ShootingPoint { get; }

        /// <summary>
        /// Start and end point of a shot. May return null if the tower is not shooting.
        /// </summary>
        Point[] Shot { get; }

        /// <summary>
        /// How much the price changes with each upgrade.
        /// </summary>
        float UpgradePriceFactor { get; }

        /// <summary>
        /// How much the damage changes with each upgrade.
        /// </summary>
        float DamageUpgradeFactor { get; }

        /// <summary>
        /// How much the attack speed changes with each upgrade.
        /// </summary>
        float AttackSpeedUpgradeFactor { get; }
    }
}
