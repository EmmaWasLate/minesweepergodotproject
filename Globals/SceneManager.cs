using Godot;
using System;

public partial class SceneManager : Node
{
	string mainLevelPath = "res://Stages/Level/Scenes/MainLevel.tscn";
	string mainMenuPath = "res://Stages/MainMenu/Scenes/MainMenu.tscn";
	public void StartMainLevel(Singleton.Difficulty difficulty)
	{
		Singleton.difficulty = difficulty;
		
		GetTree().ChangeSceneToFile(mainLevelPath);
	}
	public void GoToMainMenu()
	{
		Singleton.difficulty = 0;
		Singleton.DifficultyInfo.custom = new(new(2,2),1);

		GetTree().ChangeSceneToFile(mainMenuPath);
	}
}
