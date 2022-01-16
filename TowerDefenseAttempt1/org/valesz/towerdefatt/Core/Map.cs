using System.Collections.Generic;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.GameShop;
using Microsoft.Xna.Framework;

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
        /// Obstacles placed on the map.
        /// </summary>
        public List<IObstacle> Obstacles
        {
            get;
            private set;
        }


        public Shop Shop { get; private set; }

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
        /// Object used to gather statistic data during the game.
        /// </summary>
        public IStatsCollector StatsCollector { get; set; }

        /// <summary>
        /// Item on a map selected by player. Initialized to null.
        /// </summary>
        public ISelectable SelectedItem { get; private set; }

        /// <summary>
        /// Spawner to be used on this map.
        /// </summary>
        private AbstractSpawner spawner;

        /// <summary>
        /// Resets the current game's state and initializes new map with given base.
        /// </summary>
        /// <param name="playerBase">Player's base.</param>
        /// <param name="spawner">Spawner to be used on this map.</param>
        public void StartNewMap(IBase playerBase, AbstractSpawner spawner, List<ITower> availableTowers, List<IObstacle> availableObstacles, uint startingMoney)
        {
            Base = playerBase;
            Enemies = new List<IEnemy>();
            Towers = new List<ITower>();
            Obstacles = new List<IObstacle>();
            Shop = new Shop(availableTowers, availableObstacles);
            Score = 0;
            Money = startingMoney;
            DeselectMapItems();
            this.spawner = spawner;
            SpawnEnemies();
        }

        /// <summary>
        /// Returns true if the current game was lost.
        /// </summary>
        /// <returns></returns>
        public bool GameLost()
        {
            return Base != null && Base.Hp == 0;
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

            textureNames.AddRange(Shop.AllItemTextures());

            return textureNames;
        }

        /// <summary>
        /// Buy and place the item currently selected in the shop.
        /// </summary>
        /// <param name="clickX"></param>
        /// <param name="clickY"></param>
        public void BuySelectedItem(float clickX, float clickY)
        {
            if (Shop.SelectedShopItem == null)
            {
                return;
            } else
            {
                BuyTower(Shop.CloneSelectedItem(clickX, clickY));
            }
        }

        /// <summary>
        /// Buys given tower and places it on the map. Buying a tower resets the SelectedShopTower.
        /// If the player does not have enough money, no tower will be placed and SelectedShopTower
        /// will still be reset.
        /// </summary>
        /// <param name="shopItem">Instance of a tower to be placed</param>
        public void BuyTower(IShopItem shopItem)
        {
            if (Money >= shopItem.Price)
            {
                // todo: some generic way to handle this
                if (shopItem is ITower)
                {
                    Towers.Add((ITower)shopItem);
                    Money -= shopItem.Price;
                    CollectStatistics(StatsCollectionEvent.TOWER_BOUGHT);
                } else if (shopItem is IObstacle)
                {
                    Obstacles.Add((IObstacle)shopItem);
                    Money -= shopItem.Price;
                    CollectStatistics(StatsCollectionEvent.OBSTACLE_BOUGHT);
                }
            }

            Shop.DeselectAll();
        }

        /// <summary>
        /// Deselects selected item on map (if any).
        /// </summary>
        public void DeselectMapItems()
        {
            SelectedItem = null;
            foreach(ITower t in Towers)
            {
                t.Selected = false;
            }

            foreach(IObstacle o in Obstacles)
            {
                o.Selected = false;
            }
        }

        /// <summary>
        /// Attempts to upgrade IUpgradable entity that is currently selected on the map. 
        /// If no such entity is selected, nothing happens.
        /// </summary>
        public void UpgradeSelected()
        {
            if (SelectedItem == null || !(SelectedItem is IUpgradable))
            {
                return;
            }

            IUpgradable selectedUpgradable = (IUpgradable)SelectedItem;
            if (Money >= selectedUpgradable.UpgradePrice)
            {
                Money -= selectedUpgradable.UpgradePrice;
                selectedUpgradable.Upgrade();
                if (selectedUpgradable is ITower)
                {
                    CollectStatistics(StatsCollectionEvent.TOWER_UPGRADE);
                }
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
                CollectStatistics(StatsCollectionEvent.WAVE_SPAWNED);
            }
        }

        /// <summary>
        /// Deselects all previously selected items (on the map) and then selects one on the map. If the item on the given coordinates is already
        /// selected, this method just deselects it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SelectItemOnTheMap(float x, float y)
        {
            foreach (ITower mapTower in Towers)
            {
                if (mapTower.CheckColision(x, y))
                {
                    HandleItemSelection(mapTower);
                    return;
                }
            }

            foreach (IObstacle obstacle in Obstacles)
            {
                if (obstacle.CheckColision(x,y))
                {
                    HandleItemSelection(obstacle);
                    return;
                }
            }

            // no tower lise on given coordinates => deselect
            DeselectMapItems();
        }

        /// <summary>
        /// Returns any obstacle coliding with the given position.
        /// </summary>
        /// <param name="position">Position to check.</param>
        /// <returns>Obstacle if collision is found or null if nothing is found.</returns>
        public IObstacle CheckObstacle(Vector2 position)
        {
            foreach(IObstacle obstacle in Obstacles)
            {
                if (obstacle.CheckColision(position.X, position.Y))
                {
                    return obstacle;
                }
            }

            return null;
        }

        /// <summary>
        /// Handles selection of an item on the map.
        /// </summary>
        /// <param name="item">Selected item</param>
        private void HandleItemSelection(ISelectable item)
        {
            // item already selected => deselect
            if (item.Selected)
            {
                DeselectMapItems();
            }
            else
            {
                // deselect others and select the current one
                DeselectMapItems();
                SelectedItem = item;
                item.Selected = true;
            }
        }

        /// <summary>
        /// Attempts to call the stat collector with given event. If no collector is set, nothing happens.
        /// </summary>
        /// <param name="eventType">Type of the event to be passed to collector.</param>
        private void CollectStatistics(StatsCollectionEvent eventType) {
            StatsCollector?.CollectStatsOnEvent(eventType, this);
        }
    }
}
