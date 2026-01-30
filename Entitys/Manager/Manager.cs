using System.Security.Cryptography.X509Certificates;
using Godot;

public partial class Manager : Node2D
{
	[Export] public TileMapLayer tilemaplayer;
	Vector2I mapSize;
	public void CreateTileMap()
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

		PositionTileMap();
	}

	public float GetScaling()
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

		return scaling;
	}

	public void PositionTileMap()
	{
		float scaling = GetScaling();

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
		mapSize = Singleton.MapSizes.DifficultyToMapSize(Singleton.difficulty);
     	CreateTileMap();
    }
	public override void _Process(double delta)
    {
		float scaling = GetScaling();
        if (Input.IsActionJustPressed("left_mouse"))
		{
			Vector2 offset = new(0, 0);
			if (mapSize.X % 2 != 0)
			{
				offset.X = .5f;
			}

			if (mapSize.Y % 2 != 0)
			{
				offset.Y = .5f;
			}
			
			Vector2I tileCoords = tilemaplayer.LocalToMap(offset * 16 + GetGlobalMousePosition() / scaling);

			if (tilemaplayer.GetCellAtlasCoords(tileCoords).Equals(new(0, 0)))
			{
				tilemaplayer.SetCell(tileCoords, 0, new(2, 0));
			}
			
		}
    }
}