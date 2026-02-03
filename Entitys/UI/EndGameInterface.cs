using Godot;
using System;

public partial class EndGameInterface : MarginContainer
{
	[Export] MarginContainer gameWonConteiner;
	[Export] MarginContainer gameLostConteiner;

	Manager manager;

    public override void _Ready()
    {
        manager = GetNode<Manager>("../%manager");

		manager.gameWon += WinGame;
		manager.gameLost += LoseGame;
    }

	private void WinGame()
	{
		gameWonConteiner.Visible = true;
	}
	private void LoseGame()
	{
		gameLostConteiner.Visible = true;
	}

	public void RetryButton()
	{
		GetTree().ReloadCurrentScene();
	}
	public void MainMenuButton()
	{
		GetNode<SceneManager>("/root/SceneManager").GoToMainMenu();
	}
}
