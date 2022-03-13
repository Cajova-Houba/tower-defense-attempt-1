using Godot;

namespace TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Attack
{
    public interface IAttack
    {
        void Attack(GenericLivingObject target, Vector2 currentPosition);
    }
}
