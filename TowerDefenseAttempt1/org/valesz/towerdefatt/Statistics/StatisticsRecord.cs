using System;
using System.Collections.Generic;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Statistics
{
    class StatisticsRecord
    {
        public static string GetCsvHeader()
        {
            return "Timestamp;StatsCollectionEvent;TowerCount;TotalDps;EnemyCount";
        }

        /// <summary>
        /// MS since the start of the game.
        /// </summary>
        public long Timestamp { get; set; }

        public StatsCollectionEvent StatsCollectionEvent { get; set;}

        public uint TowerCount { get; set; }

        public float TotalDps { get; set; }

        public uint EnemyCount { get; set; }

        public string ToCsvString()
        {
            return $"{Timestamp};{StatsCollectionEvent};{TowerCount};{TotalDps};{EnemyCount}";
        }
    }
}
