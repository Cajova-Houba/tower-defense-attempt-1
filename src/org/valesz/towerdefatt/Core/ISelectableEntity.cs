using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core
{
    /// <summary>
    /// Interfaces for selectable entities.
    /// </summary>
    public interface ISelectableEntity
    {
        /// <summary>
        /// Selection details of this entity.
        /// </summary>
        SelectableBehavior Selection { get; }

        /// <summary>
        /// Deselect this entity.
        /// </summary>
        void Deselect();
    }
}
