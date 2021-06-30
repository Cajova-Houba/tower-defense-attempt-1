using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Wraper holding values that represent or are part of the game's current state.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Min time interval between tower upgrades (in ms). Applied only when the update key is pressed (without release).
        /// </summary>
        const long TOWER_UIPGRADE_TIMER_INTERVAL = 1000;
        const long NO_UPGRADE = -1;


        /// <summary>
        /// When was the last tower upgraded (in ms).
        /// </summary>
        long lastTowerUgrade = NO_UPGRADE;

        bool paused = false;

        /// <summary>
        /// Pauses the current game.
        /// </summary>
        public void Pause()
        {
            paused = true;
        }

        /// <summary>
        /// Unpauses current game.
        /// </summary>
        public void UnPause()
        {
            paused = false;
        }

        public bool IsPaused()
        {
            return paused;
        }

        /// <summary>
        /// Checks the game state values and decides whether the tower upgrade is possible. In order for it to be possible,
        /// the limit between updates must be exceeded.
        /// </summary>
        /// <returns>True if the tower upgrade is possible.</returns>
        public bool CanUpdateTower()
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return lastTowerUgrade < 0 || lastTowerUgrade + TOWER_UIPGRADE_TIMER_INTERVAL <= now;
        }

        /// <summary>
        /// Stores the current time value to the tower upgrade timer-
        /// </summary>
        public void UpdateTowerUpgradeTimer()
        {
            lastTowerUgrade = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void ResetTowerUpgradeTimer()
        {
            lastTowerUgrade = NO_UPGRADE;
        }
    }
}
