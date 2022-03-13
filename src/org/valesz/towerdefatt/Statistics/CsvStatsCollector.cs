using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Statistics
{
    public class CsvStatsCollector : IStatsCollector
    {
        List<StatisticsRecord> statistics;

        /// <summary>
        /// Timestamp when this object was created. Used to calculate timestamps of further records.
        /// </summary>
        readonly long startMs;

        bool finalized;

        public CsvStatsCollector()
        {
            startMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            finalized = false;

            statistics = new List<StatisticsRecord>();
            statistics.Add(new StatisticsRecord() { Timestamp = 0, StatsCollectionEvent = StatsCollectionEvent.GAME_START, TotalDps = 0, TowerCount = 0, EnemyCount = 0 });
        }

        public void CollectStatsOnEvent(StatsCollectionEvent eventType, Map map)
        {
            long timestamp = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - startMs;
            uint towerCount = (uint)map.Towers.Count;
            uint enemyCount = (uint)map.Enemies.Count;
            float totalDps = 0;

            map.Towers.ForEach(delegate(ITower tower)
            {
                totalDps += tower.AttackSpeed * tower.Damage;
            });

            statistics.Add(new StatisticsRecord() { Timestamp = timestamp, StatsCollectionEvent = eventType, TotalDps = totalDps, TowerCount = towerCount, EnemyCount = enemyCount });
        }

        public void FinalizeStatistics()
        {
            if (finalized)
            {
                return;
            }

            string fileName = ("game-data-" + DateTime.Now.ToString("s") + ".csv").Replace(':','-');
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteLine(StatisticsRecord.GetCsvHeader());
                foreach ( StatisticsRecord record in statistics)
                {
                    outputFile.WriteLine(record.ToCsvString());
                }
            }

            finalized = true;
        }
    }
}
