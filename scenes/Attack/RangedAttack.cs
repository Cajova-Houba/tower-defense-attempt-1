using Godot;

namespace TowerDefenseAttempt1.scenes.Attack
{
	public class RangedAttack : GenericAttack
	{
		/// <summary>
		/// Projectile to spawn.
		/// </summary>
		[Export]
		public PackedScene ProjectileScene;

		public override bool Attack(GenericLivingObject target, Vector2 currentPosition)
		{
			if (IsTargetInRange(target.Position, currentPosition) && IsTimeToAttack())
			{
				Projectile p = (Projectile)ProjectileScene.Instance();
				p.Init(currentPosition, target.Position, Damage);
				AddChild(p);
				return true;
			}

			return false;
		}
	}
}
