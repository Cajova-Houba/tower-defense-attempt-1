using Godot;
using System;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class Level : Node
{
	private const string BASE = "Base";
	private const string HUD_NODE = "HUD";
	private const string SELECTED_SHOP_ITEM = "SelectedShopItem";
	private const string CONTROLS_NODE = "Controls";

	/// <summary>
	/// Player's money at the start of level.
	/// </summary>
	[Export]
	public uint InitMoney = 50;

	/// <summary>
	/// Entity that is currently selected. May be null.
	/// </summary>
	private ISelectableEntity selectedEntity;

	/// <summary>
	/// Player's money in this level.
	/// </summary>
	private uint money;

	/// <summary>
	/// How many enemies were killed in this level.
	/// </summary>
	private uint kills;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<WaveSpawner>("WaveSpawner").Target = GetNode<GenericLivingObject>("Base");
		selectedEntity = null;
		SetMoney(InitMoney);
		SetKills(0);
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (!IsBaseAlive())
		{
			GameOver();
		} else
		{
			GetNode<HUD>(HUD_NODE).ShowBaseHp(GetBaseHp());
			ShowEnemyCount();
			HandlePressedAction();
		}
	}


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
			GetNode<HUD>(HUD_NODE).SelectItem(entity);

			if (entity is GenericShopItem shopItem)
			{
				GetNode<SelectedShopItem>(SELECTED_SHOP_ITEM).ShowForItem(shopItem);
			}
		}
	}

	/// <summary>
	/// Callback for signal emitted when enemy is killed.
	/// </summary>
	/// <param name="enemy">Killed enemy.</param>
	public void OnEnemyKilled(Enemy enemy)
	{
		SetMoney(money + enemy.RewardMoney);
		SetKills(kills + 1);
		ShowEnemyCount();
	}

	/// <summary>
	/// Upgrade selected item. Item can be upgraded only if palyer has enough money.
	/// </summary>
	public void UpgradeSelected()
	{
		if (selectedEntity != null && selectedEntity is IUpgradable upgradable)
		{
			if (HasPlayerEnoughMoney(upgradable.UpgradePrice))
			{
				SetMoney(money - upgradable.UpgradePrice);
				upgradable.Upgrade();
				GetNode<HUD>(HUD_NODE).SelectItem(selectedEntity);
			}
		}
	}
	
	private void ShowEnemyCount()
    {
		GetNode<HUD>(HUD_NODE).ShowEnemyCount((uint)GetTree().GetNodesInGroup(GameConstants.ENEMIES_GROUP).Count);
	}

	/// <summary>
	/// Check whether the base still lives and if not show game over.
	/// </summary>
	/// <returns>False if the base is dead.</returns>
	private bool IsBaseAlive()
	{
		GenericLivingObject playerBase = (GenericLivingObject)FindNode(BASE);
		return IsInstanceValid(playerBase) && !playerBase.IsQueuedForDeletion() && !playerBase.Hp.IsDead;
	}

	/// <summary>
	/// Assumes the base exists and is valid.
	/// </summary>
	/// <returns></returns>
	private uint GetBaseHp()
	{
		return GetNode<GenericLivingObject>(BASE).Hp.Hp;
	}

	/// <summary>
	/// Show game over, stop game etc.
	/// </summary>
	private void GameOver()
	{
		GetNode<HUD>(HUD_NODE).ShowGameOver();
	}

	private Controls GetControls()
	{
		return GetNode<Controls>(CONTROLS_NODE);
	}

	/// <summary>
	/// Use controls to get currently pressed action and handle it.
	/// </summary>
	private void HandlePressedAction()
	{
		string action = GetControls().GetPressedAction();
		if (GameConstants.UPGRADE_ACTION == action)
		{
			UpgradeSelected();
		}
		else if (GameConstants.SHOP_TOWER_ACTION == action)
		{
			GetNode<HUD>(HUD_NODE).SelectTowerInShop();
		} else if (GameConstants.SHOP_OBSTACLE_ACTION == action)
		{
			GetNode<HUD>(HUD_NODE).SelectObstacleInShop();
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
			BuyShopItem(selectedShopItem);
			
		}
	}

	/// <summary>
	/// Buys the shop item - places it on the map, subtracts money, clears selection in HUD.
	/// </summary>
	/// <param name="selectedShopItem">Shop item</param>
	private void BuyShopItem(SelectedShopItem selectedShopItem)
	{
		GenericVisibleObject newObject = (GenericVisibleObject)selectedShopItem.ShopItem.ItemScene.Instance();
		newObject.Position = selectedShopItem.Position;
		AddChild(newObject);
		DeselectEntities();
		GetNode<HUD>(HUD_NODE).ClearItemStatsDisplay();
		SetMoney(money - selectedShopItem.ShopItem.Price);
	}

	/// <summary>
	/// Check if the player has enough money to pay given price.
	/// </summary>
	/// <param name="price">Price to pay.</param>
	/// <returns>True if the player has enough money to pay the price.</returns>
	private bool HasPlayerEnoughMoney(uint price)
	{
		return price <= money;
	}

	/// <summary>
	/// Check if anything is selected and if it's a shop item.
	/// </summary>
	/// <returns></returns>
	private bool IsShopItemSelected()
	{
		return selectedEntity != null && selectedEntity is GenericShopItem;
	}

	/// <summary>
	/// Set new value and update HUD.
	/// </summary>
	/// <param name="newMoney">New money.</param>
	private void SetMoney(uint newMoney)
	{
		money = newMoney;
		GetNode<HUD>(HUD_NODE).ShowMoney(money);
	}

	/// <summary>
	/// Set new value and update HUD.
	/// </summary>
	/// <param name="newKills">New kills.</param>
	private void SetKills(uint newKills)
	{
		kills = newKills;
		GetNode<HUD>(HUD_NODE).ShowKills(newKills);
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
