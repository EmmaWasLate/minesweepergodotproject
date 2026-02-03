using Godot;
using System;

public partial class MainMenu : MarginContainer
{

	[ExportGroup("BaseMenu")]
	[Export] public MarginContainer baseMenuContainer;	
	[Export] public MarginContainer creditsMenuContainer;	

	[ExportGroup("DifficultyMenu")]
	[Export] public MarginContainer difficultyMenuContainer;
	
	[ExportGroup("CustomDifficultyMenu")]
	[Export] public MarginContainer customDifficultyMenuContainer;


	MarginContainer lastMenu;
	private MarginContainer _currentMenu;
	MarginContainer currentMenu {get => _currentMenu; set
		{
			lastMenu = currentMenu;
			_currentMenu = value;

			currentMenu.Visible = true;
		}}

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

		currentMenu = baseMenuContainer;
    }
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_return") && lastMenu != null && currentMenu != null)
		{
			currentMenu.Visible = false;
			currentMenu = lastMenu;
			lastMenu = baseMenuContainer;
		}
    }

	public void _ButtonDown_NewGame()
	{
		baseMenuContainer.Visible = false;
		currentMenu = difficultyMenuContainer;
	}
	public void _ButtonDown_CreditsMenu()
	{
		baseMenuContainer.Visible = false;
		currentMenu = creditsMenuContainer;
	}

	public void _ButtonDown_SelectDifficulty(int difficulty)
	{
		if (difficulty == (int)Singleton.Difficulty.custom)
		{
			difficultyMenuContainer.Visible = false;
			currentMenu = customDifficultyMenuContainer;
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

		smallSizeWarn.Visible = rowsBox.Value * columsBox.Value < 2;
		bombsWarn.Visible = bombsBox.Value >= rowsBox.Value * columsBox.Value - 10;
	}

	public bool CanPlayCustom()
	{
		return rowsBox.Value * columsBox.Value > 2 && bombsBox.Value < rowsBox.Value * columsBox.Value - 10;
	}
}