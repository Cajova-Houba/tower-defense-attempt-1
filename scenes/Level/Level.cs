using Godot;
using System;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class Level : Node
{

	private const string HUD_NODE = "HUD";

	/// <summary>
	/// Entity that is currently selected. May be null.
	/// </summary>
	private ISelectableEntity selectedEntity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<WaveSpawner>("WaveSpawner").Target = GetNode<GenericLivingObject>("Base");
		selectedEntity = null;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	/// <summary>
	/// Handler for signal from selectable entities. 
	/// </summary>
	/// <param name="entity">Selected entity.</param>
	public void OnEntitySelectionChanged(ISelectableEntity entity)
	{
		bool entitySelected = entity.Selection.IsSelected();
		DeselectEntities();
		GetNode<HUD>(HUD_NODE).ClearItemStatsDisplay();

		if (entitySelected)
		{
			entity.Selection.Select();
			selectedEntity = entity;
			GetNode<HUD>(HUD_NODE).ShowItemStats(entity);
		}
	}

	private void DeselectEntities()
	{
		selectedEntity = null;
		foreach(Node n in GetTree().GetNodesInGroup(GameConstants.SELECTABLE_GROUP))
		{
			if (n is ISelectableEntity entity)
			{
				entity.Deselect();
			}
		}
	}
}
