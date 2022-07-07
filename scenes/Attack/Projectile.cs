using Godot;
using System;

/// <summary>
/// Projectile which deals damage upon colision with enemy.
/// Disappears after leaving the screen.
/// </summary>
public class Projectile : GenericVisibleObject
{
	[Export]
	public int Speed = 300;

	/// <summary>
	/// Direction to move in. Normalized.
	/// </summary>
	private Vector2 direction;

	/// <summary>
	/// Damage to deal upon colision.
	/// </summary>
	private uint damage;

	public void Init(Vector2 currentPosition, Vector2 targetPosition, uint damage)
	{
		this.Position = currentPosition;
		this.direction = (targetPosition - currentPosition).Normalized();
		this.Rotation = Mathf.Acos(direction.Dot(new Vector2(1,0)));
		this.damage = damage;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		MoveInDirection(delta);
	}

	public uint GetHitDamage()
    {
		return damage;
    }

	private void OnScreenExit()
	{
		QueueFree();
	}

	private void MoveInDirection(float delta)
	{
		Position += (direction * Speed) * delta;
	}


}
