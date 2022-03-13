using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;

public class GenericLivingObject : Area2D, IHasHpBehavior
{
	protected const string COLLISION_NODE = "CollisionShape2D";

	[Export]
	public uint MaxHp = 100;

	public HpBehavior Hp { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hp = new HpBehavior(MaxHp);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Hp.IsDead)
		{
			QueueFree();
		}
	}
}