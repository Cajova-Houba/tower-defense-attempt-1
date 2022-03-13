using Godot;
using System.Drawing;

namespace TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Abstract
{
    /// <summary>
    /// Abstract reprezentation of the most basic game object.
    /// </summary>
    public abstract class AbstractGameObject : Area2D, IHasPosition
    {
        public Point Center => new Point(32, 32);

        public AbstractGameObject(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}
