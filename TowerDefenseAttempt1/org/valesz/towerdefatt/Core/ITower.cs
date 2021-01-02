using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Returns the point on the texture from where the shot is fired.
        /// </summary>
        Point ShootingPoint { get; }

        /// <summary>
        /// Start and end point of a shot. May return null if the tower is not shooting.
        /// </summary>
        Point[] Shot { get; }

        /// <summary>
        /// Price of this tower in the shop.
        /// </summary>
        uint Price { get; }

        /// <summary>
        /// Flag set to true if the players selects this tower either in shop or on the map.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// How much does it cost to upgrade this tower (may change with each upgrade).
        /// </summary>
        uint UpgradePrice { get; }

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

        /// <summary>
        /// Initializes new instnace of this type with given coordinates.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns></returns>
        ITower Clone(float x, float y);

        /// <summary>
        /// Upgrades stats of this tower by given coefficients.
        /// </summary>
        void Upgrade();
    }
}
