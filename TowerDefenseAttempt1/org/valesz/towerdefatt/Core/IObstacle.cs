using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Interface for all objects representing obstacles. Player can buy obstacles from shop and place them on the map to prevent
    /// the enemies from reaching his base.
    /// 
    /// By default, obstacles cannot attack on their own.
    /// 
    /// It is assumed that obstacles are immovable - once the player palces them, they cannot move and are removed only when destroyed 
    /// by enemy.
    /// </summary>
    public interface IObstacle : IHasHp, IHasPosition, IHasTexture, IShopItem
    {
    }
}
