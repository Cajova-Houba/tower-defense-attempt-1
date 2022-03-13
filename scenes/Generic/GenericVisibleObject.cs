using Godot;
using System;

public class GenericVisibleObject : Area2D
{
	/// <summary>
	/// Milliseconds.
	/// </summary>
	[Export]
	public uint ClickDetectionTime = 100;

	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer clickTimer;

	public override void _Ready()
	{
		base._Ready();
		clickTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer(ClickDetectionTime);
	}

	/// <summary>
	/// Callback for when this object is clicked on.
	/// </summary>
	protected virtual void OnClick()
	{

	}

	protected void OnInput(object viewport, object @event, int shape_idx)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && clickTimer.HasPassed())
			{
				OnClick();
			}
		}
	}

}


