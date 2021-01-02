using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Enemy;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Tower;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Holds info about one map - enemies, spawners, base, towers, ... Basically an instance of one game.
    /// </summary>
    public class Map
    {
        public IBase Base {
            get;
            private set;
        }

        public List<IEnemy> Enemies
        {
            get;
            private set;
        }

        /// <summary>
        /// Towers placed on the map.
        /// </summary>
        public List<ITower> Towers
        {
            get;
            private set;
        }

        /// <summary>
        /// Towers available through all the game.
        /// </summary>
        public List<ITower> AvailableTowers
        {
            get;
            private set;
        }

        /// <summary>
        /// Tower selected in shop by player. Initialized to null.
        /// </summary>

        public ITower SelectedShopTower { get; private set; }

        /// <summary>
        /// Score gained by killing enemies.
        /// </summary>
        public uint Score { get; private set; }

        /// <summary>
        /// Enemy kill counter.
        /// </summary>
        public uint EnemiesKilled { get; private set; }

        /// <summary>
        /// How much money player has. Money can be gained by killing enemies.
        /// </summary>
        public uint Money { get; private set; }

        /// <summary>
        /// Spawner to be used on this map.
        /// </summary>
        private AbstractSpawner spawner;

        /// <summary>
        /// Resets the current game's state and initializes new map with given base.
        /// </summary>
        /// <param name="playerBase">Player's base.</param>
        /// <param name="spawner">Spawner to be used on this map.</param>
        public void StartNewMap(IBase playerBase, AbstractSpawner spawner)
        {
            Base = playerBase;
            Enemies = new List<IEnemy>();
            Towers = new List<ITower>();
            AvailableTowers = new List<ITower>();
            AvailableTowers.Add(new DefaultTower(0, 0));
            Score = 0;
            Money = new DefaultTower(0,0).Price;
            SelectedShopTower = null;
            this.spawner = spawner;

            SpawnEnemies();
        }

        /// <summary>
        /// Returns a collections of names of all textures used by all objects during the game.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllTextures()
        {
            List<string> textureNames = new List<string>();
            textureNames.AddRange(Base.AllTextures);

            foreach (IEnemy enemy in Enemies)
            {
                textureNames.AddRange(enemy.AllTextures);
            }

            foreach (ITower tower in AvailableTowers)
            {
                textureNames.AddRange(tower.AllTextures);
            }

            return textureNames;
        }

        /// <summary>
        /// Buys given tower and places it on the map. Buying a tower resets the SelectedShopTower.
        /// If the player does not have enough money, no tower will be placed and SelectedShopTower
        /// will still be reset.
        /// </summary>
        /// <param name="tower">Instance of a tower to be placed</param>
        public void BuyTower(ITower tower)
        {
            if (Money >= tower.Price)
            {
                Towers.Add(tower);
                Money -= tower.Price;
            }

            DeselectShopTower();
        }

        /// <summary>
        /// Deselects selected tower in shop (if any).
        /// </summary>
        public void DeselectShopTower()
        {
            SelectedShopTower = null;
            foreach (ITower t in AvailableTowers)
            {
                t.Selected = false;
            }
        }

        /// <summary>
        /// Adds value to the score and also to the money.
        /// </summary>
        /// <param name="value">Value to be added to the current score.</param>
        public void AddScore(uint value)
        {
            Score += value;
            Money += value;
        }

        /// <summary>
        /// Increments the the enemy kill counter by 1.
        /// </summary>
        public void IncrementKillCounter()
        {
            EnemiesKilled += 1;
        }

        /// <summary>
        /// Spawns enemies on this map using provided Spawner object. 
        /// Enemies are spawned only when there are no enemies left on the map.
        /// </summary>
        public void SpawnEnemies()
        {
            if (Enemies.Count == 0)
            {
                Enemies.AddRange(spawner.Spawn());
            }
        }

        /// <summary>
        /// Deselects all previously selected towers in shop and then selects available tower 
        /// from shop on the given coordinates. If the tower on the given coordinates is already
        /// selected, this method just deselects it.
        /// </summary>
        public void SelectTowerFromShop(float x, float y)
        {
            foreach(ITower availableTower in AvailableTowers)
            {
                if ((availableTower.Position.X <= x && availableTower.Position.X + 64 >= x) &&
                    (availableTower.Position.Y <= y && availableTower.Position.Y + 64 >= y)  
                    )
                {
                    // tower already selected => deselect
                    if (availableTower.Selected)
                    {
                        DeselectShopTower();
                    } else
                    {
                        SelectedShopTower = availableTower;
                        availableTower.Selected = true;
                    }
                    return;
                }
            }

            // no tower lise on given coordinates => deselect
            DeselectShopTower();
        }
    }
}
