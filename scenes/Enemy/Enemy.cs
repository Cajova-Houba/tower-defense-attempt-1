using Godot;
using System.Collections.Generic;
using TowerDefenseAttempt1.scenes.Attack;
using TowerDefenseAttempt1.scenes.UI.EnemyModifier;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

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
	/// How much money player gains on this enemy's dead.
	/// </summary>
	[Export]
	public uint RewardMoney = 25;

	/// <summary>
	/// Target this enemy will seek to destroy. Typically a player's base in a level.
	/// </summary>
	[Export]
	public GenericLivingObject PrimaryTarget { get; set; }

	/// <summary>
	/// Targets picked up around the route to the main Target. Currently only Obstacle which will
	/// block movement of this enemy.
	/// </summary>
	private GenericLivingObject intermediateTarget;

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public void ApplyModifiers(List<EnemyModifierData> modifiers)
	{
		foreach (EnemyModifierData modifierData in modifiers)
		{
			switch (modifierData.modifierType)
			{
				case EnemyModifier.ModifierType.HP:
					MaxHp = MaxHp + modifierData.value;
					break;
				case EnemyModifier.ModifierType.DAMAGE:
					foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
					{
						if (child is GenericAttack attack)
						{
							attack.Damage += modifierData.value;
						}
					}
					break;
				case EnemyModifier.ModifierType.SPEED:
					MovementSpeed += (int)modifierData.value;
					break;
			}
		}
	}

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

	protected override void OnDeath()
	{
		GetNode<Level>(GameConstants.LEVEL_NODE).OnEnemyKilled(this);
	}

	private void Attack()
	{
		foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
		{
			if (child is GenericAttack attack)
			{
				bool attacked = IsPrimaryTargetValid() && attack.IsTargetInRange(PrimaryTarget.Position, Position) && attack.Attack(PrimaryTarget, Position);

				// we tried to attack the main target -> no success -> try to attack intermediate if any
				if (!attacked && IsIntermediateTargetValid())
				{
					attack.Attack(intermediateTarget, Position);
				}
			}
		}
	}

	private void MoveTowardsDestination(float delta)
	{
		if (!IsPrimaryTargetValid() || IsBlockedByObstacle())
		{
			//GD.Print("Primary target not valid or blocked by obstacle");
			//GD.Print("Is blocked by obstacle: "+IsBlockedByObstacle());
			return;
		}

		Vector2 velocity = PrimaryTarget.Position - Position;

		if (velocity.Length() <= DestinationReachedTreshold)
		{
			velocity = new Vector2(0, 0);
		}

		velocity = velocity.Normalized() * MovementSpeed;
		Position += velocity * delta;
	}

	private bool IsPrimaryTargetValid()
	{
		return PrimaryTarget != null && IsInstanceValid(PrimaryTarget) && !PrimaryTarget.IsQueuedForDeletion();
	}

	private bool IsIntermediateTargetValid()
	{
		return intermediateTarget != null && IsInstanceValid(intermediateTarget) && !intermediateTarget.IsQueuedForDeletion();
	}

	private bool IsBlockedByObstacle()
	{
		return intermediateTarget != null && IsInstanceValid(intermediateTarget);
	}

	private void UpdateAnimation()
	{
		string animationName = (Hp.Hp < MaxHp / 2) ? HURT_ANIMATION : DEFAULT_ANIMATION;
		GetAnimationNode().Animation = animationName;
	}

	private void OnCollision(Area2D otherArea)
	{
		if (otherArea is Obstacle)
		{
			intermediateTarget = (Obstacle)otherArea;
		} else if (otherArea is Projectile)
		{
			Projectile p = (Projectile)otherArea;
			Hp.TakeHit(p.GetHitDamage());
			p.QueueFree();
		}
	}

}
