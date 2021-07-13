using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Statistics
{
    /// <summary>
    /// Possible types of events on which stats collection may be triggered.
    /// </summary>
    public enum StatsCollectionEvent
    {
        /// <summary>
        /// Event marking the game start.
        /// </summary>
        GAME_START,

        /// <summary>
        /// Fired after the tower is bought.
        /// </summary>
        TOWER_BOUGHT,

        /// <summary>
        /// Fired after a tower is upgraded.
        /// </summary>
        TOWER_UPGRADE,

        /// <summary>
        /// Fired after an enemy wave is spawned.
        /// </summary>
        WAVE_SPAWNED
    }
}
