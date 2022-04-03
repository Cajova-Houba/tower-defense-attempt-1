using Godot;
using System;
using TowerDefenseAttempt1.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;

public class Obstacle : GenericLivingObject, IUpgradable
{

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";


	[Export]
	public uint BaseUpgradePrice = 200;

	[Export]
	public float UpgradePriceFactor = 2f;

	[Export]
	public float UpgradeHpFactor = 1.5f;

	public uint UpgradePrice { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		AllowSelection();
		UpgradePrice = BaseUpgradePrice;
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		Selection.UpdateAimation(this);
	}

	public void Upgrade()
	{
		UpgradePrice = (uint)(UpgradePrice * UpgradePriceFactor);
		MaxHp = (uint)(MaxHp * UpgradeHpFactor);
		Hp.Init(MaxHp);
	}
}
