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

	public void ShowMoney(uint money)
	{
		GetNode<Label>(STATS_NODE+"/Money").Text = money.ToString();
	}

	public void ShowKills(uint kills)
	{
		GetNode<Label>(STATS_NODE + "/Kills").Text = kills.ToString();
	}

	public void ShowBaseHp(uint baseHp)
	{
		GetNode<Label>(STATS_NODE + "/BaseHp").Text = baseHp.ToString();
	}

	/// <summary>
	/// Show statistics of selected item.
	/// </summary>
	/// <param name="item">Item to show stats of</param>
	public void ShowItemStats(object item)
	{
		GridContainer statsNode = GetNode<GridContainer>(ITEM_STATS_NODE);


		if (item is Tower tower)
		{
			GenericAttack attack = tower.GetAttack();
			if (attack != null)
			{
				ShowStat(1, "Attack", attack.Damage.ToString());
				ShowStat(2, "Speed", attack.AttackSpeed.ToString());
			}
		}
		else if (item is Obstacle obstacle)
		{
			ShowStat(1, "HP", obstacle.Hp.Hp.ToString());
		}

		if (item is IUpgradable upgradable)
		{
			ShowUpgradeButton(statsNode);
			ShowUpgradePrice(upgradable.UpgradePrice);
		} 
	}

	public void ClearItemStatsDisplay()
	{
		GridContainer statsNode = GetNode<GridContainer>(ITEM_STATS_NODE);
		HideItemStats(statsNode);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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

}

