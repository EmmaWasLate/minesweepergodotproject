using Godot;
using System;

public partial class SceneManager : Node
{
	string mainLevelPath = "res://Stages/Level/Scenes/MainLevel.tscn";
	public void StartMainLevel(Singleton.Difficulty difficulty)
	{
		GetTree().ChangeSceneToFile(mainLevelPath);
		// Manager manager = GetNode<Manager>("%manager");
		GD.Print(GetNode<Node2D>("/root/").Name);
		// manager.StartGame(difficulty);
	}
	
}
