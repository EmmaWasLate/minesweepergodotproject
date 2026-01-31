using Godot;
using System;

public partial class MainMenu : MarginContainer
{

	[ExportGroup("BaseMenu")]
	[Export] public MarginContainer baseMenuContainer;	

	[ExportGroup("DifficultyMenu")]
	[Export] public MarginContainer difficultyMenuContainer;
	
	[ExportGroup("CustomDifficultyMenu")]
	[Export] public MarginContainer customDifficultyMenuContainer;

	string rowsBoxPath = "%RowsBox";
	string ColumnsBoxPath = "%ColumsBox";
	string BombsBoxPath = "%BombsBox";

	public void _ButtonDown_NewGame()
	{
		baseMenuContainer.Visible = false;
		difficultyMenuContainer.Visible = true;
	}

	public void _ButtonDown_SelectDifficulty(int difficulty)
	{
		GetNode<SceneManager>("/root/SceneManager").StartMainLevel((Singleton.Difficulty)difficulty);
	}

	public void _ButtonDown_CustomDifficulty()
	{
		SpinBox rowsBox = GetNode<SpinBox>(rowsBoxPath);
		SpinBox columsBox = GetNode<SpinBox>(ColumnsBoxPath);
		SpinBox bombsBox = GetNode<SpinBox>(BombsBoxPath);

		Vector2I mapSize = new((int)rowsBox.Value,(int)columsBox.Value);

		Singleton.DifficultyInfo.custom = new(mapSize,(int)bombsBox.Value);
		
		GetNode<SceneManager>("/root/SceneManager").StartMainLevel(Singleton.Difficulty.custom);
	}
}