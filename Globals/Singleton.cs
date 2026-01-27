using Godot;
using System;

public partial class Singleton : Node
{
	public class MapSizes
	{
		public static readonly Vector2I easy = new(10, 8);
		public static readonly Vector2I medium = new(16, 12);
	}
}
