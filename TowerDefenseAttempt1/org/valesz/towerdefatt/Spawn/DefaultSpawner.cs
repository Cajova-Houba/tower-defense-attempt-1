using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Enemy;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Spawn
{
    /// <summary>
    /// The simplest spawner. Picks random spawn point and spawns one enemy. 
    /// </summary>
    public class DefaultSpawner : AbstractSpawner
    {

        public DefaultSpawner(List<ISpawnPoint> spawnPoints) : base(spawnPoints)
        {
        }

        public override IEnumerable<IEnemy> Spawn()
        {
            List<IEnemy> enemies = new List<IEnemy>();
            ISpawnPoint spawnPoint = SelectRandomSpawnPoint();
            if (spawnPoint != null)
            {
                enemies.Add(new DefaultEnemy(spawnPoint.Position));
            }

            return enemies;
        }
    }
}
