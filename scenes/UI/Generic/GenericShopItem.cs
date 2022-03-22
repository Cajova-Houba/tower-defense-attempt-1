using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;

public class GenericShopItem : PanelContainer
{
	protected const string TEXTURE_NODE = "HBoxContainer/TextureRect";
	protected const string PRICE_NODE = "HBoxContainer/PriceLabel";

	/// <summary>
	/// Default texture.
	/// </summary>
	[Export]
	public Texture Texture;

	/// <summary>
	/// Texture to use when shop item is selected.
	/// </summary>
	[Export]
	public Texture SelectedTexture;

	[Export]
	public string Price = "0";

	private uint clickDetectionTime = 100;
	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer clickTimer;
	private SelectableBehavior selectableBehavior;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		selectableBehavior = new SelectableBehavior();
		clickTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer(clickDetectionTime);

		if (Texture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = Texture;
		}

		if (Price != null)
		{
			GetNode<Label>(PRICE_NODE).Text = Price;
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (selectableBehavior.IsSelected() && SelectedTexture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = SelectedTexture;
		}
		else if (!selectableBehavior.IsSelected() && Texture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = Texture;
		
		}

		if (Price != null)
		{
			GetNode<Label>(PRICE_NODE).Text = Price;
		}
	}

	private void OnGuiInput(object @event)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && clickTimer.HasPassed())
			{
				selectableBehavior.ChangeSelect();
			}
		}
	}
}



