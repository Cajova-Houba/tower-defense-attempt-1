using Godot;
using System;
using System.Collections.Generic;
using TowerDefenseAttempt1.scenes.UI.EnemyModifier;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core.Util;

public class WaveSpawner : Node2D
{
	/// <summary>
	/// Name of the node to put all spawned enemies into.
	/// </summary>
	private const string SPAWNED_ENEMIES_NODE = "SpawnedEnemies";
	private const string ENEMIES_GROUP = "enemies";

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
	/// Delay between spawning enemies in one wave. In milliseconds.
	/// </summary>
	[Export]
	public int spawnDelay = 30;

	/// <summary>
	/// Target for the spawned enemies.
	/// </summary>
	public GenericLivingObject Target { get; set; }
#pragma warning restore 649


	/// <summary>
	/// How many enemies to spawn in the current wave.
	/// </summary>
	private float enemiesPerWave = 1f;

	/// <summary>
	/// Flag indicating active spawning in one wave.
	/// </summary>
	private bool spawning;

	private int enemiesSpawnedInWave;

	private bool firstWave;

	private TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer spawnTimer;

	private List<EnemyModifierData> modifiers;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spawnTimer = new TowerDefenseAttempt1.org.valesz.towerdefatt.Core.Util.Timer(spawnDelay);
		firstWave = true;
		modifiers = new List<EnemyModifierData>();
	}

	public void AddModifier(EnemyModifierData modifier)
    {
		modifiers.Add(modifier);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (AllEnemisGone())
		{
			GD.Print("All enemies gone, start spawning");
			StartSpawning();
		}

		HandleSpawning();
	}

    private void HandleSpawning()
    {
		if (EnemiesToSpawn == null || EnemiesToSpawn.Count == 0 || !spawning)
		{
			StopSpawning();
			return;
		}

		if (spawnTimer.HasPassed() && enemiesSpawnedInWave < enemiesPerWave)
        {
			GD.Print("Spawning");
			Spawn();
			GD.Print($"{enemiesSpawnedInWave} out of {enemiesPerWave:0.##} spawned");
			
			if (enemiesSpawnedInWave >= enemiesPerWave)
            {
				GD.Print($"All enemies spawned ({enemiesSpawnedInWave}), stopping spawning");
				EndWave();
				GD.Print($"{enemiesPerWave} in next wave");
            }
        }
	}

    /// <summary>
    /// Checks whether there are any live enemies and returns true if there are not any.
    /// </summary>
    /// <returns></returns>
    private bool AllEnemisGone()
	{
		return GetTree().GetNodesInGroup(ENEMIES_GROUP).Count == 0;
	}

	private void StartSpawning()
    {
		spawning = true;
		enemiesSpawnedInWave = 0;
		if (!firstWave)
        {
			GetNode<Level>(GameConstants.LEVEL_NODE).OnWaveEnd();
        }

		firstWave = false;
	}

	private void EndWave()
    {
		StopSpawning();
		enemiesSpawnedInWave = 0;
		enemiesPerWave *= EnemyGrowthFactor;
    }

	private void StopSpawning()
    {
		spawning = false;
	}

	/// <summary>
	/// Spawns one enemy in wave.
	/// </summary>
	private void Spawn()
	{
		Vector2 spawnPosition = RandomSpawnPosition();
		PackedScene toSpawn = RandomEnemyType();

		Enemy enemy = (Enemy)toSpawn.Instance();
		enemy.Position = spawnPosition;
		enemy.PrimaryTarget = Target;
		enemy.ApplyModifiers(modifiers);
		GetNode(SPAWNED_ENEMIES_NODE).AddChild(enemy);

		enemiesSpawnedInWave++;
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
