using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Enemy;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Spawn
{
    /// <summary>
    /// Spawns enemies in waves. 1 wave is spawned on Spawn() method call. Number of enemies per wave is increased with each wave.
    /// </summary>
    public class WaveSpawner : AbstractSpawner
    {
        /// <summary>
        /// How many enemies to spawn in the current wave.
        /// </summary>
        private float enemiesPerWave;

        public WaveSpawner(List<ISpawnPoint> spawnPoints) : base(spawnPoints)
        {
            enemiesPerWave = 1f;
        }

        public override IEnumerable<IEnemy> Spawn()
        {
            List<IEnemy> enemies = new List<IEnemy>();
            for (uint i = 0; i < enemiesPerWave; i++)
            {
                ISpawnPoint spawnPoint = SelectRandomSpawnPoint();
                if (spawnPoint != null)
                {
                    enemies.Add(new DefaultEnemy(spawnPoint.Position));
                }
            }

            enemiesPerWave*=1.5f;

            // float overflow
            if (enemiesPerWave < 0)
            {
                enemiesPerWave = (float)uint.MaxValue;
            }
            return enemies;
        }
    }
}
