using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Interface for entities that can be selected (either in the map or 
    /// </summary>
    [Obsolete]
    public interface ISelectable
    {
        /// <summary>
        /// Flag set to true if the players selects this tower either in shop or on the map.
        /// </summary>
        bool Selected { get; set; }
    }
}
