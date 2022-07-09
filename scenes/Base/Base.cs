using Godot;
using System;

public class Base : GenericLivingObject
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	public void Respawn()
	{
		if (Hp != null)
		{
			Hp.Init(MaxHp);
		}
	}

	protected override void OnAfterDeath()
	{
		// do nothing
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
