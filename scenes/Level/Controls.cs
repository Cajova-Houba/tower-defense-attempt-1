using Godot;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

/// <summary>
/// Simple node used to handle keyboard inputs - mainly timeouts between them.
/// </summary>
public class Controls : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	/// <summary>
	/// How long to wait before registering key presses again. In ms.
	/// </summary>
	[Export]
	public long keyPressCoolDown = 500;

	/// <summary>
	/// A map of action name => last press timestamp.
	/// </summary>
	Dictionary<string, long> coolDowns;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		coolDowns = new Dictionary<string, long>();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	/// <summary>
	/// Returns the action that is currently pressed. Multi-action input is ignored and only 
	/// one action is returned. 
	/// </summary>
	/// <returns>Name fo the action or GameConstants.NO_ACTION</returns>
	public string GetPressedAction()
	{
		foreach(string actionName in GameConstants.ACTIONS)
		{
			if (IsActionPressed(actionName))
			{
				return actionName;
			}
		}

		return GameConstants.NO_ACTION;
	}

	/// <summary>
	/// Checks whether the given action is pressed and if it has cooled down.
	/// </summary>
	/// <param name="actionName">Name of the action to check.</param>
	/// <returns>True if the action is pressed and cooled down.</returns>
	public bool IsActionPressed(string actionName)
	{
		if (Input.IsActionPressed(actionName) && CheckCoolDown(actionName))
		{
			RegisterActionPress(actionName);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Checks if the given action has cooled down and can be pressed again.
	/// </summary>
	/// <param name="actionName"> Key to check</param>
	/// <returns>True if the key has cooled down.</returns>
	private bool CheckCoolDown(string actionName)
	{
		if (!coolDowns.ContainsKey(actionName))
		{
			return true;
		}

		long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
		return now - coolDowns[actionName] > keyPressCoolDown;
	}


	private void RegisterActionPress(string actionName)
	{
		long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

		if (coolDowns.ContainsKey(actionName))
		{
			coolDowns[actionName] = now;
		}
		else
		{
			coolDowns.Add(actionName, now);
		}

	}
}
