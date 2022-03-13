using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Base interface for all objects that are computer-controlled and need to be updated in the game loop.
    /// </summary>
    public interface IHasAI
    {
        /// <summary>
        /// Called once per iteration of game loop. Think and update state.
        /// </summary>
        /// <param name="gameMap">Instance of current game map used to access other objects.</param>
        void UpdateState(Map gameMap);
    }
}
