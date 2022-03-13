using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Attack;

/// <summary>
/// </summary>
public class Enemy : GenericLivingObject, IHasHpBehavior
{
	private const string DEFAULT_ANIMATION = "default";
	private const string HURT_ANIMATION = "hurt";
	private const string ATTACKS_NODE = "Attacks";

	/// <summary>
	/// Destination is reached when it's closer to the current position than this treshold.
	/// 
	/// For the convenience, this constant holds squared treshold.
	/// </summary>
	[Export]
	public float DestinationReachedTreshold = 64f;

	/// <summary>
	/// Pixels per second.
	/// </summary>
	[Export]
	public int MovementSpeed = 75;

	/// <summary>
	/// Target this enemy will seek to destroy.
	/// </summary>
	public GenericLivingObject Target { get; set; }

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		base._Process(delta);
		UpdateAnimation();
		MoveTowardsDestination(delta);
		Attack();
	}

	private void Attack()
	{
		foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
		{
			if (child is IAttack && Target != null)
			{
				((IAttack)child).Attack(Target, Position);
			}
		}
	}

	private void MoveTowardsDestination(float delta)
	{
		Vector2 velocity = Target.Position - Position;

		if (velocity.Length() <= DestinationReachedTreshold)
		{
			velocity = new Vector2(0, 0);
		}

		velocity = velocity.Normalized() * MovementSpeed;
		Position += velocity * delta;
	}

	private void UpdateAnimation()
	{
		string animationName = (Hp.Hp < MaxHp / 2) ? HURT_ANIMATION : DEFAULT_ANIMATION;
		GetNode<AnimatedSprite>("AnimatedSprite").Animation = animationName;
	}
}
