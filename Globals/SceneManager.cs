using Godot;
using System;

public partial class SceneManager : Node
{
	string mainLevelPath = "res://Stages/Level/Scenes/MainLevel.tscn";
	public void StartMainLevel(Singleton.Difficulty difficulty)
	{
		Singleton.difficulty = difficulty;
		
		GetTree().ChangeSceneToFile(mainLevelPath);
	}
	
}
