using Godot;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;

public class GenericLivingObject : GenericVisibleObject, IHasHpBehavior
{

	[Export]
	public uint MaxHp = 100;

	public HpBehavior Hp { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Hp = new HpBehavior(MaxHp);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Hp.IsDead)
		{
			GD.Print(Name + " is dying");
			OnDeath();
			OnAfterDeath();
		}
	}

	/// <summary>
	/// Called when this entity dies. Implement to provide custom behavior.
	/// </summary>
	protected virtual void OnDeath()
	{
	}

	protected virtual void OnAfterDeath()
	{
		QueueFree();
	}
}
