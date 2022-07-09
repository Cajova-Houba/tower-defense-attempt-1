using Godot;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.scenes.Attack;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class HUD : CanvasLayer
{
	private const string ITEM_STATS_NODE = "SidePanel/Items/ItemStats";
	private const string STATS_NODE = "SidePanel/Items/Stats";
	private const string UPGRADE_BUTTON = "UpgradeButton";
	private const string SHOP_TOWER = "SidePanel/Items/Shop/ShopItems/Tower";
	private const string SHOP_OBSTACLE = "SidePanel/Items/Shop/ShopItems/Obstacle";
	private const string ENEMY_MODIFIER_WINDOW = "EnemyModifierWindow";

	private const string GAME_OVER = "GameOverPanel";

	private object selectedItem;

	public void ShowMoney(uint money)
	{
		GetNode<Label>(STATS_NODE+"/Money").Text = money.ToString();
	}

	public void ShowKills(uint kills)
	{
		GetNode<Label>(STATS_NODE + "/Kills").Text = kills.ToString();
	}

	public void ShowEnemyCount(uint enemyCount)
	{
		GetNode<Label>(STATS_NODE + "/Enemies").Text = enemyCount.ToString();
	}

	public void ShowBaseHp(uint baseHp)
	{
		GetNode<Label>(STATS_NODE + "/BaseHp").Text = baseHp.ToString();
	}

	public void ShowGameOver()
	{
		GetNode<Panel>(GAME_OVER).Show();
	}

	public void HideGameOver()
	{
		GetNode<Panel>(GAME_OVER).Hide();
	}

	public void HideEnemyModifierWindow()
	{
		GetNode<Panel>(ENEMY_MODIFIER_WINDOW).Hide();
	}

	public void ShowEnemyModifierWindow()
	{
		GetNode<EnemyModifierWindow>(ENEMY_MODIFIER_WINDOW).ShowWithRandomModifiers();
	}

	/// <summary>
	/// Select item to show statistics its statistics.
	/// </summary>
	/// <param name="item">Item to select and show stats of</param>
	public void SelectItem(object item)
	{
		selectedItem = item;
	}

	public void ClearItemStatsDisplay()
	{
		selectedItem = null;
		GridContainer statsNode = GetNode<GridContainer>(ITEM_STATS_NODE);
		HideItemStats(statsNode);
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		ShowSelectedItem();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Panel>(GAME_OVER).Hide();
		GetNode<EnemyModifierWindow>(ENEMY_MODIFIER_WINDOW).Hide();
		ClearItemStatsDisplay();
	}

	public void SelectTowerInShop()
	{
		GetNode<GenericShopItem>(SHOP_TOWER).SelectItem();
	}

	public void SelectObstacleInShop()
	{
		GetNode<GenericShopItem>(SHOP_OBSTACLE).SelectItem();
	}

	/// <summary>
	/// Show stats of selected item (if any).
	/// </summary>
	private void ShowSelectedItem()
	{
		GridContainer statsNode = GetNode<GridContainer>(ITEM_STATS_NODE);

		if (selectedItem == null)
		{
			return;
		}

		if (selectedItem is Tower tower)
		{
			GenericAttack attack = tower.GetAttack();
			if (attack != null)
			{
				ShowStat(1, "Attack", attack.Damage.ToString());
				ShowStat(2, "Speed", attack.AttackSpeed.ToString());
			}
		}
		else if (selectedItem is Obstacle obstacle)
		{
			ShowStat(1, "HP", obstacle.Hp.Hp.ToString());
		}

		if (selectedItem is IUpgradable upgradable)
		{
			ShowUpgradeButton(statsNode);
			ShowUpgradePrice(upgradable.UpgradePrice);
		}
	}

	private void HideItemStats(GridContainer statsContainer)
	{
		foreach(Node n in statsContainer.GetChildren())
		{
			if (n is Control control)
			{
				control.Hide();
			}
		}
	}

	private void ShowUpgradePrice(uint price)
	{
		GetNode<Label>(ITEM_STATS_NODE + "/PriceLabel").Visible = true;
		SetLabelText(ITEM_STATS_NODE + "/Price", price.ToString());
	}

	private void ShowStat(uint statNum, string label, string value)
	{
		SetLabelText(ITEM_STATS_NODE + $"/Stat{statNum}Label", label);
		SetLabelText(ITEM_STATS_NODE + $"/Stat{statNum}", value);
	}

	private void SetLabelText(string nodePath, string text)
	{
		Label l = GetNode<Label>(nodePath);
		if (l != null)
		{
			GetNode<Label>(nodePath).Text = text;
			GetNode<Label>(nodePath).Show();
		}
	}

	private void ShowUpgradeButton(GridContainer statsContainer)
	{
		statsContainer.GetNode<Button>(UPGRADE_BUTTON).Show();
	}

	private void OnUpgradeButton()
	{
		GetNode<Level>(GameConstants.LEVEL_NODE).UpgradeSelected();
	}

	private void OnRetryButtonPressed()
	{
		HideGameOver();
		GetTree().Paused = false;
		GetNode<Level>(GameConstants.LEVEL_NODE).Reset();
	}


	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}




