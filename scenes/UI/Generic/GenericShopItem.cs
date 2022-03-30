using Godot;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class GenericShopItem : PanelContainer, ISelectableEntity
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

	/// <summary>
	/// Texture to use when this item is being placed in incorrect place.
	/// </summary>
	[Export]
	public Texture UnplacableTexture;

	/// <summary>
	/// Scene that is used to create new item and place it into the level when this item is bought. 
	/// </summary>
	[Export]
	public PackedScene ItemScene;

	[Export]
	public uint Price = 0;

	/// <summary>
	/// Selection details of this entity.
	/// </summary>
	public SelectableBehavior Selection { get; private set; }

	private uint clickDetectionTime = 100;
	private Level currentLevel;
	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer clickTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Selection = new SelectableBehavior();
		AddToGroup(GameConstants.SELECTABLE_GROUP);
		clickTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer(clickDetectionTime);
		ConnectSelectionSignal();

		if (Texture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = Texture;
		}

		GetNode<Label>(PRICE_NODE).Text = Price.ToString();
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Selection.IsSelected() && SelectedTexture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = SelectedTexture;
		}
		else if (!Selection.IsSelected() && Texture != null)
		{
			GetNode<TextureRect>(TEXTURE_NODE).Texture = Texture;
		
		}

		GetNode<Label>(PRICE_NODE).Text = Price.ToString();
	}
	public void Deselect()
	{
		Selection.Deselect();
	}

	private void OnGuiInput(object @event)
	{
		if (@event is InputEventMouseButton inputEventMouseButton)
		{
			if (inputEventMouseButton.ButtonIndex == ((int)ButtonList.Left) && inputEventMouseButton.IsActionReleased(GameConstants.LEFT_MOUSE_CLICK) && clickTimer.HasPassed() && currentLevel != null)
			{
				Selection.ChangeSelect();
				currentLevel.OnEntitySelectionChanged(this);
			}
		}
	}

	/// <summary>
	/// Attempt to connecting the SelectionChanged signal to handler in Level class.
	/// </summary>
	private void ConnectSelectionSignal()
	{
		Level currentLevel = GetNode<Level>(GameConstants.LEVEL_NODE);
		
		if (currentLevel != null)
		{
			this.currentLevel = currentLevel;
		}
	}

}



