using Godot;
using Godot.Collections;
using System;

public partial class Singleton : Node
{
	// enums são integers chiques.
	public enum Difficulty{
		easy, // valor: 0
		medium, // valor: 1
		hard, // valor: 2
		custom, // valor: 3
	}
	public static Difficulty difficulty;
	public class MapSizes
	{
		public static readonly Vector2I easy = new(11, 9);
		public static readonly Vector2I medium = new(16, 12);
		public static readonly Vector2I hard = new(24, 20);
		
		
		
		// retorna o tamanho de um mapa de acordo com a dificuldade passada.
		// faz o mesmo que dictionary<Tkey,TValue>[key], porem mais legivel.
		public static Vector2I DifficultyToMapSize(Difficulty difficulty)
		{
			return gameDifficulty[difficulty];
		}

	// organiza as dificuldades de acordo com seus enums.
	public static Dictionary<Difficulty, Vector2I> gameDifficulty = new Dictionary<Difficulty, Vector2I>
	{
		{Difficulty.easy , MapSizes.easy},
		{Difficulty.medium , MapSizes.medium},
		{Difficulty.hard , MapSizes.hard},
	};
	}
}
