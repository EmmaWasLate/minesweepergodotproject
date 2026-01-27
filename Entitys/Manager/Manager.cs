using Godot;
using System;

public partial class Manager : Node2D
{
	[Export] public TileMapLayer tilemaplayer;
	public override void _Ready()
	{
		float xScaling = (DisplayServer.WindowGetSize().X - 100) / (Singleton.MapSizes.easy.X * 16);
		float yScaling = (DisplayServer.WindowGetSize().Y - 100) / (Singleton.MapSizes.easy.Y * 16);
		
		if (xScaling < yScaling)
		{
			tilemaplayer.Scale = new(xScaling, xScaling);
		} else
		{
			tilemaplayer.Scale = new(yScaling, yScaling);
		}

		tilemaplayer.Position = new (DisplayServer.WindowGetSize().X/2, DisplayServer.WindowGetSize().Y/2);
	}
	
	public override void _Process(double delta)
	{
	}
}
