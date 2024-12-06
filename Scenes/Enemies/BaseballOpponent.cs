using Godot;
using System;

public partial class BaseballOpponent : Enemy
{
	public override void GoToNextRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tentacle of the Lake", "res://Scenes/Enemies/TentacleOpponent.tscn");
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Joe Baseball", "res://Scenes/Enemies/BaseballOpponent.tscn");
	}
}
