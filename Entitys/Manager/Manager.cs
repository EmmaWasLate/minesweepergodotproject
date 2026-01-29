using Godot;

public partial class Manager : Node2D
{
	[Export] public TileMapLayer tilemaplayer;

	public void CreateTileMap(Vector2I mapSize)
	{
		int xOffset = (mapSize.X/2);
		int yOffset = (mapSize.Y/2);
		for (int i = 0; i < mapSize.X; i++)
		{
			for (int j = 0; j < mapSize.Y; j++)
			{
				tilemaplayer.SetCell(new(i - xOffset, j - yOffset), 0, new(0, 0));
			}
		}

		tilemaplayer.SetCell(new(0, 0), 0, new(0, 0));

		PositionTileMap(mapSize);
	}

	public void PositionTileMap(Vector2I mapSize)
	{
		//Cria a variavel de Scaling de x e y
		float xScaling = ((float)DisplayServer.WindowGetSize().X - 0) / (mapSize.X * 16);
		float yScaling = ((float)DisplayServer.WindowGetSize().Y - 0) / (mapSize.Y * 16);
		float scaling = 0;
		//Verifica qual Scaling eh menor e aplica no tilemap
		if (xScaling < yScaling)
		{
			scaling = xScaling;
		} else
		{
			scaling = yScaling;
		}

		tilemaplayer.Scale = new(scaling, scaling);

		//Posiciona o tilemap no centro da tela
		float xPosition = 0;
		float yPosition = 0;

		if (mapSize.X % 2 != 0)
		{
			xPosition -= 0.5f * scaling * 16;
		}

		if (mapSize.Y % 2 != 0)
		{
			yPosition -= 0.5f * scaling * 16;
		}

		tilemaplayer.Position = new (xPosition, yPosition);
		
	}

    public override void _Ready()
    {
		Vector2I mapSize = Singleton.MapSizes.DifficultyToMapSize(Singleton.difficulty);
     	CreateTileMap(mapSize);
    }
}
