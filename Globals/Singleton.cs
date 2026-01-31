using Godot;
using System.Collections.Generic;

public partial class Singleton : Node
{
	public struct DifficultyStruct
	{
		public readonly Vector2I size = new();
		public readonly int bombAmount;

		public DifficultyStruct(Vector2I size, int bombAmount)
		{
			this.size = size;
			this.bombAmount = bombAmount;
		}
	}

	// enums são integers chiques.
	public enum Difficulty{
		easy, // valor: 0
		medium, // valor: 1
		hard, // valor: 2
		custom, // valor: 3
	}

	public static Difficulty difficulty;

	public class DifficultyInfo
	{
		public static readonly DifficultyStruct easy = new(new Vector2I(10, 8), 10);
		public static readonly DifficultyStruct medium = new(new Vector2I(16, 12), 40);
		public static readonly DifficultyStruct hard = new(new Vector2I(24, 20), 99);
		public static DifficultyStruct custom = new(new(2,2), 1);
		
		
		// retorna o tamanho de um mapa de acordo com a dificuldade passada.
		// faz o mesmo que dictionary<Tkey,TValue>[key], porem mais legivel.
		public static Vector2I DifficultyToMapSize(Difficulty difficulty)
		{
			return gameDifficulty[difficulty].size;
		}

		public static int DifficultyToBombAmount(Difficulty difficulty)
		{
			return gameDifficulty[difficulty].bombAmount;
		}
		// organiza as dificuldades de acordo com seus enums.
		public static Dictionary<Difficulty, DifficultyStruct> gameDifficulty = new Dictionary<Difficulty, DifficultyStruct>
		{
			{Difficulty.easy , DifficultyInfo.easy},
			{Difficulty.medium , DifficultyInfo.medium},
			{Difficulty.hard , DifficultyInfo.hard},
			{Difficulty.custom, DifficultyInfo.custom}
		};
	}
}
