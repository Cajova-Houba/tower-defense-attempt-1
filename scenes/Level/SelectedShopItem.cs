using Godot;

/// <summary>
/// This scene display the currently selected shop item under the mouse cursor. It is hidden by default
/// and made visible only if a shop item is selected.
/// </summary>
public class SelectedShopItem : Area2D
{
	private const string SPRITE_NODE = "Sprite";

	/// <summary>
	/// If set to true, the item cannot be placed.
	/// </summary>
	private bool collision = false;

	/// <summary>
	/// Currently selected shop item. May be null.
	/// </summary>
	public GenericShopItem ShopItem { get; private set; }

	public bool CanBePlaced()
	{
		return !collision;
	}

	public void ShowForItem(GenericShopItem shopItem)
	{
		ShopItem = shopItem;
		GetNode<Sprite>(SPRITE_NODE).Texture = shopItem.SelectedTexture;
		Visible = true;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Position = GetViewport().GetMousePosition();
		if (Visible)
		{
			if (collision)
			{
				GetNode<Sprite>(SPRITE_NODE).Texture = ShopItem.UnplacableTexture;
			} else
			{
				GetNode<Sprite>(SPRITE_NODE).Texture = ShopItem.SelectedTexture;
			}
		}
	}

	private void OnCollision(object area)
	{
		collision = true;
	}

	private void OnCollisionOff(object area)
	{
		collision = false;
	}

}





