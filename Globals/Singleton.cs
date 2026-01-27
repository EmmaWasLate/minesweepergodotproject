using Godot;
using System;

public partial class Singleton : Node
{
	public class MapSizes
	{
		public static readonly Vector2 easy = new(8, 10);
		public static readonly Vector2 medium = new(12, 16);
	}
}
