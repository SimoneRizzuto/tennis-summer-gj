using Godot;
using System;

public partial class KidOpponent : Enemy
{
	public override void GoToNextRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Agent 10", "res://Scenes/Enemies/AgentOpponent.tscn");
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Ben Ten (10 year old)", "res://Scenes/Enemies/KidOpponent.tscn");
	}
}
