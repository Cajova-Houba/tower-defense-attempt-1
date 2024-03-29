using Godot;
using System;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.scenes.Attack;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class Tower : GenericVisibleObject, IUpgradable
{
	
	private const string ATTACKS_NODE = "Attacks";
	private const string ATTACK_POSITION_NODE = "AttackPosition";

	[Export]
	public uint BaseUpgradePrice = 100;

	[Export]
	public float UpgradePriceFactor = 2f;



	public uint UpgradePrice { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		AllowSelection();
		UpgradePrice = BaseUpgradePrice;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		base._Process(delta);
		UpdateAnimation();
		Attack();
		Update();
	}

	public override void _Draw()
	{
		if (Selection.IsSelected())
		{
			foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
			{
				if (child is RangedAttack)
				{
					RangedAttack attack = (RangedAttack)child;
					DrawCircle(Vector2.Zero, attack.Range, new Color(0,0,0,0.5f));
				}
			}
		}
	}

	public GenericAttack GetAttack()
	{
		// todo: refactor when attacks are finally decided
		foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
		{
			if (child is GenericAttack attack)
			{
				return attack;
			}
		}

		return null;
	}

	public void Upgrade()
	{
		UpgradePrice = (uint)(UpgradePrice * UpgradePriceFactor);
		foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
		{
			if (child is GenericAttack attack)
			{
				attack.Upgrade();
			}
		}
	}

	private void Attack()
	{
		Enemy target = PickTarget();
		Vector2 attackStart = Position + GetAttackPosition();
		foreach (object child in GetNode<Node>(ATTACKS_NODE).GetChildren())
		{
			if (child is GenericAttack && target != null)
			{
				((GenericAttack)child).Attack(target, attackStart);
			}
		}
	}

	private Vector2 GetAttackPosition()
	{
		return GetNode<Position2D>(ATTACK_POSITION_NODE).Position;
	}

	/// <summary>
	/// Selects enemy to attack.
	/// </summary>
	/// <returns>Selected enemy to be attacked.</returns>
	private Enemy PickTarget()
	{
		Enemy target = null;
		float minDist = float.MaxValue;
		foreach(object node in GetTree().GetNodesInGroup(GameConstants.ENEMIES_GROUP)) {
			if (node is Enemy)
			{
				Enemy enemy = (Enemy)node;
				float dist = (Position - enemy.Position).LengthSquared();

				if (minDist == float.MaxValue || dist < minDist)
				{
					target = enemy;
					minDist = dist;
				}
			}
		}

		return target;
	}

	private void UpdateAnimation()
	{
		Selection.UpdateAimation(this);
	}


}
