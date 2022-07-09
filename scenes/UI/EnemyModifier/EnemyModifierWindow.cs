using Godot;
using System;
using System.Collections.Generic;

public class EnemyModifierWindow : Panel
{
	private const string MODIFIERS_NODE = "Layout/Modifiers";

	/// <summary>
	/// Pool of modifiers from which some are selected and 
	/// offered to player.
	/// 
	/// Use EnemyModifier.
	/// </summary>
	[Export]
	public PackedScene[] ModifierPool;

	/// <summary>
	/// How many modifiers to display each round.
	/// </summary>
	private int modifierCount = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void ShowWithRandomModifiers()
	{
		// remove old modifiers
		foreach (Node n in GetNode<Node>(MODIFIERS_NODE).GetChildren())
		{
			n.QueueFree();
		}

		foreach (EnemyModifier modifier in SelectRandomModifiers())
		{
			GetNode<Node>(MODIFIERS_NODE).AddChild(modifier);
		}

		Show();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	/// <summary>
	/// Select random modifiers from ModifierPool.
	/// </summary>
	/// <returns>Randomly picked modifiers.</returns>
	private EnemyModifier[] SelectRandomModifiers()
	{
		int toSelect = modifierCount < ModifierPool.Length ? modifierCount : ModifierPool.Length;

		List<int> selected = new List<int>();
		List<EnemyModifier> selectedModifiers = new List<EnemyModifier>();
		Random r = new Random();

		for (int i = 0; i < toSelect; i++)
		{
			int index = r.Next(0, ModifierPool.Length);
			while (selected.Contains(index))
			{
				index = r.Next(0, ModifierPool.Length);
			}

			selected.Add(index);
			EnemyModifier m = (EnemyModifier)ModifierPool[index].Instance();
			selectedModifiers.Add(m);
		}

		return selectedModifiers.ToArray();
	}
}
