using Godot;
using System;

public partial class Manager : Node2D
{
	[Export] public TileMapLayer tilemaplayer;
	public Vector2 mapSize = new(8, 10);

	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
	}
}
