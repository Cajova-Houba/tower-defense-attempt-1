using Godot;
using TowerDefenseAttempt1.scenes.Attack;

public class HUD : CanvasLayer
{
	private const string ITEM_STATS_NODE = "SidePanel/Items/ItemStats";
	private const string UPGRADE_BUTTON = "UpgradeButton";

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

				ShowUpgradeButton(statsNode);
			}
		} else if (item is Obstacle obstacle)
		{
			ShowStat(1, "HP", obstacle.Hp.Hp.ToString());
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

	private void ShowStat(uint statNum, string label, string value)
	{
		SetStatĹabelText(ITEM_STATS_NODE + $"/Stat{statNum}Label", label);
		SetStatĹabelText(ITEM_STATS_NODE + $"/Stat{statNum}", value);
	}

	private void SetStatĹabelText(string nodePath, string text)
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
}
