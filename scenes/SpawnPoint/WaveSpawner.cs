using Godot;
using System;
using System.Collections.Generic;

public class WaveSpawner : Node2D
{
	/// <summary>
	/// Name of the node to put all spawned enemies into.
	/// </summary>
	private const string SPAWNED_ENEMIES_NODE = "SpawnedEnemies";

	/// <summary>
	/// How fast the count of spawned enemies grows with each passed wave.
	/// </summary>
	[Export]
	public float EnemyGrowthFactor = 1.5f;

	/// <summary>
	/// Wave of enemies are spawned in this area.
	/// </summary>
	[Export]
	public Rect2 SpawnArea = new Rect2(new Vector2(), new Vector2());

	#pragma warning disable 649
	/// <summary>
	/// Types of enemies to spawn.
	/// </summary>
	[Export]
	public List<PackedScene> EnemiesToSpawn;

	/// <summary>
	/// Target for the spawned enemies.
	/// </summary>
	public GenericLivingObject Target { get; set; }
#pragma warning restore 649


	/// <summary>
	/// How many enemies to spawn in the current wave.
	/// </summary>
	private float enemiesPerWave = 1f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (GetNode(SPAWNED_ENEMIES_NODE).GetChildCount() == 0)
		{
			Spawn();
		}
	}

	public void Spawn()
	{
		if (EnemiesToSpawn == null || EnemiesToSpawn.Count == 0)
		{
			return;
		}

		for(uint i = 0; i < enemiesPerWave; i++)
		{
			Vector2 spawnPosition = RandomSpawnPosition();
			PackedScene toSpawn = RandomEnemyType();

			Enemy enemy = (Enemy)toSpawn.Instance();
			enemy.Position = spawnPosition;
			enemy.Target = Target;
			GetNode(SPAWNED_ENEMIES_NODE).AddChild(enemy);
		}

		enemiesPerWave *= EnemyGrowthFactor;
	}

	private PackedScene RandomEnemyType()
	{
		Random r = new Random();
		return EnemiesToSpawn[r.Next(0, EnemiesToSpawn.Count)];
	}

	private Vector2 RandomSpawnPosition()
	{
		Random r = new Random();
		return SpawnArea.Position +
			new Vector2(r.Next(0, (int)SpawnArea.Size.x), r.Next(0, (int)SpawnArea.Size.y))
		;
	}
}
