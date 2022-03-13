using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics
{
    /// <summary>
    /// Base interface for statistics collectors.
    /// </summary>
    public interface IStatsCollector
    {
        /// <summary>
        /// Called each time an event occurs. It's up to the implementation to decide whether to skip the event or handle it somehow.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="map">Instance of map used to gather stat data.</param>
        void CollectStatsOnEvent(StatsCollectionEvent eventType, Map map);

        /// <summary>
        /// Finalize the stat collection. It is expected (though not guarantied) that no more CollectsStatsOnEvent() calls will be made.
        /// 
        /// Any further calls on this method should be ignored.
        /// </summary>
        void FinalizeStatistics();
    }
}
