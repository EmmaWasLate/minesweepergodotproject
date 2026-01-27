using Godot;
using Godot.Collections;
using System;

public partial class Singleton : Node
{
	// organiza as dificuldades em numeros inteiros, começando no 0.
	public static Dictionary<int, Vector2I> gameDifficulty = new Dictionary<int, Vector2I>
	{
		{0 , MapSizes.easy},
		{1 , MapSizes.medium},
	};
			
	public class MapSizes
	{
		public static readonly Vector2I easy = new(10, 8);
		public static readonly Vector2I medium = new(16, 12);
		
		
		
		// retorna o tamanho de um mapa de acordo com a dificuldade passada.
		// faz o mesmo que dictionary<Tkey,TValue>[key], porem mais legivel.
		public static Vector2I DifficultyToMapSize(int difficulty)
		{
			return gameDifficulty[difficulty];
		}	
	}
}
