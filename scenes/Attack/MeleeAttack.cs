using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Attack;

public class MeleeAttack : Node, IAttack
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

	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer attackTimer;

	public void Attack(GenericLivingObject target, Vector2 currentPosition)
	{
		if (isTargetInRange(target.Position, currentPosition) && isTimeToAttack())
		{
			target.Hp.TakeHit(Damage);
		}
	}

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attackTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer((long)(1000 / AttackSpeed));
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private bool isTimeToAttack()
	{
		return attackTimer.HasPassed();
	}

	private bool isTargetInRange(Vector2 targetPosition, Vector2 currentPosition)
	{
		float distance = (targetPosition - currentPosition).Length();
		return (distance >= 0 && distance <= Range) || (distance < 0 && distance >= -Range);
	}
}
