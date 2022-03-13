using System.Drawing;
using Godot;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core
{
    public interface IHasPosition
    {
        /// <summary>
        /// Top left corner of this object.
        /// </summary>
        Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the center point of this object.
        /// </summary>
        Point Center { get; }
    }
}
