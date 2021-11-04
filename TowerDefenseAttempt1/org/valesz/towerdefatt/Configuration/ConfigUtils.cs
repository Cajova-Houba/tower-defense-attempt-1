using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Base;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Obstacle;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Spawn;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Tower;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Configuration
{
    /// <summary>
    /// Utility class for methods that will probably get rfactored to some other place.
    /// </summary>
    public static class ConfigUtils
    {
        public static void StartNewMapWithDefaultConfiguration(Map gameMap)
        {
            List<ITower> availableTowers = new List<ITower>
            {
                new DefaultTower(0, 0)
            };
            List<IObstacle> availableObstacles = new List<IObstacle>
            {
                new DefaultObstacle(0,0)
            };
            uint startingMoney = new DefaultTower(0, 0).Price;

            float baseX = 480, baseY = 30, topY = 400;
            int xSpan = 100;
            int steps = 100;
            float yStep = (topY - baseY) / steps;
            List<ISpawnPoint> spawnPoints = new List<ISpawnPoint>();
            for(int i = 0; i < steps; i++)
            {
                uint xDiff = (uint)new Random().Next(0, xSpan);
                spawnPoints.Add(new DefaultSpawnPoint(baseX + xDiff, baseY + yStep*i));
            }

            gameMap.StartNewMap(new DefaultBase(50, 50), new WaveSpawner(spawnPoints), availableTowers, availableObstacles, startingMoney);
        }
    }
}
