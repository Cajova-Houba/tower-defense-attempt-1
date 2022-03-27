using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class Level : Node
{

	private const string HUD_NODE = "HUD";
	private const string SELECTED_SHOP_ITEM = "SelectedShopItem";

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

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && inputEventMouseButton.IsActionReleased(GameConstants.LEFT_MOUSE_CLICK) && IsShopItemSelected())
			{
				PlaceSelectedShopItem();
			}
		}
	}


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

			if (entity is GenericShopItem shopItem)
			{
				GetNode<SelectedShopItem>(SELECTED_SHOP_ITEM).ShowForItem(shopItem);
			}
		}
	}

	/// <summary>
	/// Tries to place the selected shop item - if possible.
	/// </summary>
	private void PlaceSelectedShopItem()
	{
		Console.WriteLine("Placing stuff");

		if (!IsShopItemSelected())
		{
			return;
		}

		SelectedShopItem selectedShopItem = GetNode<SelectedShopItem>(SELECTED_SHOP_ITEM);
		bool canBePlaced = HasPlayerEnoughMoney(selectedShopItem.ShopItem.Price) && selectedShopItem.CanBePlaced();

		if (canBePlaced)
		{
			GenericVisibleObject newObject = (GenericVisibleObject)selectedShopItem.ShopItem.ItemScene.Instance();
			newObject.Position = selectedShopItem.Position;
			AddChild(newObject);
			DeselectEntities();
			GetNode<HUD>(HUD_NODE).ClearItemStatsDisplay();
		}
	}

	/// <summary>
	/// Check if the player has enough money to pay given price.
	/// </summary>
	/// <param name="price">Price to pay.</param>
	/// <returns>True if the player has enough money to pay the price.</returns>
	private bool HasPlayerEnoughMoney(string price)
	{
		// todo: later
		return true;
	}

	/// <summary>
	/// Check if anything is selected and if it's a shop item.
	/// </summary>
	/// <returns></returns>
	private bool IsShopItemSelected()
	{
		return selectedEntity != null && selectedEntity is GenericShopItem;
	}

	private void HideSelectedShopItemNode()
	{
		GetNode<SelectedShopItem>(SELECTED_SHOP_ITEM).Visible = false;
	}

	private void DeselectEntities()
	{
		selectedEntity = null;
		HideSelectedShopItemNode();
		foreach(Node n in GetTree().GetNodesInGroup(GameConstants.SELECTABLE_GROUP))
		{
			if (n is ISelectableEntity entity)
			{
				entity.Deselect();
			}
		}
	}
}
