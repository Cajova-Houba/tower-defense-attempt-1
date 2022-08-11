using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class GenericVisibleObject : Area2D, ISelectableEntity
{
	protected const string ANIMATION_NODE = "AnimatedSprite";
	protected const string COLLISION_NODE = "CollisionShape2D";


	/// <summary>
	/// Milliseconds.
	/// </summary>
	[Export]
	public uint ClickDetectionTime = 100;

	/// <summary>
	/// Selection details of this entity.
	/// </summary>
	public SelectableBehavior Selection { get; private set; }

	/// <summary>
	/// Use this variable to control whether the selection of this entity is allowed or not. If it is allowed, the entity will emit 
	/// SelectionChanged signal on click.
	/// 
	/// Turned off by default.
	/// </summary>
	private bool selectionAllowed = false;
	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer clickTimer;
	private Level currentLevel;

	public override void _Ready()
	{
		base._Ready();
		AddToGroup(GameConstants.SELECTABLE_GROUP);
		clickTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer(ClickDetectionTime);
		Selection = new SelectableBehavior();
		ConnectSelectionSignal();
	}


	public AnimatedSprite GetAnimationNode()
	{
		return GetNode<AnimatedSprite>(ANIMATION_NODE);
	}

	public void Deselect()
	{
		Selection.Deselect();
	}

	protected void AllowSelection()
	{
		selectionAllowed = true;
	}
	protected void DisableSelection()
	{
		selectionAllowed = false;
	}

	/// <summary>
	/// Callback for when this object is clicked on.
	/// </summary>
	protected virtual void OnClick()
	{
		if (selectionAllowed && currentLevel != null)
		{
			Selection.ChangeSelect();
			currentLevel.OnEntitySelectionChanged(this);
		}
	}

	protected void OnInput(object viewport, object @event, int shape_idx)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && inputEventMouseButton.IsActionReleased(GameConstants.LEFT_MOUSE_CLICK) && clickTimer.HasPassed())
			{
				OnClick();
			}
		}
	}

	/// <summary>
	/// Attempt to connecting the SelectionChanged signal to handler in Level class.
	/// </summary>
	private void ConnectSelectionSignal()
	{
		Level level = GetNodeOrNull<Level>(GameConstants.LEVEL_NODE);
		if (level != null)
		{
			currentLevel = level;
		}
	}

}



