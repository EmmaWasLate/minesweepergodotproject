using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Godot;

public partial class Manager : Node2D
{
	Random random = new();
	[Export] public TileMapLayer ground;
	[Export] public TileMapLayer flags;
	Vector2I mapSize;
	int bombAmount;

	int[,] tileInfo = new int[0, 0];
	public void CreateTileMap()
	{
		int xOffset = (mapSize.X/2);
		int yOffset = (mapSize.Y/2);
		for (int i = 0; i < mapSize.X; i++)
		{
			for (int j = 0; j < mapSize.Y; j++)
			{
				ground.SetCell(new(i - xOffset, j - yOffset), 0, new(0, 0));
			}
		}

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

		ground.Scale = new(scaling, scaling);
		flags.Scale = new(scaling, scaling);

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

		ground.Position = new (xPosition, yPosition);
		flags.Position = new (xPosition, yPosition);
	}

	public void CreateTileInfo()
	{
		int counter = 0;
		while (counter < bombAmount)
		{
			int x = random.Next(mapSize.X);
			int y = random.Next(mapSize.Y);

			if (tileInfo[x, y] != -1)
			{
				tileInfo[x, y] = -1;
				counter++;
			}
		}

		for (int i = 0; i < mapSize.X; i++)
		{
			for (int j = 0; j < mapSize.Y; j++)
			{
				counter = 0;
				if (tileInfo[i, j] != -1)
				{
					for (int h = -1; h < 2; h++)
					{
						for (int l = -1; l < 2; l++)
						{
							if (i + h >=0 && i + h < mapSize.X && j + l < mapSize.Y && j + l >= 0)
							{
								if (tileInfo [i + h, j + l] == -1)
								{
									counter++;
								}
							}
							
						}
					}
					tileInfo[i, j] = counter;
				}
			}
		}
	}

    public override void _Ready()
    {
		mapSize = Singleton.DifficultyInfo.DifficultyToMapSize(Singleton.difficulty);
		bombAmount = Singleton.DifficultyInfo.DifficultyToBombAmount(Singleton.difficulty);
		tileInfo = new int[mapSize.X, mapSize.Y];
     	CreateTileMap();
		CreateTileInfo();
    }
	public override void _Process(double delta)
    {
		float scaling = GetScaling();
        if (Input.IsActionJustPressed("left_mouse"))
		{
			int xOffset = (mapSize.X/2);
			int yOffset = (mapSize.Y/2);
			Vector2 offset = new(0, 0);
			if (mapSize.X % 2 != 0)
			{
				offset.X = .5f;
			}

			if (mapSize.Y % 2 != 0)
			{
				offset.Y = .5f;
			}

			Vector2I tileCoords = ground.LocalToMap(offset * 16 + GetGlobalMousePosition() / scaling);

			if (ground.GetCellAtlasCoords(tileCoords).Equals(new(0, 0)) && !flags.GetCellAtlasCoords(tileCoords).Equals(new(0, 0)))
			{
				ground.SetCell(tileCoords, 0, new(1, 0));
				if (tileInfo[tileCoords.X + xOffset, tileCoords.Y + yOffset] == -1)
				{
					flags.SetCell(tileCoords, 0, new(9, 0));
				} else if (tileInfo[tileCoords.X + xOffset, tileCoords.Y + yOffset] != 0)
				{
					flags.SetCell(tileCoords, 0, new(tileInfo[tileCoords.X + xOffset, tileCoords.Y + yOffset], 0));
				}
			}
		}

		if (Input.IsActionJustPressed("right_mouse"))
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

			Vector2I tileCoords = flags.LocalToMap(offset * 16 + GetGlobalMousePosition() / scaling);

			if (ground.GetCellAtlasCoords(tileCoords).Equals(new(0, 0)))
			{
				if (flags.GetCellAtlasCoords(tileCoords).Equals(new(0, 0)))
				{
					flags.SetCell(tileCoords, 0, new(-1, -1));
				} else
				{
					flags.SetCell(tileCoords, 0, new(0, 0));
				}
				
			}
		}
    }
}