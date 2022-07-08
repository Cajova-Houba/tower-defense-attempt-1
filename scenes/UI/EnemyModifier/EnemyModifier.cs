using Godot;
using System;
using TowerDefenseAttempt1.scenes.UI.EnemyModifier;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class EnemyModifier : PanelContainer
{
	public enum ModifierType
	{
		HP,
		SPEED,
		DAMAGE
	}

	protected const string TEXTURE_NODE = "VBoxContainer/TextureRect";
	protected const string INFO_NODE = "VBoxContainer/Info";

	/// <summary>
	/// Texture to use for this modifier
	/// </summary>
	[Export]
	public Texture Texture;

	/// <summary>
	/// What is modified by this particular modifier.
	/// </summary>
	[Export]
	public ModifierType Type;

	/// <summary>
	/// Value by which the attribute defined by Type is modified.
	/// </summary>
	[Export]
	public uint Value;

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Texture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = Texture;
		}
		
		GetNode<Label>(INFO_NODE).Text = "+" + Value + " " + Type;
	}

	private EnemyModifierData GetData()
	{
		return new EnemyModifierData(Type, Value);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	private void OnGuiInput(object @event)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && inputEventMouseButton.IsActionReleased(GameConstants.LEFT_MOUSE_CLICK))
			{
				GetNode<Level>(GameConstants.LEVEL_NODE).OnEnemyModifierSelected(GetData());
			}
		}
	}
}



