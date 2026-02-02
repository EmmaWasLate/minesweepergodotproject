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

	SpinBox rowsBox;
	SpinBox columsBox;
	SpinBox bombsBox;
    public override void _Ready()
    {
		rowsBox = GetNode<SpinBox>(rowsBoxPath);
		columsBox = GetNode<SpinBox>(ColumnsBoxPath);
		bombsBox = GetNode<SpinBox>(BombsBoxPath);
    }

	public void _ButtonDown_NewGame()
	{
		baseMenuContainer.Visible = false;
		difficultyMenuContainer.Visible = true;
	}

	public void _ButtonDown_SelectDifficulty(int difficulty)
	{
		if (difficulty == (int)Singleton.Difficulty.custom)
		{
			difficultyMenuContainer.Visible = false;
			customDifficultyMenuContainer.Visible = true;
			return;
		}
		GetNode<SceneManager>("/root/SceneManager").StartMainLevel((Singleton.Difficulty)difficulty);
	}

	public void _ButtonDown_CustomDifficulty()
	{
		if(!CanPlayCustom()) return;

		Vector2I mapSize = new((int)rowsBox.Value,(int)columsBox.Value);
		Singleton.DifficultyInfo.custom = new(mapSize,(int)bombsBox.Value);

		Singleton.DifficultyInfo.UpdateDictionaryObj();
		
		GetNode<SceneManager>("/root/SceneManager").StartMainLevel(Singleton.Difficulty.custom);
	}

	public void _ValueChanged_ValuesWarn(float _)
	{
		Label smallSizeWarn = GetNode<Label>("%SmallSizeWarn");
		Label bombsWarn = GetNode<Label>("%BombsWarn");

		GD.Print(smallSizeWarn.Name);
		GD.Print(bombsWarn.Name);
		smallSizeWarn.Visible = rowsBox.Value * columsBox.Value < 2;
		bombsWarn.Visible = bombsBox.Value >= rowsBox.Value * columsBox.Value - 10;
	}

	public bool CanPlayCustom()
	{
		return rowsBox.Value * columsBox.Value > 2 && bombsBox.Value < rowsBox.Value * columsBox.Value - 10;
	}
}