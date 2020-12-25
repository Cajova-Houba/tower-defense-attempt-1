using System;
using System.Collections.Generic;
using System.Text;
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
        /// Score gained by killing enemies.
        /// </summary>
        public uint Score { get; private set; }

        /// <summary>
        /// Resets the current game's state and initializes new map with given base.
        /// </summary>
        /// <param name="playerBase">Player's base</param>
        public void StartNewMap(IBase playerBase)
        {
            Base = playerBase;
            Enemies = new List<IEnemy>();
            Towers = new List<ITower>();
            AvailableTowers = new List<ITower>();
            AvailableTowers.Add(new DefaultTower(0, 0));
            Score = 0;

            SpawnEnemy();
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
        /// Place tower to the map.
        /// </summary>
        /// <param name="tower">Tower to be placed</param>
        public void PlaceTower(DefaultTower tower)
        {
            if (Towers.Count == 0)
            {
                Towers.Add(tower);
            }
        }

        /// <summary>
        /// Adds value to the score.
        /// </summary>
        /// <param name="value">Value to be added to the current score.</param>
        public void AddScore(uint value)
        {
            Score += value;
        }

        /// <summary>
        /// Spawns enemy at default position.
        /// </summary>
        public void SpawnEnemy()
        {
            Enemies.Add(new DefaultEnemy(300, 200));
        }
    }
}
