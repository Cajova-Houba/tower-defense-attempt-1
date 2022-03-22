using Godot;
using System;
using TowerDefenseAttempt1.src.org.valesz.towerdefatt.Core;

public class Obstacle : GenericLivingObject
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private SelectableBehavior selectableBehavior;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        selectableBehavior = new SelectableBehavior();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        selectableBehavior.UpdateAimation(this);
    }
}
