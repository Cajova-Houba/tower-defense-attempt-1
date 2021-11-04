using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop
{
    /// <summary>
    /// Interface for entities that can be bought in shop.
    /// </summary>
    public interface IShopItem : ISelectable, IHasTexture
    {
        /// <summary>
        /// Initializes new instnace of this type with given coordinates.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns></returns>
        IShopItem Clone(float x, float y);

        /// <summary>
        /// Price of this entity in the shop.
        /// </summary>
        uint Price { get; }
    }
}
