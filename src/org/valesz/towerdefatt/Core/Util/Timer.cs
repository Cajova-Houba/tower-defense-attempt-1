using Godot;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util
{
    /// <summary>
    /// A very simple timer which will take a period as input and has method for
    /// checking whether given time has already passed. 
    /// 
    /// This time does not use any thread nor has anything to do with synchronization.
    /// </summary>
    public class Timer
    {
        public const long NOT_PASSED = -1;

        /// <summary>
        /// Timestamp in millis of the last pass.
        /// </summary>
        private long lastPass;

        /// <summary>
        /// Period in millis.
        /// </summary>
        private long period;

        public Timer(long period)
        {
            lastPass = NOT_PASSED;
            this.period = period;
        }

        /// <summary>
        /// Resets the internal timer period to a new value.
        /// </summary>
        /// <param name="newPeriod">New value in millis.</param>
        public void ResetPeriod(long newPeriod)
        {
            this.period = newPeriod;
        }


        /// <summary>
        /// Checks whether the configured period has already passed.
        /// </summary>
        /// <returns>True if the period has already passed.</returns>
        public bool HasPassed()
        {
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (lastPass == NOT_PASSED)
            {
                lastPass = now;
                return true;
            } else
            {
                bool hasPassed = (now - lastPass) >= period;
                if (hasPassed)
                {
                    lastPass = now;
                }
                return hasPassed;
            }
        }

    }
}
