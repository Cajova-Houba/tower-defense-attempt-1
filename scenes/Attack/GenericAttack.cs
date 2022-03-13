using Godot;

namespace TowerDefenseAttempt1.scenes.Attack
{
	/// <summary>
	/// Base class for all attacks.
	/// </summary>
	public class GenericAttack : Node
	{
		/// <summary>
		/// Range of this melee attack. Can attack targets within.
		/// </summary>
		[Export]
		public uint Range = 64;

		/// <summary>
		/// Damage caused by this attack.
		/// </summary>
		[Export]
		public uint Damage = 100;

		/// <summary>
		/// Attacks per second.
		/// </summary>
		[Export]
		public float AttackSpeed = 1;

		protected org.valesz.towerdefatt.Core.Util.Timer attackTimer;

		public override void _Ready()
		{
			attackTimer = new org.valesz.towerdefatt.Core.Util.Timer((long)(1000 / AttackSpeed));
		}

		public virtual void Attack(GenericLivingObject target, Vector2 currentPosition)
		{
			if (IsTargetInRange(target.Position, currentPosition) && IsTimeToAttack())
			{
				target.Hp.TakeHit(Damage);
			}
		}

		/// <summary>
		/// Checks the internal timer to see whether it's possible to perform another attack.
		/// </summary>
		/// <returns>True if it's possible to attack.</returns>
		protected bool IsTimeToAttack()
		{
			return attackTimer.HasPassed();
		}

		/// <summary>
		/// Checks whether the target is in range of this attack.
		/// </summary>
		/// <param name="targetPosition">Target's position.</param>
		/// <param name="currentPosition">Attacker's position.</param>
		/// <returns>True if the target is in range of this attack.</returns>
		protected bool IsTargetInRange(Vector2 targetPosition, Vector2 currentPosition)
		{
			float distance = (targetPosition - currentPosition).Length();
			return (distance >= 0 && distance <= Range) || (distance < 0 && distance >= -Range);
		}
	}
}
