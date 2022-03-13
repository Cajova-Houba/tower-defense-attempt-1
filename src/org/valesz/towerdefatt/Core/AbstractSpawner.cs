using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Base class for Spawners - not to be confused with spawn points. The spawner contains more complex logic
    /// for spawning enemies using multiple spawn points and/or spawn conditions.
    /// 
    /// The class contains the Spawn() method declaration and some definitiones which are likely to be used by 
    /// implementing classes.
    /// </summary>
    public abstract class AbstractSpawner
    {
        /// <summary>
        /// Spawn points controlled by this spawner.
        /// </summary>
        protected List<ISpawnPoint> spawnPoints;

        /// <summary>
        /// Random variable.
        /// </summary>
        protected Random r;

        public AbstractSpawner(List<ISpawnPoint> spawnPoints)
        {
            this.spawnPoints = spawnPoints;
            r = new Random();
        }

        /// <summary>
        /// Spawn enemies.
        /// </summary>
        /// <returns>Collection of spawned enemies.</returns>
        public abstract IEnumerable<IEnemy> Spawn();

        /// <summary>
        /// Selects random spawn point. If no spawn points are available, null is returned.
        /// </summary>
        /// <returns>Spawn point or null.</returns>
        protected ISpawnPoint SelectRandomSpawnPoint()
        {
            if (spawnPoints == null || spawnPoints.Count == 0)
            {
                return null;
            }

            return spawnPoints[r.Next(spawnPoints.Count)];
        }
    }
}
