using Godot;
using System;

public partial class MainMenu : MarginContainer
{
	[ExportGroup("BaseMenu")]
	[Export] public MarginContainer baseMenuContainer;	
	[Export] public Button newGameButton;

	[ExportGroup("DifficultyMenu")]
	[Export] public MarginContainer difficultyMenuContainer;
	[Export] public Button easyOption;
	[Export] public Button mediumOption;
	[Export] public Button hardOption;
	[Export] public Button customOption;

	public void _ButtonDown_NewGame()
	{
		baseMenuContainer.Visible = false;
		difficultyMenuContainer.Visible = true;
	}

	public void _ButtonDown_SelectDifficulty(int difficulty)
	{
		
	}
}
